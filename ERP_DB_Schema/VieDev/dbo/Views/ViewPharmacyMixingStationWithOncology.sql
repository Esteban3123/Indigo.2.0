CREATE VIEW [dbo].[ViewPharmacyMixingStationWithOncology]
AS

WITH Cte_MainDataHC AS(
		SELECT	hc.CODCONCEC,
				hc.CODCONCEP,
				hc.CODCONCES,
				hc.FECHAORDE,
				hc.CODCENATE,
				hc.UFUCODIGO,
				hc.CODCENCOS,
				hc.IPCODPACI,
				hc.NUMINGRES,
				hc.NUMEFOLIO,
				hc.IDETIPHIS,
				hc.ORDTRANUE,
				ISNULL(hc.TIPOSOLICITUD, 0) TIPOSOLICITUD,
				hc.MEDICAMENTOVALIDADO,
				hc.CODBODEGA,
				hc.CODPROSAL,
				hc.ORDESTADO,
				hc.ORDENQUIMIO
		FROM HCFARMEPC hc WITH(NOLOCK)
		WHERE hc.ORDESTADO = '1'
			AND hc.IDAGEPROGQX IS NULL
)	
, Cte_InfoDetail AS(
		SELECT hd.CODCONCEC, hd.CodeSusceptibleMixingStation,
				psms.ApplicationsNumber RequestedQuantity, 
					COUNT (
							DISTINCT CASE WHEN pd.IsDispensed = 1 
								THEN pd.GroupingCodeDose
							END 
						) DispensedQuantity
		FROM HCFARMEPD hd WITH(NOLOCK)
		JOIN MedicalHistory.ProductSusceptibleMixingStation psms WITH(NOLOCK) ON hd.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation
		JOIN MedicalHistory.PharmaDose pd WITH(NOLOCK) ON psms.CodeSusceptibleMixingStation = pd.CodeSusceptibleMixingStation
		WHERE hd.SENDTO IN (0,2) 
		GROUP BY hd.CODCONCEC, psms.ApplicationsNumber, hd.CodeSusceptibleMixingStation
		HAVING psms.ApplicationsNumber > COUNT (DISTINCT CASE WHEN pd.IsDispensed = 1 THEN pd.GroupingCodeDose END)
)
, Cte_ControlDispesed AS (
		SELECT 
			hd.CODCONCEC, 
			SUM(hd.RequestedQuantity)RequestedQuantity, 
			SUM(hd.DispensedQuantity) DispensedQuantity
		FROM Cte_InfoDetail hd
		GROUP BY hd.CODCONCEC
		HAVING SUM(hd.RequestedQuantity) > SUM(hd.DispensedQuantity)
)
SELECT	hc.CODCONCEC,
		hc.CODCONCEP,
		hc.CODCONCES,
		hc.FECHAORDE, 
		hc.CODCENATE, 
		hc.UFUCODIGO,
		hc.CODCENCOS,
		hc.IPCODPACI,
		hc.NUMINGRES,
		hc.NUMEFOLIO,
		hc.IDETIPHIS,
		hc.ORDTRANUE, 
		ISNULL(hc.TIPOSOLICITUD, 0) TIPOSOLICITUD,
		hc.MEDICAMENTOVALIDADO,
		hc.CODBODEGA,
		hc.CODPROSAL,
		ing.CODCAMACT,
		hc.ORDESTADO,
		ISNULL(cg.ExtramuralPharmaceuticalDispensing,0) TIPOSOLICITUD1, 
		cg.ExtramuralPharmaceuticalDispensing as PERMITEEXTRA,
		1 RoutingMP
FROM Cte_MainDataHC hc
JOIN Cte_ControlDispesed cd ON hc.CODCONCEC = cd.CODCONCEC
JOIN dbo.ADINGRESO ing WITH(NOLOCK) ON hc.NUMINGRES = ing.NUMINGRES 
LEFT JOIN Contract.CareGroup cg WITH(NOLOCK) ON ing.GENCAREGROUP = cg.Id
WHERE	ISNULL(hc.ORDENQUIMIO, 0) = 0
		AND (ing.IESTADOIN NOT IN('F','C') OR (hc.TIPOSOLICITUD = 2 AND ing.TRATAESPECIA <> 3))

UNION ALL

-- No se encontró documentación de este escenario, estos son Ingresos ambulatorios que sean de oncología pero no quimioterapia
SELECT	hc.CODCONCEC,
		hc.CODCONCEP,
		hc.CODCONCES,
		hc.FECHAORDE, 
		hc.CODCENATE, 
		hc.UFUCODIGO,
		hc.CODCENCOS,
		hc.IPCODPACI,
		hc.NUMINGRES,
		hc.NUMEFOLIO,
		hc.IDETIPHIS,
		hc.ORDTRANUE, 
		ISNULL(hc.TIPOSOLICITUD, 0) TIPOSOLICITUD,
		hc.MEDICAMENTOVALIDADO,
		hc.CODBODEGA,
		hc.CODPROSAL,
		ing.CODCAMACT,
		hc.ORDESTADO,
		ISNULL(cg.ExtramuralPharmaceuticalDispensing,0) TIPOSOLICITUD1, 
		cg.ExtramuralPharmaceuticalDispensing as PERMITEEXTRA,
		1 RoutingMP
FROM Cte_MainDataHC hc
JOIN Cte_ControlDispesed cd ON hc.CODCONCEC = cd.CODCONCEC
JOIN dbo.ADINGRESO ing WITH(NOLOCK) ON hc.NUMINGRES = ing.NUMINGRES AND ing.TRATAESPECIA = 3
LEFT JOIN Contract.CareGroup cg WITH(NOLOCK) ON ing.GENCAREGROUP = cg.Id
WHERE	ISNULL(hc.ORDENQUIMIO, 0) = 0 
		AND hc.TIPOSOLICITUD = 2
		AND ing.IESTADOIN NOT IN('F','C')

UNION ALL

--Procesos Oncológicos de Quimioterapia
SELECT hc.CODCONCEC,
		hc.CODCONCEP,
		hc.CODCONCES,
		hc.FECHAORDE, 
		hc.CODCENATE, 
		hc.UFUCODIGO,
		hc.CODCENCOS,
		hc.IPCODPACI,
		hc.NUMINGRES,
		hc.NUMEFOLIO,
		hc.IDETIPHIS,
		hc.ORDTRANUE, 
		ISNULL(hc.TIPOSOLICITUD, 0) TIPOSOLICITUD,
		hc.MEDICAMENTOVALIDADO,
		hc.CODBODEGA,
		hc.CODPROSAL,
		ing.CODCAMACT,
		hc.ORDESTADO,
		ISNULL(cg.ExtramuralPharmaceuticalDispensing,0) TIPOSOLICITUD1, 
		cg.ExtramuralPharmaceuticalDispensing as PERMITEEXTRA,
		1 RoutingMP
FROM Cte_MainDataHC hc
JOIN Cte_ControlDispesed cd ON hc.CODCONCEC = cd.CODCONCEC
JOIN dbo.ADINGRESO ing WITH(NOLOCK) ON hc.NUMINGRES = ing.NUMINGRES
JOIN Contract.CareGroup cg WITH(NOLOCK) ON ing.GENCAREGROUP = cg.Id
WHERE ISNULL(hc.ORDENQUIMIO, 0) = 1
		AND ing.IESTADOIN NOT IN('F','C')