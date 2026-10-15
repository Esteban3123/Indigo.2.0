CREATE TABLE [Inventory].[DocumentInvoiceProductSalesDevolution] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                          VARCHAR (20)    NOT NULL,
    [DocumentDate]                  DATETIME        NOT NULL,
    [WarehouseId]                   INT             NOT NULL,
    [Detail]                        VARCHAR (300)   NULL,
    [DocumentInvoiceProductSalesId] INT             NOT NULL,
    [FreightValue]                  DECIMAL (18, 2) NOT NULL,
    [FreightIVAPercentage]          NUMERIC (5, 2)  NOT NULL,
    [FreightIVAValue]               DECIMAL (18, 2) NOT NULL,
    [Value]                         DECIMAL (18, 2) NOT NULL,
    [ValueDiscount]                 DECIMAL (18, 2) NOT NULL,
    [ValueTax]                      DECIMAL (18, 2) NOT NULL,
    [WithholdingTax]                DECIMAL (18, 2) NOT NULL,
    [WithholdingICA]                DECIMAL (18, 2) NOT NULL,
    [RetentionSource]               DECIMAL (18, 2) NOT NULL,
    [RetentionOther]                DECIMAL (18, 2) NOT NULL,
    [DeductionOther]                DECIMAL (18, 2) NOT NULL,
    [DistrictTax]                   DECIMAL (18, 2) NOT NULL,
    [TotalValue]                    DECIMAL (18, 2) NOT NULL,
    [Status]                        TINYINT         NOT NULL,
    [CreationUser]                  VARCHAR (20)    NOT NULL,
    [CreationDate]                  DATETIME        NOT NULL,
    [ModificationUser]              VARCHAR (20)    NULL,
    [ModificationDate]              DATETIME        NULL,
    [ConfirmationUser]              VARCHAR (20)    NULL,
    [ConfirmationDate]              DATETIME        NULL,
    [AnnulmentUser]                 VARCHAR (20)    NULL,
    [AnnulmentDate]                 DATETIME        NULL,
    [TimeStamp]                     ROWVERSION      NOT NULL,
    CONSTRAINT [PK_DocumentInvoiceProductSalesDevolution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDevolution_DocumentInvoiceProductSales] FOREIGN KEY ([DocumentInvoiceProductSalesId]) REFERENCES [Inventory].[DocumentInvoiceProductSales] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDevolution_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSalesDevolution] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSalesDevolution_DocumentInvoiceProductSales];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSalesDevolution] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSalesDevolution_Warehouse];

