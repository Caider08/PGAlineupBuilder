using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;

namespace PGAlineupBuilder.ViewModels
{
    public class BuiltFDraftLineupsViewModel
    {
        public FDraftTourney BuiltFDraft { get; set; }

        public IList<FDraftLineup> listFDraftLineups { get; set; }

        public BuiltFDraftLineupsViewModel()
        {
            List<FDraftLineup> listFDLineups = new List<FDraftLineup>();

        }

        public BuiltFDraftLineupsViewModel(FDraftTourney nameFD, List<FDraftLineup> lineups)
        {
            BuiltFDraft = nameFD;

            listFDraftLineups = lineups.ToList();
        }
    }
}
