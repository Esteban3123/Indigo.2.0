CREATE TABLE [Contract].[GroupersCups] (
    [Id]                              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GrouperId]                       INT NOT NULL,
    [CUPSEntityId]                    INT NOT NULL,
    [CUPSEntityContractDescriptionId] INT NULL,
    [ContractDescriptionId]           INT NULL,
    CONSTRAINT [PK_GroupersCups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersCups_ContractDescriptions] FOREIGN KEY ([ContractDescriptionId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_GroupersCups_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_GroupersCups_CUPSEntityContractDescriptions] FOREIGN KEY ([CUPSEntityContractDescriptionId]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_GroupersCups_Groupers] FOREIGN KEY ([GrouperId]) REFERENCES [Contract].[Groupers] ([Id])
);


GO
ALTER TABLE [Contract].[GroupersCups] NOCHECK CONSTRAINT [FK_GroupersCups_CUPSEntity];




GO



GO
ALTER TABLE [Contract].[GroupersCups] NOCHECK CONSTRAINT [FK_GroupersCups_CUPSEntity];


GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_GroupersCups]
    ON [Contract].[GroupersCups]([GrouperId] ASC, [CUPSEntityId] ASC, [CUPSEntityContractDescriptionId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la descripción contractual relacionada con el CUPS; referencia a ContractDescriptions que define términos, valores y condiciones del servicio o procedimiento en el contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripción que se relaciona con el cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la asociación entre la entidad CUPS y su descripción contractual específica; vínculo que relaciona el código CUPS con los detalles y condiciones del contrato aplicables.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la relación entre el cups y la descripción', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del CUPS (Código Único de Procedimientos en Salud); referencia a la entidad que contiene el código de procedimiento, servicio o producto de salud según nomenclatura oficial.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del agrupador (Grouper); referencia al sistema de clasificación de procedimientos o servicios que agrupa CUPS según criterios de facturación, codificación o gestión contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'GrouperId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Agrupador', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'GrouperId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'GrouperId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria única de la tabla GroupersCups; identificador secuencial que vincula un agrupador con un CUPS y sus descripciones contractuales en el contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave principal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona grupos tarifarios o agrupadores de contratos con los servicios y procedimientos CUPS pactados, permitiendo asociar cada código CUPS a un agrupador y a las descripciones o ítems del contrato correspondiente.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCups';
