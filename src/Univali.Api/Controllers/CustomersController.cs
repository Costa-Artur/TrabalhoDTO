using Microsoft.AspNetCore.Mvc;
using Univali.Api.Entities;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Customer>> GetCustomers()
    {
        var result = Data.Instance.Customers;
        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetCustomerById")]
    public ActionResult<CustomerResponseDTO> GetCustomerById (int id) 
    {
        Console.WriteLine($"id: {id}");

        var customer = Data.Instance.Customers.FirstOrDefault(c => c.Id == id);

       var responseDTO = new CustomerResponseDTO(customer.Name);

        var result = responseDTO; 

        if(result != null) return Ok(result);

        return NotFound();
    }

    [HttpGet("cpf/{cpf}")]
    public ActionResult<CustomerResponseDTO> GetCustomerByCpf (string cpf) 
    {
        Console.WriteLine($"cpf: {cpf}");
        var customer = Data.Instance.Customers.FirstOrDefault(c => c.Cpf == cpf);

        var responseDTO = new CustomerResponseDTO(customer.Name);

        var result = responseDTO; 

        if(result != null) return Ok(result);

        return NotFound();
    }

    [HttpPost]
    public ActionResult<Customer> CreateCustomer (CustomerDTO customer) 
    {
        var newCustomer = new Customer 
        {
            Id = Data.Instance.Customers.Max(c => c.Id)+1,
            Name = customer.Name,
            Cpf = customer.Cpf
        };

        Data.Instance.Customers.Add(newCustomer);
        return CreatedAtRoute
        (
            "GetCustomerById",
            new {id = newCustomer.Id },
            newCustomer
        );
    }
}