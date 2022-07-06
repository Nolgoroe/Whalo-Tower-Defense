using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [SerializeField]
    private int _hp = 200;

    [SerializeField]
    private ParticleSystem coreHit;

    public bool IsAlive => _hp > 0;

    public int Hp => _hp;

    public void TakeDamage(int amount)
    {
        if (!IsAlive)
        {
            return;
        }

        coreHit.Play();

        _hp = Mathf.Max(0, _hp - amount);

        if (!IsAlive)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        ClassRefrencer.instance.UIManager.DisplayEndGameScreen();
    }
}
