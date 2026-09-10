CREATE TABLE [dbo].[HCNUTPAREND] (
    [ID]           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCNUTPAREC] INT             NOT NULL,
    [IDHCPARNUTC]  INT             NOT NULL,
    [IDHCPARNUTD]  INT             NOT NULL,
    [NAMENUT]      CHAR (300)      NULL,
    [CODPRODUC]    CHAR (20)       NOT NULL,
    [CODUNIMED]    VARCHAR (20)    NOT NULL,
    [APORTENUT]    NUMERIC (18, 3) NULL,
    [VOLUMENCAL]   NUMERIC (18, 3) NOT NULL,
    [FORVOLUADM]   VARCHAR (2000)  NOT NULL,
    [RVOLUADM]     VARCHAR (1000)  NOT NULL,
    [CORRPURGACAL] NUMERIC (18, 3) NOT NULL,
    [FORCORRPURGA] VARCHAR (2000)  NOT NULL,
    [RCORRPURGA]   VARCHAR (1000)  NULL,
    [CANTMED]      INT             NULL,
    CONSTRAINT [PK__HCNUTPAR__3214EC27204B518A] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNUTPAREC_HCNUTPAREND] FOREIGN KEY ([IDHCNUTPAREC]) REFERENCES [dbo].[HCNUTPAREC] ([ID]),
    CONSTRAINT [FK_HCPARNUTC_HCNUTPAREND] FOREIGN KEY ([IDHCPARNUTC]) REFERENCES [dbo].[HCPARNUTC] ([ID]),
    CONSTRAINT [FK_HCPARNUTD_HCNUTPAREND] FOREIGN KEY ([IDHCPARNUTD]) REFERENCES [dbo].[HCPARNUTD] ([ID]),
    CONSTRAINT [FK_IHLISTPRO_HCNUTPAREND] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de medicamento/nutriente solicitada en la prescripción parenteral. INT. Unidades de presentación del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CANTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de medicamento solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CANTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CANTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula de corrección de purga (versión reemplazada/histórica). VARCHAR(1000). Cálculo descontinuado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'RCORRPURGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula de la correccion de purga (Reemplazada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'RCORRPURGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'RCORRPURGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula de corrección de purga aplicada al nutriente parenteral. VARCHAR(2000). Ajuste de dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'FORCORRPURGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula de la correcion de purga', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'FORCORRPURGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'FORCORRPURGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico calculado de la corrección de purga. NUMERIC(18,3). Resultado del ajuste de dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CORRPURGACAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la correccion de purga', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CORRPURGACAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CORRPURGACAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula de volumen a administrar (versión reemplazada, sustituida por valores numéricos). VARCHAR(1000). Cálculo histórico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'RVOLUADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula de volumen administrar (reemplazada con los valores numericos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'RVOLUADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'RVOLUADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula del volumen a administrar del nutriente parenteral. VARCHAR(2000). Expresión de cálculo de infusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'FORVOLUADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula del volumen administrar del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'FORVOLUADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'FORVOLUADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total a administrar al paciente. NUMERIC(18,3). ML o unidad de medida especificada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'VOLUMENCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen administrar al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'VOLUMENCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'VOLUMENCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aporte/contribución nutricional suministrada del nutriente (calorías, proteína, grasa, etc.). NUMERIC(18,3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'APORTENUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aporte subministrado del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'APORTENUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'APORTENUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad de medida del medicamento/nutriente. VARCHAR(20). ML, gr, UI, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la unidad del medicamento asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento/nutriente parenteral asociado. CHAR(20). FK→IHLISTPRO (catálogo de productos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicamento asociado al nutriente parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del nutriente parenteral prescrito. CHAR(300). Ej: Lípidos, aminoácidos, glucosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'NAMENUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'NAMENUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'NAMENUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de parametrización detallada de nutrientes parenterales. INT. FK→HCPARNUTD (configuración específica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la pametrizacion de los nutrientes parenterales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de parametrización de nutrición parenteral. INT. FK→HCPARNUTC (configuración general)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la pametrizacion de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la cabecera/encabezado de la prescripción de nutrición parenteral. INT. FK→HCNUTPAREC (orden principal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCNUTPAREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera de la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCNUTPAREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCNUTPAREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de nutrición parenteral. INT IDENTITY. Clave primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de nutrientes y productos incluidos en una pauta o prescripción de nutrición parenteral. Registra cada componente nutricional (aminoácidos, lípidos, glucosa, electrolitos, vitaminas, oligoelementos, etc.) con sus volúmenes, aportes y fórmulas de cálculo asociadas a la orden de nutrición del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND';
