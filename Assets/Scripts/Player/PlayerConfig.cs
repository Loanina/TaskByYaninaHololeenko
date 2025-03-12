using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Settings/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Range(0.5f, 100f)] public float MoveSpeed = 3f;
        [Range(0f,50f)] public float RotationSmoothing = 15f;
        [Range(60f, 120f)] public float RotationClampY = 80f;
    }
}