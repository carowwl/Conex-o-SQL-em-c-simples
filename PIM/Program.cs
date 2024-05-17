using System;
using System.Data.SqlClient;

namespace ControleEstoqueFazenda
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=DESKTOP-02A4SNL\SQLEXPRESS;Initial Catalog=ControleEstoqueFazenda;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Inicia a transação explicitamente
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Adiciona a maçã ao estoque
                    AdicionarItemEstoque(connection, transaction, "Maçã", 100, 2.5m);

                    // Exibe todos os itens no estoque (incluindo a maçã adicionada)
                    MostrarItensEstoque(connection);

                    // Atualiza o preço da maçã no estoque
                    AtualizarPrecoItem(connection, transaction, "Maçã", 3.0m);

                    // Exclui a maçã do estoque
                    RemoverItemEstoque(connection, transaction, "Maçã");

                    // Confirma apenas a adição e exibição da maçã no estoque
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Em caso de erro, desfaz a transação
                    transaction.Rollback();
                    Console.WriteLine("Erro: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            Console.WriteLine("Operações concluídas. Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }

        static void AdicionarItemEstoque(SqlConnection connection, SqlTransaction transaction, string nome, int quantidade, decimal preco)
        {
            string sql = "INSERT INTO dbo.Estoque (Nome, Quantidade, Preco) VALUES (@Nome, @Quantidade, @Preco)";
            SqlCommand command = new SqlCommand(sql, connection, transaction);
            command.Parameters.AddWithValue("@Nome", nome);
            command.Parameters.AddWithValue("@Quantidade", quantidade);
            command.Parameters.AddWithValue("@Preco", preco);
            command.ExecuteNonQuery();
            Console.WriteLine("Item adicionado ao estoque: " + nome);
        }

        static void MostrarItensEstoque(SqlConnection connection)
        {
            string sql = "SELECT * FROM Estoque";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("Itens no estoque:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Nome"]}, Quantidade: {reader["Quantidade"]}, Preco: {reader["Preco"]}");
            }
            reader.Close();
        }

        static void AtualizarPrecoItem(SqlConnection connection, SqlTransaction transaction, string nome, decimal novoPreco)
        {
            string sql = "UPDATE Estoque SET Preco = @NovoPreco WHERE Nome = @Nome";
            SqlCommand command = new SqlCommand(sql, connection, transaction);
            command.Parameters.AddWithValue("@Nome", nome);
            command.Parameters.AddWithValue("@NovoPreco", novoPreco);
            command.ExecuteNonQuery();
            Console.WriteLine("Preço atualizado para " + novoPreco + " para o item " + nome);
        }

        static void RemoverItemEstoque(SqlConnection connection, SqlTransaction transaction, string nome)
        {
            string sql = "DELETE FROM Estoque WHERE Nome = @Nome";
            SqlCommand command = new SqlCommand(sql, connection, transaction);
            command.Parameters.AddWithValue("@Nome", nome);
            command.ExecuteNonQuery();
            Console.WriteLine("Item removido do estoque: " + nome);
        }
    }
}
