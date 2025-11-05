using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum battleState { START, PLAYERTURN, ENEMYTURN, LOST, WON, ESCAPED }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    //add these later
    //public GameObject enemyPrefab2;
    //public GameObject enemyPrefab3;

    public Transform PlayerBattleStation;
    public Transform EnemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public Text Dialogue;

    public BattleHud playerHud;
    public BattleHud enemyHud;

    public battleState state;

    private EnemyController enemyController;

    void Start()
    {
        state = battleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, PlayerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, EnemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        

        // Get the enemy controller from the enemy prefab
        enemyController = enemyGO.GetComponent<EnemyController>();
        Debug.Log("EnemyController found: " + (enemyController != null));

        Dialogue.text = "A Wild " + enemyUnit.unitName + " Approaches...";

        playerHud.SetHud(playerUnit);
        enemyHud.SetHud(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = battleState.PLAYERTURN;
        PlayerTurn();
    }


    public void PlayerTurn()
    {
        Dialogue.text = "Choose an Action:";
    }

    public void AttackButton()
    {
        if (state != battleState.PLAYERTURN) return;

        state = battleState.ENEMYTURN;
        StartCoroutine(PlayerAttack());
    }

    public void RunButton()
    {
        if (state != battleState.PLAYERTURN) return;
        state = battleState.ESCAPED;
        StartCoroutine(RunSequence());
    }

    public void HealButton()
    {
        if(state  != battleState.PLAYERTURN) return;

        state = battleState.ENEMYTURN;
        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);
        playerHud.SetHP(playerUnit.currentHealth);
        Dialogue.text = "You gained determination!";

        yield return new WaitForSeconds(2f);
        state = battleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator RunSequence()
    {
        Dialogue.text = "You Choosen To Escape The Battle....";

        yield return new WaitForSeconds(2f);

        Dialogue.text = "No Rewards....";
    }

    IEnumerator PlayerAttack()
    {
        Dialogue.text = playerUnit.unitName + " attacked!";

        if (enemyController != null)
        {
            enemyController.EnemyHit(true);
        }
        else
        {
            Debug.LogWarning("EnemyController is NULL! Cannot play hit animation.");
        }

            bool isDead = enemyUnit.TakeDamage(playerUnit.dmg);
        enemyHud.SetHP(enemyUnit.currentHealth);
        

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            StartCoroutine(PlayerWon());
            yield break; 
        }
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        Dialogue.text = enemyUnit.unitName + " attacked!";
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.dmg);
        playerHud.SetHP(playerUnit.currentHealth);

        if (isDead)
        {
            state = battleState.LOST;
            Dialogue.text = "You have lost the battle!";
            yield break;
        }

        yield return new WaitForSeconds(2f);

        state = battleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerWon()
    {
        state = battleState.WON;
        Dialogue.text = "You Have Won The Battle!";
        yield return new WaitForSeconds(2f);
        Dialogue.text = "You Got 5 Gold...";
    }


}
