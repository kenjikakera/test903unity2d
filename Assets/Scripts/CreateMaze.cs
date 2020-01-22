using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EnumList;



public class CreateMaze : MonoBehaviour
{
    public const int MazeSizeX = 14;
    public const int MazeSizeY = 14;
    public const int MazeSizeZ = 3;
    public const float startX = -5.2f, startY = -4.5f, startZ = 140;
    public float enemyPosZ = 130;
    public const float chipXS = 0.63f, chipYS = 0.63f;
    public GameObject[] mazeItem;
    public GameObject goFloor;

    public GameObject[] goEnemy;
    public int[,,] maze = new int[MazeSizeX, MazeSizeY, MazeSizeZ];

    private GameObject[,,] mazeItemGO = new GameObject[MazeSizeX, MazeSizeY, MazeSizeZ];
    private int[,,] itemNum = new int[MazeSizeX, MazeSizeY, MazeSizeZ];

    private GameObject goGM;
    private GameManager scGM;

    /*
        private const int maxBox = 15;
        // 宝箱のデータ
        public struct ITEMBOX
        {
            public int x;
            public int y;
            public Vector2 pos2D;
            public int floor;
            public int item;
        }
        public ITEMBOX[] itemBox = new ITEMBOX[maxBox];
    */

    private int nWay = 1;
    private int msX = 1;
    private int msY = 1;
    private int msZ = 0;
    private int block = 0;

    void Awake()
    {
        goGM = GameObject.Find("GameManager");
        scGM = goGM.GetComponent<GameManager>();
        makeAllMaze();
    }

    void Start()
    {
    }
    public void makeAllMaze()
    {
        DestroyMaze();
        for (int z = 0; z < MazeSizeZ; z++)
        {
            for (int y = 0; y < MazeSizeY; y++)
            {
                for (int x = 0; x < MazeSizeX; x++)
                {
                    maze[x, y, z] = 1;
                }
            }
        }

        maze[1, 1, 0] = 0;
        block++;

        // 迷路自動生成
        for (msZ = 0; msZ < MazeSizeZ; msZ++)
        {
            block = 0;
            if (msZ == 0)
            {
                maze[1, 1, 0] = 0;
                block++;
            }
            do
            {
                bool flag;
                switch (nWay)
                {
                    case 0:
                        msY--;
                        flag = false;
                        if (msY > 0)
                        {
                            flag = mazeMakeSubDig();
                        }
                        if (!flag)
                        {
                            msY++;
                            mazeMakeSubNot();
                        }
                        break;
                    case 1:
                        msX++;
                        flag = false;
                        if (msX < MazeSizeX - 1)
                        {
                            flag = mazeMakeSubDig();
                        }
                        if (!flag)
                        {
                            msX--;
                            mazeMakeSubNot();
                        }
                        break;
                    case 2:
                        msY++;
                        flag = false;
                        if (msY < MazeSizeY - 1)
                        {
                            flag = mazeMakeSubDig();
                        }
                        if (!flag)
                        {
                            msY--;
                            mazeMakeSubNot();
                        }
                        break;
                    case 3:
                        msX--;
                        flag = false;
                        if (msX > 0)
                        {
                            flag = mazeMakeSubDig();
                        }
                        if (!flag)
                        {
                            msX++;
                            mazeMakeSubNot();
                        }
                        break;
                }

            }
            while (block < MazeSizeX * MazeSizeY / 2);
            // 休憩所は各階にある
            makeRest(msZ);
        }

        // 階段と宝箱の配置
        makeUpDown((int)Floor.floor0);
        makeBoxt((int)Floor.floor0, (int)Items.niku);
        makeBoxt((int)Floor.floor0, (int)Items.longsword);
        makeBoxt((int)Floor.floor0, (int)Items.niku);
        makeBoxt((int)Floor.floor0, (int)Items.niku);
        makeBoxt((int)Floor.floor0, (int)Items.silverarmer);

        makeUpDown((int)Floor.floor1);
        makeBoxt((int)Floor.floor1, (int)Items.niku);
        makeBoxt((int)Floor.floor1, (int)Items.firesowrd);
        makeBoxt((int)Floor.floor1, (int)Items.niku);
        makeBoxt((int)Floor.floor1, (int)Items.firearmer);
        makeBoxt((int)Floor.floor1, (int)Items.niku);

        makeBoxt((int)Floor.floor2, (int)Items.niku);
        makeBoxt((int)Floor.floor2, (int)Items.niku);
        makeBoxt((int)Floor.floor2, (int)Items.niku);
        makeBoxt((int)Floor.floor2, (int)Items.niku);
        makeBoxt((int)Floor.floor2, (int)Items.niku);

        // 敵の作成と配置 floor0
        makeEnemny((int)Floor.floor0, 0);
        makeEnemny((int)Floor.floor0, 0);
        makeEnemny((int)Floor.floor0, 0);
        makeEnemny((int)Floor.floor0, 0);
        makeEnemny((int)Floor.floor0, 0);
        makeEnemny((int)Floor.floor0, 0);
        makeEnemny((int)Floor.floor0, 1);
        makeEnemny((int)Floor.floor0, 1);
        makeEnemny((int)Floor.floor0, 1);
        makeEnemny((int)Floor.floor0, 1);

        // 敵の作成と配置 floor1
        makeEnemny((int)Floor.floor1, 0);
        makeEnemny((int)Floor.floor1, 0);
        makeEnemny((int)Floor.floor1, 0);
        makeEnemny((int)Floor.floor1, 1);
        makeEnemny((int)Floor.floor1, 1);
        makeEnemny((int)Floor.floor1, 1);
        makeEnemny((int)Floor.floor1, 2);
        makeEnemny((int)Floor.floor1, 2);
        makeEnemny((int)Floor.floor1, 2);
        makeEnemny((int)Floor.floor1, 2);

        // 敵の作成と配置 floor2
        makeEnemny((int)Floor.floor2, 0);
        makeEnemny((int)Floor.floor2, 0);
        makeEnemny((int)Floor.floor2, 1);
        makeEnemny((int)Floor.floor2, 1);
        makeEnemny((int)Floor.floor2, 2);
        makeEnemny((int)Floor.floor2, 2);
        makeEnemny((int)Floor.floor2, 2);
        makeEnemny((int)Floor.floor2, 2);
        makeEnemny((int)Floor.floor2, 2);
        makeEnemny((int)Floor.floor2, 3);
    }



    public void makeMaze(int floor)
    {

        Vector3 pos3D = new Vector3(startX, startY, startZ);

        for (int y = 0; y < MazeSizeY; y++)
        {
            pos3D.x = startX;
            for (int x = 0; x < MazeSizeY; x++)
            {
                mazeItemGO[x, y, floor] = Instantiate(mazeItem[maze[x, y, floor]], pos3D, Quaternion.identity, goFloor.transform);
                if (maze[x, y, floor] == 5) {
                    mazeItem scMI = mazeItemGO[x, y, floor].GetComponent<mazeItem>();
                    scMI.itemNum = itemNum[x, y, floor];
                    scMI.x = x;
                    scMI.y = y;
                    scMI.pos3D = pos3D;
                    scMI.go = mazeItemGO[x, y, floor];
                }
                pos3D.x += chipXS;
            }
            pos3D.y += chipYS;
        }


    }


    // 敵以外のすべての子オブジェクトを削除
    public void DestroyMaze()
    {
        foreach (Transform n in goFloor.transform)
        {
            if(n.gameObject.tag!="enemy") GameObject.Destroy(n.gameObject);
        }

    }




    // Start is called before the first frame update
    //    void Start()
    //    {

    //    }

    // Update is called once per frame
    void Update()
    {
    }




    void mazeMakeSubNot()
    {
        if ((Random.Range(0, 1.0f)) > 0.8f)
        {
            int x = Random.Range(1, MazeSizeX - 1);
            int y = Random.Range(1, MazeSizeY - 1);
            if (maze[x, y, msZ] == 0)
            {
                msX = x;
                msY = y;
                return;
            }
        }
        if ((Random.Range(0, 1.0f)) > 0.5f)
        {
            nWay--;
            if (nWay < 0) nWay = 3;
        }
        else
        {
            nWay++;
            if (nWay > 3) nWay = 0;
        }
    }

    /// <summary>
    // 上下階段を作る
    /// </summary>
    void makeUpDown(int floor)
    {
        do
        {
            int posX = Random.Range(1, MazeSizeX - 1);
            int posY = Random.Range(1, MazeSizeY - 1);
            if (maze[posX, posY, floor] == 0 && maze[posX, posY, floor + 1] == 0)
            {
                maze[posX, posY, floor] = (int)MazeParts.down;
                maze[posX, posY, floor + 1] = (int)MazeParts.up;
                break;
            }
        } while (true);
    }


    /// <summary>
    // 床を作る
    /// </summary>
    bool mazeMakeSubDig()
    {
        if (maze[msX, msY, msZ] == 0) return false;
        if ((Random.Range(0, 1.0f)) > 0.8f) return false;
        maze[msX, msY, msZ] = 0;
        block++;
        return true;
    }

    /// <summary>
    ///  休憩所を作る
    /// </summary>
    void makeRest(int floor)
    {
        // 休憩所は各階にある
        int posX;
        int posY;
        do
        {
            posX = Random.Range(1, MazeSizeX - 1);
            posY = Random.Range(1, MazeSizeY - 1);
            if (maze[posX, posY, floor] == (int)MazeParts.floor)
            {
                maze[posX, posY, floor] = (int)MazeParts.reast;
                break;
            }
        } while (true);
    }

    /// <summary>
    ///  宝箱をる
    /// </summary>
    void makeBoxt(int floor, int numItem)
    {
        int posX;
        int posY;
        do
        {
            posX = Random.Range(1, MazeSizeX - 1);
            posY = Random.Range(1, MazeSizeY - 1);
            if (maze[posX, posY, floor] == 0)
            {
                maze[posX, posY, floor] = (int)MazeParts.box;
                itemNum[posX, posY, floor] = numItem;
                break;
            }
        } while (true);
    }

    // 敵の作成
    void makeEnemySub(int numType, int posX, int posY, float life, float attack, float deffence, float speed, int lifeAdd, float attackExp, float deffenceExp, float searchArea, int score,int floor)
    {
                GameObject goEY = Instantiate(goEnemy[numType], scGM.pos3Dnot, Quaternion.identity, goFloor.transform);
                Enemy scEY = goEY.GetComponent<Enemy>();

                scEY.type = numType;
                scEY.life = life;
                scEY.attack = attack;
                scEY.deffence = deffence;
                scEY.speed = speed;
                scEY.lifeAdd = lifeAdd;
                scEY.attackExp = attackExp;
                scEY.deffenceExp = deffenceExp;
                scEY.searchArea = searchArea;
                scEY.score = score;
                scEY.floor = floor;
                scEY.pos3DReal= new Vector3(startX + posX * chipXS + 0.05f, startY + posY * chipYS + 0.05f, enemyPosZ);


    }
    void makeEnemny(int floor, int numType)
    {
        int posX;
        int posY;
        do
        {
            posX = Random.Range(1, MazeSizeX - 1);
            posY = Random.Range(1, MazeSizeY - 1);
            if (maze[posX, posY, floor] == 0 && maze[posX, posY, floor] != 3 && maze[posX, posY, floor] != 4 && (posX != 1 && posY != 1))
            {
                {
                    switch (numType)
                    {
                        case 0:
                            makeEnemySub(0, posX, posY, 100, 70, 50, 0.006f, 10, 1.025f, 1.025f, 0.2f, 50,floor);
                            break;
                        case 1:
                            makeEnemySub(1, posX, posY, 200, 100, 60, 0.003f, 15, 1.05f, 1.05f, 0.8f, 100,floor);
                            break;
                        case 2:
                            makeEnemySub(2, posX, posY, 300, 150, 100, 0.006f, 20, 1.1f, 1.1f, 0.8f, 200,floor);
                            break;
                        case 3:
                            makeEnemySub(3, posX, posY, 1000, 300, 300, 0.006f, 100, 1.1f, 1.1f, 0.8f, 1000,floor);
                            break;
                    }
                    break;
                }
            }
        } while (true);
    }
}

