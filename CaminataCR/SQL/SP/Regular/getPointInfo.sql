CREATE PROCEDURE [dbo].getPointInfo
	@idRuta int
AS
BEGIN
	SELECT DISTINCT PI.idPuntosImportantes, PPRPUPC.posicion, PI.longitud, PI.latitud FROM PuntosImportantes PI
	INNER JOIN PuntosPorRPUPC PPRPUPC
		ON PI.idPuntosImportantes = PPRPUPC.idPuntosImportantes
	INNER JOIN RutaPorUPC RPUPC
		ON PPRPUPC.idRutaPorUPC = RPUPC.idRutaPorUPC
	WHERE RPUPC.idRuta = @idRuta
	ORDER BY PPRPUPC.posicion
END