using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMelee : IAttackable
{
    bool CanAttack();
}