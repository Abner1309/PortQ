using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class MenuMovement : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private TMP_Dropdown menuDropdown;

    [Serializable]
    public class MenuSectionLink
    {
        public string optionLabel; // deve corresponder ao texto da opção no Dropdown (ex: "Início", "Pilares")
        public RectTransform sectionTarget;
    }

    [SerializeField] private List<MenuSectionLink> sectionLinks;

    private void Start()
    {
        menuDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index)
    {
        string selectedLabel = menuDropdown.options[index].text;

        MenuSectionLink link = sectionLinks.Find(l => l.optionLabel == selectedLabel);

        if (link != null && link.sectionTarget != null)
        {
            ScrollToTarget(link.sectionTarget);
        }
        else
        {
            Debug.LogWarning($"Nenhuma seção encontrada para a opção '{selectedLabel}'.");
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
