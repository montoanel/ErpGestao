using System.Diagnostics.Eventing.Reader;

namespace ErpGestao
{
    public partial class frmcadastrofcfo : Form
    {

        private readonly CpfValidador cpfValidador = new CpfValidador();

        public frmcadastrofcfo()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkboxcliente_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmcadastrofcfo_Load(object sender, EventArgs e)
        {
            //valores default combo box
            chkboxcliente.Checked = true;
            chkboxmembro.Checked = true;
            cmbtipofcfo.SelectedIndex = 0;

            //mascara datas

            msktxtboxdatanascimentofcfo.Mask = "00/00/0000";
            msktxtboxdatacadastrofcfo.Mask = "00/00/0000";

            //mascara cpf
            msktxtboxcpfcnpjfcfo.Mask = "000,000,000-00";

        }

        private void cmbtipofcfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // verifica o tipo de pessoa selecionado na ComboBox
            if (cmbtipofcfo.SelectedItem.ToString() == "Jur�dica")
            {
                msktxtboxcpfcnpjfcfo.Mask = "00,000,000/0000-00"; // M�scara para CNPJ
                txtboxrazaosocialfcfo.Text = ""; //limpa a razao social quando for pessoa juridica
            }
            else
            {
                msktxtboxcpfcnpjfcfo.Mask = "000,000,000-00"; // M�scara para CPF

            }
        }

        private void txtboxnomefantasiafcfo_TextChanged(object sender, EventArgs e)
        {
            // verifica se o combobox esta selecionado esta selecionado com o 'f�sica ou rural'
            // muda em tempo real o valor do txtboxrazaosocialfcfo = txtboxnomefantasiafcfo
            //if (cmbtipofcfo.selecteditem != null && (cmbtipofcfo.selecteditem.tostring() == "f�sica" || cmbtipofcfo.selecteditem.tostring() == "rural"))
            //{
            //    //define o valor do txtboxrazaosocialfcfo como o mesmo valor de txtboxnomefantasiafcfo
            //    txtboxrazaosocialfcfo.text = txtboxnomefantasiafcfo.text;
            //}

        }

        private void txtboxnomefantasiafcfo_Leave(object sender, EventArgs e)
        {
            // verifica se o combobox esta selecionado como f�sica ou rural
            if (cmbtipofcfo.SelectedItem != null && (cmbtipofcfo.SelectedItem.ToString() == "F�sica" || cmbtipofcfo.SelectedItem.ToString() == "Rural"))
            {
                //define o valor do txtboxrazaosocialfcfo como o mesmo valor do txtboxnomefantasiafcfo
                txtboxrazaosocialfcfo.Text = txtboxnomefantasiafcfo.Text;
            }
        }

        private void msktxtboxcpfcnpjfcfo_Leave(object sender, EventArgs e)
        {
            // Verifica se o ComboBox est� selecionado como "F�sica" ou "Rural"
            if (cmbtipofcfo.SelectedItem != null && (cmbtipofcfo.SelectedItem.ToString() == "F�sica" || cmbtipofcfo.SelectedItem.ToString() == "Rural"))
            {
                // obtem o CPF digitado e verifica se � valido
                string cpf = msktxtboxcpfcnpjfcfo.Text;
                if (!cpfValidador.ValidarCPF(cpf))
                {
                    MessageBox.Show("CPF inv�lido!", "Valida��o de CPF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    msktxtboxcpfcnpjfcfo.Focus();//volta o foco para o campo CPF
                }
            }
        }
        }
}