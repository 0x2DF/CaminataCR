CREATE PROCEDURE dbo.getFilteredHikes
    @NameOfLocation NVARCHAR(20) = NULL,
	@Province NVARCHAR(30) = NULL,
	@Canton NVARCHAR(30) = NULL,
	@District NVARCHAR(30) = NULL,
	@Longitud FLOAT = NULL,
	@Latitud FLOAT = NULL,
	@HikeType NVARCHAR(30) = NULL,
	@Price NVARCHAR(30) = NULL,
	@Quality NVARCHAR(30) = NULL,
	@Difficulty NVARCHAR(30) = NULL
AS
BEGIN
	
	DECLARE @idProvincia  INT
	IF @Province IS NULL
		SET @idProvincia = (SELECT idProvincia FROM dbo.Provincia WHERE provincia = @Province)
	ELSE
		SET @idProvincia = NULL

	DECLARE @idCanton  INT
	IF @Canton IS NULL
		SET @idCanton = (SELECT idCanton FROM dbo.Canton WHERE canton = @Canton)
	ELSE
		SET @idCanton = NULL

	DECLARE @idDistrito  INT
	IF @District IS NULL
		SET @idDistrito = (SELECT idDistrito FROM dbo.Distrito WHERE distrito = @District)
	ELSE
		SET @idDistrito = NULL

	DECLARE @idTipoDeCaminata  INT
	IF @HikeType IS NULL
		SET @idTipoDeCaminata = (SELECT idTipoDeCaminata FROM dbo.TipoDeCaminata WHERE tipoDeCaminata = @HikeType)
	ELSE
		SET @idTipoDeCaminata = NULL

	DECLARE @idNivelDePrecio  INT
	IF @Price IS NULL
		SET @idNivelDePrecio = (SELECT idNivelDePrecio FROM dbo.NivelDePrecio WHERE nivelDePrecio = @Price)
	ELSE
		SET @idNivelDePrecio = NULL

	DECLARE @idNivelDeCalidad  INT
	IF @Quality IS NULL
		SET @idNivelDeCalidad = (SELECT idNivelDeCalidad FROM dbo.NivelDeCalidad WHERE nivelDeCalidad = @Quality)
	ELSE
		SET @idNivelDeCalidad = NULL

	DECLARE @idNivelDeDificultad  INT
	IF @Difficulty IS NULL
		SET @idNivelDeDificultad = (SELECT idNivelDeDificultad FROM dbo.NivelDeDificultad WHERE nivelDeDificultad = @Difficulty)
	ELSE
		SET @idNivelDeDificultad = NULL
	

    SELECT DISTINCT C.idCaminata, C.nombreDelLugar, C.latitud, C.longitud
	FROM Caminata C
	INNER JOIN UsuarioPorCaminata UPC
		ON C.idCaminata = UPC.idCaminata
	WHERE (
			((@NameOfLocation IS NULL) OR (C.nombreDelLugar LIKE @NameOfLocation)) AND
			((@idProvincia IS NULL) OR (C.idProvincia = @idProvincia)) AND
			((@idCanton IS NULL) OR (C.idCanton = @idCanton)) AND
			((@idDistrito IS NULL) OR (C.idDistrito = @idDistrito)) AND
			((@Longitud IS NULL) OR (C.longitud = @Longitud)) AND
			((@Latitud IS NULL) OR (C.latitud = @Latitud)) AND
			((@idTipoDeCaminata IS NULL) OR (UPC.idtipoDeCaminata = @idTipoDeCaminata)) AND
			((@idNivelDeCalidad IS NULL) OR (UPC.idNivelDeCalidad = @idNivelDeCalidad)) AND
			((@idNivelDeDificultad IS NULL) OR (UPC.idNivelDeDificultad = @idNivelDeDificultad)) AND
			((@idNivelDePrecio IS NULL) OR (UPC.idNivelDePrecio = @idNivelDePrecio))
		)
END