CREATE TABLE [Common].[MilitaryCard] (
    [Id]    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]  VARCHAR (3)  NOT NULL,
    [Name]  VARCHAR (50) NOT NULL,
    [State] BIT          NOT NULL,
    CONSTRAINT [PK_MilitaryCard] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1 Activo, 0 Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la libreta militar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la libreta militar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la libreta militar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Id';

