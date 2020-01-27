using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;    //引用命名空间

public class RightHandWith : MonoBehaviour
{
    //定义跟踪的骨骼
    private KinectInterop.JointType trackedJoint = KinectInterop.JointType.HandRight;
    //得到canvas，在激活游戏界面后使它失活,结束第一场景的KinectManager
    public GameObject camera;
    //右手的图片对象，用来跟踪右手移动
    public Image rightHandImag;
    //识别的三个按钮
    public GameObject button1;
    public GameObject button2;
    //public GameObject button3;
    //手部握拳和打开，不同状态对应不同图片
    public Sprite handCloseSprite;
    public Sprite handOpenSprite;

    public float smoothFactor = 5f;
    private float distanceToCamera = 10f;

    void Start()
    {

    }

    void Update()
    {
        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized())
        {
            int iJointIndex = (int)trackedJoint;

            if (manager.IsUserDetected())
            {

                long userId = manager.GetPrimaryUserID();

                if (manager.IsJointTracked(userId, iJointIndex))
                {
                    ChangeRightHandImag(userId);
                    //得到骨骼的游戏界面的坐标
                    Vector3 vPosOverlay = GetjointPos(manager, userId, iJointIndex);
                    //将游戏界面的物体跟随骨骼移动
                    rightHandImag.transform.position = Vector3.Lerp(rightHandImag.transform.position, vPosOverlay, smoothFactor * Time.deltaTime);

                    //判断是否点击按钮
                    bool isClickButton1 = IsClickButton(vPosOverlay, button1, userId);
                    bool isClickButton2 = IsClickButton(vPosOverlay, button2, userId);
                    //bool isClickButton3 = IsClickButton(vPosOverlay, button3, userId);
                    if (isClickButton1)
                    {
                        //Rigidbody2D r1 = button1.GetComponent<Rigidbody2D>();
                        //r1.gravityScale = 30;
                        //r1.AddForce(new Vector2(0, 800));
                        print("点击按钮1");
                    }
                    if (isClickButton2)
                    {
                        //Rigidbody2D r2 = button2.GetComponent<Rigidbody2D>();
                        //r2.gravityScale = 30;
                        //r2.AddForce(new Vector2(0, 800));
                        print("点击按钮2");
                        //加载游戏场景
                        //camera.SetActive(false);
                        //SceneManager.LoadScene(1);    //括号内加入场景名字 （字符串类型）
                    }
                    //if (isClickButton3)
                    //{
                    //    Rigidbody2D r3 = button3.GetComponent<Rigidbody2D>();
                    //    r3.gravityScale = 30;
                    //    r3.AddForce(new Vector2(0, 800));
                    //    print("点击按钮3");
                    //}
                }

            }

        }
    }
    //得到骨骼的游戏界面的坐标
    Vector3 GetjointPos(KinectManager manager, long userId, int iJointIndex)
    {
        Vector3 posJoint = manager.GetJointKinectPosition(userId, iJointIndex);

        if (posJoint != Vector3.zero)
        {
            // 3 d位置深度
            Vector2 posDepth = manager.MapSpacePointToDepthCoords(posJoint);
            ushort depthValue = manager.GetDepthForPixel((int)posDepth.x, (int)posDepth.y);

            if (depthValue > 0)
            {
                // 颜色pos深度pos
                Vector2 posColor = manager.MapDepthPointToColorCoords(posDepth, depthValue);

                float xNorm = (float)posColor.x / manager.GetColorImageWidth();
                float yNorm = 1.0f - (float)posColor.y / manager.GetColorImageHeight();

                if (rightHandImag)
                {
                    Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(xNorm, yNorm, distanceToCamera));
                    return vPosOverlay;
                }
            }
        }
        return Vector3.zero;
    }
    bool IsRightHandClosed(long userId)
    {
        KinectInterop.HandState rightHandState = KinectManager.Instance.GetRightHandState(userId);
        if (rightHandState == KinectInterop.HandState.Closed)
        {
            return true;
        }
        return false;
    }
    void ChangeRightHandImag(long userId)
    {
        //根据手势切换手部图片
        bool isRightHandClosed = IsRightHandClosed(userId);
        if (isRightHandClosed)
        {
            rightHandImag.sprite = handCloseSprite;
        }
        else
        {
            rightHandImag.sprite = handOpenSprite;
        }
    }
    bool IsClickButton(Vector3 vPosOverlay, GameObject button, long userId)
    {
        Vector2 joinsPos = new Vector2(vPosOverlay.x, vPosOverlay.y);
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)
            button.transform, joinsPos, null))
        {
            bool isRightHandColsed = IsRightHandClosed(userId);
            return isRightHandColsed;
        }
        return false;
    }
}
