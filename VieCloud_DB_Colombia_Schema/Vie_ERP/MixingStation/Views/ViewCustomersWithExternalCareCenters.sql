
CREATE VIEW [MixingStation].[ViewCustomersWithExternalCareCenters]
AS
SELECT	DISTINCT 
		c.Id ViewKey,
		c.Id IdCustomer,
		c.Nit NitCustomer,
		c.Name NameCustomer
FROM Common.Customer c
JOIN MixingStation.ExternalCareCenter ecc ON c.Id = ecc.CustomerId
WHERE ecc.Status = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clientes o entidades pagadoras (EPS, aseguradoras, empresas) que tienen al menos un centro de atención externo activo registrado en el módulo de Estación de Mezclas. Combina la información de clientes del sistema con los centros de atención externos vinculados a cada cliente, filtrando únicamente los que están activos (estado = 1). Permite identificar rápidamente qué pagadores o contratantes cuentan con centros externos habilitados para operar, útil para procesos de facturación, autorización y trazabilidad de servicios prestados fuera de la institución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCustomersWithExternalCareCenters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCustomersWithExternalCareCenters';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista de clientes que tienen al menos un centro de atención externo activo asociado en la estación de mezclas, exponiendo su identificación, NIT y nombre.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCustomersWithExternalCareCenters';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen clientes que tienen al menos un centro de atención externo activo (ExternalCareCenter.Status = 1).; El resultado es DISTINCT: cada cliente aparece una sola vez aunque tenga múltiples centros externos activos asociados.; El vínculo cliente–centro externo se establece por ExternalCareCenter.CustomerId = Customer.Id.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCustomersWithExternalCareCenters';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cliente; Centro de atención externo; Estación de mezclas', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCustomersWithExternalCareCenters';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.Customer: Devuelve clientes (Id, Nit, Name) únicamente cuando existe un ExternalCareCenter vinculado con Status = 1 (activo).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCustomersWithExternalCareCenters';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Customer; MixingStation.ExternalCareCenter', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCustomersWithExternalCareCenters';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCustomersWithExternalCareCenters';
GO
