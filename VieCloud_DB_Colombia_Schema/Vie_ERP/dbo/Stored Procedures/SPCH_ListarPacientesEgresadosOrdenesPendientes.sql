
CREATE PROCEDURE [dbo].[SPCH_ListarPacientesEgresadosOrdenesPendientes]
(
@UnidadFuncional Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
	DISTINCT 
		cast(' ' as char(100)) AS Origen,'Normal' as Alerta,
		P.IPCODPACI AS Identificacion,
		NUMINGRES AS Ingreso,
		RTRIM(IPNOMCOMP) AS Paciente,
		CAST(0 AS BIT) AS Resultado,
		P.FECREGCRE ,
		CAST('' as bit) AS MuestraAlerta
	FROM INPACIENT P with(nolock)
		 INNER JOIN
		 HCHISPACA H with(nolock)
		 ON P.IPCODPACI = H.IPCODPACI 
	WHERE
		
			H.INDICAPAC IN('9','10','11','12')
			AND 
			H.UFUCODIGO =@UnidadFuncional
			AND
			P.IPCODPACI IN ( SELECT HEMO.IPCODPACI FROM HCORDHEMO HEMO with(nolock) WHERE HEMO.IPCODPACI = P.IPCODPACI AND HEMO.ESTSERIPS ='1'  ) OR
			P.IPCODPACI IN ( SELECT  IPCODPACI FROM HCHOJAMED HM with(nolock) WHERE IPCODPACI = P.IPCODPACI AND HM.MEDESTADO= '2' ) OR
			P.IPCODPACI IN ( SELECT IPCODPACI FROM HCORDPRON with(nolock) WHERE IPCODPACI = P.IPCODPACI AND ESTSERIPS='1')
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes egresados (dados de alta o con estado de egreso) que aún tienen órdenes médicas pendientes de atención en una unidad funcional específica. Cruza la historia clínica del paciente (HCHISPACA) para filtrar por estados de egreso (indicadores 9, 10, 11, 12) con la unidad funcional solicitada, y luego verifica si ese paciente tiene órdenes de hemoterapia pendientes (HCORDHEMO con estado 1), medicamentos en estado pendiente (HCHOJAMED con estado 2), o procedimientos/servicios sin ejecutar (HCORDPRON con estado 1). Se usa para alertar al equipo asistencial o administrativo sobre pacientes que ya egresaron pero cuyas órdenes clínicas (laboratorios, medicamentos, procedimientos) quedaron sin cumplir, evitando omisiones en la atención post-egreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesEgresadosOrdenesPendientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesEgresadosOrdenesPendientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes egresados (o en estados clínicos específicos) de una unidad funcional que aún tienen órdenes médicas, hemoderivados u hojas médicas pendientes por procesar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEgresadosOrdenesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse el código de la unidad funcional a consultar.; Las tablas de pacientes, historia clínica, órdenes hemoderivados, hojas médicas y órdenes pronto deben estar accesibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEgresadosOrdenesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran pacientes cuya historia clínica tenga indicador de estado en (''9'',''10'',''11'',''12'') combinada con la unidad funcional indicada (rama principal del WHERE).; Los estados pendientes de órdenes se identifican con ESTSERIPS=''1''.; El estado pendiente de hoja médica se identifica con MEDESTADO=''2''.; El resultado siempre incluye columnas constantes de presentación (Origen en blanco, Alerta=''Normal'', Resultado=0, MuestraAlerta='''').; Se eliminan duplicados mediante SELECT DISTINCT.; Las consultas se realizan con NOLOCK, permitiendo lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEgresadosOrdenesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Egreso; Unidad funcional; Historia clínica; Orden de hemoderivados; Hoja médica; Órdenes pendientes; Alerta clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEgresadosOrdenesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto distinto de pacientes con columnas fijas Origen='' '', Alerta=''Normal'', Resultado=0 y MuestraAlerta='''' junto con identificación, ingreso, nombre y fecha de registro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEgresadosOrdenesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si H.INDICAPAC IN (''9'',''10'',''11'',''12'') AND H.UFUCODIGO = unidad funcional → Considera al paciente como candidato dentro de la unidad funcional con esos estados clínicos.; si Existe registro en HCORDHEMO con ESTSERIPS=''1'' para el paciente → Incluye al paciente por tener orden de hemoderivado pendiente.; si Existe registro en HCHOJAMED con MEDESTADO=''2'' para el paciente → Incluye al paciente por tener hoja médica en ese estado.; si Existe registro en HCORDPRON con ESTSERIPS=''1'' para el paciente → Incluye al paciente por tener orden pronto pendiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEgresadosOrdenesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.HCHISPACA; dbo.HCORDHEMO; dbo.HCHOJAMED; dbo.HCORDPRON', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEgresadosOrdenesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEgresadosOrdenesPendientes';
-- GO
