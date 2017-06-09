CREATE PROCEDURE dbo.getLikeStatus
	@UserId INT,
	@HikeId INT
AS
BEGIN
	IF (SELECT count(*)  FROM dbo.Likes L WHERE @UserId = L.idUsuario AND @HikeId = L.idUsuarioPorCaminata) <> 0
		RETURN(1)
	ELSE
		RETURN(0)
END