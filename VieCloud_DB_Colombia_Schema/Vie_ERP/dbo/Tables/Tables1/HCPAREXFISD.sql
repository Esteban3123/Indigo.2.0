CREATE TABLE [dbo].[HCPAREXFISD] (
    [ID]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCABECERA]  INT          NOT NULL,
    [CODIGOCAMPO] VARCHAR (10) NOT NULL,
    [VISIBLE]     BIT          NOT NULL,
    [OBLIGATORIO] BIT          NOT NULL,
    [PRECONSULTA] BIT          CONSTRAINT [DF_HCPAREXFISD_PRECONSULTA] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_HCPAREXFISD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAREXFISD_HCPAREXFISC] FOREIGN KEY ([IDCABECERA]) REFERENCES [dbo].[HCPAREXFISC] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de uso en preconsulta (BIT: 1=Habilitado, 0=Deshabilitado). Visibilidad condicionada: solo aplica cuando la Unidad Funcional es tipo ''''Consulta Externa'''' Y el Tipo de Examen Físico es ''''Control Examen Físico Básico''''. Permite parametrizar campos específicos para valoración inicial previa a consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'PRECONSULTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me va indicar si funciona para una preconsulta en la consulta externa.    Esta columna en la parametrizacion del examen fisico solo se visualizará cuando se cumple dos condiciones relacionadas a continuación:    1. La Unidad funcional seleccionada sea de tipo “Consulta externa”.     2. El Tipo de examen físico seleccionado sea “Control examen físico básico”.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'PRECONSULTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'PRECONSULTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de obligatoriedad del campo (BIT: 1=Obligatorio, 0=Opcional). Determina si el profesional de salud está forzado a diligenciar este campo del examen físico durante la consulta o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si es obligatorio   1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de visibilidad del campo (BIT: 1=Visible, 0=Oculto). Controla si el campo de examen físico se muestra en la interfaz de consulta externa según parametrización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'VISIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si es visible  1 = Si      0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'VISIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'VISIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del campo de examen físico (VARCHAR 10). Identificador único que referencia el campo específico del formulario de examen físico (ej: tensión arterial, frecuencia cardíaca, temperatura, peso, talla, inspección general).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo campo examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera del examen físico (INT, FK → HCPAREXFISC.ID). Relación directa con la tabla maestra HCPAREXFISC que agrupa los parámetros de examen físico por unidad funcional y tipo de examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'IDCABECERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera; campo que me tiene la relacion con la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'IDCABECERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'IDCABECERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY). Consecutivo secuencial de la tabla de detalle de parámetros de examen físico. Clave primaria que indexa cada registro de configuración de campo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de campos en exámenes físicos de historia clínica: define qué campos del examen físico son visibles, obligatorios o de preconsulta, agrupados bajo una cabecera de configuración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD';
