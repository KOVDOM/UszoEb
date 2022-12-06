using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using UszoEb.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UszoEb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VersenyzokController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (var context =new uszoebContext())
            {
                try
                {
                    return Ok(context.Versenyzoks.ToList());
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (var context = new uszoebContext())
            {
                try
                {
                    return Ok(context.Versenyzoks.Where(v=>v.Id==id).ToList());
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet]
        [Route("GetVersenyzoNev")]
        public IActionResult GetVersenyzoNev(string nev)
        {
            using (var context = new uszoebContext())
            {
                try
                {
                    return Ok(context.Versenyzoks.Include(cx=>cx.Szamoks).Include(cx=>cx.Orszag).FirstOrDefault(sz=>sz.Nev==nev));
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost]
        public IActionResult Post(Versenyzok versenyzo)
        {
            using(var context = new uszoebContext())
            {
                try
                {
                    context.Versenyzoks.Add(versenyzo);
                    context.SaveChanges();
                    return StatusCode(201, "A versenyző adatainak a tárolása megtörtént!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut]
        public IActionResult Put(Versenyzok versenyzo)
        {
            using (var context = new uszoebContext())
            {
                try
                {
                    context.Versenyzoks.Update(versenyzo);
                    context.SaveChanges();
                    return StatusCode(200, "A versenyző adatainak a modósítása megtörtént!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = new uszoebContext())
            {
                try
                {
                    Versenyzok versenyzok = new Versenyzok();
                    versenyzok.Id = id;
                    context.Versenyzoks.Remove(versenyzok);
                    context.SaveChanges();
                    return StatusCode(201, "A versenyző adatainak a törlése megtörtént!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
