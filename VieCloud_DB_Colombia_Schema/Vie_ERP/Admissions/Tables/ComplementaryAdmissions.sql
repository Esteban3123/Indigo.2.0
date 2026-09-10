CREATE TABLE [Admissions].[ComplementaryAdmissions] (
    [id]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [code]   VARCHAR (3)   NOT NULL,
    [names]  VARCHAR (100) NOT NULL,
    [estado] BIT           NOT NULL,
    CONSTRAINT [PK__Compleme__3213E83F33681CE3] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [UK_codigo] UNIQUE NONCLUSTERED ([code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos o categorías de admisiones complementarias utilizadas en el proceso de ingreso de pacientes, como clasificaciones adicionales al tipo de admisión principal.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de admisión complementaria.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissions', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissions', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código corto que identifica el tipo de admisión complementaria.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissions', @level2type = N'COLUMN', @level2name = N'code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissions', @level2type = N'COLUMN', @level2name = N'code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del tipo de admisión complementaria.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissions', @level2type = N'COLUMN', @level2name = N'names';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissions', @level2type = N'COLUMN', @level2name = N'names';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el tipo de admisión complementaria está activo (1) o inactivo (0).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissions', @level2type = N'COLUMN', @level2name = N'estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissions', @level2type = N'COLUMN', @level2name = N'estado';
