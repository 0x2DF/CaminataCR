CREATE PROCEDURE [dbo].getDistricts
	@canton NVARCHAR(30)
AS
BEGIN
	DECLARE @idCanton INT
	SET @idCanton = (SELECT idCanton FROM Canton WHERE canton = @canton)
	
	SELECT distrito FROM Distrito D
	INNER JOIN DistritoPorCanton DPC
		ON D.idDistrito = DPC.idDistrito
	WHERE @idCanton = idCanton
END