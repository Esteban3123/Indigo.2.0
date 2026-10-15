CREATE TABLE [MedicalHistory].[XMLPersonalization] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [IdModel]         INT             NOT NULL,
    [IdGroup]         INT             NOT NULL,
    [OriginModelName] VARCHAR (30)    NOT NULL,
    [OriginGroupName] VARCHAR (30)    NULL,
    [XML]             VARBINARY (MAX) NOT NULL,
    [UserCreation]    CHAR (20)       NOT NULL,
    [DateCreation]    DATETIME        NOT NULL,
    [UserModify]      CHAR (20)       NULL,
    [DateModify]      DATETIME        NULL,
    CONSTRAINT [PK_XMLPersonalization] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se modifica la customizacion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'DateModify';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que modifica la customizacion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'UserModify';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se creo la customizacion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que creo la customizacion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'XML con la customizacion de las variables por grupo', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'XML';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la tabla Origen del grupo:  [ClinicalParameters].[HistoryGroups] - [ClinicalParameters].[HistoryGroups]', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'OriginGroupName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la tabla origen del modelo de HC: [ClinicalParameters].[ClinicalHistoryFormats]', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'OriginModelName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id entidad origen: [ClinicalParameters].[HistoryGroups] - [ClinicalParameters].[HistoryGroups]', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'IdGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id entidad origen: ClinicalParameters.ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'IdModel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'Id';

