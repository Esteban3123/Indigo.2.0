CREATE VIEW [Common].[ViewLatestAddressByPerson]
AS
	SELECT a.IdPerson PersonId, MAX(a.Id) AddressId
	FROM Common.Address a WITH (NOLOCK)
	GROUP BY a.IdPerson
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección más reciente registrada por persona en el sistema. Para cada persona (paciente, profesional u otro actor), retorna el identificador de la última dirección cargada, basándose en el mayor ID registrado en la tabla de direcciones. Se utiliza para obtener de forma rápida la dirección vigente o más actualizada de una persona sin procesar el historial completo de domicilios.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewLatestAddressByPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewLatestAddressByPerson';
GO
