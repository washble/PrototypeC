using UnityEngine;

/// <summary>
/// class interface.
/// </summary>
public abstract class ViewBase<P, M> : MonoBehaviour where P : class where M : class
{
    /// <summary>
    /// Gets the associated Presenter.
    /// </summary>
    protected P Presenter { get; set; }
    
    /// <summary>
    /// Gets the associated Model.
    /// </summary>
    protected M Model { get; set; }

    /// <summary>
    /// MonoBehaviour's Awake method.
    /// Initializes the View by creating the Presenter and Model.
    /// </summary>
    protected virtual void Awake()
    {
        CreatePresenterAndModel();
    }

    /// <summary>
    /// Method to create the Presenter and Model.
    /// 
    /// Example usage:
    /// 
    /// Model = new Model();
    /// Presenter = new Presenter(this, Model);
    /// 
    /// This method should be implemented by classes that implement IView.
    /// </summary>
    protected abstract void CreatePresenterAndModel();
}
