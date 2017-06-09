CREATE PROCEDURE dbo.pay
AS
BEGIN

	DECLARE @totalDonation INT

	SELECT @totalDonation = SUM(D.monto)
	FROM Donacion D
	WHERE CONVERT(DATE, D.fechaHora) = CONVERT(DATE, GETDATE())

	SET @totalDonation *= 0.1

	INSERT INTO CierreDiario(idUsuario, monto, fechaHora)
		SELECT U.idUsuario, @totalDonation * ((Hiker.Cantidad * 100) / @totalDonation) / 100, CONVERT(DATE, GETDATE())
		FROM Usuario U
			INNER JOIN (
				SELECT UC.idUsuario, COUNT(*) Cantidad
				FROM Likes L, UsuarioPorCaminata UC
				WHERE L.idUsuarioPorCaminata = UC.idUsuarioPorCaminata AND
					  CONVERT(DATE, L.fechaHora) = CONVERT(DATE, GETDATE())
				GROUP BY UC.idUsuario
			) Hiker
				ON Hiker.idUsuario = U.idUsuario
END