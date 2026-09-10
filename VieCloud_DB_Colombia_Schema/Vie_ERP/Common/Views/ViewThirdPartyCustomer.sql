

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que combina terceros y clientes activos del sistema, unificando en un solo registro la información de identificación (NIT, nombre) y la relación comercial de cada entidad. Para cada tercero activo muestra si también está registrado como cliente facturado (EPS, aseguradora, empresa pagadora), incluyendo su cuenta por cobrar principal y datos de persona. Sirve como fuente consolidada para búsquedas de entidades externas, ya sean proveedores, contratistas o pagadores, facilitando reportería de cartera, facturación y gestión de terceros.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewThirdPartyCustomer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewThirdPartyCustomer';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica terceros y sus clientes asociados activos, priorizando datos de identificación del cliente sobre los del tercero, para consultas combinadas.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyCustomer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen terceros activos (State = 1).; Si existe cliente asociado, debe estar activo (State = 1); de lo contrario se aceptan terceros sin cliente.; Los datos de identificación (NIT y Nombre) priorizan los del cliente; si no existe cliente, se toman los del tercero.; El identificador de la vista combina el Id del tercero con el del cliente (o 0 si no hay cliente), garantizando unicidad por par tercero-cliente.; Un tercero puede aparecer aunque no tenga cliente asociado (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyCustomer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tercero; Cliente; NIT; Tipo de cotizante; Cuenta por cobrar principal; Persona', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyCustomer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente registros donde el tercero está activo (tp.State = 1) y, si tiene cliente vinculado, dicho cliente también está activo (c.State IS NULL OR c.State = 1).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyCustomer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Common.Customer', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyCustomer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyCustomer';
GO
