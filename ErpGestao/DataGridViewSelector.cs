using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpGestao
{
    public class DataGridViewSelector<T> where T : class
    {
        public T Selecionar(DataGridView dataGridView)
        {
            if (dataGridView.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView.Rows[selectedRowIndex];
                T itemSelecionado = selectedRow.DataBoundItem as T;

                if (itemSelecionado != null)
                {
                    return itemSelecionado;
                }
                else
                {
                    MessageBox.Show("Por favor, selecione um item válido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um item.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
    }


}