using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void Init();
    void Move(Vector3 direction);
}