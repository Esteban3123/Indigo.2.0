CREATE TABLE [Budget].[SuspensionCancellationDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SuspensionCancellationId] INT             NOT NULL,
    [SuspensionDetailId]       INT             NOT NULL,
    [Value]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_SuspensionAnnularDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SuspensionAnnularDetail_SuspensionAnnular] FOREIGN KEY ([SuspensionCancellationId]) REFERENCES [Budget].[SuspensionCancellation] ([Id]),
    CONSTRAINT [FK_SuspensionAnnularDetail_SuspensionDetail] FOREIGN KEY ([SuspensionDetailId]) REFERENCES [Budget].[SuspensionDetail] ([Id])
);


GO
ALTER TABLE [Budget].[SuspensionCancellationDetail] NOCHECK CONSTRAINT [FK_SuspensionAnnularDetail_SuspensionAnnular];


GO
ALTER TABLE [Budget].[SuspensionCancellationDetail] NOCHECK CONSTRAINT [FK_SuspensionAnnularDetail_SuspensionDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del levantamiento de la suspencion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la suspension', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'SuspensionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de el levantamiento de la suspension', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'SuspensionCancellationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'Id';

