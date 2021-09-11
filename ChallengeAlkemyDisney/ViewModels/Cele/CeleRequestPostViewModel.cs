using ChallengeAlkemyDisney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney.ViewModels.Cele
{
    public class CeleRequestPostViewModel
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Double Weight { get; set; }
        public string History { get; set; }
        public ICollection<MovieOrSerie> MovieOrSeries { get; set; }
    }
}
