using System;

namespace 入门小案例
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 输入输出
            //如果ReadKey(ture),则不会把内容打印到控制台上
            //char c = Console.ReadKey().KeyChar;
            //Console.WriteLine(c);
            #endregion

            #region 控制台其他方法
            //1、清空：
            //Console.Clear();
            ////2、设置控制台大小 
            ////窗口大小，缓冲区大小(就是显示内容的部分大小）
            ////注意：
            ////1）要先设置窗口，在设置缓冲区大小
            ////2）缓冲区大小不能小于窗口的大小
            ////3）窗口的大小不能大于控制台的最大尺寸

            ////设置窗口大小
            //Console.SetWindowSize(100, 50);
            ////缓冲区大小，（可打印内容区域的宽高）
            //Console.SetBufferSize(1000, 1000);
            ////3、设置光标的位置 左上角是原点
            ////视觉上看  1y=2x
            //Console.SetCursorPosition(10, 5);
            //Console.WriteLine("123");
            ////4、设置颜色
            ////文字颜色 forguand
            //Console.ForegroundColor = ConsoleColor.Red;
            ////背景颜色
            //Console.BackgroundColor = ConsoleColor.Green;
            ////5、光标显隐
            //Console.CursorVisible = false;
            ////6、退出窗口
            //Environment.Exit(0);


            #endregion

            #region 案例练习
            //Console.BackgroundColor = ConsoleColor.Red;
            //Console.SetWindowSize(50, 25);
            //Console.SetBufferSize(50, 25);

            //int initPosX = 10;
            //int initPosY = 5;


            ////▲
            //Console.Clear();
            //Console.SetCursorPosition(initPosX, initPosY);
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.CursorVisible = false;
            //Console.WriteLine("▲");

            //while (true)
            //{
            //   char value =  Console.ReadKey().KeyChar;
            //    switch (value)
            //    {
            //        case 'w':
            //            initPosY -= 1;
            //            if (initPosY <= 0)
            //            {
            //                initPosY = 0;
            //            }
            //            break;
            //        case 's':
            //            initPosY += 1;
            //            if (initPosY >= Console.BufferHeight)
            //            {
            //                initPosY = Console.BufferHeight - 1;
            //            }
            //            break;
            //        case 'a':
            //            //中文符号 在控制台上占2个位置
            //            initPosX -= 2;
            //            if (initPosX <= 0)
            //            {
            //                initPosX = 0;
            //            }
            //            break;
            //        case 'd':
            //            initPosX += 2;
            //            if (initPosX >= Console.BufferWidth)
            //            {
            //                initPosX = Console.BufferWidth-2;
            //            }
            //            break;
            //        default:
            //            break;
            //    }
            //    Console.Clear();
            //    Console.SetCursorPosition(initPosX, initPosY);
            //    Console.WriteLine("▲");
            //}

            #endregion

            #region 随机数练习题

            //Random random = new Random();
            //int def = 10;
            //int hp = 20;
            //int attack = 0;
            //int rand = 1;
            //while (true)
            //{
            //     attack = random.Next(8, 13);
            //    Console.WriteLine("第{0}回合,玩家攻击力:{1}",rand,attack);
            //    if (attack < 10)
            //    {
            //        Console.WriteLine("伤害:"+attack+",打不动怪物!");
            //    }
            //    else
            //    {
            //        hp -= attack - def;
            //        Console.WriteLine("怪物受到伤害,生命值:"+hp);
            //        if (hp <= 0)
            //        {
            //            Console.WriteLine("怪物死亡");
            //            break;
            //        }
            //    }
            //    rand++;
            //}


            #endregion

            #region 项目实战  营救公主
            //第一步 设置舞台大小和隐藏光标
            int w = 50;
            int h = 30;
            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);

            #region 多个场景
            int nowSceneId = 1;
            bool isOver = false;//营救公主跳出内循环,最后界面用于显示内容
            bool exitScene = false;
            while (true)
            {
                switch (nowSceneId)
                {
                    case 1:
                        Console.Clear();
                        #region 1、开始场景逻辑
                        Console.SetCursorPosition(w / 2-6,8);
                        Console.WriteLine("肖恩营救公主");
                        //因为要输入，构造一个死循环，
                        //专用来处理当前开始从场景的逻辑
                        int curPpos = 1;
                        while (true)
                        {
                            Console.ForegroundColor = curPpos == 1 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.SetCursorPosition(w / 2 - 4, 10);
                            Console.WriteLine("开始游戏");
                            Console.ForegroundColor = curPpos == 0 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.SetCursorPosition(w / 2 - 4, 12);
                            Console.WriteLine("退出游戏");
                            //显示内容
                            char input = Console.ReadKey(true).KeyChar;
                            switch (input)
                            {
                                case 'w':
                                    curPpos ^= 1;
                                    break;
                                case 's':
                                    curPpos ^= 1;
                                    break;
                                case 'j':
                                    if (curPpos == 1)
                                    {
                                        exitScene = true;
                                        nowSceneId = 2;
                                    }
                                    else //否则退出
                                    {
                                        Environment.Exit(0);
                                    }
                                    break;
                            }
                            if (exitScene)
                            {
                                break;
                            }
                        }
                        #endregion
                        break;
                    case 2:
                        Console.Clear();
                        #region 2、不变的红墙
                        //■
                        Console.ForegroundColor = ConsoleColor.Red;
                        for (int i = 0; i < w; i+=2)
                        {
                            Console.SetCursorPosition(i, 0);
                            Console.Write("■");

                            Console.SetCursorPosition(i, h - 1);
                            Console.Write("■");

                            Console.SetCursorPosition(i, h - 6);
                            Console.Write("■");
                        }
                        for (int i = 0; i < h; i++)
                        {
                            Console.SetCursorPosition(0, i);
                            Console.Write("■");
                            Console.SetCursorPosition(w-2, i);
                            Console.Write("■");
                        }
                        #endregion

                        //设置boss的属性
                        #region 3、Boss属性
                        int bossX = 24;
                        int bossY = 15;
                        int bossAtkMin = 7;
                        int bossAtkMax = 13;
                        int bossHp = 100;
                        string bossIcon = "■";
                        ConsoleColor bossColor = ConsoleColor.Green;//boss的色彩
                        #endregion

                        #region 4、玩家属性
                        int playX = 4;
                        int playY = 5;
                        int playerAtkMin = 8;
                        int playerAtkMax = 12;
                        int playerHp = 100;
                        string playerIcon = "●";
                        ConsoleColor playerColor = ConsoleColor.Yellow;//play的色彩
                        char playerInput;
                        //玩家状态
                        bool isFight = false;
                        #endregion

                        #region 5、公主属性
                        int princessX = 24;
                        int princessY = 5;
                        string princessIcon = "★";
                        ConsoleColor princessColor = ConsoleColor.Blue;
                        #endregion
                        isOver = false;
                        #region 6、游戏战斗逻辑
                        //游戏场景的死循环，检测玩家输入相关循环
                        while (true)
                        {
                            if (bossHp > 0)
                            {
                                //绘制的boos图标
                                Console.SetCursorPosition(bossX, bossY);
                                Console.ForegroundColor = bossColor;//设置boss颜色
                                Console.WriteLine(bossIcon);
                            }
                            else
                            {
                                Console.SetCursorPosition(princessX, princessY);
                                Console.ForegroundColor = princessColor;
                                Console.WriteLine(princessIcon); 
                            }
                            
                            //绘制的player图标
                            Console.SetCursorPosition(playX, playY);
                            Console.ForegroundColor = playerColor;//设置player颜色
                            Console.WriteLine(playerIcon);
                            playerInput = Console.ReadKey(true).KeyChar;

                            if (isFight)
                            {
                                if (playerInput == 'j')
                                {
                                    if (playerHp <= 0)
                                    {
                                        //输掉了，直接进入游戏结束画面
                                        nowSceneId = 3;
                                        break;
                                    }
                                    else if (bossHp <= 0 )
                                    {
                                        //营救公主
                                        //擦除boss
                                        Console.SetCursorPosition(bossX, bossY);
                                        Console.WriteLine("    ");
                                        isFight = false;
                                    }
                                    else
                                    {
                                        Random random = new Random();
                                        //玩家打怪物
                                        int atkP = random.Next(playerAtkMin, playerAtkMax);
                                        bossHp -= atkP;
                                        //print info
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        //先擦除之前的信息
                                        Console.SetCursorPosition(2, h - 4);
                                        Console.Write("                                             ");
                                        Console.SetCursorPosition(2, h - 4);
                                        Console.Write("你对boss造成了{0}点伤害，boss剩余血量为{1}", atkP, bossHp);
                                        if (bossHp > 0)
                                        {
                                            //怪物打玩家
                                            atkP = random.Next(bossAtkMin, bossAtkMax);
                                            playerHp -= atkP;
                                            Console.SetCursorPosition(2, h - 3);
                                            Console.Write("                                             ");
                                            if (playerHp < 0)
                                            {
                                                Console.SetCursorPosition(2, h - 3);
                                                Console.WriteLine("很遗憾，你未能通过boss的试炼，战败！");
                                            }
                                            else
                                            {
                                                Console.SetCursorPosition(2, h - 3);
                                                Console.Write("boss对你造成了{0}点伤害，你的剩余血量为{1}", atkP, playerHp);
                                            }
                                        }
                                        else
                                        {

                                            Console.SetCursorPosition(2, h - 5);
                                            Console.Write("                                              ");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("                                              ");
                                            Console.SetCursorPosition(2, h - 3);
                                            Console.Write("                                              ");
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.Write("恭喜你打赢了boss,请前去营救公主");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("前往公主身边按j键继续");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                #region 7、玩家移动逻辑
                                //擦除前面脚步
                                Console.SetCursorPosition(playX, playY);
                                Console.WriteLine("  ");
                                switch (playerInput)
                                {
                                    case 'w':
                                        --playY;
                                        if (playY < 1) playY = 1;
                                        if (playX == bossX && playY == bossY && bossHp > 0) ++playY;
                                        if (playX == princessX && playY == princessY && bossHp <= 0) ++playY;
                                        break;
                                    case 's':
                                        ++playY;
                                        if (playY > h - 7) playY = h - 7;
                                        if (playX == bossX && playY == bossY && bossHp > 0) --playY;
                                        if (playX == princessX && playY == princessY && bossHp <= 0) --playY;
                                        break;
                                    case 'a':
                                        playX -= 2;
                                        if (playX < 2) playX = 2;
                                        if (playX == bossX && playY == bossY && bossHp > 0) playX += 2;
                                        if (playX == princessX && playY == princessY && bossHp <= 0) playX+=2;
                                        break;
                                    case 'd':
                                        playX += 2;
                                        if (playX > w - 4) playX = w - 4;
                                        if (playX == bossX && playY == bossY && bossHp > 0) playX -= 2;
                                        if (playX == princessX && playY == princessY && bossHp <= 0) playX -= 2;
                                        break;
                                    case 'j'://战斗按钮
                                                //开始战斗，
                                        if ((playX == bossX && playY == bossY - 1 ||
                                            playX == bossX && playY == bossY + 1 ||
                                            playY == bossY && playX == bossX + 2 ||
                                            playY == bossY && playX == bossX - 2) && bossHp > 0)
                                        {
                                            isFight = true;
                                            //可以开始战斗
                                            Console.SetCursorPosition(2, h - 5);//中间的围墙是h-6
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write("开始和boss战斗了按J键继续");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("玩家当前血量为{0}", playerHp);
                                            Console.SetCursorPosition(2, h - 3);
                                            Console.Write("怪物当前血量为{0}", bossHp);
                                        }
                                        //在公主旁边
                                        else if((playX == princessX && princessY == playY - 1 ||
                                            playX == princessX && princessY == playY + 1 ||
                                            playY == princessY && princessX == playX + 2 ||
                                            playY == princessY && princessX == playX - 2) && bossHp <= 0)
                                        {
                                            //修改场景，跳出循环，这里是switch，需要加标识符跳出外层
                                            nowSceneId = 3;
                                            isOver = true;
                                            break;
                                        }
                                            
                                           
                                        break;

                                }
                                #endregion
                            }
                            if (isOver)
                            {
                                break;
                            }
                            
                        }
                        #endregion
                        break;
                    case 3:
                        Console.Clear();
                        //显示标题
                        Console.SetCursorPosition(w/2-4,5);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("GameOver");
                        //根据胜利或者失败显示内容
                        exitScene = false;
                        Console.SetCursorPosition(w / 2 - 4, 7);
                        if (isOver)
                        {
                            Console.WriteLine("英雄救美");
                        }
                        else
                        {
                            Console.WriteLine("请再努力");
                        }
                        int curPosOver = 1;

                        while (true)
                        {
                            Console.SetCursorPosition(w / 2 - 6, 9);
                            Console.ForegroundColor = curPosOver == 1 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("回到开始界面");
                            Console.SetCursorPosition(w / 2 - 4, 11);
                            Console.ForegroundColor = curPosOver == 0 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("退出游戏");
                            char inputKey = Console.ReadKey(true).KeyChar;
                            switch (inputKey)
                            {
                                case 'w':
                                    curPosOver ^= 1;
                                    break;
                                case 's':
                                    curPosOver ^= 1;
                                    break;
                                case 'j':
                                    if (curPosOver == 1)
                                    {
                                        exitScene = true;
                                        nowSceneId = 1;
                                    }
                                    else //否则退出
                                    {
                                        Environment.Exit(0);
                                    }
                                    break;
                            }
                            if (exitScene)
                            {
                                break;
                            }
                        }
                        break;
                }
            }
            #endregion





            #endregion


        }
    }
}
