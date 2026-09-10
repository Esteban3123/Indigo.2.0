
CREATE PROCEDURE [dbo].[SP_HC_ListarFoliosPendientesValidarHCTelefonicas]
(
@INDPacient as varchar(25),
@CentroAtencion Char(10) ,
@UFuncional char(20)  ,
@Profesional as char(20),
@DashboardEnfermeria as bit
)
AS
BEGIN
	SET NOCOUNT ON;

if @DashboardEnfermeria = 1 begin

Select     cast(0 as bit) as Seleccionar,
			   HC.ID
			  ,	HC.FECHISPAC as FechaHistoria
			  , RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico
			  , case hc.INDICATION when 1 then 'Telefónica' when 2 then 'Verbal' when 3 then 'Escrita' when 4 then 'Resultado crítico' end as TipoIndicacion
			  , Rtrim(t.NOMMEDICO) as NombreProfesionalAsisteshc
			  , G.DESESPECI AS EspecialidadProfesionalAsistehc
			  , X.UFUDESCRI AS UnidadFuncionadelFolio
			  , HC.NUMEFOLIO as Folio			  
			  , HC.IPCODPACI as Paciente				  
			  , HC.NUMINGRES as Ingreso
			  , case PendingValidationHCTelephone when 0 then 'Sin firmar' when 1 then 'Firmados' end as Estado, HC.PendingValidationHCTelephone
	from HCHISPACA HC with(nolock)
					INNER JOIN INPACIENT I with(nolock) on hc.IPCODPACI = I.IPCODPACI 
					INNER JOIN ADINGRESO N with(nolock) ON N.NUMINGRES = hc.NUMINGRES
					INNER JOIN INUNIFUNC Z with(nolock) ON  Z.UFUCODIGO  = hc.UFUCODIGO
					INNER JOIN INUNIFUNC X ON X.UFUCODIGO = HC.UFUCODIGO 
					INNER JOIN INPROFSAL T ON T.CODPROSAL = HC.MEDSOLHCTELEF 				
					INNER JOIN INESPECIA G ON G.CODESPECI = HC.ESPMEDSOLHCTELEF 
					LEFT OUTER JOIN INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = I.IPCODPACI AND NUMINGRES = N.NUMINGRES AND CODDIAPRI = 1)  
   WHERE hc.IPCODPACI = @INDPacient and HC.HCTELEFONICA = 1 and HC.PendingValidationHCTelephone in(0,1) --and hc.CODPROSAL = @Profesional

end else begin

Select     cast(0 as bit) as Seleccionar,
			   HC.ID
			  ,	HC.FECHISPAC as FechaHistoria
			  , RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico
			  , case hc.INDICATION when 1 then 'Telefonica' when 2 then 'Verbal' when 3 then 'Escrita' when 4 then 'Resultado crítico' end as TipoIndicacion
			  , Rtrim(t.NOMMEDICO) as NombreProfesionalAsisteshc
			  , G.DESESPECI AS EspecialidadProfesionalAsistehc
			  , X.UFUDESCRI AS UnidadFuncionadelFolio
			  , HC.NUMEFOLIO as Folio			  
			  , HC.IPCODPACI as Paciente				  
			  , HC.NUMINGRES as Ingreso
			  , case PendingValidationHCTelephone when 0 then 'Sin firmar' when 1 then 'Firmados' end as Estado, HC.PendingValidationHCTelephone
	from HCHISPACA HC with(nolock)
					INNER JOIN INPACIENT I with(nolock) on hc.IPCODPACI = I.IPCODPACI 
					INNER JOIN ADINGRESO N with(nolock) ON N.NUMINGRES = hc.NUMINGRES
					INNER JOIN INUNIFUNC Z with(nolock) ON  Z.UFUCODIGO  = hc.UFUCODIGO
					INNER JOIN INUNIFUNC X ON X.UFUCODIGO = HC.UFUCODIGO 
					INNER JOIN INPROFSAL T ON T.CODPROSAL = HC.CODPROSAL 				
					INNER JOIN INESPECIA G ON G.CODESPECI = HC.CODESPTRA
					LEFT OUTER JOIN INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = I.IPCODPACI AND NUMINGRES = N.NUMINGRES AND CODDIAPRI = 1)  
   WHERE hc.IPCODPACI = @INDPacient and HC.HCTELEFONICA = 1 and HC.PendingValidationHCTelephone in(0,1) and hc.MEDSOLHCTELEF = @Profesional
   end
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los folios de historia clínica telefónica pendientes de validación (firmar o sin firmar) para un paciente específico, filtrando por centro de atención, unidad funcional y profesional. Consulta las historias clínicas de HCHISPACA cruzando con el maestro de pacientes (INPACIENT), los ingresos o admisiones (ADINGRESO), las unidades funcionales (INUNIFUNC), los profesionales de la salud (INPROFSAL), las especialidades médicas (INESPECIA) y los diagnósticos CIE-10 principales del ingreso (INDIAGNOS/INDIAGNOP). Tiene dos modos de operación: cuando el parámetro de dashboard de enfermería está activo, muestra los folios asociados al médico que solicitó la HC telefónica; de lo contrario, filtra además por el profesional tratante que la debe firmar. Se usa para que médicos o enfermería identifiquen y gestionen las historias clínicas telefónicas que aún requieren firma o validación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFoliosPendientesValidarHCTelefonicas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFoliosPendientesValidarHCTelefonicas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las historias clínicas telefónicas de un paciente que están pendientes de validar/firmar, diferenciando si la consulta proviene del dashboard de enfermería o del profesional tratante.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFoliosPendientesValidarHCTelefonicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT y tener ingresos en ADINGRESO.; Las historias deben estar marcadas como telefónicas (HCTELEFONICA=1).; El estado de validación (PendingValidationHCTelephone) debe ser 0 (Sin firmar) o 1 (Firmados).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFoliosPendientesValidarHCTelefonicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran historias clínicas marcadas como telefónicas (HCTELEFONICA=1).; Solo se incluyen historias con estado de validación 0 o 1 (excluye otros estados).; El diagnóstico mostrado es siempre el diagnóstico principal (CODDIAPRI=1) del ingreso, tomando el primero encontrado.; La columna Seleccionar siempre se devuelve en 0 (false) por defecto.; El filtro por paciente (@INDPacient) es obligatorio en ambos flujos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFoliosPendientesValidarHCTelefonicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica telefónica; Validación/firma de historia clínica; Indicación médica (telefónica, verbal, escrita, resultado crítico); Diagnóstico principal; Profesional tratante; Médico solicitante; Unidad funcional; Especialidad médica; Folio; Ingreso del paciente; Dashboard de enfermería', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFoliosPendientesValidarHCTelefonicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHISPACA: Cuando @DashboardEnfermeria=1, retorna las HC telefónicas del paciente filtrando solo por paciente, sin restringir por profesional, mostrando el médico solicitante (MEDSOLHCTELEF) y su especialidad (ESPMEDSOLHCTELEF).; [RETURN_RESULT] HCHISPACA: Cuando @DashboardEnfermeria=0, retorna las HC telefónicas del paciente filtradas además por MEDSOLHCTELEF=@Profesional, mostrando el profesional tratante (CODPROSAL) y su especialidad (CODESPTRA).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFoliosPendientesValidarHCTelefonicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @DashboardEnfermeria = 1 → Listar todas las HC telefónicas pendientes del paciente sin filtrar por profesional, mostrando datos del médico solicitante. else Listar las HC telefónicas pendientes del paciente filtrando por el profesional solicitante (@Profesional) y mostrando datos del profesional tratante.; si hc.INDICATION (1=Telefónica, 2=Verbal, 3=Escrita, 4=Resultado crítico) → Traducir el código numérico a la etiqueta de tipo de indicación correspondiente.; si PendingValidationHCTelephone (0=Sin firmar, 1=Firmados) → Traducir el código numérico al estado textual de la historia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFoliosPendientesValidarHCTelefonicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; dbo.INDIAGNOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFoliosPendientesValidarHCTelefonicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFoliosPendientesValidarHCTelefonicas';
-- GO
