CREATE TABLE [Billing].[Invoice] (
    [Id]                               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]                  INT                                                                              NOT NULL,
    [DocumentType]                     TINYINT                                                                          NOT NULL,
    [InvoiceNumber]                    VARCHAR (20)                                                                     NOT NULL,
    [RevenueControlDetailId]           INT                                                                              NULL,
    [AdmissionNumber]                  CHAR (10)                                                                        NULL,
    [HealthAdministratorId]            INT                                                                              NULL,
    [ThirdPartyId]                     INT                                                                              NOT NULL,
    [PatientCode]                      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [CareGroupId]                      INT                                                                              NULL,
    [InvoiceDate]                      DATETIME                                                                         NOT NULL,
    [InvoiceExpirationDate]            DATETIME                                                                         NOT NULL,
    [TotalInvoice]                     NUMERIC (20, 2)                                                                  NOT NULL,
    [CapitationInitialDate]            DATETIME                                                                         NULL,
    [CapitationEndDate]                DATETIME                                                                         NULL,
    [CapitationlPatientsAmount]        INT                                                                              NULL,
    [CapitationPatientValue]           DECIMAL (18)                                                                     NOT NULL,
    [ThirdPartySalesValue]             DECIMAL (20, 2)                                                                  NOT NULL,
    [ThirdPartyDiscountValue]          NUMERIC (20, 2)                                                                  NOT NULL,
    [ResponsibleRecoveryFee]           TINYINT                                                                          NOT NULL,
    [TotalPatientSalesPrice]           NUMERIC (20, 2)                                                                  CONSTRAINT [DF_Invoice_TotalPatientSalesPrice] DEFAULT ((0)) NOT NULL,
    [PatientDiscount]                  NUMERIC (18)                                                                     CONSTRAINT [DF_Invoice_PatientDiscount] DEFAULT ((0)) NOT NULL,
    [PatientDiscountPercentage]        NUMERIC (5, 2)                                                                   CONSTRAINT [DF_Invoice_PatientDiscountPercentage] DEFAULT ((0)) NOT NULL,
    [TotalPatientWithDiscount]         NUMERIC (18)                                                                     CONSTRAINT [DF_Invoice_TotalPatientWithDiscount] DEFAULT ((0)) NOT NULL,
    [ValueVoucher]                     NUMERIC (18)                                                                     CONSTRAINT [DF_Invoice_ValueVoucher] DEFAULT ((0)) NOT NULL,
    [CashReceiptId]                    INT                                                                              NULL,
    [JournalVoucherId]                 INT                                                                              NULL,
    [PatientPaidValue]                 NUMERIC (20, 2)                                                                  NOT NULL,
    [ThirdPartyAccountReceivableValue] NUMERIC (20, 2)                                                                  NOT NULL,
    [PatientAccountReceivableValue]    NUMERIC (18)                                                                     NOT NULL,
    [PatientAccountReceivableId]       INT                                                                              NULL,
    [PatientType]                      INT                                                                              NULL,
    [PatientAffiliatedType]            INT                                                                              NULL,
    [PatientPaidAbility]               INT                                                                              NULL,
    [PatientSocialClass]               CHAR (3)                                                                         NULL,
    [CREETaxRetentionValue]            NUMERIC (18)                                                                     NOT NULL,
    [CREETaxRetentionBaseValue]        NUMERIC (18)                                                                     NOT NULL,
    [Observation]                      VARCHAR (MAX)                                                                    NULL,
    [InvoiceCategoryId]                INT                                                                              NULL,
    [InitialDate]                      DATETIME                                                                         NOT NULL,
    [OutputDate]                       DATETIME                                                                         NOT NULL,
    [IsCutAccount]                     BIT                                                                              CONSTRAINT [DF_Invoice_IsCutAccount] DEFAULT ((0)) NOT NULL,
    [OutputDiagnosis]                  CHAR (4)                                                                         NULL,
    [CutType]                          TINYINT                                                                          CONSTRAINT [DF_Invoice_CutType] DEFAULT ((1)) NOT NULL,
    [Status]                           TINYINT                                                                          NOT NULL,
    [InvoicedUser]                     VARCHAR (20)                                                                     NOT NULL,
    [InvoicedDate]                     DATETIME                                                                         NOT NULL,
    [AnnulmentUser]                    VARCHAR (20)                                                                     NULL,
    [AnnulmentDate]                    DATETIME                                                                         NULL,
    [ReversalReasonId]                 INT                                                                              NULL,
    [DescriptionReversal]              VARCHAR (MAX)                                                                    NULL,
    [TimeStamp]                        ROWVERSION                                                                       NOT NULL,
    [BillingAuthorizationId]           INT                                                                              NULL,
    [CUFE]                             VARCHAR (250)                                                                    NULL,
    [QR]                               VARCHAR (500)                                                                    NULL,
    [ContractId]                       INT                                                                              NULL,
    [InvoiceValue]                     DECIMAL (20, 2)                                                                  CONSTRAINT [DF_Invoice_InvoiceValue] DEFAULT ((0)) NOT NULL,
    [ValueTax]                         DECIMAL (20, 2)                                                                  CONSTRAINT [DF_Invoice_ValueTax] DEFAULT ((0)) NOT NULL,
    [TotalValue]                       DECIMAL (20, 2)                                                                  CONSTRAINT [DF_Invoice_TotalValue] DEFAULT ((0)) NOT NULL,
    [CurrencyId]                       INT                                                                              NULL,
    [TRMValue]                         NUMERIC (20, 5)                                                                  NULL,
    [TaxDevolutionValue]               NUMERIC (20, 2)                                                                  CONSTRAINT [DF__Invoice__TaxDevo__7F681368] DEFAULT ((0)) NOT NULL,
    [ElectronicInvoiceNumber]          VARCHAR (30)                                                                     NULL,
    [IsElectronicTicket]               BIT                                                                              DEFAULT ((0)) NOT NULL,
    [EconomicActivityId]               INT                                                                              NULL,
    CONSTRAINT [PK_Invoice__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Invoice_ValidateThirdPartySales] CHECK ([ThirdpartySalesValue]>=(0)),
    CONSTRAINT [FK_Invoice_AccountReceivable1] FOREIGN KEY ([PatientAccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_Invoice_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_Invoice_CashReceipts] FOREIGN KEY ([CashReceiptId]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_Invoice_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Contract].[Contract] ([Id]),
    CONSTRAINT [FK_Invoice_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_Invoice_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_Invoice_InvoiceCategories] FOREIGN KEY ([InvoiceCategoryId]) REFERENCES [Billing].[InvoiceCategories] ([Id]),
    CONSTRAINT [FK_Invoice_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_Invoice_RevenueControlDetail] FOREIGN KEY ([RevenueControlDetailId]) REFERENCES [Billing].[RevenueControlDetail] ([Id]),
    CONSTRAINT [FK_Invoice_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [CK_Invoice_ValidateThirdPartySales];


GO
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [FK_Invoice_AccountReceivable1];


GO
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [FK_Invoice_CareGroup];


GO
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [FK_Invoice_CashReceipts];


GO
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [FK_Invoice_Contract];


GO
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [FK_Invoice_Currency];


GO
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [FK_Invoice_HealthAdministrator];


GO
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [FK_Invoice_InvoiceCategories];


GO
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [FK_Invoice_OperatingUnit];


GO
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [FK_Invoice_RevenueControlDetail];


GO
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [FK_Invoice_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_Invoice_Status_Admission]
    ON [Billing].[Invoice]([Status] ASC, [AdmissionNumber] ASC, [Id] ASC)
    INCLUDE([InvoiceNumber], [OutputDiagnosis], [HealthAdministratorId], [RevenueControlDetailId], [DocumentType]);


GO
CREATE NONCLUSTERED INDEX [IX_Invoice_MegaPlane]
    ON [Billing].[Invoice]([DocumentType] ASC, [ThirdPartyId] ASC, [CareGroupId] ASC, [InvoiceCategoryId] ASC, [Status] ASC, [InvoiceDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Invoice_PatientCode]
    ON [Billing].[Invoice]([PatientCode] ASC);


GO
CREATE NONCLUSTERED INDEX [UX_Invoice_Status]
    ON [Billing].[Invoice]([Status] ASC)
    INCLUDE([OperatingUnitId], [InvoiceNumber], [RevenueControlDetailId], [InvoiceDate], [InvoicedUser], [CurrencyId]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Invoice__RevenueControlDetailId]
    ON [Billing].[Invoice]([RevenueControlDetailId] ASC) WHERE ([RevenueControlDetailId] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_Invoice__Status__InvoiceDate__INC__AdmissionNumber__CareGroupId__DocumentType__HealthAdministratorId__Id__InvoiceCategoryId__]
    ON [Billing].[Invoice]([Status] ASC, [InvoiceDate] ASC)
    INCLUDE([Id], [DocumentType], [InvoiceNumber], [RevenueControlDetailId], [InvoiceCategoryId], [IsCutAccount], [InvoicedUser], [AdmissionNumber], [HealthAdministratorId], [ThirdPartyId], [PatientCode], [CareGroupId], [TotalInvoice]);


GO
CREATE NONCLUSTERED INDEX [UX_Invoice_RevenueControlDetailId]
    ON [Billing].[Invoice]([RevenueControlDetailId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Invoice__InvoiceNumber]
    ON [Billing].[Invoice]([InvoiceNumber] ASC);


GO

-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 01/02/2016
-- Description:	Trigger para que genere error cuando hayan datos inconsistentes
-- =============================================
create TRIGGER [Billing].[TriggerValidateDataInvoice]
   ON [Billing].[Invoice]
   AFTER INSERT,UPDATE
AS 
BEGIN
	if (select count(*) from inserted) > 0 begin
		if(select count(*) from inserted i inner join Contract.CareGroup c on c.Id = i.CareGroupId where HealthAdministratorId is null and c.EntityType <> 99 and c.CareGroupType <> 3) > 0 begin
			THROW 51000, 'Error la factura no puede quedar con la entidad administradora vacia', 1
		end
	end
END
GO

-- =============================================
-- Author:		Oscar stiven astudillo
-- Create date: 2023-01-23
-- Description:	Trigger que muestra mensaje error cuando el tipo de liquidacion sea diferente al del grupo de atención 
-- =============================================
CREATE TRIGGER [Billing].[TriggerValidateLiquidationTypeCareGroup]
   ON [Billing].[Invoice]
   AFTER INSERT,UPDATE
AS 
BEGIN
	IF (SELECT COUNT(*) FROM INSERTED) > 0 
    BEGIN
        -- Validar si el tipo de liquidación en la factura es diferente al del grupo de atención
        IF (SELECT COUNT(*) 
            FROM INSERTED i
            JOIN Contract.CareGroup cg ON cg.Id = i.CareGroupId 
            JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = i.RevenueControlDetailId
            WHERE cg.LiquidationType <> rcd.LiquidationType) > 0 
        BEGIN
            -- Lanzar un error si los tipos de liquidación no coinciden
            THROW 51000, 'Mensaje desde trigger tabla [Billing].[Invoice] - El tipo de liquidación con el que se está confirmando la factura es diferente al del grupo de atención', 1;
        END
    END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el documento es un tiquete electronico de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'IsElectronicTicket';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la moneda.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda oficial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la descripción de la reversión de la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'DescriptionReversal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la razón por la cual se reversa la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ReversalReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de Documentos
1 = Factura EAPB con Contrato  
2 = Factura EAPB Sin Contrato  
3 = Factura Particular  
4 = Factura Capitada   
5 = Control de Capitacion  
6 = Factura Basica  
7 = Factura de Venta de Productos  
8 = Tiquete Electronico de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'DocumentType';

