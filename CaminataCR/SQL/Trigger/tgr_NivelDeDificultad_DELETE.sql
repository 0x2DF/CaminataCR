CREATE TRIGGER dbo.tgr_NivelDeDificultad_DELETE
ON NivelDeDificultad
AFTER DELETE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Eliminación de nivel de dificultad ' + original.nivelDeDificultad, SYSTEM_USER, 'DELETE', 'NivelDeDificultad'
		FROM deleted original

END