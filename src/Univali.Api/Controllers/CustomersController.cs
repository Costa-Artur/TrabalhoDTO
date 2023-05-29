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
    public ActionResult<CustomerDTO> CreateCustomer (CustomerDTO customer) 
    {
        var newCustomer = customer.toObject();
        newCustomer.Id = Data.Instance.Customers.Any() ? Data.Instance.Customers.Max(c => c.Id)+1 : 1;

        Data.Instance.Customers.Add(newCustomer);
        return CreatedAtRoute
        (
            "GetCustomerById",
            new {id = newCustomer.Id },
            newCustomer
        );
    }

    [HttpPut("{id}")]
    public ActionResult<CustomerDTO> UpdateCustomer (int id, CustomerDTO customer)
    {
        var newCustomer = Data.Instance.Customers.FirstOrDefault(c => c.Id == id);

        if(newCustomer != null) 
        {
            newCustomer.Name = customer.Name;
            newCustomer.Cpf = customer.Cpf;

            return Ok(customer);
        } else 
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCustomer (int id) 
    {
        var customers = Data.Instance.Customers;
        customers.Remove(customers.FirstOrDefault(c => c.Id == id));
        return NoContent();
    }
}