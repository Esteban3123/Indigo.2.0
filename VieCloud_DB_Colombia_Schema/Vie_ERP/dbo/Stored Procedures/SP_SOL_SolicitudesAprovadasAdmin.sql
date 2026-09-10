

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y retorna todas las solicitudes de compra que han sido aprobadas por un administrador o usuario específico, identificado por su código de usuario. Cruza las autorizaciones registradas en SOL_AUTORI con las solicitudes de compra de SOLCOMPRA, enriqueciendo el resultado con la descripción de la unidad funcional solicitante (INUNIFUNC), el nombre del usuario que realizó la solicitud (SEGusuaru) y el tipo de solicitud (SOLTIPSOL). Se usa para que un administrador visualice el historial de solicitudes de adquisición que él mismo ha aprobado, incluyendo fechas de autorización, estado de la solicitud y observaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SOL_SolicitudesAprovadasAdmin';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SOL_SolicitudesAprovadasAdmin';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de compra que han sido autorizadas por un usuario administrador específico, junto con datos descriptivos de unidad funcional, usuario solicitante y tipo de solicitud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolicitudesAprovadasAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe suministrarse el código del usuario autorizador para filtrar las autorizaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolicitudesAprovadasAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan solicitudes que tengan registro de autorización en SOL_AUTORI vinculado al usuario autorizador indicado.; Solo se incluyen solicitudes con unidad funcional, usuario solicitante y tipo de solicitud existentes (INNER JOIN obliga integridad referencial).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolicitudesAprovadasAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de compra; Autorización de solicitud; Unidad funcional; Usuario autorizador; Tipo de solicitud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolicitudesAprovadasAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.SOL_AUTORI: Cuando SOL_AUTORI.CODUSUARI = @CodigoUsuario, se retorna el conjunto de solicitudes autorizadas por ese usuario unidas a SOLCOMPRA, INUNIFUNC, SEGusuaru y SOLTIPSOL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolicitudesAprovadasAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SOL_AUTORI; dbo.SOLCOMPRA; dbo.INUNIFUNC; dbo.SEGusuaru; dbo.SOLTIPSOL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolicitudesAprovadasAdmin';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_SolicitudesAprovadasAdmin';
-- GO
