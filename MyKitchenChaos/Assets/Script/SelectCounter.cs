using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounter : MonoBehaviour
{
    private bool isSeleceted = false;
    [SerializeField] private GameObject[] selecetVisual;
    [SerializeField] private BaseCounter baseCounter;
    // 订阅事件
    private void Start()
    {
        Player.Instance.OnSelcetedCounterChanged += Player_OnSelcetedCounterChanged;
        Debug.Log(gameObject.name + " 已注册消息");
    }

    private void Player_OnSelcetedCounterChanged(object sender, Player.OnSelcetedCounterChangedEventArgs e)
    {
        isSeleceted = baseCounter == e.selectedCounter;
        if (isSeleceted)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var obj in selecetVisual)
        {
            obj.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach(var obj in selecetVisual)
        {
            obj.SetActive(false);  
        }
    }
}
