using UnityEngine;

public static class CONSTANT
{
    public static float SCREEN_WIDTH;
    public static float SCREEN_HEIGHT;
    public static int ENEMIES_QUANTITY = 16;
    public static int ENEMY_MAX_HEALTH = 5;
    static CONSTANT()
    {
        SCREEN_WIDTH = Camera.main.orthographicSize * 2 * 9 / 16;
        SCREEN_HEIGHT = Camera.main.orthographicSize * 2;
    }
}
