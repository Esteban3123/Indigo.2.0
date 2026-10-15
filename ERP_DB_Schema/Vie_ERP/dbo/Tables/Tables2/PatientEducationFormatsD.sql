CREATE TABLE [dbo].[PatientEducationFormatsD] (
    [Id]                                INT           IDENTITY (1, 1) NOT NULL,
    [IdPatientEducationFormatsC]        INT           NOT NULL,
    [IdParamEducationFormatsGroups]     INT           NOT NULL,
    [IdParamEducationFormatsVariablesC] INT           NOT NULL,
    [Value]                             VARCHAR (MAX) NOT NULL,
    [IdParamEducationFormatsVariablesD] INT           NULL,
    CONSTRAINT [PK_PatientEducationFormatsD] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla ParamEducationFormatsVariablesD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el valor del formato de educación para el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla ParamEducationFormatsVariablesC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla ParamEducationFormatsGroups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla PatientEducationFormatsC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdPatientEducationFormatsC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'Id';

