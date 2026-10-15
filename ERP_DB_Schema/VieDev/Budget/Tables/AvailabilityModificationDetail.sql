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

