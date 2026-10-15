

CREATE PROCEDURE [dbo].[SP_SOL_ListarSolicitudesAdmin]
(
@CodUser char (20),
@Estado as int
)
AS
BEGIN
	SET NOCOUNT ON;

-- si no hemos enviado un estado listaremos todas las solicitudes que se hayan realizado en las unidades
-- funcionales del administrador
if @Estado is null
				
select a.UFUCODIGO  ,a.codusuari as coduseradmin , b.CODUSUARI as codsolicitante , b.COMAUTON ,b.COMESTADO ,b.COMFECHA ,b.COMOBSERV  ,c.NOMUSUARI  ,B.AUTO 
from SOLADMIFUN as a 
inner join SOLCOMPRA as b on a.UFUCODIGO = b.UFUCODIGO inner join SEGusuaru  as c on b.CODUSUARI = c.CODUSUARI  
where a.CODUSUARI = @CodUser 

else	
-- si hemos enviado un estado listaremos todas las solicitudes deacuerdo al estado que queremos
select a.ufucodigo ,a.codusuari as coduseradmin , b.CODUSUARI as codsolicitante , b.COMAUTON ,b.COMESTADO ,b.COMFECHA ,b.COMOBSERV  ,c.NOMUSUARI  ,b.AUTO,d.DESTISOL ,e.UFUDESCRI 
from SOLADMIFUN as a 
inner join SOLCOMPRA as b on a.UFUCODIGO = b.UFUCODIGO inner join SEGusuaru  as c on b.CODUSUARI = c.CODUSUARI  inner join SOLTIPSOL as d on b.AUTO = d.AUTO inner join 
INUNIFUNC  as e on b.UFUCODIGO = e.UFUCODIGO 
where a.CODUSUARI = @CodUser and b.COMESTADO = @Estado 


END