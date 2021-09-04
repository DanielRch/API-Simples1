using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data.Dtos
{
    public class ReadFilmeDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo título é obrigatório.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo diretor é obrigatório.")]
        public string Diretor { get; set; }

        [StringLength(50, ErrorMessage = "O gênero não pode passar de 50 caracteres.")]
        public string Genero { get; set; }

        [Range(1, 600, ErrorMessage = "A duração deve ter no mínimo 1 e no máximo 600 minutos.")]
        public int Duracao { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public DateTime HoraDaConsulta { get; set; }
    }
}
