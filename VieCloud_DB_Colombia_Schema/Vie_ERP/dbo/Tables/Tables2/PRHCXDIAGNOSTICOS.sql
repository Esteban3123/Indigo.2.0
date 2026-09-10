CREATE TABLE [dbo].[PRHCXDIAGNOSTICOS] (
    [ID]            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]    INT            NOT NULL,
    [CODDIAGNO]     CHAR (4)       NOT NULL,
    [PRINCIPAL]     BIT            NOT NULL,
    [TIPO]          VARCHAR (5)    NOT NULL,
    [CLASE]         VARCHAR (5)    NOT NULL,
    [ESTADIO1]      VARCHAR (5)    NULL,
    [ESTADIO2]      VARCHAR (5)    NULL,
    [OBSERVACIONES] VARCHAR (1000) NULL,
    CONSTRAINT [PK_PRHCXDIAGNOSTICOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXDIAGNOSTICOS_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[PRHCXDIAGNOSTICOS] NOCHECK CONSTRAINT [FK_PRHCXDIAGNOSTICOS_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas del diagnóstico. Texto libre (hasta 1000 caracteres) para documentar hallazgos, comentarios adicionales o aclaraciones sobre el diagnóstico registrado en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'OBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda las observaciones del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'OBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'OBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadio alfabético del diagnóstico (0, A, B, C). Clasificación de severidad o progresión en sistemas oncológicos o patologías con estadificación por letras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ESTADIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el estadio de diagnóstico (0 -> 0,  A -> A, B -> B, C -> C)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ESTADIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ESTADIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadio numérico del diagnóstico (0, I, II, III, IV). Clasificación de gravedad o progresión tumoral/patológica en escala romana.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ESTADIO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el estadio de diagnóstico (0 -> 0, 1 -> I, 2 -> II, 3 -> III, 4 -> IV)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ESTADIO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ESTADIO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase del diagnóstico: PR (Pre-Operatorio), PO (Post-Operatorio), PP (Pre y Post-Operatorio), HI (Histopatológico), NA (No Aplica). Indica el momento temporal en que se registra el diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CLASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la clase de diagnósticos (PR -> Pre-Operatorio, PO -> Pos-Operatorio, PP -> Pre y Pos-Operatorio, HI -> Histopatológico, NA -> No Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CLASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CLASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de diagnóstico: I (Impresión Diagnóstica), C (Confirmado Nuevo), R (Confirmado Repetido). Diferencia entre presunción clínica y diagnósticos confirmados por laboratorio o patología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el tipo del diagnóstico (I -> Impresión Diagnóstica, C -> Confirmado Nuevo, R -> Confirmado Repetido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (1/0) que marca si este es el diagnóstico principal de la atención/hospitalización. Solo un diagnóstico por modelo debe ser principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el diagnostico principal (Checked or unchecked)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 del diagnóstico (4 caracteres). Llave foránea a tabla INDIAGNOS que contiene el catálogo de diagnósticos internacionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla INDIAGNOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la historia clínica/modelo de atención a la que pertenece este diagnóstico. Referencia al documento de atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el ID de PRMODELOHC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (INT, identidad) para cada registro de diagnóstico en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos registrados en las plantillas de historia clínica (modelos HC). Vincula cada modelo de historia clínica con uno o más códigos de diagnóstico CIE-10, indicando si es principal o secundario, su tipo, clase y estadio clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS';
