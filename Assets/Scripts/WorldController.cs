public static class WorldController {

    public enum World { Cave, Fire, Water, Ice, Dark };

    private static World currWorld = World.Cave;
    private static World prevWorld;
    private static int currArea = 0;
    private static int prevArea;
    private static int spawn = 0;

    public static void SetSpawn(int newSpawn)
    {
        spawn = newSpawn;
    }

    public static void SetPrevWorld(World world)
    {
        prevWorld = world;
    }

    public static void SetWorld(World world)
    {
        currWorld = world;
    }

    public static void SetPrevArea(int area)
    {
        prevArea = area;
    }

    public static void SetArea(int area)
    {
        currArea = area;
    }

    public static int GetSpawn()
    {
        return spawn;
    }

    public static World GetWorld()
    {
        return currWorld;
    }

    public static int GetArea()
    {
        return currArea;
    }

    public static World GetPrevWorld()
    {
        return prevWorld;
    }

    public static int GetPrevArea()
    {
        return prevArea;
    }

}
