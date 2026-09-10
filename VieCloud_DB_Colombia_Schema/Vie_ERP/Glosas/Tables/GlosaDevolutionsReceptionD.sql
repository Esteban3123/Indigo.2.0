CREATE TABLE [Glosas].[GlosaDevolutionsReceptionD] (
    [Id]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GlosaDevolutionsReceptionCId] INT                                                                              NOT NULL,
    [GlosasParametersInterfaceId]  INT                                                                              NULL,
    [InvoiceNumber]                VARCHAR (50)                                                                     NOT NULL,
    [InvoiceDate]                  DATETIME                                                                         NOT NULL,
    [RadicatedNumber]              VARCHAR (50)                                                                     NULL,
    [RadicatedDate]                DATETIME                                                                         NULL,
    [BalanceInvoice]               MONEY                                                                            NULL,
    [PatientCode]                  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [PatientName]                  VARCHAR (200) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NOT NULL,
    [Ingress]                      VARCHAR (15)                                                                     NOT NULL,
    [UserNameInvoice]              VARCHAR (200)                                                                    NOT NULL,
    [Comment]                      VARCHAR (500)                                                                    NULL,
    [PlanCode]                     VARCHAR (20)                                                                     NULL,
    [ContractCode]                 VARCHAR (20)                                                                     NOT NULL,
    [ContractName]                 VARCHAR (200)                                                                    NOT NULL,
    [State]                        CHAR (1)                                                                         NOT NULL,
    [TimeStamp]                    ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_GlosaDevolutionsReceptionD__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaDevolutionsReceptionD_GlosaDevolutionsReceptionC] FOREIGN KEY ([GlosaDevolutionsReceptionCId]) REFERENCES [Glosas].[GlosaDevolutionsReceptionC] ([Id]),
    CONSTRAINT [FK_GlosaDevolutionsReceptionD_GlosasParametersInterface] FOREIGN KEY ([GlosasParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Glosas].[GlosaDevolutionsReceptionD].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Glosas].[GlosaDevolutionsReceptionD].[PatientName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');




GO
CREATE NONCLUSTERED INDEX [IX_GlosaDevolutionsReceptionD__GlosaDevolutionsReceptionCId]
    ON [Glosas].[GlosaDevolutionsReceptionD]([GlosaDevolutionsReceptionCId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento de creación, registro o modificación del detalle de devolución de glosa. Registra el instante exacto de auditoría.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del detalle de devolución: 1=Sin confirmar, 2=Confirmado. Indica si la devolución de glosa ha sido validada.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1:Sin confirmar 2:Confirmado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del contrato (VARCHAR 200) asociado a la factura en devolución. Identificador legible del acuerdo comercial/contratual.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'ContractName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'ContractName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'ContractName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del contrato (VARCHAR 20) vinculado a la factura. Referencia al acuerdo comercial con asegurador o entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de beneficios o cobertura (VARCHAR 20) asociado al paciente. Identifica la modalidad de afiliación/aseguramiento.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de plan', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de observaciones/comentarios (VARCHAR 500) sobre la devolución de glosa, gestión administrativa o notas del proceso.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'comentario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o profesional de facturación (VARCHAR 200) responsable del registro o procesamiento de la factura en devolución.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de facturacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso, atención o admisión del paciente (VARCHAR 15). Referencia al evento clínico asociado a la factura.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Ingress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de ingreso', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Ingress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Ingress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente (VARCHAR 200, PII ofuscado). Identificador legible del sujeto de atención.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre de paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente: cédula, documento, RUT o equivalente (VARCHAR 25, PII ofuscado con Identification_Ofuscado). Documento único de identificación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificacion de paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente de la factura en devolución (MONEY NULL). Monto adeudado, diferencia o remanente en disputa de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'saldo de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación o presentación formal de la devolución ante la entidad pagadora (DATETIME NULL). Fecha de trámite administrativo.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código de radicación (VARCHAR 50, NULL) asignado al trámite de devolución. Referencia de seguimiento ante autoridades/asegurador.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión de la factura original (DATETIME). Fecha en que se generó el comprobante de cobro.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura o comprobante de cobro (VARCHAR 50). Identificador único del documento fiscal en devolución.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK (INT NULL) a tabla GlosasParametersInterface. Relación con configuración de interfaz o parámetros de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id relacion con configuracion de interfaces', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK (INT NOT NULL) a tabla GlosaDevolutionsReceptionC. Relación con el encabezado/cabecera de la devolución de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosaDevolutionsReceptionCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion con cabecera devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosaDevolutionsReceptionCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosaDevolutionsReceptionCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) del detalle de devolución de glosa. Clave primaria de la línea de procesamiento.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico detalle de devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las facturas incluidas en una devolución de glosa recibida. Registra cada factura glosada con su paciente, contrato, saldo, estado y trazabilidad dentro del proceso de gestión de glosas y cartera.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD';
