CREATE PROCEDURE [dbo].getActiveHikeTypes
AS
BEGIN
	SELECT tipoDeCaminata FROM TipoDeCaminata WHERE activo = 1
END