CREATE TABLE [dbo].[HCDIAGCAUPCE] (
    [IDDIAGCAU]    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCAUSAPCE]  INT         NOT NULL,
    [CODPLANCENF]  VARCHAR (3) NULL,
    [CODVALORAPCE] INT         NULL,
    [CODDIAGENF]   VARCHAR (5) NULL,
    [IDHCCLASENF]  INT         NULL,
    CONSTRAINT [FK_HCDIAGCAUPCE] PRIMARY KEY CLUSTERED ([IDDIAGCAU] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCDIAGCAUPCE]
    ON [dbo].[HCDIAGCAUPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC, [IDHCCLASENF] ASC, [CODDIAGENF] ASC, [CODCAUSAPCE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de clase de enfermería (INT). Referencia a clasificación de diagnósticos de enfermería según taxonomía de cuidados. Clave foránea para tipificar intervenciones enfermeras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda los id de clases de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico de enfermería (VARCHAR 5). Código estandarizado de valoración y diagnóstico enfermero en plan de cuidados. Referencia a taxonomía NANDA o similar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Código valoración  diagnostico enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de valoración del plan de cuidados de enfermería (INT). Identificador del proceso de evaluación inicial y seguimiento de paciente en atención de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código de valoración plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de cuidados de enfermería (VARCHAR 3). Identificador del plan de atención enfermero personalizado para el paciente, vinculado a diagnósticos y valoraciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de causa del plan de cuidados de enfermería (INT). Identifica la causa clínica o diagnóstica que origina o justifica la intervención enfermera en cuidados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Codigo de la causa plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del diagnóstico de causa (INT, PK, IDENTITY). Clave primaria que registra unívocamente cada diagnóstico causal en la historia clínica de enfermería del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'IDDIAGCAU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Id del daignostico de la causa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'IDDIAGCAU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'IDDIAGCAU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos y causas asociadas a problemas o enfermedades de enfermería en la historia clínica. Relaciona cada causa de problema de enfermería con su plan de cuidados, valor diagnóstico y clasificación dentro del proceso de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE';
