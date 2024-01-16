using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlatformMovement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray ray;
            if (Input.touchCount > 0)
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            else
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                TurnPlatforms clicktable = hit.transform.GetComponent<TurnPlatforms>();
                clicktable.Rotate();
            }
        }
    }

    public void AddMoves()
    {
        
        Time.timeScale = 1;
    }
}