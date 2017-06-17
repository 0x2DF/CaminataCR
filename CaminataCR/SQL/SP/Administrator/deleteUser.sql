CREATE PROCEDURE [dbo].[deleteUser]
	@userName varchar(20)
AS
	DELETE Usuario WHERE @userName = cuenta