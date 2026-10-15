CREATE TABLE [Glosas].[GlosaInvoiceDetail] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ObjectionsReceptionDId]  INT           NOT NULL,
    [InvoiceNumber]           VARCHAR (50)  NOT NULL,
    [InvoiceDetailNativeId]   INT           NULL,
    [ServiceOrderDetailId]    INT           NULL,
    [ServiceDate]             DATETIME      NULL,
    [ServiceCode]             VARCHAR (20)  NOT NULL,
    [ServiceName]             VARCHAR (250) NOT NULL,
    [ServiceAreaCode]         VARCHAR (10)  NULL,
    [DescriptionServiceArea]  VARCHAR (300) NULL,
    [MedicalCode]             VARCHAR (20)  NULL,
    [MedicalName]             VARCHAR (200) NULL,
    [BillerCode]              VARCHAR (20)  NULL,
    [BillerName]              VARCHAR (200) NULL,
    [BillingGroupCode]        VARCHAR (50)  NULL,
    [BillingGroup]            VARCHAR (120) NULL,
    [ValueServiceManual]      MONEY         NULL,
    [UnitValue]               MONEY         NOT NULL,
    [InvoicedValue]           MONEY         NULL,
    [Ammount]                 INT           NOT NULL,
    [CostCenterCode]          VARCHAR (14)  NOT NULL,
    [CostCenterName]          VARCHAR (500) NOT NULL,
    [TypeServiceProduct]      CHAR (1)      NULL,
    [TypeProcedure]           CHAR (1)      NULL,
    [AccountantAccountIncome] VARCHAR (30)  NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    CONSTRAINT [PK_GlosaInvoiceDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaInvoiceDetail_GlosaObjectionsReceptionD] FOREIGN KEY ([ObjectionsReceptionDId]) REFERENCES [Glosas].[GlosaObjectionsReceptionD] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaInvoiceDetail] NOCHECK CONSTRAINT [FK_GlosaInvoiceDetail_GlosaObjectionsReceptionD];


GO
CREATE NONCLUSTERED INDEX [IX_GID_ByObjectionD]
    ON [Glosas].[GlosaInvoiceDetail]([ObjectionsReceptionDId] ASC)
    INCLUDE([InvoiceNumber], [ServiceCode], [ServiceName], [CostCenterCode], [CostCenterName]);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaInvoiceDetail_InvoiceNumber_AccountantAccountIncome_Ammount_BillerCode_BillerName_BillingGroup_BillingGroupCode]
    ON [Glosas].[GlosaInvoiceDetail]([InvoiceNumber] ASC)
    INCLUDE([AccountantAccountIncome], [Ammount], [BillerCode], [BillerName], [BillingGroup], [BillingGroupCode], [CostCenterCode], [CostCenterName], [DescriptionServiceArea], [InvoiceDetailNativeId], [InvoicedValue], [MedicalCode], [MedicalName], [ObjectionsReceptionDId], [ServiceAreaCode], [ServiceCode], [ServiceDate], [ServiceName], [ServiceOrderDetailId], [TimeStamp], [TypeProcedure], [TypeServiceProduct], [UnitValue], [ValueServiceManual]);

