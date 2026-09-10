CREATE TABLE [dbo].[HCPCEVALDIAG] (
    [IDVALDIAG]      INT         IDENTITY (1, 1) NOT NULL,
    [IDPLANVAL]      INT         NOT NULL,
    [IDHCCLASENF]    INT         NULL,
    [CODDIAGENF]     VARCHAR (5) NOT NULL,
    [CODESCMEDPCE]   INT         NULL,
    [IDHCPCECONTROL] INT         NOT NULL,
    CONSTRAINT [PK_HCPCEVALDIAG] PRIMARY KEY CLUSTERED ([IDVALDIAG] ASC),
    CONSTRAINT [FK_HCPCEVALDIAG_HCDIAGENF] FOREIGN KEY ([CODDIAGENF]) REFERENCES [dbo].[HCDIAGENF] ([CODDIAGENF]),
    CONSTRAINT [FK_HCPCEVALDIAG_HCPCEPLANVAL] FOREIGN KEY ([IDPLANVAL]) REFERENCES [dbo].[HCPCEPLANVAL] ([IDPLANVAL])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del control/seguimiento del plan de cuidado de enfermería. Referencia a registro de atención/monitoreo del paciente en unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el id del control del plan de cuidado enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de escala o medida del plan de cuidado enfermería. Registro numérico de valoración/evaluación clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'CODESCMEDPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Codigo del registo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'CODESCMEDPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'CODESCMEDPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico de enfermería (VARCHAR 5). Clasificación estandarizada del problema/necesidad de cuidado identificado en el paciente. FK a HCDIAGENF.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Codigo diagnostico enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de clase/clasificación de enfermería. Agrupa diagnósticos de enfermería por categoría clínica de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda los id de clases de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del plan de valoración/evaluación de enfermería. FK a HCPCEPLANVAL; vincula diagnóstico a plan de cuidado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, IDENTITY) del valor/resultado del diagnóstico de enfermería evaluado en la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id valor diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de evaluaciones diagnósticas de enfermería asociadas a un plan de valoración del paciente en historia clínica. Relaciona cada diagnóstico de enfermería con su control o seguimiento clínico correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG';
