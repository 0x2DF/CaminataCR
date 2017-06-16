CREATE PROCEDURE [dbo].[addPriceLevel]
	@priceLevel varchar(30),
	@state bit
AS
BEGIN

    SET NOCOUNT ON

    DECLARE @alreadyExists INT
	SET @alreadyExists = (SELECT COUNT(*) FROM NivelDePrecio WHERE @priceLevel = nivelDePrecio)

    IF @alreadyExists = 0
    BEGIN        
		INSERT INTO NivelDePrecio ([nivelDePrecio],[activo]) VALUES(@priceLevel,@state)
		RETURN(0)
    END
    ELSE
       RETURN(1)

END