using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class BallBase : MonoBehaviour
{
    // 基础属性
    public int upgradeLevel = 0; // 升级等级
    public int enhanceLevel = 0; // 强化等级
    public float baseUpgradeCost = 2f; // 初始升级花费
    public float upgradeCostRatio = 1.3f; // 升级花费公比
    public float baseSpeed = 0.5f; // 初始角速度（圈/秒）
    public float speedDelta = 0.2f; // 每级角速度提升
    public float baseScoreDelta = 0.1f; // 初始得分因子公差
    public float scoreFactor = 1f; // 当前得分因子

    public float angle = 0f; // 当前角度
    public float radius = 2f; // 旋转半径

    // 计算当前角速度
    public float GetSpeed()
    {
        return baseSpeed + upgradeLevel * speedDelta;
    }

    // 计算当前升级花费
    public float GetUpgradeCost()
    {
        return baseUpgradeCost * Mathf.Pow(upgradeCostRatio, 100 * enhanceLevel + upgradeLevel - 1);
    }

    // 计算当前得分因子公差
    public float GetScoreDelta()
    {
        return baseScoreDelta * Mathf.Pow(10, enhanceLevel);
    }

    // 升级
    public virtual void Upgrade()
    {
        if (upgradeLevel < 100)
        {
            upgradeLevel++;
        }
        else
        {
            Enhance();
        }
    }

    // 强化
    public virtual void Enhance()
    {
        enhanceLevel++;
        upgradeLevel = 1;
    }

    // 旋转并判断是否转一圈
    public bool RotateAndCheckCircle(float deltaTime)
    {
        float speed = GetSpeed() * 360f; // 1圈=360度
        angle += speed * deltaTime;
        if (angle >= 360f)
        {
            angle -= 360f;
            scoreFactor += GetScoreDelta();
            return true;
        }
        return false;
    }

    // 设置小球位置
    public void UpdatePosition(Vector3 center)
    {
        float rad = angle * Mathf.Deg2Rad;
        transform.position = center + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
    }
}
