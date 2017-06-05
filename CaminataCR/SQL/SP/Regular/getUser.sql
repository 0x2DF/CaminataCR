CREATE PROCEDURE dbo.getUser
    @account NVARCHAR(20)
AS
BEGIN
	SELECT U.idUsuario, 
	R.primerNombre, R.segundoNombre, R.primerApellido, R.segundoApellido, R.cuentaBancaria, R.fotografia
	
	FROM Usuario U
	INNER JOIN Regular R
		ON U.idUsuario = R.idUsuario
	WHERE U.cuenta = @account
END