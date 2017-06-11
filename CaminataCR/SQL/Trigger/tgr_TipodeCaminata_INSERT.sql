CREATE TRIGGER dbo.tgr_TipodeCaminata_INSERT
ON TipodeCaminata
AFTER INSERT
AS
BEGIN
	
	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Creación de tipo de caminata ' + nuevo.tipoDeCaminata, SYSTEM_USER, 'INSERT', 'TipodeCaminata'
		FROM inserted nuevo

END