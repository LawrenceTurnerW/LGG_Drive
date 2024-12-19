using UnityEngine;
using System.Collections;
using LookingGlass;

public class MovingForwardScript : MonoBehaviour
{
    public float moveSpeed = 150f;  // 移動速度
    public float moveSpeed2 = 10f;  // 左右への移動速度
    private int roadLineValue = 0;  // 現在の値
    public float lineReferencePoint = 3.7f;  // 道路の幅として適切なx座標の基準

    [SerializeField] private GameObject car;

    private Rigidbody rb;
    private Vector3 targetPosition; //目標座標を保存する用

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbodyを取得
        targetPosition = transform.position;
        // デバッグ用
        //StartCoroutine(GenerateRandomValue());
    }

    void FixedUpdate()
    {

        if (InputManager.GetButtonDown(HardwareButton.Forward))
        {
            IncrementValue();
        }

        if (InputManager.GetButtonDown(HardwareButton.Back))
        {
            DeccrementValue();
        }

        // オブジェクトの前方への力+X座標への力を考慮した新しい座標を計算する
        float targetX = roadLineValue * lineReferencePoint;
        // targetPosition.xを滑らかに移動させる
        targetPosition.x = Mathf.Lerp(targetPosition.x, targetX, moveSpeed2 * Time.fixedDeltaTime);
        targetPosition.z += moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }

    void IncrementValue()
    {
        if (roadLineValue < 1)
        {
            roadLineValue++;
        }
    }

    void DeccrementValue()
    {
        if (roadLineValue > -1)
        {
            roadLineValue--;
        }
    }

    // コルーチンで3秒ごとにランダムな値を生成
    private IEnumerator GenerateRandomValue()
    {
        while (true)
        {
            // -1から1の範囲でランダムな値を生成
            roadLineValue = Random.Range(-1, 2);
            Debug.Log("Generated Value: " + roadLineValue);

            // 3秒待機
            yield return new WaitForSeconds(3f);
        }
    }
}