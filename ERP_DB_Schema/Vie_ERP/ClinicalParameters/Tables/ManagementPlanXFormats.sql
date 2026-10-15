CREATE TABLE [ClinicalParameters].[ManagementPlanXFormats] (
    [Id]                       INT IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats] INT NOT NULL,
    [CodeManagementPlan]       INT NOT NULL,
    CONSTRAINT [PK_ManagementPlanXFormats] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Public Enum ePlanManejo          Dieta = 1          Medicamentos = 2          Insumos_Dispositivos = 3          OrdenesServicio = 4          Interconsultas_Referencia = 5          Recomendaciones = 6          Hemocomponentes = 7          PAD = 8          Mezcla_liquidos = 9  End Enum', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'CodeManagementPlan';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con el formato', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ManagementPlanXFormats', @level2type = N'COLUMN', @level2name = N'Id';

