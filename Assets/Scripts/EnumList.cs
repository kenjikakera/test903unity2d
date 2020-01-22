public class EnumList
{
    // 迷路の構成物
    public enum MazeParts : int 
    {
        floor,
        block,
        reast,
        down,
        up,
        box
    }

    // アイテム
    public enum Items : int
    {
        niku,
        longsword,
        silverarmer,
        firesowrd,
        firearmer,
        nikuImage,
        openBox
    }

    // アイテム
    public enum ButtonItems : int
    {
        nuku = 1,
        down,
        up,
        box
    }


    // フロアー(階)
    public enum Floor : int
    {
        floor0,
        floor1,
        floor2
    }
}

