CREATE TRIGGER dbo.tgr_NivelDePrecio_UPDATE
ON NivelDePrecio
AFTER UPDATE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Modificación de nivel de precio ' + nuevo.nivelDePrecio, SYSTEM_USER, 'UPDATE', 'NivelDePrecio'
		FROM inserted nuevo
			INNER JOIN deleted original
				ON nuevo.idNivelDePrecio = original.idNivelDePrecio

END