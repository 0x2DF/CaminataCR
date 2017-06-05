CREATE PROCEDURE dbo.removeFriend
	@UserId INT,
	@FriendId INT
AS
BEGIN
	
	IF (SELECT count(*)  FROM dbo.Amigos A WHERE @UserId = A.idUsuario2 AND @FriendId = A.idUsuario1 OR
												 @UserId = A.idUsuario1 AND @FriendId = A.idUsuario2) = 0
		RETURN(1)
	ELSE
		DELETE FROM dbo.Amigos WHERE (idUsuario1 = @UserId AND idUsuario2 = @FriendId) OR (idUsuario1 = @FriendId AND idUsuario2 = @UserId)
		RETURN(0)
END