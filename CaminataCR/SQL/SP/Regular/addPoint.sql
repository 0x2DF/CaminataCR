CREATE PROCEDURE dbo.addPoint
    @idRutaPorUPC INT,
	@Longitud FLOAT,
	@Latitud FLOAT,
	@Pos INT,
	@Comment NVARCHAR(500),
	@image VARBINARY(max) = NULL

AS
BEGIN
    SET NOCOUNT ON

	DECLARE @ReturnVal INT

	INSERT INTO PuntosImportantes (longitud, latitud)
	VALUES (@Longitud, @Latitud)
	Set @ReturnVal = SCOPE_IDENTITY()

	INSERT INTO PuntosPorRPUPC(idRutaPorUPC, idPuntosImportantes, posicion, comentario, fotografia)
	VALUES (@idRutaPorUPC, @ReturnVal, @Pos, @Comment, @image)
	Set @ReturnVal = SCOPE_IDENTITY()

    RETURN @ReturnVal
END