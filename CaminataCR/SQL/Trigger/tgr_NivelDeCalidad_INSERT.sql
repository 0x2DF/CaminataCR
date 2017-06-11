CREATE TRIGGER dbo.tgr_NivelDeCalidad_INSERT
ON NivelDeCalidad
AFTER INSERT
AS
BEGIN
	
	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Creación de nivel de calidad ' + nuevo.nivelDeCalidad, SYSTEM_USER, 'INSERT', 'NivelDeCalidad'
		FROM inserted nuevo

END