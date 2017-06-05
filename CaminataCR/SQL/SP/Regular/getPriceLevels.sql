CREATE PROCEDURE [dbo].getPriceLevels
AS
BEGIN
	SELECT nivelDePrecio FROM NivelDePrecio WHERE activo = 1
END