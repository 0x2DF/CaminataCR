CREATE PROCEDURE [dbo].getCantons
	@province NVARCHAR(30)
AS
BEGIN
	DECLARE @idProvincia INT
	SET @idProvincia = (SELECT idProvincia FROM Provincia WHERE provincia = @province)
	
	SELECT canton FROM Canton C
	INNER JOIN CantonPorProvincia CPP
		ON C.idCanton = CPP.idCanton
	WHERE @idProvincia = CPP.idProvincia
END