using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
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
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);


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
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme([FromBody] UpdateFilmeDto filmeDto, int id)
        {
            //Filme filmeOld = _context.Filmes.FirstOrDefault(f => f.Id == id);
            var filmeOld = _context.Filmes.Find(id);
            if (filmeOld == null)
            {
                return NotFound();
            }
            _mapper.Map(filmeDto, filmeOld);
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
