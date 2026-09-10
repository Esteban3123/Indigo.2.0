CREATE TABLE [dbo].[HCVALOBJPCE] (
    [IDVALOBJ]     INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODOBJPCE]    INT         NOT NULL,
    [CODPLANCENF]  VARCHAR (3) NULL,
    [CODVALORAPCE] INT         NULL,
    [IDHCCLASENF]  INT         NULL,
    [CODDIAGENF]   VARCHAR (5) NULL,
    CONSTRAINT [FK_HCVALOBJPCE] PRIMARY KEY CLUSTERED ([IDVALOBJ] ASC),
    CONSTRAINT [FK_HCVALOBJPCE_HCOBJPCE] FOREIGN KEY ([CODOBJPCE]) REFERENCES [dbo].[HCOBJPCE] ([CODOBJPCE])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCVALOBJPCE]
    ON [dbo].[HCVALOBJPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC, [IDHCCLASENF] ASC, [CODDIAGENF] ASC, [CODOBJPCE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico de enfermería (VARCHAR 5). Identificador del diagnóstico clínico registrado por el profesional de enfermería en la valoración del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  codigo de diagnostico enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de clasificación de enfermería (INT). Referencia a la clase o categoría de enfermería asignada en la historia clínica de la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id clases enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de valoración del plan de cuidado de enfermería (INT). Identificador que vincula la evaluación o medición realizada en el plan de cuidados de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de valoracion plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de cuidado de enfermería (VARCHAR 3). Identificador del plan terapéutico de enfermería diseñado para el paciente en su ingreso o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda codigo de plan de cuidad de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del objetivo del plan de cuidado de enfermería (INT). Identificador de los objetivos o metas de cuidado establecidos en el plan de enfermería. FK a [dbo].[HCOBJPCE].', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo objtos de planes  de cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de valor de objetivo (INT IDENTITY). Clave primaria que registra cada valoración o evaluación de los objetivos de planes de cuidado de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'IDVALOBJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id valoresobjetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'IDVALOBJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'IDVALOBJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valoraciones de objetivos en planes de cuidado de enfermería: registra las evaluaciones realizadas sobre cada objetivo definido en el plan de cuidados del paciente, vinculando el objetivo con su valoración, el diagnóstico de enfermería y la clase o categoría de enfermería correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE';
