using System;
using System.Net;
using System.Threading.Tasks;
using JCP.Catalog.API.IntegrationEvents;
using JCP.Catalog.Domain.Model;
using JCP.Catalog.Infrastructure.IntegrationEvents.Interfaces;
using JCP.Catalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogDbContext catalogContext;
        private readonly ICatalogIntegrationEventService catalogIntegrationEventService;

        public CatalogController(CatalogDbContext catalogContext,
                                 ICatalogIntegrationEventService catalogIntegrationEventService)
        {
            this.catalogContext = catalogContext;
            this.catalogIntegrationEventService = catalogIntegrationEventService;

        }
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogItem(Guid id)
        {
            // TODO - Pasar a una capa de Business Logic e inyectar la interfaz en el controlador.
            var catalogItem = await catalogContext.CatalogItems.SingleOrDefaultAsync(i => i.Id == id);
            if (catalogItem == null)
            {
                return NotFound(new { Message = $"Car with id {id} not found." });
            }

            return Ok(catalogItem);
        }

        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CatalogItem catalogItem)
        {
            // TODO - Extraer el saveContext a un repository para aplicar el repository pattern.
            var catalogItemAdded = catalogContext.Add(catalogItem);
            await catalogContext.SaveChangesAsync();

            var catalogItemAddedEvent = new CatalogItemAddedIntegrationEvent(catalogItem);
            await catalogIntegrationEventService.AddAndSaveEventAsync(catalogItemAddedEvent);
            await catalogIntegrationEventService.PublishEventsThroughEventBusAsync(catalogItemAddedEvent);

            return Ok(catalogItemAdded.Entity.Id);
            //return CreatedAtAction(nameof(GetCatalogItem), new { id = catalogItemAdded.Entity.Id });
        }
    }
}