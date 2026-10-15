CREATE TABLE [Inventory].[LoanMerchandiseDevolution] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              VARCHAR (20)  NOT NULL,
    [OperatingUnitId]   INT           NOT NULL,
    [DocumentDate]      DATETIME      NOT NULL,
    [WarehouseId]       INT           NOT NULL,
    [LoanMerchandiseId] INT           NOT NULL,
    [Observation]       VARCHAR (300) NULL,
    [Status]            TINYINT       NOT NULL,
    [CreationUser]      VARCHAR (20)  CONSTRAINT [DF_LoanMerchandiseDevolution_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]      DATETIME      CONSTRAINT [DF_LoanMerchandiseDevolution_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]  VARCHAR (20)  NULL,
    [ModificationDate]  DATETIME      NULL,
    [ConfirmationUser]  VARCHAR (20)  NULL,
    [ConfirmationDate]  DATETIME      NULL,
    [AnnulmentUser]     VARCHAR (20)  NULL,
    [AnnulmentDate]     DATETIME      NULL,
    [TimeStamp]         ROWVERSION    NOT NULL,
    CONSTRAINT [PK_LoanMerchandiseDevolution__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanMerchandiseDevolution_LoanMerchandise] FOREIGN KEY ([LoanMerchandiseId]) REFERENCES [Inventory].[LoanMerchandise] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolution_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolution_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolution] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolution_LoanMerchandise];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolution] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolution_OperatingUnit];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolution] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolution_Warehouse];

