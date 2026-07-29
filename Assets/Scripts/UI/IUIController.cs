namespace UI
{
    public interface IUIController
    {
        void ShowPage<PageType>() where PageType : UIBaseItem;
    }
}