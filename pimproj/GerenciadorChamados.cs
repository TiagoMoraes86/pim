using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaChamados.Model;

namespace SistemaChamados.Controller
{
    public class GerenciadorChamados
    {
        private static GerenciadorChamados? _instancia;
        private List<Chamado> _chamados = new List<Chamado>();
        private int _proximoId = 1;

       
        public static GerenciadorChamados GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new GerenciadorChamados();
            }
            return _instancia;
        }

        // Construtor 
        private GerenciadorChamados()
        {
            // Adicionar chamados de exemplo para teste
            AdicionarChamadosExemplo();
        }

        // Métodos
        public Chamado CriarChamado(string titulo, string descricao, string categoria,
                                   string prioridade, string solicitante)
        {
            Chamado novoChamado = new Chamado(_proximoId++, titulo, descricao,
                                             categoria, prioridade, solicitante);
            _chamados.Add(novoChamado);
            return novoChamado;
        }

        public List<Chamado> ObterChamadosEmAberto()
        {
            return _chamados.Where(c => c.Status != "Finalizado").ToList();
        }

        public List<Chamado> ObterChamadosFinalizados()
        {
            return _chamados.Where(c => c.Status == "Finalizado").ToList();
        }

        public Chamado? ObterChamadoPorId(int id)
        {
            return _chamados.FirstOrDefault(c => c.Id == id);
        }

        public List<Chamado> ObterChamadosPorSolicitante(string solicitante)
        {
            return _chamados.Where(c => c.Solicitante == solicitante).ToList();
        }

        public List<Chamado> ObterChamadosPorTecnico(string tecnico)
        {
            return _chamados.Where(c => c.Tecnico == tecnico).ToList();
        }

        private void AdicionarChamadosExemplo()
        {
            // Chamados de exemplo para testes
            Chamado chamado1 = CriarChamado(
                "Computador não liga",
                "O computador da estação 3 não está ligando",
                "Hardware",
                "Alta",
                "usuario1@empresa.com");
            chamado1.AtribuirTecnico("tecnico1@empresa.com");
            chamado1.AlterarStatus("Em análise");

            Chamado chamado2 = CriarChamado(
                "Problema com impressora",
                "A impressora do setor financeiro não está imprimindo",
                "Impressora",
                "Média",
                "usuario2@empresa.com");

            Chamado chamado3 = CriarChamado(
                "Internet lenta",
                "A conexão com a internet está muito lenta",
                "Rede",
                "Baixa",
                "usuario3@empresa.com");
            chamado3.AtribuirTecnico("tecnico2@empresa.com");
            chamado3.AlterarStatus("Em andamento");

            Chamado chamado4 = CriarChamado(
                "Monitor com imagem distorcida",
                "O monitor da recepção está com imagem distorcida",
                "Hardware",
                "Média",
                "usuario1@empresa.com");
            chamado4.AtribuirTecnico("tecnico1@empresa.com");
            chamado4.AlterarStatus("Finalizado");
            chamado4.DefinirSolucao("Substituição do cabo de vídeo");
        }
    }
}
