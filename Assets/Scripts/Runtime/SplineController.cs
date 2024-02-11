using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class SplineController : MonoBehaviour
{
    [field:SerializeField] public SplineContainer Container { get; private set; }

    [SerializeField] private Transform[] _splinePoints;

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
}