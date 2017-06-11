CREATE TRIGGER dbo.tgr_Regular_DELETE
ON Regular
AFTER DELETE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Eliminación de usuario regular ' + original.primerNombre + ' ' + original.primerApellido + ' con el Id ' + CONVERT(NVARCHAR(MAX), original.idUsuario), 
			   SYSTEM_USER, 'DELETE', 'Regular'
		FROM deleted original

END