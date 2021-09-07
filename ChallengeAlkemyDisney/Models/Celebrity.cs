using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney.Models
{
    public class Celebrity
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Double Weight { get; set; }
        public string History { get; set; }
        public ICollection<MovieOrSerie> MovieOrSeries { get; set; }
    }
}
