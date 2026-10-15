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
    CAST((CASE WHEN EXISTS (
        SELECT 1 
        FROM MixingStation.RequestPackageDetailStatus ds 
        JOIN MixingStation.RequestMixingStationDetail rmd on ds.RequestMixingStationDetailId = rmd.Id 
        WHERE rmd.CampaignDetailId = cd.Id AND ds.[Status] <> 2
    ) THEN 0 ELSE 1 END) AS BIT) AS HasProductEnd,
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