using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.DataAccess.ViewModel
{
    public class ResultModel
    {
        public List<string> RecommendedPlaces { get; set; }
        public List<string> NotRecommendedPlaces { get; set; }
    }
}
