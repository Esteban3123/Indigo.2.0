CREATE TABLE [MedicalHistory].[CustomReports] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [ReportName]   VARCHAR (250)   NOT NULL,
    [Report]       VARBINARY (MAX) NOT NULL,
    [UserCreation] CHAR (20)       NOT NULL,
    [DateCreation] DATETIME        NOT NULL,
    [UserModify]   CHAR (20)       NULL,
    [DateModify]   DATETIME        NULL,
    CONSTRAINT [PK_CustomReports] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CustomReports]
    ON [MedicalHistory].[CustomReports]([ReportName] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del informe personalizado; NULL si nunca fue editado tras su creación inicial', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'DateModify';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  la fecha de la modificación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'DateModify';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'DateModify';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (CHAR 20) del usuario que realizó la última modificación del informe; NULL si el informe no ha sido modificado', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'UserModify';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo el usuario quien modifico', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'UserModify';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'UserModify';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del informe personalizado; registro de auditoría de cuándo se generó inicialmente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (CHAR 20) del usuario que creó el informe personalizado; trazabilidad del autor original del documento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo el usuario quien lo creo', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido binario (VARBINARY MAX) del informe personalizado; almacena el archivo/documento compilado de diagnóstico, atención o procedimiento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'Report';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo el informe', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'Report';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'Report';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 250) del informe personalizado; título del documento de historia clínica, factura, RIPS, glosa o reporte clínico', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'ReportName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el nombre del informe', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'ReportName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'ReportName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (INT IDENTITY); clave primaria y consecutivo autonumérico de la tabla CustomReports', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IX_CR_ReportName_Id]
    ON [MedicalHistory].[CustomReports]([ReportName] ASC)
    INCLUDE([Id]);


GO
ALTER INDEX [IX_CR_ReportName_Id]
    ON [MedicalHistory].[CustomReports] DISABLE;


GO
CREATE NONCLUSTERED INDEX [IX_CR_ReportName]
    ON [MedicalHistory].[CustomReports]([ReportName] ASC)
    INCLUDE([Id], [DateCreation], [DateModify], [UserCreation], [UserModify]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de informes o reportes personalizados del módulo de historia clínica, incluyendo el contenido del reporte en formato binario y la trazabilidad de quién lo creó o modificó y cuándo.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports';
