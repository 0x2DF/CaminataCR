CREATE PROCEDURE dbo.removeLike
	@UserId INT,
	@HikeId INT
AS
BEGIN
	
	IF (SELECT count(*)  FROM dbo.Likes L WHERE @UserId = L.idUsuario AND @HikeId = L.idUsuarioPorCaminata) = 0
		RETURN(1)
	ELSE
		DELETE FROM dbo.Likes WHERE (idUsuario = @UserId) AND (idUsuarioPorCaminata = @HikeId)
		RETURN(0)
END