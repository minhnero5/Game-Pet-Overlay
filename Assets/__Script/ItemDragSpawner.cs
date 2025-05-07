using UnityEngine;
using UnityEngine.UI;

public class ItemDragSpawner : MonoBehaviour
{
    public Button toggleButton;         // Nút b?t/t?t ch? ??
    public GameObject itemPrefab;       // Prefab v?t ph?m

    private bool isDraggingMode = false;
    private GameObject currentItem;

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleDragMode);
    }

    void Update()
    {
        if (!isDraggingMode) return;

        if (currentItem != null)
        {
            FollowMouse();

            if (Input.GetMouseButtonDown(0))
            {
                // Nh?n chu?t ? gi? l?i v?t ph?m c?, t?o cái m?i
                currentItem = Instantiate(itemPrefab, GetMouseWorldPosition(), Quaternion.identity);
            }
        }
        else
        {
            // N?u ch?a có item nào, t?o m?t cái ??u tiên
            CreateNewItem();
        }
    }

    void ToggleDragMode()
    {
        isDraggingMode = !isDraggingMode;

        if (!isDraggingMode)
        {
            // N?u t?t ch? ??, xóa v?t ph?m ?ang kéo
            if (currentItem != null)
            {
                Destroy(currentItem);
                currentItem = null;
            }
        }
        else
        {
            CreateNewItem();
        }
    }

    void CreateNewItem()
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning("itemPrefab ch?a ???c gán!");
            return;
        }

        currentItem = Instantiate(itemPrefab, GetMouseWorldPosition(), Quaternion.identity);
    }

    void FollowMouse()
    {
        currentItem.transform.position = GetMouseWorldPosition();
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // kho?ng cách z phù h?p ?? th?y v?t ph?m trong camera 2D
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
