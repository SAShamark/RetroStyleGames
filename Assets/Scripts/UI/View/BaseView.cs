using UnityEngine;

namespace UI.View
{
    public class BaseView : MonoBehaviour, IView
    {
        [SerializeField] private Canvas _rootCanvas;

        public virtual void Show()
        {
            _rootCanvas.enabled = true;
        }

        public virtual void Hide()
        {
            _rootCanvas.enabled = false;
        }
    }
}