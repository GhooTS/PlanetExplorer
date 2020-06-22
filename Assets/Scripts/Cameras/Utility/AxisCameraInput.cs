using UnityEngine;

[System.Serializable]
public class AxisCameraInput : ICameraInput
{
    public bool axisSmoothing = false;
    public string horizontal = "Horizontal";
    public string vertical = "Vertical";


    public float GetHoriozntal()
    {
        return GetAxis(horizontal);
    }

    public float GetVertical()
    {
        return GetAxis(vertical);
    }

    private float GetAxis(string axis)
    {
        return axisSmoothing ? Input.GetAxis(axis) : Input.GetAxisRaw(axis);
    }
}