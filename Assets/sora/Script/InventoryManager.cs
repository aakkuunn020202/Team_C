using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    // じょうろを持っているかどうかのフラグ
    public bool hasWateringCan = false;

    void Start()
    {
        
    }


    // テスト用：スペースキーを押したらじょうろを手に入れたことにする
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            hasWateringCan = true;
            Debug.Log("じょうろを装備した！");
        }
    }
}
