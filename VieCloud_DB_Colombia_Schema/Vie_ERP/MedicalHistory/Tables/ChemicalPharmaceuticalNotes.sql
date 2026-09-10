CREATE TABLE [MedicalHistory].[ChemicalPharmaceuticalNotes] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [IPCODPACI]    VARCHAR (25)   NOT NULL,
    [NUMINGRES]    CHAR (10)      NOT NULL,
    [CODPROSAL]    VARCHAR (20)   NOT NULL,
    [CODCENATE]    CHAR (10)      NOT NULL,
    [UFUCODIGO]    CHAR (10)      NOT NULL,
    [CODNIVIMP]    CHAR (2)       NULL,
    [Title]        VARCHAR (150)  NOT NULL,
    [Analysis]     VARCHAR (3000) NOT NULL,
    [ActionPlan]   VARCHAR (3000) NOT NULL,
    [DocumentDate] DATETIME       NOT NULL,
    [CreationUser] VARCHAR (20)   NOT NULL,
    [CreationDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_ChemicalPharmaceuticalNotes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalHistory.ChemicalPharmaceuticalNotes_HCNIVIMPN] FOREIGN KEY ([CODNIVIMP]) REFERENCES [dbo].[HCNIVIMPN] ([CODNIVIMP])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de nota químico farmacéutica en el sistema (DATETIME). Auditoría de cuándo se registró la evaluación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o profesional que creó el registro de la nota químico farmacéutica (VARCHAR 20). Identificación del autor/farmacéutico responsable de la documentación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el usuario quien lo creo', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento clínico o evaluación químico farmacéutica realizada (DATETIME). Momento en que se efectuó el análisis farmacoterapéutico.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha del documento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plan de acción, recomendaciones o intervenciones farmacéuticas derivadas del análisis (VARCHAR 3000). Instrucciones de seguimiento, ajustes de medicación o monitoreo.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'ActionPlan';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda plan de acción', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'ActionPlan';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'ActionPlan';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis clínico químico farmacéutico completo, evaluación de farmacocinética, interacciones medicamentosas o notas de perfil terapéutico (VARCHAR 3000). Hallazgos técnicos de farmacoterapia.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Analysis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el analisis Químico Farmacéutico Notas', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Analysis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Analysis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Título o encabezado descriptivo de la nota químico farmacéutica (VARCHAR 150). Resumen breve del tipo de análisis o evaluación realizada.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Title';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la descripcion titulo', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Title';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del nivel de importancia o urgencia del perfil farmacoterapéutico químico farmacéutico (CHAR 2, FK a HCNIVIMPN). Prioridad clínica: crítico, alto, medio, bajo.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de Importancia de Perfil Farmacoterapeutico -  Quimico Famaceutico', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional o departamento donde se registra la nota (CHAR 10). Identificador de farmacia, urgencias, unidad de cuidados intensivos u otra área clínica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código unidad funcional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, institución o clínica donde se documenta la evaluación (CHAR 10). Identificador de sede hospitalaria o consultorio.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código centro de atención', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional (químico farmacéutico, farmacólogo o especialista) responsable de la nota (VARCHAR 20). Identificación del especialista que realiza la evaluación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código profesional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso, admisión u atención del paciente en el episodio clínico (CHAR 10). Referencia al movimiento hospitalario o consulta ambulatoria.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero de ingreso', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del paciente: cédula, identificación o documento (VARCHAR 25, PII Identification_Ofuscado). Referencia única del paciente en evaluación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y consecutivo de la nota químico farmacéutica en la tabla (INT IDENTITY). Clave primaria del registro.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas farmacéuticas y químico-farmacéuticas registradas durante la atención del paciente. Contiene el análisis clínico del medicamento, el plan de acción farmacéutico y la fecha del documento, asociadas a un ingreso, profesional y unidad funcional específicos.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes';
