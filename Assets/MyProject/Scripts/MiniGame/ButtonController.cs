using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Image up, down, left, right;
    Sprite upDefault, downDefault, leftDefault, rightDefault;
    [SerializeField] Sprite upPress, downPress, leftPress, rightPress;

    private void Start()
    {
        upDefault = up.sprite;
        downDefault = down.sprite;
        leftDefault = left.sprite;
        rightDefault = right.sprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            up.sprite = upPress;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            up.sprite = upDefault;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            down.sprite = downPress;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            down.sprite = downDefault;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left.sprite = leftPress;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            left.sprite = leftDefault;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            right.sprite = rightPress;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            right.sprite = rightDefault;
        }
    }
}
