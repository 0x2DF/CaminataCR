CREATE TRIGGER dbo.tgr_Usuario_DELETE
ON Usuario
AFTER DELETE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Creaci�n de usuario regular ' + original.cuenta + ' con Id ' + CONVERT(NVARCHAR(MAX), original.idUsuario), 
			   original.idUsuario, 'DELETE', 'Regular'
		FROM deleted original

END