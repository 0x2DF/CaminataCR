CREATE TRIGGER dbo.tgr_TipoDeCaminata_UPDATE
ON TipoDeCaminata
AFTER UPDATE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Modificación de tipo de caminata ' + nuevo.tipoDeCaminata, SYSTEM_USER, 'UPDATE', 'TipoDeCaminata'
		FROM inserted nuevo
			INNER JOIN deleted original
				ON nuevo.idTipoDeCaminata = original.idTipoDeCaminata

END