namespace Univali.Api.Entities;

public class CustomerResponseDTO {

    
    public string Name{get; set;} = string.Empty;

    public CustomerResponseDTO (string Name) {    
        this.Name = Name;
    }

}