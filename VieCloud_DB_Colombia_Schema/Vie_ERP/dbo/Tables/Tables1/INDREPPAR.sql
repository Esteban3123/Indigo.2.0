CREATE TABLE [dbo].[INDREPPAR] (
    [CODREPORT] NCHAR (3)   NOT NULL,
    [NOMREPORT] NCHAR (100) NOT NULL,
    [MODREPORT] NCHAR (13)  NOT NULL,
    [ID]        INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_INDREPPAR] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (INT IDENTITY) de cada registro de parámetro de reporte en el sistema Indigo Vie Cloud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación consecutiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Módulo origen del reporte (NCHAR 13): 1=Historias Clínicas, 2=Ley 100 (aseguramiento/afiliación), 3=Órdenes Médicas IntraHospitalarias, 4=Órdenes Médicas Extramurales, 5=Otros. Define contexto clínico o administrativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'MODREPORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modulo:     1= ''''Historias Clinicas''''   2= ''''Ley 100''''   3= ''''Ordenes Medicas IntraHospitalarias''''  4= ''''Ordenes Medicas Extramurales''''   5=''''Otros''''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'MODREPORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'MODREPORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del reporte (NCHAR 100, tipo texto). Identifica el tipo de salida de información generada en el módulo correspondiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'NOMREPORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'NOMREPORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'NOMREPORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del reporte (NCHAR 3). Código único alphanumerico que referencia cada tipo de reporte en el sistema, clave de búsqueda y clasificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'CODREPORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'CODREPORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR', @level2type = N'COLUMN', @level2name = N'CODREPORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de reportes del sistema. Registra cada reporte disponible con su código, nombre y módulo al que pertenece, permitiendo identificar y clasificar los informes generados en la plataforma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDREPPAR';
