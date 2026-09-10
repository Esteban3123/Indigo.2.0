CREATE TABLE [Billing].[InvoiceDetailJsonMap] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [InvoiceDetailId] INT           NOT NULL,
    [ConsecutiveJson] INT           NOT NULL,
    [ServiceNameJson] VARCHAR (250) NOT NULL,
    [CreationDate]    DATETIME      NOT NULL,
    [TimeStamp]       ROWVERSION    NOT NULL,
    CONSTRAINT [PK_InvoiceDetailJsonMap] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceDetailJsonMap_InvoiceDetail] FOREIGN KEY ([InvoiceDetailId]) REFERENCES [Billing].[InvoiceDetail] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del servicio tal como aparece en el JSON de validación ministerial. Campo de texto que identifica la prestación de salud reportada (procedimiento, consulta, medicamento, examen).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'ServiceNameJson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de los servicios del Json del ministerio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'ServiceNameJson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'ServiceNameJson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del servicio dentro del JSON ministerial. Índice de orden que posiciona cada línea de factura en la estructura de validación enviada al ministerio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'ConsecutiveJson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de los servicios del Json del ministerio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'ConsecutiveJson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'ConsecutiveJson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de factura. Referencia a [Billing].[InvoiceDetail] para vincular la línea de facturación (procedimiento, servicio, medicamento) con su representación en JSON.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la tabla de factura ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de mapeo. Clave primaria que identifica unívocamente cada relación detalle-JSON.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mapeo de detalles de factura con JSON de validación ministerial. Vincula líneas de facturación (detalle) con la estructura JSON generada para cumplimiento normativo ante el ministerio de salud (RIPS, validaciones, auditoría).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla que guarda la relacion de los detalles de la factura con el Json Generado para validacion al ministerio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registró el mapeo del detalle de factura al formato JSON.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo automática del sistema para control de concurrencia y auditoría de cambios en el registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'TimeStamp';
