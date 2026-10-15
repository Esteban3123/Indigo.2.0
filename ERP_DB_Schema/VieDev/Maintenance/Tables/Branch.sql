CREATE TABLE [Maintenance].[Branch] (
    [Id]        TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]      VARCHAR (20) NOT NULL,
    [IdCity]    INT          NOT NULL,
    [Name]      VARCHAR (50) NOT NULL,
    [IdBranch]  TINYINT      NULL,
    [State]     BIT          NOT NULL,
    [TimeStamp] ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Branch__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Branch_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id])
);


GO
ALTER TABLE [Maintenance].[Branch] NOCHECK CONSTRAINT [FK_Branch_City];


GO
CREATE NONCLUSTERED INDEX [IX_Branch__State]
    ON [Maintenance].[Branch]([State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'columna para controlar la concurrencia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Branch', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la sede', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Branch', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la sede relacionada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Branch', @level2type = N'COLUMN', @level2name = N'IdBranch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de  la sede', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Branch', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la ciudad relacionada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Branch', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la sede', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Branch', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla de Sedes', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Branch', @level2type = N'COLUMN', @level2name = N'Id';

