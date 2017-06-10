CREATE PROCEDURE dbo.addPointWithPointId
    @idRutaPorUPC INT,
	@idPuntosImportantes INT,
	@Pos INT,
	@Comment NVARCHAR(500),
	@image VARBINARY(max) = NULL

AS
BEGIN
    SET NOCOUNT ON

	DECLARE @ReturnVal INT

	INSERT INTO PuntosPorRPUPC(idRutaPorUPC, idPuntosImportantes, posicion, comentario, fotografia)
	VALUES (@idRutaPorUPC, @idPuntosImportantes, @Pos, @Comment, @image)
	Set @ReturnVal = SCOPE_IDENTITY()

    RETURN @ReturnVal
END