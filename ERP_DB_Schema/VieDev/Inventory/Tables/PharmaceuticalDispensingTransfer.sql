CREATE TABLE [Inventory].[PharmaceuticalDispensingTransfer] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)  NOT NULL,
    [OperatingUnitId]            INT           NOT NULL,
    [DocumentDate]               DATETIME      NOT NULL,
    [WarehouseId]                INT           NOT NULL,
    [AdmissionNumber]            CHAR (10)     NOT NULL,
    [AdmissionNumberDestination] CHAR (10)     NOT NULL,
    [Observation]                VARCHAR (300) NULL,
    [Status]                     TINYINT       NOT NULL,
    [CreationUser]               VARCHAR (20)  NOT NULL,
    [CreationDate]               DATETIME      NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    [ConfirmationUser]           VARCHAR (20)  NULL,
    [ConfirmationDate]           DATETIME      NULL,
    [AnnulmentUser]              VARCHAR (20)  NULL,
    [AnnulmentDate]              DATETIME      NULL,
    [TimeStamp]                  ROWVERSION    NOT NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingTransfer__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaceuticalDispensingTransfer_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingTransfer_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingTransfer] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingTransfer_OperatingUnit];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingTransfer] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingTransfer_Warehouse];

