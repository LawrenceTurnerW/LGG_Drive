using UnityEngine;
using System.Collections;

public class obstacleCarSpawnController : MonoBehaviour
{
    public string targetTag = "car";       // 探すオブジェクトのタグ
    public float distance = 90.0f; // 基準オブジェクトの前方への距離

    // 車の種類を入れる引数で、これらの中からランダムで車が出現する
    public GameObject[] carObjectArray;

    private int randomValue;

    // 車の出現パターンの配列
    int[,] array2D = new int[,] {
        { 0, 0, 1},
        { 0, 1, 0},
        { 1, 0, 0},
        { 1, 0, 1}
    };

    void Start()
    {
        // コルーチンを開始して、定期的にランダムな生成パターンを実行する
        StartCoroutine(GenerateRandomCarSpawn());
    }

    private IEnumerator GenerateRandomCarSpawn()
    {
        while (true)
        {
            // 0から4の範囲でランダムな整数値を生成
            randomValue = Random.Range(0, 4);

            // 3回繰り返す
            for (int i = 0; i < 3; i++)
            {
                CarSpawn(randomValue, i);
            }

            // 3秒待機
            yield return new WaitForSeconds(3f);
        }
    }

    void CarSpawn(int randomValue, int index)
    {
        if (array2D[randomValue, index] != 0)
        {
            // タグに一致するオブジェクトを検索
            GameObject referenceObject = GameObject.FindGameObjectWithTag(targetTag);
            // 基準オブジェクトの前方を計算
            Vector3 spawnPosition = referenceObject.transform.position + referenceObject.transform.forward * distance;
            // 特定のZ座標を上書き
            spawnPosition.x = (index - 1) * 3.7f;

            // 配列の中からランダムにインデックスを選択
            int randomIndex = Random.Range(0, carObjectArray.Length);

            // オブジェクトを生成
            Instantiate(carObjectArray[randomIndex], spawnPosition, referenceObject.transform.rotation);
        }
    }
}