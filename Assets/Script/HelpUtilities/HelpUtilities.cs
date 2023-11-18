using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelpUtilities
{
    public static List<string> enemyNames = new List<string>() { "Riley Harper","Savannah Brooks","Ethan Mitchell","Lily Anderson","Jackson Foster",
"Olivia Hayes","Caleb Turner","Ava Bennett","Mason Sullivan","Zoe Harrison","Noah Parker","Emma Coleman","Aiden Taylor","Mia Richardson","Lucas Reynolds",
"Isabella Morgan","Logan Carter","Sophia Davis","Carter Mitchell","Grace Evans"};
    /// <summary>
    /// Get random position from a targetPosition within the offset rectang area
    /// </summary>
    public static Vector2 GetRandomPositionOutBoundary(Vector2 targetPosition, float xLimit, float yLimit, float xDetal, float yDelta)
    {
        float xMaxInBoundary = targetPosition.x + xLimit;
        float xMinInBoundary = targetPosition.x - xLimit;
        float yMaxInBoundary = targetPosition.y + yLimit;
        float yMinInBoundary = targetPosition.y - yLimit;

        float xMaxOutBoundary = targetPosition.x + xLimit + xDetal;
        float xMinOutBoundary = targetPosition.x - xLimit - xDetal;
        float yMaxOutBoundary = targetPosition.y + yLimit + yDelta;
        float yMinOutBoundary = targetPosition.y - yLimit - yDelta;

        float xPosition;
        float yPosition;
        xPosition = Random.Range(xMinOutBoundary, xMaxOutBoundary);
        if (xPosition < xMinInBoundary || xPosition > xMaxInBoundary)
        {
            yPosition = Random.Range(yMinOutBoundary, yMaxOutBoundary);
        }
        else
        {
            List<float> tempList = new List<float>() { Random.Range(yMaxInBoundary, yMaxOutBoundary), Random.Range(yMinInBoundary, yMinOutBoundary) };
            yPosition = tempList[Random.Range(0, 2)];
        }

        return new Vector2(xPosition, yPosition);
    }
    /// <summary>
    /// Get the angle in degrees from a direction vector
    /// </summary>
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);

        float degrees = radians * Mathf.Rad2Deg;

        return degrees;

    }

    /// <summary>
    /// Get the direction vector from an angle in degrees
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetDirectionVectorFromAngle(float angle)
    {
        Vector3 directionVector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0f);
        return directionVector;
    }

}
