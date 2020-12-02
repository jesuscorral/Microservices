using System.Threading.Tasks;
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

        public CatalogController(CatalogDbContext context)
        {
            catalogContext = context;

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var catalogItems = await catalogContext.CatalogItems.ToListAsync();

            return Ok(catalogItems);
        }
    }
}