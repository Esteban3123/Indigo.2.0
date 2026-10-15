CREATE TABLE [Budget].[AvailabilityModificationDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AvailabilityModificationId] INT             NOT NULL,
    [AvailabilityDetailId]       INT             NOT NULL,
    [Nature]                     TINYINT         NOT NULL,
    [Value]                      NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_AvailabilityModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AvailabilityModificationDetail_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_AvailabilityModificationDetail_AvailabilityModification] FOREIGN KEY ([AvailabilityModificationId]) REFERENCES [Budget].[AvailabilityModification] ([Id])
);


GO
ALTER TABLE [Budget].[AvailabilityModificationDetail] NOCHECK CONSTRAINT [FK_AvailabilityModificationDetail_AvailabilityDetail];


GO
ALTER TABLE [Budget].[AvailabilityModificationDetail] NOCHECK CONSTRAINT [FK_AvailabilityModificationDetail_AvailabilityModification];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza (Debito = 1, Credito = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del detalle de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cabecera de la modificacion de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la modificacion de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';

