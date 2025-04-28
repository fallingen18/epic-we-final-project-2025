using UnityEngine;

public class TestTwoHandGrab : MonoBehaviour
{
    public TwoHandGrabDetector grabDetector;
    public Material materialToChange; // 🎨 Drag your material here in the Inspector

    private void Start()
    {
        if (grabDetector != null)
        {
            grabDetector.OnSecondHandGrabbed += HandleSecondHandGrab;
        }
    }

    private void HandleSecondHandGrab()
    {
        // Do something when the second hand grabs
        Debug.Log("Second hand just grabbed the object!");

        if (materialToChange != null)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            materialToChange.color = randomColor;
            Debug.Log($"Changed material color to: {randomColor}");
        }
        else
        {
            Debug.LogWarning("No material assigned to change color.");
        }
    }
}
