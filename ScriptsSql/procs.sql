-- PROC de SELECT
CREATE PROCEDURE BuscarUsuarios
AS
	SELECT
		Id,
		Nome as [Nome do Infeliz],
		Idade as [Idade dele]
	FROM usuario
	ORDER BY Nome
GO;

ALTER PROCEDURE BuscarUsuarios
AS
	SELECT
		Id,
		Nome as [Nome do Infeliz],
		Idade as [Idade dele]
	FROM usuario
	ORDER BY Nome
GO;

DELETE PROCEDURE BuscarUsuarios

-- PROC de INSERT
CREATE PROCEDURE InserirUsuarios
	@Nome varchar(100),
	@Idade int
AS
	INSERT INTO usuario                        
	VALUES (@Nome, @Idade);

	SELECT SCOPE_IDENTITY();
GO;

ALTER PROCEDURE InserirUsuarios
	@Nome varchar(100),
	@Idade int
AS
	INSERT INTO usuario                        
	VALUES (@Nome, @Idade);

	SELECT SCOPE_IDENTITY();
GO;

DELETE PROCEDURE InserirUsuarios