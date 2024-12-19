using UnityEngine;

public class RoadScript : MonoBehaviour
{
    public string targetTag = "car";       // 探すオブジェクトのタグ
    public float detectionRange = 100.0f;       // 範囲の設定
    // 道オブジェクトの種類を入れる引数で、これらの中からランダムで道が出現する
    public GameObject[] carObjectArray;

    private bool hasSpawned = false;          // 一度のみ配置するためのフラグ

    void Update()
    {
        // すでにオブジェクトを配置している場合は処理を終了
        if (hasSpawned)
        {
            return;
        }

        // 指定した範囲内のコライダーを取得
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider collider in hitColliders)
        {
            // タグで対象オブジェクトを確認
            if (collider.CompareTag(targetTag))
            {
                // 配列の中からランダムにインデックスを選択
                int randomIndex = Random.Range(0, carObjectArray.Length);

                GameObject objectToSpawn = carObjectArray[randomIndex];
                // 自身の20m前方にオブジェクトを配置
                Vector3 spawnPosition = transform.position + transform.forward * 20.0f;
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

                // フラグを更新して処理を終了
                hasSpawned = true;
                break;
            }
        }
    }
}
