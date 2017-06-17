CREATE PROCEDURE [dbo].getActiveDifficultyLevels
AS
BEGIN
	SELECT nivelDeDificultad FROM NivelDeDificultad WHERE activo = 1
END