CREATE TRIGGER dbo.tgr_Regular_DELETE
ON Regular
AFTER DELETE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Creaci�n de usuario regular ' + original.primerNombre + ' ' + original.primerApellido + ' con Id ' + CONVERT(NVARCHAR(MAX), original.idUsuario), 
			   original.idUsuario, 'INSERT', 'Regular'
		FROM deleted original

END