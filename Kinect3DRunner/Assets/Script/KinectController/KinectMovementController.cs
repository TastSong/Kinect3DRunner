using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//此脚本用于测试Kinect的动作识别，由于现在2020.1.24没有Kinect所以只能这样测试
//如果动作测试没有问题，就将Kinect的动作检测放入PlayController.cs脚本中的
//MoveForward()    MoveLeftRight()
public class KinectMovementController : MonoBehaviour
{
    private GestureListener gestureListener;
    // Start is called before the first frame update
    void Start()
    {
        gestureListener = GestureListener.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(gestureListener)
        {
            if(gestureListener.IsSwipeLeft())
            {
                PlayerController.instance.MoveRight();
                Debug.Log("Kinect动作识别后左移");
            }
            if(gestureListener.IsSwipeRight())
            {
                PlayerController.instance.MoveLeft();
                Debug.Log("Kinect动作识别后右移");
            }
            if(gestureListener.IsJumpUp())
            {
                PlayerController.instance.JumpUp();
                Debug.Log("Kinect动作识别后跳跃");
            }
        }
    }
}
