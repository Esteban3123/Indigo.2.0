CREATE TABLE [dbo].[HCFICHA903D] (
    [ID]                          INT           IDENTITY (1, 1) NOT NULL,
    [IDFICHANOTIFICACION]         INT           NULL,
    [OtherNeighborhood]           VARCHAR (50)  NULL,
    [EducationLevel]              INT           NULL,
    [CivilStatus]                 INT           NULL,
    [CountryOfOrigin]             VARCHAR (50)  NULL,
    [DepartmentOfOrigin]          VARCHAR (50)  NULL,
    [MunicipalityOfOrigin]        VARCHAR (50)  NULL,
    [IncidenceDate]               DATETIME      NULL,
    [LocalityOccurrence]          VARCHAR (20)  NULL,
    [NeighborhoodOfOccurrence]    VARCHAR (50)  NULL,
    [UPZ]                         VARCHAR (50)  NULL,
    [AreaOfOccurrence]            INT           NULL,
    [InjuryOf]                    VARCHAR (MAX) NULL,
    [TypoeOfService]              INT           NULL,
    [TransportMode]               INT           NULL,
    [VehicleType]                 INT           NULL,
    [ConditionOfTheInjured]       INT           NULL,
    [ConsumptionInjuryType]       VARCHAR (MAX) NULL,
    [TypeOfAestheticInjury]       VARCHAR (MAX) NULL,
    [NoOfSurgicalProcedure]       INT           NULL,
    [TypeOfAestheticProfessional] INT           NULL,
    [WhichInjuryOf]               VARCHAR (50)  NULL,
    [TypeOfAbuse]                 INT           NULL,
    [AllegedAggressor]            INT           NULL,
    [SexOfAggressor]              INT           NULL,
    [WasReported]                 BIT           NULL,
    [SPAVictim]                   BIT           NULL,
    [AlcoholVictim]               BIT           NULL,
    [OthersVictim]                BIT           NULL,
    [SPAAggressor]                BIT           NULL,
    [AlcoholAggressor]            BIT           NULL,
    [OthersAggressor]             BIT           NULL,
    [MechanismOrElement]          VARCHAR (MAX) NULL,
    [TypeOfDrowning]              INT           NULL,
    [WhichMechanism]              VARCHAR (30)  NULL,
    [WhichConsumerItem]           VARCHAR (30)  NULL,
    [WhichBeautyItem]             VARCHAR (30)  NULL,
    [BurnType]                    VARCHAR (MAX) NULL,
    [CauseOfChemicalInjury]       INT           NULL,
    [GunpowderType]               INT           NULL,
    [AnatomicalSite]              VARCHAR (MAX) NULL,
    [LocationOfTheEvent]          INT           NULL,
    [Grade]                       INT           NULL,
    [Extension]                   INT           NULL,
    [Scenery]                     VARCHAR (MAX) NULL,
    [TypeOfJob]                   INT           NULL,
    [WorkAccidentInjury]          BIT           NULL,
    [ARL]                         VARCHAR (50)  NULL,
    [CIE10Code]                   VARCHAR (MAX) NULL,
    [TriageClassification]        INT           NULL,
    [DateOfAttention]             DATETIME      NULL,
    [EventDescription]            VARCHAR (500) NULL,
    [IECDone]                     BIT           NULL,
    [IECDoneDate]                 DATE          NULL,
    [IECResult]                   INT           NULL,
    [IECType]                     INT           NULL,
    [IECModality]                 INT           NULL,
    [IECMonitoringDate]           DATE          NULL,
    [IECClosingDate]              DATE          NULL,
    [DoctorName]                  VARCHAR (100) NULL,
    [MedicalRecord]               VARCHAR (10)  NULL,
    [DoctorId]                    NUMERIC (25)  NULL,
    [DoctorPhone]                 BIGINT        NULL,
    [CODDIAGNO]                   CHAR (4)      NULL,
    [VERSION]                     VARCHAR (20)  NULL,
    CONSTRAINT [PK_HCFICHA903D] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del formulario/esquema de la ficha 903D (DATETIME). Tipo: VARCHAR(20). Hint: control de cambios de estructura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la VERSION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 (4 caracteres). Tipo: CHAR(4). Diagnóstico principal del evento/lesión notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono del médico tratante o notificador (PII - Identification_Ofuscado). Tipo: BIGINT. Contacto profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorPhone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Teléfono del médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorPhone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula/Documento de identidad del médico (PII - Identification_Ofuscado). Tipo: NUMERIC(25). Identificación profesional sanitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Cédula del médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro médico/Número de historia clínica del paciente. Tipo: VARCHAR(10). Referencia a HC en sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MedicalRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Registro médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MedicalRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MedicalRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del médico/profesional de la salud que atiende. Tipo: VARCHAR(100). Datos del profesional sanitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Nombre del médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de cierre/conclusión de la Intervención Epidemiológica de Campo (IEC). Tipo: DATE. Fin del proceso investigativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECClosingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Fecha de cierre IEC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECClosingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECClosingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de seguimiento/control posterior de la Intervención Epidemiológica de Campo (IEC). Tipo: DATE. Supervisión post-intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECMonitoringDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Fecha de seguimiento IEC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECMonitoringDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECMonitoringDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de realización IEC (1=Institucional/centro, 2=Domiciliaria/hogar). Tipo: INT. Tipo de intervención epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECModality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Modalidad de la IEC  1 - Institucional  2 - Domiciliaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECModality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECModality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de IEC según urgencia (1=Control/Rutinario, 2=Prioritario/Urgente). Tipo: INT. Clasificación de la intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Tipo de IEC  1 - Control  2 - Prioritario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado/Desenlace de la IEC (1=Efectiva/Exitosa, 2=Fallida/No lograda). Tipo: INT. Evaluación de intervención epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Resultado de la IEC  1 - Efectiva  2 - Fallida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización/ejecución de la Intervención Epidemiológica de Campo (IEC). Tipo: DATE. Fecha de contacto investigativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECDoneDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Fecha de realización de IEC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECDoneDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECDoneDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si se realizó Intervención Epidemiológica de Campo (IEC) al caso (BIT: 0=No, 1=Sí). Tipo: BIT. Bandera de intervención epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECDone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Se realizó intervención epidemiológica de Campo - IEC al caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECDone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECDone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción narrativa del evento/incidente/lesión (máx 500 caracteres). Tipo: VARCHAR(500). Relato circunstanciado del suceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'EventDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Descripción del evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'EventDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'EventDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de atención médica/ingreso al centro de atención. Tipo: DATETIME. Registro de atención al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DateOfAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Fecha y hora de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DateOfAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DateOfAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de urgencia Triage (1=I/Rojo/Emergencia, 2=II/Amarillo/Urgente, 3=III/Verde/Menor, 4=IV/Azul/Espera, 5=V/Blanco/Girado). Tipo: INT. Prioridad atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TriageClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Clasificación según Triage  1 - Triage I  2 - Triage II  3 - Triage III  4 - Triage IV  5 - Triage V', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TriageClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TriageClassification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 del diagnóstico (clasificación internacional diagnósticos). Tipo: VARCHAR(MAX). Codificación diagnóstica OMS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CIE10Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Falta ver como le hago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CIE10Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CIE10Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aseguradora de Riesgos Laborales (entidad responsable accidente trabajo). Tipo: VARCHAR(50). Asegurador accidente laboral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ARL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo ARL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ARL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ARL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si lesión califica como Accidente de Trabajo (BIT: 0=No, 1=Sí). Tipo: BIT. Clasificación accidente laboral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WorkAccidentInjury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Lesión con definición de accidente de trabajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WorkAccidentInjury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WorkAccidentInjury';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de empleo/ocupación (1=Formal/Registrado, 2=Informal/Independiente). Tipo: INT. Categoría laboral lesionado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfJob';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Tipo de trabajo  1 - Formal  2 - Informal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfJob';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfJob';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escenario/Contexto del evento en JSON (detalles lugar/ambiente). Tipo: VARCHAR(MAX). Descripción JSON del escenario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Scenery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo JSON Escenario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Scenery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Scenery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión/Grado de afectación (1=≤5%, 2=6-14%, 3=>15%). Tipo: INT. Porcentaje de superficie afectada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Extension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Extensión  1 - < o = al 5%  2 - 6 % a 14%  3 - > al 15%', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Extension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Extension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado de severidad lesión (1=Primero, 2=Segundo, 3=Tercero). Tipo: INT. Clasificación profundidad/severidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Grade';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Grado  1 - Primer   2 - Segundo   3 - Tercer ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Grade';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Grade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar del hecho (1=Cerrado/Interior, 2=Abierto/Exterior). Tipo: INT. Localización evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'LocationOfTheEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Lugar del hecho  1 - Cerrado   2 - Abierto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'LocationOfTheEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'LocationOfTheEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio anatómico afectado en JSON (región/estructura corporal lesionada). Tipo: VARCHAR(MAX). Anatomía lesión en JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AnatomicalSite';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo JSON Sitio anatómico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AnatomicalSite';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AnatomicalSite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de pólvora/explosivo (1=Pirotecnia, 2=Negra, 3=Explosivo industrial). Tipo: INT. Clasificación material explosivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'GunpowderType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Tipo pólvora  1 - Pirotecnia  2 - Negra  3 - Explosivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'GunpowderType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'GunpowderType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa lesión por químico (1=Accidente laboral/doméstico, 2=Agresión intencional). Tipo: INT. Origen lesión química.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CauseOfChemicalInjury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Causa de lesión por químico  1 - Accidente  2 - Agresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CauseOfChemicalInjury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CauseOfChemicalInjury';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de quemadura en JSON (térmica, química, radiante, eléctrica). Tipo: VARCHAR(MAX). Clasificación quemadura en JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'BurnType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo JSON Tipo de quemadura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'BurnType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'BurnType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación artículo belleza/cuidado/higiene causante lesión. Tipo: VARCHAR(30). Detalle producto estético.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichBeautyItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo ¿Cuál artículo de belleza, cuidado e higiene?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichBeautyItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichBeautyItem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación artículo consumo causante lesión. Tipo: VARCHAR(30). Detalle producto consumo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichConsumerItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo ¿Cuál artículo de consumo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichConsumerItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichConsumerItem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación mecanismo/forma lesión (caída, golpe, corte, etc). Tipo: VARCHAR(30). Descripción mecanismo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichMechanism';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo ¿Cuál mecanismo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichMechanism';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichMechanism';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo ahogamiento (1=Asfixia/Sofocación, 2=Inmersión acuática). Tipo: INT. Clasificación ahogo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfDrowning';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Tipo de ahogamiento  1 - Asfixia  2 - Inmersión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfDrowning';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfDrowning';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mecanismo o elemento productor lesión en JSON (objeto, agente, acción). Tipo: VARCHAR(MAX). Detalles mecanismo en JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MechanismOrElement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo JSON Mecanismo o elemento que produce lesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MechanismOrElement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MechanismOrElement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sospecha consumo otras sustancias del agresor (BIT: 0=No, 1=Sí). Tipo: BIT. Consumo sustancias agresor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OthersAggressor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Sospecha de consumo de otros agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OthersAggressor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OthersAggressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sospecha consumo alcohol del agresor (BIT: 0=No, 1=Sí). Tipo: BIT. Consumo etanol agresor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AlcoholAggressor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Sospecha de consumo de alcohol agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AlcoholAggressor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AlcoholAggressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sospecha consumo SPA (drogas) del agresor (BIT: 0=No, 1=Sí). Tipo: BIT. Consumo sustancias psicoactivas agresor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SPAAggressor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Sospecha de consumo de SPA agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SPAAggressor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SPAAggressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sospecha consumo otras sustancias de la víctima (BIT: 0=No, 1=Sí). Tipo: BIT. Consumo sustancias víctima.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OthersVictim';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Sospecha de consumo de otros víctima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OthersVictim';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OthersVictim';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sospecha consumo alcohol de la víctima (BIT: 0=No, 1=Sí). Tipo: BIT. Consumo etanol víctima.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AlcoholVictim';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Sospecha de consumo de alcohol víctima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AlcoholVictim';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AlcoholVictim';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sospecha consumo SPA (drogas) de la víctima (BIT: 0=No, 1=Sí). Tipo: BIT. Consumo sustancias psicoactivas víctima.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SPAVictim';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Sospecha de consumo de SPA víctima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SPAVictim';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SPAVictim';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si evento fue denunciado/reportado a autoridades (BIT: 0=No, 1=Sí). Tipo: BIT. Denuncia autoridades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WasReported';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo fue denunciado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WasReported';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WasReported';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo/Género del agresor (1=Masculino/Hombre, 2=Femenino/Mujer, 3=Desconocido). Tipo: INT. Género agresor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SexOfAggressor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Sexo del agresor  1 - Hombre  2 - Mujer  3 - Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SexOfAggressor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SexOfAggressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación víctima-agresor (1=Familiar, 2=Conocido, 3=Desconocido/Extraño). Tipo: INT. Tipo agresor relación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AllegedAggressor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Presunto agresor  1 - Familiar  2 - Conocido  3 - Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AllegedAggressor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AllegedAggressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo maltrato/violencia (1=Común, 2=Delito sexual, 3=Conyugal, 4=Menor, 5=Intrafamiliar, 6=Institucional). Tipo: INT. Clasificación violencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAbuse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Tipo de maltrato  1 - Violencia común  2 - Delito sexual  3 - Violencia conyugal  4 - Maltrato al menor  5 - Violencia intrafamiliar  6 - Institucional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAbuse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAbuse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de la lesión según clasificación (detalle de tipo lesión). Tipo: VARCHAR(50). Tipo lesión específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichInjuryOf';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo ¿Cuál? de la sección Tipo de lesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichInjuryOf';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichInjuryOf';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo profesional procedimiento estético (1=Sanitario, 2=Cirujano plástico, 3=Esteticista médico, 4=Especialista, 5=Esteticista, 6=Cosmetólogo). Tipo: INT. Profesional estética.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAestheticProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Tipo de profesional del procedimiento estético  1 - Profesional de la salud  2 - Cirujano plástico  3 - Médico esteticista  4 - Médico especialista  5 - Esteticista  6 - Cosmetólogo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAestheticProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAestheticProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número procedimientos quirúrgicos simultáneos (1=Uno, 2=Dos, 3=Tres, 4=Más de tres). Tipo: INT. Cantidad procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'NoOfSurgicalProcedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo No de procedimientos quirúrgicos simultáneos  1 - 1  2 - 2  3 - 3  4 - Más de 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'NoOfSurgicalProcedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'NoOfSurgicalProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo lesión por procedimiento estético en JSON (infección, quemadura, deformidad). Tipo: VARCHAR(MAX). Lesión estética en JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAestheticInjury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo JSON Tipo de lesión por estética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAestheticInjury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAestheticInjury';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo lesión por consumo sustancia en JSON (intoxicación, envenenamiento). Tipo: VARCHAR(MAX). Lesión consumo en JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ConsumptionInjuryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo JSON Tipo de lesión de consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ConsumptionInjuryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ConsumptionInjuryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición del lesionado en evento transporte (1=Peatón, 2=Pasajero, 3=Conductor, 4=Copiloto). Tipo: INT. Rol lesionado transporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ConditionOfTheInjured';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Condición del lesionado  1 - Peatón  2 - Pasajero  3 - Conductor  4 - Copiloto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ConditionOfTheInjured';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ConditionOfTheInjured';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo vehículo involucrado (1=Auto, 2=Moto, 3=Bicicleta, 4=Taxi, 5=Ambulancia, 6=Bici-taxi, 7=Moto-taxi, 8=Camioneta, 9=Bus, 10=Transmilenio, 11=SITP, 12=Carga). Tipo: INT. Clasificación vehículo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'VehicleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Tipo de vehículo  1 - Automóvil  2 - Moto  3 - Bicicleta  4 - Taxi  5 - Ambulancia  6 - Bici - taxi  7 - Moto taxi  8 - Camioneta  9 - Bus  10 - Transmilenio  11 - SITP  12 - Vehículo de carga', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'VehicleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'VehicleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad transporte (1=Individual, 2=Colectivo, 3=Masivo/Público, 4=Carga). Tipo: INT. Tipo transporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TransportMode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Modalidad de transporte  1 - Individual  2 - Colectivo  3 - Masivo  4 - Carga', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TransportMode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TransportMode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio transporte (1=Público/Estatal, 2=Privado). Tipo: INT. Prestador servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypoeOfService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Tipo de servicio  1 - Público   2 - Privado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypoeOfService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypoeOfService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación tipo lesión en JSON (contusión, fractura, herida, quemadura). Tipo: VARCHAR(MAX). Tipo lesión en JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'InjuryOf';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo JSON Lesión de', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'InjuryOf';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'InjuryOf';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área geográfica evento (1=Urbana/Ciudad, 3=Rural/Campo). Tipo: INT. Zona ocurrencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AreaOfOccurrence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Área de ocurrencia  1 - Urbana  3 - Rural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AreaOfOccurrence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AreaOfOccurrence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de Planeamiento Zonal (UPZ-Bogotá). Tipo: VARCHAR(50). Zona de planeamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'UPZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo UPZ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'UPZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'UPZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Barrio/Vereda donde ocurrió el evento. Tipo: VARCHAR(50). Localidad evento paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'NeighborhoodOfOccurrence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Barrio de ocurrencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'NeighborhoodOfOccurrence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'NeighborhoodOfOccurrence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localidad administrativa de ocurrencia (Bogotá: Usaquén, Chapinero, etc). Tipo: VARCHAR(20). Localidad evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'LocalityOccurrence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Localidad de ocurrencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'LocalityOccurrence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'LocalityOccurrence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta ocurrencia del evento/lesión. Tipo: DATETIME. Timestamp evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IncidenceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Fecha y hora de ocurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IncidenceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IncidenceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Municipio de procedencia/residencia del paciente. Tipo: VARCHAR(50). Municipio origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MunicipalityOfOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Municipio de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MunicipalityOfOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MunicipalityOfOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento de procedencia del paciente (Cundinamarca, Atlántico, etc). Tipo: VARCHAR(50). Región origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DepartmentOfOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Departamento de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DepartmentOfOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DepartmentOfOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de procedencia del paciente (Colombia, Venezuela, etc). Tipo: VARCHAR(50). País origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CountryOfOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo País de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CountryOfOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CountryOfOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado civil (1=Soltero, 2=Casado, 3=Divorciado, 4=Separado, 5=Viudo, 6=Unión libre, 7=Sin dato). Tipo: INT. Estado marital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CivilStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Estado civil  1 - Soltero(a)  2 - Casado(a)  3 - Divorciado(a)  4 - Separado(a)  5 - Viudo(a)  6 - Unión libre  7 - Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CivilStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CivilStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel educativo alcanzado (1=Sin escuela, 2=Preescolar, 3-4=Primaria, 5-6=Secundaria, 7-8=Técnico, 9-10=Universidad, 11-12=Postgrado, 13=Sin dato). Tipo: INT. Escolaridad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'EducationLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Nivel educativo.  1 - No fue a la escuela  2 - Preescolar  3 - Primaria incompleta  4 - Primaria completa  5 - Secundaria incompleta  6 - Secundaria completa  7 - Técnico post-secundaria incompleta  8 - Técnico post-secundaria completa  9 - Universidad incompleta  10 - Universidad completa  11 - Postgrado incompleto  12 - Postgrado completo  13 - Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'EducationLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'EducationLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación otro barrio/vereda si es diferente a lista estándar. Tipo: VARCHAR(50). Barrio adicional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OtherNeighborhood';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Otro¿Cuál barrio?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OtherNeighborhood';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OtherNeighborhood';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la ficha de notificación epidemiológica asociada. Tipo: INT. FK a tabla notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único/consecutivo registro en tabla HCFICHA903D. Tipo: INT IDENTITY(1,1). PK tabla 903D.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del formulario de notificación obligatoria de eventos en salud pública (ficha 903 - lesiones de causa externa, intoxicaciones, quemaduras, ahogamientos y violencias). Registra las circunstancias, el lugar, el mecanismo, el agresor, la víctima y la atención médica de cada evento notificado al SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D';
