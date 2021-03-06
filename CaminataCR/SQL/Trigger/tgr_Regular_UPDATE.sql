CREATE TRIGGER dbo.tgr_Regular_UPDATE
ON Regular
AFTER UPDATE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Modificación del usuario regular ' + nuevo.primerNombre + ' ' + nuevo.primerApellido + ' con el Id ' + CONVERT(NVARCHAR(MAX), nuevo.idUsuario),
			   SYSTEM_USER, 'UPDATE', 'Regular'
		FROM inserted nuevo
			INNER JOIN deleted original
				ON nuevo.idUsuario = original.idUsuario

END