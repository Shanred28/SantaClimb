using UnityEngine;

public class TrackBuilder : MonoBehaviour
{
    public static CheckPoint[] Build(Transform trackTransform)
    {
        CheckPoint[] _points = new CheckPoint[trackTransform.childCount];

        ResetPoints(trackTransform, _points);
        MakeLinks(_points);
        MarkPoint(_points);

        return _points;
    }

    private static void ResetPoints(Transform trackTransform, CheckPoint[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = trackTransform.GetChild(i).GetComponent<CheckPoint>();

            if (points[i] == null)
            {
                Debug.LogError("There is no TrackPoint script on one of the child objects");
                return;
            }
            points[i].Reset();
        }
    }

    private static void MakeLinks(CheckPoint[] points)
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            points[i].next = points[i + 1];
        }

/*        if (type == TrackType.Circular)
        {
            points[points.Length - 1].next = points[0];
        }*/
    }

    private static void MarkPoint(CheckPoint[] points)
    {
        points[0].isFerst = true;

            points[points.Length - 1].isLast = true;


    }
}
