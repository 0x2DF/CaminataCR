﻿CREATE PROCEDURE [dbo].getHikeInfo
	@idCaminata int
AS
BEGIN
	SELECT C.nombreDelLugar, P.provincia, Ca.canton, D.distrito, C.detalle, C.latitud, C.longitud FROM Caminata C
	INNER JOIN Provincia P
		ON C.idProvincia = P.idProvincia
	INNER JOIN Canton Ca
		ON C.idCanton = Ca.idCanton
	INNER JOIN Distrito D
		ON C.idDistrito = D.idDistrito
	WHERE idCaminata = @idCaminata
END