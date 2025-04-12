
/// <summary>
/// Presenter class.
/// </summary>
public abstract class PresenterBase<V, M> where V : class where M : class
{
    /// <summary>
    /// Gets the associated View.
    /// </summary>
    protected V View { get; private set; }
    
    /// <summary>
    /// Gets the associated Model.
    /// </summary>
    protected M Model { get; private set; }

    /// <summary>
    /// Initializes a new instance of the Presenter class.
    /// </summary>
    /// <param name="view">The view associated with this presenter.</param>
    /// <param name="model">The model associated with this presenter.</param>
    protected PresenterBase(V view, M model)
    {
        View = view;
        Model = model;
    }
}
