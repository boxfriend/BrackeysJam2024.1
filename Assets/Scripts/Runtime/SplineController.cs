using Boxfriend.Extensions;
using UnityEngine;
using UnityEngine.Splines;

public class SplineController : MonoBehaviour
{
    [field:SerializeField] public SplineContainer Container { get; private set; }

    [SerializeField] private Transform[] _splinePoints;

    [SerializeField] private Transform _pathPrefab;
    [SerializeField] private float _pathObjectDistance;

    [ContextMenu("ResetSpline")]
    private void ResetSplines()
    {
        while (Container.Splines.Count > 0)
            Container.RemoveSplineAt(0);

        BezierKnot startKnot = default;
        for(var i = 0; i < _splinePoints.Length; i++)
        {
            var point = _splinePoints[i].position;

            var inTangent = i > 0 ? _splinePoints[i - 1].position - point : Vector3.zero;
            var outTangent = i < _splinePoints.Length - 1 ? _splinePoints[i + 1].position -  point 
                : Vector3.zero;

            var knot = new BezierKnot(point, inTangent.normalized, outTangent.normalized);
            //Container.Spline.Add(knot);
            if(i != 0)
            {
                var spline = new Spline
                {
                    startKnot,
                    knot
                };
                Container.AddSpline(spline);
            }

            startKnot = knot;
        }
    }

    [ContextMenu("Spawn Path")]
    private void SpawnPath ()
    {
#if UNITY_EDITOR
        if(!UnityEditor.EditorApplication.isPlaying)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(transform.GetChild(i).gameObject);
        }
        else
#endif
            gameObject.DestroyChildren();


        foreach(Spline spline in Container.Splines)
        {
            var pathLength = spline.GetLength();
            var pathCount = Mathf.CeilToInt(pathLength / _pathObjectDistance);
            for (var i = 0; i < pathCount; i++)
            {
                var percent = (i * _pathObjectDistance) / pathLength;
                var t = Mathf.Lerp(0, pathLength, percent);
                var position = spline.EvaluatePosition(percent);
                var rotation = Quaternion.LookRotation(spline.EvaluateTangent(percent));
                var obj = Instantiate(_pathPrefab, position, rotation, transform);
            }
        }
        
    }
}