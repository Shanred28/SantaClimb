using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MainMenu
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
    {
        public UnityEvent OnClick;

        public event UnityAction<UIButton> eventPointerEnter;
        public event UnityAction<UIButton> eventPointerExit;
        public event UnityAction<UIButton> eventPointerClick;

        [SerializeField] protected bool interactable;

        private bool _focuse = false;
        public bool focuse => _focuse;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (interactable == false) return;

            eventPointerClick?.Invoke(this);
            OnClick.Invoke();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (interactable == false) return;

            eventPointerEnter?.Invoke(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (interactable == false) return;

            eventPointerExit?.Invoke(this);
        }

        public virtual void SetFocuse()
        {
            if (interactable == false) return;

            _focuse = true;
        }

        public virtual void SetUnFocuse()
        {
            if (interactable == false) return;

            _focuse = false;
        }
    }
}

