using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using App1.API.Db;
using App1.Common.Models;

namespace App1.API.Controllers
{
    [Authorize]
    [RoutePrefix("Api/Factura")]
    public class FacturasController : ApiController
    {
        private Model1 db = new Model1();

        [HttpPost]
        [Route("ListarFacturaPorListaCliente")]

        public async Task<List<Factura>> ListarFacturaPorListaCliente(List<Cliente> clientes)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var listaSalida = new List<Factura>();
            foreach (var c in clientes)
            {
                var facturas =await db.Factura.Where(x => x.ClienteId == c.ClienteId).ToListAsync();
                listaSalida.AddRange(facturas);
            }
            return listaSalida;
        }

        // GET: api/Facturas/5
        [ResponseType(typeof(Factura))]
        public async Task<IHttpActionResult> GetFactura(int id)
        {
            Factura factura = await db.Factura.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        // PUT: api/Facturas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFactura(int id, Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factura.FacturaId)
            {
                return BadRequest();
            }

            db.Entry(factura).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Facturas
        [ResponseType(typeof(Factura))]
        public async Task<IHttpActionResult> PostFactura(Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Factura.Add(factura);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = factura.FacturaId }, factura);
        }

        // DELETE: api/Facturas/5
        [ResponseType(typeof(Factura))]
        public async Task<IHttpActionResult> DeleteFactura(int id)
        {
            Factura factura = await db.Factura.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            db.Factura.Remove(factura);
            await db.SaveChangesAsync();

            return Ok(factura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FacturaExists(int id)
        {
            return db.Factura.Count(e => e.FacturaId == id) > 0;
        }
    }
}