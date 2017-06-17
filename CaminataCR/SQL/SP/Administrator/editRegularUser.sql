CREATE PROCEDURE [dbo].[editRegularUser]
	@account varchar(20),
	@state bit
AS
	DECLARE @role int = 3
	UPDATE R
		set activo = @state
	FROM Regular R 
	INNER JOIN Usuario U
		ON U.idUsuario = R.idUsuario
	WHERE @account = U.cuenta AND idRolUsuario = @role