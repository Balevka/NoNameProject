
public class Node
{

    private int g;
    private int h;

    public int GridIndexX
    {
        get; set;
    }

    public int GridIndexY
    {
        get; set;
    }
    public int G
    {
        get { return g; }
        set { g = value; }
    }

    public int H
    {
        get { return h; }
        set { h = value; }
    }

    public int F
    {
        get { return g + h; }
    }

    public Node ParentNode { get; set; }


    public bool IsWalkable
    {
        get;
        set;
    }

    public Node(int gridIndexX, int gridIndexY)
    {
        GridIndexX = gridIndexX;
        GridIndexY = gridIndexY;
        IsWalkable = true;
    }


}
