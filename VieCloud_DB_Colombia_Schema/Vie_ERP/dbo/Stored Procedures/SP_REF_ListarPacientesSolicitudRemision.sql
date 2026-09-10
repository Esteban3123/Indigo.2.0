-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,27-10-2018,>
-- Description:	<Description,Sp que me lista los pacientes de referencia con solicitud de remisión>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarPacientesSolicitudRemision]
(
  @CentroAtencion as varchar(MAX)
)

AS
BEGIN
  SET NOCOUNT ON;
  
 declare @Sql as nvarchar(MAX)

  Set @Sql = ' Select RequestType, NOVEDAD,FECSOLICIT,HCREF.AUTO,HCREF.IPCODPACI,RTRIM(IPNOMCOMP)AS IPNOMCOMP,IPFECNACI,HCREF.UFUCODIGO,RTRIM(UFUDESCRI) AS CodDesUnidadFuncio,NUMINGRES,inen.CODENTIDA,RTRIM(NOMENTIDA) AS CodNomEntida,HCREF.CODPROSAL,RTRIM(NOMMEDICO) AS CodNomProf,espec.CODESPECI,RTRIM(DESESPECI) AS CodDesEspeci,SERVIDOREM, CASE WHEN HCREF.ESTADO = 1 THEN ''1. Solicitado sin definir pertinencia''	  WHEN HCREF.ESTADO = 2 THEN ''3. Gestionando''	  WHEN HCREF.ESTADO = 3 THEN ''4. Aceptado con pendiente de salida''	  WHEN HCREF.ESTADO = 6 THEN ''2. Solicitado con pertinencia médica'' END AS CASE_ESTADO,HCREF.ESTADO,FECHCONFIR,Rtrim(Z.CODCENATE) as ''Codigo Centro Atencion'',Rtrim(z.NOMCENATE) as ''Nombre Centro Atencion'', dbo.[RiskFactorAlert](HCREF.IPCODPACI, HCREF.NUMINGRES, 1) AS ''AlertaFactoresRiesgo'',
		  dbo.[RiskFactorAlert](HCREF.IPCODPACI, HCREF.NUMINGRES, 2) AS ''AlertaEscalas''
			  FROM dbo.HCREFCONP AS HCREF  
					 INNER JOIN dbo.INUNIFUNC AS unif ON unif.UFUCODIGO = HCREF.UFUCODIGO and unif.UFUTIPUNI <> 15 
					 INNER JOIN dbo.INPACIENT AS inpa  ON inpa.IPCODPACI = HCREF.IPCODPACI 
					 INNER JOIN dbo.INENTIDAD AS inen ON inen.CODENTIDA =  inpa.CODENTIDA 
					 INNER JOIN dbo.INPROFSAL  AS inprof ON  inprof.CODPROSAL = HCREF.CODPROSAL 
					 INNER JOIN dbo.ADCENATEN  AS Z ON  Z.CODCENATE = HCREF.CODCENATE  
					 LEFT JOIN  dbo.INESPECIA  AS espec ON espec.CODESPECI =  HCREF.CODESPECI
					 LEFT JOIN  dbo.HCREFCONTD as d ON d.ID = (select top 1 ID from dbo.HCREFCONTD where HCREFCONPID = HCREF.AUTO AND FECHCONFIR IS NOT NULL)
  Where  convert(varchar(500),HCREF.CODCENATE) IN (' + @CentroAtencion + ')  AND HCREF.ESTADO <> 4 AND HCREF.ESTADO <> 5 AND (HCREF.EXTRAMURAL IS NULL OR  HCREF.EXTRAMURAL = 0 )'
--  PRINT @SQL
  exec sp_executesql @Sql 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con solicitudes de remisión activas (referencia y contrarreferencia) para uno o varios centros de atención indicados como parámetro. Muestra datos del paciente (cédula, nombre, fecha de nacimiento), del ingreso (número de ingreso, unidad funcional), del profesional solicitante, de la entidad aseguradora, la especialidad destina y el estado actual de la remisión (solicitado, con pertinencia médica, gestionando, aceptado con pendiente de salida). Excluye remisiones en estado cerrado o anulado y solicitudes extramuros. Adicionalmente calcula alertas de factores de riesgo y escalas clínicas del paciente mediante la función RiskFactorAlert. Construye la consulta de forma dinámica para filtrar por múltiples centros de atención recibidos como lista en el parámetro @CentroAtencion.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarPacientesSolicitudRemision';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarPacientesSolicitudRemision';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las solicitudes de remisión de pacientes vigentes (no canceladas ni extramurales) para uno o varios centros de atención, con datos del paciente, entidad, profesional, especialidad, estado y alertas clínicas asociadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudRemision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe entregarse pre-formateado como lista válida para una cláusula IN (riesgo de SQL dinámico/inyección); Deben existir relaciones íntegras entre la remisión y paciente, entidad, profesional, unidad funcional y centro de atención (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudRemision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se excluyen remisiones con ESTADO 4 y 5 (descartadas/canceladas según convención); Se excluyen unidades funcionales de tipo 15 (UFUTIPUNI <> 15); Solo se listan remisiones no extramurales (EXTRAMURAL IS NULL o = 0); El detalle de contrarreferencia tomado siempre corresponde al primer registro con FECHCONFIR no nulo; El listado se restringe a los centros de atención indicados en el parámetro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudRemision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de remisión; Referencia y contrarreferencia; Paciente; Centro de atención; Unidad funcional; Especialidad médica; Profesional de la salud; Entidad/aseguradora; Pertinencia médica; Alertas de factores de riesgo; Escalas clínicas; Atención extramural', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudRemision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve remisiones de HCREFCONP filtrando por CODCENATE en la lista recibida, ESTADO distinto de 4 y 5, y EXTRAMURAL nulo o 0; incluye estado traducido, datos relacionados y alertas de factores de riesgo y escalas vía dbo.RiskFactorAlert.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudRemision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTADO = 1 → Etiqueta ''1. Solicitado sin definir pertinencia''; si ESTADO = 2 → Etiqueta ''3. Gestionando''; si ESTADO = 3 → Etiqueta ''4. Aceptado con pendiente de salida''; si ESTADO = 6 → Etiqueta ''2. Solicitado con pertinencia médica''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudRemision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudRemision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREFCONP; dbo.INUNIFUNC; dbo.INPACIENT; dbo.INENTIDAD; dbo.INPROFSAL; dbo.ADCENATEN; dbo.INESPECIA; dbo.HCREFCONTD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudRemision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarPacientesSolicitudRemision';
-- GO
