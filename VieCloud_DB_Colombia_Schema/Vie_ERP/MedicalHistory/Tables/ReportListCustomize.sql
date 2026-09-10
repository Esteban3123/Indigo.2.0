CREATE TABLE [MedicalHistory].[ReportListCustomize] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [ReportClass]       VARCHAR (150) NOT NULL,
    [ReportName]        VARCHAR (150) NOT NULL,
    [ReportDescription] VARCHAR (800) NOT NULL,
    [ModuleName]        VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_ReportListCustomize] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del módulo funcional Indigo Vie Cloud (VARCHAR 150) al que pertenece el reporte: Atención, Facturación, Urgencia, Laboratorio, Imagen, RIPS, Farmacia, Contratación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ModuleName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Nombre del Modulo ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ModuleName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ModuleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del informe o reporte (VARCHAR 800). Incluye propósito, contenido, usuarios destino y módulos asociados (ERP/EHR). Mejora búsqueda semántica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Informe Descripción', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del informe o reporte (VARCHAR 150). Etiqueta legible para búsqueda de reportes: historia clínica, egreso, receta, glosa, factura, diagnóstico.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Nombre del informe o reporte', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación o categoría del informe/reporte (VARCHAR 150). Agrupa reportes por tipo: clínico, administrativo, facturación, RIPS, laboratorio, imagen, procedimiento.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Clase de informe', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY), consecutivo secuencial de la tabla ReportListCustomize. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de reportes personalizados disponibles en el módulo de historia clínica, donde se registran los informes configurados por clase, nombre y descripción, agrupados por módulo del sistema.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize';
