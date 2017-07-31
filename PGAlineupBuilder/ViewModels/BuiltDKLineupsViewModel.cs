using PGAlineupBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGAlineupBuilder.ViewModels
{
    public class BuiltDKLineupsViewModel
    {
        public DkTourney BuiltDK { get; set; }

        public IList<DKlineup> listDKlineups { get; set; }

        public BuiltDKLineupsViewModel()
        {
            List<DKlineup> listDKLineups = new List<DKlineup>();

        }
       
        public BuiltDKLineupsViewModel(DkTourney nameDK, List<DKlineup> lineups)
        {
            BuiltDK = nameDK;

            listDKlineups = lineups.ToList();
        }



    }
}
