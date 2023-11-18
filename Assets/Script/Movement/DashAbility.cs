using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float currentMana;
    Player player;
    [HideInInspector] public bool isDashing;
    private Coroutine useManacorotine;
    [SerializeField] GameObject dashTrailEffect;
    public void Awake()
    {
        player = GetComponent<Player>();

    }
    public void Start()
    {

        FillMana(player.playerSO.maxMana);
        StartCoroutine(ManaRegeneration());
    }

    public void CallDash()
    {
        isDashing = true;
        player.moveByVelocity.SetSpeed(player.moveByVelocity.permanentSpeed * 2);
        useManacorotine = StartCoroutine(UseMana(1, isDashing));
        dashTrailEffect.SetActive(true);
    }
    public void StopDash()
    {
        player.moveByVelocity.SetSpeed(player.moveByVelocity.permanentSpeed);
        StopCoroutine(useManacorotine);
        dashTrailEffect.SetActive(false);
        isDashing = false;

    }

    public void FillMana(float value)
    {

        currentMana += value;
        if (currentMana > player.playerSO.maxMana)
        {
            currentMana = player.playerSO.maxMana;
        }
        UpdateUI.Instance.UpdateManaBar(currentMana / player.playerSO.maxMana);

    }
    public IEnumerator UseMana(float manacostPerSec, bool isDashing)
    {
        while (isDashing)
        {
            currentMana -= Time.deltaTime * manacostPerSec; //Giảm 1 mana/ giây
            UpdateUI.Instance.UpdateManaBar(currentMana / player.playerSO.maxMana);
            yield return null;
        }
    }
    public IEnumerator ManaRegeneration()
    {
        while (true)
        {
            // FillMana(1);
            FillManaBySec(0.2f);
            yield return null;
        }
    }
    public void FillManaBySec(float manaFillPerSec)
    {
        if (currentMana < player.playerSO.maxMana)
        {
            currentMana += Time.deltaTime * manaFillPerSec;
            UpdateUI.Instance.UpdateManaBar(currentMana / player.playerSO.maxMana);
        }

    }
}
