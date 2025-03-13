using System;
using UnityEngine;
using UnityEngine.UI;
using Interaction;
using Zenject;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button dropButton;
        [SerializeField] private Button pickUpButton;
        private InteractionHandler interactionHandler;

        public void Awake()
        {
            dropButton.gameObject.SetActive(false);
            pickUpButton.gameObject.SetActive(false);

            dropButton.onClick.AddListener(OnDropButtonClicked);
            pickUpButton.onClick.AddListener(OnPickUpButtonClicked);
        }

        [Inject]
        public void Construct(InteractionHandler interactionHandler)
        {
            this.interactionHandler = interactionHandler;
            this.interactionHandler.OnLookAtInteractable += ShowPickUpButton;
            this.interactionHandler.OnObjectPickedUp += InteractionHandlerOnOnObjectPickedUp;
            this.interactionHandler.OnObjectDrop += InteractionHandlerOnOnObjectDrop;
        }

        private void InteractionHandlerOnOnObjectDrop()
        {
            ShowDropButton(false);
        }

        private void InteractionHandlerOnOnObjectPickedUp()
        {
            ShowDropButton(true);
            ShowPickUpButton(false);
        }

        private void OnDestroy()
        {
            interactionHandler.OnLookAtInteractable -= ShowPickUpButton;
            interactionHandler.OnObjectPickedUp -= InteractionHandlerOnOnObjectPickedUp;
            interactionHandler.OnObjectDrop -= InteractionHandlerOnOnObjectDrop;
        }

        private void OnDropButtonClicked()
        {
            interactionHandler.DropItem();
        }

        private void OnPickUpButtonClicked()
        {
            interactionHandler.TryPickup();
        }

        private void ShowDropButton(bool show)
        {
            dropButton.gameObject.SetActive(show); 
        }

        private void ShowPickUpButton(bool show)
        {
            pickUpButton.gameObject.SetActive(show);
        }
    }
}