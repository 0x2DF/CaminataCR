CREATE TRIGGER dbo.tgr_TipoDeCaminata_DELETE
ON TipoDeCaminata
AFTER DELETE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Eliminación de tipo de caminata ' + original.tipoDeCaminata, SYSTEM_USER, 'DELETE', 'TipoDeCaminata'
		FROM deleted original

END