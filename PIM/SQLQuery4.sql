USE ControleEstoqueFazenda;

-- Adiciona um novo item ao estoque (Maçã)
INSERT INTO Estoque (Nome, Quantidade, Preco) VALUES ('Maçã', 100, 2.5);

-- Exibe todos os itens no estoque
SELECT * FROM Estoque;

-- Atualiza o preço da Maçã para 3.0
UPDATE Estoque SET Preco = 3.0 WHERE Nome = 'Maçã';

-- Exclui a Maçã do estoque
DELETE FROM Estoque WHERE Nome = 'Maçã';

