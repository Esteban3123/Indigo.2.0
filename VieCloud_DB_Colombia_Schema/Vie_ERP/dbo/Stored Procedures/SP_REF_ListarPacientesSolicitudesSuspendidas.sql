-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,27-10-2018,>
-- Description:	<Description,Sp que me lista los pacientes de referencia con solicitud de remisión>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarPacientesSolicitudesSuspendidas]
(
  @CentroAtencion as varchar(max)
)

AS
BEGIN
  SET NOCOUNT ON;
  
 declare @Sql as nvarchar(max)

  Set @Sql = ' SELECT 
					S.CODTIPPAC as Tipo , 
					G.AUTO,	
					G.FECSOLICIT as ''Fecha Solicitud'', 
					G.IPCODPACI as Identificacion, 
					RTRIM(I.IPNOMCOMP) as ''Nombre Paciente'', 
					IPFECNACI, 
					RTRIM(E.CODENTIDA) + ''-'' + RTRIM(E.NOMENTIDA) AS Entdidad, 
					A.NUMINGRES as ingreso, 
					RTRIM(UFUDESCRI) AS UnidadFuncional, 
					S.FECREGCRE, 
					S.FECHEGRESO , 
					Rtrim(Z.CODCENATE) as ''Codigo Centro Atencion'', 
					Rtrim(z.NOMCENATE) as ''Nombre Centro Atencion'', 
					G.ESTADO  
			   FROM dbo.HCREFCONP  AS G  
					INNER JOIN dbo.INPACIENT AS I ON I.IPCODPACI = G.IPCODPACI 
					INNER JOIN dbo.ADINGRESO  AS S ON  S.NUMINGRES = G.NUMINGRES
					LEFT JOIN dbo.CHREGEGRE  AS A ON A.NUMINGRES  = S.NUMINGRES 
					INNER JOIN dbo.INENTIDAD as E ON E.CODENTIDA =s.CODENTIDA 
					INNER JOIN dbo.ADCENATEN  AS Z ON  Z.CODCENATE = g.CODCENATE
					LEFT JOIN dbo.INUNIFUNC as R ON R.UFUCODIGO = UFUEGRHOS
			   WHERE  convert(varchar(500),g.CODCENATE) IN (' + @CentroAtencion + ') 
					AND ( A.ESTPACEGR <> 4 OR G.ESTADO = 4  ) 
					and G.VISADOSUSPE is null '
  print @sql
  exec sp_executesql @Sql 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes del módulo de referencia y contrarreferencia que tienen solicitudes de remisión en estado suspendido o pendiente de visado, filtradas por uno o varios centros de atención. Combina información del paciente (cédula, nombre, fecha de nacimiento), del ingreso hospitalario (número de ingreso, unidad funcional, fecha de egreso), de la entidad aseguradora y del centro de atención. Se utiliza para hacer seguimiento y control de remisiones que no han sido completadas o aprobadas, permitiendo identificar casos que requieren gestión administrativa en referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarPacientesSolicitudesSuspendidas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarPacientesSolicitudesSuspendidas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de remisión de pacientes pendientes de visado de suspensión, filtradas por uno o varios centros de atención, excluyendo egresos definitivos salvo que la solicitud esté en estado específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe entregarse como una lista de valores válidos para concatenarse en una cláusula IN (formato SQL dinámico).; Deben existir registros relacionados en pacientes, ingresos, entidades y centros de atención para que la solicitud aparezca.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan solicitudes de remisión cuyo visado de suspensión está pendiente (VISADOSUSPE IS NULL).; Se excluyen pacientes ya egresados (ESTPACEGR = 4) salvo que la solicitud esté en estado 4.; El listado se restringe a los centros de atención provistos en el parámetro.; Cada solicitud devuelta tiene paciente, ingreso, entidad y centro de atención obligatorios (INNER JOIN); el egreso y la unidad funcional son opcionales (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Solicitud de remisión/referencia; Ingreso/Admisión; Egreso hospitalario; Entidad (aseguradora); Centro de atención; Unidad funcional; Estado de paciente al egreso; Visado/Suspensión de solicitud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCREFCONP: Cuando VISADOSUSPE IS NULL y (ESTPACEGR<>4 OR ESTADO=4) y el centro de atención está en la lista recibida, se retorna la fila con datos de la solicitud, paciente, ingreso, entidad y centro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREFCONP; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHREGEGRE; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudesSuspendidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudesSuspendidas';
-- GO
