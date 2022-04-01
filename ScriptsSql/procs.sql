-- PROC de SELECT
CREATE PROCEDURE BuscarUsuarios
AS
	SELECT
		Id,
		Nome,
		Idade
	FROM usuario
	ORDER BY Nome
GO

EXEC BuscarUsuarios

ALTER PROCEDURE BuscarUsuarios
AS
	SELECT
		Id,
		Nome,
		Idade
	FROM usuario
	ORDER BY Nome
GO

DROP PROCEDURE BuscarUsuarios

-- PROC de INSERT
CREATE PROCEDURE InserirUsuarios
	@Nome varchar(100),
	@Idade int
AS
	INSERT INTO usuario                        
	VALUES (@Nome, @Idade);

	SELECT SCOPE_IDENTITY();
GO

EXEC InserirUsuarios 'Stephan', 35

ALTER PROCEDURE InserirUsuarios
	@Nome varchar(100),
	@Idade int
AS
	INSERT INTO usuario                        
	VALUES (@Nome, @Idade);

	SELECT SCOPE_IDENTITY();
GO

DROP PROCEDURE InserirUsuarios