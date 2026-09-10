CREATE TABLE [dbo].[HCFICHA875D] (
    [ID]                         INT           IDENTITY (1, 1) NOT NULL,
    [IDFICHANOTIFICACION]        INT           NULL,
    [Child]                      BIT           NULL,
    [CivilStatus]                INT           NULL,
    [EducationLevel]             INT           NULL,
    [InitialRiskLevel]           BIT           NULL,
    [FinalClassification]        INT           NULL,
    [FinalRiskLevel]             BIT           NULL,
    [SED]                        VARCHAR (100) NULL,
    [FamilyFormation]            VARCHAR (MAX) NOT NULL,
    [PersonWithDisability]       BIT           NULL,
    [VictimActivity]             VARCHAR (MAX) NOT NULL,
    [SexualOrientation]          INT           NOT NULL,
    [GenderIdentity]             INT           NOT NULL,
    [SPAConsumer]                BIT           NOT NULL,
    [HeadOfHousehold]            BIT           NOT NULL,
    [AlcoholVictim]              BIT           NOT NULL,
    [LiveWiththeAgressor]        BIT           NOT NULL,
    [ArmedConflict]              BIT           NOT NULL,
    [VictimOfArmedConflict]      BIT           NOT NULL,
    [MentalDisorder]             BIT           NOT NULL,
    [UnfavorableSocioeconomic]   BIT           NOT NULL,
    [Unemployment]               BIT           NOT NULL,
    [StableCouple]               BIT           NOT NULL,
    [Children]                   BIT           NOT NULL,
    [NoSupportNetworks]          BIT           NOT NULL,
    [IgnoranceOfRights]          BIT           NOT NULL,
    [HistoryOfViolence]          BIT           NOT NULL,
    [DateEvent]                  DATE          NOT NULL,
    [TypeOfViolence1]            VARCHAR (MAX) NOT NULL,
    [PlaceOcurrence1Agressor]    INT           NOT NULL,
    [HappenedBefore1Agressor]    BIT           NOT NULL,
    [Sex1Agressor]               INT           NOT NULL,
    [Age1Agressor]               INT           NOT NULL,
    [Relation1Agressor]          INT           NOT NULL,
    [Drugs1Agressor]             BIT           NOT NULL,
    [TypeOfViolence2]            VARCHAR (MAX) NULL,
    [PlaceOcurrence2Agressor]    INT           NULL,
    [HappenedBefore2Agressor]    BIT           NULL,
    [Sex2Agressor]               INT           NULL,
    [Age2Agressor]               INT           NULL,
    [Relation2Agressor]          INT           NULL,
    [Drugs2Agressor]             BIT           NULL,
    [TypeOfViolence3]            VARCHAR (MAX) NULL,
    [PlaceOcurrence3Agressor]    INT           NULL,
    [HappenedBefore3Agressor]    BIT           NULL,
    [Sex3Agressor]               INT           NULL,
    [Age3Agressor]               INT           NULL,
    [Relation3Agressor]          INT           NULL,
    [Drugs3Agressor]             BIT           NULL,
    [TypeOfViolence4]            VARCHAR (MAX) NULL,
    [PlaceOcurrence4Agressor]    INT           NULL,
    [HappenedBefore4Agressor]    BIT           NULL,
    [Sex4Agressor]               INT           NULL,
    [Age4Agressor]               INT           NULL,
    [Relation4Agressor]          INT           NULL,
    [Drugs4Agressor]             BIT           NULL,
    [TypeOfViolence5]            VARCHAR (MAX) NULL,
    [PlaceOcurrence5Agressor]    INT           NULL,
    [HappenedBefore5Agressor]    BIT           NULL,
    [Sex5Agressor]               INT           NULL,
    [Age5Agressor]               INT           NULL,
    [Relation5Agressor]          INT           NULL,
    [Drugs5Agressor]             BIT           NULL,
    [SexualViolence]             VARCHAR (MAX) NULL,
    [HealthCareHealthProvider]   VARCHAR (MAX) NOT NULL,
    [Mechanisms]                 VARCHAR (MAX) NOT NULL,
    [OtherMechanisms]            VARCHAR (50)  NULL,
    [AnatomicalSiteWithBurn]     VARCHAR (MAX) NULL,
    [BurnDegree]                 INT           NULL,
    [Extension]                  INT           NULL,
    [AtTimeOfCare]               VARCHAR (MAX) NOT NULL,
    [ManagementInstitution]      VARCHAR (MAX) NOT NULL,
    [WhereCaseDetected]          INT           NOT NULL,
    [WhichCaseDetected]          VARCHAR (50)  NULL,
    [RiskAssessment]             INT           NOT NULL,
    [EntityMonitoring]           VARCHAR (100) NULL,
    [MonitoringStartDate]        DATE          NOT NULL,
    [HomeVisitScheduled]         BIT           NOT NULL,
    [HomeVisitScheduledDate]     DATE          NULL,
    [HomeVisitDone]              BIT           NOT NULL,
    [HomeVisitDoneDate]          DATE          NULL,
    [IdentifiedParentingStyle]   INT           NULL,
    [InitialReferralDone]        BIT           NOT NULL,
    [WhereInitialReferral]       VARCHAR (MAX) NULL,
    [InitialReferralOthers]      VARCHAR (30)  NULL,
    [FiledToICBF]                VARCHAR (30)  NULL,
    [FiledToPolice]              VARCHAR (30)  NULL,
    [FiledToProsecutorOffice]    VARCHAR (30)  NULL,
    [FiledToOther]               VARCHAR (30)  NULL,
    [MonitoringActivity]         VARCHAR (MAX) NULL,
    [NumberOfMonitoring]         INT           NULL,
    [CaseClosedIn]               VARCHAR (MAX) NULL,
    [CaseClosingDate]            DATE          NULL,
    [ResponsibleIECProfessional] VARCHAR (100) NULL,
    [FALLIDO]                    BIT           NOT NULL,
    [SISVECOSCheck]              BIT           NULL,
    [SISVECOSText]               VARCHAR (20)  NULL,
    [SIVELCECheck]               BIT           NULL,
    [SIVELCEText]                VARCHAR (20)  NULL,
    [VESPACheck]                 BIT           NULL,
    [VESPAText]                  VARCHAR (20)  NULL,
    [InitialCaseNumber]          DECIMAL (20)  NULL,
    [Observations]               VARCHAR (500) NULL,
    [CODDIAGNO]                  CHAR (4)      NULL,
    [VERSION]                    VARCHAR (20)  NULL,
    CONSTRAINT [PK_HCFICHA875D] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Número del caso inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'InitialCaseNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo de texto de VESPA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'VESPAText';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo de Check VESPA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'VESPACheck';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo de texto de SIVELCE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'SIVELCEText';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo de Check SIVELCE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'SIVELCECheck';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo de texto de SISVECOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'SISVECOSText';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo de Check SISVECOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'SISVECOSCheck';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo FALLIDO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'FALLIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Profesional responsable de la IEC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'ResponsibleIECProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Fecha cierre caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'CaseClosingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Caso cerrado en', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'CaseClosedIn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Número de seguimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'NumberOfMonitoring';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Actividad de seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'MonitoringActivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Radicado a Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'FiledToOther';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Radicado a Fiscalía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'FiledToProsecutorOffice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Radicado a Comisaría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'FiledToPolice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Radicado a ICBF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'FiledToICBF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Otra institución ¿Cuál?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'InitialReferralOthers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Donde remisión inicial?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'WhereInitialReferral';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Se realizó remisión inicial?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'InitialReferralDone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Estilo de crianza identificado  1 - Autoritario  2 - Democrático  3 - Negligente  4 - Permisivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'IdentifiedParentingStyle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Fecha visita domiciliaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HomeVisitDoneDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Se realizó visita domiciliaria?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HomeVisitDone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Fecha programación de visita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HomeVisitScheduledDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Se programó visita domiciliaria?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HomeVisitScheduled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Fecha inicio seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'MonitoringStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Entidad responsable del seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'EntityMonitoring';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Valoración de riesgo  1 - Alto  2 - Medio  3 - Bajo   4 - Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'RiskAssessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Cual?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'WhichCaseDetected';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Donde se detectó el caso?  1 - Consulta externa  2 - Urgencias  3 - Línea 106  4 - Centro educativo  5 - Reporte comunitario  6 - Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'WhereCaseDetected';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Manejo en la institución', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'ManagementInstitution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo En el momento de la atención presenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'AtTimeOfCare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Extensión   1 - Menor o igual al 5%  2 - Del 6% al 14%  3 - Mayor o igual al 15%', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Extension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Grado  1 - Primer grado  2 - Segundo grado  3 - Tercer grado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'BurnDegree';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Sitio anatómico comprometido con quemadura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'AnatomicalSiteWithBurn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Cual otro mecanismo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'OtherMechanisms';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Mecanismos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Mechanisms';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Atención en salud del prestador de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HealthCareHealthProvider';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'lmacena el JSON que contiene el campo Violencia sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'SexualViolence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Drogras del 5to agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Drugs5Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Relación del 5to agresor  1 - Padre  2 - Madre  3 - Padrastro  4 - Madrastra  5 - Hermano (a)  6 - Pareja  7 - Hijo (a)  8 - Abuelo (a)  9 - Otro familiar  10 - Conocido  11 - Desconocido  12 - Docentes  13 - Administrativos  14 - Estudiantes  15 - Mujer gestante  16 - Expareja  17 - Vecino  18 - Jefe  19 - Sacerdote  20 - Servidor público', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Relation5Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Edad del 5to agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Age5Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sexo del 5to agresor  1 = M  2 = F', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Sex5Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Ócurrió antes del 5to agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HappenedBefore5Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Lugar de ocurrencia del 5to agresor  1 - Vivienda  2 - Vía pública  3 - Comercio y áreas de servicios (tiendas, centro comercial, etc.)  4 - Lugar de trabajo  5 - Establecimiento educativo  6 - Otro  7 - Lugares de esparcimiento con expendio de alcohol  8 - Área deportiva y recreativa  9 - Otros espacios abiertos (bosques, potreros, etc.)  10 -Institución de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'PlaceOcurrence5Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Tipo de violencia del 5to agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'TypeOfViolence5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Drogras del 4to agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Drugs4Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Relación del 4to agresor  1 - Padre  2 - Madre  3 - Padrastro  4 - Madrastra  5 - Hermano (a)  6 - Pareja  7 - Hijo (a)  8 - Abuelo (a)  9 - Otro familiar  10 - Conocido  11 - Desconocido  12 - Docentes  13 - Administrativos  14 - Estudiantes  15 - Mujer gestante  16 - Expareja  17 - Vecino  18 - Jefe  19 - Sacerdote  20 - Servidor público', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Relation4Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Edad del 4to agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Age4Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sexo del 4to agresor  1 = M  2 = F', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Sex4Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Ócurrió antes del 4to agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HappenedBefore4Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Lugar de ocurrencia del 4to agresor  1 - Vivienda  2 - Vía pública  3 - Comercio y áreas de servicios (tiendas, centro comercial, etc.)  4 - Lugar de trabajo  5 - Establecimiento educativo  6 - Otro  7 - Lugares de esparcimiento con expendio de alcohol  8 - Área deportiva y recreativa  9 - Otros espacios abiertos (bosques, potreros, etc.)  10 -Institución de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'PlaceOcurrence4Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Tipo de violencia del 4to agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'TypeOfViolence4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Drogras del 3er agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Drugs3Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Relación del 3er agresor  1 - Padre  2 - Madre  3 - Padrastro  4 - Madrastra  5 - Hermano (a)  6 - Pareja  7 - Hijo (a)  8 - Abuelo (a)  9 - Otro familiar  10 - Conocido  11 - Desconocido  12 - Docentes  13 - Administrativos  14 - Estudiantes  15 - Mujer gestante  16 - Expareja  17 - Vecino  18 - Jefe  19 - Sacerdote  20 - Servidor público', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Relation3Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Edad del 3er agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Age3Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sexo del 3er agresor  1 = M  2 = F', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Sex3Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Ócurrió antes del 3er agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HappenedBefore3Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Lugar de ocurrencia del 3er agresor  1 - Vivienda  2 - Vía pública  3 - Comercio y áreas de servicios (tiendas, centro comercial, etc.)  4 - Lugar de trabajo  5 - Establecimiento educativo  6 - Otro  7 - Lugares de esparcimiento con expendio de alcohol  8 - Área deportiva y recreativa  9 - Otros espacios abiertos (bosques, potreros, etc.)  10 -Institución de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'PlaceOcurrence3Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Tipo de violencia del 3er agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'TypeOfViolence3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Drogras del 2do agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Drugs2Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Relación del 2do agresor  1 - Padre  2 - Madre  3 - Padrastro  4 - Madrastra  5 - Hermano (a)  6 - Pareja  7 - Hijo (a)  8 - Abuelo (a)  9 - Otro familiar  10 - Conocido  11 - Desconocido  12 - Docentes  13 - Administrativos  14 - Estudiantes  15 - Mujer gestante  16 - Expareja  17 - Vecino  18 - Jefe  19 - Sacerdote  20 - Servidor público', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Relation2Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Edad del 2do agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Age2Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sexo del 2do agresor  1 = M  2 = F', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Sex2Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Ócurrió antes del 2do agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HappenedBefore2Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Lugar de ocurrencia del 2do agresor  1 - Vivienda  2 - Vía pública  3 - Comercio y áreas de servicios (tiendas, centro comercial, etc.)  4 - Lugar de trabajo  5 - Establecimiento educativo  6 - Otro  7 - Lugares de esparcimiento con expendio de alcohol  8 - Área deportiva y recreativa  9 - Otros espacios abiertos (bosques, potreros, etc.)  10 -Institución de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'PlaceOcurrence2Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Tipo de violencia del 2do agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'TypeOfViolence2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Drogras del 1er agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Drugs1Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Relación del 1er agresor  1 - Padre  2 - Madre  3 - Padrastro  4 - Madrastra  5 - Hermano (a)  6 - Pareja  7 - Hijo (a)  8 - Abuelo (a)  9 - Otro familiar  10 - Conocido  11 - Desconocido  12 - Docentes  13 - Administrativos  14 - Estudiantes  15 - Mujer gestante  16 - Expareja  17 - Vecino  18 - Jefe  19 - Sacerdote  20 - Servidor público', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Relation1Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Edad del 1er agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Age1Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sexo del 1er agresor  1 = M  2 = F', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Sex1Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Ócurrió antes del 1er agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HappenedBefore1Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Lugar de ocurrencia del 1er agresor  1 - Vivienda  2 - Vía pública  3 - Comercio y áreas de servicios (tiendas, centro comercial, etc.)  4 - Lugar de trabajo  5 - Establecimiento educativo  6 - Otro  7 - Lugares de esparcimiento con expendio de alcohol  8 - Área deportiva y recreativa  9 - Otros espacios abiertos (bosques, potreros, etc.)  10 -Institución de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'PlaceOcurrence1Agressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON que contiene el campo Tipo de violencia del 1er agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'TypeOfViolence1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Fecha del evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'DateEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Antecedentes de violencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HistoryOfViolence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Desconocimiento de derechos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'IgnoranceOfRights';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sin redes de apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'NoSupportNetworks';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Hijos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Children';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Pareja estable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'StableCouple';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Desempleo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Unemployment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Condiciones socioeconómicas desfavorables', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'UnfavorableSocioeconomic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo trastorno mental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'MentalDisorder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Víctima del conflicto armado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'VictimOfArmedConflict';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo ¿Hecho violento ocurrido en el marco del conflicto armado?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'ArmedConflict';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Convive con el agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'LiveWiththeAgressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Alcohol víctima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'AlcoholVictim';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Persona con jefatura de hogar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'HeadOfHousehold';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Persona consumidora de SPA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'SPAConsumer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo identidad de género  1 - Masculino  2 - Femenino  3 - Transgénero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'GenderIdentity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Orientación sexual  1 - Homosexual  2 - Bisexual  3 - Heterosexual  4 - Asexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'SexualOrientation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON del campo Actividad de la víctima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'VictimActivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Persona con discapacidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'PersonWithDisability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el JSON del campo Conformación familiar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'FamilyFormation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo SED', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'SED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Nivel de riesgo final del caso  True = Prioritario  False = Control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'FinalRiskLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Clasificación final del caso  1 - No aplica  2 - Descartado  3 - Conf por laboratorio  4 - Otra actualización  5 - Conf por clínica  6 - Descartado por error de digitación  7 - Conf por nexo epidemiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'FinalClassification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Nivel de riesgo inicial  True = Prioritario  False = Control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'InitialRiskLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo nivel educativo  1 - No fue a la escuela  2 - Preescolar  3 - Primaria incompleta  4 - Primaria completa  5 - Secundaria incompleta  6 - Secundaria completa  7 - Técnico post-secundaria incompleta  8 - Técnico post-secundaria completa  9 - Universidad incompleta  10 - Universidad completa  11 - Postgrado incompleto  12 - Postgrado completo  13 - Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'EducationLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo estado civil  1 - Soltero/a  2 - Casado/a  3 - Divorciado/a  4 - Separado/a  5 - Viudo/a  6 - Unión libre  7 - Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'CivilStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Hijo/Hija de', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'Child';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de  la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875D', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de detalle de la ficha de notificación 875 (violencia intrafamiliar/sexual en el contexto colombiano de salud pública). Registra datos sociodemográficos de la víctima, factores de riesgo, caracterización de hasta cinco agresores con tipo de violencia, lugar y relación, así como atención clínica y seguimiento del caso. Incluye referencias a reportes a sistemas de vigilancia (SISVECOS, SIVELCE, VESPA), radicados ante autoridades (ICBF, Fiscalía, Comisaría) y el estado de cierre del caso con visitas domiciliarias y remisiones iniciales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCFICHA875D';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCFICHA875D';
GO
