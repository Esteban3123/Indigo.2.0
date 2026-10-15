CREATE TABLE [ClinicalParameters].[ClinicalHistoryGroupXPage] (
    [Id]                       INT IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats] INT NOT NULL,
    [IdHistoryGroups]          INT NOT NULL,
    [OrderGroup]               INT NULL,
    CONSTRAINT [PK_ClinicalHistoryGroupXPage] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo para indicar el orden en el cual se parametrizan los grupos de las pestañas dinamicas', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'OrderGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla HistoryGroups', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'IdHistoryGroups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'Id';

