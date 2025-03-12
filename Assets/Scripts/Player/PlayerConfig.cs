using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Settings/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Range(0.5f, 10f)] public float MoveSpeed = 3f;
    }
}