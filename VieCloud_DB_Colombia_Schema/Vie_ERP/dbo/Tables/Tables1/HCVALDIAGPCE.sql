CREATE TABLE [dbo].[HCVALDIAGPCE] (
    [IDVALDIAG]    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODDIAGENF]   VARCHAR (5) NOT NULL,
    [CODPLANCENF]  VARCHAR (3) NULL,
    [CODVALORAPCE] INT         NULL,
    [IDHCCLASENF]  INT         NULL,
    CONSTRAINT [PK_HCVALDIAGPCE] PRIMARY KEY CLUSTERED ([IDVALDIAG] ASC),
    CONSTRAINT [FK_HCVALDIAGPCE_HCDIAGENF] FOREIGN KEY ([CODDIAGENF]) REFERENCES [dbo].[HCDIAGENF] ([CODDIAGENF])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCVALDIAGPCE]
    ON [dbo].[HCVALDIAGPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC, [CODDIAGENF] ASC, [IDHCCLASENF] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la clase de enfermería asociada al diagnóstico; vincula la valoración con la clasificación clínica de cuidados de enfermería (taxonomía NANDA/NIC/NOC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda los id de clases de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (INT) de la valoración del plan de cuidados de enfermería; referencia la evaluación clínica y valoración de intervenciones de enfermería registradas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Codigo valoracion plac cuidado enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 3) del plan de enfermería; identifica el plan de cuidados y atención de enfermería asociado al diagnóstico del paciente en la atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Codigo Plan Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 5) del diagnóstico de enfermería (FK → HCDIAGENF); clave de referencia que vincula el diagnóstico NANDA o de dominio de enfermería en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Codigo diagnostico enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) de la validación del diagnóstico de enfermería; clave primaria que registra cada asociación de diagnóstico-valoración-plan en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la identidad diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de valoraciones diagnósticas por enfermería en la historia clínica. Relaciona cada diagnóstico de enfermería con su plan de cuidados y la valoración clínica correspondiente al proceso de atención de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE';
