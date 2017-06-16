CREATE PROCEDURE [dbo].[deleteWalkType]
	@walkType varchar(30)
AS
BEGIN
	DELETE FROM TipoDeCaminata WHERE tipoDeCaminata = @walkType
END