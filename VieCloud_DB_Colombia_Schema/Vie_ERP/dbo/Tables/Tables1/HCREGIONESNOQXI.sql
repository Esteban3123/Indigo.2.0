CREATE TABLE [dbo].[HCREGIONESNOQXI] (
    [ID]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOHCORPNOQ]   INT           NOT NULL,
    [NOMREGTRATA]    VARCHAR (100) NOT NULL,
    [DOSTOTAL]       INT           NOT NULL,
    [UNMEDDOSISTO]   INT           NOT NULL,
    [DOSTOTALAPROB]  INT           NOT NULL,
    [DOSFRACC]       INT           NOT NULL,
    [UNMEDDOSFRAC]   INT           NOT NULL,
    [NUMEROFRACCION] INT           NOT NULL,
    [CODDIAGNO]      CHAR (4)      NULL,
    CONSTRAINT [PK_HCREGIONESNOQXI] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico de la orden (CIE-10), clasificación de enfermedad o condición clínica tratada. CHAR(4), nullable, para búsquedas por patología, diagnóstico o condición médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo diagnostico Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de la fracción o sesión de tratamiento dentro del plan dosificado. INT, identifica cada administración fraccionada de la dosis total.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'NUMEROFRACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Fracción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'NUMEROFRACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'NUMEROFRACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la dosis por fracción (mg, ml, UI, Gy, etc.). INT, permite cálculos y validaciones de dosificación fraccionada en procedimientos, radioterapia o medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'UNMEDDOSFRAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medidad de Dosis Fracción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'UNMEDDOSFRAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'UNMEDDOSFRAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis administrada por fracción o sesión de tratamiento. INT, cantidad unitaria en cada administración dentro del esquema fraccionado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'DOSFRACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis fraccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'DOSFRACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'DOSFRACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis total aprobada por el médico o protocolo clínico. INT, límite máximo de dosis para todo el tratamiento, validación y cumplimiento normativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'DOSTOTALAPROB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis total aprobado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'DOSTOTALAPROB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'DOSTOTALAPROB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la dosis total (mg, ml, UI, Gy). INT, normalización de magnitudes para cálculos, reportes y estándares de radioterapia o quimioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'UNMEDDOSISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida de la Dosis Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'UNMEDDOSISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'UNMEDDOSISTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis total planificada o acumulada en todo el tratamiento. INT, suma de fracciones, referencia para auditoría, glosa y cumplimiento de protocolos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la región anatómica o sitio de tratamiento (tumor, órgano, zona). VARCHAR(100), búsqueda por localización clínica, unidad funcional o centro de procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'NOMREGTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Region de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'NOMREGTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'NOMREGTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación o referencia de folio a tabla padre de historia clínica (HCORPNOQ). INT, clave foránea para trazabilidad de orden, ingreso, atención y paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'AUTOHCORPNOQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'AUTOHCORPNOQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'AUTOHCORPNOQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de registro en tabla. INT IDENTITY, clave primaria para auditoría, integridad referencial y búsquedas de tratamientos oncológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Regiones de tratamiento en radioterapia no quirúrgica (NOQXI): registra las áreas o campos irradiados dentro de un plan de radioterapia, incluyendo dosis total, dosis fraccionada, número de fracciones y diagnóstico asociado. Se utiliza para documentar y controlar la planificación dosimétrica por región anatómica en oncología radioterápica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQXI';
