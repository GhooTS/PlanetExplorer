using UnityEngine;

public class DonutMeshGenerator
{

    public void GenerateMesh(Mesh mesh, float arcStart, float arcSize, float innerRadius, float outerRadius, int resolution)
    {
        if (mesh == null)
            return;

        mesh.Clear();
        bool isFullDonut = IsFullDonut(arcSize);
        mesh.vertices = GetVertices(arcStart, arcSize, innerRadius, outerRadius, resolution, isFullDonut);
        mesh.triangles = GetTriangles(mesh.vertices.Length, isFullDonut);
    }

    private bool IsFullDonut(float arcSize)
    {
        return arcSize == 360;
    }

    private Vector3[] GetVertices(float arcStart, float arcSize, float innerRadius, float outerRadius, int resolution, bool isFullDonut)
    {
        Vector3[] output = new Vector3[GetNumberOfVertices(resolution, isFullDonut)];

        float oneStepAngle = arcSize / (float)resolution;

        for (int i = 0; i < output.Length; i += 2)
        {
            float angle = (arcStart + (i / 2) * oneStepAngle) * Mathf.Deg2Rad;
            output[i] = new Vector3(innerRadius * Mathf.Cos(angle), innerRadius * Mathf.Sin(angle));
            output[i + 1] = new Vector3(outerRadius * Mathf.Cos(angle), outerRadius * Mathf.Sin(angle));
        }


        return output;
    }

    private int[] GetTriangles(int numberOfVertices, bool isFullDonut)
    {
        int[] output = new int[GetNumberOfTriangles(numberOfVertices, isFullDonut)];


        for (int i = 0; i < output.Length; i += 6)
        {
            int index = i / 3;

            output[i] = index;
            output[i + 1] = (index + 3) % numberOfVertices;
            output[i + 2] = (index + 1) % numberOfVertices;

            output[i + 3] = index;
            output[i + 4] = (index + 2) % numberOfVertices;
            output[i + 5] = (index + 3) % numberOfVertices;
        }

        return output;
    }

    private int GetNumberOfVertices(int resolution, bool isFullDonut)
    {
        return resolution * 2 + (isFullDonut ? 0 : 2);
    }

    private int GetNumberOfTriangles(int numberOfVertices, bool isFullDonut)
    {
        return (numberOfVertices - (isFullDonut ? 0 : 2)) * 3;
    }
}
