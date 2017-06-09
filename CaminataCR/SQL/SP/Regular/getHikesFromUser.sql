CREATE PROCEDURE dbo.getHikesFromUser
    @UserId INT
AS
BEGIN

    SELECT C.nombreDelLugar, P.provincia, Ca.canton, D.distrito, C.detalle, C.latitud, C.latitud, 
			UPC.fechaHora, NC.nivelDeCalidad, ND.nivelDeDificultad, NP.nivelDePrecio, TC.tipoDeCaminata,
			UPC.fotografia, UPC.comentario, UPC.idUsuarioPorCaminata, RPUPC.idRutaPorUPC
	FROM Caminata C
	INNER JOIN Provincia P
		ON C.idProvincia = P.idProvincia
	INNER JOIN Canton Ca
		ON C.idCanton = Ca.idCanton
	INNER JOIN Distrito D
		ON C.idDistrito = D.idDistrito
	INNER JOIN UsuarioPorCaminata UPC
		ON C.idCaminata = UPC.idCaminata
	INNER JOIN NivelDeCalidad NC
		ON UPC.idNivelDeCalidad = NC.idNivelDeCalidad
	INNER JOIN NivelDeDificultad ND
		ON UPC.idNivelDeDificultad = ND.idNivelDeDificultad
	INNER JOIN NivelDePrecio NP
		ON UPC.idNivelDePrecio = NP.idNivelDePrecio
	INNER JOIN TipoDeCaminata TC
		ON UPC.idtipoDeCaminata = TC.idTipoDeCaminata
	INNER JOIN RutaPorUPC RPUPC
		ON UPC.idUsuarioPorCaminata = RPUPC.idUsuarioPorCaminata
	WHERE (UPC.idUsuario = @UserId) AND (UPC.fechaHora > DATEADD(month, -1, GETDATE()) )
END