using UnityEngine;

[ExecuteInEditMode]
public class MirrorReflection : MonoBehaviour
{
    public GameObject mirrorPlane;

    private Camera reflectionCamera;

    void Start()
    {
        reflectionCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (mirrorPlane == null || reflectionCamera == null)
            return;

        // Calculate the reflection matrix
        Vector3 normal = mirrorPlane.transform.up;
        Vector3 position = mirrorPlane.transform.position;
        Matrix4x4 reflectionMatrix = CalculateReflectionMatrix(position, normal);

        // Set the reflection camera's view matrix
        reflectionCamera.worldToCameraMatrix = Camera.main.worldToCameraMatrix * reflectionMatrix;

        // Adjust the clipping plane
        Vector4 clipPlane = CameraSpacePlane(reflectionCamera, position, normal, 1.0f);
        reflectionCamera.projectionMatrix = Camera.main.CalculateObliqueMatrix(clipPlane);
    }

    private Matrix4x4 CalculateReflectionMatrix(Vector3 planePosition, Vector3 planeNormal)
    {
        float d = -Vector3.Dot(planeNormal, planePosition);
        Matrix4x4 reflectionMat = new Matrix4x4();

        reflectionMat.m00 = 1 - 2 * planeNormal.x * planeNormal.x;
        reflectionMat.m01 = -2 * planeNormal.x * planeNormal.y;
        reflectionMat.m02 = -2 * planeNormal.x * planeNormal.z;
        reflectionMat.m03 = -2 * planeNormal.x * d;

        reflectionMat.m10 = -2 * planeNormal.y * planeNormal.x;
        reflectionMat.m11 = 1 - 2 * planeNormal.y * planeNormal.y;
        reflectionMat.m12 = -2 * planeNormal.y * planeNormal.z;
        reflectionMat.m13 = -2 * planeNormal.y * d;

        reflectionMat.m20 = -2 * planeNormal.z * planeNormal.x;
        reflectionMat.m21 = -2 * planeNormal.z * planeNormal.y;
        reflectionMat.m22 = 1 - 2 * planeNormal.z * planeNormal.z;
        reflectionMat.m23 = -2 * planeNormal.z * d;

        reflectionMat.m30 = 0;
        reflectionMat.m31 = 0;
        reflectionMat.m32 = 0;
        reflectionMat.m33 = 1;

        return reflectionMat;
    }

    private Vector4 CameraSpacePlane(Camera cam, Vector3 planePos, Vector3 planeNormal, float sideSign)
    {
        Vector3 offsetPos = planePos + planeNormal * 0.07f; // Offset to avoid z-fighting
        Vector3 cameraSpacePos = cam.worldToCameraMatrix.MultiplyPoint(offsetPos);
        Vector3 cameraSpaceNormal = cam.worldToCameraMatrix.MultiplyVector(planeNormal).normalized * sideSign;
        return new Vector4(cameraSpaceNormal.x, cameraSpaceNormal.y, cameraSpaceNormal.z, -Vector3.Dot(cameraSpacePos, cameraSpaceNormal));
    }
}