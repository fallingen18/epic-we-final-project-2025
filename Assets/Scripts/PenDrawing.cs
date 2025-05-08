using UnityEngine;
using Oculus.Interaction;
using System.Collections.Generic;

[RequireComponent(typeof(Grabbable))]
public class PenDrawing : MonoBehaviour
{
    [Tooltip("Transform at the tip of the pen.")]
    public Transform drawTip;

    [Tooltip("LineRenderer prefab.")]
    public GameObject linePrefab;

    [Tooltip("Minimum distance between points.")]
    public float drawDistanceThreshold = 0.01f;

    private Grabbable grabbable;
    private bool isGrabbed = false;
    private bool isDrawing = false;

    private LineRenderer currentLine;
    private List<Vector3> currentPositions = new List<Vector3>();
    private List<GameObject> allLines = new List<GameObject>();

    private List<GameObject> currentColliders = new List<GameObject>();


    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();

        if (drawTip == null || linePrefab == null)
        {
            Debug.LogError("DrawTip and LinePrefab must be assigned.");
            enabled = false;
            return;
        }

        grabbable.WhenPointerEventRaised += HandlePointerEvent;
    }

    private void OnDestroy()
    {
        grabbable.WhenPointerEventRaised -= HandlePointerEvent;
    }

    private void HandlePointerEvent(PointerEvent evt)
    {
        switch (evt.Type)
        {
            case PointerEventType.Select:
                isGrabbed = true;
                break;
            case PointerEventType.Unselect:
                isGrabbed = false;
                StopDrawing();
                break;
        }
    }

    private void Update()
    {
        if (!isGrabbed) return;

        bool triggerPressed = OVRInput.Get(OVRInput.RawButton.RIndexTrigger) || OVRInput.Get(OVRInput.RawButton.LIndexTrigger);

        if (triggerPressed)
{
    if (!isDrawing) StartNewLine();

    Vector3 currentPos = drawTip.position;

    if (currentPositions.Count == 0 || Vector3.Distance(currentPositions[^1], currentPos) > drawDistanceThreshold)
    {
        currentPositions.Add(currentPos);
        currentLine.positionCount = currentPositions.Count;
        currentLine.SetPositions(currentPositions.ToArray());

        // Add this block to create a capsule collider between the last two points
        if (currentPositions.Count >= 2)
        {
            Vector3 start = currentPositions[^2];
            Vector3 end = currentPositions[^1];
            CreateCapsuleCollider(start, end); // <- This is the method you define below
        }
    }
}
else
{
    StopDrawing();
}

    }

    private void StartNewLine()
{
    GameObject newLineObj = Instantiate(linePrefab);
    currentLine = newLineObj.GetComponent<LineRenderer>();
    currentLine.positionCount = 0;
    currentLine.startWidth = 0.01f;
    currentLine.endWidth = 0.01f;
    currentLine.useWorldSpace = true;

    // 🔧 Assign a new instance of the material with the current line color
    if (currentLine.material != null)
    {
        Material newMat = new Material(currentLine.material);
        newMat.color = LineColorManager.lineColor;
        currentLine.material = newMat;
    }

    currentPositions.Clear();
    allLines.Add(newLineObj);
    isDrawing = true;
}


    private void StopDrawing()
    {
        isDrawing = false;
        currentLine = null;
        currentPositions.Clear();
    }

    public void ClearAllLines()
    {
        foreach (var line in allLines)
        {
            Destroy(line);
        }
        allLines.Clear();
    }

   private void CreateCapsuleCollider(Vector3 start, Vector3 end)
{
    GameObject segment = new GameObject("LineColliderSegment");
    segment.transform.parent = currentLine.transform;

    Vector3 midPoint = (start + end) / 2f;
    segment.transform.position = midPoint;
    segment.transform.LookAt(end);

    float length = Vector3.Distance(start, end);

    CapsuleCollider col = segment.AddComponent<CapsuleCollider>();

    // SCALE FACTOR — increase to make it bigger
    float thicknessMultiplier = 1.0f; // e.g. 2x thicker
    float radius = (currentLine.startWidth / 2f) * thicknessMultiplier;

    col.radius = radius;
    col.height = length + radius * 2f; // maintain proper capsule shape
    col.direction = 2; // Z axis
    col.isTrigger = true;

    segment.AddComponent<LineTriggerDelete>(); 


    currentColliders.Add(segment);
}
}
