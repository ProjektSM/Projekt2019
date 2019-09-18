using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class OnPress : UIBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{

    public bool shrink = false;


    public bool rotate = false;
    private bool isRotated = true;
    public UnityEvent onLongPress = new UnityEvent();
    private bool isPointerDown = false;



    private void Update()
    {
        if (isPointerDown)
        {
            onLongPress.Invoke();  
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(shrink)
            transform.transform.localScale = new Vector3(0.9f,0.9f,1);

        if (rotate)
        {
            isRotated = !isRotated;
            if (isRotated)
                transform.rotation = Quaternion.AngleAxis(0,new Vector3(0,0,1));
            else
                transform.rotation = Quaternion.AngleAxis(90, new Vector3(0, 0, 1));

        }
           
        isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (shrink)
            transform.transform.localScale = new Vector3(1, 1, 1);
        isPointerDown = false;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (shrink)
            transform.transform.localScale = new Vector3(1, 1, 1);
        isPointerDown = false;
    }
}