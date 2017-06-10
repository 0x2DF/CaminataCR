CREATE PROCEDURE [dbo].getHikeInfoWithRoute
	@idRuta int
AS
BEGIN
	SELECT DISTINCT C.idCaminata, C.latitud, C.longitud, C.nombreDelLugar, P.provincia, Ca.canton, D.distrito, C.detalle FROM Caminata C
	INNER JOIN Provincia P
		ON C.idProvincia = P.idProvincia
	INNER JOIN Canton Ca
		ON C.idCanton = Ca.idCanton
	INNER JOIN Distrito D
		ON C.idDistrito = D.idDistrito
	INNER JOIN UsuarioPorCaminata UPC
		ON C.idCaminata = UPC.idCaminata
	INNER JOIN RutaPorUPC RPUPC
		ON UPC.idUsuarioPorCaminata = RPUPC.idUsuarioPorCaminata
	WHERE RPUPC.idRuta = @idRuta
END