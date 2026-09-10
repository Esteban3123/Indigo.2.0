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
ADD SENSITIVITY CLASSIFICATION TO
    [Portfolio].[RadicadoD].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Portfolio].[RadicadoD].[PatientName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo NVARCHAR. Registra instante de creación, modificación o auditoría del detalle radicado. Útil para trazabilidad y sincronización.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del detalle del radicado (FLOAT). Indica situación: pendiente, glosa, aprobado, rechazado, pagado. Clave para flujo de facturación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado del detalle del radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto o motivo de devolución NVARCHAR. Especifica razón: devengue, error, ajuste, rechazo. Descentraliza la gestión contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ConceptDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece descentralización del concepto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ConceptDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ConceptDevolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de devolución FLOAT. Monto reembolsado al paciente o entidad por concepto de devengue, error o reclamación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'Devolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el valor de devolución.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'Devolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'Devolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de nota débito FLOAT. Monto de incremento/cargo en factura. Típico en ajustes, intereses, complementos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'DebitNoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el valor de la nota débito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'DebitNoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'DebitNoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de nota crédito FLOAT. Monto de descuento/abono en factura. Usado en devoluciones, bonificaciones, glosas aceptadas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'CreditNoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el valor de la nota crédito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'CreditNoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'CreditNoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión de factura DATETIME. Momento en que se expide el documento de cobro por servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la fecha de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entidad contratante NVARCHAR. Asegurador, EPS, IPS o pagador responsable. Identifica a quién facturar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ContractEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece entidad del contrato.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ContractEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ContractEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de salud asignado NVARCHAR. Plan contractual, cobertura y tarifario aplicables al paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del plan asignado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato NVARCHAR. Identificador único del acuerdo comercial entre IPS y entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica código del contrato.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del paciente NVARCHAR con ofuscación parcial (Name_Ofuscado). PII protegida. Búsqueda: identificación, documento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/cédula del paciente FLOAT. Identificador único del paciente. Similar a: número de identificación, documento de identidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuentas contables del cliente NVARCHAR. Códigos de cuenta para registros en mayor: 2105 clientes, 4105 ingresos, glosas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece cuentas contables del cliente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'AccountantAccountCustomers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso/admisión DATETIME. Momento en que el paciente accede al servicio de salud (urgencia, consulta, hospitalización).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'IngressDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de ingreso.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'IngressDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'IngressDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número/ID del ingreso FLOAT. Identificador único de la atención, episodio o admisión del paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'IngressNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el número de ingreso.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'IngressNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'IngressNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/responsable de la factura FLOAT. Profesional o sistema que generó, radicó o tramitó el documento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del usuario de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente de la factura FLOAT. Diferencia entre valor total y pagos recibidos. Estado de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica saldo de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor/copago del paciente FLOAT. Cuota, coaseguro o responsabilidad económica del paciente sobre servicios.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor de la factura del paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceValuePacient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor a cargo de la entidad FLOAT. Monto que debe pagar EPS, asegurador o entidad contractante por servicios.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece entidad del valor de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceValueEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación DATETIME. Momento en que se presenta formalmente la factura ante entidad pagadora (trámite oficial).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece la fecha de radicación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicación NVARCHAR. ID de presentación oficial ante el pagador. Comprobante de gestión de cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el número de radicación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura NVARCHAR. Identificador único del documento de cobro, asignado por sistema de facturación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento factura FLOAT. Clasifica: factura, nota crédito, nota débito, complemento RIPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceDocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el tipo de documento de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceDocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'InvoiceDocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID interfaz de parámetros de glosas NVARCHAR. Configuración y reglas para detección/cálculo de glosas en radicación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la interfaz de parámetros de glosas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID cabecera de factura radicada FLOAT. FK a tabla maestra RadicadoC. Agrupa detalles de un radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicateInvoiceCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la factura radicada.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicateInvoiceCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD', @level2type = N'COLUMN', @level2name = N'RadicateInvoiceCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de facturas radicadas ante entidades pagadoras (EPS, aseguradoras, etc.). Registra el estado de cobro de cada factura, incluyendo valores, glosas, notas débito/crédito, devoluciones y datos del paciente y contrato asociado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicadoD';
