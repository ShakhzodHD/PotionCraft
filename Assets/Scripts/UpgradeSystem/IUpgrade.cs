public interface IUpgrade 
{
    int Level { get; }
    int MaxLevel { get; }
    int GetPrice();
    void ApplyUpgrade();
}
