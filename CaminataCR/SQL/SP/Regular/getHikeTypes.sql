CREATE PROCEDURE [dbo].getHikeTypes
AS
BEGIN
	SELECT tipoDeCaminata FROM TipoDeCaminata WHERE activo = 1
END