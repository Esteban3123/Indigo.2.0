CREATE TABLE [Glosas].[GlosaInvoiceDetailQX] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceDetailId]              INT           NOT NULL,
    [ServiceOrderDetailSurgicalId] INT           NULL,
    [ServiceCode]                  VARCHAR (50)  NOT NULL,
    [ServiceName]                  VARCHAR (300) NOT NULL,
    [MedicalCode]                  VARCHAR (20)  NULL,
    [MedicalName]                  NCHAR (200)   NULL,
    [ValueServiceManual]           MONEY         NOT NULL,
    [UnitValue]                    MONEY         NOT NULL,
    [InvoicedValue]                MONEY         NOT NULL,
    [Ammount]                      INT           NOT NULL,
    [CostCenterCode]               VARCHAR (100) NOT NULL,
    [CostCenterName]               NCHAR (500)   NOT NULL,
    [ServiceAreaCode]              VARCHAR (10)  NOT NULL,
    [DescriptionServiceArea]       NCHAR (300)   NOT NULL,
    [AccountantAccountIncome]      VARCHAR (30)  NOT NULL,
    [TimeStamp]                    ROWVERSION    NOT NULL,
    CONSTRAINT [PK_GlosaInvoiceDetailQX] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaInvoiceDetailQX_InvoiceDetail] FOREIGN KEY ([InvoiceDetailId]) REFERENCES [Glosas].[GlosaInvoiceDetail] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaInvoiceDetailQX] NOCHECK CONSTRAINT [FK_GlosaInvoiceDetailQX_InvoiceDetail];


GO
CREATE NONCLUSTERED INDEX [IX_GlosaInvoiceDetailQX_InvoiceDetailId_[AccountantAccountIncome_Ammount_CostCenterCode_CostCenterName_[DescriptionServiceArea]
    ON [Glosas].[GlosaInvoiceDetailQX]([InvoiceDetailId] ASC)
    INCLUDE([AccountantAccountIncome], [Ammount], [CostCenterCode], [CostCenterName], [DescriptionServiceArea], [InvoicedValue], [MedicalCode], [MedicalName], [ServiceAreaCode], [ServiceCode], [ServiceName], [ServiceOrderDetailSurgicalId], [TimeStamp], [UnitValue], [ValueServiceManual]);

