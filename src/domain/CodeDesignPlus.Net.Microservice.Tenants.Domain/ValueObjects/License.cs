using System.Text.Json.Serialization;

namespace CodeDesignPlus.Net.Microservice.Tenants.Domain.ValueObjects;

public sealed partial class License
{
    [GeneratedRegex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]{1,128}$")]
    private static partial Regex Regex();

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Instant StartDate { get; private set; }
    public Instant EndDate { get; private set; }
    public Dictionary<string, string> Metadata { get; private set; } 

    [JsonConstructor]
    public License(Guid Id, string Name, Instant StartDate, Instant EndDate, Dictionary<string, string> Metadata)
    {        
        DomainGuard.GuidIsEmpty(Id, Errors.LicenseIdIsEmpty);
        DomainGuard.IsNullOrEmpty(Name, Errors.LicenseNameIsEmpty);
        DomainGuard.IsGreaterThan(StartDate, EndDate, Errors.LicenseStartDateGreaterThanEndDate);
        DomainGuard.IsNull(Metadata, Errors.LicenseMetadataIsNull);

        DomainGuard.IsFalse(Regex().IsMatch(Name), Errors.LicenseNameIsInvalid);

        this.Id = Id;
        this.Name = Name;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
        this.Metadata = Metadata;
    }

    public static License Create(Guid Id, string Name, Instant StartDate, Instant EndDate, Dictionary<string, string> Metadata)
    {
        return new License(Id, Name, StartDate, EndDate, Metadata);
    }
}
