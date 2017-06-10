CREATE PROCEDURE dbo.addRouteWithRouteId
    @UserPerHikeId INT,
	@RouteId INT
AS
BEGIN
    SET NOCOUNT ON
	DECLARE @ReturnVal INT

	INSERT INTO RutaPorUPC(idRuta, idUsuarioPorCaminata)
	VALUES (@RouteId, @UserPerHikeId)

	Set @ReturnVal = SCOPE_IDENTITY()

    RETURN @ReturnVal
END