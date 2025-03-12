using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button dropButton;
    
        public void ShowDropButton(bool show)
        {
            dropButton.gameObject.SetActive(show);
        }
    }
}