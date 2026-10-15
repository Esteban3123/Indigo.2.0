CREATE TABLE [Billing].[InvoiceEntityCapitated] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)    NOT NULL,
    [OperatingUnitId]        INT             NOT NULL,
    [DocumentDate]           DATETIME        NOT NULL,
    [CareGroupId]            INT             NOT NULL,
    [InitialDate]            DATE            NULL,
    [EndDate]                DATE            NULL,
    [UserNumber]             INT             NOT NULL,
    [UserValue]              DECIMAL (18, 2) NOT NULL,
    [TotalValue]             DECIMAL (18, 2) NOT NULL,
    [InvoiceCategoryId]      INT             NOT NULL,
    [InvoiceId]              INT             NULL,
    [BillingAuthorizationId] INT             NULL,
    [Status]                 TINYINT         NOT NULL,
    [CreationUser]           VARCHAR (20)    NOT NULL,
    [CreationDate]           DATETIME        NOT NULL,
    [ModificationUser]       VARCHAR (20)    NULL,
    [ModificationDate]       DATETIME        NULL,
    [ConfirmationUser]       VARCHAR (20)    NULL,
    [ConfirmationDate]       DATETIME        NULL,
    [AnnulmentUser]          VARCHAR (20)    NULL,
    [AnnulmentDate]          DATETIME        NULL,
    [TimeStamp]              ROWVERSION      NOT NULL,
    [DiscountPercentage]     NUMERIC (5, 2)  CONSTRAINT [DF_InvoiceEntityCapitated_DiscountPercentage] DEFAULT ((0)) NOT NULL,
    [DiscountValue]          DECIMAL (18, 2) CONSTRAINT [DF_InvoiceEntityCapitated_DiscountValue] DEFAULT ((0)) NOT NULL,
    [Observations]           VARCHAR (MAX)   NULL,
    [InvoicePeriod]          TINYINT         NULL,
    [PreviousRIPSInvoice]    INT             NULL,
    [CurrencyId]             INT             NOT NULL,
    [CopaymentAmount]        DECIMAL (18, 2) CONSTRAINT [DF_InvoiceEntityCapitated_CopaymentAmount] DEFAULT ((0.00)) NOT NULL,
    [ModeratingFeeAmount]    DECIMAL (18, 2) CONSTRAINT [DF_InvoiceEntityCapitated_ModeratingFeeAmount] DEFAULT ((0.00)) NOT NULL,
    [SharedPaymentAmount]    DECIMAL (18, 2) CONSTRAINT [DF_InvoiceEntityCapitated_SharedPaymentAmount] DEFAULT ((0.00)) NOT NULL,
    CONSTRAINT [PK_InvoiceEntityCapitated__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceEntityCapitated_BillingAuthorization] FOREIGN KEY ([BillingAuthorizationId]) REFERENCES [Billing].[BillingAuthorization] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitated_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitated_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitated_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitated_InvoiceCategories] FOREIGN KEY ([InvoiceCategoryId]) REFERENCES [Billing].[InvoiceCategories] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitated_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_PreviousRIPSInvoice] FOREIGN KEY ([PreviousRIPSInvoice]) REFERENCES [Billing].[InvoiceEntityCapitated] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceEntityCapitated] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitated_BillingAuthorization];


GO
ALTER TABLE [Billing].[InvoiceEntityCapitated] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitated_CareGroup];


GO
ALTER TABLE [Billing].[InvoiceEntityCapitated] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitated_Invoice];


GO
ALTER TABLE [Billing].[InvoiceEntityCapitated] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitated_InvoiceCategories];


GO
ALTER TABLE [Billing].[InvoiceEntityCapitated] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitated_OperatingUnit];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_InvoiceEntityCapitated__Code]
    ON [Billing].[InvoiceEntityCapitated]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de los pagos compartidos, calculados en función de los servicios prestados y el periodo de referencia (PGP o Capitación).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'SharedPaymentAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de las cuotas moderadoras, aplicadas según los criterios de fechas establecidos para la atención (PGP o Capitación).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'ModeratingFeeAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto correspondiente a los copagos calculados según los servicios facturados dentro del rango de fechas definido (PGP o Capitación).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'CopaymentAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id que referencia a la Factura RIPS asociada anteriormente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'PreviousRIPSInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica las opciones de Factura perioro: 1. Inicial, 2. Periodo, 3. Final', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'InvoicePeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de la factura de monto fijo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de descuento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de decuento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del traslado (Registrado = 1, Confirmado = 2, Anulado = 3, Reversado = 4, Confirmado RIPS Final = 5)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la autorizacion de facturacion, permite null ya que se debe crear desde ordene de servicios, pero en liquidacion se debe pedir de forma obligatoria ya que a través de esta se saca el numero de factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la factura que se genra al confirmar el documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la factura es No POS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor total, este se calcula multiplicando el (numero de usuario) por el (valor por usuario)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor por Usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'UserValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Usuarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'UserNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la factura a entidad capitada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitated', @level2type = N'COLUMN', @level2name = N'Id';

