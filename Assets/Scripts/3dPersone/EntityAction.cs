using UnityEngine;
using UnityEngine.Events;
public abstract class EntityActionProperties { }

public abstract class EntityAction : MonoBehaviour
{
    [SerializeField] private UnityEvent eventOnStart;
    [SerializeField] private UnityEvent eventOnEnd;

    public UnityEvent EventOnStart => eventOnStart;
    public UnityEvent EventOnEnd => eventOnEnd;


    private EntityActionProperties properties;
    public EntityActionProperties Properties => properties;

    private bool isStart;

    public virtual void StartAction()
    {
        if (isStart == true) return;

        isStart = true;
        eventOnStart?.Invoke();
    }

    public virtual void EndAction()
    {
        isStart = false;
        eventOnEnd?.Invoke();
    }

    protected virtual void EndActionByAnimation()
    {
        isStart = false;
        eventOnEnd?.Invoke();
    }

    public virtual void SetProperties(EntityActionProperties prop)
    {
        properties = prop;
    }
}