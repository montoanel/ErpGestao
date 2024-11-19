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
        }

        private void InicializarComponentes()
        {
            // Preencher o ComboBox de filtro
            cmbfiltrocidades.Items.AddRange(new string[] { "ID", "Código", "Nome", "UF", "Nome Estado" });
        }

        private void btnfiltrarcidade_Click(object sender, EventArgs e)
        {
            string filtro = cmbfiltrocidades.SelectedItem?.ToString();
            string valor = txtboxfiltrarcidade.Text;

            if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrEmpty(valor))
            {
                List<Cidade> cidadesFiltradas = BuscarCidades(filtro, valor);
                listBoxCidades.DataSource = cidadesFiltradas;
                listBoxCidades.DisplayMember = "NomeComEstado";
                listBoxCidades.ValueMember = "Id";

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
            var cidades = new List<Cidade>();

            using (SqlConnection conn = new SqlConnection("Data Source=CAIXA\\SQLEXPRESS;Initial Catalog=erpgestao;Integrated Security=True;TrustServerCertificate=True"))
            {
                conn.Open();
                MessageBox.Show("Conexão aberta com sucesso.");

                // Adaptar a consulta SQL conforme o filtro selecionado
                string query = @"
                    SELECT c.id, c.codigo, c.nome, c.uf, e.nome AS estado
                    FROM cidade c
                    JOIN estado e ON c.estadoid = e.id";

                switch (filtro)
                {
                    case "ID":
                        query += " WHERE c.id = @valor";
                        break;
                    case "Código":
                        query += " WHERE c.codigo LIKE @valor";
                        break;
                    case "Nome":
                        query += " WHERE c.nome LIKE @valor";
                        break;
                    case "UF":
                        query += " WHERE c.uf LIKE @valor";
                        break;
                    case "Nome Estado":
                        query += " WHERE e.nome LIKE @valor";
                        break;
                }

                MessageBox.Show($"Consulta SQL: {query}");

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@valor", $"%{valor}%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
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
                                MessageBox.Show($"Cidade encontrada: {reader.GetString(2)} - {reader.GetString(4)}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nenhuma linha encontrada.");
                        }
                    }
                }
            }

            MessageBox.Show($"Total de cidades encontradas: {cidades.Count}");
            return cidades;
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            CidadeSelecionada = listBoxCidades.SelectedItem as Cidade;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
