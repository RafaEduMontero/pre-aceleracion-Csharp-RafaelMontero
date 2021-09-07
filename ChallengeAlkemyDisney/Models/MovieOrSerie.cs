using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney.Models
{
    public class MovieOrSerie
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }
        public int Valuation { get; set; }
        public ICollection<Celebrity> Celebrities { get; set; }
        public Gender Gender { get; set; }
    }
}
