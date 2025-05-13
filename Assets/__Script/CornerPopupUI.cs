using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CornerPopupUI : MonoBehaviour
{
    public RectTransform panelToMove;    // Panel g�c ph?i d??i
    public Button toggleButton;          // N�t nh?n m?/?�ng
    public float moveDistance = 200f;    // Kho?ng c�ch tr??t sang tr�i
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

        // H?y tween c? n?u c�n ?ang ch?y
        currentTween?.Kill();

        // Tween t?i v? tr� m?i
        Vector2 targetPos = isOpen ? movedPos : originalPos;
        currentTween = panelToMove.DOAnchorPos(targetPos, slideDuration).SetEase(Ease.OutCubic);
    }
}