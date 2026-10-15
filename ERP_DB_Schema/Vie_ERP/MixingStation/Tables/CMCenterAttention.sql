CREATE TABLE [MixingStation].[CMCenterAttention] (
    [Id]                  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdMixingStation]     INT       NOT NULL,
    [IdCenterAttention]   INT       NULL,
    [IdProductionLine]    INT       NOT NULL,
    [CodeCenterAttention] CHAR (10) CONSTRAINT [DF_CMCenterAttention_CodeCenterAttention] DEFAULT ('          ') NOT NULL,
    [StateCA]             BIT       NULL,
    CONSTRAINT [PK_CMCenterAttention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMCenterAttention_CMConfiguration] FOREIGN KEY ([IdMixingStation]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_CMCenterAttention_ProductionLine] FOREIGN KEY ([IdProductionLine]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);


GO
ALTER TABLE [MixingStation].[CMCenterAttention] NOCHECK CONSTRAINT [FK_CMCenterAttention_CMConfiguration];


GO
ALTER TABLE [MixingStation].[CMCenterAttention] NOCHECK CONSTRAINT [FK_CMCenterAttention_ProductionLine];


GO
CREATE NONCLUSTERED INDEX [IX_CMCenterAttention_IdProductionLine]
    ON [MixingStation].[CMCenterAttention]([IdProductionLine] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: 1:Activo, 0:Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'StateCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo centro de atención', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdProductionLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id centro de atención', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdCenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de Estación de mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdMixingStation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'Id';

