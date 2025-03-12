using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button dropButton;

        public void Awake()
        {
            dropButton.gameObject.SetActive(false);
        }

        public void ShowDropButton(bool show)
        {
            dropButton.gameObject.SetActive(show);
        }
    }
}