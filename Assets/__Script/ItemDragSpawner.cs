using UnityEngine;
using UnityEngine.UI;

public class ItemDragSpawner : MonoBehaviour
{
    public Button toggleButton;         // Nút bật/tắt chế độ
    public GameObject itemPrefab;       // Prefab vật phẩm

    private bool isDraggingMode = false;
    private bool canToggle = true;      // Để ngăn nhấn nút liên tục
    private GameObject currentItem;

    void Start()
    {
        // Gán sự kiện nút chỉ 1 lần
        //toggleButton.onClick.RemoveAllListeners();
        //toggleButton.onClick.AddListener(ToggleDragMode);
    }

    void Update()
    {
        if (!isDraggingMode) return;

        if (currentItem != null)
        {
            FollowMouse();

            // Nhấn chuột trái để "thả" vật và chuẩn bị vật mới
            if (Input.GetMouseButtonDown(0))
            {
                currentItem = null; // Vật cũ không theo chuột nữa
            }
        }
        else
        {
            // Nếu chưa có vật nào thì tạo mới cái đang kéo theo chuột
            CreateNewItem();
        }
    }

    public void ToggleDragMode()
    {
        if (!canToggle) return;
        canToggle = false;
        Invoke(nameof(EnableToggle), 0.2f); // Chặn spam nút trong 0.2s

        isDraggingMode = !isDraggingMode;
        Debug.Log("Dragging mode: " + isDraggingMode);

        if (!isDraggingMode && currentItem != null)
        {
            Destroy(currentItem); // Xoá vật đang theo chuột khi tắt chế độ
            currentItem = null;
        }
    }

    void EnableToggle()
    {
        canToggle = true;
    }

    void CreateNewItem()
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning("itemPrefab chưa được gán!");
            return;
        }

        currentItem = Instantiate(itemPrefab, GetMouseWorldPosition(), Quaternion.identity);
    }

    void FollowMouse()
    {
        if (currentItem != null)
        {
            currentItem.transform.position = GetMouseWorldPosition();
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Camera 2D cần giá trị z để chuyển đúng tọa độ
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
