using UnityEngine;
using UnityEngine.Events;

public class CheckPointTrack : MonoBehaviour
{
    public event UnityAction<CheckPoint> TrackPointTriggered;
    public event UnityAction OnFinishTrack;

    private CheckPoint[] _points;

    private int _lapsComplited = -1;

    private void Awake()
    {
        BuildCircuit();
    }

    private void Start()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            _points[i].triggered += OnTrackPointTriggered;
        }

        _points[0].AssignAsTarget();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            _points[i].triggered -= OnTrackPointTriggered;
        }
    }

    private void OnTrackPointTriggered(CheckPoint checkPoint)
    {
        if (checkPoint.isTarget == false) return;

        checkPoint.Passed();
        checkPoint.next?.AssignAsTarget();

        TrackPointTriggered?.Invoke(checkPoint);

        if (checkPoint.isLast == true)
        {
            OnFinishTrack?.Invoke();
        }

/*        if (checkPoint.isLast == true)
        {
            _lapsComplited++;

            *//*            if (checkPoint == TrackType.Sprint)
                            LapCompleted?.Invoke(_lapsComplited);

                        if (_trackType == TrackType.Circular)
                            if (_lapsComplited > 0)
                                LapCompleted?.Invoke(_lapsComplited);*//*
            LapCompleted?.Invoke(_lapsComplited);
        }*/
    }

    [ContextMenu(nameof(BuildCircuit))]
    private void BuildCircuit()
    {
        _points = TrackBuilder.Build(transform);
    }
}
