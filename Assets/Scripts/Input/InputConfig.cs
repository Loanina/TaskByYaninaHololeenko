using UnityEngine;

namespace Input
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "Settings/InputConfig")]
    public class InputConfig : ScriptableObject
    {
        [Range(50f, 200f)] public float JoystickRadius = 150f;
        [Range(0.1f, 1f)] public float RotationSensitivity = 0.3f;
    }
}