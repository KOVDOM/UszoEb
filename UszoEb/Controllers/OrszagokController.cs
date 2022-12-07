using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using UszoEb.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UszoEb.Controllers
{
        [Route("[controller]")]
        [ApiController]
    public class OrszagokController : Controller
    {
            [HttpGet]
            public IActionResult Get()
            {
                using (var context = new uszoebContext())
                {
                    try
                    {
                        return Ok(context.Orszagoks.ToList());
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
                        return Ok(context.Orszagoks.Where(v => v.Id == id).ToList());
                    }
                    catch (System.Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }

            [HttpGet]
            [Route("GetOrszagNev")]
            public IActionResult GetOrszagNev(string nev)
            {
                using (var context = new uszoebContext())
                {
                    try
                    {
                        return Ok(context.Orszagoks.Include(cx => cx.Versenyzoks).Include(cx => cx.Versenyzoks).FirstOrDefault(sz => sz.Nev == nev));
                    }
                    catch (System.Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }

            [HttpPost]
            public IActionResult Post(Orszagok orszag)
            {
                using (var context = new uszoebContext())
                {
                    try
                    {
                        context.Orszagoks.Add(orszag);
                        context.SaveChanges();
                        return StatusCode(201, "A Ország adatainak a tárolása megtörtént!");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }

            [HttpPut]
            public IActionResult Put(Orszagok orszag)
            {
                using (var context = new uszoebContext())
                {
                    try
                    {
                        context.Orszagoks.Update(orszag);
                        context.SaveChanges();
                        return StatusCode(200, "Az Ország adatainak a modósítása megtörtént!");
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
                        Orszagok orszagok = new Orszagok();
                        orszagok.Id = id;
                        context.Orszagoks.Remove(orszagok);
                        context.SaveChanges();
                        return StatusCode(201, "Az ország adatainak a törlése megtörtént!");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
    }
}
