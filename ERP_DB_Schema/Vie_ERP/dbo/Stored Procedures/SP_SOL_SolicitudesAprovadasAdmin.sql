

CREATE procedure [dbo].[SP_SOL_SolicitudesAprovadasAdmin]
(
@CodigoUsuario char(20)
)
AS
BEGIN
SET NOCOUNT ON;
select b.COMAUTON, a.COMAUTO as AutoSolicitud,b.COMFECHA,b.UFUCODIGO,b.CODUSUARI,a.CODUSUARI, b.COMESTADO,c.UFUDESCRI, a.FECHAUTORI ,d.NOMUSUARI,e.AUTO, b.COMOBSERV
from SOL_AUTORI  as a
inner join SOLCOMPRA as b on a.COMAUTO=b.COMAUTON
inner join INUNIFUNC  as c on b.UFUCODIGO = c.UFUCODIGO 
inner join SEGusuaru  as d on b.CODUSUARI = d.CODUSUARI
inner join SOLTIPSOL as e on b.AUTO = e.AUTO
where a.CODUSUARI=@CodigoUsuario
 
 
 end