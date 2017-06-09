CREATE PROCEDURE [dbo].getRouteInfo
	@idCaminata int
AS
BEGIN
	SELECT DISTINCT R.idRuta FROM Ruta R
	INNER JOIN RutaPorUPC RPUPC
		ON R.idRuta = RPUPC.idRuta
	INNER JOIN UsuarioPorCaminata UPC
		ON RPUPC.idUsuarioPorCaminata = UPC.idUsuarioPorCaminata
	WHERE UPC.idCaminata = @idCaminata
END