namespace Shared.Models;

public class PackageBox
{
    public long Id { get; set; }
    
    public double Weight { get; set; }
    
    public string Name { get; set; }
    
    public string ReferenceNumber { get; set; }

    public PackageBox(long id, double weight, string name, string referenceNumber)
    {
        Id = id;
        Weight = weight;
        Name = name;
        ReferenceNumber = referenceNumber;
    }
}