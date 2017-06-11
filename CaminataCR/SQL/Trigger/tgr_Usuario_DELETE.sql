CREATE TRIGGER dbo.tgr_Usuario_DELETE
ON Usuario
AFTER DELETE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Eliminación de usuario ' + original.cuenta + ' con el Id ' + CONVERT(NVARCHAR(MAX), original.idUsuario), 
			   SYSTEM_USER, 'DELETE', 'Usuario'
		FROM deleted original

END