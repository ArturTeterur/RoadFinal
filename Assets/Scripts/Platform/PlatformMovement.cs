using System.Collections;
using Scripts.Platform.Rotation;
using UnityEngine;

namespace Scripts.Platform.PlatformMovement
{
    public class PlatformMovement : MonoBehaviour
    {
        [SerializeField] private bool _gameHasBegun = false;

        private float _lastClickTime = 0f;
        private float _clickDelay = 0.5f;
        private bool _canClick = true;

        private void Update()
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
            ((Input.GetMouseButtonDown(0) && _canClick) && Time.time - _lastClickTime >= _clickDelay))
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

                if (Physics.Raycast(ray, out hit) && _gameHasBegun == true)
                {
                    if (hit.transform.GetComponent<RotationPlatform>())
                    {
                        RotationPlatform clicktable = hit.transform.GetComponent<RotationPlatform>();
                        clicktable.Rotate();
                    }
                }

                _lastClickTime = Time.time;
                _canClick = false;
                StartCoroutine(UnlockButtonClick());
            }
        }

        public bool GameStart()
        {
            _gameHasBegun = true;

            return _gameHasBegun;
        }

        private IEnumerator UnlockButtonClick()
        {
            yield return new WaitForSeconds(_clickDelay);
            _canClick = true;
        }
    }
}