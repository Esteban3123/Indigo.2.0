

CREATE procedure [dbo].[SP_SOL_SolAceptadasUsuario]
(
@CodigoUsuario char(20),
@EstadoSolicitud  int
)
AS
BEGIN
	SET NOCOUNT ON;
select a.COMAUTON ,a.COMFECHA,a.UFUCODIGO  ,a.CODUSUARI as usersoli,a.COMESTADO ,
rtrim(b.UFUDESCRI)as ufunom ,
c.CODUSUARI as userautori,
c.FECHAUTORI ,
rtrim(d.NOMUSUARI ) as nomusersoli,
RTRIM (e.NOMUSUARI ) as nomautori
from SOLCOMPRA  as a   
inner join INUNIFUNC  as b on a.UFUCODIGO = b.UFUCODIGO 
inner join SOL_AUTORI  as c on a.COMAUTON = c.COMAUTO 
inner join SEGusuaru  as d on a.CODUSUARI = d.CODUSUARI 
inner join SEGusuaru  as e on e.CODUSUARI = c.CODUSUARI 
where a.CODUSUARI = @CodigoUsuario  and a.COMESTADO = @EstadoSolicitud
 
 
 end