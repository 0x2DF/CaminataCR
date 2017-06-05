CREATE PROCEDURE dbo.checkEmail
	@email NVARCHAR(20)
AS
BEGIN
	DECLARE @result INT
	SET @result = (SELECT COUNT(*) FROM Regular WHERE correo = @email)
	RETURN @result
END