

CREATE view [Common].[ViewHealthProfessional]
as
	SELECT	TRIM(inp.CODPROSAL) as Code,
			TRIM(inp.NOMMEDICO) as FullName,
			TRIM(ipna.DESESPECI) as DescriptionSpeciality,
			cast(IIF(inp.ESTADOMED=1,1,0) AS bit) Status,
			p.IdentificationNumber,
			p.IdentificationTypeId,
			adt.NOMBRE IdentificationTypeName,
			th.Id as ThirdPartyId,
			ipna.CODESPECI as CodeProfessionalSpecialty
	FROM INPROFSAL inp with(nolock)
	JOIN INESPECIA ipna with(nolock) on inp.CODESPEC1 = ipna.CODESPECI
	JOIN common.ThirdParty th with(nolock) on th.Nit = inp.CODIGONIT
	JOIN Common.Person p with(nolock) on p.Id=th.PersonId
	JOIN ADTIPOIDENTIFICA adt with(nolock) on p.IdentificationTypeId = adt.ID
	LEFT JOIN common.HealthProfessional hp with(nolock) on inp.CODIGONIT = hp.IdentificationNumber
	WHERE (hp.Id is null or hp.ExternalProfessional = 0)

	union all

	select	hp.IdentificationNumber as Code,
			concat(hp.FirstName,' ', hp.FirstLastName) FullName,
			ipna.DESESPECI as DescriptionSpeciality,
			hp.Status,
			hp.IdentificationNumber,
			hp.IdentificationTypeId,
			adt.NOMBRE IdentificationTypeName,
			th.Id as ThirdPartyId,
			hp.ProfessionalSpecialty  as CodeProfessionalSpecialty
	from common.HealthProfessional hp with(nolock)
	join common.Person p with(nolock) on p.IdentificationNumber = hp.IdentificationNumber and p.IdentificationTypeId = hp.IdentificationTypeId
	join common.ThirdParty th with(nolock) on th.PersonId = p.Id
	JOIN INESPECIA ipna with(nolock) on ipna.CODESPECI = hp.ProfessionalSpecialty
	join ADTIPOIDENTIFICA adt with(nolock) on hp.IdentificationTypeId = adt.ID
	where hp.ExternalProfessional = 1