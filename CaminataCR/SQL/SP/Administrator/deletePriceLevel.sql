CREATE PROCEDURE [dbo].[deletePriceLevel]
	@price varchar(30)
AS
BEGIN
	DELETE FROM NivelDePrecio WHERE nivelDePrecio = @price
END