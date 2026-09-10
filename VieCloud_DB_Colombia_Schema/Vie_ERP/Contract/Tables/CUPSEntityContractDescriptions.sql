CREATE TABLE [Contract].[CUPSEntityContractDescriptions] (
    [Id]                    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CUPSEntityId]          INT NOT NULL,
    [ContractDescriptionId] INT NOT NULL,
    [CupsSubgroupId]        INT NOT NULL,
    [BillingGroupId]        INT NOT NULL,
    [BillingConceptId]      INT NOT NULL,
    [IsDelete]              BIT CONSTRAINT [DF_CUPSEntityContractDescriptions_IsDelete] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CUPSEntityContractDescriptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CUPSEntityContractDescriptions_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_CUPSEntityContractDescriptions_BillingGroup] FOREIGN KEY ([BillingGroupId]) REFERENCES [Billing].[BillingGroup] ([Id]),
    CONSTRAINT [FK_CUPSEntityContractDescriptions_ContractDescriptions] FOREIGN KEY ([ContractDescriptionId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_CUPSEntityContractDescriptions_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_CUPSEntityContractDescriptions_CupsSubgroup] FOREIGN KEY ([CupsSubgroupId]) REFERENCES [Contract].[CupsSubgroup] ([Id])
);




GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CUPSEntityContractDescriptions]
    ON [Contract].[CUPSEntityContractDescriptions]([CUPSEntityId] ASC, [ContractDescriptionId] ASC, [CupsSubgroupId] ASC, [BillingGroupId] ASC, [BillingConceptId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de eliminación lógica (BIT). Indica si el registro está marcado como eliminado: true si fue eliminado en proceso, false si está activo. La eliminación física se restringe si el registro está en tablas de configuración Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'IsDelete';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si el registro está eliminado lógicamente:    -Se verifica que no este en tablas de configuración de Crystal, si esta en esas tablas no se permite eliminar el registro    -Si no esta en tablas de configuración pero si en tablas de proceso entonces se asigna este campo en true    -Si no esta en ninguna de las dos tablas entonces si se puede eliminar el registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'IsDelete';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'IsDelete';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Concepto de Facturación (FK → Billing.BillingConcept). Vincula el concepto tarifario o rubro de facturación usado en la facturación de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de facturación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Grupo de Facturación (FK → Billing.BillingGroup). Agrupa conceptos de facturación para procesos de cobro, factura o RIPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de facturación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Subgrupo CUPS (FK → Contract.CupsSubgroup). Clasificación secundaria dentro del catálogo nacional CUPS de procedimientos, diagnósticos o servicios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'CupsSubgroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del subgrupo cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'CupsSubgroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'CupsSubgroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Descripción del Contrato (FK → Contract.ContractDescriptions). Referencia la descripción detallada del servicio, procedimiento o producto contratado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripción', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Entidad CUPS (FK → Contract.CUPSEntity). Código del servicio, procedimiento o prestación según catálogo CUPS nacional.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (INT IDENTITY). Clave primaria que identifica unívocamente cada asociación entre CUPS, contrato, grupo y concepto de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los códigos CUPS (servicios/procedimientos) de una entidad con las descripciones de contrato, grupos de facturación y conceptos de cobro aplicables. Permite definir cómo se factura cada procedimiento o servicio dentro de un contrato específico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions';
