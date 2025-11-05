using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Text hpText;

    public void SetHud(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpText.text = unit.currentHealth.ToString();
    }

    public void SetHP(int currentHp)
    {
        hpText.text = currentHp.ToString();
    }


}
