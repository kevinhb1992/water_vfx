using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBallControl : MonoBehaviour
{
    [SerializeField] bool _update = true;  //KEVIN: give condition when to activate, for now true
    [SerializeField] Transform _CreationPoint;
    [SerializeField] WaterBall WaterBallPrefab;
    WaterBall waterBall;

    private void Update()
    {
        if (!_update)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //KEVIN: Debug.Log("Left mouse button clicked");

            if (WaterBallCreated())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (waterBall != null)
                    {
                        BreakWaterBall(hit.point);
                    }
                }
                
            }
            else
            {
                CreateWaterBall();
            }
        }
    }
    public bool WaterBallCreated()
    {
        return waterBall != null;
    }
    public void CreateWaterBall()
    {
        waterBall = Instantiate(WaterBallPrefab, _CreationPoint.position, Quaternion.identity);
    }

    public void BreakWaterBall(Vector3 pos)
    {
        waterBall.Break(pos);
    }
}
