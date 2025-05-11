using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaChamados.Model;

namespace SistemaChamados.Controller
{
    public class GerenciadorUsuarios
    {
        private static GerenciadorUsuarios? _instancia;
        private List<Usuario> _usuarios = new List<Usuario>();

       
        public static GerenciadorUsuarios GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new GerenciadorUsuarios();
            }
            return _instancia;
        }

        // Construtor 
        private GerenciadorUsuarios()
        {
            // Adicionar usuários de exemplo
            _usuarios.Add(new Tecnico("admin@exemplo.com", "123", "Administrador", "Suporte Geral"));
            _usuarios.Add(new UsuarioComum("usuario@exemplo.com", "123", "Usuário Teste"));
        }

        public Usuario? Autenticar(string email, string senha)
        {
            return _usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

        public List<Tecnico> ObterTodosTecnicos()
        {
            return _usuarios.OfType<Tecnico>().ToList();
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            _usuarios.Add(usuario);
        }
    }
}
