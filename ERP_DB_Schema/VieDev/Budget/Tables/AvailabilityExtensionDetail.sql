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

