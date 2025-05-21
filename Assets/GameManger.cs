using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform centerPoint;
    public BallBase[] balls; // 7个小球
    public Button[] upgradeButtons; // 7个升级按钮
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreFactorText;

    private float score = 0f;

    void Start()
    {
        // 初始化小球显示
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].gameObject.SetActive(balls[i].upgradeLevel >= 1);
            int idx = i;
            upgradeButtons[i].onClick.AddListener(() => OnUpgradeButton(idx));
        }
        UpdateUI();
    }

    void Update()
    {
        Vector3 center = centerPoint.position; // 使用自定义Center点
        float totalScoreFactor = 1f;
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i].upgradeLevel >= 1)
            {
                bool finishedCircle = balls[i].RotateAndCheckCircle(Time.deltaTime);
                balls[i].UpdatePosition(center);
                totalScoreFactor *= balls[i].scoreFactor;
                if (finishedCircle)
                {
                    score += GetScorePerCircle();
                    UpdateUI();
                }
            }
        }
    }

    // 计算当前分数（所有可见小球的得分因子乘积）
    float GetScorePerCircle()
    {
        float product = 1f;
        foreach (var ball in balls)
        {
            if (ball.upgradeLevel >= 1)
                product *= ball.scoreFactor;
        }
        return product;
    }

    // 升级按钮点击
    void OnUpgradeButton(int idx)
    {
        var ball = balls[idx];
        if (ball.upgradeLevel < 100)
        {
            float cost = ball.GetUpgradeCost();
            if (score >= cost)
            {
                score -= cost;
                ball.Upgrade();
                if (ball.upgradeLevel == 1)
                    ball.gameObject.SetActive(true);
            }
        }
        else
        {
            // 强化
            ball.Enhance();
        }
        UpdateUI();
    }

    // 更新UI
    void UpdateUI()
    {
        scoreText.text = $"{score:F2}";

        // 得分因子表达式
        string[] factors = new string[balls.Length];
        for (int i = 0; i < balls.Length; i++)
        {
            factors[i] = balls[i].scoreFactor.ToString("F2");
        }
        scoreFactorText.text = string.Join(" ✖️ ", factors);

        for (int i = 0; i < balls.Length; i++)
        {
            var ball = balls[i];
            // 获取按钮下的TextMeshProUGUI组件
            var btnText = upgradeButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (ball.upgradeLevel < 100)
            {
                btnText.text = $"Lv.{ball.upgradeLevel} 角速度+{ball.speedDelta:F2}\n升级({ball.GetUpgradeCost():F2})";
            }
            else
            {
                btnText.text = $"强化\n当前强化Lv.{ball.enhanceLevel}\n得分因子公差x10";
            }
            upgradeButtons[i].interactable = score >= ball.GetUpgradeCost();
        }
    }
}
