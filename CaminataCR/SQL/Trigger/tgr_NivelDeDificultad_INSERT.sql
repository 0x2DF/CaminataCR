CREATE TRIGGER dbo.tgr_NivelDeDificultad_INSERT
ON NivelDeDificultad
AFTER INSERT
AS
BEGIN
	
	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Creación de nivel de dificultad ' + nuevo.nivelDeDificultad, SYSTEM_USER, 'INSERT', 'NivelDeDificultad'
		FROM inserted nuevo

END