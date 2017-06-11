CREATE TRIGGER dbo.tgr_NivelDePrecio_INSERT
ON NivelDePrecio
AFTER INSERT
AS
BEGIN
	
	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Creaci�n de nivel de precio ' + nuevo.nivelDePrecio, SYSTEM_USER, 'INSERT', 'NivelDePrecio'
		FROM inserted nuevo

END