CREATE TABLE [dbo].[HCFICHA607N] (
    [ID]                        INT           IDENTITY (1, 1) NOT NULL,
    [IDFICHANOTIFICACION]       INT           NOT NULL,
    [CODDIAGNO]                 VARCHAR (4)   NULL,
    [FamilyMemberName]          VARCHAR (100) NOT NULL,
    [ContactPhone1]             VARCHAR (20)  NOT NULL,
    [ContactPhone2]             VARCHAR (20)  NULL,
    [ArrivalDateColombia]       DATETIME      NULL,
    [HasTravelHistory]          BIT           NOT NULL,
    [TravelCity1]               VARCHAR (100) NULL,
    [TravelState1]              VARCHAR (100) NULL,
    [TravelCountry1]            VARCHAR (100) NULL,
    [TravelDate1]               DATETIME      NULL,
    [TravelCity2]               VARCHAR (100) NULL,
    [TravelState2]              VARCHAR (100) NULL,
    [TravelCountry2]            VARCHAR (100) NULL,
    [TravelDate2]               DATETIME      NULL,
    [TravelCity3]               VARCHAR (100) NULL,
    [TravelState3]              VARCHAR (100) NULL,
    [TravelCountry3]            VARCHAR (100) NULL,
    [TravelDate3]               DATETIME      NULL,
    [TravelCity4]               VARCHAR (100) NULL,
    [TravelState4]              VARCHAR (100) NULL,
    [TravelCountry4]            VARCHAR (100) NULL,
    [TravelDate4]               DATETIME      NULL,
    [TravelCity5]               VARCHAR (100) NULL,
    [TravelState5]              VARCHAR (100) NULL,
    [TravelCountry5]            VARCHAR (100) NULL,
    [TravelDate5]               DATETIME      NULL,
    [HasSignsSymptoms]          BIT           NOT NULL,
    [SelectedSigns]             VARCHAR (MAX) NULL,
    [OtherSigns]                VARCHAR (200) NULL,
    [HasPostSymptomTravel]      BIT           NOT NULL,
    [PostTravelCity1]           VARCHAR (100) NULL,
    [PostTravelState1]          VARCHAR (100) NULL,
    [PostTravelCountry1]        VARCHAR (100) NULL,
    [PostTravelSpecificPlace1]  VARCHAR (100) NULL,
    [PostTravelDate1]           DATETIME      NULL,
    [PostTravelCity2]           VARCHAR (100) NULL,
    [PostTravelState2]          VARCHAR (100) NULL,
    [PostTravelCountry2]        VARCHAR (100) NULL,
    [PostTravelSpecificPlace2]  VARCHAR (100) NULL,
    [PostTravelDate2]           DATETIME      NULL,
    [PostTravelCity3]           VARCHAR (100) NULL,
    [PostTravelState3]          VARCHAR (100) NULL,
    [PostTravelCountry3]        VARCHAR (100) NULL,
    [PostTravelSpecificPlace3]  VARCHAR (100) NULL,
    [PostTravelDate3]           DATETIME      NULL,
    [PostTravelCity4]           VARCHAR (100) NULL,
    [PostTravelState4]          VARCHAR (100) NULL,
    [PostTravelCountry4]        VARCHAR (100) NULL,
    [PostTravelSpecificPlace4]  VARCHAR (100) NULL,
    [PostTravelDate4]           DATETIME      NULL,
    [ContactName1]              VARCHAR (100) NULL,
    [ContactPhone11]            VARCHAR (20)  NULL,
    [ContactPhone12]            VARCHAR (20)  NULL,
    [ContactDate1]              DATETIME      NULL,
    [ContactName2]              VARCHAR (100) NULL,
    [ContactPhone21]            VARCHAR (20)  NULL,
    [ContactPhone22]            VARCHAR (20)  NULL,
    [ContactDate2]              DATETIME      NULL,
    [ContactName3]              VARCHAR (100) NULL,
    [ContactPhone31]            VARCHAR (20)  NULL,
    [ContactPhone32]            VARCHAR (20)  NULL,
    [ContactDate3]              DATETIME      NULL,
    [ContactName4]              VARCHAR (100) NULL,
    [ContactPhone41]            VARCHAR (20)  NULL,
    [ContactPhone42]            VARCHAR (20)  NULL,
    [ContactDate4]              DATETIME      NULL,
    [ContactName5]              VARCHAR (100) NULL,
    [ContactPhone51]            VARCHAR (20)  NULL,
    [ContactPhone52]            VARCHAR (20)  NULL,
    [ContactDate5]              DATETIME      NULL,
    [InterviewDate]             DATE          NOT NULL,
    [InterviewTime]             TIME (0)      NOT NULL,
    [VERSION]                   VARCHAR (20)  NULL,
    [JSON]                      VARCHAR (MAX) NULL,
    [InterviewProfessionalType] INT           NOT NULL,
    CONSTRAINT [PK_HCFICHA607N] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA607N_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA607N_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA607N] NOCHECK CONSTRAINT [CK_HCFICHA607N_JSON];




GO
ALTER TABLE [dbo].[HCFICHA607N] NOCHECK CONSTRAINT [CK_HCFICHA607N_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de profesional de la salud que realiza la entrevista (médico, enfermero, epidemiólogo, etc.), almacenado como enumerado INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'InterviewProfessionalType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el (enum) del tipo de profesional que realiza la entrevista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'InterviewProfessionalType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'InterviewProfessionalType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Payload JSON validado que contiene datos adicionales o nuevas columnas de la ficha de notificación en formato estructurado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación; valor nulo indica primera versión, valores posteriores marcan revisiones o actualizaciones del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora exacta (TIME) de realización de la entrevista epidemiológica o de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'InterviewTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de realización de la entrevista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'InterviewTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'InterviewTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) en que se realizó la entrevista de notificación al paciente o contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'InterviewDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se realizó la entrevista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'InterviewDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'InterviewDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del quinto contacto realizado con el paciente o persona de contacto para seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del contacto con el paciente (Contacto 5).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono secundario del quinto contacto (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone52';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono secundario del contacto 5.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone52';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone52';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono principal del quinto contacto (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone51';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono principal del contacto 5.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone51';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone51';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del quinto contacto o seguimiento realizado (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre completo del contacto 5.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del cuarto contacto realizado con el paciente o persona de contacto para seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del contacto con el paciente (Contacto 4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono secundario del cuarto contacto (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone42';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono secundario del contacto 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone42';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone42';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono principal del cuarto contacto (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone41';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono principal del contacto 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone41';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone41';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del cuarto contacto o seguimiento realizado (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre completo del contacto 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del tercer contacto realizado con el paciente o persona de contacto para seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del contacto con el paciente (Contacto 3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono secundario del tercer contacto (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone32';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono secundario del contacto 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone32';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone32';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono principal del tercer contacto (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone31';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono principal del contacto 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone31';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del tercer contacto o seguimiento realizado (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre completo del contacto 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del segundo contacto realizado con el paciente o persona de contacto para seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del contacto con el paciente (Contacto 2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono secundario del segundo contacto (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono secundario del contacto 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono principal del segundo contacto (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone21';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono principal del contacto 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone21';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del segundo contacto o seguimiento realizado (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre completo del contacto 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del primer contacto realizado con el paciente o persona de contacto para seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del contacto con el paciente (Contacto 1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactDate1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono secundario del primer contacto (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone12';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono secundario del contacto 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone12';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono principal del primer contacto (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono principal del contacto 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del primer contacto o seguimiento realizado (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre completo del contacto 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactName1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del cuarto desplazamiento posterior al inicio de síntomas o diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del desplazamiento posterior 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar específico, establecimiento o institución visitado en el cuarto desplazamiento post-síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar específico visitado en desplazamiento posterior 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País del cuarto desplazamiento realizado después del inicio de síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País del desplazamiento posterior 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento, estado o región del cuarto desplazamiento post-síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o estado del desplazamiento posterior 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad o municipio del cuarto desplazamiento posterior al inicio de síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad del desplazamiento posterior 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del tercer desplazamiento posterior al inicio de síntomas o diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del desplazamiento posterior 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar específico, establecimiento o institución visitado en el tercer desplazamiento post-síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar específico visitado en desplazamiento posterior 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País del tercer desplazamiento realizado después del inicio de síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País del desplazamiento posterior 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento, estado o región del tercer desplazamiento post-síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o estado del desplazamiento posterior 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad o municipio del tercer desplazamiento posterior al inicio de síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad del desplazamiento posterior 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del segundo desplazamiento posterior al inicio de síntomas o diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del desplazamiento posterior 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar específico, establecimiento o institución visitado en el segundo desplazamiento post-síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar específico visitado en desplazamiento posterior 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País del segundo desplazamiento realizado después del inicio de síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País del desplazamiento posterior 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento, estado o región del segundo desplazamiento post-síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o estado del desplazamiento posterior 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad o municipio del segundo desplazamiento posterior al inicio de síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad del desplazamiento posterior 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del primer desplazamiento posterior al inicio de síntomas o diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del desplazamiento posterior 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelDate1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar específico, establecimiento o institución visitado en el primer desplazamiento post-síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar específico visitado en desplazamiento posterior 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelSpecificPlace1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País del primer desplazamiento realizado después del inicio de síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País del desplazamiento posterior 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCountry1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento, estado o región del primer desplazamiento post-síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o estado del desplazamiento posterior 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelState1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad o municipio del primer desplazamiento posterior al inicio de síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad del desplazamiento posterior 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'PostTravelCity1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (1=Sí, 0=No) que refleja si el paciente se desplazó después del inicio de síntomas, relevante para trazabilidad epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'HasPostSymptomTravel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si hubo desplazamientos posteriores al inicio de síntomas (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'HasPostSymptomTravel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'HasPostSymptomTravel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 200) de signos y síntomas adicionales o no clasificados reportados por el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'OtherSigns';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de otros signos y síntomas cuando aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'OtherSigns';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'OtherSigns';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cadena JSON (VARCHAR MAX) que almacena estados booleanos (True/False) de cada signo/síntoma seleccionado en el formulario de notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'SelectedSigns';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena una cadena JSON que contiene los estados booleanos (True/False) de cada signo y síntoma seleccionado en el formulario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'SelectedSigns';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'SelectedSigns';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (1=Sí, 0=No) que especifica si el paciente presenta signos, síntomas o manifestaciones clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'HasSignsSymptoms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el paciente presenta signos y síntomas (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'HasSignsSymptoms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'HasSignsSymptoms';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del quinto viaje o desplazamiento previo realizado antes de la notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del desplazamiento previo 5.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de destino del quinto desplazamiento previo (últimos 21 días).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País del desplazamiento previo 5.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento, estado o provincia del quinto desplazamiento previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o estado del desplazamiento previo 5.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad o municipio del quinto desplazamiento previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad del desplazamiento previo 5.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del cuarto viaje o desplazamiento previo realizado antes de la notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del desplazamiento previo 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de destino del cuarto desplazamiento previo (últimos 21 días).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País del desplazamiento previo 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento, estado o provincia del cuarto desplazamiento previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o estado del desplazamiento previo 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad o municipio del cuarto desplazamiento previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad del desplazamiento previo 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del tercer viaje o desplazamiento previo realizado antes de la notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del desplazamiento previo 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de destino del tercer desplazamiento previo (últimos 21 días).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País del desplazamiento previo 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento, estado o provincia del tercer desplazamiento previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o estado del desplazamiento previo 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad o municipio del tercer desplazamiento previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad del desplazamiento previo 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del segundo viaje o desplazamiento previo realizado antes de la notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del desplazamiento previo 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de destino del segundo desplazamiento previo (últimos 21 días).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País del desplazamiento previo 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento, estado o provincia del segundo desplazamiento previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o estado del desplazamiento previo 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad o municipio del segundo desplazamiento previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad del desplazamiento previo 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del primer viaje o desplazamiento previo realizado antes de la notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del desplazamiento previo 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelDate1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de destino del primer desplazamiento previo (últimos 21 días).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País del desplazamiento previo 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCountry1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento, estado o provincia del primer desplazamiento previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o estado del desplazamiento previo 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelState1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad o municipio del primer desplazamiento previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad del desplazamiento previo 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'TravelCity1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (1=Sí, 0=No) que señala si el paciente viajó en los últimos 21 días, factor epidemiológico relevante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'HasTravelHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el paciente viajó en los últimos 21 días (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'HasTravelHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'HasTravelHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) en que el paciente ingresó o llegó al territorio colombiano, clave para rastreo de procedencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ArrivalDateColombia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que el paciente ingresó a Colombia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ArrivalDateColombia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ArrivalDateColombia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono secundario del familiar o persona de contacto principal (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de teléfono secundario del contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono principal del familiar o persona de contacto (VARCHAR 20), información de contacto PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de teléfono principal del contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ContactPhone1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del familiar, acudiente o persona de contacto principal del paciente (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'FamilyMemberName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del familiar o persona de contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'FamilyMemberName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'FamilyMemberName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CIE-10 u otro clasificador) asociado al caso notificado (VARCHAR 4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de diagnóstico asociado al caso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia HCFICHANOTIFICACION; INT, NOT NULL, vincula el detalles de seguimiento con la ficha madre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clave foránea hacia HCFICHANOTIFICACION.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) y clave primaria del registro de seguimiento de notificación epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha epidemiológica de notificación (formulario 607N) para seguimiento de casos de enfermedad de vigilancia en salud pública. Registra antecedentes de viaje, signos y síntomas, contactos estrechos del caso y datos de la entrevista realizada por el profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607N';
