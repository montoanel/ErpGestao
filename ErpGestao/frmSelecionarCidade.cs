using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace ErpGestao
{
    public partial class frmSelecionarCidade : Form
    {
        public Cidade CidadeSelecionada { get; private set; }

        public frmSelecionarCidade()
        {
            InitializeComponent();
            InicializarComponentes();
            CarregarTodasCidades(); // Carregar todas as cidades ao inicializar
        }

        private void InicializarComponentes()
        {
            // Preencher o ComboBox de filtro
            cmbfiltrocidades.Items.AddRange(new string[] { "ID", "Código", "Nome", "UF", "Nome Estado" });

            // Configurar as colunas do DataGridView
            dgvcidades.AutoGenerateColumns = false;
            dgvcidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "Id"
            });
            dgvcidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Código",
                DataPropertyName = "Codigo"
            });
            dgvcidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nome",
                DataPropertyName = "Nome"
            });
            dgvcidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                DataPropertyName = "Estado"
            });
            dgvcidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "UF",
                DataPropertyName = "Uf"
            });

            // Vincular evento do botão ao método
            btnfiltrarcidade.Click += new EventHandler(btnfiltrarcidade_Click);
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

            if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrEmpty(valor))
            {
                List<Cidade> cidadesFiltradas = BuscarCidades(filtro, valor);
                dgvcidades.DataSource = cidadesFiltradas;

                if (cidadesFiltradas.Count == 0)
                {
                    MessageBox.Show("Nenhuma cidade encontrada para os critérios de busca.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Total de cidades encontradas para exibição: {cidadesFiltradas.Count}");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um critério de filtro e insira um valor.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private List<Cidade> BuscarCidades(string filtro, string valor)
        {
            var cidades = new List<Cidade>();

            using (SqlConnection conn = new SqlConnection("Data Source=CAIXA\\SQLEXPRESS;Initial Catalog=erpgestao;Integrated Security=True;TrustServerCertificate=True"))
            {
                conn.Open();

                // Adaptar a consulta SQL conforme o filtro selecionado
                string query = @"
                    SELECT c.id, c.codigo, c.nome, c.uf, e.nome AS estado
                    FROM cidade c
                    JOIN estado e ON c.estadoid = e.id";

                if (!string.IsNullOrEmpty(filtro))
                {
                    switch (filtro)
                    {
                        case "ID":
                            query += " WHERE c.id = @valor";
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

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(valor))
                    {
                        cmd.Parameters.AddWithValue("@valor", $"%{valor}%");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cidades.Add(new Cidade
                            {
                                Id = reader.GetInt32(0),
                                Codigo = reader.GetString(1),
                                Nome = reader.GetString(2),
                                Uf = reader.GetString(3),
                                Estado = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            return cidades;
        }

       
            private void btncancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
