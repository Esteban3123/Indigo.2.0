CREATE TABLE [Inventory].[PharmaceuticalDispensingDevolution] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [OperatingUnitId]  INT           NOT NULL,
    [DocumentDate]     DATETIME      NOT NULL,
    [WarehouseId]      INT           NOT NULL,
    [AdmissionNumber]  CHAR (10)     NOT NULL,
    [Observation]      VARCHAR (300) NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [ConfirmationUser] VARCHAR (20)  NULL,
    [ConfirmationDate] DATETIME      NULL,
    [AnnulmentUser]    VARCHAR (20)  NULL,
    [AnnulmentDate]    DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [RevenueControlId] INT           NULL,
    [EntityName]       VARCHAR (250) NULL,
    [EntityCode]       VARCHAR (20)  NULL,
    [EntityId]         INT           NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingDevolution__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaceuticalDispensingDevolution_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDevolution_RevenueControl] FOREIGN KEY ([RevenueControlId]) REFERENCES [Billing].[RevenueControl] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDevolution_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDevolution] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDevolution_OperatingUnit];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDevolution] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDevolution_RevenueControl];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDevolution] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDevolution_Warehouse];


GO
CREATE NONCLUSTERED INDEX [IX_PharmaceuticalDispensingDevolution_AdmissionNumber_Status]
    ON [Inventory].[PharmaceuticalDispensingDevolution]([AdmissionNumber] ASC, [Status] ASC);

