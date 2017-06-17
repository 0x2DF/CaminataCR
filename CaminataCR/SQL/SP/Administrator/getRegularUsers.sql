CREATE PROCEDURE [dbo].[getRegularUsers]
AS
BEGIN
	DECLARE @role INT = 3
	SELECT U.cuenta, R.activo
	FROM Usuario U
	INNER JOIN Regular R
		ON U.idUsuario = R.idUsuario
	WHERE U.idRolUsuario = @role
END