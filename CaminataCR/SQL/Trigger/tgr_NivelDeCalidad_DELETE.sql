CREATE TRIGGER dbo.tgr_NivelDeCalidad_DELETE
ON NivelDeCalidad
AFTER DELETE
AS
BEGIN

	INSERT INTO Bitacora(fechaHora, descripcion, idUsuario, tipoCambio, objeto)
		SELECT GETDATE(), 'Eliminaci�n de nivel de calidad ' + original.nivelDeCalidad, SYSTEM_USER, 'DELETE', 'NivelDeCalidad'
		FROM deleted original

END