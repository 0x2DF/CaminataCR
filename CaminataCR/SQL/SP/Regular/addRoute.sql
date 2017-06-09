CREATE PROCEDURE dbo.addRoute
    @UserPerHikeId INT
AS
BEGIN
    SET NOCOUNT ON
	DECLARE @ReturnVal INT

	INSERT INTO Ruta DEFAULT VALUES

	Set @ReturnVal = SCOPE_IDENTITY()

	INSERT INTO RutaPorUPC(idRuta, idUsuarioPorCaminata)
	VALUES (@ReturnVal, @UserPerHikeId)

	Set @ReturnVal = SCOPE_IDENTITY()

    RETURN @ReturnVal
END