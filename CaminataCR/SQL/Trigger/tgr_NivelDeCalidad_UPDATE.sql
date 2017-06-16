CREATE TRIGGER dbo.tgr_NivelDeCalidad_UPDATE
ON NivelDeCalidad
AFTER UPDATE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Modificación de nivel de calidad ' + nuevo.nivelDeCalidad, SYSTEM_USER, 'UPDATE', 'NivelDeCalidad'
		FROM inserted nuevo
			INNER JOIN deleted original
				ON nuevo.idNivelDeCalidad = original.idNivelDeCalidad

END