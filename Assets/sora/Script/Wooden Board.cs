using UnityEngine;
using UnityEngine.InputSystem;

public class WoodenBoard : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private InventoryManager inventoryManager;

    // プレイヤーが範囲内に入った
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            // プレイヤーのインベントリコンポーネントを取得
            inventoryManager = other.GetComponent<InventoryManager>();
        }
    }

    // プレイヤーが範囲から出た
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            inventoryManager = null;
        }
    }


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーが近くにいて、スペースキーを押したとき
        if (isPlayerNearby && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryGetPlank();
        }

    }

    void TryGetPlank()
    {
        if (inventoryManager != null && inventoryManager.hasWateringCan)
        {
            Debug.Log("じょうろを使って砂を流し、木の板をゲットした！");

            // TODO: ここにプレイヤーのインベントリに木の板を追加する処理を書く

            // 木の板を画面から消す
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("砂が固くて素手では掘り出せない。何か道具（じょうろ等）が必要だ...");
        }
    }
}