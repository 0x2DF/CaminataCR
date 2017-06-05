CREATE PROCEDURE dbo.addRegUser
    @account NVARCHAR(20), 
    @photo VARBINARY(max) = NULL,
	@FirstName NVARCHAR(20), 
    @MiddleName NVARCHAR(20),
	@Surname NVARCHAR(20),
	@SecondSurname NVARCHAR(20),
	@Email NVARCHAR(50),
	@TelephoneNumber NVARCHAR(14),
	@Birthdate NVARCHAR(10),
	@Sex CHAR(1),
	@Nacionality NVARCHAR(20),
    @BankAccount NVARCHAR(20),
	@responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
	DECLARE @idUser INT
	SET @idUser = (SELECT idUsuario FROM Usuario WHERE cuenta = @account)
	BEGIN TRY
    INSERT INTO Regular (idUsuario, 
						primerNombre, 
						segundoNombre, 
						primerApellido, 
						segundoApellido, 
						correo, 
						telefono, 
						fechaNacimiento, 
						sexo, 
						nacionalidad,
						fotografia,
						cuentaBancaria,
						activo)
        VALUES (@idUser,
				@FirstName,
				@MiddleName,
				@Surname,
				@SecondSurname,
				@Email,
				@TelephoneNumber,
				@Birthdate,
				@Sex,
				@Nacionality,
				@photo,
				@BankAccount,
				1)
		END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH
END