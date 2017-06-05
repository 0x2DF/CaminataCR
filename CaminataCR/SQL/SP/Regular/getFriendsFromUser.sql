CREATE PROCEDURE dbo.getFriendsFromUser
    @UserId INT
AS
BEGIN

    SELECT R.idUsuario, R.primerNombre, R.segundoNombre, R.primerApellido, R.segundoApellido, R.fotografia
	FROM Regular R
	INNER JOIN (
		SELECT idUsuario2 AS idAmigos FROM Amigos WHERE idUsuario1 = @UserId
		UNION (SELECT idUsuario1 AS idAmigos FROM Amigos WHERE idUsuario2 = @UserId)
				) amistades
	ON R.idUsuario = amistades.idAmigos
END