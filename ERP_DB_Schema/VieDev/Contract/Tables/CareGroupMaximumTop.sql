CREATE TABLE [Contract].[CareGroupMaximumTop] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Year]        INT             NULL,
    [CareGroupId] INT             NULL,
    [Value]       NUMERIC (18, 2) NULL,
    [Status]      BIT             NULL,
    CONSTRAINT [PK_CareGroupMaximumTop] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareGroupMaximumTop_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id])
);


GO
ALTER TABLE [Contract].[CareGroupMaximumTop] NOCHECK CONSTRAINT [FK_CareGroupMaximumTop_CareGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del grupo de atención máximo  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMaximumTop', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el valor establecido', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMaximumTop', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Grupo de Atención', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMaximumTop', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el año elegido', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMaximumTop', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupMaximumTop', @level2type = N'COLUMN', @level2name = N'Id';

