CREATE TABLE [dbo].[SOLORDCONS] (
    [ORDCONSEC] INT NOT NULL,
    CONSTRAINT [PK_SOLORDCONS] PRIMARY KEY CLUSTERED ([ORDCONSEC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único de la orden de compra. Identificador secuencial (INT) que ordena cronológicamente cada solicitud de compra en el sistema. Equivalente a número de orden, folio de compra o referencia de adquisición. Clave primaria de la tabla SOLORDCONS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCONS', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el consecutivo de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCONS', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCONS', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de solicitudes u órdenes de consulta generadas en el sistema. Cada fila representa una orden o solicitud individual identificada por un consecutivo único, utilizada en el proceso de agendamiento y atención de consultas médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCONS';
