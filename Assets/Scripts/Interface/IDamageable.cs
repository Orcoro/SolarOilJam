using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    bool TakeDamage(int damage, string attacker);
    void Heal(int heal);
}

