CREATE TRIGGER dbo.tgr_Usuario_INSERT
ON Usuario
AFTER INSERT
AS
BEGIN
	
	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Creación de usuario ' + nuevo.cuenta + ' ' + ' con el Id ' + CONVERT(NVARCHAR(MAX), nuevo.idUsuario), 
			   SYSTEM_USER, 'INSERT', 'Usuario'
		FROM inserted nuevo

END