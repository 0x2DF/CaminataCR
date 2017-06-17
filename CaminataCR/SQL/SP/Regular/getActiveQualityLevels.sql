CREATE PROCEDURE [dbo].getActiveQualityLevels
AS
BEGIN
	SELECT nivelDeCalidad FROM NivelDeCalidad WHERE activo = 1
END