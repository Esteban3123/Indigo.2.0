CREATE TABLE [Budget].[CollectionModificationDetail] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CollectionModificationId] INT          NOT NULL,
    [CollectionDetailId]       INT          NOT NULL,
    [Nature]                   TINYINT      NOT NULL,
    [Value]                    NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CollectionModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CollectionModificationDetail_CollectionDetail] FOREIGN KEY ([CollectionDetailId]) REFERENCES [Budget].[CollectionDetail] ([Id]),
    CONSTRAINT [FK_CollectionModificationDetail_CollectionModification] FOREIGN KEY ([CollectionModificationId]) REFERENCES [Budget].[CollectionModification] ([Id])
);


GO
ALTER TABLE [Budget].[CollectionModificationDetail] NOCHECK CONSTRAINT [FK_CollectionModificationDetail_CollectionDetail];


GO
ALTER TABLE [Budget].[CollectionModificationDetail] NOCHECK CONSTRAINT [FK_CollectionModificationDetail_CollectionModification];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la modificacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obtiene o establece la naturaleza de la modificacion ( Debito = 1 Credito = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del recaudo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'CollectionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la modificacion al Recaudo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'CollectionModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';

