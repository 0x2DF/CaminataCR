CREATE PROCEDURE dbo.checkUsername
	@account NVARCHAR(20)
AS
BEGIN
	DECLARE @result INT
	SET @result = (SELECT COUNT(*) FROM Usuario WHERE cuenta = @account)
	RETURN @result
END