CREATE TABLE [Budget].[CommitmentModificationDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CommitmentModificationId] INT             NOT NULL,
    [CommitmentDetailId]       INT             NOT NULL,
    [Nature]                   TINYINT         NOT NULL,
    [Value]                    NUMERIC (18, 2) NOT NULL,
    [IsLogBase]                BIT             NOT NULL,
    CONSTRAINT [PK_CommitmentModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CommitmentModificationDetail_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_CommitmentModificationDetail_CommitmentModification] FOREIGN KEY ([CommitmentModificationId]) REFERENCES [Budget].[CommitmentModification] ([Id])
);


GO
ALTER TABLE [Budget].[CommitmentModificationDetail] NOCHECK CONSTRAINT [FK_CommitmentModificationDetail_CommitmentDetail];


GO
ALTER TABLE [Budget].[CommitmentModificationDetail] NOCHECK CONSTRAINT [FK_CommitmentModificationDetail_CommitmentModification];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'establece si el Registro es Base del compromiso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentModificationDetail', @level2type = N'COLUMN', @level2name = N'IsLogBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la modificacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle del compromiso.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentModificationDetail', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cabecera de la modificacion de compromisos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentModificationDetail', @level2type = N'COLUMN', @level2name = N'CommitmentModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la modificacion del compromiso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';

