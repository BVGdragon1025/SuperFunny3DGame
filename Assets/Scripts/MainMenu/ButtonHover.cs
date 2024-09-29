using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    private Vector3 originalScale;
    private float originalRotationZ;

    void Start()
    {
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
        originalRotationZ = transform.eulerAngles.z;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        float randomRotationZ = Random.Range(-7f, -2f);
        if (Random.value > 0.5f)
        {
            randomRotationZ = Random.Range(2f, 7f);
        }

        float randomZoomFactor = Random.Range(1.05f, 1.10f);
        Vector3 targetScale = new Vector3(originalScale.x * randomZoomFactor, originalScale.y * randomZoomFactor, originalScale.z);

        LeanTween.scale(gameObject, targetScale, 0.2f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.rotateZ(gameObject, originalRotationZ + randomRotationZ, 0.2f).setEase(LeanTweenType.easeInOutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, originalScale, 0.2f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.rotateZ(gameObject, originalRotationZ, 0.2f).setEase(LeanTweenType.easeInOutBack);
    }
}
