namespace Pictura.Vita.Presentation.Object;

public abstract record Element
{
    public Element()
    {
        Children = new List<Element>();
    }

    public bool IsRoot { get; init; }

    public bool HasChildren() => Children.Any();

    public IList<Element> Children { get; }

    public abstract string RenderOpen();

    public abstract string RenderClose();

    public void AddChild(Element child)
    {
        Children.Add(child);
    }
}
