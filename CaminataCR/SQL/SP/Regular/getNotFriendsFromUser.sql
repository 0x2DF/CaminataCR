CREATE PROCEDURE dbo.getNotFriendsFromUser
    @UserId INT,
	@nombre NVARCHAR(20)
AS
BEGIN

    SELECT R.idUsuario, R.primerNombre, R.segundoNombre, R.primerApellido, R.segundoApellido, R.fotografia
	FROM Regular R
	INNER JOIN Usuario U
	ON R.idUsuario = U.idUsuario
	LEFT JOIN (
		SELECT idUsuario2 AS idAmigos FROM Amigos WHERE idUsuario1 = @UserId
		UNION (SELECT idUsuario1 AS idAmigos FROM Amigos WHERE idUsuario2 = @UserId)
				) amistades
	ON R.idUsuario = amistades.idAmigos
	WHERE (amistades.idAmigos IS NULL) AND 
		((R.primerNombre LIKE '%' + @nombre + '%') OR (R.primerApellido LIKE '%' + @nombre + '%') OR (U.cuenta LIKE '%' + @nombre + '%')) AND
		(R.idUsuario <> @UserId)
END