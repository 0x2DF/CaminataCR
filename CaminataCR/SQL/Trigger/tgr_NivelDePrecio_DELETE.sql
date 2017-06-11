CREATE TRIGGER dbo.tgr_NivelDePrecio_DELETE
ON NivelDePrecio
AFTER DELETE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Eliminación de nivel de precio ' + original.nivelDePrecio, SYSTEM_USER, 'DELETE', 'NivelDePrecio'
		FROM deleted original

END