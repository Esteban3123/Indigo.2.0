CREATE TABLE [Nursing].[IndicationsTherapeuticRestraints] (
    [idTherapeuticRestraints] INT      NOT NULL,
    [CodeIndication]          CHAR (4) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (CHAR 4) de la indicación terapéutica que justifica la restricción (limitación de movimiento, contención, aislamiento); clave para búsqueda de protocolos de restricción en pacientes.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'IndicationsTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'CodeIndication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la indicación de las restricciones terapeuticas', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'IndicationsTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'CodeIndication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'IndicationsTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'CodeIndication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT, clave foránea) que referencia el registro de restricción terapéutica en la tabla TherapeuticRestraints; vincula la indicación clínica con los detalles de contención o limitación ordenada.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'IndicationsTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'idTherapeuticRestraints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el id de la tabla TherapeuticRestraints', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'IndicationsTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'idTherapeuticRestraints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'IndicationsTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'idTherapeuticRestraints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las indicaciones clínicas con los registros de restricciones terapéuticas (sujeciones) aplicadas a pacientes en enfermería. Permite identificar qué indicaciones médicas justifican el uso de cada restricción terapéutica.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'IndicationsTherapeuticRestraints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'IndicationsTherapeuticRestraints';
