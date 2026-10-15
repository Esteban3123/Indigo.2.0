CREATE TABLE [Inventory].[DocumentInvoiceProductSales] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)    NOT NULL,
    [BillingAuthorizationId]    INT             NOT NULL,
    [FunctionalUnitId]          INT             NOT NULL,
    [DocumentDate]              DATETIME        NOT NULL,
    [OperatingUnitId]           INT             CONSTRAINT [DF_DocumentInvoiceProductSales_OperatingUnitId] DEFAULT ((9)) NOT NULL,
    [ThirdPartyId]              INT             NOT NULL,
    [WarehouseId]               INT             NOT NULL,
    [Description]               VARCHAR (MAX)   NULL,
    [Value]                     DECIMAL (18, 2) NOT NULL,
    [ValueDiscount]             DECIMAL (18, 2) NOT NULL,
    [ValueTax]                  DECIMAL (18, 2) NOT NULL,
    [TotalValue]                DECIMAL (18, 2) NOT NULL,
    [InvoiceId]                 INT             NULL,
    [RecognitionId]             INT             NULL,
    [Status]                    TINYINT         NOT NULL,
    [CreationUser]              VARCHAR (20)    NOT NULL,
    [CreationDate]              DATETIME        NOT NULL,
    [ModificationUser]          VARCHAR (20)    NULL,
    [ModificationDate]          DATETIME        NULL,
    [ConfirmationUser]          VARCHAR (20)    NULL,
    [ConfirmationDate]          DATETIME        NULL,
    [AnnulmentUser]             VARCHAR (20)    NULL,
    [AnnulmentDate]             DATETIME        NULL,
    [TimeStamp]                 ROWVERSION      NOT NULL,
    [BranchOfficeId]            INT             NULL,
    [RetentionSource]           DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Reten__5E13A343] DEFAULT ((0)) NOT NULL,
    [WithholdingICA]            DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Withh__5F07C77C] DEFAULT ((0)) NOT NULL,
    [WithholdingTax]            DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Withh__5FFBEBB5] DEFAULT ((0)) NOT NULL,
    [DistrictTax]               DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Distr__60F00FEE] DEFAULT ((0)) NOT NULL,
    [IcaPercentage]             DECIMAL (6, 3)  CONSTRAINT [DF__DocumentI__IcaPe__61E43427] DEFAULT ((0)) NOT NULL,
    [FreightIVAValue]           DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Freig__62D85860] DEFAULT ((0)) NOT NULL,
    [FreightValue]              DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Freig__63CC7C99] DEFAULT ((0)) NOT NULL,
    [SaleModality]              TINYINT         CONSTRAINT [DF__DocumentI__SaleM__1D8F09DA] DEFAULT ((1)) NOT NULL,
    [ContractExternalClientsId] INT             NULL,
    [ConditionSalesId]          INT             NULL,
    [EconomicActivityId]        INT             NULL,
    CONSTRAINT [PK_DocumentInvoiceProductSales] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentInvoiceProductSales_BillingAuthorization] FOREIGN KEY ([BillingAuthorizationId]) REFERENCES [Billing].[BillingAuthorization] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_ConditionSales] FOREIGN KEY ([ConditionSalesId]) REFERENCES [Billing].[ConditionSales] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_ContractExternalClientsId] FOREIGN KEY ([ContractExternalClientsId]) REFERENCES [MixingStation].[ContractExternalClients] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_Recognition] FOREIGN KEY ([RecognitionId]) REFERENCES [Budget].[Recognition] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_BillingAuthorization];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_BranchOffice];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_ContractExternalClientsId];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_FunctionalUnit];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_Invoice];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_OperatingUnit];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_Recognition];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_ThirdParty];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la condicion de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConditionSalesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relaciona el Id del Contrato de centro de atencion externo, cuando un documento lo tiene calcula el valor   de los productos decuardo a la nueva tarificacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto de distrito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'DistrictTax';

