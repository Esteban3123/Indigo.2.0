CREATE TABLE [FixedAsset].[InputRemissionEquipmentDetail] (
    [Id]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdInputRemissionEquipment] INT          NOT NULL,
    [IdEquipment]               INT          NOT NULL,
    [LicensePlate]              VARCHAR (50) NOT NULL,
    [Serie]                     VARCHAR (50) NOT NULL,
    [IdResponsible]             INT          NOT NULL,
    [IdFunctionalUnit]          INT          NOT NULL,
    [IdLocation]                INT          NOT NULL,
    [AdquisitionDate]           DATE         NOT NULL,
    [Depreciate]                BIT          NOT NULL,
    [ComponentDepreciate]       BIT          NULL,
    CONSTRAINT [PK_InputRemissionEquipmentDetail_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InputRemissionEquipmentDetail_FunctionalUnit] FOREIGN KEY ([IdFunctionalUnit]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);


GO
ALTER TABLE [FixedAsset].[InputRemissionEquipmentDetail] NOCHECK CONSTRAINT [FK_InputRemissionEquipmentDetail_FunctionalUnit];

