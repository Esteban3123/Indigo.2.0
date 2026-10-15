



CREATE VIEW [Contract].[VProcedureMarketingUnitCUPS]
as
(
select  ROW_NUMBER() OVER(ORDER BY pc.ProceduresTemplateId ASC) as Row,
pc.ProceduresTemplateId
,cups.Id as CUPSId
,cups.Code as CUPSCode
,cups.Description as CUPSDescription
,csub.Id as SubGroupId
,csub.Code as SubGroupCode
,csub.Name as SubGroupName
,cgro.Id as GroupId
,cgro.Code as GroupCode
,cgro.Name as GroupName
from [Contract].ProcedureCups pc with (nolock) 
inner join [Contract].CUPSEntity cups  with (nolock)  on pc.CupsId = cups.Id
inner join [Contract].CupsSubgroup csub  with (nolock) on cups.CUPSSubGroupId = csub.Id
inner join [Contract].CupsGroup cgro  with (nolock) on csub.CupsGroupId = cgro.Id
)