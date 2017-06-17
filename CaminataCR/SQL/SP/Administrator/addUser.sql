CREATE PROCEDURE dbo.addUser
    @account NVARCHAR(20), 
    @password NVARCHAR(20), 
	@roleId INT,
	@responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

	DECLARE @salt UNIQUEIDENTIFIER=NEWID()

	BEGIN TRY
    INSERT INTO Usuario (cuenta, contrasenaHash, contrasenaSalt, idRolUsuario)
        VALUES (@account,
				HASHBYTES('SHA2_512', @password+CAST(@salt AS nvarchar(36))),
				@salt,
				@roleId)
		END TRY
    BEGIN CATCH
        SET @responseMessage = ERROR_MESSAGE() 
    END CATCH
END