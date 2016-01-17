namespace Employees.MVP
{
    public abstract class BasePresenter<T> : IPresenter<T> where T : IView
    {
        public T View { get; set; }
    }
}