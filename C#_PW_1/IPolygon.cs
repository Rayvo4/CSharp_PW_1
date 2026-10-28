namespace C__PW_1
{
    public interface IPolygon
    {
        int VertexCount { get; }
        double Area();
        bool IsTriangle();
        bool IsRectangle();
        bool IsSquare();
        bool IsRegular();
    }
}