using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tenetApi.Context;
using tenetApi.Model;
using tenetApi.ViewModel;

namespace tenetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IEnumerable<CustomerViewModel> _customerViewModel;
        public CustomerController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("CustomerByID")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomerByID(long CustomerID)
        {
            _customerViewModel = _context.customers.Select(c => new CustomerViewModel()
            {
                CellPhone = c.CellPhone,
                CustomerFirstName = c.CustomerFirstName.Replace("_"," "),
                CustomerID = c.CustomerID,
                CustomerLastName = c.CustomerLastName.Replace("_", " "),
                Email = c.Email,
                Telephone = c.Telephone,
                UserID = c.UserID
            }).ToList().Where(c => c.CustomerID == CustomerID);

            if (_customerViewModel == null)
            {
                return NotFound();
            }

            return _customerViewModel.ToList();

        }
        [HttpGet]
        [Route("CustomerByName")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomerByName(string CustomerName)
        {
            _customerViewModel = _context.customers.Select(c => new CustomerViewModel()
            {
                CellPhone = c.CellPhone,
                CustomerFirstName = c.CustomerFirstName,
                CustomerID = c.CustomerID,
                CustomerLastName = c.CustomerLastName,
                Email = c.Email,
                Telephone = c.Telephone,
                UserID = c.UserID
            }).ToList().Where(c => (c.CustomerFirstName + " " + c.CustomerLastName).Contains(CustomerName.ToLower()));//search through firstname and lastname together!

            if (_customerViewModel == null)
            {
                return NotFound();
            }
            return _customerViewModel.ToList();
        }

        [HttpGet]
        [Route("CustomerByEmail")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomerByEmail(string CustomerEmail)
        {
            _customerViewModel = _context.customers.Select(c => new CustomerViewModel()
            {
                CellPhone = c.CellPhone,
                CustomerFirstName = c.CustomerFirstName,
                CustomerID = c.CustomerID,
                CustomerLastName = c.CustomerLastName,
                Email = c.Email,
                Telephone = c.Telephone,
                UserID = c.UserID
            }).ToList().Where(c => c.Email.Contains(CustomerEmail.ToLower()));

            if (_customerViewModel == null)
            {
                return NotFound();
            }
            return _customerViewModel.ToList();
        }

        [HttpGet]
        [Route("CustomerByTelephone")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomerByTelephone(string CustomerTelephone)
        {
            if (CustomerTelephone.StartsWith("0"))//telephone is "long" and does
            {
                CustomerTelephone = CustomerTelephone.Remove(0, 1);
            }
            _customerViewModel = _context.customers.Select(c => new CustomerViewModel()
            {
                CellPhone = c.CellPhone,
                CustomerFirstName = c.CustomerFirstName,
                CustomerID = c.CustomerID,
                CustomerLastName = c.CustomerLastName,
                Email = c.Email,
                Telephone = c.Telephone,
                UserID = c.UserID
            }).ToList().Where(c => c.Telephone.ToString().Contains(CustomerTelephone));

            if (_customerViewModel == null)
            {
                return NotFound();
            }
            return _customerViewModel.ToList();
        }

        [HttpGet]
        [Route("CustomerByCellphone")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomerByCellphone(string CustomerCellphone)
        {
            if (CustomerCellphone.StartsWith("0"))
            {
                CustomerCellphone = CustomerCellphone.Remove(0, 1);
            }
            _customerViewModel = _context.customers.Select(c => new CustomerViewModel()
            {
                CellPhone = c.CellPhone,
                CustomerFirstName = c.CustomerFirstName,
                CustomerID = c.CustomerID,
                CustomerLastName = c.CustomerLastName,
                Email = c.Email,
                Telephone = c.Telephone,
                UserID = c.UserID
            }).ToList().Where(c => c.CellPhone.ToString().Contains(CustomerCellphone));

            if (_customerViewModel == null)
            {
                return NotFound();
            }
            return _customerViewModel.ToList();
        }
        
        [HttpPost]
        [Route("CustomerAdd")]
        public async Task<ActionResult<CustomerViewModel>> AddCustomer([FromBody] CustomerViewModel customer)
        {
            if (customer.CustomerFirstName.Contains(" ") || customer.CustomerLastName.Contains(" "))
            {
                customer.CustomerFirstName = customer.CustomerFirstName.Replace(" ", "_").ToLower();
                customer.CustomerLastName = customer.CustomerLastName.Replace(" ", "_").ToLower();
            }
            if (_context.customers.Any(c => (c.CustomerFirstName + " " + c.CustomerLastName) == (customer.CustomerFirstName + " " + customer.CustomerLastName)))
            {
                return BadRequest();
            }
            var userId = _context.Users.FirstOrDefault(c => c.Id == customer.UserID);

            if (userId == null)
            {
                return BadRequest("Invalid user!");
            }

            Customer theCustomer = new Customer();
            theCustomer.CellPhone = customer.CellPhone;
            theCustomer.CustomerFirstName = customer.CustomerFirstName;
            theCustomer.CustomerLastName = customer.CustomerLastName;
            theCustomer.Email = customer.Email;
            theCustomer.Telephone = customer.Telephone;
            theCustomer.UserID = customer.UserID;
            theCustomer.CreatedDate = DateTime.Now;

            _context.customers.Add(theCustomer);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpPut]
        [Route("CustomerUpdate")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerViewModel customer)
        {
            if (customer.CustomerFirstName.Contains(" ") || customer.CustomerLastName.Contains(" "))
            {
                customer.CustomerFirstName = customer.CustomerFirstName.Replace(" ", "_").ToLower();
                customer.CustomerLastName = customer.CustomerLastName.Replace(" ", "_").ToLower();
            }
            if (!_context.customers.Any(c => c.CustomerID == customer.CustomerID))
            {
                return NotFound();
            }

            Customer theCustomer = _context.customers.FirstOrDefault(c=> c.CustomerID == customer.CustomerID);
            theCustomer.CellPhone = customer.CellPhone;
            theCustomer.CustomerFirstName = customer.CustomerFirstName;
            theCustomer.CustomerID = customer.CustomerID;
            theCustomer.CustomerLastName = customer.CustomerLastName;
            theCustomer.Email = customer.Email;
            theCustomer.Telephone = customer.Telephone;

            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpDelete]
        [Route("CustomerDelete")]
        public async Task<IActionResult> DeleteCustomer(long customerID)
        {
            if (!_context.customers.Any(c => c.CustomerID == customerID))
            {
                return NotFound();
            }

            Customer theCustomer = _context.customers.FirstOrDefault(c => c.CustomerID == customerID);
            theCustomer.IsDeleted = true;

            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpPost]
        [Route("CustomerDeleteUndo")]
        public async Task<IActionResult> DeleteCustomerUndo(long customerID)
        {
            if (!_context.customers.Any(c => c.CustomerID == customerID))
            {
                return NotFound();
            }

            Customer theCustomer = _context.customers.FirstOrDefault(c => c.CustomerID == customerID);
            theCustomer.IsDeleted = false;

            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpPost]
        [Route("CustomerActivate")]
        public async Task<IActionResult> ActivateCustomer(long customerID)
        {
            if (!_context.customers.Any(c => c.CustomerID == customerID))
            {
                return NotFound();
            }

            Customer theCustomer = _context.customers.FirstOrDefault(c => c.CustomerID == customerID);
            theCustomer.IsActive = true;

            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpDelete]
        [Route("CustomerInactivate")]
        public async Task<IActionResult> InactivateCustomer(long customerID)
        {
            if (!_context.customers.Any(c => c.CustomerID == customerID))
            {
                return NotFound();
            }

            Customer theCustomer = _context.customers.FirstOrDefault(c => c.CustomerID == customerID);
            theCustomer.IsDeleted = false;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
