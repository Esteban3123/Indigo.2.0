CREATE TABLE [Common].[DocumentsTemplate] (
    [Id]       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]     VARCHAR (20)  NOT NULL,
    [Name]     VARCHAR (200) NOT NULL,
    [Template] VARCHAR (MAX) NOT NULL,
    [Status]   BIT           NOT NULL,
    CONSTRAINT [PK_DocumentsTemplate] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DocumentsTemplate', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DocumentsTemplate', @level2type = N'COLUMN', @level2name = N'Template';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DocumentsTemplate', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DocumentsTemplate', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DocumentsTemplate', @level2type = N'COLUMN', @level2name = N'Id';

