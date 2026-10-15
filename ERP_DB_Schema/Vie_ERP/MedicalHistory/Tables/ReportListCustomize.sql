CREATE TABLE [MedicalHistory].[ReportListCustomize] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [ReportClass]       VARCHAR (150) NOT NULL,
    [ReportName]        VARCHAR (150) NOT NULL,
    [ReportDescription] VARCHAR (800) NOT NULL,
    [ModuleName]        VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_ReportListCustomize] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Nombre del Modulo ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ModuleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Informe Descripción', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Nombre del informe o reporte', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Clase de informe', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'ReportClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ReportListCustomize', @level2type = N'COLUMN', @level2name = N'Id';

