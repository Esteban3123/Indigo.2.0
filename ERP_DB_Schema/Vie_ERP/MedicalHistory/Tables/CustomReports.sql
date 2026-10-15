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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  la fecha de la modificación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'DateModify';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo el usuario quien modifico', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'UserModify';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo el usuario quien lo creo', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo el informe', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'Report';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el nombre del informe', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'ReportName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CustomReports', @level2type = N'COLUMN', @level2name = N'Id';

