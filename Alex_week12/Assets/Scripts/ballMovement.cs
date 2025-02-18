using UnityEngine;

public class ballMovement : MonoBehaviour
{
    float moveSpeed;
    public float moveSpeedMin, moveSpeedMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(this.transform.right * moveSpeed * Time.deltaTime);
    }
}
