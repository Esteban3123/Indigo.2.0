

CREATE view [Common].[ViewThirdPartyCustomer]
as
	select concat(tp.Id, '-', isnull(c.Id, 0)) as Id
		, isnull(c.Nit, tp.Nit) as Nit
		, isnull(c.[Name], tp.[Name]) as [Name]
		, tp.Id as ThirdPartyId
		,tp.ContributionType
		, c.Id as CustomerId
		, c.MainAccountReceivableId
		, tp.PersonId
	from Common.ThirdParty tp
	left join Common.Customer c on c.ThirdPartyId = tp.Id
	where tp.State = 1 and (c.State is null or c.State = 1)