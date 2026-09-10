CREATE TABLE [dbo].[INTERLABC_HISTORICA] (
    [AUTO] INT NOT NULL,
    [ORDEN_INDIGO] VARCHAR (20) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro histórico de órdenes de laboratorio enviadas a un sistema externo (interfaz de laboratorio). Permite rastrear qué órdenes de Indigo fueron transmitidas e integradas con el laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro histórico de la interfaz de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código de la orden de laboratorio generada en Indigo, que identifica el pedido de exámenes enviado al sistema externo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC_HISTORICA', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC_HISTORICA', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
