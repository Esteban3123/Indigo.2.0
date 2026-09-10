CREATE TABLE [Contract].[ProcedureCups] (
    [Id]                              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProceduresTemplateId]            INT NOT NULL,
    [CupsId]                          INT NOT NULL,
    [Contracted]                      BIT CONSTRAINT [DF_ProcedureCups_Contracted] DEFAULT ((1)) NOT NULL,
    [Quoted]                          BIT CONSTRAINT [DF_ProcedureCups_Quoted] DEFAULT ((0)) NOT NULL,
    [CUPSEntityContractDescriptionId] INT NULL,
    [ContractDescriptionId]           INT NULL,
    CONSTRAINT [PK_ProcedureCups__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProcedureCups_ContractDescriptions] FOREIGN KEY ([ContractDescriptionId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_ProcedureCups_CupsEntity] FOREIGN KEY ([CupsId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_ProcedureCups_CUPSEntityContractDescriptions] FOREIGN KEY ([CUPSEntityContractDescriptionId]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_ProcedureCups_ProcedureTemplate] FOREIGN KEY ([ProceduresTemplateId]) REFERENCES [Contract].[ProcedureTemplate] ([Id])
);


GO
ALTER TABLE [Contract].[ProcedureCups] NOCHECK CONSTRAINT [FK_ProcedureCups_CupsEntity];




GO



GO
ALTER TABLE [Contract].[ProcedureCups] NOCHECK CONSTRAINT [FK_ProcedureCups_CupsEntity];


GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_Contract_ProcedureCups_ProceduresTemplateId_CupsId]
    ON [Contract].[ProcedureCups]([ProceduresTemplateId] ASC)
    INCLUDE([CupsId]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProcedureCups__CupsId__ProceduresTemplateId]
    ON [Contract].[ProcedureCups]([CupsId] ASC, [ProceduresTemplateId] ASC, [ContractDescriptionId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la descripción contractual asociada al CUPS; referencia a ContractDescriptions que detalla condiciones, coberturas y términos específicos del procedimiento en el contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripción que se relaciona con el cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la relación específica entre el código CUPS y su descripción contractual en CUPSEntityContractDescriptions; vincula el procedimiento a términos particulares negociados.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la relación entre el cups y la descripción', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que indica si el servicio/procedimiento ha sido cotizado o presupuestado en el contrato; 1=cotizado, 0=no cotizado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Quoted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si el servicio está cotizado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Quoted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Quoted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que indica si el servicio/procedimiento está formalmente contratado con la entidad; 1=contratado (activo), 0=no contratado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Contracted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si el servicio es contratado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Contracted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Contracted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código CUPS (Clasificación Única de Procedimientos en Salud) asociado; referencia a CUPSEntity que identifica el procedimiento, examen, servicio o intervención sanitaria.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'CupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cups asociado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'CupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'CupsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la plantilla de procedimiento que define la estructura, estándares y configuración base del procedimiento; referencia a ProcedureTemplate.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'ProceduresTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la plantilla del procedimiento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'ProceduresTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'ProceduresTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) de la asociación entre plantilla de procedimiento (ProceduresTemplateId) y código CUPS (CupsId) dentro del contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la union de cups con plantilla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Códigos CUPS (procedimientos y servicios de salud) asociados a una plantilla de contrato. Indica qué procedimientos están contratados y/o cotizados dentro de un contrato, vinculando cada CUPS a su descripción tarifaria o cláusula contractual correspondiente.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups';
