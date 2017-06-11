CREATE TRIGGER dbo.tgr_Regular_INSERT
ON Regular
AFTER INSERT
AS
BEGIN
	
	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Creación de usuario regular ' + nuevo.primerNombre + ' ' + nuevo.primerApellido + ' con el Id ' + CONVERT(NVARCHAR(MAX), nuevo.idUsuario), 
			   SYSTEM_USER, 'INSERT', 'Regular'
		FROM inserted nuevo

END