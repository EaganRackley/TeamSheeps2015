[System.Serializable]
public struct ElementProperty
{
    public string ID;
    public bool isServer;
    public bool isVisible;
    public int shapeFrame;
    public ElementProperty(string id, bool visible, int frame, bool server)
    {
        ID = id;
        isVisible = visible;
        shapeFrame = frame;
        isServer = server;
    }
}