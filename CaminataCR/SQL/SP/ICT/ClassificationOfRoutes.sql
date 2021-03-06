create procedure dbo.ClassificationOfRoutes
as begin
select c.nombreDelLugar as caminata,count(c.idCaminata)cantidadRutas,sum(temp.likes)likes
from Caminata c inner join UsuarioPorCaminata uc on c.idCaminata=uc.idCaminata  inner join RutaPorUPC ruc on uc.idUsuarioPorCaminata=ruc.idUsuarioPorCaminata inner join
(select uc.idUsuarioPorCaminata,count(l.idUsuarioPorCaminata)likes
from UsuarioPorCaminata uc left join Likes l on uc.idUsuarioPorCaminata=l.idUsuarioPorCaminata
group by uc.idUsuarioPorCaminata)temp on temp.idUsuarioPorCaminata=uc.idUsuarioPorCaminata
group by c.nombreDelLugar
end