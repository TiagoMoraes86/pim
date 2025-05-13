using SistemaChamados.Data;
using SistemaChamados.Model;
using System.Collections.Generic;

namespace SistemaChamados.Controller
{
    public class GerenciadorUsuarios
    {
        private static GerenciadorUsuarios? _instancia;
        private UsuarioRepository _repository = new UsuarioRepository();

        public static GerenciadorUsuarios GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new GerenciadorUsuarios();
            }
            return _instancia;
        }

        private GerenciadorUsuarios()
        {
            // Inicializar com usuários padrão se o banco estiver vazio
            InicializarUsuariosPadrao();
        }

        private void InicializarUsuariosPadrao()
        {
            try
            {
                List<Tecnico> tecnicos = _repository.ObterTodosTecnicos();

                if (tecnicos.Count == 0)
                {
                    // Adicionar usuários padrão
                    _repository.AdicionarUsuario(new Tecnico("admin@exemplo.com", "123", "Administrador", "Suporte Geral"));
                    _repository.AdicionarUsuario(new UsuarioComum("usuario@exemplo.com", "123", "Usuário Teste"));
                }
            }
            catch
            {
                // Ignorar erros na inicialização
            }
        }


        public Usuario? Autenticar(string email, string senha)
        {
            return _repository.Autenticar(email, senha);
        }

        public List<Tecnico> ObterTodosTecnicos()
        {
            return _repository.ObterTodosTecnicos();
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            _repository.AdicionarUsuario(usuario);
        }
    }
}
