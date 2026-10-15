CREATE TABLE [Inventory].[PharmaceuticalDispensing] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                  VARCHAR (20)  NOT NULL,
    [OperatingUnitId]       INT           NOT NULL,
    [AdmissionNumber]       CHAR (10)     NOT NULL,
    [DocumentDate]          DATETIME      NOT NULL,
    [AffectInventory]       BIT           CONSTRAINT [DF_PharmaceuticalDispensing_AffectInventory] DEFAULT ((1)) NOT NULL,
    [Status]                TINYINT       NOT NULL,
    [CreationUser]          VARCHAR (20)  NOT NULL,
    [CreationDate]          DATETIME      NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [ConfirmationUser]      VARCHAR (20)  NULL,
    [ConfirmationDate]      DATETIME      NULL,
    [AnnulmentUser]         VARCHAR (20)  NULL,
    [AnnulmentDate]         DATETIME      NULL,
    [TimeStamp]             ROWVERSION    NOT NULL,
    [SurgeryExpenseSheetId] INT           NULL,
    [EntityName]            VARCHAR (250) NULL,
    [EntityCode]            VARCHAR (20)  NULL,
    [EntityId]              INT           NULL,
    CONSTRAINT [PK_PharmaceuticalDispensing__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaceuticalDispensing_OperatingUnit1] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensing] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensing_OperatingUnit1];

