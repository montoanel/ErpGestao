using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErpGestao
{
    public partial class frmSelecionarCidade : Form
    {
        public Cidade CidadeSelecionada { get; private set; }
        public frmSelecionarCidade()
        {
            InitializeComponent();
            CarregarCidades();
        }
        private void CarregarCidades()
        {
            List<Cidade> cidades = Cidade.ObterTodasCidades();
            listBoxCidades.DataSource = cidades;
            listBoxCidades.DisplayMember = "Nome";
            listBoxCidades.ValueMember = "Estado";
        }
        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            CidadeSelecionada = listBoxCidades.SelectedItem as Cidade;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
