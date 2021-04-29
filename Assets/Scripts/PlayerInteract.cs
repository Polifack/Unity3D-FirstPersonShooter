using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    bool isInteracting()
    {
        return (Input.GetKey(KeyCode.E));
    }


    Ray getShootingRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        return ray;
    }
    LayerMask getLayerMask()
    {
        return GameManager.instance.whatIsInteractable;
    }

    private void Update()
    {
        Ray shootingRay = getShootingRay();
        RaycastHit hitInfo;
        if (Physics.Raycast(shootingRay, out hitInfo))
            {
            LayerMask target = getLayerMask();
            if ((target & 1 << hitInfo.collider.gameObject.layer) == 1 << hitInfo.collider.gameObject.layer)
            {
                InteractuableObject ec = hitInfo.collider.gameObject.GetComponentInParent<InteractuableObject>();
                ec.onAim();
                if (isInteracting())
                {
                        ec.onInteract();
                }
            }
        }
    }
}
