CREATE TABLE [Inventory].[InventoryContract] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)    NOT NULL,
    [OperatingUnitId]            INT             NOT NULL,
    [ContractTypeId]             INT             NOT NULL,
    [InitialDate]                DATETIME        NOT NULL,
    [EndDate]                    DATETIME        NOT NULL,
    [SupplierId]                 INT             NOT NULL,
    [SupplierDistributionLineId] INT             NOT NULL,
    [ContractNumber]             VARCHAR (20)    NOT NULL,
    [Description]                VARCHAR (300)   NOT NULL,
    [PaymentMethod]              VARCHAR (200)   NOT NULL,
    [DeliveryMethod]             VARCHAR (200)   NULL,
    [DeliveryPlace]              VARCHAR (200)   NULL,
    [SourceOrder]                TINYINT         NOT NULL,
    [PurchaseProcess]            TINYINT         NOT NULL,
    [Exclusivity]                BIT             NOT NULL,
    [OnlyGuarantee]              BIT             NOT NULL,
    [TechnicalSupervicion]       VARCHAR (100)   NULL,
    [SupervisionExecution]       VARCHAR (100)   NULL,
    [Clauses]                    VARCHAR (MAX)   NULL,
    [Attachments]                VARCHAR (MAX)   NULL,
    [Availability]               VARCHAR (200)   NULL,
    [Resolution]                 VARCHAR (200)   NULL,
    [ResolutionDate]             DATE            NULL,
    [QuoteNumber]                VARCHAR (50)    NULL,
    [QuoteDate]                  DATE            NULL,
    [RecordNumber]               VARCHAR (50)    NULL,
    [RecordDate]                 DATE            NULL,
    [NegotiationType]            VARCHAR (200)   NULL,
    [Approved]                   VARCHAR (100)   NULL,
    [Deadline]                   DATE            NULL,
    [ValidityDate]               DATE            NULL,
    [Value]                      DECIMAL (18, 2) NOT NULL,
    [DiscountValue]              DECIMAL (18, 2) NOT NULL,
    [IvaValue]                   DECIMAL (18, 2) NOT NULL,
    [TotalValue]                 DECIMAL (18, 2) NOT NULL,
    [Status]                     TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    NOT NULL,
    [CreationDate]               DATETIME        NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    [DocumentDate]               DATETIME        NULL,
    [ManageProducts]             BIT             CONSTRAINT [DF_InventoryContract_ManageProducts] DEFAULT ((1)) NOT NULL,
    [ModificationValue]          DECIMAL (18, 2) CONSTRAINT [DF_InventoryContract_ModificationValue] DEFAULT ((0)) NOT NULL,
    [CurrencyId]                 INT             CONSTRAINT [DF__Inventory__Curre__64C92BC5] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_InventoryContract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryContract_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_InventoryContract_InventoryContractType] FOREIGN KEY ([ContractTypeId]) REFERENCES [Inventory].[InventoryContractType] ([Id]),
    CONSTRAINT [FK_InventoryContract_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_InventoryContract_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_InventoryContract_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryContract] NOCHECK CONSTRAINT [FK_InventoryContract_Currency];


GO
ALTER TABLE [Inventory].[InventoryContract] NOCHECK CONSTRAINT [FK_InventoryContract_InventoryContractType];


GO
ALTER TABLE [Inventory].[InventoryContract] NOCHECK CONSTRAINT [FK_InventoryContract_OperatingUnit];


GO
ALTER TABLE [Inventory].[InventoryContract] NOCHECK CONSTRAINT [FK_InventoryContract_Supplier];


GO
ALTER TABLE [Inventory].[InventoryContract] NOCHECK CONSTRAINT [FK_InventoryContract_SuppliersDistributionLines];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:   1. Registrado  2. Confirmado   3. Anulado  4. Legalizado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el proceso de compras  1 - Directa  2 - Negociacion Conjunta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'PurchaseProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen segun de la Cuantia  1 - Orden Simple  2 - Llamado Oferta  3 - Licitacion Publica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SourceOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de entrega', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DeliveryMethod';

