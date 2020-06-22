using UnityEngine;

public class CircleMeshGenerator
{

    public void GenerateMesh(Mesh mesh, float arcStart, float arcSize, float radius, int resolution)
    {
        if (mesh == null)
            return;

        mesh.Clear();
        bool isFullCircle = IsFullCircle(arcSize);
        mesh.vertices = GetVertices(arcStart, arcSize, radius, resolution, isFullCircle);
        mesh.triangles = GetTriangles(mesh.vertices.Length, isFullCircle);
    }

    private Vector3[] GetVertices(float arcStart, float arcSize, float radius, int resolution, bool isFullCircle)
    {
        Vector3[] output = new Vector3[GetNumberOfVertices(resolution, isFullCircle)];
        output[0] = Vector3.zero;

        float oneStepAngle = arcSize / (float)resolution;

        for (int i = 1; i < output.Length; i++)
        {
            float angle = (arcStart + (i - 1) * oneStepAngle) * Mathf.Deg2Rad;
            output[i] = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
        }


        return output;
    }

    private int[] GetTriangles(int numberOfVertices, bool isFullCircle)
    {
        int[] output = new int[GetNumberOfTriangles(numberOfVertices, isFullCircle)];


        for (int i = 0; i < output.Length; i += 3)
        {
            output[i] = 0;
            int index = i / 3 + 1;
            output[i + 1] = (index + 1) % numberOfVertices;
            output[i + 2] = (index) % numberOfVertices;
        }
        if (isFullCircle)
        {
            output[output.Length - 3] = 0;
            output[output.Length - 2] = 1;
            output[output.Length - 1] = numberOfVertices - 1;
        }

        return output;
    }

    private int GetNumberOfVertices(int resolution, bool isFullCircle)
    {
        return resolution + (isFullCircle ? 1 : 2);
    }

    private int GetNumberOfTriangles(int numberOfVertices, bool isFullCircle)
    {
        return (numberOfVertices - (isFullCircle ? 1 : 2)) * 3;
    }

    private bool IsFullCircle(float arcSize)
    {
        return arcSize == 360;
    }
}
