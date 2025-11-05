using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator anim;

    public void EnemyHit(bool enemyWasHit)
    {
        Debug.Log("Enemy hit method called!");
        if (enemyWasHit)
        {
            anim.SetBool("isHit", true);
            Invoke(nameof(ResetHit), 0.5f);
        }
    }

    private void ResetHit()
    {
        anim.SetBool("isHit", false);
    }

}
