using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System;
using Project.Common.Patterns;
using Project.Core.UiNavigationSystem.TemplateViews;

namespace Project.Core.UiNavigationSystem
{
    public class ViewManager : StateMachine<View>
    {
        [SerializeField]
        private View homeViewInCurrentScene;

        [EditorButton(nameof(UpdateViewsInCurrentSceneReferences), "Update Views References", activityType: ButtonActivityType.OnEditMode)]
        [SerializeField]
        private List<View> viewsInCurrentScene;
        
        protected override View InitialState => homeViewInCurrentScene;
        protected override List<View> States => viewsInCurrentScene;

        private void OnValidate()
        {
            Assert.IsNotNull(viewsInCurrentScene);
            Assert.IsTrue(viewsInCurrentScene.Any());
        }

        protected override void Start()
        {
            base.Start();
            viewsInCurrentScene.ForEach(view => view.OnSwitchedToViewOfType += SwitchToViewOfType);
            viewsInCurrentScene.ForEach(view => view.OnSwitchedToHomeView += SwitchToHomeViewCurrentScene);
        }

        private void OnDestroy()
        {
            viewsInCurrentScene.ForEach(view => view.OnSwitchedToViewOfType -= SwitchToViewOfType);
            viewsInCurrentScene.ForEach(view => view.OnSwitchedToHomeView -= SwitchToHomeViewCurrentScene);
        }

        public void SwitchToViewOfType(Type type) => SwitchState(v => v.GetType() == type);
        public void SwitchToHomeViewCurrentScene() => SwitchToInitialState();

        private void UpdateViewsInCurrentSceneReferences() => viewsInCurrentScene = GetReferencesToAllViews().ToList();
        private IEnumerable<View> GetReferencesToAllViews() => FindObjectsOfType<View>(true);
    }
}