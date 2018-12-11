namespace Algoritmiek.Containervervoer
{
    public interface IContainer
    {
        Freight Freight { get; }
        double Weight { get; }
    }
}
