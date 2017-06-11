CREATE TRIGGER dbo.tgr_NivelDeDificultad_UPDATE
ON NivelDeDificultad
AFTER UPDATE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Modificación de nivel de dificultad ' + nuevo.nivelDeDificultad, SYSTEM_USER, 'UPDATE', 'NivelDeDificultad'
		FROM inserted nuevo
			INNER JOIN deleted original
				ON nuevo.idUsuario = original.idUsuario

END