using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PieProgressBarController : MonoBehaviour
{
    public Image pieImage;
    private float currentFill = 0f;
    private float maxFill = 100f;

    private Data data;

    public TMP_Text moneyText;
    public TMP_Text redMultiplierText;

    public Button redLevelUpButton;




    void Awake()
    {
        data = new Data();
        moneyText.text = data.money.ToString("F2");
        redMultiplierText.text = data.red.multiplier.ToString("F2");
        redLevelUpButton.GetComponentInChildren<TMP_Text>().text = data.red.speed.ToString() + "[+0.13]\n"+data.red.costToLevelUp.ToString("F2");
    }

    void Start()
    {
        UpdateProgress(0);
    }


    public void UpdateProgress(float amount)
    {
        currentFill = Mathf.Clamp(amount, 0f, maxFill);
        pieImage.fillAmount = currentFill / maxFill;
    }


    void Update()
    {
        if (currentFill < maxFill)
        {
            UpdateProgress(currentFill + Time.deltaTime * data.red.speed * maxFill); // 每秒增加 20 单位
        }
        else if (currentFill == maxFill)
        {
            data.money += 1 * data.red.multiplier;
            data.red.increaseMultiplier(0.1f);
            redMultiplierText.text = data.red.multiplier.ToString("F2");
              moneyText.text = data.money.ToString("F2");
            UpdateProgress(0);
        }
    }

    public void redIncreaseOnClick()
    {
        if (data.money > data.red.costToLevelUp)
        {
            data.red.increaseSpeed(0.13f);
            data.red.increaseCostToLevelUp(1.99f);
            data.money -= data.red.costToLevelUp;
            moneyText.text = data.money.ToString("F2");
            redLevelUpButton.GetComponentInChildren<TMP_Text>().text = data.red.speed.ToString() + "[+0.13]\n" + data.red.costToLevelUp.ToString("F2");
        }
    }
}