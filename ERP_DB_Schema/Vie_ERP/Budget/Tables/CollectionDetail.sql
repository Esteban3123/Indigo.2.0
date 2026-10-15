CREATE TABLE [Budget].[CollectionDetail] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CollectionId]            INT          NOT NULL,
    [RecognitionDetailId]     INT          NOT NULL,
    [CollectionType]          TINYINT      NOT NULL,
    [InitialValue]            NUMERIC (18) NOT NULL,
    [DebitModificationValue]  NUMERIC (18) NOT NULL,
    [CreditModificationValue] NUMERIC (18) NOT NULL,
    [Balance]                 NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CollectionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CollectionDetail_Collection] FOREIGN KEY ([CollectionId]) REFERENCES [Budget].[Collection] ([Id]),
    CONSTRAINT [FK_CollectionDetail_RecognitionDetail] FOREIGN KEY ([RecognitionDetailId]) REFERENCES [Budget].[RecognitionDetail] ([Id])
);


GO
ALTER TABLE [Budget].[CollectionDetail] NOCHECK CONSTRAINT [FK_CollectionDetail_Collection];


GO
ALTER TABLE [Budget].[CollectionDetail] NOCHECK CONSTRAINT [FK_CollectionDetail_RecognitionDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el saldo del recaudo el cual se calcula asi (InitialValue - DebitModificationValue + CreditModificationValue)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Modificacion credito al Rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor De modificacion debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de recaudo  1 = Efectivo, 2 =Papeles, 3 = Otras', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CollectionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del reconocimiento que se va a modificar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'RecognitionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cabecera del recaudo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'Id';

