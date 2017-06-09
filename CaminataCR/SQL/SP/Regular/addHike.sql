CREATE PROCEDURE dbo.addHike
    @UserId INT,
    @NameOfLocation NVARCHAR(20),
	@Province NVARCHAR(30),
	@Canton NVARCHAR(30),
	@District NVARCHAR(30),
	@Details NVARCHAR(500),
	@Longitud FLOAT,
	@Latitud FLOAT,
	@HikeType NVARCHAR(30),
	@Price NVARCHAR(30),
	@Quality NVARCHAR(30),
	@Difficulty NVARCHAR(30),
	@image VARBINARY(max) = NULL,
	@Comment NVARCHAR(500)

AS
BEGIN
    SET NOCOUNT ON

	DECLARE @idProvincia  INT
	SET @idProvincia = (SELECT idProvincia FROM dbo.Provincia WHERE provincia = @Province)

	DECLARE @idCanton  INT
	SET @idCanton = (SELECT idCanton FROM dbo.Canton WHERE canton = @Canton)

	DECLARE @idDistrito  INT
	SET @idDistrito = (SELECT idDistrito FROM dbo.Distrito WHERE distrito = @District)

	DECLARE @idTipoDeCaminata  INT
	SET @idTipoDeCaminata = (SELECT idTipoDeCaminata FROM dbo.TipoDeCaminata WHERE tipoDeCaminata = @HikeType)

	DECLARE @idNivelDePrecio  INT
	SET @idNivelDePrecio = (SELECT idNivelDePrecio FROM dbo.NivelDePrecio WHERE nivelDePrecio = @Price)

	DECLARE @idNivelDeCalidad  INT
	SET @idNivelDeCalidad = (SELECT idNivelDeCalidad FROM dbo.NivelDeCalidad WHERE nivelDeCalidad = @Quality)

	DECLARE @idNivelDeDificultad  INT
	SET @idNivelDeDificultad = (SELECT idNivelDeDificultad FROM dbo.NivelDeDificultad WHERE nivelDeDificultad = @Difficulty)

	DECLARE @ReturnVal INT

	INSERT INTO Caminata (nombreDelLugar, idProvincia, idCanton, idDistrito, detalle, longitud, latitud)
	VALUES (@NameOfLocation, @idProvincia, @idCanton, @idDistrito, @Details, @Longitud, @Latitud)
	Set @ReturnVal = SCOPE_IDENTITY()

	INSERT INTO UsuarioPorCaminata (idUsuario, idCaminata, fechaHora, idtipoDeCaminata, idNivelDeDificultad, idNivelDePrecio, idNivelDeCalidad, fotografia, comentario)
	VALUES (@UserId, @ReturnVal, GETDATE(), @idTipoDeCaminata, @idNivelDeDificultad, @idNivelDePrecio, @idNivelDeCalidad, @image, @Comment)
	Set @ReturnVal = SCOPE_IDENTITY()

    RETURN @ReturnVal
END