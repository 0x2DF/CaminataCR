CREATE TRIGGER dbo.tgr_Usuario_UPDATE
ON Usuario
AFTER UPDATE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Modificación del usuario ' + nuevo.cuenta + ' con el Id ' + CONVERT(NVARCHAR(MAX), nuevo.idUsuario),
			   SYSTEM_USER, 'UPDATE', 'Usuario'
		FROM inserted nuevo
			INNER JOIN deleted original
				ON nuevo.idUsuario = original.idUsuario

END