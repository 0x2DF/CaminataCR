create procedure dbo.remunerationsReport (
				 @top int =1,
				 @startDateP varchar(50),
				 @endDateP varchar(50)
)
as
begin

declare @sqlHead nvarchar(max) = N'select top '+ convert(nvarchar(3),@top) +
								' CONCAT(r.primerNombre,@space,r.segundoNombre,@space,primerApellido,@space,segundoApellido)nombreCompleto,temp.idUsuario,temp.total'

declare @sql nvarchar(max) = @sqlHead +   ' from Regular r inner join
											(select idUsuario,sum(monto)total
											from CierreDiario cd 
											where fechaHora between @startDate and @endDate
											group by idUsuario)temp on r.idUsuario = temp.idUsuario
											order by total desc'

exec sp_executesql @statement =@sql,
				   @params=N'@space varchar(1),@startDate varchar(50),@endDate varchar(50)',
				   @space=' ',@startDate=@startDateP,@endDate=@endDateP


end