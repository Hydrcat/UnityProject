using System;
using UnityEngine;

public class Player : MonoBehaviour,IKichenHolder
{
    // 实现一个玩家单例
    public static Player Instance { get; private set; }


    [SerializeField] private float speed = 1.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private GameObject playerKitchenObjectHolder;

    private bool isWalking = false;
    private Vector3 lastInteractDir;
    private BaseCounter selecetedCounter;
    private KitchenObject kitchenObject;

    public event EventHandler<OnSelcetedCounterChangedEventArgs> OnSelcetedCounterChanged;
    public class OnSelcetedCounterChangedEventArgs
    {
        public BaseCounter selectedCounter;
    }
   
    /********************** 虚方法区 ********************/
    public bool IsWalking()
    {
        return isWalking;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void Awake()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;

        if (Instance != null)
        {
            Debug.LogError("Player Instance 重复:" + Instance.name);
        }
        Instance = this;
    }



    private void Start()
    {
  

    }

    /********************* 事件回调区 ****************************/
    // 这里写交互逻辑，其实可以简单的考虑，当前高亮的柜台就是需求交互的柜台
    // TODO: 简写，高亮交互逻辑
    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        // 碰撞检测
        float interactDistance = 2f;

        bool canInteract = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hitInfo, interactDistance, countersLayerMask);

        if (canInteract)
        {
            if (hitInfo.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter))
            {
                baseCounter.InteractAlternate(this);
            }
        }
    }
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        // 碰撞检测
        float interactDistance = 2f;

        bool canInteract = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hitInfo, interactDistance, countersLayerMask);

        if (canInteract)
        {
            if (hitInfo.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter))
            {
                baseCounter.Interact(this);
            }
        }
    }

    //交互事件处理
    private void HandleInteractions()
    {
       
        Vector2 velocity = gameInput.GetInputNormalized();
        Vector3 moveDir = new Vector3(velocity.x, 0f, velocity.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        moveDir = lastInteractDir;

        // 碰撞检测
        float interactDistance = 2f;

        bool canInteract = Physics.Raycast(transform.position,moveDir,out RaycastHit hitInfo, interactDistance,countersLayerMask);

        if (canInteract)
        {
            if (hitInfo.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter))
            {
                if (selecetedCounter != baseCounter)
                {
                    selecetedCounter = baseCounter;
                    
                }
                else
                {

                }
            }
            else
            {
                selecetedCounter = null;
            }
        }
        else
        {
            selecetedCounter = null;
        }
        OnSelcetedCounterChanged?.Invoke(Instance, new OnSelcetedCounterChangedEventArgs { selectedCounter = selecetedCounter });

    }

    //移动处理
    private void HandleMovement()
    {
        Vector2 velocity = gameInput.GetInputNormalized();
        Vector3 originMoveDir = new Vector3(velocity.x, 0f, velocity.y);
        Vector3 moveDir = new Vector3(velocity.x, 0f, velocity.y);



        // 碰撞检测
        float moveDistance = Time.deltaTime * speed;
        float playerHeight = 1.8f;
        float playerRadius = 0.5f;

        //TODO: 重构这一块
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {

                }
            }
        }

        if (canMove)
        {
            transform.position +=  moveDistance* moveDir;
        }
        isWalking = moveDir != Vector3.zero; // 更新目前的移动状态

        float roateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, originMoveDir, roateSpeed * Time.deltaTime);
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kObject)
    {
        if (kObject == null)
        {
            throw new ArgumentNullException(nameof(kObject));
        }
        this.kitchenObject = kObject;
    }

    public GameObject GetKitchenObjectHolder()
    {
        return playerKitchenObjectHolder;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
}
