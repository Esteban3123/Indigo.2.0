

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las solicitudes de compra aceptadas (o en un estado específico) asociadas a un usuario determinado, cruzando la información de la solicitud con la unidad funcional responsable, el registro de autorización y los nombres tanto del usuario que generó la solicitud como del usuario que la autorizó. Combina las tablas de solicitudes de compra, unidades funcionales, autorizaciones y usuarios del sistema para devolver un resumen completo del seguimiento de cada requerimiento de adquisición. Se usa para que un usuario pueda visualizar el estado y el historial de aprobación de sus propias solicitudes de compra dentro del flujo administrativo de la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SOL_SolAceptadasUsuario';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SOL_SolAceptadasUsuario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las solicitudes de compra de un usuario filtradas por estado, junto con la unidad funcional, datos de autorización y nombres del solicitante y autorizador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolAceptadasUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La solicitud debe tener una autorización asociada en SOL_AUTORI (INNER JOIN obligatorio).; La solicitud debe tener una unidad funcional válida en INUNIFUNC.; Tanto el usuario solicitante como el usuario autorizador deben existir en SEGusuaru.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolAceptadasUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna solicitudes que tienen registro de autorización (SOL_AUTORI) emparejado.; El filtro siempre combina usuario solicitante y estado de solicitud de forma conjunta.; Los nombres y descripciones se devuelven sin espacios en blanco a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolAceptadasUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de compra; Autorización de solicitud; Unidad funcional; Usuario solicitante; Usuario autorizador; Estado de solicitud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolAceptadasUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] SOLCOMPRA: Devuelve solicitudes donde CODUSUARI = usuario indicado y COMESTADO = estado indicado, enriquecidas con descripción de unidad funcional y nombres de solicitante y autorizador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolAceptadasUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SOLCOMPRA; dbo.INUNIFUNC; dbo.SOL_AUTORI; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolAceptadasUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolAceptadasUsuario';
-- GO
