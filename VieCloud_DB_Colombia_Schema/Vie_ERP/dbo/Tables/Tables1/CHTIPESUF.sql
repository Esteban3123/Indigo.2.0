CREATE TABLE [dbo].[CHTIPESUF] (
    [CODTIPEST] CHAR (3)     NOT NULL,
    [UFUTIPUNI] INT          NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHTIPESUF] PRIMARY KEY CLUSTERED ([CODTIPEST] ASC, [UFUTIPUNI] ASC),
    CONSTRAINT [FK_CHTIPESUF_CHTIPESTA] FOREIGN KEY ([CODTIPEST]) REFERENCES [dbo].[CHTIPESTA] ([CODTIPEST]) ON DELETE CASCADE
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador único (NUMERIC 18) para rastreo y control de auditoría de cambios en registros de tipos de estancia por unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESUF', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESUF', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESUF', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad funcional (INT): clasificación de centros de atención (urgencias, hospitalización, UCI, cuidado intensivo, apoyo diagnóstico, apoyo terapéutico, consulta externa, quirófano, laboratorio, unidades especializadas como oncología, cardiología, ginecología, psiquiatría, quemados, cuidado paliativo, nefrología, medicina nuclear)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESUF', @level2type = N'COLUMN', @level2name = N'UFUTIPUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Otras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESUF', @level2type = N'COLUMN', @level2name = N'UFUTIPUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESUF', @level2type = N'COLUMN', @level2name = N'UFUTIPUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de estancia (CHAR 3, PK, FK a CHTIPESTA): clasificación de permanencia del paciente en el centro de atención, referencia a tabla maestra de tipos de estadía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESUF', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESUF', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESUF', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de estructura de unidades funcionales (UFU): relaciona cada tipo de estructura con su tipo de unidad funcional y un indicador de auditoría. Se usa para clasificar y validar la configuración de unidades funcionales dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESUF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESUF';
