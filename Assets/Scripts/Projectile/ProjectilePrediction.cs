using UnityEngine;

public class ProjectilePrediction
{
    public static Vector3 Predict(Vector3 projectileVeloctiy, Vector3 targetVelocity, Vector3 targetCurrentPosition, Vector3 projectileSpawnPoint)
    {
        Vector3 output = targetCurrentPosition;
        if (targetVelocity == Vector3.zero)
        {
            return output;
        }
        else
        {
            //Get distance between shooting object and bullet spawn point
            float distance = Vector2.Distance(targetCurrentPosition, projectileSpawnPoint);
            float projectileSpeed = projectileVeloctiy.magnitude * Time.fixedDeltaTime;
            float travelTime = distance / projectileSpeed;
            Vector3 predictedPosition = (targetCurrentPosition + targetVelocity * travelTime);
            distance = Vector2.Distance(predictedPosition, projectileSpawnPoint);
            travelTime = distance / projectileSpeed;
            output = targetCurrentPosition + targetVelocity * travelTime;
        }


        return output;
    }

    public static Vector3 Predict(float projectileVeloctiy, Vector3 targetVelocity, Vector3 targetCurrentPosition, Vector3 projectileSpawnPoint)
    {
        Vector3 output = targetCurrentPosition;
        if (targetVelocity == Vector3.zero)
        {
            return output;
        }
        else
        {
            //Get distance between shooting object and bullet spawn point
            float distance = Vector2.Distance(targetCurrentPosition, projectileSpawnPoint);
            float projectileSpeed = projectileVeloctiy * Time.fixedDeltaTime;
            float travelTime = distance / projectileSpeed;
            Vector3 predictedPosition = (targetCurrentPosition + targetVelocity * travelTime);
            distance = Vector2.Distance(predictedPosition, projectileSpawnPoint);
            travelTime = distance / projectileSpeed;
            output = targetCurrentPosition + targetVelocity * travelTime;
        }


        return output;
    }
}
