CREATE TABLE [Billing].[ElectronicsRIPSDetail] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [ElectronicsRIPSId] INT            NOT NULL,
    [MessageCode]       VARCHAR (25)   NOT NULL,
    [Message]           VARCHAR (1000) NOT NULL,
    [CreationUser]      VARCHAR (20)   NOT NULL,
    [CreationDate]      DATETIME       NOT NULL,
    [Path]              VARCHAR (500)  NULL,
    [TypeMessage]       VARCHAR (50)   NULL,
    [SourcePath]        VARCHAR (500)  NULL,
    CONSTRAINT [PK_ElectronicRIPSDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicsRIPSDetail_ElectronicsRIPS] FOREIGN KEY ([ElectronicsRIPSId]) REFERENCES [Billing].[ElectronicsRIPS] ([Id])
);


GO
ALTER TABLE [Billing].[ElectronicsRIPSDetail] NOCHECK CONSTRAINT [FK_ElectronicsRIPSDetail_ElectronicsRIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta de origen del campo de datos reportado en el JSON transmitido al Ministerio de Protección Social (MPS); identifica la ubicación exacta del valor enviado en la factura electrónica RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'SourcePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Postula la información del dato que se reportó en cada campo al momento de transmitir el JSON al MPS ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'SourcePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'SourcePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta de ubicación del detalle de rechazo o validación generado por el Ministerio de Salud y Protección Social; señala específicamente qué campo o sección de la factura/nota transmitida contiene el error.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Path';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Postula el detalle del rechazo generado por la validación que realiza el Ministerio de salud y proteccion de cada factura o nota transmitida ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Path';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Path';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de validación RIPS (DATETIME); momento en que se registró el mensaje de validación del ministerio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o proceso que creó el registro de detalle de validación RIPS; identifica quién registró el rechazo o mensaje de validación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del mensaje de validación o rechazo emitido por el validador de RIPS electrónicos del Ministerio de Salud; detalla la causa del error en factura o nota.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'mensaje del validador de rips electronicos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del validador de RIPS electrónicos del Ministerio de Salud; identificador estándar del tipo de validación o rechazo aplicado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'MessageCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del mensaje dado por el validador de rips electronicos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'MessageCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'MessageCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/registro maestro de RIPS electrónico (FK); vincula el detalle de validación con su factura o nota RIPS de origen.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'ElectronicsRIPSId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la trazabilidad del rips electronico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'ElectronicsRIPSId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'ElectronicsRIPSId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de detalle de validación RIPS; clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de mensajes y errores generados durante el proceso de envío y validación de RIPS electrónicos. Registra cada evento, advertencia o error asociado a un archivo RIPS, incluyendo el mensaje, su origen y la ruta del archivo involucrado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o categoría del mensaje registrado, por ejemplo: error, advertencia o informativo. Indica la severidad o naturaleza del evento ocurrido durante el procesamiento del RIPS electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'TypeMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'TypeMessage';
