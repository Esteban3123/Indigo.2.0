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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la factura otorgado por haciendo cuando ya esta es factura electronica (Costa Rica)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ElectronicInvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TaxDevolutionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la moneda.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda oficial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Factura con IVA, con Descuentos y Retenciones', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del IVA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ValueTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Factura sin IVA, sin Descuentos y sin Retenciones', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del contrato al momento de realizar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Bidimensional QR del CUFE. Para la representación gráfica de las facturas electrónicas es requisito la generación de un código QR con la siguiente información:  NumFac: [NUMERO_FACTURA]  FecFac: [FECHA_FACTURA] en formato YYYYmmddHHMMss  NitFac: [NIT FACTURADOR] sin puntos ni guiones  DocAdq: [NUMERO_ID_ADQUIRIENTE] sin puntos ni guiones  ValFac: [VALOR_FACTURA] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValIva: [VALOR_IVA] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValOtroIm: [VALOR_OTROS_IMPUESTOS] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValFacIm: [VALOR_OTROS_IMPUESTOS] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  CUFE: [CUFE]', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'QR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código CUFE. Este código se genera con la concatenciación y la encripación SHA-1 del:  Numero de la Factura  Fecha Factura  Valor Factura  Codigo Impuesto 01 (Valor IVA)  Valor Impuesto 01  Codigo Impuesto 02 (Valor Impuesto al Consumo)  Valor Impuesto 02  Codigo Impuesto 03 (Valor ICA)  Valor Impuesto 03  Valor a Pagar  NIT Facturador Electrónico  Tipo de Adquiriente  Numero de Identificación del Adquiriente  Clave técnica del rango de Facturación  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CUFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autorización de Facturación asociada a la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la descripción de la reversión de la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'DescriptionReversal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la razón por la cual se reversa la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ReversalReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoicedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoicedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (Facturado = 1,Anulado = 2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de corte   1 - Fecha Ingreso - Fecha Egreso  2 - Fecha Ingreso - Fecha Corte  3 - Fecha Inicial - Fecha Corte  4 - Fecha Inicial - Fecha Egreso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CutType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Diagnostico de Egreso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OutputDiagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es un Corte de Cuenta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'IsCutAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Fecha de Egreso o de Corte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OutputDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial del rango de factura, puede ser la misma fecha del ingreso o la fecha inicial del corte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la factura es No POS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base de la retencion del impuesto del CREE - Pendiente definicion funcinal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CREETaxRetentionBaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la retencion del impuesto del CREE - Pendiente definicion funcional', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CREETaxRetentionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Nivel o Estrato del Paciente    Se trae de la tabla de paciente Indigo Crystal (INPACIENT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientSocialClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el paciente tiene capacidad de pago  0: No Aplica  1: Si / 100% Paciente  2: No / Cuota Recuperacion Paciente  3: Desplazado / 100% Entidad    Se trae de la tabla de paciente Indigo Crystal (INPACIENT) Solo para Subsidiado, Vinculados y Desplazados', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientPaidAbility';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Afiliacion del Paciente:  0: No Aplica  1: Cotizante  2: Beneficiario  3: Adicional  4: Jub/Retirado  5: Pensionado    Se trae de la tabla de paciente Indigo Crystal (INPACIENT) Solo para Contributivo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAffiliatedType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Paciente:  0=Contributivo  1=Subsidiado  2=Vinculado  3=Particular  5=Desplazado Reg. Contributivo  6=Desplazado Reg. Subsidiado  7=Desplazado no Asegurado    Se trae de la tabla de paciente Indigo Crystal (INPACIENT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por cobrar que se le genera al paciente por concepto de cuota de recuperacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la cuenta por cobrar que se le genera al paciente por concepto de cuota de recuperacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientAccountReceivableValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la cuenta por cobrar que se le genera a la entidad o al tercero', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyAccountReceivableValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Pagado por el Paciente. Corresponde a la sumatoria del recibo de caja generado al momento de realizar la liquidacion y los abonos afectados en el cruce', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientPaidValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Comprobante Contable que se genera para contabilizar la venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Recibo de Caja que se genera para cancelar los valores a cargo del paciente al momento de la liquidacion de la factura    O tambien puede ser el recibo de caja que se genera para pagar la factura de ventas de productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CashReceiptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del bono que le corresponde al paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ValueVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor real cobrado al paciente por concepto de cuota de recuperacion con descuento incluido.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalPatientWithDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de descuento aplicado a la cuota de recuperacion a cargo del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de descuento aplicado a la cuota de recuperacion a cargo del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al total de la cuota de recuperacion de la factura, es decir que es la sumatoria de todas las cuotas moderadoras de los detalles de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable de la Cuota de Recuperacion  1 - Ninguno  2 - Paciente  3 - Tercero', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ResponsibleRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del Descuento que se le realizo a la Entidad o tercero, es decir que es la suma de  todos los descuentos que se le ha realizado a cada item', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que le corresponde pagar a la entidad o al tercero, este valor ya tiene aplicado el descuento que se le hace a la entidad o tercero', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de cada pacientes a liquidar en la Capitacion - Aplica solo para facturas capitadas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationPatientValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de pacientes a liquidar en la Capitacion - Aplica solo para facturas capitadas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationlPatientsAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final Capitacion - Aplica solo para facturas capitadas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial Capitacion - Aplica solo para facturas capitadas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CapitationInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'TotalInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Vencimiento de la Factura  Corresponde a la fecha de factura mas el plazo estipulado en el grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente, esto se saca de tabla de INPACIENT de Crystal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del folio con el que se creo la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Factura y/o Documento de Control Capitacion  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Invoice', @level2type = N'COLUMN', @level2name = N'Id';

