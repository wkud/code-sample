using Project.Core.SceneSystem;
using Project.Core.UiNavigationSystem.TemplateViews;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Project.UI.Views
{
    public class MainMenuView : View
    {
        [SerializeField]
        private Button newGameButton;
        [SerializeField]
        private Button loadGameButton;
        [SerializeField]
        private Button settingsButton;
        [SerializeField]
        private Button controlsButton;
        [SerializeField]
        private Button quitGameButton;

        private void OnValidate()
        {
            Assert.IsNotNull(newGameButton);
            Assert.IsNotNull(loadGameButton);
            Assert.IsNotNull(settingsButton);
            Assert.IsNotNull(controlsButton);
            Assert.IsNotNull(quitGameButton);

            Assert.IsTrue(shouldBeVisibleOnStart, $"{nameof(MainMenuView)} should have {nameof(shouldBeVisibleOnStart)} set to True, to display properly on the scene.");
        }

        protected override void AddButtonListeners()
        {
            newGameButton.onClick.AddListener(OnNewGame);
            loadGameButton.onClick.AddListener(OnLoadGameButton);
            settingsButton.onClick.AddListener(OnSettingsButton);
            controlsButton.onClick.AddListener(OnControlsButton);
            quitGameButton.onClick.AddListener(OnQuitGameButton);
        }

        protected override void RemoveButtonListeners()
        {
            newGameButton.onClick.RemoveListener(OnNewGame);
            loadGameButton.onClick.RemoveListener(OnLoadGameButton);
            settingsButton.onClick.RemoveListener(OnSettingsButton);
            controlsButton.onClick.RemoveListener(OnControlsButton);
            quitGameButton.onClick.RemoveListener(OnQuitGameButton);
        }

        private void OnNewGame() => SceneLoadManager.Instance.LoadScene(SceneLoadManager.Instance.StartingWorldScene);

        private void OnLoadGameButton() => InvokeOnSwitchedToViewOfType(typeof(LoadGameView));

        private void OnSettingsButton() => InvokeOnSwitchedToViewOfType(typeof(SettingsView));

        private void OnControlsButton() => InvokeOnSwitchedToViewOfType(typeof(ControlsView));

        private void OnQuitGameButton() => Application.Quit();
    }
}