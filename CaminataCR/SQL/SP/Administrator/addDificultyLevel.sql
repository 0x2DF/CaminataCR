CREATE PROCEDURE [dbo].[addDificultyLevel]
	@dificultyLevel varchar(30),
	@state bit

AS
BEGIN

    SET NOCOUNT ON

    DECLARE @alreadyExists INT
	SET @alreadyExists = (SELECT COUNT(*) FROM NivelDeDificultad WHERE @dificultyLevel = nivelDeDificultad)

    IF @alreadyExists = 0
    BEGIN        
		INSERT INTO NivelDeDificultad ([nivelDeDificultad],[activo]) VALUES(@dificultyLevel,@state)
		RETURN(0)
    END
    ELSE
       RETURN(1)

END