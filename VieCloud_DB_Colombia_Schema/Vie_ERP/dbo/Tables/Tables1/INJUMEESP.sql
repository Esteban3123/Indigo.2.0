CREATE TABLE [dbo].[INJUMEESP] (
    [CODJUMEES] CHAR (3)      NOT NULL,
    [DESJUMEES] VARCHAR (100) NOT NULL,
    [ESTJUMEES] BIT           NOT NULL,
    CONSTRAINT [PK_INJUMEESP] PRIMARY KEY CLUSTERED ([CODJUMEES] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (activo/inactivo) de la justificación de medicamentos especiales. Booleano que indica si la justificación está vigente o habilitada en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'ESTJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la justificacion de medicamentos especiales ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'ESTJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'ESTJUMEES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la justificación de medicamentos especiales; detalle del motivo o criterio clínico para autorizar fármacos de alto costo, restringidos o fuera de formulario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'DESJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'DESJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'DESJUMEES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (3 caracteres) que identifica unívocamente cada tipo de justificación de medicamentos especiales. Clave primaria de la tabla de catálogo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP', @level2type = N'COLUMN', @level2name = N'CODJUMEES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de especialidades médicas o jurídicas (junta médica de especialidades), que registra cada especialidad con su código, descripción y estado activo/inactivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INJUMEESP';
