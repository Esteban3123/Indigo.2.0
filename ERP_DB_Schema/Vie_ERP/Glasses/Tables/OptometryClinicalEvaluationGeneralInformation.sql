CREATE TABLE [Glasses].[OptometryClinicalEvaluationGeneralInformation] (
    [Id]                                         INT            IDENTITY (1, 1) NOT NULL,
    [idOptometryClinicalEvaluationC]             INT            NOT NULL,
    [OphthalmologicalAnamnesisObservations]      VARCHAR (500)  NULL,
    [OphthalmologicalHistoryObservations]        VARCHAR (500)  NULL,
    [ExternalReviewObservations]                 VARCHAR (1000) NULL,
    [VisualAcuityObservations]                   VARCHAR (500)  NULL,
    [VisualAcuityCorrectionObservations]         VARCHAR (500)  NULL,
    [ObjectiveRefractionObservations]            VARCHAR (200)  NULL,
    [SubjectiveRefractionObservations]           VARCHAR (200)  NULL,
    [KeratometryRefractionObservations]          VARCHAR (200)  NULL,
    [AutokeratometryRefractionObservations]      VARCHAR (200)  NULL,
    [FurtherEvaluationPupilarExamObservations]   VARCHAR (200)  NULL,
    [FurtherEvaluationTonometryObservations]     VARCHAR (500)  NULL,
    [FurtherEvaluationColorVisionObservations]   VARCHAR (500)  NULL,
    [FurtherEvaluationContactologyObservations]  VARCHAR (200)  NULL,
    [VisualAcuityCorrection]                     TINYINT        NULL,
    [PupillaryDistance]                          VARCHAR (5)    NULL,
    [Dominance]                                  VARCHAR (200)  NULL,
    [OphthalmologicalDiagnoses]                  VARCHAR (8000) NULL,
    [Analysis]                                   VARCHAR (8000) NULL,
    [ManagementPlan]                             VARCHAR (8000) NULL,
    [WorkTeam]                                   VARCHAR (8000) NULL,
    [FurtherEvaluationBiomicroscopyObservations] VARCHAR (5000) NULL,
    [FurtherEvaluationFundusObservations]        VARCHAR (5000) NULL,
    [FurtherEvaluationGonioscopyObservations]    VARCHAR (500)  NULL,
    [FurtherEvaluationHasStereopsis]             TINYINT        NULL,
    [FurtherEvaluationStereopsisTechnique]       TINYINT        NULL,
    [FurtherEvaluationStereopsisArcSeconds]      VARCHAR (50)   NULL,
    CONSTRAINT [PK_OptometryClinicalEvaluationGeneralInformation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryClinicalEvaluationGeneralInformation_OptometryClinicalEvaluationC] FOREIGN KEY ([idOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Arco de segundos estereopsis (14-05-2023 - Debido a la actualización San José, este campo se crea ya que se traslada desde el page examen externo hasta valoración complementaria, pero al no tener dependencia con ninguna de las tablas creadas para ese page se crea el campo en esta tabla  -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationStereopsisArcSeconds';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina la técnica usada para la esteriopsis: 1 Tismus, 2 Random, 3 Frisby, 4 Lang (14-05-2023 - Debido a la actualización San José, este campo se crea ya que se traslada desde el page examen externo hasta valoración complementaria, pero al no tener dependencia con ninguna de las tablas creadas para ese page se crea el campo en esta tabla  -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationStereopsisTechnique';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el paciente tiene esteriopsis: 1 Sí, 2 No (14-05-2023 - Debido a la actualización San José, este campo se crea ya que se traslada desde el page examen externo hasta valoración complementaria, pero al no tener dependencia con ninguna de las tablas creadas para ese page se crea el campo en esta tabla  -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationHasStereopsis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dominancia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'Dominance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del campo Distancia pupilar en grupo Refracción subjetiva (creado el 13-05-2023 por Leonardo Rojas - actualización San josé)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el uso de corrección actual: 1 Lentes de contacto, 2 Gafas, 3 Ninguno', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Contactología del page Valoración complementaria (14-05-2023 - Este campo deja de ser utilizado tras actualización San José ya que el agrupador Contactología desaparece del page Valoracion Complementaria -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationContactologyObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Visión cromática del page Valoración complementaria', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationColorVisionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Tonometría del page Valoración complementaria', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationTonometryObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Examen Pupilar del page Valoración complementaria', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationPupilarExamObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Autoqueratometría de refracción', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'AutokeratometryRefractionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Queratometría de Refracción', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'KeratometryRefractionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Refracción subjetiva', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'SubjectiveRefractionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Refracción objetiva', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'ObjectiveRefractionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Corrección de agudeza visual', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityCorrectionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Agudeza visual', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Examen externo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'ExternalReviewObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Antecedentes oftalmológicos del page Anamnesis / Antecedentes', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'OphthalmologicalHistoryObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Anamnesis oftalmológica del page Anamnesis / Antecedentes', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'OphthalmologicalAnamnesisObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de valoracion (OptometryClinicalEvaluationC)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'idOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'Id';

