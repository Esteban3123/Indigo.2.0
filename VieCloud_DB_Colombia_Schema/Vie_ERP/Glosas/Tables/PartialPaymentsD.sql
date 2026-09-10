CREATE TABLE [Glosas].[PartialPaymentsD] (
    [Id]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PartialPaymentsCId]       INT                                                                              NOT NULL,
    [PortfolioGlosaId]         INT                                                                              NULL,
    [InvoiceNumber]            VARCHAR (50)                                                                     NOT NULL,
    [InvoiceDate]              DATETIME                                                                         NOT NULL,
    [RadicatedNumber]          VARCHAR (50)                                                                     NOT NULL,
    [RadicatedDate]            DATETIME                                                                         NOT NULL,
    [PatientCode]              VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [PatientName]              VARCHAR (200) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NULL,
    [ContractCode]             VARCHAR (15)                                                                     NOT NULL,
    [ValuePendingConciliation] MONEY                                                                            NULL,
    [ValuePayments]            MONEY                                                                            NULL,
    [State]                    CHAR (1)                                                                         NOT NULL,
    [TimeStamp]                ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_PartialPaymentsD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PartialPaymentsD_GlosaPortfolioGlosada] FOREIGN KEY ([PortfolioGlosaId]) REFERENCES [Glosas].[GlosaPortfolioGlosada] ([Id]),
    CONSTRAINT [FK_PartialPaymentsD_PartialPaymentsC] FOREIGN KEY ([PartialPaymentsCId]) REFERENCES [Glosas].[PartialPaymentsC] ([Id])
);


GO
ALTER TABLE [Glosas].[PartialPaymentsD] NOCHECK CONSTRAINT [FK_PartialPaymentsD_GlosaPortfolioGlosada];


GO
ALTER TABLE [Glosas].[PartialPaymentsD] NOCHECK CONSTRAINT [FK_PartialPaymentsD_PartialPaymentsC];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Glosas].[PartialPaymentsD].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Glosas].[PartialPaymentsD].[PatientName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');




GO
ALTER TABLE [Glosas].[PartialPaymentsD] NOCHECK CONSTRAINT [FK_PartialPaymentsD_GlosaPortfolioGlosada];


GO
ALTER TABLE [Glosas].[PartialPaymentsD] NOCHECK CONSTRAINT [FK_PartialPaymentsD_PartialPaymentsC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento: instante exacto de creación, registro o modificación del detalle de pago parcial. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del pago parcial: ''''1''''=sin confirmar, ''''2''''=confirmado. Indicador de validación y aceptación de la conciliación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 sin confirmar    2 confirmado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (MONEY) del pago parcial aplicado a la factura. Monto abonado en la transacción de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ValuePayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Pago parcial total Aplicado a la factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ValuePayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ValuePayments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (MONEY) pendiente de conciliación al momento del pago parcial. Saldo por validar o ajustar.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ValuePendingConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Pendiente de Conciliacion al momento de realizar el pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ValuePendingConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ValuePendingConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato (VARCHAR 15) asociado a la factura y pago parcial. Identificador del acuerdo comercial o prestacional.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del paciente (VARCHAR 200, PII ofuscado con Name_Ofuscado). Identificación nominal del beneficiario de la atención.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente (VARCHAR 25, PII ofuscado con Identification_Ofuscado): cédula, documento o equivalente. Clave única del beneficiario.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación (DATETIME) del pago parcial ante la aseguradora o entidad receptora. Instante de presentación oficial del trámite.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Radicacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicación (VARCHAR 50): comprobante de presentación ante la entidad. Referencia de trámite del pago parcial.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de radicacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de factura (DATETIME) original de la factura cuyos valores se están pagando parcialmente.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura (VARCHAR 50): identificador único del documento de cobro. Referencia de la reclamación o glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de relación con cartera de glosa (GlosaPortfolioGlosada). Vinculación a portafolio o lista de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Relacion de Cartera de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de relación con cabecera de pago parcial (PartialPaymentsC). Vinculación a encabezado de la transacción.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PartialPaymentsCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Relacion de Cabecera del pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PartialPaymentsCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'PartialPaymentsCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de detalle de pago parcial de factura. Clave primaria del registro de línea de pago.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de detalle de factura de pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de pagos parciales asociados a glosas de cartera. Registra cada línea de pago parcial vinculada a una factura radicada, incluyendo valores pendientes de conciliación, datos del paciente y el contrato correspondiente.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsD';
