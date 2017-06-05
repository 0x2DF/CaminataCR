CREATE PROCEDURE dbo.addDonation
    @amount FLOAT,
	@responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
	BEGIN TRY
    INSERT INTO Donacion (monto, fechaHora)
        VALUES (@amount,
				GETDATE())
		END TRY
    BEGIN CATCH
        SET @responseMessage = ERROR_MESSAGE() 
    END CATCH
END