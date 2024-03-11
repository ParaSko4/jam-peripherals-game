using UnityEngine;

public static class TUtility
{
    public static void ChangeXZ(this Transform transform, Vector3 point)
    {
        transform.position = new Vector3(point.x, transform.position.y, point.z);
    }

    public static void ChangeX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public static void AddX(this Transform transform, float x)
    {
        transform.position += new Vector3(x, 0f, 0f);
    }

    public static void AddZ(this Transform transform, float z)
    {
        transform.position += new Vector3(0f, 0f, z);
    }
}