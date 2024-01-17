using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    bool isAbleToPunch;
    Animator anim;
    // Update is called once per frame
    private void Start()
    {
        anim = GetComponent<Animator>();
        isAbleToPunch = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAbleToPunch == true)
        {
            isAbleToPunch = false;
            anim.SetBool("Fist", true);
            Invoke("ResetAnim", 0.6f);
        }
    }
    void ResetAnim()
    {
        isAbleToPunch = true;
        anim.SetBool("Fist", false);
    }
}
