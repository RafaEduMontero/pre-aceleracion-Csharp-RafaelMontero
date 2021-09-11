using ChallengeAlkemyDisney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney.ViewModels.MoOrSerie
{
    public class MsRequestViewModel
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Valuation { get; set; }
        public ICollection<Celebrity> Celebrities { get; set; }
        public Gender IdGender { get; set; }
    }
}
