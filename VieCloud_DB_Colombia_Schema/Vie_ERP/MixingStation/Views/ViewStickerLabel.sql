CREATE VIEW [MixingStation].[ViewStickerLabel]
AS

WITH CTE_PharmaInfo AS (
    SELECT pd.GroupingCodeDose,
           psms.FunctionalUnitCode,
           fu.UFUDESCRI,
           hc.IPCODPACI,
           hc.NUMINGRES, 
           fu.UFUCODIGO,
           si.AdministrationName 
    FROM MedicalHistory.PharmaDose pd WITH (NOLOCK)
    JOIN MedicalHistory.ProductSusceptibleMixingStation psms WITH (NOLOCK) ON pd.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation
    JOIN HCFARMEPC hc WITH (NOLOCK) ON pd.IDHCFARMEPC = hc.CODCONCEC
    LEFT JOIN INUNIFUNC fu WITH (NOLOCK) ON psms.FunctionalUnitCode = fu.UFUCODIGO
    OUTER APPLY (SELECT * FROM MixingStation.GetSyringeInformation(psms.Origin, psms.IdOrigin)) si
    GROUP BY pd.GroupingCodeDose, psms.FunctionalUnitCode, fu.UFUDESCRI, hc.IPCODPACI, hc.NUMINGRES, fu.UFUCODIGO, si.AdministrationName
)

SELECT DISTINCT	rmsd.Id AS Id
		, cd.Id AS CampaignDetailId
		, cd.CampaignNumber
		, ISNULL(pp.Id, p.Id) AS PackageId
		, ISNULL(pp.[Name], p.[Name]) AS PackageName
		, ISNULL(pp.[Description], p.[Description]) AS PackageDescription
		, c.Main
		, c.Vehicle
		, c.Thinner
		, ISNULL(p.ProductId, 0) AS ProductId
		, ISNULL(ipr.SerialNumber, '') AS SerialNumber
		, rpds.BatchCode
		, rpds.Id AS RequestPackageDetailStatusId
		, COALESCE(pp.VolumeTotalPrepared, p.VolumeTotalPrepared, pp.VolumeTotalOrder, p.VolumeTotalOrder, 0)	AS VolumeTotalOrder
		, ISNULL(pp.VolumeTotalOrderMeasurementUnitId, p.VolumeTotalOrderMeasurementUnitId) AS VolumeTotalOrderMeasurementUnitId
		, ISNULL(imu.Abbreviation, imump.Abbreviation)  AS AbbreviationUnitTotalOrder
		, TRIM(pac.IPNOMCOMP) AS PatientName
		, pac.IPCODPACI AS PatientCode
		, pd.UFUDESCRI AS FunctionalUnitName
		, ISNULL(cma.NUMCAMHOS, '') AS Bed
		, pd.AdministrationName
		, rpds.BatchExpirationDate ExpirationDate
		, c.Dose Dosis
		, CAST(NULL AS VARCHAR(20)) DosisAbbreviation
		, IIF(COALESCE(pp.Concentration, p.Concentration, 0) = 0, ISNULL(pp.ConcentrationAntibiotic, p.ConcentrationAntibiotic), CONCAT(ISNULL(pp.Concentration, p.Concentration),' ', imuc.Abbreviation)) AS Concentration
		, cduQC.Nombre AS QualityChemist
		, cduPC.Nombre AS ProductionChemist
FROM MixingStation.CampaignDetail cd WITH(NOLOCK)
JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.CampaignDetailId = cd.Id
JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON rpds.RequestMixingStationDetailId = rmsd.Id
JOIN CTE_PharmaInfo pd  ON pd.GroupingCodeDose = rpds.GroupingCodeDose 
JOIN MixingStation.Package p WITH(NOLOCK) ON rmsd.PackageId = p.Id
lEFT JOIN MixingStation.PackagePersonalized pp WITH(NOLOCK) ON rmsd.PackagePersonalizedId = pp.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) ON ISNULL(pp.VolumeTotalOrderMeasurementUnitId, p.VolumeTotalOrderMeasurementUnitId) = imu.Id
LEFT JOIN Inventory.InventoryProduct ipr WITH(NOLOCK) ON p.ProductId = ipr.Id --PREGUNTAR
LEFT JOIN Inventory.InventoryMeasurementUnit imump WITH(NOLOCK) ON ISNULL(pp.MeasurementPreparedId, p.MeasurementPreparedId) = imump.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imuc WITH(NOLOCK) ON ISNULL(pp.ConcentrationMeasurementUnitId, p.ConcentrationMeasurementUnitId) = imuc.id
LEFT JOIN INPACIENT pac WITH(NOLOCK) ON pd.IPCODPACI= pac.IPCODPACI
LEFT JOIN ADINGRESO ing WITH(NOLOCK) ON pd.NUMINGRES = ing.NUMINGRES
LEFT JOIN CHCAMASHO cma WITH(NOLOCK) ON (CASE WHEN ing.CODCAMACT IS NULL OR ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END) = cma.CODICAMAS
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC (NOLOCK) ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 --QF Calidad
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC (NOLOCK) ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción 
OUTER APPLY (
	SELECT *
	FROM (
		SELECT 
			CASE pd.ComponentType
				WHEN 0 THEN concat(pd.Quantity, ' ', mu.Abbreviation)
				WHEN 1 THEN left(a.AbbreviationName, 27)
				WHEN 2 THEN concat(left(s.SupplieName, 27), ' - ', pd.Quantity, '/', mu.Abbreviation)
				WHEN 3 THEN concat(left(pr.Abbreviation, 27), ' - ', pd.Quantity, '/', mu.Abbreviation)
			END AS Item
			, case 				
				WHEN pd.Vehicle = 1 THEN 'Vehicle'
				WHEN pd.Thinner = 1 THEN 'Thinner'
				WHEN pd.ComponentType = 0 THEN 'Dose'
			END AS TypeItem
		FROM (
			SELECT pd.MeasurementUnitId, pd.AtcId, pd.SupplieId, pd.ProductId, pd.ComponentType, pd.Vehicle, pd.Thinner, pd.Quantity
			FROM MixingStation.PackagePersonalizedDetail pd WITH(NOLOCK)
			WHERE pp.Id IS NOT NULL AND pd.PackagePersonalizedId = pp.Id AND ISNULL(pd.MainMedicine, 0) = 0

			UNION

			SELECT pd.MeasurementUnitId, pd.AtcId, pd.SupplieId, pd.ProductId, pd.ComponentType, pd.Vehicle, pd.Thinner, pd.Quantity
			FROM MixingStation.PackageDetail pd WITH(NOLOCK)
			WHERE pp.Id IS NULL AND pd.PackageId = p.Id AND ISNULL(pd.MainMedicine, 0) = 0
		) pd
		JOIN Inventory.InventoryMeasurementUnit mu WITH(NOLOCK) ON pd.MeasurementUnitId = mu.Id
		LEFT JOIN Inventory.ATC a WITH(NOLOCK) ON pd.AtcId = a.Id
		LEFT JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON pd.SupplieId = s.Id
		LEFT JOIN Inventory.InventoryProduct pr WITH(NOLOCK) ON pd.ProductId = pr.Id

		UNION ALL

		SELECT 
			ISNULL(aggMain.Abbreviations, '') AS Item
			, 'Main' AS TypeItem
		FROM (
			SELECT 
				STRING_AGG(
					CASE 
						WHEN a.AbbreviationName IS NOT NULL AND LTRIM(RTRIM(a.AbbreviationName)) <> '' 
						THEN left(a.AbbreviationName, 27)
						ELSE NULL
					END, 
					' + '
				) WITHIN GROUP (ORDER BY pdMain.Id) AS Abbreviations
			FROM (
				SELECT pd.Id, pd.AtcId
				FROM MixingStation.PackagePersonalizedDetail pd WITH(NOLOCK)
				WHERE pp.Id IS NOT NULL AND pd.PackagePersonalizedId = pp.Id AND pd.MainMedicine = 1

				UNION ALL

				SELECT pd.Id, pd.AtcId
				FROM MixingStation.PackageDetail pd WITH(NOLOCK)
				WHERE pp.Id IS NULL AND pd.PackageId = p.Id AND pd.MainMedicine = 1
			) pdMain
			LEFT JOIN Inventory.ATC a WITH(NOLOCK) ON pdMain.AtcId = a.Id
		) aggMain

		UNION ALL

		SELECT 
			CONCAT(aggDose.TotalQuantity, ' ', aggDose.FirstUnitAbbreviation) AS Item
			, 'Dose' AS TypeItem
		FROM (
			SELECT 
				SUM(ISNULL(pdDose.Quantity, 0)) AS TotalQuantity,
				MIN(muDose.Abbreviation) AS FirstUnitAbbreviation
			FROM (
				SELECT pd.MeasurementUnitId, pd.Quantity
				FROM MixingStation.PackagePersonalizedDetail pd WITH(NOLOCK)
				WHERE pp.Id IS NOT NULL AND pd.PackagePersonalizedId = pp.Id AND pd.MainMedicine = 1

				UNION ALL

				-- Medicamentos principales de paquete estándar (MainMedicine = 1)
				SELECT pd.MeasurementUnitId, pd.Quantity
				FROM MixingStation.PackageDetail pd WITH(NOLOCK)
				WHERE pp.Id IS NULL AND pd.PackageId = p.Id AND pd.MainMedicine = 1
			) pdDose
			JOIN Inventory.InventoryMeasurementUnit muDose WITH(NOLOCK) ON pdDose.MeasurementUnitId = muDose.Id
			HAVING SUM(ISNULL(pdDose.Quantity, 0)) > 0
		) aggDose
	) c
	PIVOT
	(
		MAX(Item)
		FOR c.TypeItem IN (Vehicle, Thinner, Main, Dose)
	) piv
) c
WHERE rmsd.Status <> 3 AND rpds.Status <> 6 AND rmsd.LabelType = 3 AND rmsd.Source = 1

UNION ALL
	
	SELECT DISTINCT	rpds.Id AS Id
		, cd.Id AS CampaignDetailId
		, cd.CampaignNumber
		, ISNULL(p.Id, p.Id) AS PackageId
		, ISNULL(p.[Name], p.[Name]) AS PackageName
		, ISNULL(p.[Description], p.[Description]) AS PackageDescription
		, ISNULL(c.Main, a.Name) Main
		, c.Vehicle
		, c.Thinner
		, ISNULL(p.ProductId, 0) AS ProductId
		, ISNULL(ipr.SerialNumber, '') AS SerialNumber
		, rpds.BatchCode
		, rpds.Id AS RequestPackageDetailStatusId
		, COALESCE(p.VolumeTotalPrepared, p.VolumeTotalOrder, 0) AS VolumeTotalOrder
		, p.VolumeTotalOrderMeasurementUnitId
		, ISNULL(imu.Abbreviation, imump.Abbreviation)  AS AbbreviationUnitTotalOrder
		, NULL PatientName
		, NULL PatientCode
		, NULL FunctionalUnitName
		, NULL Bed
		, ad.Name AdministrationName
		, rpds.BatchExpirationDate ExpirationDate
		, c.Dose Dosis
		, CAST(NULL AS VARCHAR(20)) DosisAbbreviation
		, IIF(ISNULL(p.Concentration, 0) = 0, p.ConcentrationAntibiotic, CONCAT(p.Concentration,' ', imuc.Abbreviation)) AS Concentration
		, cduQC.Nombre AS QualityChemist
		, cduPC.Nombre AS ProductionChemist
	FROM MixingStation.RequestUnitDoseInventoryDetail rudid
	JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.EntityName = 'RequestUnitDoseInventoryDetail' AND rmsd.EntityId = rudid.Id
	JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.RequestMixingStationDetailId = rmsd.Id
	JOIN MixingStation.CampaignDetail cd ON cd.Id = rmsd.CampaignDetailId
	LEFT JOIN Inventory.ATC a ON a.Id = rudid.ATCId
	LEFT JOIN Inventory.AdministrationRoute ad ON ad.Id = a.AdministrationRouteId
	LEFT JOIN MixingStation.Package p ON p.Id = rudid.PackageId
	LEFT JOIN Inventory.InventoryProduct ipr WITH(NOLOCK) ON p.ProductId = ipr.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) ON p.VolumeTotalOrderMeasurementUnitId = imu.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imump WITH(NOLOCK) ON p.MeasurementPreparedId = imump.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imuc WITH(NOLOCK) ON p.ConcentrationMeasurementUnitId = imuc.id
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC (NOLOCK) ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 --QF Calidad
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC (NOLOCK) ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción 
	OUTER APPLY (
		SELECT *
		FROM (
			-- Componentes que NO son medicamentos principales (Vehicle, Thinner, Dose, Insumos, otros)
			SELECT 
				CASE pd.ComponentType
					WHEN 0 THEN concat(pd.Quantity, ' ', mu.Abbreviation)
					WHEN 1 THEN left(a.AbbreviationName, 27)
					WHEN 2 THEN concat(left(s.SupplieName, 27), ' - ', pd.Quantity, '/', mu.Abbreviation)
					WHEN 3 THEN concat(left(pr.Abbreviation, 27), ' - ', pd.Quantity, '/', mu.Abbreviation)
				END AS Item
				, case 				
					WHEN pd.Vehicle = 1 THEN 'Vehicle'
					WHEN pd.Thinner = 1 THEN 'Thinner'
					WHEN pd.ComponentType = 0 THEN 'Dose'
				END AS TypeItem
			FROM (
				SELECT pd.MeasurementUnitId, pd.AtcId, pd.SupplieId, pd.ProductId, pd.ComponentType, pd.Vehicle, pd.Thinner, pd.Quantity
				FROM MixingStation.PackageDetail pd WITH(NOLOCK)
				WHERE p.Id IS NOT NULL AND pd.PackageId = p.Id AND ISNULL(pd.MainMedicine, 0) = 0
			) pd
			JOIN Inventory.InventoryMeasurementUnit mu WITH(NOLOCK) ON pd.MeasurementUnitId = mu.Id
			LEFT JOIN Inventory.ATC a WITH(NOLOCK) ON pd.AtcId = a.Id
			LEFT JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON pd.SupplieId = s.Id
			LEFT JOIN Inventory.InventoryProduct pr WITH(NOLOCK) ON pd.ProductId = pr.Id

			UNION ALL

			SELECT 
				ISNULL(aggMain.Abbreviations, '') AS Item
				, 'Main' AS TypeItem
			FROM (
				SELECT 
					STRING_AGG(
						CASE 
							WHEN a.AbbreviationName IS NOT NULL AND LTRIM(RTRIM(a.AbbreviationName)) <> '' 
							THEN left(a.AbbreviationName, 27)
							ELSE NULL
						END, 
						' + '
					) WITHIN GROUP (ORDER BY pdMain.Id) AS Abbreviations
				FROM (
					SELECT pd.Id, pd.AtcId
					FROM MixingStation.PackageDetail pd WITH(NOLOCK)
					WHERE p.Id IS NOT NULL AND pd.PackageId = p.Id AND pd.MainMedicine = 1
				) pdMain
				LEFT JOIN Inventory.ATC a WITH(NOLOCK) ON pdMain.AtcId = a.Id
			) aggMain

			UNION ALL

			SELECT 
				CONCAT(aggDose.TotalQuantity, ' ', aggDose.FirstUnitAbbreviation) AS Item
				, 'Dose' AS TypeItem
			FROM (
				SELECT 
					SUM(ISNULL(pdDose.Quantity, 0)) AS TotalQuantity,
					MIN(muDose.Abbreviation) AS FirstUnitAbbreviation
				FROM (
					SELECT pd.MeasurementUnitId, pd.Quantity
					FROM MixingStation.PackageDetail pd WITH(NOLOCK)
					WHERE p.Id IS NOT NULL AND pd.PackageId = p.Id AND pd.MainMedicine = 1
				) pdDose
				JOIN Inventory.InventoryMeasurementUnit muDose WITH(NOLOCK) ON pdDose.MeasurementUnitId = muDose.Id
				HAVING SUM(ISNULL(pdDose.Quantity, 0)) > 0
			) aggDose
		) c
		PIVOT
		(
			MAX(Item)
			FOR c.TypeItem IN (Vehicle, Thinner, Main, Dose)
		) piv
	) c
	WHERE rmsd.Status <> 3 AND rpds.Status <> 6 AND rmsd.LabelType = 3 AND rmsd.Source = 4
																												   
GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida toda la información necesaria para imprimir la etiqueta (sticker) de cada bolsa o envase preparado en la estación de mezclas farmacéuticas. Integra datos del paciente (nombre, cédula o código, cama y unidad funcional), del ciclo de preparación (número de campaña, lote, fecha de vencimiento, número de serie, dosis, concentración, vehículo, diluyente y componentes del paquete), y del estado del paquete dentro del flujo de calidad y dispensación. Combina las órdenes médicas farmacéuticas (HCFARMEPC), las dosis prescritas (PharmaDose), los productos susceptibles de mezcla, el paquete estándar o personalizado con su detalle de componentes, el ingreso del paciente con su cama asignada, y los químicos farmacéuticos responsables de calidad y producción. Se usa para generar e imprimir la etiqueta física que acompaña cada preparación magistral o mezcla intravenosa entregada al servicio de hospitalización.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewStickerLabel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewStickerLabel';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información necesaria para imprimir el rótulo (sticker) de cada preparación farmacéutica de la estación de mezclas, combinando datos de paciente, paquete, componentes (vehículo, diluyente, principio activo, dosis), químicos responsables y datos de lote/vencimiento.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStickerLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de la solicitud (RequestMixingStationDetail) debe tener LabelType = 3 (tipo de etiqueta sticker).; El detalle no debe estar en Status = 3 ni el RequestPackageDetailStatus en Status = 6 (estados que excluyen del etiquetado).; Para el primer bloque, rmsd.Source debe ser 1 (origen relacionado con paciente/historia clínica) y debe existir un GroupingCodeDose vinculado en PharmaDose.; Para el segundo bloque, rmsd.Source debe ser 4 (origen de inventario de dosis unitaria) y la asociación se hace vía EntityName=''RequestUnitDoseInventoryDetail'' y EntityId.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStickerLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen registros con LabelType = 3 (sticker) y excluyendo Status=3 (rmsd) y Status=6 (rpds).; El campo DosisAbbreviation siempre se devuelve como NULL (CAST(NULL AS VARCHAR(20))).; La abreviatura ATC del principio activo se trunca a 27 caracteres (LEFT(...,27)).; PackageId/Name/Description usan PackagePersonalized cuando existe; si no, recurren a Package estándar.; El VolumeTotalOrder se prioriza en el orden VolumeTotalPrepared(personalizado) > VolumeTotalPrepared(estándar) > VolumeTotalOrder(personalizado) > VolumeTotalOrder(estándar) > 0.; En el bloque de dosis unitaria de inventario (Source=4), PatientName, PatientCode, FunctionalUnitName y Bed siempre son NULL.; Solo se consideran como componentes del Main aquellos con MainMedicine=1; los demás se reportan como Vehicle/Thinner/Dose/insumo según ComponentType.; El pivot agrega los ítems en columnas fijas: Vehicle, Thinner, Main, Dose.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStickerLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve una fila por sticker para cada RequestPackageDetailStatus elegible, con identificadores de campaña, paquete, paciente (si aplica), cama, dosis, concentración, lote y vencimiento.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStickerLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rmsd.Source = 1 y rmsd.LabelType = 3 → Genera la etiqueta para preparaciones asociadas a paciente: trae datos de paciente (INPACIENT), unidad funcional, cama (CHCAMASHO vía ADINGRESO) y vía de administración desde GetSyringeInformation.; si rmsd.Source = 4 y rmsd.LabelType = 3 → Genera la etiqueta para preparaciones de dosis unitaria de inventario (RequestUnitDoseInventoryDetail): no incluye paciente/cama/UF; el principio Main proviene de Inventory.ATC y la vía de administración de Inventory.AdministrationRoute.; si pp.Id IS NOT NULL (existe PackagePersonalized) → Los componentes se leen de PackagePersonalizedDetail filtrando por pp.Id. else Los componentes se leen de PackageDetail filtrando por p.Id (paquete estándar).; si ComponentType del detalle del paquete → 0=Dose (Cantidad+UM); 1=Main/ATC (abreviatura ATC); 2=Insumo (nombre+cantidad/UM); 3=Producto (abreviatura+cantidad/UM).; si MainMedicine = 1 en el detalle → El componente se agrega al ítem ''Main'' (concatenación de abreviaturas ATC con '' + '') y al ítem ''Dose'' (suma de cantidades con la primera UM).; si COALESCE(Concentration,0) = 0 → Se muestra ConcentrationAntibiotic. else Se muestra Concentration concatenada con la abreviatura de su unidad de medida.; si ing.CODCAMACT IS NULL OR = 0 → No se asocia cama (Bed = ''''). else Se busca la cama en CHCAMASHO mediante CAST de CODCAMACT a VARCHAR(15).; si cduQC.UserRole = 1 / cduPC.UserRole = 2 → Se identifican respectivamente al Químico Farmacéutico de Calidad (QualityChemist) y al de Producción (ProductionChemist) desde ViewUsersRolsCM.; si SUM(Quantity) de medicamentos principales > 0 → Se reporta el ítem ''Dose'' agregada; en caso contrario no se incluye (HAVING).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStickerLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'MixingStation.GetSyringeInformation', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStickerLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalHistory.PharmaDose; MedicalHistory.ProductSusceptibleMixingStation; HCFARMEPC; INUNIFUNC; MixingStation.CampaignDetail; MixingStation.RequestMixingStationDetail; MixingStation.RequestPackageDetailStatus; MixingStation.Package; MixingStation.PackagePersonalized; MixingStation.PackageDetail; MixingStation.PackagePersonalizedDetail; MixingStation.RequestUnitDoseInventoryDetail; Inventory.InventoryMeasurementUnit; Inventory.InventoryProduct; Inventory.InventorySupplie; Inventory.ATC; Inventory.AdministrationRoute; INPACIENT; ADINGRESO; CHCAMASHO; MixingStation.ViewUsersRolsCM', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStickerLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewStickerLabel';
GO
