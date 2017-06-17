CREATE PROCEDURE [dbo].getActivePriceLevels
AS
BEGIN
	SELECT nivelDePrecio FROM NivelDePrecio WHERE activo = 1
END