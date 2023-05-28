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

        CustomerResponseDTO? result;
        CustomerResponseDTO customerDTO = new CustomerResponseDTO();
        var customer = Data.Instance.Customers.FirstOrDefault(c => c.Id == id);
        if(customer != null) {
            result = customerDTO.toDTO(customer);
        } else {
            result = null;
        }

        if(result != null) return Ok(result);

        return NotFound();
    }

    [HttpGet("cpf/{cpf}")]
    public ActionResult<CustomerResponseDTO> GetCustomerByCpf (string cpf) 
    {
        Console.WriteLine($"cpf: {cpf}");
        CustomerResponseDTO? result;
        CustomerResponseDTO customerDTO = new CustomerResponseDTO();
        var customer = Data.Instance.Customers.FirstOrDefault(c => c.Cpf == cpf);
        if(customer != null) {
            result = customerDTO.toDTO(customer);
        } else {
            result = null;
        }

        if(result != null) return Ok(result);

        return NotFound();
    }

    [HttpPost]
    public ActionResult<Customer> CreateCustomer (CustomerDTO customer) 
    {
        var newCustomer = customer.toObject();
        newCustomer.Id = Data.Instance.Customers.Max(c => c.Id)+1;

        Data.Instance.Customers.Add(newCustomer);
        return CreatedAtRoute
        (
            "GetCustomerById",
            new {id = newCustomer.Id },
            newCustomer
        );
    }
}