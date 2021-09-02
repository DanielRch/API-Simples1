using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = filme.Id }, filme);
        }

        [HttpGet]
        public IActionResult RecuperaFilmes()
        {
            return Ok(_context.Filmes);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            //foreach (Filme filme in filmes)
            //{
            //    if (filme.Id == id)
            //    {
            //        return filme;
            //    }
            //}
            //return null;

            //return filmes.FirstOrDefault(f => f.Id == id);

            Filme filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme([FromBody] Filme filmeNew, int id)
        {
            //Filme filmeOld = _context.Filmes.FirstOrDefault(f => f.Id == id);
            var filmeOld = _context.Filmes.Find(id);
            if (filmeOld == null)
            {
                return NotFound();
            }
            filmeOld.Titulo = filmeNew.Titulo;
            filmeOld.Genero = filmeNew.Genero;
            filmeOld.Diretor = filmeNew.Diretor;
            filmeOld.Duracao = filmeNew.Duracao;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filmeExiste = _context.Filmes.Find(id);
            if (filmeExiste == null)
            {
                return NotFound();
            }
            _context.Remove(filmeExiste);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
