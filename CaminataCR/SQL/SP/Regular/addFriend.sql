CREATE PROCEDURE dbo.addFriend
	@UserId INT,
	@FriendId INT
AS
BEGIN
	
	IF (SELECT count(*)  FROM dbo.Amigos A WHERE @UserId = A.idUsuario2 AND @FriendId = A.idUsuario1 OR
												 @UserId = A.idUsuario1 AND @FriendId = A.idUsuario2) <> 0
    
		RETURN(1)
	ELSE
		INSERT INTO dbo.Amigos (idUsuario1, idUsuario2)
        VALUES (@UserId, @FriendId)

		RETURN(0)
END