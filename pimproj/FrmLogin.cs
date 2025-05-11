using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaChamados.Model;
using SistemaChamados.Controller;

namespace SistemaChamados
{

    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string senha = txtSenha.Text;

            Usuario? usuario = GerenciadorUsuarios.GetInstancia().Autenticar(email, senha);

            if (usuario != null)
            {
                FrmPrincipal frmPrincipal = new FrmPrincipal(usuario.Email, usuario.ObterTipoUsuario());
                frmPrincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Email ou senha incorretos!", "Erro de Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
