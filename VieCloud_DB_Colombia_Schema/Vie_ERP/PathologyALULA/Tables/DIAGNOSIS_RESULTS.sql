CREATE TABLE [PathologyALULA].[DIAGNOSIS_RESULTS] (
    [id]                    INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [order_id]              VARCHAR (16)   NOT NULL,
    [report_base64]         VARCHAR (MAX)  NOT NULL,
    [diagnosis_description] VARCHAR (5000) NULL,
    [diagnosis_date]        DATETIME       NULL,
    [diagnosis_type]        INT            NULL,
    [diagnosing_clinician]  VARCHAR (150)  NULL,
    [parent_diagnosis]      INT            NULL,
    CONSTRAINT [PK_DIAGNOSIS_RESULTS] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del diagnóstico padre o principal; usado para agrupar diagnósticos relacionados o adendums a un diagnóstico primario', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'parent_diagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnóstico de los padres', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'parent_diagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'parent_diagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del médico, patólogo o profesional de la salud que realiza o firma el diagnóstico; campo de texto (VARCHAR 150)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosing_clinician';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Médico que realiza el diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosing_clinician';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosing_clinician';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de diagnóstico: 1=Reporte inicial, 2=Adendum o complemento; clasificación de naturaleza del resultado (INT)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 reporte  2 adendum', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del diagnóstico o emisión del reporte; tipo DATETIME para búsqueda temporal', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual completa del diagnóstico, hallazgos clínicos, impresión diagnóstica; hasta 5000 caracteres (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento del reporte codificado en base64; contiene archivo binario (PDF, imagen) del informe patológico para descarga/visualización', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'report_base64';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reporte en formato base 64', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'report_base64';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'report_base64';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la orden o solicitud de patología; referencia a la orden de laboratorio/examen (VARCHAR 16)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'order_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del pedido', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'order_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'order_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo (IDENTITY INT); clave primaria del registro de resultado diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultados de diagnóstico patológico por orden: guarda el informe en formato base64, la descripción clínica del diagnóstico, la fecha, el tipo de diagnóstico, el médico que lo emitió y la relación con un diagnóstico previo o padre. Usado en patología/anatomía patológica para registrar y consultar resultados de estudios diagnósticos.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS';
