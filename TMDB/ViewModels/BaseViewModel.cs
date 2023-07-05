namespace TMDB.ViewModels
{
    public class BaseViewModel
    {
        private bool isInitialized;

        public BaseViewModel() { }

        public virtual void OnPageAppearing()
        {
            if (!isInitialized)
            {
                Initialize();
                isInitialized = true;
            }                
        }

        public virtual void Initialize()
        {

        }
    }
}
