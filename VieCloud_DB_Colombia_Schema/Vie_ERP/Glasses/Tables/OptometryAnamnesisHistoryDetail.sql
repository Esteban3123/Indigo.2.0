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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de uso de corrección óptica: 1=Permanente (diario), 2=Ocupacional (laboral), 3=Ocasional. Campo VARCHAR(100) condicional habilitado cuando UseCorrelation=Sí. Tipo SQL: INT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'FormUseCorrelation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma de uso de corrección: 1. Permanente, 2. Ocupacional, 3. Ocasional (Se habilita en el formulario cuando la selección del campo Uso de corrección fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'FormUseCorrelation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'FormUseCorrelation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de corrección óptica o dispositivo visual: 1=Lentes de contacto, 2=Gafas/anteojos. Campo INT condicional habilitado cuando UseCorrelation=Sí. Indica modalidad de compensación refractiva.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'CorrelationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de corrección: 1. Lentes de contacto, 2. Gafas (Se habilita en el formulario cuando la selección del campo Uso de corrección fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'CorrelationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'CorrelationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Uso de corrección óptica: 1=Sí (requiere compensación visual), 2=No (sin necesidad de corrección). Campo INT booleano que controla habilitación de CorrelationType y FormUseCorrelation.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'UseCorrelation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Uso de corrección: 1. Si, 2. No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'UseCorrelation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'UseCorrelation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de antecedentes farmacológicos: medicamentos actuales, alergias, reacciones adversas. Campo VARCHAR(100) descriptivo, habilitado cuando PharmacologicalHistory=Sí. Información libre del paciente en evaluación optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichPharmacological';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se ingresan los antecedentes farmacologicos (Se habilita en el formulario cuando la selección del campo Farmacológicos fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichPharmacological';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichPharmacological';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes farmacológicos del paciente: 1=Sí (reporta medicamentos/alergias), 2=No. Campo INT booleano que controla visibilidad de WhichPharmacological en anamnesis optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'PharmacologicalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes farmacológicos: 1. Si, 2. No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'PharmacologicalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'PharmacologicalHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de antecedentes quirúrgicos: intervenciones oculares o sistémicas previas. Campo VARCHAR(100) descriptivo, habilitado cuando SurgicalHistory=Sí. Relevante para evaluación oftalmológica y seguridad.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se ingresan los antecedentes quirurgicos (Se habilita en el formulario cuando la selección del campo Quirurgicos fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichSurgical';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes quirúrgicos del paciente: 1=Sí (cirugías oculares/sistémicas), 2=No. Campo INT booleano que controla visibilidad de WhichSurgical en historia clínica optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'SurgicalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes quirurgicos: 1. Si, 2. No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'SurgicalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'SurgicalHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de antecedentes familiares oftalmológicos: miopía, hipermetropía, astigmatismo, glaucoma, cataratas, degeneración macular. Campo VARCHAR(100), habilitado cuando FamilyHistory=Sí.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichFamily';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se ingresan los antecedentes familiares (Se habilita en el formulario cuando la selección del campo Familiares fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichFamily';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichFamily';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes familiares de patología oftalmológica o refractiva: 1=Sí (familiares con defectos visuales), 2=No. Campo INT booleano que controla WhichFamily en anamnesis optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'FamilyHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes familiares: 1. Si, 2. No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'FamilyHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'FamilyHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de antecedentes personales oftalmológicos: defectos refractivos previos, enfermedades oculares, traumatismos. Campo VARCHAR(100), habilitado cuando PersonalHistory=Sí. Historia visual del paciente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichPersonal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se ingresan los antecedentes personales (Se habilita en el formulario cuando la selección del campo Personales fue SI)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichPersonal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'WhichPersonal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes personales oftalmológicos: 1=Sí (defectos visuales/enfermedades oculares), 2=No. Campo INT booleano que controla WhichPersonal en historia clínica de evaluación optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'PersonalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes personales: 1. Si, 2. No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'PersonalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'PersonalHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) a tabla [Glasses].[OptometryClinicalEvaluationC]: identificador único de la evaluación clínica optométrica padre. Campo INT NOT NULL que vincula detalles anamnésicos con evaluación principal.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla ClinicalEvaluationOptometry', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes de salud registrados en la anamnesis optométrica del paciente: personales, familiares, quirúrgicos y farmacológicos, junto con el uso de correlación visual (lentes u otros auxiliares ópticos). Forma parte de la historia clínica de optometría.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de antecedentes de la anamnesis optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisHistoryDetail', @level2type = N'COLUMN', @level2name = N'Id';
