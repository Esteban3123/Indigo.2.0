CREATE TABLE [dbo].[HCFICHA356D] (
    [ID]                              INT           IDENTITY (1, 1) NOT NULL,
    [IDFICHANOTIFICACION]             INT           NULL,
    [CivilStatus]                     INT           NOT NULL,
    [CountryOriginOfCase]             VARCHAR (50)  NOT NULL,
    [DepartmentOriginOfCase]          VARCHAR (50)  NOT NULL,
    [MunicipalityOriginOfCase]        VARCHAR (50)  NOT NULL,
    [EducationLevel]                  INT           NOT NULL,
    [ClassificationOfInitialBehavior] INT           NOT NULL,
    [OccurrenceDate]                  DATE          NOT NULL,
    [PreviousAttempts]                BIT           NOT NULL,
    [NumberOfAttempts]                INT           NULL,
    [PatientComesAccompanied]         BIT           NOT NULL,
    [AccompanistName]                 VARCHAR (100) NULL,
    [CurrentlyConsumingSPA]           INT           NOT NULL,
    [SubstanceName]                   VARCHAR (100) NULL,
    [AccompanistPhone]                VARCHAR (10)  NULL,
    [CurrentlyInSchool]               BIT           NOT NULL,
    [AttendPrenatalCheckups]          BIT           NULL,
    [SexualOrientation]               INT           NOT NULL,
    [DifferentialPopulations]         INT           NOT NULL,
    [WhichDifferentialPopulation]     VARCHAR (30)  NULL,
    [CaseDetected]                    INT           NOT NULL,
    [WhichCaseDetected]               VARCHAR (30)  NULL,
    [Triggers]                        VARCHAR (MAX) NOT NULL,
    [RiskFactors]                     VARCHAR (MAX) NOT NULL,
    [Mechanism]                       VARCHAR (MAX) NOT NULL,
    [WhichMechanism]                  VARCHAR (30)  NULL,
    [SubstanceType]                   VARCHAR (MAX) NULL,
    [ProductName]                     VARCHAR (50)  NULL,
    [ExhibitionPathway]               VARCHAR (MAX) NOT NULL,
    [EventPlace]                      VARCHAR (MAX) NOT NULL,
    [WhichEventPlace]                 VARCHAR (30)  NULL,
    [MentalDisorder]                  CHAR (4)      NULL,
    [ReferredToMentalHealthServices]  VARCHAR (MAX) NOT NULL,
    [Observations]                    VARCHAR (500) NULL,
    [CODDIAGNO]                       CHAR (4)      NULL,
    [VERSION]                         VARCHAR (20)  NULL,
    [AreaOriginOfCase]                INT           NULL,
    CONSTRAINT [PK_HCFICHA356D] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Area de procedencia del caso:  1. Cabecera municipal  2. Centro poblado  3. Rural disperso  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AreaOriginOfCase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona remitida a servicios de salud mental:  1. Psiquiatría  2. Psicología  3. Trabajo Social', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ReferredToMentalHealthServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Trastorno mental o del compromiso asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'MentalDisorder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual (cuando en el campo Lugar donde se produjo la intoxicacion o evento seleccionan la opcion Otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichEventPlace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'JSON que guarda los datos del campo Lugar donde se produjo la intoxicacion o el evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'EventPlace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'JSON que guarda los datos del campo Vía de exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ExhibitionPathway';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ProductName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'JSON que guarda los datos del campo Tipo de sustancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SubstanceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual mecanismo (cuando en el campo Mecanismo seleccionan la opcion Otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichMechanism';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'JSON que guarda los datos del campo Mecanismo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Mechanism';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'JSON que guarda los datos del campo Factores de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'RiskFactors';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'JSON que guarda los datos del campo Factores desencadenantes:  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Triggers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual (cuando en el campo Caso Detectado Por seleccionan la opción Otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichCaseDetected';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Caso detectado por:  1. Consulta externa  2. Servicio de urgencias  3. DUES (CRUE)  4. Línea 106  5. Establecimiento educativo  6. Comunidad  7. Otro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CaseDetected';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual poblacion diferencial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichDifferentialPopulation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Poblaciones diferenciales:  1. Menor abandonado  2. En protección  3. Población LGTBI  4. Servidor público  5. Consumidor habitual de SPA  6. Trabajador informal  7. Madre cabeza de hogar  8. Ninguna  9. Otra ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'DifferentialPopulations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orientacion sexual:  1. Sin dato  2. Heterosexual  3. Lesbiana  4. Gay  5. Bisexual  6. Transgénero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SexualOrientation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Asiste a controles prenatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AttendPrenatalCheckups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paciente actualmente escolarizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CurrentlyInSchool';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono del acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AccompanistPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de sustancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SubstanceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paciente consume sustancias psicoactivas:  1. Si   2. No  3. Sin determinar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CurrentlyConsumingSPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AccompanistName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paciente viene acompañado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'PatientComesAccompanied';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de intentos:  1. Una vez  2. Dos veces  3. Tres veces  4. Más de tres veces  5. Sin dato  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'NumberOfAttempts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Intentos previos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'PreviousAttempts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de ocurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'OccurrenceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificacion inicial del caso:  1. Ideación suicida  2. Amenaza  3. Intento de suicidio  4. Suicidio consumado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ClassificationOfInitialBehavior';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel educativo:  1. No fue a la escuela  2. Preescolar  3. Primaria incompleta  4. Primaria completa  5. Secundaria incompleta  6. Secundaria completa  7. Técnico post-secundaria incompleta  8. Técnico post-secundaria completa  9. Universidad incompleta  10. Universidad completa  11. Postgrado incompleto  12. Postgrado completo  13. Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'EducationLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Municipio de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'MunicipalityOriginOfCase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Departamento de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'DepartmentOriginOfCase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'País de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CountryOriginOfCase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado civil:  1. Soltero(a)  2. Casado(a)  3. Divorciado(a)  4. Separado(a)  5. Viudo(a)  6. Unión libre  7. Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CivilStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabebera de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ID';

