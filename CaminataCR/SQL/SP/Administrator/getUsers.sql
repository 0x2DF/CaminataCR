CREATE PROCEDURE [dbo].[getusers]
	@roleId int,
	@account varchar(20)
AS
	SELECT cuenta
	FROM Usuario
	WHERE idRolUsuario = @roleId AND cuenta != @account