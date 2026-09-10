

CREATE view [MixingStation].[ViewItemsCampaigns] 
as 

with dataPatients as (
	select rdp.CampaignDetailId, rdp.RequestMixingStationDetailId, SUM(rdp.Quantity) Quantity
	from MixingStation.RequestMixingStationDetailPatients rdp with(nolock)
	where rdp.CampaignDetailId is not null
	group by rdp.CampaignDetailId, rdp.RequestMixingStationDetailId
), 
dataDetail as (
	select ISNULL(dataPatients.CampaignDetailId, rd.CampaignDetailId) CampaignDetailId,
	ISNULL(dataPatients.Quantity, rd.Quantity) Quantity,
	rd.ATCId, rd.PackageId, rd.PackagePersonalizedId
	from MixingStation.RequestMixingStationDetail rd with(nolock)
	left join dataPatients with(nolock) on dataPatients.RequestMixingStationDetailId = rd.Id
	where rd.CampaignDetailId is not null or dataPatients.CampaignDetailId is not null
)

select CONCAT(cd.Id, '') Id,
dataDetail.CampaignDetailId,
IIF(pp.Id is not null, 3, IIF(p.Id is not null, 1, 2)) ItemType, 
IIF(pp.Id is not null, 'Paquete Personalizado', IIF(p.Id is not null, 'Paquete', 'Medicamento')) ItemTypeName, 
ISNULL(pp.Id, ISNULL(p.Id, a.Id)) ItemId, 
ISNULL(pp.Code + ' - ' + pp.Name, ISNULL(p.Code + ' - ' + p.Name, a.Code + ' - ' + a.Name)) ItemCodeName,
dataDetail.Quantity
from MixingStation.CampaignDetail cd with(nolock)
inner join dataDetail on dataDetail.CampaignDetailId = cd.Id
left join Inventory.ATC a with(nolock) on a.Id = dataDetail.ATCId
left join MixingStation.Package p with(nolock) on p.Id = dataDetail.PackageId
left join MixingStation.PackagePersonalized pp with(nolock) on pp.Id = dataDetail.PackagePersonalizedId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolida los ítems (medicamentos, paquetes o paquetes personalizados) asociados a cada lote de campaña de la estación de mezclas farmacéuticas. Para cada campaña, determina qué producto se va a preparar —clasificándolo como Medicamento, Paquete o Paquete Personalizado— y calcula la cantidad total requerida, priorizando la cantidad asignada por paciente (desde RequestMixingStationDetailPatients) sobre la cantidad general del detalle de solicitud (RequestMixingStationDetail). Se usa para reportería y control de producción de mezclas magistrales, mostrando de forma unificada qué insumo o preparado corresponde a cada campaña y cuánto debe elaborarse.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewItemsCampaigns';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewItemsCampaigns';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los ítems (medicamentos, paquetes o paquetes personalizados) asociados al detalle de cada campaña de la estación de mezclas, totalizando cantidades por paciente cuando aplique.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewItemsCampaigns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los detalles de RequestMixingStationDetail o RequestMixingStationDetailPatients deben tener CampaignDetailId no nulo para participar en el resultado.; CampaignDetail.Id debe existir para enlazar con el detalle agregado.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewItemsCampaigns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cantidad proveniente de pacientes (suma agrupada) tiene prioridad sobre la cantidad del detalle de solicitud cuando ambas existen.; Solo se incluyen detalles vinculados a una campaña (CampaignDetailId no nulo en alguna de las dos fuentes).; La clasificación del ítem es jerárquica: PackagePersonalized > Package > ATC.; El identificador del ítem (ItemId) y su código-nombre se toman siguiendo la misma jerarquía PackagePersonalized → Package → ATC.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewItemsCampaigns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de preparación (estación de mezclas); Detalle de campaña; Solicitud de mezcla; Paciente asignado a mezcla; Medicamento (ATC); Paquete farmacéutico; Paquete personalizado (mezcla magistral)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewItemsCampaigns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewItemsCampaigns: Devuelve una fila por CampaignDetail con su ítem clasificado: ItemType=3 si existe PackagePersonalized, =1 si existe Package, =2 si solo existe ATC (Medicamento).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewItemsCampaigns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rdp.CampaignDetailId is not null en RequestMixingStationDetailPatients → Se agrupa la cantidad por (CampaignDetailId, RequestMixingStationDetailId) y prevalece sobre la cantidad/CampaignDetailId del detalle de solicitud else Se usan CampaignDetailId y Quantity directamente desde RequestMixingStationDetail; si pp.Id is not null (PackagePersonalized existe) → ItemType=3 y ItemTypeName=''Paquete Personalizado'' else Si p.Id is not null → ItemType=1, ''Paquete''; en caso contrario ItemType=2, ''Medicamento'' (ATC)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewItemsCampaigns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStationDetailPatients; MixingStation.RequestMixingStationDetail; MixingStation.CampaignDetail; Inventory.ATC; MixingStation.Package; MixingStation.PackagePersonalized', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewItemsCampaigns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewItemsCampaigns';
GO
