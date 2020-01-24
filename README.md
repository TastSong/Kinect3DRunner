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

## 第三部分：游戏开始动作识别

