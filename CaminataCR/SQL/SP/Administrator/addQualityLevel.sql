CREATE PROCEDURE [dbo].[addQualityLevel]
	@qualityLevel varchar(30),
	@state bit

AS
BEGIN

    SET NOCOUNT ON

    DECLARE @alreadyExists INT
	SET @alreadyExists = (SELECT COUNT(*) FROM NivelDeCalidad WHERE @qualityLevel = nivelDeCalidad)

    IF @alreadyExists = 0
    BEGIN        
		INSERT INTO NivelDeCalidad ([nivelDeCalidad],[activo]) VALUES(@qualityLevel,@state)
		RETURN(0)
    END
    ELSE
       RETURN(1)

END