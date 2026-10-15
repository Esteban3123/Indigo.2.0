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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la VERSION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Teléfono del médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Cédula del médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Registro médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MedicalRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Nombre del médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DoctorName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Fecha de cierre IEC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECClosingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Fecha de seguimiento IEC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECMonitoringDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Modalidad de la IEC  1 - Institucional  2 - Domiciliaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECModality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Tipo de IEC  1 - Control  2 - Prioritario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Resultado de la IEC  1 - Efectiva  2 - Fallida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Fecha de realización de IEC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECDoneDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Se realizó intervención epidemiológica de Campo - IEC al caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IECDone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Descripción del evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'EventDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Fecha y hora de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DateOfAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Clasificación según Triage  1 - Triage I  2 - Triage II  3 - Triage III  4 - Triage IV  5 - Triage V', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TriageClassification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Falta ver como le hago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CIE10Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo ARL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ARL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Lesión con definición de accidente de trabajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WorkAccidentInjury';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Tipo de trabajo  1 - Formal  2 - Informal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfJob';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo JSON Escenario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Scenery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Extensión  1 - < o = al 5%  2 - 6 % a 14%  3 - > al 15%', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Extension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Grado  1 - Primer   2 - Segundo   3 - Tercer ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'Grade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Lugar del hecho  1 - Cerrado   2 - Abierto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'LocationOfTheEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo JSON Sitio anatómico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AnatomicalSite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Tipo pólvora  1 - Pirotecnia  2 - Negra  3 - Explosivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'GunpowderType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Causa de lesión por químico  1 - Accidente  2 - Agresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CauseOfChemicalInjury';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo JSON Tipo de quemadura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'BurnType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo ¿Cuál artículo de belleza, cuidado e higiene?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichBeautyItem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo ¿Cuál artículo de consumo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichConsumerItem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo ¿Cuál mecanismo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichMechanism';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Tipo de ahogamiento  1 - Asfixia  2 - Inmersión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfDrowning';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo JSON Mecanismo o elemento que produce lesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MechanismOrElement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sospecha de consumo de otros agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OthersAggressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sospecha de consumo de alcohol agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AlcoholAggressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sospecha de consumo de SPA agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SPAAggressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sospecha de consumo de otros víctima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OthersVictim';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sospecha de consumo de alcohol víctima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AlcoholVictim';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sospecha de consumo de SPA víctima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SPAVictim';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo fue denunciado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WasReported';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sexo del agresor  1 - Hombre  2 - Mujer  3 - Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'SexOfAggressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Presunto agresor  1 - Familiar  2 - Conocido  3 - Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AllegedAggressor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Tipo de maltrato  1 - Violencia común  2 - Delito sexual  3 - Violencia conyugal  4 - Maltrato al menor  5 - Violencia intrafamiliar  6 - Institucional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAbuse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo ¿Cuál? de la sección Tipo de lesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'WhichInjuryOf';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Tipo de profesional del procedimiento estético  1 - Profesional de la salud  2 - Cirujano plástico  3 - Médico esteticista  4 - Médico especialista  5 - Esteticista  6 - Cosmetólogo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAestheticProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo No de procedimientos quirúrgicos simultáneos  1 - 1  2 - 2  3 - 3  4 - Más de 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'NoOfSurgicalProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo JSON Tipo de lesión por estética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypeOfAestheticInjury';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo JSON Tipo de lesión de consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ConsumptionInjuryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Condición del lesionado  1 - Peatón  2 - Pasajero  3 - Conductor  4 - Copiloto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ConditionOfTheInjured';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Tipo de vehículo  1 - Automóvil  2 - Moto  3 - Bicicleta  4 - Taxi  5 - Ambulancia  6 - Bici - taxi  7 - Moto taxi  8 - Camioneta  9 - Bus  10 - Transmilenio  11 - SITP  12 - Vehículo de carga', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'VehicleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Modalidad de transporte  1 - Individual  2 - Colectivo  3 - Masivo  4 - Carga', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TransportMode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Tipo de servicio  1 - Público   2 - Privado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'TypoeOfService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo JSON Lesión de', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'InjuryOf';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Área de ocurrencia  1 - Urbana  3 - Rural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'AreaOfOccurrence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo UPZ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'UPZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Barrio de ocurrencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'NeighborhoodOfOccurrence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Localidad de ocurrencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'LocalityOccurrence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Fecha y hora de ocurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IncidenceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Municipio de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'MunicipalityOfOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Departamento de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'DepartmentOfOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo País de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CountryOfOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Estado civil  1 - Soltero(a)  2 - Casado(a)  3 - Divorciado(a)  4 - Separado(a)  5 - Viudo(a)  6 - Unión libre  7 - Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'CivilStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Nivel educativo.  1 - No fue a la escuela  2 - Preescolar  3 - Primaria incompleta  4 - Primaria completa  5 - Secundaria incompleta  6 - Secundaria completa  7 - Técnico post-secundaria incompleta  8 - Técnico post-secundaria completa  9 - Universidad incompleta  10 - Universidad completa  11 - Postgrado incompleto  12 - Postgrado completo  13 - Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'EducationLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Otro¿Cuál barrio?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'OtherNeighborhood';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA903D', @level2type = N'COLUMN', @level2name = N'ID';

