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
ALTER TABLE [Billing].[Invoice] NOCHECK CONSTRAINT [FK_Invoice_RevenueControlDetail];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Billing].[Invoice].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
CREATE NONCLUSTERED INDEX [IX_Invoice_Status_Admission]
    ON [Billing].[Invoice]([Status] ASC, [AdmissionNumber] ASC, [Id] ASC)
    INCLUDE([InvoiceNumber], [OutputDiagnosis], [HealthAdministratorId], [RevenueControlDetailId], [DocumentType]);

GO
CREATE NONCLUSTERED INDEX [IX_Invoice__ElectronicInvoiceNumber__INC__InvoiceNumber__CareGroupId__CurrencyId] 
    ON [Billing].[Invoice] ([ElectronicInvoiceNumber] ASC) INCLUDE ([InvoiceNumber], [CareGroupId], [CurrencyId]) WHERE [ElectronicInvoiceNumber] IS NOT NULL;

GO
CREATE NONCLUSTERED INDEX [IX_Invoice_MegaPlane]
    ON [Billing].[Invoice]([DocumentType] ASC, [ThirdPartyId] ASC, [CareGroupId] ASC, [InvoiceCategoryId] ASC, [Status] ASC, [InvoiceDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Invoice_PatientCode]
    ON [Billing].[Invoice]([PatientCode] ASC);


GO
ALTER INDEX [IX_Invoice_PatientCode]
    ON [Billing].[Invoice] DISABLE;




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
			;THROW 51000, 'Error la factura no puede quedar con la entidad administradora vacia', 1
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
            ;THROW 51000, 'Mensaje desde trigger tabla [Billing].[Invoice] - El tipo de liquidación con el que se está confirmando la factura es diferente al del grupo de atención', 1;
        END
    END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (BIT) que determina si el documento es un tiquete electrónico de venta (tipo 8) para representación digital de transacciones de venta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'IsElectronicTicket';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el documento es un tiquete electronico de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'IsElectronicTicket';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'IsElectronicTicket';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura electrónica asignado por la autoridad fiscal (DIAN/Hacienda) cuando la factura es electrónica; aplica para Costa Rica y mercados regulados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ElectronicInvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la factura otorgado por haciendo cuando ya esta es factura electronica (Costa Rica)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ElectronicInvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ElectronicInvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (DECIMAL 20,2) del monto de devolución o ajuste de impuesto; base para cálculo de impuestos devueltos o créditos fiscales.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TaxDevolutionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TaxDevolutionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TaxDevolutionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la Tasa Representativa del Mercado (TRM); tipo de cambio oficial de conversión de moneda extranjera a moneda local (COP, etc.).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la moneda.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda oficial en que se emite la factura (FK Contract.Currency); define si es COP, USD, EUR u otra divisa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda oficial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de la factura con IVA incluido, descuentos y retenciones aplicadas; monto final a pagar por tercero + paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Factura con IVA, con Descuentos y Retenciones', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del Impuesto al Valor Agregado (IVA) calculado sobre la base imponible de la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ValueTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del IVA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ValueTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ValueTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base de la factura sin IVA, sin descuentos y sin retenciones; monto antes de impuestos y deducciones.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Factura sin IVA, sin Descuentos y sin Retenciones', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato vigente (FK Contract.Contract) al momento de generar la factura; vincula EAPB, plan y condiciones comerciales.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del contrato al momento de realizar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código QR bidimensional codificado con: número factura, fecha, NIT facturador, documento adquiriente, valor factura, IVA, otros impuestos y CUFE; requisito legal para factura electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'QR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Bidimensional QR del CUFE. Para la representación gráfica de las facturas electrónicas es requisito la generación de un código QR con la siguiente información:  NumFac: [NUMERO_FACTURA]  FecFac: [FECHA_FACTURA] en formato YYYYmmddHHMMss  NitFac: [NIT FACTURADOR] sin puntos ni guiones  DocAdq: [NUMERO_ID_ADQUIRIENTE] sin puntos ni guiones  ValFac: [VALOR_FACTURA] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValIva: [VALOR_IVA] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValOtroIm: [VALOR_OTROS_IMPUESTOS] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValFacIm: [VALOR_OTROS_IMPUESTOS] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  CUFE: [CUFE]', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'QR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'QR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Único de Factura Electrónica; resultado SHA-1 encriptado de número, fecha, valores, impuestos (IVA, ICA, consumo), monto a pagar, NIT, tipo/ID adquiriente y clave técnica de rango.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CUFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código CUFE. Este código se genera con la concatenciación y la encripación SHA-1 del:  Numero de la Factura  Fecha Factura  Valor Factura  Codigo Impuesto 01 (Valor IVA)  Valor Impuesto 01  Codigo Impuesto 02 (Valor Impuesto al Consumo)  Valor Impuesto 02  Codigo Impuesto 03 (Valor ICA)  Valor Impuesto 03  Valor a Pagar  NIT Facturador Electrónico  Tipo de Adquiriente  Numero de Identificación del Adquiriente  Clave técnica del rango de Facturación  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CUFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CUFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la autorización de facturación (rango DIAN o equivalente) asociada y utilizada para emitir legalmente la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autorización de Facturación asociada a la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) que registra el instante exacto de creación, modificación o evento crítico de la factura en base de datos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo (VARCHAR MAX) que especifica motivo, causa o justificación detallada de la anulación o reversión de la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'DescriptionReversal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la descripción de la reversión de la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'DescriptionReversal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'DescriptionReversal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la razón/causa de reversión (códigos internos: 1=Error facturación, 2=Duplicado, 3=Cancelación paciente, etc.); FK a catálogo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ReversalReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la razón por la cual se reversa la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ReversalReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ReversalReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se anula o cancela la factura; marca el momento del cambio de estado a anulado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de usuario (VARCHAR 20) que realizó la anulación o reversión de la factura; trazabilidad de auditoría.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación/emisión de la factura; marca el inicio del documento en el sistema.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoicedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoicedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoicedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de usuario (VARCHAR 20) que generó o creó la factura; responsable de emisión.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoicedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoicedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoicedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de estado de la factura (TINYINT): 1=Facturado (vigente), 2=Anulado (reversado/cancelado); controla visibilidad y cobrabilidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (Facturado = 1,Anulado = 2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de rango de fechas para liquidación (TINYINT 1-4): 1=Ingreso a Egreso, 2=Ingreso a Corte, 3=Inicial a Corte, 4=Inicial a Egreso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CutType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de corte   1 - Fecha Ingreso - Fecha Egreso  2 - Fecha Ingreso - Fecha Corte  3 - Fecha Inicial - Fecha Corte  4 - Fecha Inicial - Fecha Egreso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CutType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CutType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 (CHAR 4) del egreso o salida del paciente; ej: A009, B800, registra motivo de alta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OutputDiagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Diagnostico de Egreso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OutputDiagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OutputDiagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (BIT) que señala si la factura es un corte de cuenta parcial o ajuste; 1=corte, 0=liquidación completa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'IsCutAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es un Corte de Cuenta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'IsCutAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'IsCutAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de egreso del paciente o fecha de corte de cuenta; marca cierre del período de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OutputDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Fecha de Egreso o de Corte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OutputDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OutputDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial (DATETIME) del rango de facturación; puede ser fecha de ingreso o fecha inicial del corte establecido.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial del rango de factura, puede ser la misma fecha del ingreso o la fecha inicial del corte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de categoría de factura (FK InvoiceCategories); indica si es POS, No-POS, capitación u otro tipo de cobertura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la factura es No POS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de observaciones (VARCHAR MAX) con descripción, notas o comentarios adicionales de la factura para auditoría y gestión.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base (NUMERIC 18) sobre el cual se calcula la retención del impuesto CREE (Impuesto a la Renta para Equidad); sujeto a validación funcional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CREETaxRetentionBaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base de la retencion del impuesto del CREE - Pendiente definicion funcinal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CREETaxRetentionBaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CREETaxRetentionBaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de retención (NUMERIC 18) por concepto del impuesto CREE aplicable a la factura; pendiente definición funcional completa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CREETaxRetentionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la retencion del impuesto del CREE - Pendiente definicion funcional', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CREETaxRetentionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CREETaxRetentionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de nivel o estrato socioeconómico del paciente (CHAR 3): 1-6 según encuesta; obtenido de tabla INPACIENT Crystal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientSocialClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Nivel o Estrato del Paciente    Se trae de la tabla de paciente Indigo Crystal (INPACIENT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientSocialClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientSocialClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de capacidad de pago del paciente (INT): 0=No aplica, 1=Sí/100%, 2=No/cuota, 3=Desplazado; solo Subsidiado/Vinculado/Desplazado de Crystal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientPaidAbility';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el paciente tiene capacidad de pago  0: No Aplica  1: Si / 100% Paciente  2: No / Cuota Recuperacion Paciente  3: Desplazado / 100% Entidad    Se trae de la tabla de paciente Indigo Crystal (INPACIENT) Solo para Subsidiado, Vinculados y Desplazados', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientPaidAbility';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientPaidAbility';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de afiliación al régimen (INT): 0=No aplica, 1=Cotizante, 2=Beneficiario, 3=Adicional, 4=Jubilado, 5=Pensionado; solo Contributivo de Crystal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAffiliatedType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Afiliacion del Paciente:  0: No Aplica  1: Cotizante  2: Beneficiario  3: Adicional  4: Jub/Retirado  5: Pensionado    Se trae de la tabla de paciente Indigo Crystal (INPACIENT) Solo para Contributivo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAffiliatedType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAffiliatedType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo/régimen de paciente (INT): 0=Contributivo, 1=Subsidiado, 2=Vinculado, 3=Particular, 5/6/7=Desplazado; obtenido de INPACIENT Crystal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Paciente:  0=Contributivo  1=Subsidiado  2=Vinculado  3=Particular  5=Desplazado Reg. Contributivo  6=Desplazado Reg. Subsidiado  7=Desplazado no Asegurado    Se trae de la tabla de paciente Indigo Crystal (INPACIENT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta por cobrar (FK Portfolio.AccountReceivable) generada al paciente por cuota de recuperación/moderadora.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar que se le genera al paciente por concepto de cuota de recuperacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (NUMERIC 18) de la cuenta por cobrar del paciente por cuota de recuperación no pagada; deuda del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAccountReceivableValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la cuenta por cobrar que se le genera al paciente por concepto de cuota de recuperacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAccountReceivableValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAccountReceivableValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (NUMERIC 20,2) de la cuenta por cobrar generada a la entidad/EAPB/tercero; monto pendiente de pago a facturador.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyAccountReceivableValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la cuenta por cobrar que se le genera a la entidad o al tercero', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyAccountReceivableValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyAccountReceivableValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (NUMERIC 20,2) efectivamente pagado por el paciente; suma de recibos de caja + abonos en cruce de cuentas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientPaidValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Pagado por el Paciente. Corresponde a la sumatoria del recibo de caja generado al momento de realizar la liquidacion y los abonos afectados en el cruce', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientPaidValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientPaidValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante contable (asiento/póliza) generado para registrar la venta en contabilidad general.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Comprobante Contable que se genera para contabilizar la venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del recibo de caja (FK Treasury.CashReceipts) generado para cancelar cuota del paciente o para venta de productos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CashReceiptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Recibo de Caja que se genera para cancelar los valores a cargo del paciente al momento de la liquidacion de la factura    O tambien puede ser el recibo de caja que se genera para pagar la factura de ventas de productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CashReceiptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CashReceiptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (NUMERIC 18) del bono, vale o cupón de descuento asignado al paciente; reduce su cuota de recuperación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ValueVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del bono que le corresponde al paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ValueVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ValueVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor real (NUMERIC 18) que cobra el paciente por cuota de recuperación después de aplicar descuentos y bonos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalPatientWithDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor real cobrado al paciente por concepto de cuota de recuperacion con descuento incluido.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalPatientWithDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalPatientWithDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento (NUMERIC 5,2) aplicado a la cuota de recuperación del paciente; ej: 10%, 15%.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento aplicado a la cuota de recuperacion a cargo del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de descuento (NUMERIC 18) en valores absolutos restado de la cuota de recuperación del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de descuento aplicado a la cuota de recuperacion a cargo del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de cuota de recuperación/moderadora del paciente (NUMERIC 20,2); sumatoria de cuotas moderadoras de cada detalle.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al total de la cuota de recuperacion de la factura, es decir que es la sumatoria de todas las cuotas moderadoras de los detalles de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código responsable del pago de cuota de recuperación (TINYINT): 1=Ninguno, 2=Paciente, 3=Tercero/Entidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ResponsibleRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable de la Cuota de Recuperacion  1 - Ninguno  2 - Paciente  3 - Tercero', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ResponsibleRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ResponsibleRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de descuento (NUMERIC 20,2) concedido a la entidad/tercero; suma de todos los descuentos por ítem.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del Descuento que se le realizo a la Entidad o tercero, es decir que es la suma de  todos los descuentos que se le ha realizado a cada item', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor a pagar por la entidad/tercero (NUMERIC 20,2) con descuentos ya aplicados; monto facturado neto al pagador principal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que le corresponde pagar a la entidad o al tercero, este valor ya tiene aplicado el descuento que se le hace a la entidad o tercero', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor per cápita (DECIMAL 18) de cada paciente a liquidar en facturación capitada; aplica solo facturas capitadas (tipo 4, 5).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationPatientValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de cada pacientes a liquidar en la Capitacion - Aplica solo para facturas capitadas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationPatientValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationPatientValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/número de pacientes (INT) incluidos en la liquidación capitada; base para cálculo de valor total capitado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationlPatientsAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de pacientes a liquidar en la Capitacion - Aplica solo para facturas capitadas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationlPatientsAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationlPatientsAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final (DATETIME) del período de capitación; marca cierre del mes/período capitado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final Capitacion - Aplica solo para facturas capitadas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial (DATETIME) del período de capitación; marca inicio del mes/período capitado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial Capitacion - Aplica solo para facturas capitadas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total (NUMERIC 20,2) de la factura; suma de valores facturados antes de detallar impuestos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento (DATETIME) de la factura; fecha de factura más plazo comercial definido en grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Vencimiento de la Factura  Corresponde a la fecha de factura mas el plazo estipulado en el grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de emisión/creación de la factura; fecha legal del documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención (FK Contract.CareGroup); vincula plan, tarifas, convenios y condiciones de servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificador único del paciente (VARCHAR 25, PII Ofuscado con Identification_Ofuscado); obtenido de tabla INPACIENT Crystal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente, esto se saca de tabla de INPACIENT de Crystal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero/entidad (FK Common.ThirdParty) como EAPB, EPS, ARL, asegurador u otro pagador.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero administrador de salud (FK Contract.HealthAdministrator) con quien se firma el contrato; ej: EPS, EAPB.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente (CHAR 10, PII); obtenido de tabla ADINGRESO Crystal; vincula atención a factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del folio/rango de facturación (FK Billing.RevenueControlDetail) usado para crear la factura legalmente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del folio con el que se creo la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de factura (VARCHAR 20) o documento de control capitación; identificador único del documento emitido.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Factura y/o Documento de Control Capitacion  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de documento facturado (TINYINT 1-8): 1=EAPB con contrato, 2=EAPB sin contrato, 3=Particular, 4=Capitada, 5=Control Capitación, 6=Básica, 7=Venta Productos, 8=Tiquete Electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos de Documentos
1 = Factura EAPB con Contrato  
2 = Factura EAPB Sin Contrato  
3 = Factura Particular  
4 = Factura Capitada   
5 = Control de Capitacion  
6 = Factura Basica  
7 = Factura de Venta de Productos  
8 = Tiquete Electronico de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad operativa/funcional (FK Common.OperatingUnit); sede, clínica o centro de atención que emite la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la factura en la tabla; llave primaria que asegura unicidad del registro de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Facturas de cobro emitidas a pacientes y terceros pagadores (EPS, aseguradoras, empresas). Registra el encabezado de cada factura de venta: número, fecha, valores cobrados, descuentos, pagos, estado, anulación, datos de facturación electrónica (CUFE, QR) y la información del paciente, contrato y unidad operativa asociados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad económica asociada a la factura, utilizada para clasificación tributaria y reporte ante la DIAN (código de actividad económica CIIU).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
