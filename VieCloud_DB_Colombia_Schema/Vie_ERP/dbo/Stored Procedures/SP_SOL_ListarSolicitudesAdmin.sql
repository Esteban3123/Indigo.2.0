

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las solicitudes de compra asignadas a un administrador según las unidades funcionales que supervisa. Recibe el código del usuario administrador y un estado opcional: si no se indica estado, retorna todas las solicitudes de las unidades funcionales del administrador junto con el usuario solicitante y sus observaciones; si se indica un estado específico (por ejemplo, pendiente, aprobada, rechazada), filtra las solicitudes por ese estado e incluye además la descripción del tipo de solicitud y el nombre de la unidad funcional. Compone información de las solicitudes de compra (SOLCOMPRA), las unidades funcionales permitidas al administrador (SOLADMIFUN), los datos del usuario solicitante (SEGusuaru), el tipo de solicitud (SOLTIPSOL) y el catálogo de unidades funcionales (INUNIFUNC). Sirve para que los administradores gestionen y hagan seguimiento a los requerimientos de adquisición dentro de las áreas o servicios bajo su responsabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SOL_ListarSolicitudesAdmin';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SOL_ListarSolicitudesAdmin';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de compra de las unidades funcionales asignadas a un administrador, opcionalmente filtradas por estado, para su gestión y seguimiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarSolicitudesAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe estar registrado como administrador con unidades funcionales asignadas en SOLADMIFUN; Las solicitudes en SOLCOMPRA deben estar asociadas a una unidad funcional válida y a un usuario solicitante existente en SEGusuaru; Cuando se filtra por estado, el tipo de solicitud (AUTO) debe existir en SOLTIPSOL y la unidad funcional en INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarSolicitudesAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven solicitudes de unidades funcionales asignadas al administrador autenticado (a.CODUSUARI = @CodUser); Siempre se cruza la solicitud con el usuario solicitante (INNER JOIN obliga a que exista en SEGusuaru); El alcance del administrador está restringido por las filas de SOLADMIFUN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarSolicitudesAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de compra; Administrador de unidad funcional; Unidad funcional; Tipo de solicitud; Estado de solicitud; Usuario solicitante', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarSolicitudesAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] SOLCOMPRA: Cuando @Estado IS NULL, retorna todas las solicitudes de las unidades funcionales del administrador (sin filtro de estado y sin descripciones de tipo/unidad); [RETURN_RESULT] SOLCOMPRA: Cuando @Estado tiene valor, retorna solicitudes filtradas por COMESTADO=@Estado, incluyendo descripción del tipo de solicitud (DESTISOL) y descripción de la unidad funcional (UFUDESCRI)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarSolicitudesAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Estado IS NULL → Lista todas las solicitudes de las unidades funcionales del administrador sin filtrar por estado y sin incluir descripciones de tipo de solicitud ni nombre de unidad funcional else Filtra solicitudes por el estado indicado e incluye descripción del tipo de solicitud y nombre de la unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarSolicitudesAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SOLADMIFUN; dbo.SOLCOMPRA; dbo.SEGusuaru; dbo.SOLTIPSOL; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarSolicitudesAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarSolicitudesAdmin';
-- GO
