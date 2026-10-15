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
    INNER JOIN HCHISPACA HIS WITH(NOLOCK) ON  O.HCHISPACAId = HIS.Id
	LEFT JOIN CHCAMASHO G WITH(NOLOCK) ON IIF(E.CODCAMACT IS NULL,dbo.MotherCurrentBed(O.IPCODPACI, O.NUMINGRES),E.CODCAMACT) = G.CODICAMAS
	
    WHERE 
	O.CareCenterCode IN (SELECT Value FROM dbo.splitstring(@CentroAtencion))
	AND O.Status IN (1,3) AND HIS.GENCONEXT = 0
	AND NOT EXISTS (SELECT 1 FROM Lactation.BreastMilkPreparationDetails LD WHERE LD.idBreastMilkOrder = O.id)

    ORDER BY TipoComponente, FechaOrden
END