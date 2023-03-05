using System;

namespace 飞行棋
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 1、控制台初始化
            int w = 50;
            int h = 30;
            ConsoleInit(w, h);
            #endregion

            #region 2、场景选择相关
            E_SceneType nowSceneType = E_SceneType.Begin;//初始默认是开始场景
            while (true)
            {
                switch (nowSceneType)
                {
                    case E_SceneType.Begin:
                        //开始场景
                        Console.Clear();
                        //进入开始场景
                        BeginScene(w,h,ref nowSceneType);
                        break;
                    case E_SceneType.Game:
                        //游戏场景
                        Console.Clear();
                        GameScene(w, h,ref nowSceneType);
                        break;
                    case E_SceneType.End:
                        //结束场景
                        Console.Clear();
                        GameEnd(w, h, ref nowSceneType);
                        break;
                }
            }
            #endregion
            #region my
            //ConsoleColor consoleColor;
            //int nowSceneId = 1;
            //int CurPos = 1;
            //while (true)
            //{

            //    switch (nowSceneId)
            //    {
            //        case 1:
            //            Console.Clear();
            //            Console.SetCursorPosition(w / 2 - 3, 7);
            //            Console.ForegroundColor = ConsoleColor.White;
            //            Console.WriteLine("飞行棋");
            //            Console.SetCursorPosition(w / 2 - 4, 9);
            //            consoleColor = CurPos == 1 ? ConsoleColor.Red : ConsoleColor.White;
            //            Console.ForegroundColor = consoleColor;
            //            Console.WriteLine("开始游戏");
            //            consoleColor = CurPos == 0 ? ConsoleColor.Red : ConsoleColor.White;
            //            Console.SetCursorPosition(w / 2 - 4, 11);
            //            Console.ForegroundColor = consoleColor;
            //            Console.WriteLine("退出游戏");
            //            char inputKey = Console.ReadKey(true).KeyChar;
            //            switch (inputKey)
            //            {
            //                case 'w':
            //                case 's':
            //                    CurPos ^= 1;
            //                    break;
            //                case 'j':
            //                    if (CurPos == 1)
            //                    {
            //                        //开始游戏
            //                        nowSceneId = 3;
            //                        break;
            //                    }
            //                    else
            //                    {
            //                        Environment.Exit(0);
            //                    }
            //                    break;


            //            }

            //            break;
            //        case 2:
            //            Console.Clear();
            //            Console.WriteLine("游戏场景");
            //            break;
            //        case 3:
            //            Console.Clear();
            //            Console.SetCursorPosition(w / 2 - 4, 7);
            //            Console.ForegroundColor = ConsoleColor.White;
            //            Console.WriteLine("游戏结束");
            //            consoleColor = CurPos == 1 ? ConsoleColor.Red : ConsoleColor.White;
            //            Console.SetCursorPosition(w / 2 - 5, 9);
            //            Console.ForegroundColor = consoleColor;
            //            Console.WriteLine("回到主菜单");
            //            consoleColor = CurPos == 0 ? ConsoleColor.Red : ConsoleColor.White;
            //            Console.SetCursorPosition(w / 2 - 4, 11);
            //            Console.ForegroundColor = consoleColor;
            //            Console.WriteLine("退出游戏");
            //            inputKey = Console.ReadKey(true).KeyChar;
            //            switch (inputKey)
            //            {
            //                case 'w':
            //                case 's':
            //                    CurPos ^= 1;
            //                    break;
            //                case 'j':
            //                    if (CurPos == 1)
            //                    {
            //                        nowSceneId = 1;
            //                        //回到主菜单
            //                    }
            //                    else
            //                    {
            //                        Environment.Exit(0);
            //                    }
            //                    break;
            //            }

            //            break;
            //    }
            //}
            #endregion
        }

      

        #region 1、控制台初始化
        static void ConsoleInit(int w,int h)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
        }
        #endregion

        #region 3、开始场景逻辑
        static void BeginScene(int w,int h,ref E_SceneType nowSceneType)
        {
            Console.SetCursorPosition(w / 2 - 3, 8);
            Console.Write("飞行棋");
            int curPos = 1;//用于切换选项
            bool isExitScene = false;
            ConsoleColor consoleColor;
            //开始场景逻辑处理循环
            while (true)
            {
                Console.SetCursorPosition(w / 2 - 4, 13);
                consoleColor = curPos == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.ForegroundColor = consoleColor;
                Console.WriteLine("开始游戏");
                Console.SetCursorPosition(w / 2 - 4, 15);
                consoleColor = curPos == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.ForegroundColor = consoleColor;
                Console.WriteLine("退出游戏");

                switch (Console.ReadKey(true).Key)//Key枚举
                {
                    case ConsoleKey.W:
                    case ConsoleKey.S:
                        curPos ^= 1;
                        break;
                    case ConsoleKey.J:
                        if (curPos == 1)
                        {
                            nowSceneType = E_SceneType.Game;
                            isExitScene = true;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        break;
                }
                if (isExitScene)
                {
                    break;
                }
            }
        }
        #endregion

        #region 4、游戏场景逻辑
        static void GameScene(int w, int h,ref E_SceneType nowSceneType)
        {
            //绘制墙体
            DrawWall(w,h);
            //绘制地图,初始化一张地图
            Map map = new Map(14, 3, 90);
            map.Draw();

            //绘制玩家
            Player player = new Player(0, E_PlayType.Player);
            Player computer = new Player(0, E_PlayType.Computer);
            DrawPlayer(player,computer,map);
           
            bool isOver = false;
            //这里的循环就是扔色子的逻辑了
            while (true)
            {
                //1、检测输入 (玩家）
                Console.ReadKey(true);
                isOver = RandomMove(w,h,ref player, ref computer, map);
                //2、绘制地图
                map.Draw();
                //3、绘制玩家
                DrawPlayer(player, computer, map);
                //4、判断是否结束游戏
                if (isOver)
                {
                    Console.SetCursorPosition(2, h - 3);
                    Console.Write("玩家胜利!,按任意键退出游戏");
                    Console.ReadKey(true);
                    nowSceneType = E_SceneType.End;
                    break;
                }

                //1、检测输入 (电脑
                Console.ReadKey(true);
                isOver = RandomMove(w,h,ref computer, ref player, map);
                //2、绘制地图
                map.Draw();
                //3、绘制玩家
                DrawPlayer(player, computer, map);
                //4、判断是否结束游戏
                if (isOver)
                {
                    Console.SetCursorPosition(2, h - 3);
                    Console.Write("电脑胜利!,按任意键退出游戏");
                    Console.ReadKey(true);
                    nowSceneType = E_SceneType.End;
                    break;
                }

            }

        }
        #endregion

        #region 11、游戏结束场景逻辑
        static void GameEnd(int w,int h,ref E_SceneType nowSceneType)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(w / 2 - 4, 8);
            Console.Write("游戏结束");
            ConsoleColor consoleColor;
            int curPos = 1;
            while (true)
            {
                Console.SetCursorPosition(w / 2 - 5, 11);
                consoleColor = curPos == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.ForegroundColor = consoleColor;
                Console.Write("回到主菜单");
                Console.SetCursorPosition(w / 2 - 4, 13);
                consoleColor = curPos == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.ForegroundColor = consoleColor;
                Console.Write("退出游戏");
                bool isQuit = false;
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.S:
                        curPos ^= 1;
                        break;
                    case ConsoleKey.J:
                        if (curPos == 1)
                        {
                            nowSceneType = E_SceneType.Begin;
                            isQuit = true;
                            break;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        break;
                }
                if (isQuit)
                {
                    break;
                }
            }
        }
        #endregion

        #region 5、设置墙体
        static void DrawWall(int w, int h)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < w; i+=2)
            {
                //上
                Console.SetCursorPosition(i, 0);
                Console.Write("■");

                //中
                Console.SetCursorPosition(i, h-6);
                Console.Write("■");
                //中下
                Console.SetCursorPosition(i, h - 11);
                Console.Write("■");
                //下
                Console.SetCursorPosition(i, h - 1);
                Console.Write("■");

            }
            for (int i = 0; i < h; i ++)//左右
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(w - 2, i);
                Console.Write("■");
            }
            //显示文字信息
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, h - 10);
            Console.WriteLine("□:普通格子");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(2, h - 9);
            Console.Write("‖:暂停,一回合不动");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(25, h - 9);
            Console.Write("●:炸弹,倒退5格");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(2, h - 8);
            Console.WriteLine("∞:时空隧道，随机倒退，暂停，换位置");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(2, h - 7);
            Console.Write("☆:玩家");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(12, h - 7);
            Console.Write("▲:电脑");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(22, h - 7);
            Console.Write("◎:玩家与电脑重合");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, h - 5);
            Console.WriteLine("按任意键开始扔色子");
        }
        #endregion


        #region 9、绘制玩家(存在重合)
        static void DrawPlayer(Player player,Player computer,Map map)
        {
            //重合时:
            if (player.index == computer.index)
            {
                Console.SetCursorPosition(map.grids[player.index].pos.x, map.grids[player.index].pos.y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("◎");
            }
            else //不重合，就画自己的
            {
                player.Draw(map);
                computer.Draw(map);
            }
        }
        #endregion

        #region 10、扔色子
        static bool RandomMove(int w,int h,ref Player player,ref Player otherplayer,Map map)
        {

            //首先判断玩家是否这一回合暂停，是的话直接返回
            if (player.isPause)
            {
                player.isPause = false;//不修改就一直暂停了
                return false;//false表示还没有到达终点
            }

            //随机一个值1~6
            Random r = new Random();
            int randNum = r.Next(1, 7);
            player.index += randNum;
            
            //判断是否到达终点
            if (player.index >= map.grids.Length - 1)
            {
                //已经到达终点
                player.index = map.grids.Length - 1;
                player.Draw(map);
                return true;
            }
            else//还没到达终点
            {
                //那就先找到这个位置的grid，然后判断这个grid本身是个什么类型的格子
                //在根据格子类型做对应的操作(炸弹、时空隧道、暂停等)
                E_PlayType type = player.type;//记录当前player的类型
                string playName = type == E_PlayType.Computer ? "电脑" : "玩家";
                //普通前进在第一行
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, h - 5);
                Console.Write("                            ");
                Console.SetCursorPosition(2, h - 4);
                Console.Write("                                 ");
                Console.SetCursorPosition(2, h - 5);
                Console.Write("{0}正常前进{1}格", playName, randNum);
                switch (map.grids[player.index].type)
                {
                    case E_Grid_Type.Normal:
                        break;
                    case E_Grid_Type.Boom:
                        player.index -= 5;
                        if (player.index <= 0)//不能变负的(比起点还小)
                        {
                            player.index = 0;
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("                                 ");
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}遇到炸弹，后退5格", playName);
                        break;
                    case E_Grid_Type.Pause:
                        //表示暂停的标志
                        player.isPause = true;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("                                 ");
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}遇到暂停，暂停一回合", playName);
                        break;
                    case E_Grid_Type.Tunnel:
                        randNum = r.Next(0, 3);
                        if (randNum == 0)//0表示炸弹
                        {
                            player.index -= 5;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(2, h - 4);
                            Console.Write("                                 ");
                            Console.SetCursorPosition(2, h - 4);
                            Console.Write("{0}随机到炸弹，后退5格", playName);
                        }
                        else if (randNum == 1)//1表示暂停
                        {
                            player.isPause = true;
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.SetCursorPosition(2, h - 4);
                            Console.Write("                                 ");
                            Console.SetCursorPosition(2, h - 4);
                            Console.Write("{0}随机到暂停，暂停一回合", playName);
                        }
                        else//换位置，跟另一个玩家换位置
                        {
                            player.index = player.index ^ otherplayer.index;
                            otherplayer.index = player.index ^ otherplayer.index;
                            player.index = player.index ^ otherplayer.index;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition(2, h - 4);
                            Console.Write("                                 ");
                            Console.SetCursorPosition(2, h - 4);
                            Console.Write("{0}随机到交换位置与{1}交换位置", playName,playName=="电脑"?"玩家":"电脑");
                        }
                        break;
                }

            }

            return false;//false表示没有结束
        }
        #endregion

    }
    #region 2、场景选择相关
    /// <summary>
    /// 游戏场景枚举类型
    /// </summary>
    enum E_SceneType
    {
        /// <summary>
        ///    开始场景
        /// </summary>
        Begin,
        /// <summary>
        /// 游戏场景
        /// </summary>
        Game,
        /// <summary>
        /// 结束场景
        /// </summary>
        End
    }
    #endregion

    #region 6、格子结构体和格子枚举
    /// <summary>
    /// 格子类型 枚举
    /// </summary>
    enum E_Grid_Type
    {
        /// <summary>
        /// 普通格子
        /// </summary>
        Normal,
        /// <summary>
        ///     炸弹
        /// </summary>
        Boom,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        ///     时空隧道：随机 倒退、暂停、换位置
        /// </summary>
        Tunnel,
    }

    //位置结构体
    struct Vector2
    {
        public int x;//x轴
        public int y;//y轴
        //构造函数
        public Vector2(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    struct Grid
    {
        //格子的类型
        public E_Grid_Type type;
        //格子的位置，用个类型代替，
        public Vector2 pos;

        //构造函数
        public Grid(int x,int y,E_Grid_Type type)
        {
            pos.x = x;
            pos.y = y;
            this.type = type;
        }

        //既然是格子类，且后面还要画到游戏界面，在这里定义画的方法就很方便了，
        //因为位置已经有了
        public void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);//这里就很方便直接拿到位置就画
            switch (type)
            {
                case E_Grid_Type.Normal:
                    //画普通格子
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("□");
                    break;
                case E_Grid_Type.Boom:
                    //画炸弹
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("●");
                    break;
                case E_Grid_Type.Pause:
                    //画暂停
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("‖");
                    break;
                case E_Grid_Type.Tunnel:
                    //画时空隧道
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("∞");
                    break;
            }
        }
    }
    #endregion

    #region 7、地图结构体
    struct Map
    {
        public Grid[] grids;//记录所有的格子类
        public Map(int x,int y,int num)//x,y,初始坐标，num 格子数量
        {
            grids = new Grid[num];//格子数组
            Random random = new Random();
            int randValue;
            //记录X增长的次数
            int indexX = 0;
            //记录Y增长的次数
            int indexY = 0;
            //X的步长
            int stepNum = 2;//中文字符占2个字节
            for (int i = 0; i < grids.Length; i++)//遍历格子数组并初始化
            {
                randValue = random.Next(0, 101);
                //随机0~100，其他格子5%的概率，剩下的就是普通格子概率
                if (randValue <= 85 || i == 0 || i == grids.Length - 1)
                {
                    grids[i].type = E_Grid_Type.Normal;
                }else if (randValue > 85 && randValue <= 90)
                {
                    grids[i].type = E_Grid_Type.Boom;
                }else if (randValue > 90 && randValue <= 95)
                {
                    grids[i].type = E_Grid_Type.Pause;
                }else if (randValue > 95 && randValue <= 100)
                {
                    grids[i].type = E_Grid_Type.Tunnel;
                }
                //设置格子位置
                grids[i].pos = new Vector2(x, y);

                if (indexX == 10)
                {
                    y += 1;
                    indexY++;
                    if (indexY == 2)//说明又要开始移动x轴了
                    {
                        stepNum = -stepNum;//反向绘制X轴
                        indexY = 0;
                        indexX = 0;
                    }
                }
                else
                {
                    x += stepNum;
                    indexX++;
                }

            }
        }

        //画地图
        public void Draw()
        {
            for (int i = 0; i < grids.Length; i++)
            {
                grids[i].Draw();
            }
        }
    }
    #endregion

    #region 8、玩家枚举和结构体

    enum E_PlayType
    {
        /// <summary>
        /// 玩家
        /// </summary>
        Player,
        /// <summary>
        /// 电脑
        /// </summary>
        Computer,
    }

    struct Player
    {
        public E_PlayType type;
        // 这里我们不知道xy的位置，但是我们知道格子对象是带有xy的
        //所以我们只需要记录格子的下标索引 作为玩家的位置就很好
        public int index;

        public bool isPause;

        public Player(int index, E_PlayType type, bool isPause = false)
        {
            this.index = index;
            this.type = type;
            this.isPause = isPause;
        }

        //绘制方法
        public void Draw(Map map)
        {
            //从传入的map中找到index下标的格子，就是我们玩家当前的位置
            Grid grid = map.grids[index];
            //找到位置，并修改为玩家的颜色，画出玩家即可，
            Console.SetCursorPosition(grid.pos.x, grid.pos.y);
            switch (this.type)//判断是玩家还是电脑，他们都是用的这个结构体
            {
                case E_PlayType.Player:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("☆");
                    break;
                case E_PlayType.Computer:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("▲");
                    break;
                default:
                    break;
            }
            
        }

    }

    #endregion

}
