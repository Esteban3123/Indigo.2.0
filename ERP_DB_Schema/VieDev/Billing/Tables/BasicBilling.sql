CREATE TABLE [Billing].[BasicBilling] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)    NOT NULL,
    [DocumentDate]                 DATETIME        NOT NULL,
    [Description]                  VARCHAR (MAX)   NOT NULL,
    [SaleModality]                 TINYINT         NOT NULL,
    [Status]                       TINYINT         NOT NULL,
    [OperatingUnitId]              INT             NOT NULL,
    [BillingAuthorizationId]       INT             NOT NULL,
    [FunctionalUnitId]             INT             NULL,
    [CustomerId]                   INT             NOT NULL,
    [AddressId]                    INT             NOT NULL,
    [WarehouseId]                  INT             NULL,
    [InvoiceId]                    INT             NULL,
    [Value]                        DECIMAL (18, 2) NOT NULL,
    [ValueDiscount]                DECIMAL (18, 2) NOT NULL,
    [ValueIVA]                     DECIMAL (18, 2) NOT NULL,
    [WithholdingTax]               DECIMAL (18, 2) NOT NULL,
    [RetentionIdIVA]               INT             NULL,
    [RetentionPercentageIVA]       DECIMAL (6, 3)  NOT NULL,
    [WithholdingIVA]               DECIMAL (18, 2) NOT NULL,
    [WithholdingICA]               DECIMAL (18, 2) NOT NULL,
    [TotalValue]                   DECIMAL (18, 2) NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [ConfirmationUser]             VARCHAR (20)    NULL,
    [ConfirmationDate]             DATETIME        NULL,
    [AnnulmentUser]                VARCHAR (20)    NULL,
    [AnnulmentDate]                DATETIME        NULL,
    [TimeStamp]                    ROWVERSION      NOT NULL,
    [RoundLevel]                   INT             NOT NULL,
    [BudgetId]                     INT             NULL,
    [ReversalReasonId]             INT             NULL,
    [ReversalReasonDescription]    VARCHAR (200)   NULL,
    [CurrencyId]                   INT             CONSTRAINT [DF__BasicBill__Curre__11A6BF34] DEFAULT ((2)) NOT NULL,
    [ThirdPartyEntityCopayId]      INT             NULL,
    [CommercialDiscountAccounting] BIT             NULL,
    [ConditionSalesId]             INT             NULL,
    [IsImported]                   BIT             CONSTRAINT [DF__BasicBill__IsImp__1266A49A] DEFAULT ((0)) NOT NULL,
    [EconomicActivityId]           INT             NULL,
    CONSTRAINT [PK_BasicBilling] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasicBilling_Address] FOREIGN KEY ([AddressId]) REFERENCES [Common].[Address] ([Id]),
    CONSTRAINT [FK_BasicBilling_BillingAuthorization] FOREIGN KEY ([BillingAuthorizationId]) REFERENCES [Billing].[BillingAuthorization] ([Id]),
    CONSTRAINT [FK_BasicBilling_BillingReversalReason] FOREIGN KEY ([ReversalReasonId]) REFERENCES [Billing].[BillingReversalReason] ([Id]),
    CONSTRAINT [FK_BasicBilling_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_BasicBilling_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_BasicBilling_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_BasicBilling_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_BasicBilling_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_BasicBilling_RetentionConcepts] FOREIGN KEY ([RetentionIdIVA]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_BasicBilling_ThirdPartyEntityCopay] FOREIGN KEY ([ThirdPartyEntityCopayId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_BasicBilling_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_Address];


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_BillingAuthorization];


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_BillingReversalReason];


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_Budget];


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_Currency];


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_Customer];


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_Invoice];


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_OperatingUnit];


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_RetentionConcepts];


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_ThirdPartyEntityCopay];


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano que determina verdadero si el registro se creo a partir de importación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'IsImported';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la condicion de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ConditionSalesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano que especifica verdadero si la factura basica tiene parametrizado el descuento comercial en parametros de facturación para tenerlo en cuenta al momento de reversarla.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CommercialDiscountAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercero de la entidad administradora que genera el copago', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ThirdPartyEntityCopayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la moneda del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modalidad de Venta:       1. Contado      2. Crédito', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'SaleModality';

