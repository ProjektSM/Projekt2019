using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxBackground : MonoBehaviour
{
    public float parallaxSpeed;
    private float cameraLast;
    private Transform cameraTransform;
    private Transform[] childTransform;

    [SerializeField]
    private float backgroundSize;

    private int leftChild;
    private int rightChild;

    void Start()
    {

        cameraTransform = Camera.main.transform;

        childTransform = new Transform[transform.childCount];
        for (int i=0; i < transform.childCount;++i)
        {
            childTransform[i] = transform.GetChild(i);
        }
        leftChild = 0;
        rightChild = transform.childCount - 1;
 
    }
    void GoLeft()
    {
        int lastLeft = leftChild;
     
        childTransform[rightChild].position = new Vector3(
            childTransform[leftChild].position.x - backgroundSize, 
            childTransform[leftChild].position.y, 
            childTransform[leftChild].position.z);

        leftChild = rightChild;
        rightChild--;
        if(rightChild < 0)
        {
            rightChild = childTransform.Length - 1;
        }
    }
    void GoRight()
    {
        int lastRight = rightChild;
        
        childTransform[leftChild].position = new Vector3(
            childTransform[rightChild].position.x + backgroundSize,
            childTransform[rightChild].position.y,
            childTransform[rightChild].position.z);

        rightChild = leftChild;
        leftChild++;
        if (leftChild == childTransform.Length)
        {
            leftChild = 0;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float delta = cameraTransform.position.x - cameraLast;
        cameraLast = cameraTransform.position.x;

        transform.position += new Vector3(delta * parallaxSpeed,0,0);

        if (cameraTransform.position.x < (childTransform[leftChild].transform.position.x))
        {
            GoLeft();
        }
        if (cameraTransform.position.x > (childTransform[rightChild].transform.position.x ))
        {
            GoRight();
        }
        for (int i = 0; i < transform.childCount; ++i)
        {
            childTransform[i].position = new Vector3(childTransform[i].position.x, cameraTransform.position.y, childTransform[i].position.z);
        }
    }
}
