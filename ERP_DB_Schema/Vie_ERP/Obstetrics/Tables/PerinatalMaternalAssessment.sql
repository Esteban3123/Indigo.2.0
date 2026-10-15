CREATE TABLE [Obstetrics].[PerinatalMaternalAssessment] (
    [Id]                             INT             IDENTITY (1, 1) NOT NULL,
    [IdPerinatalMaternalAssessmentC] INT             NOT NULL,
    [ContinueWithTheHug]             BIT             NOT NULL,
    [DatelastMenstruation]           DATETIME        NULL,
    [ReliableMenstruationDate]       BIT             NULL,
    [DateFirstUltrasound]            DATETIME        NULL,
    [WeeksAccordingToUltrasound]     DECIMAL (18, 1) NULL,
    [UterineHeight]                  DECIMAL (18, 1) NULL,
    [GestationalAge]                 INT             NOT NULL,
    [GestationalAgeResult]           VARCHAR (200)   NOT NULL,
    [ProbableDeliveryDate]           DATETIME        NULL,
    [GestationNumber]                INT             NOT NULL,
    [CauseOfIVE]                     INT             NULL,
    [ObstetricRisk]                  INT             NULL,
    [ThromboembolicRisk]             INT             NULL,
    [PsychosocialRisk]               INT             NULL,
    [MaternalBloodGroup]             INT             NULL,
    [MaternalRH]                     INT             NULL,
    [NotRememberMaternalRH]          BIT             NULL,
    [PaternalBloodGroup]             INT             NULL,
    [PaternalRH]                     INT             NULL,
    [NotRememberPaternalRH]          BIT             NULL,
    [RiskOfIsoimmunization]          INT             NULL,
    [Sensitized]                     INT             NULL,
    [Coombs]                         INT             NULL,
    CONSTRAINT [PK_PerinatalMaternalAssessment] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Coombs   1 = Sin dato    2 = Positivo    3 =Negativo     4 = Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'Coombs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Sensibilizada   1 =  Sin dato    2= Si    3 = No     4 = Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'Sensitized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el  Riesgo de isomunización   1 = Si   2 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'RiskOfIsoimmunization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si recuerda o no el grupo sanguineo y/o el RH materno           1 = Si     0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'NotRememberPaternalRH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el RH paterno    1 = +   2 = -', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PaternalRH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Grupo sanguíneo paterno    1 =A    2 =B  3 =AB    4 =O', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PaternalBloodGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si recuerda o no el grupo sanguineo y/o el RH materno      1 = Si     0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'NotRememberMaternalRH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el RH materno   1 = +   2 = -', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'MaternalRH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Grupo sanguíneo materno 1 =A    2 =B  3 =AB    4 =O', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'MaternalBloodGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Riesgo psicosocial    1  = Alto   2 = Bajo', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PsychosocialRisk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Riesgo tromboembólico  1 = Muy Alto     2 =Alto    3 =Moderado     4 =Bajo', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ThromboembolicRisk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Riesgo obstétrico   1 = Alto   2 = Bajo', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ObstetricRisk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Causal de IVE     1  =Riesgo para la vida o salud de la mujer      2 =Grave malformación del feto     3 = Violencia sexual, incesto o inseminación artificial no consentida', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'CauseOfIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la gestación numero   ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha probable de parto ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ProbableDeliveryDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guardar el resultado calculado para la edad gestacional', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationalAgeResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la edad Edad gestacional por  1= Última menstruación        2= Ecografía obstétrica       3= Altura uterina ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationalAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la altura uterina  (cm )', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'UterineHeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda las Semanas según ecografía', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'WeeksAccordingToUltrasound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  fecha primer ecografia', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'DateFirstUltrasound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si esta chequeado el campo confiable     1 = Si     0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ReliableMenstruationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la  fecha de ultima mestruación', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'DatelastMenstruation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda ¿Desea continuar con el embarazo?:      1 = Si     0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ContinueWithTheHug';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el detalle del control de usuario "valoración materno perinatal"', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment';

