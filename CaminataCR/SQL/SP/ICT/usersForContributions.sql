create procedure dbo.usersForContributions (
				@primerNombre as nvarchar(20)=null,
			    @primerApellido as nvarchar(20)=null,
				@ascOdes as nvarchar(4),
				@orderBy as nvarchar(20)
)
as begin

	declare @order nvarchar(100)='order by '+ @orderBy +' '+@ascOdes
	if  @primerNombre is null and @primerApellido is null 
     begin
		
		declare @sql as nvarchar(max)
		set @sql='select temp.idUsuario,temp.primerNombre,temp.primerApellido,temp.correo,temp.telefono,sum(cantidadCaminatas)as cantidadCaminatas,sum(cantidadPuntos)as 
		cantidadPuntos,sum(temp.Likes) as likes
		from
		(select r.idUsuario,r.primerNombre,r.primerApellido,r.correo,r.telefono,count(r.idUsuario)as cantidadCaminatas,isnull(temp.cantidadPuntos,0)as cantidadPuntos ,temp2.likes
		from Regular r inner join UsuarioPorCaminata uc on r.idUsuario=uc.idUsuario left join 
		(select ru.idRutaPorUPC,ru.idRuta,ru.idUsuarioPorCaminata,count(ru.idUsuarioPorCaminata)as cantidadPuntos 
		from RutaPorUPC ru inner join PuntosPorRPUPC puntosPorRuta on ru.idRutaPorUPC=puntosPorRuta.idRutaPorUPC
		group by ru.idRutaPorUPC,ru.idRuta,ru.idUsuarioPorCaminata)temp on uc.idUsuarioPorCaminata=temp.idUsuarioPorCaminata inner join 
		(select uc.idUsuarioPorCaminata,count(l.idUsuarioPorCaminata)likes
		from UsuarioPorCaminata uc left join Likes l on uc.idUsuarioPorCaminata=l.idUsuarioPorCaminata
		group by uc.idUsuarioPorCaminata)temp2 on uc.idUsuarioPorCaminata = temp2.idUsuarioPorCaminata
		group by r.idUsuario,r.primerNombre ,r.primerApellido,r.correo,r.telefono,temp.cantidadPuntos,temp2.likes)temp
		group by idUsuario,primerNombre,primerApellido,correo,telefono '


		execute(@sql+@order)

	 end
	else
	 begin 
		create Table #tablaTemp (
			idUsuario int,
			primerNombre varchar(20),
			primerApellido varchar(20),
			correo varchar(50),
			telefono varchar(14),
			cantidadCaminatas int,
			cantidadPuntos int,
			cantidadLikes int
	    );
		    insert into #tablaTemp
				    select temp.idUsuario,temp.primerNombre,temp.primerApellido,temp.correo,temp.telefono,sum(cantidadCaminatas)as cantidadCaminatas,sum(cantidadPuntos)as 
					cantidadPuntos,sum(temp.Likes) as likes
					from
					(select r.idUsuario,r.primerNombre,r.primerApellido,r.correo,r.telefono,count(r.idUsuario)as cantidadCaminatas,isnull(temp.cantidadPuntos,0)as cantidadPuntos ,temp2.likes
					from Regular r inner join UsuarioPorCaminata uc on r.idUsuario=uc.idUsuario left join 
					(select ru.idRutaPorUPC,ru.idRuta,ru.idUsuarioPorCaminata,count(ru.idUsuarioPorCaminata)as cantidadPuntos 
					from RutaPorUPC ru inner join PuntosPorRPUPC puntosPorRuta on ru.idRutaPorUPC=puntosPorRuta.idRutaPorUPC
					group by ru.idRutaPorUPC,ru.idRuta,ru.idUsuarioPorCaminata)temp on uc.idUsuarioPorCaminata=temp.idUsuarioPorCaminata inner join 
					(select uc.idUsuarioPorCaminata,count(l.idUsuarioPorCaminata)likes
					from UsuarioPorCaminata uc left join Likes l on uc.idUsuarioPorCaminata=l.idUsuarioPorCaminata
					group by uc.idUsuarioPorCaminata)temp2 on uc.idUsuarioPorCaminata = temp2.idUsuarioPorCaminata
					group by r.idUsuario,r.primerNombre ,r.primerApellido,r.correo,r.telefono,temp.cantidadPuntos,temp2.likes)temp
					group by idUsuario,primerNombre,primerApellido,correo,telefono

			if @primerNombre is not null and @primerApellido is not null
			begin 
			declare @head nvarchar(max) = N'select* from #tablaTemp tp where tp.primerNombre=@primerNombre1 and tp.primerApellido=@primerApellido1 '+@order
			
			exec sp_executesql @statement = @head,
							   @params=N'@primerNombre1  nvarchar(20),@primerApellido1 nvarchar(20)',
							   @primerNombre1=@primerNombre,@primerApellido1=@primerApellido
			
			
			end

			if @primerNombre is not null and @primerApellido is null
			begin 
			declare @head1 nvarchar(max) = N'select* from #tablaTemp tp where tp.primerNombre=@primerNombre1 '+@order
			exec sp_executesql @statement = @head1,
							   @params=N'@primerNombre1  nvarchar(20)',
							   @primerNombre1=@primerNombre
			
			 end


			if @primerApellido is not null and @primerNombre is null
			begin 
			
			declare @head2 nvarchar(max) = N'select* from #tablaTemp tp where tp.primerApellido=@primerApellido1 '+@order
			exec sp_executesql @statement = @head2,
							   @params=N'@primerApellido1 nvarchar(20)',
							   @primerApellido1=@primerApellido
			
			end
	 
	 end
end