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
ALTER TABLE [Contract].[ProcedureCups] NOCHECK CONSTRAINT [FK_ProcedureCups_ContractDescriptions];


GO
ALTER TABLE [Contract].[ProcedureCups] NOCHECK CONSTRAINT [FK_ProcedureCups_CupsEntity];


GO
ALTER TABLE [Contract].[ProcedureCups] NOCHECK CONSTRAINT [FK_ProcedureCups_CUPSEntityContractDescriptions];


GO
ALTER TABLE [Contract].[ProcedureCups] NOCHECK CONSTRAINT [FK_ProcedureCups_ProcedureTemplate];


GO
CREATE NONCLUSTERED INDEX [IX_Contract_ProcedureCups_ProceduresTemplateId_CupsId]
    ON [Contract].[ProcedureCups]([ProceduresTemplateId] ASC)
    INCLUDE([CupsId]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProcedureCups__CupsId__ProceduresTemplateId]
    ON [Contract].[ProcedureCups]([CupsId] ASC, [ProceduresTemplateId] ASC, [ContractDescriptionId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la descripción que se relaciona con el cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la relación entre el cups y la descripción', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si el servicio está cotizado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Quoted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si el servicio es contratado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Contracted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cups asociado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'CupsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plantilla del procedimiento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'ProceduresTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la union de cups con plantilla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ProcedureCups', @level2type = N'COLUMN', @level2name = N'Id';

