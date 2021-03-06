create procedure dbo.getTableByParam(
				@param nvarchar(50),
				@startDate nvarchar(50),
				@endDate nvarchar(50)
				)

as begin
declare @date nvarchar(100)=' where fechaHora between '+@startDate+' and '+@endDate+' ' 
declare @group nvarchar(50)=', '+@param+' '
declare @order nvarchar(50)=' group by '+@param+' '
declare @head nvarchar(max)='select sum(tabla.cantidadPuntos)cantidadPuntos,sum(tabla.likes)likes '
declare @sql nvarchar(max)=' from(
					select c.nombreDelLugar as caminata,count(c.idCaminata)cantidadRutas,sum(temp.likes)likes,isnull(temp3.cantidadPuntos,0)as cantidadPuntos,
					uc.idtipoDeCaminata,uc.idNivelDeCalidad,uc.idNivelDeDificultad,uc.idNivelDePrecio,uc.fechaHora
					from Caminata c inner join UsuarioPorCaminata uc on c.idCaminata=uc.idCaminata  inner join RutaPorUPC ruc on uc.idUsuarioPorCaminata=ruc.idUsuarioPorCaminata inner join
					(select uc.idUsuarioPorCaminata,count(l.idUsuarioPorCaminata)likes
					from UsuarioPorCaminata uc left join Likes l on uc.idUsuarioPorCaminata=l.idUsuarioPorCaminata
					group by uc.idUsuarioPorCaminata)temp on temp.idUsuarioPorCaminata=uc.idUsuarioPorCaminata left join (
					select ru.idRutaPorUPC,ru.idRuta,ru.idUsuarioPorCaminata,count(ru.idUsuarioPorCaminata)as cantidadPuntos 
					from RutaPorUPC ru inner join PuntosPorRPUPC puntosPorRuta on ru.idRutaPorUPC=puntosPorRuta.idRutaPorUPC
					group by ru.idRutaPorUPC,ru.idRuta,ru.idUsuarioPorCaminata
					)temp3 on temp3.idUsuarioPorCaminata=uc.idUsuarioPorCaminata
					group by c.nombreDelLugar,temp3.cantidadPuntos,uc.idtipoDeCaminata,uc.idNivelDeCalidad,uc.idNivelDeDificultad,uc.idNivelDePrecio,uc.fechaHora)tabla
					 '

execute(@head+@group+@sql+@order)

end