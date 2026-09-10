CREATE TABLE [ClinicalParameters].[ManagementPlanXFormats] (
    [Id]                       INT IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats] INT NOT NULL,
    [CodeManagementPlan]       INT NOT NULL,
    CONSTRAINT [PK_ManagementPlanXFormats] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de plan de manejo (enumeración: 1=Dieta, 2=Medicamentos, 3=Insumos/Dispositivos, 4=Órdenes de servicio, 5=Interconsultas/Referencia, 6=Recomendaciones, 7=Hemocomponentes, 8=PAD, 9=Mezcla de líquidos); clasifica instrucciones terapéuticas y prescripciones en la atención clínica', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'CodeManagementPlan';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Public Enum ePlanManejo          Dieta = 1          Medicamentos = 2          Insumos_Dispositivos = 3          OrdenesServicio = 4          Interconsultas_Referencia = 5          Recomendaciones = 6          Hemocomponentes = 7          PAD = 8          Mezcla_liquidos = 9  End Enum', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'CodeManagementPlan';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'CodeManagementPlan';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de FK hacia tabla ClinicalHistoryFormats; vincula el plan de manejo al formato de historia clínica donde se registra', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con el formato', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único, consecutivo autoincrementado de la relación entre plan de manejo y formato de historia clínica (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los planes de manejo clínico con los formatos de historia clínica. Indica qué formatos de historia clínica están asociados a cada plan de manejo dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats';
