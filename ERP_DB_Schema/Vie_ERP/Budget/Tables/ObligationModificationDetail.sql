CREATE TABLE [Budget].[ObligationModificationDetail] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ObligationModificationId] INT          NOT NULL,
    [ObligationDetailId]       INT          NOT NULL,
    [ExpiredDate]              DATETIME     NOT NULL,
    [Nature]                   TINYINT      NOT NULL,
    [Value]                    NUMERIC (18) NOT NULL,
    [IsLogBase]                BIT          NOT NULL,
    CONSTRAINT [PK_ObligationModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ObligationModificationDetail_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id]),
    CONSTRAINT [FK_ObligationModificationDetail_ObligationModification] FOREIGN KEY ([ObligationModificationId]) REFERENCES [Budget].[ObligationModification] ([Id])
);


GO
ALTER TABLE [Budget].[ObligationModificationDetail] NOCHECK CONSTRAINT [FK_ObligationModificationDetail_ObligationDetail];


GO
ALTER TABLE [Budget].[ObligationModificationDetail] NOCHECK CONSTRAINT [FK_ObligationModificationDetail_ObligationModification];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el registro es base de la obligacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'IsLogBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza (NINGUNA = 0,DEBITO = 1,CREDITO = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimieno', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id detalle de la oblicacion asociada', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cabecera de la modificacion de la obligacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ObligationModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';

