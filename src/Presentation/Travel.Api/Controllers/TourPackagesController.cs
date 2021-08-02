using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Application.TourPackages.Commands.CreateTourPackage;
using Travel.Application.TourPackages.Commands.DeleteTourPackage;
using Travel.Application.TourPackages.Commands.UpdateTourPackage;
using Travel.Application.TourPackages.Commands.UpdateTourPackageDetail;
using Travel.Data.Contexts;
using Travel.Domain.Entities;

namespace Travel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourPackagesController : ApiController
    {

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTourPackageCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTourPackageCommand { Id = id });
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id , UpdateTourPackageCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);
            return NoContent();

        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateItemDetails(int id, UpdateTourPackageDetailCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
