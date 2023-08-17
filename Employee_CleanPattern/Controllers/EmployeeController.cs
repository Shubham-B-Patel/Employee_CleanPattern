using Employee_CleanPattern.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solution.Core.Features.EmployeeCQ.Commands;
using Solution.Core.Features.EmployeeCQ.Queries;
using Solution.Core.Features.EmployeeCQ.ViewModels;

namespace Employee_CleanPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _iconfiguration;

        public EmployeeController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _iconfiguration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeeList()
        {
            return Ok(await _mediator.Send(new GetEmployeeListQuery()));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var dbEmployee = await _mediator.Send(new GetEmployeeByIdQuery() { Employee_Id = id });

            if (dbEmployee == null)
            {
                return NotFound("Employee Not Found!!!");
            }

            return Ok(dbEmployee);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostEmployee(AddEmployeeVM addEmployee)
        {
            var res = await _mediator.Send(new CreateEmployeeCommand() { addEmployeeVM = addEmployee });

            if (res == 0)
            {
                return BadRequest("Failed to add new Employee!!!");
            }

            return Ok(res);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginEmployee(LoginEmployeeVM data)
        {
            var res = await _mediator.Send(new LoginEmployeeQuery(data));
            if (res != null)
            {
                CreateJwtToken obj = new CreateJwtToken(_iconfiguration);
                var token = obj.CreateToken(res.Employee_Id, res.Employee_Name);
                return Ok(new
                {
                    message = "Success",
                    token = token,
                    user = res
                }); ;
            }
            return BadRequest("Invalid Credentials!!! Please Enter valid User_Name And Password");
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> PutEmployee(PutEmployeeVM employee)
        {
            var res = await _mediator.Send(new UpdateEmployeeCommand(employee));
            if (res == -1)
            {
                return NotFound($"Employee Not Found With Employee_Id : {employee.Employee_Id}");
            }
            else if(res == 0)
            {
                return BadRequest("Updation Failed!!! Please Enter Valid Details");
            }

            return Ok(res);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var res = await _mediator.Send(new DeleteEmployeeCommand() { Employee_Id = id });
            if (res == 0)
            {
                return BadRequest($"Employee Not Found With Employee_Id : {id}");
            }
            return Ok(res);
        }

        [HttpGet("[action]/{employee_Id}")]
        public async Task<ActionResult<EmployeeDetailsVM>> GetEmployeeDetails(int employee_Id)
        {
            var dbEmployee = await _mediator.Send(new GetEmployeeDetailsQuery() { Employee_Id = employee_Id });
            if (dbEmployee == null)
            {
                return NotFound($"Employee Not Found With Employee_Id : {employee_Id}");
            }
            return Ok(dbEmployee);
        }
    }
}
