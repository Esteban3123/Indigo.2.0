CREATE VIEW [GeneralLedger].[ViewEntityNameDescriptions]
AS
	SELECT EntityName, Description
	FROM Common.GetEntityNameDescriptions()
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que expone los nombres y descripciones de las entidades del módulo de Contabilidad General (General Ledger). Consulta la función Common.GetEntityNameDescriptions() para obtener el catálogo de entidades con su nombre técnico y su descripción legible. Sirve como referencia para identificar qué representa cada entidad contable dentro del sistema, útil en reportería, configuración y documentación del libro mayor.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'ViewEntityNameDescriptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'ViewEntityNameDescriptions';
GO
