
// Owen Pallister 1113606

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// A static variable to allow the smooth rotation of objects in the game. Can be used by mulitple instances by using the ToIso function
public static class IRotation 
{

    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
