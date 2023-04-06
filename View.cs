using Project.Common.Patterns;
using System;
using UnityEngine;

namespace Project.Core.UiNavigationSystem.TemplateViews
{
    public abstract class View : MonoBehaviour, IState
    {
        [SerializeField]
        protected bool shouldBeVisibleOnStart;

        public bool IsActive => gameObject.activeSelf;

        public event Action<Type> OnSwitchedToViewOfType;
        public event Action OnSwitchedToHomeView;

        protected virtual void Start() => SetVisibility(shouldBeVisibleOnStart);

        public virtual void Show()
        {
            gameObject.SetActive(true);
            AddButtonListeners();
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            RemoveButtonListeners();
        }

        public void EnableState() => Show();
        public void DisableState() => Hide();

        protected void InvokeOnSwitchedToViewOfType(Type type) => OnSwitchedToViewOfType.Invoke(type);
        protected void InvokeOnSwitchedToHomeView() => OnSwitchedToHomeView.Invoke();

        protected void SetVisibility(bool shouldBeVisible)
        {
            if (shouldBeVisible)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        protected abstract void AddButtonListeners();
        protected abstract void RemoveButtonListeners();
    }
}