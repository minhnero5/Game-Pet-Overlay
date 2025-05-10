using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class TransparentWindow : MonoBehaviour
{
    const int GWL_EXSTYLE = -20;
    const int WS_EX_LAYERED = 0x80000;
    const int WS_EX_TRANSPARENT = 0x20;
    const int LWA_COLORKEY = 0x1;

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern bool SetWindowLong(IntPtr hWnd, int nindex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern int SetLayerWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint uFlags);

    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyToptHeight;
        public int cyBotHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

    private IntPtr hWnd;
    void Start()
    {
#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
        //SetLayerWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);

#endif
        Application.runInBackground = true;
    }

    void Update()
    {
        if (IsPointerOverUIElement() || IsPointerOverGameObject())
        {
            DisableClickThrough(); // không xuyên
        }
        else
        {
            EnableClickThrough(); // cho xuyên
        }
    }

    void EnableClickThrough()
    {
        int style = GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, style | WS_EX_TRANSPARENT);
    }

    void DisableClickThrough()
    {
        int style = GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, style & ~WS_EX_TRANSPARENT);
    }

    // Kiểm tra chuột có hover UI
    private bool IsPointerOverUIElement()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    // Kiểm tra chuột có đang hover vào bất kỳ collider 2D nào không
    private bool IsPointerOverGameObject()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        return hit.collider != null;
    }
}
