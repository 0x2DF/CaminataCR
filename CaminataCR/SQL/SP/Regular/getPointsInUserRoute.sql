CREATE PROCEDURE dbo.getPointsInUserRoute
    @idRutaPorUPC INT
AS
BEGIN

    SELECT PI.latitud, PI.longitud, PPRPUPC.posicion, PPRPUPC.comentario, PPRPUPC.fotografia
	FROM PuntosPorRPUPC PPRPUPC
	INNER JOIN PuntosImportantes PI
		ON PPRPUPC.idPuntosImportantes = PI.idPuntosImportantes
	WHERE PPRPUPC.idRutaPorUPC = @idRutaPorUPC
END