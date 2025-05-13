using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CornerPopupUI : MonoBehaviour
{
    public RectTransform panelToMove;    // Panel góc ph?i d??i
    public Button toggleButton;          // Nút nh?n m?/?óng
    public float moveDistance = 200f;    // Kho?ng cách tr??t sang trái
    public float slideDuration = 0.3f;   // Th?i gian tr??t

    private Vector2 originalPos;
    private Vector2 movedPos;
    private bool isOpen = false;
    private Tween currentTween;

    void Start()
    {
        originalPos = panelToMove.anchoredPosition;
        movedPos = originalPos + Vector2.left * moveDistance;

        toggleButton.onClick.AddListener(TogglePanel);
    }

    void TogglePanel()
    {
        isOpen = !isOpen;

        // H?y tween c? n?u còn ?ang ch?y
        currentTween?.Kill();

        // Tween t?i v? trí m?i
        Vector2 targetPos = isOpen ? movedPos : originalPos;
        currentTween = panelToMove.DOAnchorPos(targetPos, slideDuration).SetEase(Ease.OutCubic);
    }
}