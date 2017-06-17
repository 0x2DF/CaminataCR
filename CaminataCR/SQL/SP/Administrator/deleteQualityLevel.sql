CREATE PROCEDURE [dbo].[deleteQualityLevel]
	@quality varchar(30)
AS
BEGIN
	DELETE FROM NivelDeCalidad WHERE nivelDeCalidad = @quality
END