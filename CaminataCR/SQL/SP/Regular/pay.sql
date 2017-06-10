CREATE PROCEDURE dbo.pay
AS
BEGIN

	DECLARE @totalDonation INT
	SELECT @totalDonation = SUM(D.monto)
	FROM Donacion D
	WHERE CONVERT(DATE, D.fechaHora) = CONVERT(DATE, GETDATE())

	SET @totalDonation *= 0.1

	DECLARE @totalLikes INT
	SELECT @totalLikes = COUNT(*)
	FROM Likes L
	WHERE CONVERT(DATE, L.fechaHora) = CONVERT(DATE, GETDATE())

	INSERT INTO CierreDiario(idUsuario, monto, fechaHora)
		SELECT R.idUsuario, @totalDonation * ((Hiker.Cantidad * 100) / @totalLikes) / 100, CONVERT(DATE, GETDATE())
		FROM Regular R
			INNER JOIN (
				SELECT UC.idUsuario, COUNT(*) Cantidad
				FROM Likes L, UsuarioPorCaminata UC
				WHERE L.idUsuarioPorCaminata = UC.idUsuarioPorCaminata AND
					  CONVERT(DATE, L.fechaHora) = CONVERT(DATE, GETDATE())
				GROUP BY UC.idUsuario
			) Hiker
				ON Hiker.idUsuario = R.idUsuario
END