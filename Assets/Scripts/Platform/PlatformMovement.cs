using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private bool _gameHasBegun = false;
    private float lastClickTime = 0f;
    private float clickDelay = 0.5f;
    private bool canClick = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClick && Time.time - lastClickTime >= clickDelay || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray ray;
            if (Input.touchCount > 0)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            }
            else
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }

            RaycastHit hit;

            if (Physics.Raycast(ray,out hit) && _gameHasBegun == true)
            {
                if (hit.transform.GetComponent<RotationPlatform>())
                {             
                    RotationPlatform clicktable = hit.transform.GetComponent<RotationPlatform>();
                    clicktable.Rotate();             
                }
            }
            lastClickTime = Time.time;
            canClick = false;
            StartCoroutine(UnlockButtonClick());
        }
    }

    IEnumerator UnlockButtonClick()
    {
        yield return new WaitForSeconds(clickDelay);
        canClick = true;
    }

    public bool GameStart()
    {
        _gameHasBegun = true;

        return _gameHasBegun;
    }

    public void AddMoves()
    {      
        Time.timeScale = 1;
    }
}