
CREATE view [MixingStation].[ViewCampaignDetailUser]
AS

-- with campaignUser as (
--     SELECT cdu.UserId, cdu.CampaignDetailId, su.UserCode, p.Identification, p.Fullname, cdu.UserRole
--     FROM MixingStation.CampaignDetailUsers cdu
--     JOIN [Security].[User] su ON cdu.UserId = su.Id
--     JOIN [Security].Person p ON su.IdPerson = p.Id
-- ),
-- campaignRequest as (
--     SELECT rd.CampaignDetailId, SUM(rd.Quantity) AS Quantity
--     FROM MixingStation.RequestMixingStationDetail rd
--     WHERE rd.CampaignDetailId IS NOT NULL
--     GROUP BY rd.CampaignDetailId
-- )
-- SELECT
--     cd.Id,
--     CAST((CASE WHEN EXISTS (
--         SELECT 1 
--         FROM MixingStation.RequestPackageDetailStatus ds 
--         JOIN MixingStation.RequestMixingStationDetail rmd on ds.RequestMixingStationDetailId = rmd.Id 
--         WHERE rmd.CampaignDetailId = cd.Id AND ds.[Status] <> 2
--     ) THEN 0 ELSE 1 END) AS BIT) AS HasProductEnd,
--     cd.CampaignNumber,
--     cm.Id AS CMConfigurationId,
--     cm.Name AS CMConfigurationName,
--     cd.CampaignStatus,
--     ud.Id AS UnitDoseTypeId,
--     ud.Description AS UnitDoseTypeName,
--     pl.Id AS ProductionLineId,
--     pl.Name AS ProductionLineName,
--     cr.Quantity,
--     cd.CreationDate AS CampaignDate,
--     qfu.UserId AS QFQualityUserId,
--     qfu.UserCode AS QFQualityCode,
--     qfu.Fullname AS QFQualityName,
--     qfp.UserId AS QFProductionUserId,
--     qfp.UserCode AS QFProductionCode,
--     qfp.Fullname AS QFProductionName,
--     aux.UserId AS MSAuxId,
--     aux.UserCode AS MSAuxCode,
--     aux.Fullname AS MSAuxName,
--     tl.UserId AS TLUserId,
--     tl.UserCode AS TLCode,
--     tl.Fullname AS TLUserName,
--     CAST(IIF(rl.Id IS NULL, 0, 1) AS BIT) AS HasReleaseLine
-- FROM MixingStation.CampaignDetail cd
-- JOIN mixingstation.campaign c ON cd.campaignId = c.Id
-- JOIN MixingStation.CMConfiguration cm ON c.CMConfigurationId = cm.Id
-- JOIN MixingStation.UnitDoseType ud ON cd.UnitDoseTypeId = ud.Id
-- JOIN MixingStation.ProductionLine pl ON cd.ProductionLineId = pl.Id
-- JOIN campaignRequest cr ON cr.CampaignDetailId = cd.Id
-- LEFT JOIN MixingStation.ReleaseLine rl ON rl.CampaignDetailId = cd.Id
-- LEFT JOIN campaignUser AS campaignUser ON cd.Id = campaignUser.CampaignDetailId
-- LEFT JOIN ( SELECT UserId,UserCode,Fullname,CampaignDetailId from campaignUser where UserRole = 1) as qfu on qfu.CampaignDetailId = cd.id
-- LEFT JOIN ( SELECT UserId,UserCode,Fullname,CampaignDetailId from campaignUser where UserRole = 2) as qfp on qfp.CampaignDetailId = cd.id
-- LEFT JOIN ( SELECT UserId,UserCode,Fullname,CampaignDetailId from campaignUser where UserRole = 3) as aux on aux.CampaignDetailId = cd.id
-- LEFT JOIN ( SELECT UserId,UserCode,Fullname,CampaignDetailId from campaignUser where UserRole = 4) as tl on tl.CampaignDetailId = cd.id

WITH campaignUser AS (
	SELECT cdu.Id
		, cdu.UserId
		, su.UserCode
		, p.Identification
		, p.Fullname
		, cdu.CampaignDetailId
		, cdu.UserRole
	FROM MixingStation.CampaignDetailUsers cdu
	JOIN [Security].[User] su on cdu.UserId = su.Id
	JOIN [Security].Person p on su.IdPerson = p.Id
),
CampaignRequest AS
(
	SELECT 
		data.CampaignDetailId
		, SUM(data.Quantity) Quantity
	FROM
	(
		SELECT rd.CampaignDetailId, SUM(rd.Quantity) Quantity
		FROM MixingStation.RequestMixingStationDetail rd WITH (NOLOCK)
		WHERE rd.CampaignDetailId IS NOT NULL
		GROUP BY rd.CampaignDetailId
	) data 
	GROUP BY data.CampaignDetailId
)
SELECT cd.Id,
	cd.CampaignNumber,
	cm.Id as CMConfigurationId,
	cm.Name AS CMConfigurationName,
	cd.CampaignStatus,
	ud.Id as UnitDoseTypeId,
	ud.Description as UnitDoseTypeName,
	pl.Id as ProductionLineId,
	pl.Name as ProductionLineName,
	cr.Quantity,
	cd.CreationDate as CampaignDate,
	---------------------------------
	qfu.UserId as QFQualityUserId,
	qfu.UserCode as QFQualityCode,
	qfu.Fullname as QFQualityName,
	qfp.UserId as QFProductionUserId,
	qfp.UserCode as QFProductionCode,
	qfp.Fullname as QFProductionName,
	aux.UserId as MSAuxId,
	aux.UserCode as MSAuxCode,
	aux.Fullname as MSAuxName,
	tl.UserId as TLUserId,
	tl.UserCode as TLCode,
	tl.Fullname as TLUserName,
    CAST(IIF(rl.Id IS NULL, 0, 1) as BIT) AS HasReleaseLine,
    --CAST((CASE WHEN EXISTS (
    --    SELECT 1 
    --    FROM MixingStation.RequestPackageDetailStatus ds 
    --    JOIN MixingStation.RequestMixingStationDetail rmd on ds.RequestMixingStationDetailId = rmd.Id 
    --    WHERE rmd.CampaignDetailId = cd.Id AND ds.[Status] <> 2
    --) THEN 0 ELSE 1 END) AS BIT) AS HasProductEnd,
	CAST(CASE WHEN EXISTS(
		select 1 
		from [MixingStation].[ViewSumQuantityByPreparationStatus]
		where Id = cd.Id AND RequestedQuantity = ReleasedQuantity + RejectedQuantity + CancelledQuantity
	) THEN  1 ELSE 0 END AS BIT) as HasProductEnd,
    ud.MSClass As UnitDoseTypeClass
FROM MixingStation.CampaignDetail cd
JOIN mixingstation.campaign c on cd.campaignId = c.Id
JOIN MixingStation.CMConfiguration cm on c.CMConfigurationId = cm.Id
JOIN MixingStation.UnitDoseType ud on cd.UnitDoseTypeId = ud.Id
JOIN MixingStation.ProductionLine pl on cd.ProductionLineId = pl.Id
JOIN CampaignRequest cr ON cr.CampaignDetailId = cd.Id
LEFT JOIN MixingStation.ReleaseLine rl ON rl.CampaignDetailId = cd.Id
OUTER APPLY (
	SELECT * FROM campaignUser cdu WHERE cd.Id = cdu.CampaignDetailId and cdu.UserRole = 1
) AS qfu
OUTER APPLY (
	SELECT * from campaignUser cdu WHERE cd.Id = cdu.CampaignDetailId and cdu.UserRole = 2
) AS qfp
OUTER APPLY (
	SELECT * FROM campaignUser cdu WHERE cd.Id = cdu.CampaignDetailId and cdu.UserRole = 3
) AS aux
OUTER APPLY (
	SELECT * FROM campaignUser cdu WHERE cd.Id = cdu.CampaignDetailId and cdu.UserRole = 4
) AS tl
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información completa de cada lote de preparación (campaña) en la estación de mezclas farmacéuticas, integrando el detalle del lote con su configuración de cámara limpia, línea de producción, tipo de dosis unitaria y la cantidad total de unidades solicitadas. Además, identifica y expone los cuatro roles de usuario asignados al lote: el químico farmacéutico de calidad (QF Calidad, rol 1), el químico farmacéutico de producción (QF Producción, rol 2), el auxiliar de mezclas (MS Aux, rol 3) y el técnico de laboratorio o líder (TL, rol 4), mostrando para cada uno su identificación, código de usuario y nombre completo. Incluye indicadores clave de estado: si el lote ya tiene una línea de liberación registrada (HasReleaseLine) y si todos los productos del lote han finalizado su proceso de empaque (HasProductEnd). Sirve para consultas operativas y de trazabilidad del ciclo de vida de las campañas de preparación de medicamentos en la unidad de dosis.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignDetailUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignDetailUser';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, por cada detalle de campaña de la estación de mezclas, su configuración, línea de producción, cantidad solicitada total, los usuarios asignados según rol (QF Calidad, QF Producción, Auxiliar, Team Leader) y banderas de liberación de línea y de finalización de producción.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'CampaignDetail debe estar vinculado a Campaign, CMConfiguration, UnitDoseType y ProductionLine (INNER JOINs).; Debe existir al menos una fila en RequestMixingStationDetail con CampaignDetailId no nulo para que el detalle aparezca en la vista.; Los usuarios asignados deben existir tanto en Security.User como en Security.Person para resolver código y nombre completo.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cantidad total de la campaña (Quantity) se calcula sumando RequestMixingStationDetail.Quantity agrupado por CampaignDetailId, ignorando filas con CampaignDetailId NULL.; Una campaña sólo aparece en la vista si tiene al menos un RequestMixingStationDetail asociado (JOIN obligatorio con CampaignRequest).; Los roles de usuario en la campaña están codificados: 1=QF Calidad, 2=QF Producción, 3=Auxiliar MS, 4=Team Leader.; HasProductEnd se considera verdadero únicamente cuando la cantidad solicitada equivale exactamente a la suma de liberada + rechazada + cancelada.; Cada rol expuesto asume un único usuario por CampaignDetail (OUTER APPLY sin agregación); si hubiera más de uno, se multiplicarían filas.; Sólo se muestran usuarios de campaña que existan también en Security.User y Security.Person (INNER JOIN en el CTE).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de preparación farmacéutica; Estación de mezclas (Mixing Station); Línea de producción; Dosis unitaria; Liberación de línea; Roles de campaña: QF Calidad, QF Producción, Auxiliar, Team Leader; Estado de preparación (solicitada/liberada/rechazada/cancelada)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewCampaignDetailUser: Devuelve una fila por CampaignDetail con datos agregados de cantidad solicitada, usuarios pivoteados por UserRole (1..4) y banderas HasReleaseLine y HasProductEnd.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EXISTS en ViewSumQuantityByPreparationStatus con Id = cd.Id y RequestedQuantity = ReleasedQuantity + RejectedQuantity + CancelledQuantity → HasProductEnd = 1 (campaña con producción finalizada) else HasProductEnd = 0; si rl.Id IS NULL en LEFT JOIN con MixingStation.ReleaseLine por CampaignDetailId → HasReleaseLine = 0 else HasReleaseLine = 1; si campaignUser.UserRole = 1 → Usuario expuesto como QF de Calidad (QFQuality*); si campaignUser.UserRole = 2 → Usuario expuesto como QF de Producción (QFProduction*); si campaignUser.UserRole = 3 → Usuario expuesto como Auxiliar de Mixing Station (MSAux*); si campaignUser.UserRole = 4 → Usuario expuesto como Team Leader (TL*)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetailUsers; Security.User; Security.Person; MixingStation.RequestMixingStationDetail; MixingStation.CampaignDetail; MixingStation.Campaign; MixingStation.CMConfiguration; MixingStation.UnitDoseType; MixingStation.ProductionLine; MixingStation.ReleaseLine; MixingStation.ViewSumQuantityByPreparationStatus', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailUser';
GO
