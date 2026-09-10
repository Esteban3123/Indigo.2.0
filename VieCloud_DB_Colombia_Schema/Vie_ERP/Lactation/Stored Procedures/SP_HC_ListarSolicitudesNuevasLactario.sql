CREATE PROCEDURE [Lactation].[SP_HC_ListarSolicitudesNuevasLactario]
    @CentroAtencion VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Consulta 1: FÓRMULA LÁCTEA (de HCPRESCRA)
    SELECT		
        A.Id, 
        A.StatusFormulaSairy AS 'ESTADO', --4: Tratamiento Suspendido 7: Tratamiento Terminado por Salida del Paciente
        A.FECINIDOS AS 'FechaOrden',
        RTRIM(A.IPCODPACI) AS 'Identificacion',
		A.NUMINGRES,
		A.NUMEFOLIO,		
        RTRIM(P.IPNOMCOMP) AS 'Paciente',
        P.IPFECNACI AS 'FechaNacimiento',
        dbo.edad(P.IPFECNACI, common.GETDATE()) AS 'Edad',
		RTRIM(A.CODCENATE) AS 'CodCentroAtencion',
        RTRIM(CA.NOMCENATE) AS 'CentroAtencion',
        RTRIM(U.UFUDESCRI) AS 'UnidadFuncional',
        RTRIM(G.DESCCAMAS) AS 'Cama', 
        RTRIM(C.DESPRODUC) AS 'ComponenteLacteo',
        CASE C.NOPOSPROD WHEN 1 THEN 'No' WHEN 0 THEN 'Sí' END AS 'PBS', 
        A.DESADMINI AS 'Indicaciones',
		1 AS 'Tipo',
        'FORMULA LACTEA' AS 'TipoComponente',
		HIS.INDICAPAC AS 'EstadoHistoria',
		CAST(0 AS bit) AS Seleccion,
		CASE 
			WHEN HIS.INDICAPAC IN (9, 10, 11, 12, 15, 16) THEN 'Egreso del paciente'
			WHEN A.StatusFormulaSairy = 3 THEN 'Anulada leche materna'
        ELSE NULL
		END AS ALERTA,
		IIF(HIS.INDICAPAC IN (9, 10, 11, 12, 15, 16) OR A.StatusFormulaSairy = 3, CAST(1 AS BIT), CAST(0 AS BIT)) AS Anulado,
		Report.GetReportName(HISP.ID) AS Reporte
    FROM HCPRESCRA A 
    INNER JOIN IHLISTPRO C WITH(NOLOCK) ON A.CODPRODUC = C.CODPRODUC 
    INNER JOIN INPACIENT P ON A.IPCODPACI = P.IPCODPACI
    INNER JOIN INUNIFUNC U WITH(NOLOCK) ON A.UFUCODIGO = U.UFUCODIGO 
    INNER JOIN ADCENATEN CA WITH(NOLOCK) ON A.CODCENATE = CA.CODCENATE
    INNER JOIN ADINGRESO E WITH(NOLOCK) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
	OUTER APPLY (
		SELECT TOP 1 *
		FROM HCHISPACA H WITH(NOLOCK)
		WHERE H.IPCODPACI = A.IPCODPACI AND H.NUMINGRES = A.NUMINGRES
		ORDER BY H.FECHISPAC DESC
	) HIS
	INNER JOIN HCHISPACA HISP ON HISP.NUMEFOLIO = A.NUMEFOLIO AND HISP.IPCODPACI = A.IPCODPACI AND A.NUMINGRES = HISP.NUMINGRES
    LEFT JOIN CHCAMASHO G WITH(NOLOCK) ON IIF(E.CODCAMACT IS NULL,dbo.MotherCurrentBed(A.IPCODPACI, A.NUMINGRES),E.CODCAMACT) = G.CODICAMAS
    WHERE A.FormulaSairy = 1 
	AND A.StatusFormulaSairy IN (1,3) 
	AND A.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND HISP.GENCONEXT = 0
	AND NOT EXISTS (SELECT 1 FROM Lactation.LactealFormulaPreparationDetails LD WHERE LD.IdHCPRESCRA = A.id)

    UNION ALL

    -- Consulta 2: LECHE MATERNA (de BreastMilkOrder)
    SELECT 
        O.Id,
        O.Status AS 'ESTADO',      
        O.OrderDate AS 'FechaOrden',
        RTRIM(O.IPCODPACI) AS 'Identificacion',
		O.NUMINGRES,
		O.NUMEFOLIO,
        RTRIM(P.IPNOMCOMP) AS 'Paciente',
        P.IPFECNACI AS 'FechaNacimiento',
        dbo.edad(P.IPFECNACI, common.GETDATE()) AS 'Edad',
		RTRIM(O.CareCenterCode) AS 'CodCentroAtencion',
        RTRIM(CA.NOMCENATE) AS 'CentroAtencion',
        RTRIM(U.UFUDESCRI) AS 'UnidadFuncional',
        RTRIM(G.DESCCAMAS) AS 'Cama', 
        RTRIM(CH.DESTIPDIE) AS 'ComponenteLacteo',
        'No aplica' AS 'PBS',
        O.AdministrationInstructions AS 'Indicaciones',
		2 AS 'Tipo',
        'LECHE MATERNA' AS 'TipoComponente',
		HIS.INDICAPAC AS 'EstadoHistoria',
		CAST(0 AS bit) AS Seleccion,
		CASE 
			WHEN HIS.INDICAPAC IN (9, 10, 11, 12, 15, 16) THEN 'Egreso del paciente'
			WHEN O.Status = 3 THEN 'Anulada leche materna'
        ELSE NULL
		END AS ALERTA,
		IIF(HIS.INDICAPAC IN (9, 10, 11, 12, 15, 16) OR O.Status = 3, CAST(1 AS BIT), CAST(0 AS BIT)) AS Anulado,
		Report.GetReportName(HIS.ID) AS Reporte		
    FROM Lactation.BreastMilkOrder O
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
	LEFT JOIN CHCAMASHO G WITH(NOLOCK) ON IIF(E.CODCAMACT IS NULL,dbo.MotherCurrentBed(O.IPCODPACI, O.NUMINGRES),E.CODCAMACT) = G.CODICAMAS	
    WHERE 
	O.CareCenterCode IN (SELECT Value FROM dbo.splitstring(@CentroAtencion))
	AND O.Status IN (1,3) AND HIS.GENCONEXT = 0
	AND NOT EXISTS (SELECT 1 FROM Lactation.BreastMilkPreparationDetails LD WHERE LD.idBreastMilkOrder = O.id)

    ORDER BY TipoComponente, FechaOrden
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Retorna las solicitudes nuevas —tanto de fórmula láctea (HCPRESCRA) como de leche materna (Lactation.BreastMilkOrder)— que aún no tienen registro de preparación en el lactario, filtradas por centro de atención y con estado activo o anulado (1 y 3). Consolida datos del paciente, cama, unidad funcional, componente lácteo e indicaciones, e incluye alertas cuando el paciente ha egresado o la orden fue anulada. Sirve como fuente del dashboard del lactario para identificar órdenes pendientes de procesar.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesNuevasLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesNuevasLactario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes nuevas (no preparadas aún) de fórmula láctea y leche materna para el dashboard del lactario, filtradas por uno o varios centros de atención.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesNuevasLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@CentroAtencion debe contener uno o más códigos de centro de atención separables por dbo.splitstring.; Existen registros en HCHISPACA asociados al ingreso del paciente para evaluar estado de historia.; Las órdenes deben tener su episodio (ADINGRESO) y paciente (INPACIENT) registrados.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesNuevasLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan órdenes en estado 1 (solicitada) o 3 (anulada).; Se excluyen órdenes que ya tienen detalle de preparación asociado en el lactario.; Solo se consideran historias clínicas con GENCONEXT=0 (sin contexto generado/cerrado).; El resultado combina ambos tipos (Tipo=1 fórmula láctea, Tipo=2 leche materna) y se ordena por TipoComponente y FechaOrden.; La edad del paciente se calcula con dbo.edad respecto a common.GETDATE().', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesNuevasLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Fórmula láctea; Leche materna; Lactario; Prescripción; Centro de atención; Unidad funcional; Cama hospitalaria; Cama de la madre; Historia clínica; Egreso del paciente; Orden anulada; PBS (Plan Básico de Salud); Dieta; Paciente hospitalizado', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesNuevasLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCPRESCRA: Devuelve órdenes con FormulaSairy=1 y StatusFormulaSairy IN (1,3), del centro indicado, cuya historia HCHISPACA tenga GENCONEXT=0 y que no tengan registro en Lactation.LactealFormulaPreparationDetails.; [RETURN_RESULT] Lactation.BreastMilkOrder: Devuelve órdenes con Status IN (1,3), del centro indicado, cuya historia HCHISPACA tenga GENCONEXT=0 y que no tengan registro en Lactation.BreastMilkPreparationDetails.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesNuevasLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HIS.INDICAPAC IN (9,10,11,12,15,16) → Marca ALERTA=''Egreso del paciente'' y Anulado=1 else Si StatusFormulaSairy=3 o Status=3: ALERTA=''Anulada leche materna'' y Anulado=1; en otro caso ALERTA=NULL y Anulado=0; si C.NOPOSPROD = 1 (en fórmula láctea) → PBS=''No'' else Si NOPOSPROD=0 entonces PBS=''Sí''; si E.CODCAMACT IS NULL → Resuelve la cama mediante dbo.MotherCurrentBed (cama de la madre) else Usa E.CODCAMACT como cama actual', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesNuevasLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.edad; common.GETDATE; dbo.MotherCurrentBed; Report.GetReportName', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesNuevasLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCPRESCRA; IHLISTPRO; INPACIENT; INUNIFUNC; ADCENATEN; ADINGRESO; HCHISPACA; CHCAMASHO; Lactation.LactealFormulaPreparationDetails; Lactation.BreastMilkOrder; CHTIPDIET; Lactation.BreastMilkPreparationDetails', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesNuevasLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesNuevasLactario';
-- GO
