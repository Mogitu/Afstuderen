/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Standard/demo view
/// </summary>
namespace AMC.GUI
{
    public class StandardView : View
    {
        private StandardPresenter StandardPresenter;

        void Awake()
        {
            StandardPresenter = GetPresenterType<StandardPresenter>();
        }
    }
}
