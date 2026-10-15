







CREATE VIEW [Contract].[ViewListDashboardPgp]
AS

select ROW_NUMBER() over(order by gcg.DocumentDate) as Row, 
gcg.CareGroupId, 
gcg.GroupersId, 
g.Code + ' - ' + g.[Description] as GrouperName, 
g.UserNumber, 
g.UserMin, 
g.UserMax, 
g.ProjectCME, 
g.Frequence, 
g.TotalContract, 
g.UserValue,
SUM(gcg.EjectEvent) as EjectEvent, 
SUM(gcg.RealCME) as RealCME, 
SUM(gcg.TotalEject) as TotalEject, 
IIF(g.TotalContract = 0, 0, SUM(gcg.TotalEject) / g.TotalContract * 100) as Variation,
gcg.DocumentDate
from [Contract].GroupersCareGroup gcg
inner join [Contract].Groupers g on g.Id = gcg.GroupersId
group by gcg.CareGroupId, gcg.GroupersId, g.Code, g.[Description], g.UserNumber, g.UserMin, g.UserMax, g.ProjectCME, g.Frequence, 
g.TotalContract, g.UserValue, gcg.DocumentDate