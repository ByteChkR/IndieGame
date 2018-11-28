using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IController
{
    void LockControls(bool locked);
    Vector3 VTarget { get; }
    Rigidbody Rb { get; }
    bool IsPlayer { get; }
}
