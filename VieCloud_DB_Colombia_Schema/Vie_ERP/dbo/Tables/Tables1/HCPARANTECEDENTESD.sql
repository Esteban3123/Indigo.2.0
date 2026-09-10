CREATE TABLE [dbo].[HCPARANTECEDENTESD] (
    [ID]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPARANTECEDENTESC] INT           NOT NULL,
    [CODIGOCAMPO]          INT           NOT NULL,
    [NOMBREANTECEDENTE]    VARCHAR (MAX) NULL,
    [VISIBLE]              BIT           NOT NULL,
    [OBLIGATORIO]          BIT           NOT NULL,
    [TIPO]                 INT           NULL,
    CONSTRAINT [PK_HCPARANTECEDENTESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARANTECEDENTESD_HCPARANTECEDENTESD] FOREIGN KEY ([IDHCPARANTECEDENTESC]) REFERENCES [dbo].[HCPARANTECEDENTESC] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de tipo de antecedente: 1=Ginecológicos, 2=Obstétricos. INT. Categoriza antecedentes médicos por especialidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Ginecoloicos 2-OBSTETRICOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (1=Sí, 0=No) que marca si el campo de antecedente es obligatorio en el registro de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Obligatorio si ó No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (1=Visible, 0=Oculto) que controla si el campo antecedente se muestra en formularios e interfaces de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'VISIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo VIsible si ó no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'VISIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'VISIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del nombre o etiqueta del antecedente médico (VARCHAR MAX). Ej: ''''Diabetes'''', ''''Hipertensión'''', ''''Embarazos previos''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'NOMBREANTECEDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del antecendente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'NOMBREANTECEDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'NOMBREANTECEDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único INT que identifica el campo de antecedente en el sistema, utilizado para referencias y mapeos internos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea INT que vincula el detalle con la tabla cabecera HCPARANTECEDENTESC. Relación 1-a-muchos: define el grupo/sección padre del antecedente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'IDHCPARANTECEDENTESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'IDHCPARANTECEDENTESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'IDHCPARANTECEDENTESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico INT (IDENTITY 1,1) de la fila de detalle de antecedente. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los campos o ítems que componen una plantilla de antecedentes clínicos del paciente en la historia clínica. Cada fila representa un campo específico (como antecedente familiar, quirúrgico, alérgico, etc.) asociado a una cabecera de antecedentes, indicando su nombre, tipo de dato, visibilidad y obligatoriedad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD';
