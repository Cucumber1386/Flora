using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3 : Monster
{
    [SerializeField] float Speed;
    [SerializeField] MonsterAttack CollideAttack;
    protected override IEnumerator OnActionIEnum()
    {
        if (Mathf.Abs(transform.position.x - PlayerDataSO.PlayerTrans.position.x) > 5)
            yield return StartCoroutine(MoveIEnum());
        else
            yield return StartCoroutine(OnCollideAttackIEnum());
    }
    private IEnumerator MoveIEnum()//? 持續往玩家移動
    {
        if (PlayerDataSO.PlayerTrans.position.x > transform.position.x)
            transform.rotation = Quaternion.identity;
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
        Anima.SetInteger("Move", 1);
        while (Mathf.Abs(transform.position.x - PlayerDataSO.PlayerTrans.position.x) > 5)//? 接近玩家後使出衝撞攻擊
        {
            transform.Translate(Speed * Time.deltaTime, 0, 0);
            yield return 0;
        }
        yield return StartCoroutine(OnCollideAttackIEnum());
    }
    private IEnumerator OnCollideAttackIEnum()//? 衝撞攻擊
    {
        if (PlayerDataSO.PlayerTrans.position.x > transform.position.x)
            transform.rotation = Quaternion.identity;
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
        CollideAttack.UseAttack();
        Rigid.AddRelativeForce(transform.right * 1000);
        Rigid.AddRelativeForce(Vector2.up * 500);
        Anima.SetTrigger("Attack");
        yield return new WaitForSeconds(2);
        OnAction();
        yield break;
    }
    private void Start()
    {
        HpObject.SetActive(true);
    }
}