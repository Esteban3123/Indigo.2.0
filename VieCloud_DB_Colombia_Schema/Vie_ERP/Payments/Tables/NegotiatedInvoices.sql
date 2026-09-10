CREATE TABLE [Payments].[NegotiatedInvoices] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CodeHash]         VARCHAR (36) NOT NULL,
    [DateNegotiation]  DATETIME     NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [Status]           TINYINT      CONSTRAINT [DF_NegotiatedInvoices_Status] DEFAULT ((1)) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_NegotiatedInvoices] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (SQL Server) que registra automáticamente el instante exacto de creación, modificación o evento en la negociación de factura. Utilizado para auditoría y control de cambios.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de negociación. Indica cuándo se actualizó el estado o detalles de la factura negociada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última confirmación o cambio en el registro de negociación. Identificación del profesional o sistema que modificó la factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien confirma', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la negociación de factura (TINYINT): 0=Inactiva, 1=Pendiente, 2=Aceptada/OK, 3=Error, 4=Negociada con errores. Controla el flujo de aprobación de glosas y acuerdos de pago.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0: Inactiva. 1:Pending, 2: OK, 3: Error, 4: Se negocia con errores', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó el registro de negociación de factura. Marca el inicio del proceso de negociación con terceros (aseguradores, entidades).', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) oficial de negociación de la factura. Momento en que se acuerdan términos, descuentos o ajustes entre el prestador de salud y el pagador.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'DateNegotiation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Negociacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'DateNegotiation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'DateNegotiation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código hash único (VARCHAR 36) que identifica y valida la integridad de la factura negociada. Garantiza la identificación unívoca del documento de negociación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'CodeHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo hash', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'CodeHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'CodeHash';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de negociación de facturas. Clave primaria que referencia cada verificación o transacción de negociación de comprobante de pago.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las verificaciones de negociaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de facturas negociadas en el proceso de pagos. Guarda cada acuerdo o renegociación de factura con su fecha, estado actual y trazabilidad de modificaciones.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices';
