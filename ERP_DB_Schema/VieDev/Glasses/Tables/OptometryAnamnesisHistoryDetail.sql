CREATE TABLE [Glasses].[OptometryAnamnesisHistoryDetail] (
    [Id]                             INT           IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT           NOT NULL,
    [PersonalHistory]                INT           NOT NULL,
    [WhichPersonal]                  VARCHAR (100) NULL,
    [FamilyHistory]                  INT           NOT NULL,
    [WhichFamily]                    VARCHAR (100) NULL,
    [SurgicalHistory]                INT           NOT NULL,
    [WhichSurgical]                  VARCHAR (100) NULL,
    [PharmacologicalHistory]         INT           NOT NULL,
    [WhichPharmacological]           VARCHAR (100) NULL,
    [UseCorrelation]                 INT           NOT NULL,
    [CorrelationType]                INT           NULL,
    [FormUseCorrelation]             INT           NULL,
    CONSTRAINT [PK_DetailClinicalEvaluationOptometry_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Forma de uso de corrección: 1. Permanente, 2. Ocupacional, 3. Ocasional (Se habilita en el formulario cuando la selección del campo Uso de corrección fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'FormUseCorrelation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de corrección: 1. Lentes de contacto, 2. Gafas (Se habilita en el formulario cuando la selección del campo Uso de corrección fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'CorrelationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Uso de corrección: 1. Si, 2. No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'UseCorrelation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se ingresan los antecedentes farmacologicos (Se habilita en el formulario cuando la selección del campo Farmacológicos fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichPharmacological';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes farmacológicos: 1. Si, 2. No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'PharmacologicalHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se ingresan los antecedentes quirurgicos (Se habilita en el formulario cuando la selección del campo Quirurgicos fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichSurgical';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes quirurgicos: 1. Si, 2. No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'SurgicalHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se ingresan los antecedentes familiares (Se habilita en el formulario cuando la selección del campo Familiares fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichFamily';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes familiares: 1. Si, 2. No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'FamilyHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se ingresan los antecedentes personales (Se habilita en el formulario cuando la selección del campo Personales fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichPersonal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes personales: 1. Si, 2. No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'PersonalHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla ClinicalEvaluationOptometry', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';

