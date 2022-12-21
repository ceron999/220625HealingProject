using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    PortalManager portalManager;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name =="Mir")
            TakePortal();
    }

    void TakePortal()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            portalManager.MoveScene(this.name);
        }
    }
}
