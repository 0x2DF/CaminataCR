CREATE PROCEDURE [dbo].getDifficultyLevels
AS
BEGIN
	SELECT nivelDeDificultad FROM NivelDeDificultad WHERE activo = 1
END