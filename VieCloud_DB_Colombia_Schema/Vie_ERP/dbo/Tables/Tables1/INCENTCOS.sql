CREATE TABLE [dbo].[INCENTCOS] (
    [CODCENCOS] CHAR (14)    NOT NULL,
    [DESCENCOS] CHAR (80)    NOT NULL,
    [ESTCENCOS] BIT          NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INCENTCOS] PRIMARY KEY CLUSTERED ([CODCENCOS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador o código de auditoría (NUMERIC 18). Referencia a proceso de auditoría, validación o conformidad asociado al centro de costos para trazabilidad de gestión financiera y control interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del centro de costos (BIT). Indicador booleano de actividad: 1=Activo/Vigente, 0=Inactivo/Deshabilitado. Controla si el centro acepta asignaciones de costos en procesos de facturación y auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'ESTCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del centro de Costo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'ESTCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'ESTCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del centro de costos (CHAR 80). Nombre o texto detallado que identifica la unidad funcional, departamento, área de atención o centro operativo para referencia administrativa y contable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'DESCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de Centros de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'DESCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'DESCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del centro de costos (CHAR 14). Identificador primario para clasificación y seguimiento de gastos por área operativa, unidad funcional o departamento en la institución de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'CODCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Centros de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'CODCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'CODCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de centros de costo de la organización. Registra cada centro de costo con su código, nombre descriptivo y estado activo/inactivo, usado para la distribución y control de gastos internos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS';
