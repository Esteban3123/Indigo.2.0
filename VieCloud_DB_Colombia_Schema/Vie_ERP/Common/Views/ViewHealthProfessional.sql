

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista unificada de todos los profesionales de la salud disponibles en el sistema, integrando tanto los registros del maestro legado (INPROFSAL) como los profesionales externos registrados en la tabla moderna (HealthProfessional). Combina datos de identificación personal (número de documento, tipo de documento), nombre completo, especialidad médica y estado activo/inactivo de cada profesional, enlazando las entidades de persona, tercero y tipo de identificación para ofrecer una visión consolidada. Sirve como fuente única para búsquedas, asignación y validación de médicos, enfermeros y especialistas en procesos de agendamiento, órdenes médicas, atención clínica y facturación. Distingue entre profesionales internos (gestionados en el sistema legado) y externos (registrados directamente como HealthProfessional externo), evitando duplicados mediante la condición de exclusión por identificación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewHealthProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewHealthProfessional';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica el catálogo de profesionales de la salud combinando los registrados en el ERP legado (INPROFSAL) con los profesionales externos gestionados en la tabla propia, exponiendo identificación, especialidad y tercero asociado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewHealthProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada profesional del ERP (INPROFSAL) debe tener un NIT que exista como tercero en common.ThirdParty y la persona asociada debe existir en common.Person con su tipo de identificación en ADTIPOIDENTIFICA.; La especialidad referenciada (CODESPEC1 / ProfessionalSpecialty) debe existir en INESPECIA.; Para profesionales externos, debe existir coincidencia por IdentificationNumber + IdentificationTypeId entre common.HealthProfessional y common.Person.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewHealthProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un profesional externo (ExternalProfessional=1) siempre se toma de common.HealthProfessional y nunca del ERP legado, evitando duplicados.; El estado del profesional del ERP es binario: solo ESTADOMED=1 se considera activo.; Todo profesional retornado tiene un ThirdParty asociado y un tipo de identificación válido en ADTIPOIDENTIFICA.; Se utiliza únicamente la primera especialidad (CODESPEC1) del profesional del ERP.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewHealthProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Profesional de la salud; Especialidad médica; Tercero; Tipo de identificación; Profesional externo; NIT', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewHealthProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve el conjunto unificado de profesionales: del ERP cuando no existen en HealthProfessional o existen pero ExternalProfessional=0, y de HealthProfessional cuando ExternalProfessional=1.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewHealthProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Profesional proviene de INPROFSAL y (no existe en common.HealthProfessional) o (existe con ExternalProfessional = 0) → Se incluye usando los datos del ERP legado (CODPROSAL, NOMMEDICO, CODESPEC1, ESTADOMED, NIT).; si Profesional existe en common.HealthProfessional con ExternalProfessional = 1 → Se incluye usando los datos propios (IdentificationNumber, FirstName + FirstLastName, ProfessionalSpecialty, Status).; si ESTADOMED = 1 en INPROFSAL → Status se expone como 1 (activo); en cualquier otro caso se expone como 0.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewHealthProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'INPROFSAL; INESPECIA; common.ThirdParty; common.Person; ADTIPOIDENTIFICA; common.HealthProfessional', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewHealthProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewHealthProfessional';
GO
