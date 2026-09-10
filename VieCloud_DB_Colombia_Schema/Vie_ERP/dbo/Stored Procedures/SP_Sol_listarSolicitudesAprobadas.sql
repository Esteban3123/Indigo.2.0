

-- sp para listar las solicitudes autorizadas del usuario administrador logueado

CREATE PROCEDURE [dbo].[SP_Sol_listarSolicitudesAprobadas] 
(
@EstadoSolicitud  int
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT A.COMAUTON,A.COMFECHA,A.UFUCODIGO ,E.DESTISOL,A.CODUSUARI AS USERSOLI,A.COMESTADO,RTRIM(B.UFUDESCRI)AS UFUDESCRI,
RTRIM(D.NOMUSUARI ) AS NOMUSERSOLI,A.TITUAGRUS,
A.TIPOAGRUP,CAST('' AS BIT) as Marcar

FROM SOLCOMPRA  AS A   
INNER JOIN INUNIFUNC  AS B ON A.UFUCODIGO = B.UFUCODIGO 
INNER JOIN SEGusuaru  AS D ON A.CODUSUARI = D.CODUSUARI 
INNER JOIN SOLTIPSOL AS E ON A.AUTO=E.AUTO

WHERE A.COMESTADO =@EstadoSolicitud
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las solicitudes de compra registradas en el sistema filtrando por un estado específico (por ejemplo: aprobadas, pendientes, rechazadas). Combina información de la solicitud de compra con la descripción de la unidad funcional solicitante (servicio o área), el nombre del usuario que generó la solicitud y el tipo o descripción de la solicitud. Está orientada al usuario administrador que necesita revisar y gestionar el flujo de aprobación de requerimientos de adquisición de bienes o servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Sol_listarSolicitudesAprobadas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Sol_listarSolicitudesAprobadas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de compra que se encuentran en un estado específico (típicamente aprobadas), enriquecidas con datos de la unidad funcional, tipo de solicitud y usuario solicitante.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_listarSolicitudesAprobadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las solicitudes deben tener una unidad funcional válida en INUNIFUNC; El usuario solicitante debe existir en SEGusuaru; El tipo de solicitud (AUTO) debe existir en SOLTIPSOL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_listarSolicitudesAprobadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan solicitudes cuyo estado sea exactamente igual al parámetro recibido; Solo se incluyen solicitudes con relaciones existentes en unidad funcional, usuario y tipo de solicitud (INNER JOIN); La columna Marcar siempre se devuelve como BIT vacío para uso posterior en UI; Las descripciones de unidad funcional y nombre de usuario se devuelven sin espacios a la derecha (RTRIM)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_listarSolicitudesAprobadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'solicitud de compra; solicitud aprobada/autorizada; unidad funcional; tipo de solicitud; usuario solicitante; estado de solicitud; agrupación de solicitudes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_listarSolicitudesAprobadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] SOLCOMPRA: Cuando COMESTADO coincide con el estado recibido, devuelve las solicitudes con datos cruzados de unidad funcional, tipo de solicitud y usuario solicitante, incluyendo una columna Marcar inicializada en BIT vacío', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_listarSolicitudesAprobadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SOLCOMPRA; dbo.INUNIFUNC; dbo.SEGusuaru; dbo.SOLTIPSOL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_listarSolicitudesAprobadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_listarSolicitudesAprobadas';
-- GO
