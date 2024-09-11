public interface IUpgrade 
{
    int Level { get; }
    int GetPrice();
    void ApplyUpgrade();
}
