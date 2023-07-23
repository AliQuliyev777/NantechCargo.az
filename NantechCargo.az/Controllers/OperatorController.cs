using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NantechCargo.az.Models;
using NantechCargo.az.ViewModels;
using System.Linq;

namespace NantechCargo.az.Controllers
{
    public class OperatorController : Controller

    {
        private readonly CargoContext _sql;

        public OperatorController(CargoContext sql)
        {
            _sql = sql;
        }
        public IActionResult Orders(int id = 1)
        {
            var q = _sql.Orders.Include(x => x.OrderClient).Select(x => new OpOrder
            {
                ClientFullName = x.OrderClient.UserFirstName + "" + x.OrderClient.UserLastName,
                OrderDate = x.OrderDate.ToString("dd.MM.yyyy hh:mm"),
                ProductCount = x.Products.Count,
                OrderId = x.OrderId,
                LevelId = x.OrderLevelId
            });
            if (id ==1 )
            {
                q= q.Where(x=>x.LevelId == 1);
            }
            else if (id == 2)
            {
                q = q.Where(x => x.LevelId > 1 && x.LevelId < 6);
            }
            else
            {
                q = q.Where(x => x.LevelId == 6);

            }
            return View(q.ToList());
        }
        public IActionResult GetOrderProducts(int id )
        {
            var result = _sql.Products.Where(x=>x.ProductOrderId == id).ToList();
            if(result!= null)
            {
            return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult AcceptProduct(int id,decimal? price,decimal? cargo) 
        {
            var data = _sql.Products.SingleOrDefault(x => x.ProductId == id);
            if (data != null)
            {
                data.ProductPrice = price;
                data.ProductCargoAmount = cargo;
                _sql.SaveChanges();
                return Ok();
            }
            else
            { 
            return BadRequest();
            }
        }
        public IActionResult RemoveProduct(int id)
        {
            var data = _sql.Products.SingleOrDefault(x => x.ProductId == id);
            if (data != null) { 
                _sql.Products.Remove(data);
                _sql.SaveChanges();
                return Ok();
            }
            else 
            { 
                return BadRequest(); 
            }
        }
        [HttpPost]
        public IActionResult ChangeLevel(int id, int levelId)
        {
            Order o = _sql.Orders.SingleOrDefault(x => x.OrderId == id);
            if (o != null)
            {
            o.OrderLevelId = levelId;
            _sql.SaveChanges();
            return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
