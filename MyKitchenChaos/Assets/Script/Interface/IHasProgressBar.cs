using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 需要实现进度条的柜台的接口
/// </summary>
public interface IHasProgressBar{
    public event EventHandler<EventArgsOnProgressBarChanged> OnProgressBarChanged;
    public class EventArgsOnProgressBarChanged : EventArgs
    {
        public float progressNormalized; // 注意到一个细节，如果public类里面不使用的public字段，不会触发未使用提醒。
    }
}