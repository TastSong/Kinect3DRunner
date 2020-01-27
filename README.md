# Kinect3DRunner
## 第一部分：基本3D跑酷建立

* `GameController.cs` 游戏基本逻辑控制

* `AnimationManager.cs` 游戏声音控制

* `PlayerController.cs`游戏运行控制

* `UIController.cs`游戏UI画面控制

## 第二部分：游戏中动作识别

1. 导入`KinectMaganer.cs`
2. 导入`KinectGestures.cs`
3. 新建动作监听器`GesturesLitener.cs`，进行监听左滑、右滑、跳跃
4. 新建动作反应器KinectMovementController.cs，对左右滑动和跳跃进行识别

> (2020.2.24调试此处是应该禁用RightHandWith.cs，因为它会失活第一场景的KinectManager.cs)

## 第三部分：游戏开始动作识别

1. 导入手部状态的`imge`
2. 导入水果忍者中的`RightHandWith.cs`
3. 修改`RightHandWith.cs`，删除Button3，修改在触发Button1、2后的效果，尤其是2，应该触发后调用Play函数，并且会在激活游戏界面后使它失活,结束第一场景的`KinectManager.cs`

> （要仔细调试2020.1.27，触发button后的处理方法）

4. 需要让`RightHandImage`失活，这个应该让调用GameController.cs中的Play函数