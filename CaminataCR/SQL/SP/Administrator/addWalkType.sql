CREATE PROCEDURE [dbo].[addWalkType]
	@walktype varchar(30),
	@state bit

AS
BEGIN

    SET NOCOUNT ON

    DECLARE @alreadyExists INT
	SET @alreadyExists = (SELECT COUNT(*) FROM TipoDeCaminata WHERE @walktype = tipoDeCaminata)

    IF @alreadyExists = 0
    BEGIN        
		INSERT INTO TipoDeCaminata ([tipoDeCaminata],[activo]) VALUES(@walkType,@state)
		RETURN(0)
    END
    ELSE
       RETURN(1)

END