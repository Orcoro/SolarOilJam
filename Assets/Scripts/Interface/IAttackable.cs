using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    void Init(Transform attackPoint, SOWeapon weapon);
    bool CanAttack();
}