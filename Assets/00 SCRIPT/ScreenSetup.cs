using UnityEngine;

public class ScreenSetup : Singleton<ScreenSetup>
{
    void Awake()
    {
        // Lấy chiều cao màn hình hiện tại
        int screenHeight = Screen.currentResolution.height;
        // Tính toán chiều rộng dựa trên tỷ lệ 9:16
        int screenWidth = screenHeight * 9 / 16;

        // Thiết lập độ phân giải màn hình
        Screen.SetResolution(screenWidth, screenHeight, false);
    }
}
