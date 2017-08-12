using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;

namespace PGAlineupBuilder.ViewModels
{
    public class BuiltFDLineupsViewModel
    {
        public FDtourney BuiltFD { get; set; }

        public IList<FDlineup> listFDlineups { get; set; }

        public BuiltFDLineupsViewModel()
        {
            List<FDlineup> listFDLineups = new List<FDlineup>();

        }

        public BuiltFDLineupsViewModel(FDtourney nameFD, List<FDlineup> lineups)
        {
            BuiltFD = nameFD;

            listFDlineups = lineups.ToList();
        }
    }
}
