using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerTrace : MonoBehaviour
{
    Enemy enemy;
    float yMaxBoundary;
    float yMinBoundary;
    float xMaxBoundary;
    float xMinBoundary;
    float xTransform;
    float yTransform;


    public void Start()
    {
        enemy = GetComponentInParent<Enemy>();

    }
    public void Update()
    {
        SetPowerPosition();
    }
 
    private void SetPowerPosition()
    {
        Vector2 mainCameraPosion = GameManager.Instance.mainCameraTransform.position;
        yMaxBoundary = mainCameraPosion.y + 10;
        yMinBoundary = mainCameraPosion.y - 10;
        xMaxBoundary = mainCameraPosion.x + 20;
        xMinBoundary = mainCameraPosion.x - 20;
        if (!enemy.IsMoveToCameraBoundary())
        {
            xTransform = this.transform.parent.position.x;
            yTransform = this.transform.parent.position.y - 1;
            return;
        }

        else

        {

            if (this.transform.parent.position.x >= xMaxBoundary)
            {
                xTransform = xMaxBoundary - 1;
                yTransform = this.transform.parent.position.y;
            }
            else if (this.transform.parent.position.x <= xMinBoundary)
            {
                xTransform = xMinBoundary + 1;
                yTransform = this.transform.parent.position.y;

            }
            else if (this.transform.parent.position.y >= yMaxBoundary)
            {
                xTransform = this.transform.parent.position.x;
                yTransform = yMaxBoundary - 1;
            }
            else if (this.transform.parent.position.y <= yMinBoundary)
            {
                xTransform = this.transform.parent.position.x;
                yTransform = yMinBoundary + 1;
            }
            this.transform.position = new Vector2(xTransform, yTransform);
        }

    }
}
