CREATE TABLE [dbo].[HCQXVIABO] (
    [CODVIAABO] CHAR (2)      NOT NULL,
    [DESVIAABO] VARCHAR (100) NULL,
    [ESTADOREG] BIT           NOT NULL,
    [GENCODVIA] TINYINT       NULL,
    CONSTRAINT [PK_HCQXVIABO] PRIMARY KEY CLUSTERED ([CODVIAABO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de tipo de intervención quirúrgica para control de cuentas: 1-Básico, 2-Bilateral SOAT, 3-Bilateral Múltiple SOAT, 4-MIVIE ISS, 5-MDVIE ISS, 6-MIVDE ISS, 7-MDVDE ISS, 8-Politrauma Igual Vía ISS, 9-Politrauma Diferente Vía ISS, 10-No Cruento. Integración nativa para facturación y glosa de procedimientos quirúrgicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'GENCODVIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de intervencion quirurgica  1 - Basico  2 - Bilateral -- SOAT  3 - Bilateral Multiple -- SOAT  4 - MIVIE (Multiple Igual Via Igual Especialista) -- ISS  5 - MDVIE (Multiple Diferente Via Igual Especialista) -- ISS  6 - MIVDE (Multiple Igual Via Diferente Especialista) -- ISS  7 - MDVDE (Multiple Diferente Via Diferente Especialista) -- ISS  8 - PolitraumaIV (Politrauma Igual Via) -- ISS  9 - PolitraumaDV (Politrauma Diferente Via) -- ISS  10- No Cruento.    Campo que se registra para integracion nativa de control de cuentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'GENCODVIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'GENCODVIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: True=Activo, False=Inactivo. Controla disponibilidad de la vía de abordaje en operaciones quirúrgicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'ESTADOREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro  True = Activo  False = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'ESTADOREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'ESTADOREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de abordaje quirúrgico; descripción de la técnica o acceso anatómico utilizado en procedimientos e intervenciones quirúrgicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'DESVIAABO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Via de Abordaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'DESVIAABO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'DESVIAABO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno único (2 caracteres) identificador de la vía de abordaje en la tabla maestra de procedimientos quirúrgicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'CODVIAABO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'CODVIAABO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'CODVIAABO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de vías de administración de medicamentos o procedimientos quirúrgicos (por ejemplo: oral, intravenosa, intramuscular). Permite clasificar la forma en que se aplica un tratamiento al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO';
