public struct ElementProperty
{
    public string ID;
    public bool isVisible;
    public int shapeFrame;
    public ElementProperty(string id, bool visible, int frame)
    {
        ID = id;
        isVisible = visible;
        shapeFrame = frame;
    }
}