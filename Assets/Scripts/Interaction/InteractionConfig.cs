using UnityEngine;

namespace Interaction
{
    [CreateAssetMenu(fileName = "InteractionConfig", menuName = "Settings/InteractionConfig")]
    public class InteractionConfig : ScriptableObject
    {
        [Range(0.5f, 30f)] public float PickupRange = 2f;
        [Range(0.5f, 30f)] public float DropForce = 5f;
    }
}