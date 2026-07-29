using UI;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour, IUIController
    {
        [SerializeField] private UIBaseItem[] _pages;

        public void ShowPage<PageType>()  where PageType : UIBaseItem
        {
            foreach (var page in _pages)
            {
                if (page is PageType)
                {
                    page.Show();
                }
                else
                {
                    page.Hide();
                }
            }
        }
    }
}