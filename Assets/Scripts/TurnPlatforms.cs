using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlatforms : MonoBehaviour
{
    [SerializeField] private List<GameObject> _angles;
    [SerializeField] private GameObject _currentAngle;
    [SerializeField] private int _currentAngleIndex;

    public void Rotate()
    {
        if (_currentAngleIndex == _angles.Count || _currentAngleIndex == _angles.Count - 1)
        {
                 _currentAngleIndex = 0;          
            _angles[_currentAngleIndex].SetActive(true);
            _angles[_angles.Count-1].SetActive(false);
        }
        else
        {           
            _currentAngleIndex++;                   
            _angles[_currentAngleIndex].SetActive(true);
            _angles[_currentAngleIndex -1].SetActive(false);
        }       
    }
}
