using UnityEngine;
using UnityEngine.EventSystems;

public class HammerHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();  // Pobierz Animator z obiektu młotka
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetBool("isHovering", true);  // Włącz animację uderzania młotka
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetBool("isHovering", false);  // Wyłącz animację uderzania młotka
        }
    }
}
