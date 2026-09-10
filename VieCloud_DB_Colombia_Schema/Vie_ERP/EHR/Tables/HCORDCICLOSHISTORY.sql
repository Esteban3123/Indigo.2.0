CREATE TABLE [EHR].[HCORDCICLOSHISTORY] (
    [Id]                       INT             IDENTITY (1, 1) NOT NULL,
    [IdHCORDCICLOS]            INT             NOT NULL,
    [SchemesId]                INT             NOT NULL,
    [IDHCORDQUIMIO]            INT             NOT NULL,
    [CICLO]                    INT             NOT NULL,
    [ESTADO]                   INT             NOT NULL,
    [IPCODPACI]                VARCHAR (25)    NOT NULL,
    [NUMINGRES]                CHAR (10)       NOT NULL,
    [NUMEFOLIO]                CHAR (10)       NOT NULL,
    [TALLA]                    NUMERIC (4, 1)  NOT NULL,
    [PESO]                     NUMERIC (18, 3) NOT NULL,
    [IMC]                      NUMERIC (18, 2) NOT NULL,
    [SCT]                      NUMERIC (18, 2) NOT NULL,
    [CODCENATE]                CHAR (10)       NOT NULL,
    [UFUCODIGO]                CHAR (10)       NOT NULL,
    [CODPROSAL]                CHAR (20)       NOT NULL,
    [CODESPECI]                CHAR (3)        NOT NULL,
    [FECHAREGISTRO]            DATETIME        NOT NULL,
    [CODSERIPS]                CHAR (20)       NULL,
    [IDDESCRIPCIONRELACIONADA] INT             NULL,
    [IDHCORDPRON]              INT             NOT NULL,
    [IDHCMOANULB]              CHAR (4)        NULL,
    [OBSERVACIONMOD]           VARCHAR (300)   NULL,
    [Environment]              INT             NULL,
    [ProfessionalModification] CHAR (20)       NULL,
    [DateModification]         DATETIME        NULL,
    [State]                    INT             NOT NULL,
    CONSTRAINT [PK_HCORDCICLOSHISTORY] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_HCMOANULB] FOREIGN KEY ([IDHCMOANULB]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_HCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_HCORDQUIMIO];




GO



GO



GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_HCORDQUIMIO];


GO



GO



GO



GO



GO





GO



GO



GO



GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_HCORDQUIMIO];


GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la cabecera del ciclo oncológico (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado). INT, auditoría de ciclos de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la cabecera del ciclo (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se modifica la cabecera del ciclo oncológico. Auditoría de cambios en esquema de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se modifica la cabecera del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (CHAR 20) que realizó la modificación del ciclo. Trazabilidad de cambios en quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que modifica la cabecera del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de atención del ciclo: 1=Ambulatorio, 2=Hospitalario, 3=Mixta (ambulatoria y hospitalaria). Contexto de prestación del servicio oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'Environment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. Ambulatorio   2. Hospitalario   3.Mixta (Ambulatoria y Hospitalario) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'Environment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'Environment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas (VARCHAR 300) del motivo de modificación del esquema oncológico. Justificación de cambios en quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'OBSERVACIONMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion del motivo de la modificacion del esquema oncologico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'OBSERVACIONMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'OBSERVACIONMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK a HCMOANULB) del motivo de anulación o modificación del esquema oncológico. Razón de cambio: suspensión, ajuste de dosis, retraso, etc.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del motivo de la modificacion del esquema oncologico  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a HCORDPRON) del pronóstico o evaluación clínica asociada al ciclo. Vínculo con evaluación de respuesta terapéutica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Identificador de la tabla HCORDPRON', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de descripción clínica relacionada al servicio prestado. Nota o comentario adicional del ciclo de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id Descripción relacionada del servicio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS (CHAR 20, FK a INCUPSIPS) asociado al esquema oncológico. Procedimiento de quimioterapia en nomenclatura CUPS.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio asociado al esquema oncologico  o Código del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación o registro inicial de la cabecera del ciclo oncológico. Marca temporal de ingreso al sistema.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de registro', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad (CHAR 3, FK a INESPECIA) asociada al ciclo. Oncología u otra especialidad de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (CHAR 20, FK a INPROFSAL) que ordena/ejecuta el ciclo. Médico oncólogo, enfermero u otro.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código del profesional', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10, FK a INUNIFUNC): área, servicio o departamento donde se administra el ciclo oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, FK a ADCENATEN) donde se realiza el ciclo. Institución, clínica o hospital.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código centro de atención', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Superficie Corporal Total (NUMERIC 18,2 en m²). Parámetro anthropométrico para cálculo de dosis en quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'SCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Superficie Corporal Total', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'SCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'SCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de Masa Corporal (NUMERIC 18,2 en kg/m²). Indicador de estado nutricional del paciente al ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IMC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Índice de masa corporal', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IMC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IMC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso corporal (NUMERIC 18,3 en kg) del paciente registrado en el ciclo. Necesario para dosificación de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Peso', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'PESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla o altura (NUMERIC 4,1 en cm) del paciente. Dato antropométrico para cálculo de superficie corporal.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'TALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Talla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'TALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'TALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (CHAR 10) del documento clínico o comprobante asociado al ciclo. Trazabilidad de documentación.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Numero de folio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso (CHAR 10, FK a ADINGRESO) del paciente. Identificador único de la atención/internación.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, FK a INPACIENT): cédula, identificación o documento único de identidad. PII_Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Código paciente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del ciclo: 1=Solicitado sin cita, 2=Solicitado con cita, 3=Ciclo finalizado. Estatus de avance del tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Solicitado sin cita  2 - Solicitado con cita     3 - Ciclo finalizado', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del ciclo (INT) dentro del esquema de quimioterapia. Ej: ciclo 1, 2, 3, etc. de un protocolo oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Numero de ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a HCORDQUIMIO) de la orden de quimioterapia padre. Vínculo con esquema oncológico completo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a EHR.Schemes) del esquema o protocolo oncológico. Referencia a protocolo de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo el Id de los Esquemas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a HCORDCICLOS) de la cabecera original del ciclo. Relación con registro maestro del ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IdHCORDCICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla HCORDCICLOS', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IdHCORDCICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IdHCORDCICLOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT, IDENTITY) de este registro histórico. Clave primaria de auditoría de ciclos.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historial de cambios y versiones anteriores de los ciclos de quimioterapia ordenados para un paciente. Registra cada modificación realizada sobre un ciclo de tratamiento oncológico, conservando los datos clínicos y administrativos vigentes al momento del cambio.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY';
