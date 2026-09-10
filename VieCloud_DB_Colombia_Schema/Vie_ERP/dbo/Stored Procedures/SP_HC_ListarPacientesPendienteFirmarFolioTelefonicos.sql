

-- =============================================
-- Author:		
-- Create date: 14/12/2022
-- Description:	SP que lista los pacientes Que Tiene folios pendientes de firmar.
-- =============================================

CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos]
(
@CentroAtencion Char(10) ,
@UFuncional char(20),
@Profesional as char(20),
@DashboardEnfermeria as bit
)
AS
BEGIN
	SET NOCOUNT ON;

if @DashboardEnfermeria = 1 begin

	Select distinct N.CODTIPPAC as TipoPaciente, I.IPCODPACI AS Identificacion, RTRIM(I.IPNOMCOMP) AS NombreCompleto, IPFECNACI AS FechaNacimiento , 
			RTRIM(O.NOMENTIDA) as EntidadPaciente, RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico, N.NUMINGRES AS Ingreso
	from HCHISPACA HC with(nolock)
					INNER JOIN INPACIENT I with(nolock) on hc.IPCODPACI = I.IPCODPACI 
					INNER JOIN ADINGRESO N with(nolock) ON N.NUMINGRES = hc.NUMINGRES
					INNER JOIN INUNIFUNC Z with(nolock) ON  Z.UFUCODIGO  = hc.UFUCODIGO
					INNER JOIN INENTIDAD O with(nolock) ON O.CODENTIDA = N.CODENTIDA 
					LEFT OUTER JOIN INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = I.IPCODPACI AND NUMINGRES = N.NUMINGRES AND CODDIAPRI = 1)  
   WHERE HC.HCTELEFONICA = 1 and HC.PendingValidationHCTelephone = 0 and hc.CODCENATE = @CentroAtencion and hc.UFUCODIGO = @UFuncional --and  hc.CODPROSAL = @Profesional

end else begin

	Select distinct N.CODTIPPAC as TipoPaciente, I.IPCODPACI AS Identificacion, RTRIM(I.IPNOMCOMP) AS NombreCompleto, IPFECNACI AS FechaNacimiento , 
			RTRIM(O.NOMENTIDA) as EntidadPaciente, RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico, N.NUMINGRES AS Ingreso
	from HCHISPACA HC with(nolock)
					INNER JOIN INPACIENT I with(nolock) on hc.IPCODPACI = I.IPCODPACI 
					INNER JOIN ADINGRESO N with(nolock) ON N.NUMINGRES = hc.NUMINGRES
					INNER JOIN INUNIFUNC Z with(nolock) ON  Z.UFUCODIGO  = hc.UFUCODIGO
					INNER JOIN INENTIDAD O with(nolock) ON O.CODENTIDA = N.CODENTIDA 
					LEFT OUTER JOIN INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = I.IPCODPACI AND NUMINGRES = N.NUMINGRES AND CODDIAPRI = 1)  
   WHERE HC.HCTELEFONICA = 1 and HC.PendingValidationHCTelephone = 0 and hc.CODCENATE = @CentroAtencion and hc.UFUCODIGO = @UFuncional and  hc.MEDSOLHCTELEF = @Profesional

	end
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes que tienen historias clínicas telefónicas pendientes de firma o validación en una unidad funcional y centro de atención específicos. Combina datos de la historia clínica (HCHISPACA), información del paciente (INPACIENT), el ingreso o episodio de atención (ADINGRESO), la entidad aseguradora o pagadora (INENTIDAD) y el diagnóstico principal CIE-10 (INDIAGNOS/INDIAGNOP) para armar una vista consolidada del paciente pendiente de firmar. Tiene dos modos de operación: cuando se activa el modo dashboard de enfermería muestra todos los folios telefónicos pendientes de la unidad sin filtrar por profesional; en el modo normal filtra además por el médico o profesional solicitante. Se usa típicamente en tableros de control de enfermería y médicos para gestionar la firma pendiente de consultas o atenciones telefónicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los pacientes con folios de historia clínica telefónica pendientes de firma para un centro de atención y unidad funcional, diferenciando vista de enfermería (todos) vs. vista por profesional solicitante.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse el código de centro de atención y la unidad funcional; En modo no-enfermería, debe proporcionarse el código del profesional solicitante de la HC telefónica; Las tablas de pacientes, ingresos, entidades y unidades funcionales deben tener integridad referencial con HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran historias clínicas marcadas como telefónicas (HCTELEFONICA = 1); Solo se incluyen historias clínicas que ya no están pendientes de validación telefónica (PendingValidationHCTelephone = 0); El diagnóstico mostrado corresponde únicamente al diagnóstico principal del ingreso (CODDIAPRI = 1); Los resultados se devuelven sin duplicados (DISTINCT); El filtrado siempre se restringe al centro de atención y unidad funcional indicados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; historia clínica telefónica; ingreso; centro de atención; unidad funcional; entidad/aseguradora; diagnóstico principal; profesional médico solicitante; firma pendiente de folio; dashboard de enfermería', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Cuando el modo enfermería está activo, retorna pacientes con HCTELEFONICA=1 y PendingValidationHCTelephone=0 filtrados solo por centro y unidad funcional; [RETURN_RESULT] dbo.HCHISPACA: Cuando el modo enfermería está inactivo, retorna pacientes con HCTELEFONICA=1 y PendingValidationHCTelephone=0 filtrados por centro, unidad funcional y profesional solicitante (MEDSOLHCTELEF)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Modo dashboard de enfermería activo (flag = 1) → Lista pacientes filtrando solo por centro de atención y unidad funcional, sin restringir por profesional solicitante else Lista pacientes filtrando además por el profesional médico solicitante de la atención telefónica (MEDSOLHCTELEF)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.INDIAGNOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos';
-- GO
