using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MenuMovement : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;

    [Serializable]
    public class MenuSectionLink
    {
        public Button menuButton;
        public RectTransform sectionTarget;
    }

    [SerializeField] private List<MenuSectionLink> sectionLinks;

    private void Start()
    {
        foreach (var link in sectionLinks)
        {
            RectTransform target = link.sectionTarget; // evita closure capturando a variável errada
            link.menuButton.onClick.AddListener(() => ScrollToTarget(target));
        }
    }

    public void ScrollToTarget(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        Vector2 contentPos = (Vector2)scrollRect.transform.InverseTransformPoint(scrollRect.content.position);
        Vector2 targetPos = (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

        scrollRect.content.anchoredPosition = new Vector2(
            scrollRect.content.anchoredPosition.x,
            contentPos.y - targetPos.y
        );
    }
}