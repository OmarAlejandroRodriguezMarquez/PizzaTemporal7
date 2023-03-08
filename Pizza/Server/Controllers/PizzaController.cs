using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizza.Shared.Models;

namespace Pizza.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PizzaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CrearPizza(PizzaModel pizza)
        {
            context.Add(pizza);
            await context.SaveChangesAsync();
            if(pizza.Id > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<PizzaModel>>> ObtenerPizzas()
        {
            var pizzas = await context.Pizzas.ToListAsync();
            if(pizzas.Count > 0)
            {
                return pizzas;
            }
            else
            {
                return NoContent();
            }      
        }

        [HttpGet("sola/{Id:int}")]
        public async Task<ActionResult<PizzaModel>> ObtenerPizza(int Id)
        {
            var pizza = await context.Pizzas.FindAsync(Id);
            if(pizza == null)
            {
                return BadRequest();
            }
            else
            {
                return pizza;
            }
        }

        [HttpPut("actualizar/{Id:int}")]
        public async Task<ActionResult<int>> Actualizar(int Id)
        {
            var pizza = await context.Pizzas.FindAsync(Id);
            if(pizza == null)
            {
                return NotFound();
            }
            else
            {
                pizza.Nombre = "pizza actualizada";
                context.Update(pizza);
                await context.SaveChangesAsync();
                return pizza.Id;
            }       
        }

        [HttpDelete("borrar/{Id:int}")]
        public async Task<ActionResult<int>> Borrar(int Id)
        {
            var pizza = await context.Pizzas.FindAsync(Id);
            if(pizza != null)
            {
                context.Remove(pizza);
                await context.SaveChangesAsync();
                return pizza.Id;
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
