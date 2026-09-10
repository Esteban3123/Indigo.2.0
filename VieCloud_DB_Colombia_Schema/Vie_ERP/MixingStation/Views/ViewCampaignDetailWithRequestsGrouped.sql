

CREATE VIEW [MixingStation].[ViewCampaignDetailWithRequestsGrouped]
AS

WITH packagedetail AS (
	SELECT pdw.Id AS PackageDetailId
		, pdw.PackageId
		, atc.Id AS ATCId
		, atc.Code
		, atc.AbbreviationName AS Name
	FROM MixingStation.PackageDetail pdw
	JOIN Inventory.ATC atc ON pdw.AtcId = atc.Id
	WHERE pdw.MainMedicine = 1
),
packagePdetail AS (
	SELECT pdw.Id AS PackagePersonalizedDetailId
		, pdw.PackagePersonalizedId
		, atc.Id AS ATCId
		, atc.Code
		, atc.AbbreviationName AS Name
	FROM MixingStation.PackagePersonalizedDetail pdw
	JOIN Inventory.ATC atc ON pdw.AtcId = atc.Id
	WHERE pdw.MainMedicine = 1
) 
select t.Id, t.Code, t.Name, t.CampaignDetailId, count(1) as Quantity
from (
SELECT 
	COALESCE(ppd.AtcId, pda.AtcId, a.Id) AS Id
	, COALESCE(ppd.Code, pda.Code, a.Code) AS Code
	, COALESCE(ppd.Name, pda.Name, a.Name) AS Name
	, cd.Id AS CampaignDetailId
FROM MixingStation.RequestMixingStationDetail rd
JOIN MixingStation.RequestMixingStation r on r.Id = rd.RequestMixingStationId
JOIN MixingStation.UnitDoseType udt on udt.Id = rd.UnitDoseTypeId
LEFT JOIN (
	select RequestMixingStationDetailId, SUM(Quantity) Quantity, CampaignDetailId, string_agg(Id, ', ') StringIds, Bed
	from MixingStation.RequestMixingStationDetailPatients
	where CampaignDetailId is not null and Status <> 3
	group by RequestMixingStationDetailId, CampaignDetailId, Bed
) rdp on rdp.RequestMixingStationDetailId = rd.Id
JOIN MixingStation.CampaignDetail cd WITH(NOLOCK) on ISNULL(rd.CampaignDetailId,rdp.CampaignDetailId) =cd.Id
LEFT JOIN MixingStation.Package p on p.Id = rd.PackageId
LEFT JOIN Inventory.ATC a on a.Id = rd.ATCId
LEFT JOIN MixingStation.PackagePersonalized pp on pp.Id = rd.PackagePersonalizedId
LEFT JOIN MixingStation.ProductionLine pl on pl.Id = rd.ProductionLineId
LEFT JOIN packagedetail pda on pda.PackageId = p.Id
LEFT JOIN packagePdetail ppd on ppd.PackagePersonalizedId = pp.Id
WHERE rdp.CampaignDetailId IS NOT NULL OR rd.CampaignDetailId IS NOT NULL
) as t
group by t.Id, t.Code, t.Name, t.CampaignDetailId

--WITH packagedetail AS (
--	SELECT pdw.Id AS PackageDetailId
--		, pdw.PackageId
--		, atc.Id AS ATCId
--		, atc.Code
--		, atc.AbbreviationName AS Name
--	FROM MixingStation.PackageDetail pdw
--	JOIN Inventory.ATC atc ON pdw.AtcId = atc.Id
--	WHERE pdw.MainMedicine = 1
--),
--packagePdetail AS (
--	SELECT pdw.Id AS PackagePersonalizedDetailId
--		, pdw.PackagePersonalizedId
--		, atc.Id AS ATCId
--		, atc.Code
--		, atc.AbbreviationName AS Name
--	FROM MixingStation.PackagePersonalizedDetail pdw
--	JOIN Inventory.ATC atc ON pdw.AtcId = atc.Id
--	WHERE pdw.MainMedicine = 1
--) 
--SELECT 
--	COALESCE(ppd.AtcId, pda.AtcId, a.Id) AS Id
--	, COALESCE(ppd.Code, pda.Code, a.Code) AS Code
--	, COALESCE(ppd.Name, pda.Name, a.Name) AS Name
--	, count(1) as Quantity
--	, cd.Id AS CampaignDetailId
--	--, *
--FROM MixingStation.RequestMixingStationDetail rd
--JOIN MixingStation.RequestMixingStation r on r.Id = rd.RequestMixingStationId
--JOIN MixingStation.UnitDoseType udt on udt.Id = rd.UnitDoseTypeId
--LEFT JOIN (
--	select RequestMixingStationDetailId, SUM(Quantity) Quantity, CampaignDetailId, string_agg(Id, ', ') StringIds, Bed
--	from MixingStation.RequestMixingStationDetailPatients
--	where CampaignDetailId is not null and Status <> 3
--	group by RequestMixingStationDetailId, CampaignDetailId, Bed
--) rdp on rdp.RequestMixingStationDetailId = rd.Id
--JOIN MixingStation.CampaignDetail cd WITH(NOLOCK) on ISNULL(rd.CampaignDetailId,rdp.CampaignDetailId) =cd.Id
--LEFT JOIN MixingStation.Package p on p.Id = rd.PackageId
--LEFT JOIN Inventory.ATC a on a.Id = rd.ATCId
--LEFT JOIN MixingStation.PackagePersonalized pp on pp.Id = rd.PackagePersonalizedId
--LEFT JOIN MixingStation.ProductionLine pl on pl.Id = rd.ProductionLineId
--LEFT JOIN packagedetail pda on pda.PackageId = p.Id
--LEFT JOIN packagePdetail ppd on ppd.PackagePersonalizedId = pp.Id
--WHERE rdp.CampaignDetailId IS NOT NULL OR rd.CampaignDetailId IS NOT NULL
--GROUP BY ppd.Code, ppd.Name, pda.Code, cd.Id, ppd.AtcId, pda.AtcId, ppd.Name, pda.Name, a.Id, a.Code, a.Name
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida, por campaña de producción (lote de mezclas), la cantidad total de solicitudes de preparación agrupadas por medicamento principal. Para cada ítem de campaña (CampaignDetailId) identifica el medicamento principal —ya sea un ATC directo, un paquete estándar o un paquete personalizado— resolviendo el código y nombre del fármaco con prioridad: paquete personalizado > paquete estándar > ATC libre. Combina los detalles de solicitud de la estación de mezclas (RequestMixingStationDetail) con los pacientes asignados (RequestMixingStationDetailPatients), excluyendo ítems cancelados (Status <> 3), y cuenta cuántas solicitudes activas existen por medicamento y por ítem de campaña. Es útil para reportería de producción en farmacia de mezclas: saber cuántas preparaciones de cada medicamento se requieren dentro de una campaña de dosificación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignDetailWithRequestsGrouped';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignDetailWithRequestsGrouped';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por detalle de campaña (CampaignDetail) los medicamentos principales (ATC) demandados por las solicitudes de la estación de mezclas, contando cuántas veces cada principio activo aparece asociado al lote de preparación.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsGrouped';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas MixingStation.PackageDetail y MixingStation.PackagePersonalizedDetail deben tener marcados sus componentes principales con MainMedicine = 1 para que sean considerados como ATC representativo del paquete.; Cada RequestMixingStationDetail debe estar referenciado a una CampaignDetail directamente (rd.CampaignDetailId) o indirectamente vía RequestMixingStationDetailPatients (rdp.CampaignDetailId) — de lo contrario se excluye.; Los ATC referenciados (en RequestMixingStationDetail, PackageDetail o PackagePersonalizedDetail) deben existir en Inventory.ATC.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsGrouped';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila resultante representa un principio activo (ATC) por detalle de campaña con su cantidad de apariciones en las solicitudes.; Solo se contabilizan medicamentos principales (MainMedicine = 1) cuando se resuelve el ATC vía paquete o paquete personalizado.; Los pacientes con Status = 3 en RequestMixingStationDetailPatients no son considerados en el cálculo.; La vista nunca devuelve filas sin CampaignDetailId — siempre hay una campaña asociada por el JOIN a CampaignDetail.; Se usa NOLOCK al leer CampaignDetail, asumiendo tolerancia a lecturas sucias para esta consulta de visualización.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsGrouped';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas (MixingStation); Campaña de preparación farmacéutica (CampaignDetail); Solicitud de mezcla (RequestMixingStation); Paciente asignado a preparación; Medicamento principal (MainMedicine); Clasificación ATC; Paquete / preparación magistral; Paquete personalizado; Dosis unitaria (UnitDoseType); Línea de producción', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsGrouped';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewCampaignDetailWithRequestsGrouped: Devuelve filas agregadas por (Id ATC, Code, Name, CampaignDetailId) con Quantity = COUNT(1) sobre los detalles de solicitud que pertenecen a esa campaña.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsGrouped';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MainMedicine = 1 en PackageDetail / PackagePersonalizedDetail → El componente se considera el medicamento principal (ATC representativo) del paquete o paquete personalizado en los CTEs packagedetail / packagePdetail. else Se ignora ese componente y no aporta ATC al paquete.; si Resolución del ATC del detalle de solicitud por prioridad: COALESCE(ppd.AtcId, pda.AtcId, a.Id) → Se toma primero el ATC principal del paquete personalizado; si no existe, el ATC principal del paquete estándar; en última instancia el ATC asignado directamente al detalle de la solicitud.; si Vinculación con CampaignDetail vía ISNULL(rd.CampaignDetailId, rdp.CampaignDetailId) → Si el detalle de solicitud tiene CampaignDetailId propio se usa ese; si no, se hereda el de la asignación de pacientes (RequestMixingStationDetailPatients).; si En RequestMixingStationDetailPatients: CampaignDetailId IS NOT NULL AND Status <> 3 → El registro de paciente se incluye en la agregación por campaña. else Se excluye (registros con Status = 3 — interpretados como anulados/cancelados — o sin campaña no participan).; si WHERE rdp.CampaignDetailId IS NOT NULL OR rd.CampaignDetailId IS NOT NULL → Solo se devuelven detalles de solicitud que estén ligados a alguna campaña (directa o por paciente). else Filas sin campaña son descartadas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsGrouped';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.PackageDetail; Inventory.ATC; MixingStation.PackagePersonalizedDetail; MixingStation.RequestMixingStationDetail; MixingStation.RequestMixingStation; MixingStation.UnitDoseType; MixingStation.RequestMixingStationDetailPatients; MixingStation.CampaignDetail; MixingStation.Package; MixingStation.PackagePersonalized; MixingStation.ProductionLine', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsGrouped';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsGrouped';
GO
