
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
			INNER JOIN HCHISPACA HIS WITH(NOLOCK) ON  O.HCHISPACAId = HIS.Id
			INNER JOIN HCREGDIET DIE ON DIE.IPCODPACI = O.IPCODPACI AND O.NUMINGRES = DIE.NUMINGRES AND DIE.NUMEFOLIO = O.NUMEFOLIO
			LEFT JOIN CHCAMASHO G WITH(NOLOCK) ON IIF(E.CODCAMACT IS NULL,dbo.MotherCurrentBed(O.IPCODPACI, O.NUMINGRES),E.CODCAMACT) = G.CODICAMAS	
    WHERE 
	O.CareCenterCode IN (SELECT Value FROM dbo.splitstring(@CentroAtencion))
	AND OD.Status NOT IN (5,6,7)

    ORDER BY FechaOrden
END