﻿using Npgsql;
using SistemaChamados.Model;
using System;
using System.Collections.Generic;

namespace SistemaChamados.Data
{
    public class UsuarioRepository
    {
        public void AdicionarUsuario(Usuario usuario)
        {
            try
            {
                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;

                        string especialidade = null;
                        if (usuario is Tecnico tecnico)
                        {
                            especialidade = tecnico.Especialidade;
                        }

                        cmd.CommandText = @"
                            INSERT INTO usuarios (email, senha, nome, tipo, especialidade)
                            VALUES (@email, @senha, @nome, @tipo, @especialidade)";

                        cmd.Parameters.AddWithValue("@email", usuario.Email);
                        cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                        cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                        cmd.Parameters.AddWithValue("@tipo", usuario.ObterTipoUsuario());
                        cmd.Parameters.AddWithValue("@especialidade", especialidade ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar usuário: " + ex.Message);
            }
        }

        public Usuario Autenticar(string email, string senha)
        {
            try
            {
                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"
                            SELECT * FROM usuarios 
                            WHERE email = @email AND senha = @senha";

                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@senha", senha);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string tipo = reader["tipo"].ToString();
                                string nome = reader["nome"].ToString();

                                if (tipo == "tecnico")
                                {
                                    string especialidade = reader["especialidade"] == DBNull.Value ?
                                        string.Empty : reader["especialidade"].ToString();
                                    return new Tecnico(email, senha, nome, especialidade);
                                }
                                else
                                {
                                    return new UsuarioComum(email, senha, nome);
                                }
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao autenticar usuário: " + ex.Message);
            }
        }

        public List<Tecnico> ObterTodosTecnicos()
        {
            List<Tecnico> tecnicos = new List<Tecnico>();

            try
            {
                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"
                            SELECT * FROM usuarios 
                            WHERE tipo = 'tecnico'";

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string email = reader["email"].ToString();
                                string senha = reader["senha"].ToString();
                                string nome = reader["nome"].ToString();
                                string especialidade = reader["especialidade"] == DBNull.Value ?
                                    string.Empty : reader["especialidade"].ToString();

                                tecnicos.Add(new Tecnico(email, senha, nome, especialidade));
                            }
                        }
                    }
                }
                return tecnicos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter técnicos: " + ex.Message);
            }
        }
    }
}
