CREATE TRIGGER dbo.tgr_Usuario_INSERT
ON Usuario
AFTER INSERT
AS
BEGIN
	
	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Creación de usuario regular ' + nuevo.cuenta + ' ' + ' con Id ' + CONVERT(NVARCHAR(MAX), nuevo.idUsuario), 
			   nuevo.idUsuario, 'INSERT', 'Usuario'
		FROM inserted nuevo

END