using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solution.Core.Features.SkillCQ.Commands;
using Solution.Core.Features.SkillCQ.Queries;
using Solution.Core.Features.SkillCQ.ViewModels;

namespace Employee_CleanPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSkillList()
        {
            return Ok(await _mediator.Send(new GetSkillListQuery()));
        }

        [HttpGet("[action]/{skill_Id}")]
        public async Task<IActionResult> GetSkillById(int skill_Id)
        {
            var dbSkill = await _mediator.Send(new GetSkillByIdQuery() { skill_Id = skill_Id });
            if (dbSkill == null)
            {
                return NotFound($"Skill Not Found With Skill_Id : {skill_Id}");
            }
            return Ok(dbSkill);
        }

        [HttpGet("[action]/{employee_Id}")]
        public async Task<IActionResult> GetSkillsByEmployee(int employee_Id)
        {
            return Ok(await _mediator.Send(new GetSkillByEmployeeIdQuery() { Employee_Id = employee_Id }));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostSkill(AddSkillVM addSkillVM)
        {
            var res = await _mediator.Send(new CreateSkillCommand(addSkillVM));
            if (res == -1)
            {
                return BadRequest("Invalid Employee_Id!!!");
            }
            else if (res == 0)
            {
                return BadRequest("Please Enter Valid Skill Details!!!");
            }
            return Ok(res);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> PutSkill(UpdateSkillVM updateSkillVM)
        {
            var res = await _mediator.Send(new UpdateSkillCommand(updateSkillVM));
            if (res == -1)
            {
                return NotFound($"Skill Not Found With Skill_Id : {updateSkillVM.Skill_Id}");
            }
            else if (res == 0)
            {
                return BadRequest("Update Failed!!! Please Enter Valid Details");
            }
            return Ok(res);
        }

        [HttpDelete("[action]/{skill_Id}")]
        public async Task<IActionResult> DeleteSkill(int skill_Id)
        {
            var res = await _mediator.Send(new DeleteSkillCommand() { skill_Id = skill_Id });
            if (res == -1)
            {
                return NotFound($"Skill Not Found With Skill_Id : {skill_Id}");
            }
            else if (res == 0)
            {
                return BadRequest("Delete Not Possible!!!");
            }
            return Ok(res);
        }
    }
}