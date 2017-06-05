CREATE PROCEDURE [dbo].getQualityLevels
AS
BEGIN
	SELECT nivelDeCalidad FROM NivelDeCalidad WHERE activo = 1
END