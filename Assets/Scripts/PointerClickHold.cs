using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event UnityAction OnPointerDown;
    public event UnityAction OnPointerUp;


    private bool m_Hold;
    public bool IsHold => m_Hold;
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnPointerDown?.Invoke();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        OnPointerUp?.Invoke();  
    }
}
