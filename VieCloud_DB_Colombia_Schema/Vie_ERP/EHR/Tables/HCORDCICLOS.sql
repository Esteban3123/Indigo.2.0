CREATE TABLE [EHR].[HCORDCICLOS] (
    [Id]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]                INT                                                                              NOT NULL,
    [IDHCORDQUIMIO]            INT                                                                              NOT NULL,
    [CICLO]                    INT                                                                              NOT NULL,
    [ESTADO]                   INT                                                                              NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]                CHAR (10)                                                                        NOT NULL,
    [TALLA]                    NUMERIC (4, 1)                                                                   NOT NULL,
    [PESO]                     NUMERIC (18, 3)                                                                  NOT NULL,
    [IMC]                      NUMERIC (18, 2)                                                                  NOT NULL,
    [SCT]                      NUMERIC (18, 2)                                                                  NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODESPECI]                CHAR (3)                                                                         NOT NULL,
    [FECHAREGISTRO]            DATETIME                                                                         NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [IDHCORDPRON]              INT                                                                              NOT NULL,
    [IDHCMOANULB]              CHAR (4)                                                                         NULL,
    [OBSERVACIONMOD]           VARCHAR (300)                                                                    NULL,
    [Environment]              INT                                                                              NULL,
    [ProfessionalModification] CHAR (20)                                                                        NULL,
    [DateModification]         DATETIME                                                                         NULL,
    [State]                    INT                                                                              CONSTRAINT [DF_HCORDCICLOS_State] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCORDCICLOS] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HCORDCICLOS_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDCICLOS_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDCICLOS_HCMOANULB] FOREIGN KEY ([IDHCMOANULB]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCORDCICLOS_HCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_HCORDCICLOS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDCICLOS_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCORDCICLOS_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDCICLOS_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDCICLOS_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDCICLOS_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [EHR].[HCORDCICLOS].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [EHR].[HCORDCICLOS].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_HCORDCICLOS_IDHCORDPRON]
    ON [EHR].[HCORDCICLOS]([IDHCORDPRON] ASC);


GO
ALTER INDEX [IX_HCORDCICLOS_IDHCORDPRON]
    ON [EHR].[HCORDCICLOS] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCORDCICLOS_IPCODPACI_CICLO_IDHCORDQUIMIO_NUMEFOLIO_NUMINGRES_SchemesId]
    ON [EHR].[HCORDCICLOS]([IPCODPACI] ASC)
    INCLUDE([CICLO], [IDHCORDQUIMIO], [NUMEFOLIO], [NUMINGRES], [SchemesId]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDCICLOS_IDHCORDQUIMIO_FECHAREGISTRO_NUMEFOLIO]
    ON [EHR].[HCORDCICLOS]([IDHCORDQUIMIO] ASC)
    INCLUDE([FECHAREGISTRO], [NUMEFOLIO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del ciclo oncológico: 1=Origen ordenamiento, 2=Adicionado, 3=Modificado, 4=Eliminado. Auditoría de cambios en cabecera del ciclo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la cabecera del ciclo (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del ciclo oncológico. Timestamp auditoría de cambios en la cabecera del esquema.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se modifica la cabecera del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del profesional de salud que modificó el ciclo. PII Ofuscado. Responsable del cambio en esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que modifica la cabecera del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ambiente de atención: 1=Ambulatorio, 2=Hospitalario, 3=Mixta (Ambulatorio+Hospitalario). Contexto de la quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'Environment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. Ambulatorio   2. Hospitalario   3.Mixta (Ambulatoria y Hospitalario) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'Environment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'Environment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones/motivo de la modificación del esquema oncológico o ciclo. Texto libre justificación de cambios en quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'OBSERVACIONMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion del motivo de la modificacion del esquema oncologico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'OBSERVACIONMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'OBSERVACIONMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del motivo de anulación/modificación del esquema oncológico. Referencia FK a tabla HCMOANULB. Razón del cambio en ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del motivo de la modificacion del esquema oncologico  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla HCORDPRON. Referencia FK al pronóstico u orden relacionada del ciclo oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Identificador de la tabla HCORDPRON', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de descripción relacionada al servicio del ciclo. Vinculación a detalles adicionales del servicio oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id Descripción relacionada del servicio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Servicio IPS (CUPS) asociado al esquema oncológico. PII Ofuscado. Referencia FK INCUPSIPS. Procedimiento quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio asociado al esquema oncologico  o Código del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro inicial del ciclo de quimioterapia en el sistema. Timestamp creación de la cabecera.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de registro', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica del ciclo. Referencia FK INESPECIA. Oncología u otra especialidad tratante.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud responsable. PII Ofuscado. Referencia FK INPROFSAL. Oncólogo/médico tratante.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código del profesional', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional donde se gestiona el ciclo. Referencia FK INUNIFUNC. Centro de oncología, quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/institución. Referencia FK ADCENATEN. Clínica, hospital donde se administra quimio.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código centro de atención', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Superficie Corporal Total en metros cuadrados (m²). Numeric(18,2). Parámetro dosificación quimioterapia oncológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'SCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Superficie Corporal Total', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'SCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'SCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de Masa Corporal del paciente. Numeric(18,2). Indicador estado nutricional en ciclo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IMC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Índice de masa corporal', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IMC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IMC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del paciente en kilogramos. Numeric(18,3). Parámetro dosificación y monitoreo del ciclo oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Peso', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'PESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estatura/talla del paciente en metros. Numeric(4,1). Dato antropométrico para cálculo SCT y dosificación.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'TALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Talla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'TALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'TALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio del ciclo. Char(10). Identificador correlativo administrativo del ciclo en atención.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Numero de folio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención. Char(10). Referencia FK ADINGRESO. Vínculo con el episodio de ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación, documento). PII Ofuscado partial. Referencia FK INPACIENT. Identificación única.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Código paciente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del ciclo: 1=Solicitado sin cita, 2=Solicitado con cita, 3=Ciclo finalizado. Estado administrativo/clínico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Solicitado sin cita  2 - Solicitado con cita     3 - Ciclo finalizado', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del ciclo de quimioterapia. Integer. Identifica orden del ciclo dentro del esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Numero de ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de quimioterapia. Integer. Referencia FK EHR.HCORDQUIMIO. Vínculo con orden maestra.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del esquema oncológico padre. Integer. Referencia FK EHR.Schemes. Pertenencia a esquema de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo el Id de los Esquemas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la cabecera del ciclo. Integer Identity(1,1). PK HCORDCICLOS. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciclos de quimioterapia ordenados a pacientes. Registra cada ciclo de un esquema de tratamiento oncológico, incluyendo datos clínicos del paciente al momento del ciclo (talla, peso, IMC, superficie corporal), el profesional que lo prescribe, el centro y unidad de atención, y el estado de cada ciclo dentro del protocolo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOS';
