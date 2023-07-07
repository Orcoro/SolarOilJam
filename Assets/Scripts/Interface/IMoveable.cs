using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void Init(Statistic statistic);
    void Move(Vector3 direction);
}