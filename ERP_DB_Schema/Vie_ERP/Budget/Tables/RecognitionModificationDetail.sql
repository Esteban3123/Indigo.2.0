CREATE TABLE [Budget].[RecognitionModificationDetail] (
    [Id]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RecognitionModificationId] INT          NOT NULL,
    [RecognitionDetailId]       INT          NOT NULL,
    [Nature]                    TINYINT      NOT NULL,
    [Value]                     NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_RecognitionModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RecognitionModificationDetail_RecognitionDetail] FOREIGN KEY ([RecognitionDetailId]) REFERENCES [Budget].[RecognitionDetail] ([Id]),
    CONSTRAINT [FK_RecognitionModificationDetail_RecognitionModification] FOREIGN KEY ([RecognitionModificationId]) REFERENCES [Budget].[RecognitionModification] ([Id])
);


GO
ALTER TABLE [Budget].[RecognitionModificationDetail] NOCHECK CONSTRAINT [FK_RecognitionModificationDetail_RecognitionDetail];


GO
ALTER TABLE [Budget].[RecognitionModificationDetail] NOCHECK CONSTRAINT [FK_RecognitionModificationDetail_RecognitionModification];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza (DEBITO = 1,CREDITO = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del reconocimiento que se va a modificar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'RecognitionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cabecera de la modificacion del reconocimiento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'RecognitionModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';

