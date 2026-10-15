CREATE TABLE [dbo].[PostpartumCareFollowUp] (
    [ID]                 INT           IDENTITY (1, 1) NOT NULL,
    [IPCODPACI]          VARCHAR (25)  NOT NULL,
    [NUMINGRES]          CHAR (10)     NOT NULL,
    [CODCENATE]          CHAR (10)     NOT NULL,
    [UFUCODIGO]          CHAR (10)     NOT NULL,
    [CODPROSAL]          CHAR (20)     NOT NULL,
    [FECREGSIS]          DATETIME      NOT NULL,
    [RegisterDate]       DATETIME      NOT NULL,
    [ProcedurePerformed] INT           NOT NULL,
    [ObstetricEvent]     VARCHAR (100) NULL,
    [ProcedureDate]      DATETIME      NOT NULL,
    [RedCode]            BIT           NULL,
    [RedCodeHour]        TIME (7)      NULL,
    [FollowUpMinute]     INT           NULL,
    [MomentOfAssessment] TINYINT       NULL,
    CONSTRAINT [PK_PostpartumCareFollowUp] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PostpartumCareFollowUp_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_PostpartumCareFollowUp_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_PostpartumCareFollowUp_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_PostpartumCareFollowUp_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_PostpartumCareFollowUp_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[PostpartumCareFollowUp] NOCHECK CONSTRAINT [FK_PostpartumCareFollowUp_ADCENATEN];


GO
ALTER TABLE [dbo].[PostpartumCareFollowUp] NOCHECK CONSTRAINT [FK_PostpartumCareFollowUp_ADINGRESO];


GO
ALTER TABLE [dbo].[PostpartumCareFollowUp] NOCHECK CONSTRAINT [FK_PostpartumCareFollowUp_INPACIENT];


GO
ALTER TABLE [dbo].[PostpartumCareFollowUp] NOCHECK CONSTRAINT [FK_PostpartumCareFollowUp_INPROFSAL];


GO
ALTER TABLE [dbo].[PostpartumCareFollowUp] NOCHECK CONSTRAINT [FK_PostpartumCareFollowUp_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo Momento de valoración
1 - Inmediato
2 - Vigilancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'MomentOfAssessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Minuto del seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'FollowUpMinute';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora del código rojo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RedCodeHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código rojo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RedCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ProcedureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Evento obstétrico (Campo Cual del formulario)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ObstetricEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Procedimiento realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ProcedurePerformed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'RegisterDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora en la cual se guarda el registro (fecha servidor)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUp', @level2type = N'COLUMN', @level2name = N'ID';

