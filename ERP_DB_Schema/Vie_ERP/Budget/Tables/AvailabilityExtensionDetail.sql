CREATE TABLE [Budget].[AvailabilityExtensionDetail] (
    [Id]                      INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AvailabilityExtensionId] INT      NOT NULL,
    [AvailabilityId]          INT      NOT NULL,
    [ExtensionDay]            INT      NOT NULL,
    [AvailabilityDate]        DATETIME NOT NULL,
    [ExpirationDate]          DATETIME NOT NULL,
    CONSTRAINT [PK_AvailabilityExtensionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AvailabilityExtensionDetail_Availability] FOREIGN KEY ([AvailabilityId]) REFERENCES [Budget].[Availability] ([Id]),
    CONSTRAINT [FK_AvailabilityExtensionDetail_AvailabilityExtension] FOREIGN KEY ([AvailabilityExtensionId]) REFERENCES [Budget].[AvailabilityExtension] ([Id])
);


GO
ALTER TABLE [Budget].[AvailabilityExtensionDetail] NOCHECK CONSTRAINT [FK_AvailabilityExtensionDetail_Availability];


GO
ALTER TABLE [Budget].[AvailabilityExtensionDetail] NOCHECK CONSTRAINT [FK_AvailabilityExtensionDetail_AvailabilityExtension];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de extensión', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Día de extensión', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'ExtensionDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de disponibilidad de extensión', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityExtensionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'Id';

