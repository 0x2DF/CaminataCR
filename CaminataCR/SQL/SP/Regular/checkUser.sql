CREATE PROCEDURE dbo.checkUser
    @account NVARCHAR(20), 
    @password NVARCHAR(20),
	@roleId INT
AS
BEGIN

    SET NOCOUNT ON

    DECLARE @userId INT

    IF EXISTS (SELECT TOP 1 idUsuario FROM Usuario WHERE cuenta = @account AND idRolUsuario = @roleId)
    BEGIN
        SET @userId = (SELECT idUsuario FROM Usuario 
						  WHERE cuenta = @account AND 
							contrasenaHash = HASHBYTES('SHA2_512', @password+CAST(
										(SELECT contrasenaSalt FROM Usuario 
										WHERE cuenta = @account) AS NVARCHAR(36))))

       IF(@userId IS NULL)
           RETURN(2)
       ELSE
           RETURN(0)
    END
    ELSE
       RETURN(1)

END