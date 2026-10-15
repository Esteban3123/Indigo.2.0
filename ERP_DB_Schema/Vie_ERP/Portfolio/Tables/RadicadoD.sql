CREATE TABLE [Portfolio].[RadicadoD] (
    [RadicateInvoiceCId]          FLOAT (53)                                                               NULL,
    [GlosasParametersInterfaceId] NVARCHAR (255)                                                           NULL,
    [InvoiceDocumentType]         FLOAT (53)                                                               NULL,
    [InvoiceNumber]               NVARCHAR (255)                                                           NULL,
    [RadicatedNumber]             NVARCHAR (255)                                                           NULL,
    [RadicatedDate]               DATETIME                                                                 NULL,
    [InvoiceValueEntity]          FLOAT (53)                                                               NULL,
    [InvoiceValuePacient]         FLOAT (53)                                                               NULL,
    [BalanceInvoice]              FLOAT (53)                                                               NULL,
    [UserNameInvoice]             FLOAT (53)                                                               NULL,
    [IngressNumber]               FLOAT (53)                                                               NULL,
    [IngressDate]                 DATETIME                                                                 NULL,
    [AccountantAccountCustomers]  NVARCHAR (255)                                                           NULL,
    [PatientCode]                 FLOAT (53)                                                               NULL,
    [PatientName]                 NVARCHAR (255) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)') NULL,
    [ContractCode]                NVARCHAR (255)                                                           NULL,
    [PlanCode]                    NVARCHAR (255)                                                           NULL,
    [ContractEntity]              NVARCHAR (255)                                                           NULL,
    [InvoiceDate]                 DATETIME                                                                 NULL,
    [CreditNoteValue]             FLOAT (53)                                                               NULL,
    [DebitNoteValue]              FLOAT (53)                                                               NULL,
    [Devolution]                  FLOAT (53)                                                               NULL,
    [ConceptDevolution]           NVARCHAR (255)                                                           NULL,
    [State]                       FLOAT (53)                                                               NULL,
    [TimeStamp]                   NVARCHAR (255)                                                           NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el estado del detalle del radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece descentralización del concepto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ConceptDevolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el valor de devolución.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'Devolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el valor de la nota débito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'DebitNoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el valor de la nota crédito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'CreditNoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la fecha de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece entidad del contrato.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ContractEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan asignado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica código del contrato.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece cuentas contables del cliente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de ingreso.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'IngressDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el número de ingreso.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'IngressNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del usuario de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica saldo de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el valor de la factura del paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece entidad del valor de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece la fecha de radicación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el número de radicación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el número de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el tipo de documento de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceDocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la interfaz de parámetros de glosas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera de la factura radicada.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicateInvoiceCId';

