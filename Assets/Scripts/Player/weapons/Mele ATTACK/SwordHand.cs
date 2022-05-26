using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHand : MonoBehaviour
{
    [SerializeField]
    private Transform weaponArm;


    // Update is called once per frame
    void Update()
    {
        MoveGunWithMouse();
    }

    private void MoveGunWithMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        if (mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponArm.localScale = new Vector3(1f,  1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            weaponArm.localScale = Vector3.one;
        }
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        //Debug.Log("angle of gun : " + angle);
        weaponArm.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnEnable()
    {
        weaponArm.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        weaponArm.gameObject.SetActive(false);
    }
}
