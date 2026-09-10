CREATE TABLE [Integrations].[Fomag_orderSync] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Identificador] VARCHAR (50)   NOT NULL,
    [FechaEnvio]    DATETIME2 (7)  NOT NULL,
    [Estado]        INT            NOT NULL,
    [Resultado]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK__Fomag_or__3214EC070BFAB258] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de sincronización de órdenes enviadas a FOMAG (Fondo de Prestaciones Sociales del Magisterio). Guarda el historial de cada envío, su estado de procesamiento y la respuesta obtenida.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de sincronización.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o referencia de la orden enviada a FOMAG; permite rastrear el documento o transacción sincronizada.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync', @level2type = N'COLUMN', @level2name = N'Identificador';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync', @level2type = N'COLUMN', @level2name = N'Identificador';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que la orden fue enviada al servicio de FOMAG.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync', @level2type = N'COLUMN', @level2name = N'FechaEnvio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync', @level2type = N'COLUMN', @level2name = N'FechaEnvio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del envío o procesamiento de la orden (por ejemplo: pendiente, enviado, error); valor numérico que representa el resultado del ciclo de sincronización.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta o mensaje devuelto por FOMAG tras el envío; puede contener confirmaciones, errores o detalles del procesamiento.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync', @level2type = N'COLUMN', @level2name = N'Resultado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_orderSync', @level2type = N'COLUMN', @level2name = N'Resultado';
