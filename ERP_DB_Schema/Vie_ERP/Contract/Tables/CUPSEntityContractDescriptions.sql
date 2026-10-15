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
ALTER TABLE [Contract].[CUPSEntityContractDescriptions] NOCHECK CONSTRAINT [FK_CUPSEntityContractDescriptions_BillingConcept];


GO
ALTER TABLE [Contract].[CUPSEntityContractDescriptions] NOCHECK CONSTRAINT [FK_CUPSEntityContractDescriptions_BillingGroup];


GO
ALTER TABLE [Contract].[CUPSEntityContractDescriptions] NOCHECK CONSTRAINT [FK_CUPSEntityContractDescriptions_ContractDescriptions];


GO
ALTER TABLE [Contract].[CUPSEntityContractDescriptions] NOCHECK CONSTRAINT [FK_CUPSEntityContractDescriptions_CUPSEntity];


GO
ALTER TABLE [Contract].[CUPSEntityContractDescriptions] NOCHECK CONSTRAINT [FK_CUPSEntityContractDescriptions_CupsSubgroup];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CUPSEntityContractDescriptions]
    ON [Contract].[CUPSEntityContractDescriptions]([CUPSEntityId] ASC, [ContractDescriptionId] ASC, [CupsSubgroupId] ASC, [BillingGroupId] ASC, [BillingConceptId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si el registro está eliminado lógicamente:    -Se verifica que no este en tablas de configuración de Crystal, si esta en esas tablas no se permite eliminar el registro    -Si no esta en tablas de configuración pero si en tablas de proceso entonces se asigna este campo en true    -Si no esta en ninguna de las dos tablas entonces si se puede eliminar el registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'IsDelete';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de facturación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de facturación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del subgrupo cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'CupsSubgroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la descripción', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityContractDescriptions', @level2type = N'COLUMN', @level2name = N'Id';

