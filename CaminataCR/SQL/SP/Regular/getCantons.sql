CREATE PROCEDURE [dbo].getCantons
	@province NVARCHAR(30)
AS
BEGIN
	
	DECLARE @idProvincia INT
	SET @idProvincia = (SELECT idProvincia FROM Provincia WHERE provincia = @province)
	
	SELECT canton FROM Canton
	INNER JOIN CantonPorProvincia
		ON @idProvincia = idProvincia
END