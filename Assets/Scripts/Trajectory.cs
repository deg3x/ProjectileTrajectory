using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Trajectory : MonoBehaviour
{
    public int resolution;
    public Transform gunpoint;

    private float velocity;
    private LineRenderer lr;
    private float angle;
    private float g;
    private float rads;

    void Start()
    {
        lr = this.GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics.gravity.y);
        velocity = gunpoint.GetComponentInParent<Gun>().bulletVelocity;
    }

    void RenderArc()
    {
        angle = -gunpoint.parent.rotation.eulerAngles.x;
        lr.positionCount = resolution + 1;
        lr.SetPositions(CalculateArcPoints());
    }

    void Update()
    {
        this.transform.position = gunpoint.position;
        this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, gunpoint.parent.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);

        RenderArc();
    }

    Vector3[] CalculateArcPoints()
    {
        Vector3[] arcPoints = new Vector3[resolution + 1];

        rads = Mathf.Deg2Rad * angle;

        float maxDistance = ((velocity * Mathf.Cos(rads)) / g) * (velocity * Mathf.Sin(rads) + Mathf.Sqrt(Mathf.Pow(velocity * Mathf.Sin(rads), 2) + 2 * g * gunpoint.transform.parent.position.y));

        for(int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcPoints[i] = CalculatePoint(t, maxDistance);
        }

        return arcPoints;
    }

    Vector3 CalculatePoint(float t, float maxDist)
    {
        float x = t * maxDist;
        float y = x * Mathf.Tan(rads) - ((g * x * x) / (2 * Mathf.Pow(velocity * Mathf.Cos(rads), 2)));
        float z = 0f;

        return new Vector3(z, y, x);
    }
}
