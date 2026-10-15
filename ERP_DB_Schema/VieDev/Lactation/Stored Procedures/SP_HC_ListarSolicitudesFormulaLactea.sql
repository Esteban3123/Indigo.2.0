
CREATE PROCEDURE [Lactation].[SP_HC_ListarSolicitudesFormulaLactea]
    @CentroAtencion VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Consulta 1: FÓRMULA LÁCTEA (de HCPRESCRA)
    SELECT		
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
		C.PESTOTMED AS 'ConcentracionMed',
		C.CODUNIPES AS 'CodigoUnidadMedidaMed',
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
		WHERE H.IPCODPACI = A.IPCODPACI AND H.NUMEFOLIO = A.NUMINGRES
		ORDER BY H.FECHISPAC DESC
	) HIS
	LEFT JOIN HCJUNOPOM J With(NOLOCK) ON J.NUMEFOLIO = A.NUMEFOLIO AND J.IPCODPACI = A.IPCODPACI AND J.NUMINGRES = A.NUMINGRES AND J.CODPRODUC = A.CODPRODUC AND J.EXTRAMURAL = A.MANEXTPRO
    LEFT JOIN CHCAMASHO G WITH(NOLOCK) ON IIF(E.CODCAMACT IS NULL,dbo.MotherCurrentBed(A.IPCODPACI, A.NUMINGRES),E.CODCAMACT) = G.CODICAMAS
    WHERE A.FormulaSairy = 1 AND F.Status NOT IN (5,6,7) AND A.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 

    ORDER BY EstadoFormula, FechaOrden
END