
CREATE PROCEDURE [Lactation].[SP_HC_ListarSolicitudesFormulaLactea]
    @CentroAtencion VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Consulta 1: FÓRMULA LÁCTEA (de HCPRESCRA)
    SELECT	
	CAST(0 AS bit) AS Seleccion,
        A.Id AS Id,
		F.Id AS IdDetalle,
		A.StatusFormulaSairy AS 'ESTADO',
		F.Status AS 'EstadoFormula',
		CASE F.Status WHEN 1 THEN '1 - Asignadas' WHEN 2 THEN  '2 - En preparación' WHEN 3 THEN '3 - Preparadas' WHEN 4 THEN '4 - Entregadas a la unidad' END AS 'EstadoLabel',
        A.FECINIDOS AS 'FechaOrden',
        RTRIM(A.IPCODPACI) AS 'Identificacion',
		RTRIM(A.NUMINGRES) AS 'Ingreso',
        RTRIM(P.IPNOMCOMP) AS 'Paciente',
        dbo.edad(P.IPFECNACI, common.GETDATE()) AS 'Edad',
		RTRIM(A.CODCENATE) AS 'CodCentroAtencion',
        RTRIM(CA.NOMCENATE) AS 'CentroAtencion',
		RTRIM(A.UFUCODIGO) AS 'CodUnidadFuncional',
        RTRIM(U.UFUDESCRI) AS 'UnidadFuncional',
        RTRIM(G.DESCCAMAS) AS 'Cama', 
        RTRIM(C.DESPRODUC) AS 'ComponenteLacteo',
		A.DESADMINI AS 'DosisPrescrita',
		A.INDAPLMED AS 'InstruccionesAdicionales',
		A.DOSISPROD AS 'Dosisprod',
		A.CODUNIMED AS 'CodigoUnidadMedida',
		CASE C.TIPFORMED WHEN 1 THEN C.PESTOTMED WHEN 2 THEN C.VOLTOTMED WHEN 3 
		THEN IIF(C.CODUNIPES IS NOT NULL,C.PESTOTMED,IIF(C.CODUNIVOL IS NOT NULL,C.VOLTOTMED,0))  END AS 'ConcentracionMed',
		CASE C.TIPFORMED WHEN 1 THEN C.CODUNIPES WHEN 2 THEN C.CODUNIVOL WHEN 3 
		THEN IIF(C.CODUNIPES IS NOT NULL,C.CODUNIPES,IIF(C.CODUNIVOL IS NOT NULL,C.CODUNIVOL,0))  END AS 'CodigoUnidadMedidaMed',
		CAST(0 AS int) AS 'UnidadesTotalesCreadasXFormula',
		IIF((SELECT SUM(LF.QuantityPrepared) FROM Lactation.LactealFormulaPreparationDetails LF WHERE LF.IDHCPRESCRA = A.ID) IS NULL, CAST(0 AS int), (SELECT SUM(LF.QuantityPrepared) FROM Lactation.LactealFormulaPreparationDetails LF WHERE LF.IDHCPRESCRA = A.ID)) AS 'UnidadesEntregadas',
		CAST(0 AS int) AS 'UnidadesPendientes',
        CASE C.NOPOSPROD WHEN 1 THEN 'No' WHEN 0 THEN 'Sí' END AS 'PBS', 
        J.CODMINSALUD AS 'CodigoMIPRES',
		IIF(C.NOPOSPROD = 0, NULL, IIF((J.CODMINSALUD IS NULL), NULL, J.CANPEDPRO))  AS 'CantidadAutorizada',
		HIS.INDICAPAC AS 'EstadoHistoria',
		CASE 
			WHEN HIS.INDICAPAC IN (12) THEN 'Fallecimiento'
			WHEN HIS.INDICAPAC IN (9, 10, 11, 15, 16) THEN 'Egreso del paciente'
			WHEN A.StatusFormulaSairy IN (3, 4) THEN 'Suspensión de la fórmula láctea'
        ELSE NULL
		END AS ALERTA, 
		IIF(HIS.INDICAPAC IN (12) OR HIS.INDICAPAC IN (9, 10, 11, 15, 16) OR A.StatusFormulaSairy IN (3, 4), CAST(1 AS bit), CAST(0 AS bit)) AS 'Anulado',
		F.DateFormulaPreparationStart AS 'DateFormulaPreparationStart',
		F.DateFormulaPreparationEnd AS 'DateFormulaPreparationEnd'
    FROM HCPRESCRA A 
	INNER JOIN Lactation.LactealFormulaPreparationDetails F ON A.ID = F.IdHCPRESCRA
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
	LEFT JOIN HCJUNOPOM J With(NOLOCK) ON J.NUMEFOLIO = A.NUMEFOLIO AND J.IPCODPACI = A.IPCODPACI AND J.NUMINGRES = A.NUMINGRES AND J.CODPRODUC = A.CODPRODUC AND J.EXTRAMURAL = A.MANEXTPRO
    LEFT JOIN CHCAMASHO G WITH(NOLOCK) ON IIF(E.CODCAMACT IS NULL,dbo.MotherCurrentBed(A.IPCODPACI, A.NUMINGRES),E.CODCAMACT) = G.CODICAMAS
    WHERE A.FormulaSairy = 1 AND F.Status NOT IN (5,6,7) AND A.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 

    ORDER BY EstadoFormula, FechaOrden
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que lista las solicitudes de fórmula láctea activas, filtrando por centro de atención y excluyendo estados cancelados/finalizados (5,6,7). Consolida datos de prescripción, paciente, ubicación (unidad funcional y cama), estado de preparación del lactario y cantidades preparadas/entregadas. Genera alertas automáticas cuando el paciente ha egresado o fallecido, o cuando la fórmula está suspendida, marcando el registro como anulado según corresponda.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesFormulaLactea';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesFormulaLactea';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes activas de fórmula láctea (con su detalle de preparación) para uno o varios centros de atención, enriquecidas con datos del paciente, ingreso, cama, MIPRES y alertas clínicas.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesFormulaLactea';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro @CentroAtencion debe contener uno o varios códigos de centro de atención separados (procesables por dbo.splitstring).; Las prescripciones consideradas deben estar marcadas como fórmula láctea (HCPRESCRA.FormulaSairy = 1).; Debe existir al menos un detalle de preparación en Lactation.LactealFormulaPreparationDetails asociado a la prescripción (INNER JOIN por IdHCPRESCRA).', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesFormulaLactea';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen prescripciones marcadas explícitamente como fórmula láctea (FormulaSairy=1).; Se excluyen detalles de preparación con Status 5, 6 o 7 (estados finales/cancelados no visibles en el listado).; UnidadesEntregadas nunca es NULL: si SUM(QuantityPrepared) es NULL se devuelve 0.; Solo se considera la última historia clínica del ingreso (TOP 1 ordenado por FECHISPAC DESC) para determinar el estado del paciente.; El indicador Anulado se activa cuando hay fallecimiento, egreso o suspensión de la fórmula, manteniendo coherencia con el campo ALERTA.; La cantidad autorizada MIPRES solo aplica para productos NO PBS (NOPOSPROD=0) que tengan código MINSALUD asociado en HCJUNOPOM.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesFormulaLactea';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Fórmula láctea; Prescripción médica; Preparación de fórmula; Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Cama hospitalaria; Componente lácteo; Dosis prescrita; PBS/No PBS; MIPRES; Cantidad autorizada; Egreso del paciente; Fallecimiento; Suspensión de fórmula; Historia clínica; Edad del paciente; Lactario', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesFormulaLactea';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCPRESCRA: Devuelve un resultset con prescripciones donde FormulaSairy = 1, F.Status NOT IN (5,6,7) y CODCENATE pertenece a la lista derivada de @CentroAtencion, ordenado por EstadoFormula y FechaOrden.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesFormulaLactea';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si F.Status (estado del detalle de preparación) → Se etiqueta como ''1 - Asignadas'', ''2 - En preparación'', ''3 - Preparadas'' o ''4 - Entregadas a la unidad'' según el valor 1..4.; si C.TIPFORMED (tipo de formulación del producto: 1=peso, 2=volumen, 3=mixto) → Se selecciona PESTOTMED/CODUNIPES (peso), VOLTOTMED/CODUNIVOL (volumen) o se prioriza peso sobre volumen cuando es tipo 3.; si HIS.INDICAPAC = 12 → Marca alerta ''Fallecimiento'' y campo Anulado=1. else Sigue evaluando otras condiciones de alerta.; si HIS.INDICAPAC IN (9,10,11,15,16) → Marca alerta ''Egreso del paciente'' y campo Anulado=1.; si A.StatusFormulaSairy IN (3,4) → Marca alerta ''Suspensión de la fórmula láctea'' y campo Anulado=1. else ALERTA = NULL y Anulado=0.; si C.NOPOSPROD = 0 (producto no POS / no PBS) y existe J.CODMINSALUD → Devuelve J.CANPEDPRO como CantidadAutorizada; en otro caso devuelve NULL.; si E.CODCAMACT IS NULL → Resuelve la cama del paciente vía dbo.MotherCurrentBed(IPCODPACI, NUMINGRES). else Usa E.CODCAMACT como cama actual.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesFormulaLactea';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.edad; common.GETDATE; dbo.MotherCurrentBed; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesFormulaLactea';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCPRESCRA; Lactation.LactealFormulaPreparationDetails; IHLISTPRO; INPACIENT; INUNIFUNC; ADCENATEN; ADINGRESO; HCHISPACA; HCJUNOPOM; CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesFormulaLactea';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesFormulaLactea';
-- GO
