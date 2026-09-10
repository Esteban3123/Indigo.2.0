CREATE TABLE [dbo].[HCDIAGINTPCE] (
    [IDDIAGINT]    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODINTPCE]    INT         NOT NULL,
    [CODPLANCENF]  VARCHAR (3) NOT NULL,
    [CODVALORAPCE] INT         NOT NULL,
    [CODDIAGENF]   VARCHAR (5) NOT NULL,
    [IDHCCLASENF]  INT         NULL,
    CONSTRAINT [PK_HCDIAGINTPCE] PRIMARY KEY CLUSTERED ([IDDIAGINT] ASC),
    CONSTRAINT [FK_HCDIAGINTPCE_HCINTERVENPCE] FOREIGN KEY ([CODINTPCE]) REFERENCES [dbo].[HCINTERVENPCE] ([CODINTPCE])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCDIAGINTPCE]
    ON [dbo].[HCDIAGINTPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC, [IDHCCLASENF] ASC, [CODDIAGENF] ASC, [CODINTPCE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK nullable) de la clase de enfermería asociada al diagnóstico de intervención. Clasifica el tipo o categoría de cuidado de enfermería registrado en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda los id de clases de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico de enfermería (VARCHAR 5). Identifica el diagnóstico clínico de enfermería según estándares NANDA-I o nomenclatura institucional aplicada al plan de cuidado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Código diagnostico enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de valoración del plan de cuidado de enfermería (INT, FK). Referencia la evaluación y medición de indicadores del paciente en el proceso de atención de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código de valoración plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de cuidado de enfermería (VARCHAR 3). Identifica el plan específico de intervenciones y cuidados de enfermería diseñado para el paciente en esa atención o internación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda codigo de plan de cuidad de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la intervención del plan de cuidado de enfermería (INT, FK). Referencia la acción o intervención específica de enfermería ejecutada como parte del plan de atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la intervención plan cuidado enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK Identity) del registro de diagnóstico-intervención de enfermería. Llave primaria que vincula diagnósticos de enfermería con sus intervenciones en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el diagnostico de intervención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos de intervención de enfermería asociados a planes de cuidado del paciente. Relaciona cada intervención de enfermería con su diagnóstico específico según la clasificación de enfermería (NANDA u otra), dentro del plan de cuidados clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE';
