CREATE PROCEDURE [dbo].[deleteDificultyLevel]
	@dificulty varchar(30)
AS
BEGIN
	DELETE FROM NivelDeDificultad WHERE nivelDeDificultad = @dificulty
END