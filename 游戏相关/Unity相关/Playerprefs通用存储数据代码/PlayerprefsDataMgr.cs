using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 数据管理类，不需要继承MonoBehaviour
/// 
/// 统一管理数据的存储和读取
/// 
/// </summary>
public class PlayerprefsDataMgr 
{

    private static PlayerprefsDataMgr instance = new PlayerprefsDataMgr();
    private PlayerprefsDataMgr() { }
    public static PlayerprefsDataMgr Instance
    {
        get => instance;
    }

    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="data">数据对象</param>
    /// <param name="keyName">数据对象的唯一key，自己控制</param>
    public void SaveData(object data,string keyName)
    {
        #region 知识点一  获取传入数据对象的所有字段
        Type type = data.GetType();
        FieldInfo[] fieldInfos = type.GetFields();
        #endregion

        #region 知识点二  自己定义一个key的规则 进行数据存储
        //我们存储的都是通过Playerprefs来进行存储的
        //保证Key的唯一性 我们就需要自己定一个key的规则

        //这里我们定义的规则: keyName_数据类型_字段类型_字段名
        //如: player1_PlayerInfo_Int32_age
        #endregion

        #region 知识点三 遍历这些字段 进行数据存储
        string saveKeyName = "";
        FieldInfo info;
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            //对每一个字段 进行数据存储
            //得到每个字段信息
            info = fieldInfos[i];
            //通过FieldInfo可以直接获取到字段类型和 字段的名字
            //字段类型名字:info.FieldType.Name 字段的名字:info.Name
            saveKeyName = keyName
                + "_" + type.Name
                + "_" + info.FieldType.Name
                + "_" + info.Name;
            Debug.Log(saveKeyName);

            //根据我们的规则并通过Playerprefs来进行存储
            //封装函数来处理，分装了函数处理是因为后面需要递归调用
            //所有这里会封装，为什么递归？因为后续要存储Dic、List
            SaveValue(info.GetValue(data), saveKeyName);
        }
        #endregion
    }

    private void SaveValue(object value, string keyName)
    {
        //直接通过PlayerPrefs进行存储
        //根据数据类型的不同，来决定使用哪一个API来进行存储
        //因为PP只支持3中类型存储，所以其他的需要自定义:降精度、int存bool等
        Type fieldType = value.GetType();
        if (fieldType == typeof(int))
        {
            Debug.Log("存储Int"+keyName);
            PlayerPrefs.SetInt(keyName, (int)value);
        }else if (fieldType == typeof(float))
        {
            Debug.Log("存储float" + keyName);
            PlayerPrefs.SetFloat(keyName, (float)value);
        }else if (fieldType == typeof(string))
        {
            Debug.Log("存储string" + keyName);
            PlayerPrefs.SetString(keyName, (string)value);
        }else if (fieldType == typeof(bool))
        {
            Debug.Log("存储bool" + keyName);
            PlayerPrefs.SetInt(keyName, (bool)value ? 1 : 0);
        }else if (fieldType == typeof(double))
        {
            Debug.Log("存储double" + keyName);
            //很少会用到double等其他少数数据类型作为存储数据
            PlayerPrefs.SetFloat(keyName, (float)value);//上面的几乎用得最多
        }
        //如何判断泛型类的 类型呢？通过反射 判断 父子关系
        else if(typeof(IList).IsAssignableFrom(fieldType))
        {
            Debug.Log("存储List:"+keyName);
            //这一步很关键，直接转为接口对象，父类装子类
            //注意这里是value as IList,value才是值
            IList list = value as IList;
            //存储List的长度
            PlayerPrefs.SetInt(keyName, list.Count);
            //确保key唯一性
            int index = 0;
            foreach (object item in list)
            {
                SaveValue(item, keyName+index);
                ++index;
            }

        }else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            Debug.Log("存储Dictionary:" + keyName);
            //这一步很关键，直接转为接口对象，父类装子类
            //注意这里是value as IList,value才是值
            IDictionary dictionary = value as IDictionary;
            //存储List的长度
            PlayerPrefs.SetInt(keyName, dictionary.Count);
            //确保key唯一性
            int index = 0;

            foreach (object key in dictionary.Keys)
            {
                SaveValue(key, keyName + "_key_" + index);
                SaveValue(dictionary[key],keyName + "_value_" + index);
                ++index;
            }
        }
        //基本数据类型都不是，那么可能就是自定义类型
        else
        {
            SaveData(value, keyName);
        }
    }
    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="type">需要读取数据转成对象的类型，根据类型反射获取到对象存储</param>
    /// <param name="keyName">读取数据的唯一key，自己控制</param>
    /// <returns></returns>
    public object LoadData(Type type,string keyName)
    {
        //不用object对象传入，而使用type传入
        //主要是为了节约一行代码(在外部new一个对象来存储这里读取的数据)
        //传入type，在内部动态创建对象并返回出去

        object obj = Activator.CreateInstance(type);
        //往new 出来的数据中填充数据
        FieldInfo[] infos = type.GetFields();
        //自定义类型:keyName_数据类型_字段类型_字段名
        string saveKeyName = "";
        for (int i = 0; i < infos.Length; i++)
        {
            saveKeyName = keyName
                + "_" + type.Name
                + "_" + infos[i].FieldType.Name
                + "_" + infos[i].Name;

            Debug.Log(saveKeyName);
            infos[i].SetValue(obj, LoadValue(infos[i].FieldType, saveKeyName));
        }
        return obj;
    }
    /// <summary>
    /// 得到单个数据的方法
    /// </summary>
    /// <param name="fieldType">字段类型，用于判断用哪个API来读取</param>
    /// <param name="KeyName">是用于获取具体数据</param>
    /// <returns></returns>
    private object LoadValue(Type fieldType, string KeyName)
    {
        //根据字段类型判断用什么API来进行读取
        if (fieldType == typeof(int))
        {
            return PlayerPrefs.GetInt(KeyName);
        } else if (fieldType == typeof(float))
        {
            return PlayerPrefs.GetFloat(KeyName);
        } else if (fieldType == typeof(string))
        {
            return PlayerPrefs.GetString(KeyName);
        } else if (fieldType == typeof(bool))
        {
            return PlayerPrefs.GetInt(KeyName) == 1 ? true : false;
        }
        else if (fieldType == typeof(double))
        {
            return (float)PlayerPrefs.GetFloat(KeyName);
        }
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            //获取List长度
            int listCount = PlayerPrefs.GetInt(KeyName);
            //创建IList对象，传入长度
            IList list = Activator.CreateInstance(fieldType,new object[] {listCount}) as IList;
            int index = 0;
            for (int i = 0; i < listCount; i++)
            {        //GetGenericArguments获取list的泛型参数
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], KeyName + index));
                ++index;
            }
            return list;
        }else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //获取Dic长度
            int dicCount = PlayerPrefs.GetInt(KeyName);
            IDictionary dic =Activator.CreateInstance(fieldType) as IDictionary;
            //获取dic的所有泛型参数
            Type[] types = fieldType.GetGenericArguments();
            //确保key唯一性
            int index = 0;
            for (int i = 0; i < dicCount; i++)
            {
                dic.Add(
                    LoadValue(types[0], KeyName + "_key_" + index),
                    LoadValue(types[1], KeyName + "_value_" + index));
                index++;
            }
            return dic;
        }
        else
        {
           return LoadData(fieldType, KeyName);
        }
    }
}
