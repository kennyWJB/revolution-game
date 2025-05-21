using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : MonoBehaviour
{
    public Transform centerPoint; // 旋转中心
    public float speed = 60f;     // 角速度

    void Update()
    {
        // 围绕centerPoint的Z轴旋转
        transform.RotateAround(centerPoint.position, Vector3.forward, speed * Time.deltaTime);
    }
}
