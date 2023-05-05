namespace Scene
{
    public class SceneHandler : SceneContext
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach (var listViewItem in uiViewList) listViewItem.sceneContext = this;
            foreach (var selector in selectionBases) selector.sceneContext = this;
        }
    }
}
