using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflector : MonoBehaviour
{
    Vector3 position;
    Vector3 direction;
    LineRenderer lr;
    bool isOpen;

    GameObject tempReflector;
    void Start()
    {
        isOpen = false;
        lr = gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isOpen)
        {
            lr.positionCount = 2;
            lr.SetPosition(0, position);
            RaycastHit hit;

            if (Physics.Raycast(position, direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Reflector"))
                {
                    tempReflector = hit.collider.gameObject;
                    Vector3 temp = Vector3.Reflect(direction, hit.normal);
                    hit.collider.gameObject.GetComponent<LaserReflector>().OpenRay(hit.point, temp);
                }
                lr.SetPosition(1, hit.point);
            }
            else
            {
                if (tempReflector)
                {
                    tempReflector.GetComponent<LaserReflector>().CloseRay();
                }
                lr.SetPosition(1, direction * 100);
            }
        }

    }

    public void OpenRay(Vector3 pos, Vector3 dir)
    {
        isOpen = true;
        position = pos;
        direction = dir;
    }

    public void CloseRay()
    {
        isOpen = false;
        lr.positionCount = 0;
    }
}