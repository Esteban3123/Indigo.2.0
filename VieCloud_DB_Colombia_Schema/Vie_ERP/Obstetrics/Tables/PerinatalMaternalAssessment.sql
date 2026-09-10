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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Test de Coombs indirecto (prueba de antiglobulina): detecta anticuerpos IgG contra antígenos eritrocitarios. Valores: 1=Sin dato, 2=Positivo (sensibilización), 3=Negativo, 4=Riesgo no evaluado. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'Coombs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Coombs   1 = Sin dato    2 = Positivo    3 =Negativo     4 = Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'Coombs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'Coombs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de sensibilización materna (isoinmunización previa): presencia de anticuerpos contra antígenos fetales. Valores: 1=Sin dato, 2=Sí (sensibilizada), 3=No, 4=Riesgo no evaluado. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'Sensitized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Sensibilizada   1 =  Sin dato    2= Si    3 = No     4 = Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'Sensitized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'Sensitized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Riesgo de isoinmunización (aloinmunización): predicción de incompatibilidad ABO/Rh materno-fetal. Valores: 1=Sí (riesgo presente), 2=No. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'RiskOfIsoimmunization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el  Riesgo de isomunización   1 = Si   2 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'RiskOfIsoimmunization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'RiskOfIsoimmunization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de memoria/registro del factor RH paterno: si la gestante recuerda o tiene documentado el RH del padre. Valores: 1=Sí (recuerda/registrado), 0=No. Tipo BIT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'NotRememberPaternalRH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si recuerda o no el grupo sanguineo y/o el RH materno           1 = Si     0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'NotRememberPaternalRH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'NotRememberPaternalRH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor Rh paterno (positivo/negativo): antígeno D en eritrocitos del padre del feto. Valores: 1=Positivo (+), 2=Negativo (-). Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PaternalRH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el RH paterno    1 = +   2 = -', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PaternalRH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PaternalRH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo ABO paterno: tipo de sangre del padre. Valores: 1=A, 2=B, 3=AB, 4=O. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PaternalBloodGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Grupo sanguíneo paterno    1 =A    2 =B  3 =AB    4 =O', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PaternalBloodGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PaternalBloodGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de memoria/registro del grupo sanguíneo y RH materno: si la gestante recuerda o tiene documentado su grupo y factor. Valores: 1=Sí (recuerda/registrado), 0=No. Tipo BIT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'NotRememberMaternalRH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si recuerda o no el grupo sanguineo y/o el RH materno      1 = Si     0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'NotRememberMaternalRH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'NotRememberMaternalRH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor Rh materno (positivo/negativo): antígeno D en eritrocitos de la madre gestante. Valores: 1=Positivo (+), 2=Negativo (-). Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'MaternalRH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el RH materno   1 = +   2 = -', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'MaternalRH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'MaternalRH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo ABO materno: tipo de sangre de la madre gestante (A, B, AB, O). Valores: 1=A, 2=B, 3=AB, 4=O. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'MaternalBloodGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Grupo sanguíneo materno 1 =A    2 =B  3 =AB    4 =O', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'MaternalBloodGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'MaternalBloodGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría de riesgo psicosocial materno: vulnerabilidad emocional, social, familiar durante gestación. Valores: 1=Alto, 2=Bajo. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PsychosocialRisk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Riesgo psicosocial    1  = Alto   2 = Bajo', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PsychosocialRisk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'PsychosocialRisk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estratificación de riesgo tromboembólico materno: probabilidad de trombosis venosa/pulmonar. Valores: 1=Muy Alto, 2=Alto, 3=Moderado, 4=Bajo. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ThromboembolicRisk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Riesgo tromboembólico  1 = Muy Alto     2 =Alto    3 =Moderado     4 =Bajo', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ThromboembolicRisk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ThromboembolicRisk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría de riesgo obstétrico: complicaciones potenciales del embarazo/parto. Valores: 1=Alto, 2=Bajo. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ObstetricRisk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Riesgo obstétrico   1 = Alto   2 = Bajo', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ObstetricRisk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ObstetricRisk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causal de Interrupción Voluntaria del Embarazo (IVE): justificación legal/médica. Valores: 1=Riesgo para vida/salud materna, 2=Grave malformación fetal, 3=Violencia sexual/incesto/inseminación no consentida. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'CauseOfIVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Causal de IVE     1  =Riesgo para la vida o salud de la mujer      2 =Grave malformación del feto     3 = Violencia sexual, incesto o inseminación artificial no consentida', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'CauseOfIVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'CauseOfIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número ordinal de gestación: cantidad de embarazos previos + actual (G1, G2, G3...). Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la gestación numero   ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha probable de parto (FPP): estimado de fecha de nacimiento basado en edad gestacional. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ProbableDeliveryDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha probable de parto ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ProbableDeliveryDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ProbableDeliveryDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado calculado de edad gestacional: valor en semanas + días con precisión obstétrica. Tipo VARCHAR(200).', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationalAgeResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guardar el resultado calculado para la edad gestacional', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationalAgeResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationalAgeResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de cálculo de edad gestacional: fuente de estimación de edad del feto. Valores: 1=Última menstruación (FUM), 2=Ecografía obstétrica, 3=Altura uterina. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationalAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la edad Edad gestacional por  1= Última menstruación        2= Ecografía obstétrica       3= Altura uterina ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationalAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'GestationalAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Altura uterina (cm): medida de distancia sínfisis-fondo uterino; correlaciona con edad gestacional. Tipo DECIMAL(18,1).', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'UterineHeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la altura uterina  (cm )', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'UterineHeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'UterineHeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas de gestación según ultrasonografía obstétrica: estimación ecográfica de edad fetal. Tipo DECIMAL(18,1).', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'WeeksAccordingToUltrasound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda las Semanas según ecografía', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'WeeksAccordingToUltrasound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'WeeksAccordingToUltrasound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de primer ultrasonido/ecografía obstétrica: estudio prenatal inicial para viabilidad y datación. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'DateFirstUltrasound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  fecha primer ecografia', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'DateFirstUltrasound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'DateFirstUltrasound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confiabilidad de fecha de última menstruación (FUM): si la gestante recuerda con certeza. Valores: 1=Sí (confiable), 0=No (incierta). Tipo BIT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ReliableMenstruationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si esta chequeado el campo confiable     1 = Si     0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ReliableMenstruationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ReliableMenstruationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última menstruación (FUM): primer día del último ciclo menstrual; base para cálculo de edad gestacional. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'DatelastMenstruation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la  fecha de ultima mestruación', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'DatelastMenstruation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'DatelastMenstruation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deseo de continuidad del embarazo: decisión informada de la gestante respecto a proseguir con la gestación. Valores: 1=Sí (desea continuar), 0=No (desea interrumpir). Tipo BIT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ContinueWithTheHug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda ¿Desea continuar con el embarazo?:      1 = Si     0 = No', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ContinueWithTheHug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'ContinueWithTheHug';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del control de valoración materno-perinatal: registro integral de evaluación obstétrica, edad gestacional, factores de riesgo (obstétrico, tromboembólico, psicosocial), grupo sanguíneo materno-paterno, inmunización y decisión de continuidad del embarazo.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el detalle del control de usuario "valoración materno perinatal"', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de valoración materna perinatal.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al registro de valoración materna perinatal al que pertenece este detalle; vincula con el encabezado o versión del formulario de valoración.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'PerinatalMaternalAssessment', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
