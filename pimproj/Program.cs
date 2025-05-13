using SistemaChamados.Data;
using System;
using System.Windows.Forms;

namespace SistemaChamados
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Verificar conex�o com o banco de dados e exist�ncia das tabelas
                if (DatabaseInitializer.VerificarConexaoETabelas())
                {
                    Application.Run(new FrmLogin());
                }
                else
                {
                    MessageBox.Show("N�o foi poss�vel conectar ao banco de dados ou as tabelas necess�rias n�o existem.",
                                   "Erro de Conex�o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar o sistema: {ex.Message}",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
