USE ControleEstoqueFazenda;

-- Adiciona um novo item ao estoque (Ma��)
INSERT INTO Estoque (Nome, Quantidade, Preco) VALUES ('Ma��', 100, 2.5);

-- Exibe todos os itens no estoque
SELECT * FROM Estoque;

-- Atualiza o pre�o da Ma�� para 3.0
UPDATE Estoque SET Preco = 3.0 WHERE Nome = 'Ma��';

-- Exclui a Ma�� do estoque
DELETE FROM Estoque WHERE Nome = 'Ma��';

