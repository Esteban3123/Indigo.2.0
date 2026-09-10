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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RecommendPatient].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RecommendPatient].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que fue retirada/cancelada la recomendación. DATETIME, null si aún está vigente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'DateRetirement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha en que fue borrada las recomendaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'DateRetirement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'DateRetirement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la recomendación: 1=Activa, 0=Inactiva/Retirada. BIT, indica vigencia del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'estado en que esta la recomendaciones 1 = activo y 0 = inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que retiró/canceló la recomendación. VARCHAR(10), PII, referencia a INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'ProfessionalRetirement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para saber que profesional fue quien retiro la recomendaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'ProfessionalRetirement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'ProfessionalRetirement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional donde se crea la recomendación. CHAR(10), FK a INUNIFUNC, identifica área/servicio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (sede, clínica, hospital) donde se origina la recomendación. CHAR(10), FK a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de recomendación. DATETIME, marca el momento de generación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la recomendación solicitada al paciente. VARCHAR(500), contenido clínico de la derivación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'Recommendation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'R4ecomendación solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'Recommendation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'Recommendation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica hacia la cual se recomienda remitir al paciente. CHAR(3), FK a INESPECIA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'SpecialtyToRecommend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especialidad a recomendar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'SpecialtyToRecommend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'SpecialtyToRecommend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de prioridad asignada (urgencia, rutina, etc). INT, FK a Admissions.Prioritytype.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'PriorityTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de prioridad (Id de tabla PriorityType)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'PriorityTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'PriorityTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica del profesional que crea la recomendación (primera especialidad registrada). CHAR(3), FK a INESPECIA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la especialidad del profesional que realiza el registro (En el PBI 14084 se deja claro que se debe guardar la primer especialidad del profesional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realiza/crea la recomendación. VARCHAR(25), PII ofuscado, FK a INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que realiza el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención del paciente en el ERP. CHAR(10), FK a ADINGRESO, vincula con el episodio clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento de identidad del paciente (cédula, pasaporte, identificación). VARCHAR(25), PII ofuscado, FK a INPACIENT, clave de búsqueda por paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Documento de identidad del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo del registro de recomendación. INT IDENTITY, clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de recomendaciones de interconsulta o remisión entre especialidades generadas por un profesional de la salud durante un ingreso del paciente. Permite hacer seguimiento a las sugerencias clínicas de derivación, incluyendo prioridad, estado y retiro de la recomendación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecommendPatient';
