using UnityEngine;
// 新しいInput Systemを使うために必要です
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    // プレイヤーが近くにいるかどうかの判定フラグ
    private bool isPlayerNearby = false;

    // トリガー（コライダー）に何かが入ったとき
    private void OnTriggerEnter(Collider other)
    {
        // 入ってきたオブジェクトのタグが "Player" だった場合
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("アイテムに近づいた！（スペースキーで拾う）");
        }
    }

    // トリガーから何かが離れたとき
    private void OnTriggerExit(Collider other)
    {
        // 離れたオブジェクトのタグが "Player" だった場合
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("アイテムから離れた");
        }
    }

    void Update()
    {
        // プレイヤーが近くにいて、かつスペースキーが今押された瞬間なら
        if (isPlayerNearby && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Pickup();
        }
    }

    // 拾ったときの処理
    void Pickup()
    {
        Debug.Log(gameObject.name + " を拾いました！");

        // ここにインベントリ（所持品リスト）に追加する処理などを後から書けます

        // オブジェクトをゲーム画面から消去する
        Destroy(gameObject);
    }
}