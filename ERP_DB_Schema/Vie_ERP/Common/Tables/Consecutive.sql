CREATE TABLE [Common].[Consecutive] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              INT           NOT NULL,
    [Description]       VARCHAR (100) NOT NULL,
    [NumberConsecutive] DECIMAL (18)  NOT NULL,
    CONSTRAINT [PK_Consecutive__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Consecutive__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Consecutive', @level2type = N'COLUMN', @level2name = N'NumberConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del consecutivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Consecutive', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del consecutivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Consecutive', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de los consecutivos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Consecutive', @level2type = N'COLUMN', @level2name = N'Id';

