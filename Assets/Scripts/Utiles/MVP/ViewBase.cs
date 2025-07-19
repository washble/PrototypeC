using UnityEngine;

/// <summary>
/// class interface.
/// </summary>
public abstract class ViewBase<P> : MonoBehaviour where P : class
{
    /// <summary>
    /// Gets the associated Presenter.
    /// </summary>
    protected P Presenter { get; set; }
    
    /// <summary>
    /// MonoBehaviour's Awake method.
    /// Initializes the View by creating the Presenter and Model.
    /// </summary>
    protected virtual void Awake()
    {
        CreatePresenter();
    }

    /// <summary>
    /// Method to create the Presenter and Model.
    /// 
    /// Example usage:
    /// 
    /// Presenter = new Presenter(this);
    /// 
    /// This method should be implemented by classes that implement IView.
    /// </summary>
    protected abstract void CreatePresenter();
}
