
CREATE PROCEDURE [Lactation].[SP_HC_ListarSolicitudesLecheMaterna]
    @CentroAtencion VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Consulta 2: LECHE MATERNA (de BreastMilkOrder)
    SELECT DISTINCT
        O.Id,
		OD.Id AS IdDetalle,
		OD.Status AS EstadoFormula,
		O.Status AS 'ESTADO', 
		OD.Batch,
		CASE OD.Status WHEN 1 THEN '1 - Asignadas' WHEN 2 THEN  '2 - En preparación' WHEN 3 THEN '3 - Preparadas' WHEN 4 THEN '4 - Entregadas a la unidad' END AS 'EstadoLabel',
        O.OrderDate AS 'FechaOrden',
        RTRIM(O.IPCODPACI) AS 'Identificacion',
		RTRIM(o.NUMINGRES) AS 'Ingreso',
		O.NUMEFOLIO,
        RTRIM(P.IPNOMCOMP) AS 'Paciente',
        P.IPFECNACI AS 'FechaNacimiento',
        dbo.edad(P.IPFECNACI, common.GETDATE()) AS 'Edad',
		RTRIM(O.CareCenterCode) AS 'CodCentroAtencion',
        RTRIM(CA.NOMCENATE) AS 'CentroAtencion',
		RTRIM(O.FunctionalUnitCode) AS 'CodUnidadFuncional',
        RTRIM(U.UFUDESCRI) AS 'UnidadFuncional',
        RTRIM(G.DESCCAMAS) AS 'Cama', 
        RTRIM(CH.DESTIPDIE) AS 'ComponenteLacteo',
        'No aplica' AS 'PBS',
        O.AdministrationInstructions AS 'DosisPrescrita',
		HIS.INDICAPAC AS 'EstadoHistoria',
		DIE.OBSERVACI AS 'Observaciones',
		CAST(0 AS bit) AS Seleccion,
		CASE 
			WHEN HIS.INDICAPAC IN (9, 10, 11, 12, 15, 16) THEN 'Egreso del paciente'
			WHEN O.Status IN (3,4) THEN 'Anulada leche materna'
        ELSE NULL
		END AS ALERTA,
		IIF(HIS.INDICAPAC IN (9, 10, 11, 12, 15, 16) OR O.Status = 3, CAST(1 AS BIT), CAST(0 AS BIT)) AS Anulado,
		od.DateFormulaPreparationStart AS 'DateFormulaPreparationStart',
		od.DateFormulaPreparationEnd AS 'DateFormulaPreparationEnd',
		Report.GetReportName(HIS.ID) AS Reporte		
    FROM Lactation.BreastMilkOrder O
			INNER JOIN Lactation.BreastMilkPreparationDetails OD ON OD.IdBreastMilkOrder = O.Id
			INNER JOIN ADINGRESO E WITH(NOLOCK) ON O.IPCODPACI = E.IPCODPACI AND O.NUMINGRES = E.NUMINGRES 
			INNER JOIN INPACIENT P ON O.IPCODPACI = P.IPCODPACI
			INNER JOIN CHTIPDIET CH ON O.DietCode = CH.CODTIPDIE
			INNER JOIN ADCENATEN CA WITH(NOLOCK) ON O.CareCenterCode = CA.CODCENATE
			INNER JOIN INUNIFUNC U WITH(NOLOCK) ON O.FunctionalUnitCode = U.UFUCODIGO 
				OUTER APPLY (
							SELECT TOP 1 *
							FROM HCHISPACA H WITH(NOLOCK)
							WHERE H.IPCODPACI = o.IPCODPACI AND H.NUMINGRES = o.NUMINGRES
							ORDER BY H.FECHISPAC DESC
						) HIS
			INNER JOIN HCREGDIET DIE ON DIE.IPCODPACI = O.IPCODPACI AND O.NUMINGRES = DIE.NUMINGRES AND DIE.NUMEFOLIO = O.NUMEFOLIO
			LEFT JOIN CHCAMASHO G WITH(NOLOCK) ON IIF(E.CODCAMACT IS NULL,dbo.MotherCurrentBed(O.IPCODPACI, O.NUMINGRES),E.CODCAMACT) = G.CODICAMAS	
    WHERE 
	O.CareCenterCode IN (SELECT Value FROM dbo.splitstring(@CentroAtencion))
	AND OD.Status NOT IN (5,6,7)

    ORDER BY FechaOrden
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lista las solicitudes de leche materna activas por centro de atención, combinando órdenes de la tabla de prescripciones con sus detalles de preparación (teteros), excluyendo estados finalizados o cancelados (5,6,7). Presenta información clínica del paciente, cama, unidad funcional, estado del proceso de preparación (asignada, en preparación, preparada, entregada) y genera alertas cuando el paciente ha egresado o la orden está anulada, orientada al dashboard del lactario.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesLecheMaterna';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes activas de leche materna con datos de paciente, ubicación, dieta, estado de preparación y alertas clínicas, filtradas por uno o varios centros de atención.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe ser una cadena parseable por dbo.splitstring (lista delimitada).; Deben existir registros relacionados en BreastMilkOrder y BreastMilkPreparationDetails enlazados por IdBreastMilkOrder.; El paciente debe tener ingreso vigente en ADINGRESO y ficha en INPACIENT.; Deben existir catálogos de dieta (CHTIPDIET), centro de atención (ADCENATEN) y unidad funcional (INUNIFUNC) referenciados por la orden.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las órdenes con detalle en estados 5, 6 o 7 nunca se devuelven (se consideran fuera del flujo activo).; Se selecciona solo la última historia clínica del ingreso (TOP 1 ordenado por FECHISPAC DESC) para evaluar el estado del paciente.; El campo Seleccion siempre se devuelve en 0 (bit) — control de UI inicial.; El componente PBS siempre se reporta como ''No aplica'' para leche materna.; Una orden anulada se identifica cuando O.Status=3 o el paciente presenta indicador de egreso (9,10,11,12,15,16).; La cama siempre se intenta resolver: si no hay cama actual, se delega a la lógica de cama de la madre.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Leche materna; Solicitud/orden de lactancia; Preparación de tetero (batch); Estado de preparación (asignada, en preparación, preparada, entregada); Centro de atención; Unidad funcional; Cama hospitalaria; Componente lácteo / dieta; Ingreso hospitalario; Egreso del paciente; Historia clínica del paciente; Anulación de orden; Cama de la madre (binomio madre-hijo)', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Lactation.BreastMilkOrder: Retorna solo órdenes cuyo CareCenterCode esté en la lista de @CentroAtencion (split) y cuyo detalle OD.Status NOT IN (5,6,7), ordenadas por FechaOrden.; [RETURN_RESULT] Lactation.BreastMilkPreparationDetails: Excluye detalles con Status 5, 6 y 7; mapea Status 1→''Asignadas'', 2→''En preparación'', 3→''Preparadas'', 4→''Entregadas a la unidad''.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si OD.Status = 1/2/3/4 → Etiqueta EstadoLabel como ''1 - Asignadas'' / ''2 - En preparación'' / ''3 - Preparadas'' / ''4 - Entregadas a la unidad'' else EstadoLabel = NULL; si HIS.INDICAPAC IN (9,10,11,12,15,16) → ALERTA = ''Egreso del paciente'' y Anulado = 1 else Evalúa siguiente condición; si O.Status IN (3,4) → ALERTA = ''Anulada leche materna'' else ALERTA = NULL; si O.Status = 3 o INDICAPAC en lista de egreso → Anulado = 1 (bit) else Anulado = 0; si E.CODCAMACT IS NULL (sin cama actual del ingreso) → Usa dbo.MotherCurrentBed(IPCODPACI,NUMINGRES) para resolver la cama (caso madre/recién nacido) else Usa E.CODCAMACT del ingreso', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.edad; common.GETDATE; dbo.MotherCurrentBed; Report.GetReportName; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Lactation.BreastMilkOrder; Lactation.BreastMilkPreparationDetails; dbo.ADINGRESO; dbo.INPACIENT; dbo.CHTIPDIET; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.HCHISPACA; dbo.HCREGDIET; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesLecheMaterna';
-- GO
