using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatform : MonoBehaviour
{
    private SpriteRenderer renderer;
    private BoxCollider2D collider;

    private float opacity;


    private bool isFading;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        opacity = 1f;
        StartCoroutine(StartFading());
    }

    // Update is called once per frame
    void Update()
    {
        if(opacity <= 0f)
        {
            collider.enabled = false;
            StartCoroutine(UnFading());
        }
        if(opacity >= 1 && !isFading)
        {

            
            StartCoroutine(StartFading());
        }

        if (isFading && opacity > 0f)
        {
            opacity -= 0.02f * Time.deltaTime * 50;
        }
        else if(opacity < 1f)
        {
            opacity += 0.02f * Time.deltaTime * 50;
        }

        renderer.color = new Color(1f, 1f, 1f, opacity);


    }
    public IEnumerator StartFading()
    {
        yield return new WaitForSeconds(5f);
        isFading = true;
    }
    public IEnumerator UnFading()
    {
        yield return new WaitForSeconds(3f);
        isFading = false;
        collider.enabled = true;
    }
}
