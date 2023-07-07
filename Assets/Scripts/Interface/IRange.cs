using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRange : IAttackable
{
    void Attack(bool fire, bool reload, Vector3 direction);
    bool CanReload();
}