CREATE TABLE [dbo].[RecommendPatient] (
    [ID]                     INT                                                                              IDENTITY (1, 1) NOT NULL,
    [IPCODPACI]              VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]              CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]              CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODESPECI]              CHAR (3)                                                                         NOT NULL,
    [PriorityTypeId]         INT                                                                              NOT NULL,
    [SpecialtyToRecommend]   CHAR (3)                                                                         NOT NULL,
    [Recommendation]         VARCHAR (500)                                                                    NOT NULL,
    [CreationDate]           DATETIME                                                                         NOT NULL,
    [CODCENATE]              CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]              CHAR (10)                                                                        NOT NULL,
    [ProfessionalRetirement] CHAR (10)                                                                        NULL,
    [Status]                 BIT                                                                              NULL,
    [DateRetirement]         DATETIME                                                                         NULL,
    CONSTRAINT [PK_RecommendPatient] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RecommendPatient_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_RecommendPatient_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_RecommendPatient_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_RecommendPatient_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_RecommendPatient_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_RecommendPatient_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_RecommendPatient_PriorityType] FOREIGN KEY ([PriorityTypeId]) REFERENCES [Admissions].[Prioritytype] ([Id]),
    CONSTRAINT [FK_RecommendPatientSpecialtyToRecommend_INESPECIA] FOREIGN KEY ([SpecialtyToRecommend]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ALTER TABLE [dbo].[RecommendPatient] NOCHECK CONSTRAINT [FK_RecommendPatient_ADCENATEN];


GO
ALTER TABLE [dbo].[RecommendPatient] NOCHECK CONSTRAINT [FK_RecommendPatient_ADINGRESO];


GO
ALTER TABLE [dbo].[RecommendPatient] NOCHECK CONSTRAINT [FK_RecommendPatient_INESPECIA];


GO
ALTER TABLE [dbo].[RecommendPatient] NOCHECK CONSTRAINT [FK_RecommendPatient_INPACIENT];


GO
ALTER TABLE [dbo].[RecommendPatient] NOCHECK CONSTRAINT [FK_RecommendPatient_INPROFSAL];


GO
ALTER TABLE [dbo].[RecommendPatient] NOCHECK CONSTRAINT [FK_RecommendPatient_INUNIFUNC];


GO
ALTER TABLE [dbo].[RecommendPatient] NOCHECK CONSTRAINT [FK_RecommendPatient_PriorityType];


GO
ALTER TABLE [dbo].[RecommendPatient] NOCHECK CONSTRAINT [FK_RecommendPatientSpecialtyToRecommend_INESPECIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha en que fue borrada las recomendaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'DateRetirement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'estado en que esta la recomendaciones 1 = activo y 0 = inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para saber que profesional fue quien retiro la recomendaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'ProfessionalRetirement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'R4ecomendación solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'Recommendation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especialidad a recomendar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'SpecialtyToRecommend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de prioridad (Id de tabla PriorityType)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'PriorityTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la especialidad del profesional que realiza el registro (En el PBI 14084 se deja claro que se debe guardar la primer especialidad del profesional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del profesional que realiza el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Documento de identidad del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'ID';

