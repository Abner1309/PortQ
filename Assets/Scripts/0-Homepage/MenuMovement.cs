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

    private bool isScrollingProgrammatically; // evita que o listener de scroll rode durante um ScrollToTarget

    private void Start()
    {
        menuDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        scrollRect.onValueChanged.AddListener(OnScrollChanged);
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
        isScrollingProgrammatically = true;

        Canvas.ForceUpdateCanvases();

        Vector2 contentPos = (Vector2)scrollRect.transform.InverseTransformPoint(scrollRect.content.position);
        Vector2 targetPos = (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

        scrollRect.content.anchoredPosition = new Vector2(
            scrollRect.content.anchoredPosition.x,
            contentPos.y - targetPos.y
        );

        isScrollingProgrammatically = false;
    }

    private void OnScrollChanged(Vector2 normalizedPos)
    {
        if (isScrollingProgrammatically) return;

        UpdateDropdownToCurrentSection();
    }

    private void UpdateDropdownToCurrentSection()
    {
        RectTransform viewport = scrollRect.viewport != null ? scrollRect.viewport : (RectTransform)scrollRect.transform;

        MenuSectionLink closestLink = null;
        float closestDistance = float.MaxValue;

        foreach (var link in sectionLinks)
        {
            if (link.sectionTarget == null) continue;

            // Posição da seção relativa ao topo da viewport
            Vector3 viewportLocalPos = viewport.InverseTransformPoint(link.sectionTarget.position);
            float distance = Mathf.Abs(viewportLocalPos.y);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestLink = link;
            }
        }

        if (closestLink == null) return;

        int optionIndex = menuDropdown.options.FindIndex(o => o.text == closestLink.optionLabel);

        if (optionIndex >= 0 && menuDropdown.value != optionIndex)
        {
            menuDropdown.SetValueWithoutNotify(optionIndex);
            menuDropdown.RefreshShownValue(); // atualiza o texto exibido no Dropdown fechado
        }
    }
}
