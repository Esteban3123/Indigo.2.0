CREATE TABLE [MixingStation].[RequestPackageDetailStatusDefectClassificationDetail] (
    [Id]                                               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestPackageDetailStatusDefectClassificationId] INT NOT NULL,
    [DefectClassificationItemId]                       INT NOT NULL,
    [Production]                                       BIT NULL,
    [Quality]                                          BIT NULL,
    CONSTRAINT [PK_RequestPackageDetailStatusDefectClassificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestPackageDetailStatusDefectClassificationDetail_DefectClassificationItem] FOREIGN KEY ([DefectClassificationItemId]) REFERENCES [MixingStation].[DefectClassificationItem] ([Id]),
    CONSTRAINT [FK_RequestPackageDetailStatusDefectClassificationDetail_RequestPackageDetailStatusDefectClassification] FOREIGN KEY ([RequestPackageDetailStatusDefectClassificationId]) REFERENCES [MixingStation].[RequestPackageDetailStatusDefectClassification] ([Id])
);


GO
ALTER TABLE [MixingStation].[RequestPackageDetailStatusDefectClassificationDetail] NOCHECK CONSTRAINT [FK_RequestPackageDetailStatusDefectClassificationDetail_DefectClassificationItem];


GO
ALTER TABLE [MixingStation].[RequestPackageDetailStatusDefectClassificationDetail] NOCHECK CONSTRAINT [FK_RequestPackageDetailStatusDefectClassificationDetail_RequestPackageDetailStatusDefectClassification];

