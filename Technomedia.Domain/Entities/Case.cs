using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technomedia.Domain.Entities
{
    /// <summary>
    /// Заявка
    /// </summary>
    public class Case
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateTimeEnd { get; set; }

        public string Note { get; set; }

        public int TeamId { get; set; }

        public int StatusId { get; set; }

        public bool Deleted { get; set; }

    }
}
