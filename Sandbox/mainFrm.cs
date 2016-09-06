using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bracketAPI.Challonge;

namespace Sandbox
{
    public partial class mainFrm : Form
    {
        private ChallongeClient challongeClient;

        public mainFrm()
        {
            InitializeComponent();
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            challongeClient = new ChallongeClient("sartron", "UYfqEJRqQTXB1aVAs6XUQ63bxxsPVKv5rxJrWv0n");
        }

        private async void btnGetTournaments_Click(object sender, EventArgs e)
        {
            var tournament = await challongeClient.GetTournament(1482738);
            var tournamentList = await challongeClient.GetTournaments();
            Debug.WriteLine("Breakpoint");
        }
    }
}
