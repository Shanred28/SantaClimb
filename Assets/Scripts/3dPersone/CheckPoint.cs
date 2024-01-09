using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
    public event UnityAction<CheckPoint> triggered;

    protected virtual void OnPassed() { }
    protected virtual void OnAssignAsTarget() { }


    public CheckPoint next;
    public bool isFerst;
    public bool isLast;

    protected bool _isTarget;
    public bool isTarget => _isTarget;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<Player>() == null) return;

        triggered?.Invoke(this);
    }

    public void Passed()
    {
        _isTarget = false;
        OnPassed();
    }

    public void AssignAsTarget()
    {
        _isTarget = true;
        OnAssignAsTarget();
    }

    public void Reset()
    {
        next = null;
        isFerst = false;
        isLast = false;
    }
}
