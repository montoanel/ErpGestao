using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ErpGestao
{
    public partial class frmSelecionarCidade : Form
    {
        public Cidade CidadeSelecionada { get; private set; }
        private CidadeDataAccess cidadeDataAccess;

        public frmSelecionarCidade()
        {
            InitializeComponent();
            InicializarComponentes();
            cidadeDataAccess = new CidadeDataAccess();
            CarregarTodasCidades();
        }

        private void InicializarComponentes()
        {
            cmbfiltrocidades.Items.AddRange(new string[] { "ID", "Código", "Nome", "UF", "Nome Estado" });
            cmbfiltrocidades.SelectedIndex = 2;
            dgvcidades.AutoGenerateColumns = false;
            dgvcidades.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id" });
            dgvcidades.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código IBGE", DataPropertyName = "Codigo" });
            dgvcidades.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nome", DataPropertyName = "Nome" });
            dgvcidades.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado", DataPropertyName = "Estado" });
            dgvcidades.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "UF", DataPropertyName = "Uf" });

            btnfiltrarcidade.Click += new EventHandler(btnfiltrarcidade_Click);
            btnselecionar.Click += new EventHandler(btnselecionar_Click);
            txtboxfiltrarcidade.KeyPress += new KeyPressEventHandler(txtboxfiltrarcidade_KeyPress);
            dgvcidades.KeyDown += new KeyEventHandler(dgvcidades_KeyDown);
        }

        private void CarregarTodasCidades()
        {
            List<Cidade> todasCidades = BuscarCidades(null, null);
            dgvcidades.DataSource = todasCidades;
        }

        private void btnfiltrarcidade_Click(object sender, EventArgs e)
        {
            string filtro = cmbfiltrocidades.SelectedItem?.ToString();
            string valor = txtboxfiltrarcidade.Text;

            if (string.IsNullOrWhiteSpace(valor))
            {
                // Recarregar todas as cidades se o campo de texto estiver vazio
                CarregarTodasCidades();
            }
            else if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrEmpty(valor))
            {
                List<Cidade> cidadesFiltradas = BuscarCidades(filtro, valor);
                dgvcidades.DataSource = cidadesFiltradas;

                if (cidadesFiltradas.Count == 0)
                {
                    MessageBox.Show("Nenhuma cidade encontrada para os critérios de busca.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um critério de filtro e insira um valor.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private List<Cidade> BuscarCidades(string filtro, string valor)
        {
            var query = @"
                SELECT c.id, c.codigo, c.nome, c.uf, e.nome AS estado
                FROM cidade c
                JOIN estado e ON c.estadoid = e.id";

            if (!string.IsNullOrEmpty(filtro))
            {
                switch (filtro)
                {
                    case "ID":
                        if (int.TryParse(valor, out int id))
                        {
                            query += " WHERE c.id = @valor";
                        }
                        else
                        {
                            MessageBox.Show("O valor do ID deve ser um número inteiro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return new List<Cidade>();
                        }
                        break;
                    case "Código":
                        query += " WHERE c.codigo COLLATE Latin1_General_CI_AI LIKE @valor";
                        break;
                    case "Nome":
                        query += " WHERE c.nome COLLATE Latin1_General_CI_AI LIKE @valor";
                        break;
                    case "UF":
                        query += " WHERE c.uf COLLATE Latin1_General_CI_AI LIKE @valor";
                        break;
                    case "Nome Estado":
                        query += " WHERE e.nome COLLATE Latin1_General_CI_AI LIKE @valor";
                        break;
                }
            }

            return cidadeDataAccess.ObterCidades(query, (cmd) =>
            {
                if (!string.IsNullOrEmpty(valor))
                {
                    if (filtro == "ID")
                    {
                        cmd.Parameters.AddWithValue("@valor", int.Parse(valor));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@valor", $"%{valor}%");
                    }
                }
            });
        }

        private DataGridViewSelector<Cidade> cidadeSelector = new DataGridViewSelector<Cidade>();

        private void btnselecionar_Click(object sender, EventArgs e)
        {
            Cidade cidadeSelecionada = cidadeSelector.Selecionar(dgvcidades);

            if (cidadeSelecionada != null)
            {
                CidadeSelecionada = cidadeSelecionada;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtboxfiltrarcidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnfiltrarcidade.PerformClick(); // Executar o clique do botão
            }
        }

        private void dgvcidades_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                btnselecionar_Click(sender, e); // Chamar a função do botão selecionar
            }
        }
    }
}
