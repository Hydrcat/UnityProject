using System;
using UnityEngine;

public class SotveCounter : BaseCounter,IHasProgressBar
{
    // 火炉
    // 玩家可以放指定的厨房道具上，
    // 根据时间的进度，收获不同的道具。
    [SerializeField] private MyProgressBar progressBar;
    public enum State{
        Idle, //
        Cooking,
        Burned,
    }

    public State state ;
    //TODO 搞清楚unity中更通用的，设置timer的方法，或者学习一下协程的使用。
    private float stoveTimer;

    public event EventHandler<EventArgsOnStateChanged> OnStateChanged;
    public event EventHandler<IHasProgressBar.EventArgsOnProgressBarChanged> OnProgressBarChanged;

    public class EventArgsOnStateChanged:EventArgs{
        public State state;
    }

    private void Start() {
        state = State.Idle;

    }

    private void Update() {
        switch (state)
        {
            case State.Idle:
                // Idle状态下啥都不干。
                break;
            case State.Cooking:
                // 开火状态下计时器增长
                stoveTimer += Time.deltaTime;
                OnProgressBarChanged?.Invoke(this,new IHasProgressBar.EventArgsOnProgressBarChanged{
                    progressNormalized = stoveTimer / kitchenObject.GetKitchenObjectSO().StovingTime
                });
                // 检查cooked 进度，如果进度达成，则进行更换。
                if (IsReachCookedTime())
                {
                    stoveTimer = 0f;
                    // 删除旧的然后增加新的
                    KitchenObject nextKitchenObject = kitchenObject.GetKitchenObjectSO().StovingObject;
                    if (nextKitchenObject == null)
                    {
                        Debug.LogError("KitchenObjec"+kitchenObject.name+"do not have Stoved item");
                    }
                    kitchenObject.DestroySelf();
                    KitchenObject.AddKitchenObjectOnHolder(nextKitchenObject,this);


                    // 切换状态
                    if (kitchenObject.GetKitchenObjectSO().StovingObject == null)
                    {
                        state = State.Burned;
                        OnStateChanged?.Invoke(this,new EventArgsOnStateChanged{
                            state = state
                        });
                    }
                }
                
                break;
            case State.Burned:
                // 过火状态就停止。
                break;
            
        }


    }

    public override void Interact(Player player)
    {
        // 如果玩家手上持有道具 并且可烤
        if (player.HasKitchenObject() && !this.HasKitchenObject())
        {
            if (player.GetKitchenObject().IsStovable())
            {
                // 更换持有者为自己
                KitchenObject m_kitchenObject = player.GetKitchenObject();
                m_kitchenObject.SetKitchenObjectHolder(this);
                state = State.Cooking;
                stoveTimer = 0f;
                OnProgressBarChanged?.Invoke(this,new IHasProgressBar.EventArgsOnProgressBarChanged{
                    progressNormalized = stoveTimer / kitchenObject.GetKitchenObjectSO().StovingTime
                });
                OnStateChanged?.Invoke(this,new EventArgsOnStateChanged{
                    state = state
                });
                progressBar.gameObject.SetActive(true);
                return;
            }
        }

        // 如果玩家手上没有东西
        if (!player.HasKitchenObject() && this.HasKitchenObject())
        {
            KitchenObject m_kitchenObject = this.GetKitchenObject();
            m_kitchenObject.SetKitchenObjectHolder(player);
            state = State.Idle; 
            OnStateChanged?.Invoke(this,new EventArgsOnStateChanged{
                state = state
            });
            progressBar.gameObject.SetActive(false);
            return;
        }
    }

    private bool IsReachCookedTime() => stoveTimer >= kitchenObject.GetKitchenObjectSO().StovingTime;

}
