CREATE PROCEDURE dbo.addLike
	@UserId INT,
	@HikeId INT
AS
BEGIN
	
	IF (SELECT count(*)  FROM dbo.Likes L WHERE @UserId = L.idUsuario AND @HikeId = L.idUsuarioPorCaminata) <> 0
		RETURN(1)
	ELSE
		INSERT INTO dbo.Likes (idUsuarioPorCaminata, idUsuario, fechaHora)
        VALUES (@HikeId, @UserId, GETDATE())
		RETURN(0)
END