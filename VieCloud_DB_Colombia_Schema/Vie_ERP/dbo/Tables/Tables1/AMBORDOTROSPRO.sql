CREATE TABLE [dbo].[AMBORDOTROSPRO] (
    [ID]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCITA]                   INT                                                                              NULL,
    [ESTADO]                   INT                                                                              NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [FECHAREG]                 DATETIME                                                                         NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [CANTIDAD]                 INT                                                                              NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [GENCAREGROUP]             INT                                                                              NULL,
    [GENCONENTITY]             INT                                                                              NULL,
    [GENINVOICE]               VARCHAR (20)                                                                     NULL,
    [GENINVOICEID]             INT                                                                              NULL,
    [GENSERVICEORDER]          INT                                                                              NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODESPECI]                CHAR (3)                                                                         NULL,
    [CODCENATEPRO]             CHAR (10)                                                                        NULL,
    [UFUCODIGOPRO]             CHAR (10)                                                                        NULL,
    [CODUSUPRO]                CHAR (20)                                                                        NULL,
    [FECHAPRO]                 DATETIME                                                                         NULL,
    [INTERPRETACION]           VARCHAR (4000)                                                                   NULL,
    [CODPROSALINT]             CHAR (20)                                                                        NULL,
    [NUMEFOLIOINT]             CHAR (10)                                                                        NULL,
    [MEDICOREALI]              CHAR (20)                                                                        NULL,
    [CODESPREALI]              CHAR (3)                                                                         NULL,
    [FECHAREALI]               DATETIME                                                                         NULL,
    [CORRELACION]              TINYINT                                                                          NULL,
    [OBSERVACIONCORRELA]       VARCHAR (4000)                                                                   NULL,
    [OBSERVACION]              VARCHAR (4000)                                                                   NULL,
    [IDAREAPRO]                INT                                                                              NULL,
    [NOMARCPAT]                VARCHAR (250)                                                                    NULL,
    [PRILLAMADO]               BIT                                                                              CONSTRAINT [DF_AMBORDOTROSPRO_PRILLAMADO] DEFAULT ((0)) NULL,
    [SEGLLAMADO]               BIT                                                                              CONSTRAINT [DF_AMBORDOTROSPRO_SEGLLAMADO] DEFAULT ((0)) NULL,
    [TERLLAMADO]               BIT                                                                              CONSTRAINT [DF_AMBORDOTROSPRO_TERLLAMADO] DEFAULT ((0)) NULL,
    [FECPRILLAMADO]            DATETIME                                                                         NULL,
    [FECSEGILLAMADO]           DATETIME                                                                         NULL,
    [FECTERLLAMADO]            DATETIME                                                                         NULL,
    [PROFPRIMERLLAMADO]        CHAR (20)                                                                        NULL,
    [PROFSEGLLAMADO]           CHAR (20)                                                                        NULL,
    [PROFTERLLAMADO]           CHAR (20)                                                                        NULL,
    [OBVAUSENT]                VARCHAR (MAX)                                                                    NULL,
    [PROAUSENT]                CHAR (20)                                                                        NULL,
    [FECAUSENT]                DATETIME                                                                         NULL,
    [AbsenceReasonCode]        CHAR (4)                                                                         NULL,
    CONSTRAINT [PK_AMBORDOTROSPRO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AMBORDOTROSPRO_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AMBORDOTROSPRO_ADcenaten2] FOREIGN KEY ([CODCENATEPRO]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AMBORDOTROSPRO_Area] FOREIGN KEY ([IDAREAPRO]) REFERENCES [dbo].[HCAREASC] ([ID]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INESPECIA2] FOREIGN KEY ([CODESPREALI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AMBORDOTROSPRO_Ingreso] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INPROFSAL2] FOREIGN KEY ([CODPROSALINT]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INPROFSAL3] FOREIGN KEY ([MEDICOREALI]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AMBORDOTROSPRO_UnidadFuncional] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_AMBORDOTROSPRO_UnidadFuncional2] FOREIGN KEY ([UFUCODIGOPRO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDOTROSPRO].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDOTROSPRO].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de anulación (tabla HCMOANULB). PBI 26684: validación de servicios eliminados en Nota de otros procedimientos. Tipo CHAR(4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'AbsenceReasonCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del motivo de anulacion (codigo de la tabla HCMOANULB) -->23-05-2025 - PBI 26684 Modificación a la validación de los servicios eliminados en la "Nota de otros procedimientos" <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'AbsenceReasonCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'AbsenceReasonCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se registra al paciente como ausente a la cita o procedimiento. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que se marca como ausente el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que registra la ausencia del paciente. Tipo CHAR(20). FK: INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional que ausenta al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o motivo registrado al marcar paciente como ausente. Tipo VARCHAR(MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'OBVAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion al ausentar el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'OBVAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'OBVAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realiza el tercer llamado al paciente. Tipo CHAR(20). FK: INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROFTERLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional del tercer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROFTERLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROFTERLLAMADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realiza el segundo llamado al paciente. Tipo CHAR(20). FK: INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROFSEGLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional del segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROFSEGLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROFSEGLLAMADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realiza el primer llamado al paciente. Tipo CHAR(20). FK: INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROFPRIMERLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional del primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROFPRIMERLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PROFPRIMERLLAMADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del tercer llamado al paciente. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECTERLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha tercer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECTERLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECTERLLAMADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del segundo llamado al paciente. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECSEGILLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECSEGILLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECSEGILLAMADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del primer llamado al paciente. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECPRILLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECPRILLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECPRILLAMADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: tercer intento de contacto/llamado al paciente. Tipo BIT (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'TERLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'TERLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'TERLLAMADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: segundo intento de contacto/llamado al paciente. Tipo BIT (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'SEGLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'SEGLLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'SEGLLAMADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: primer intento de contacto/llamado al paciente. Tipo BIT (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PRILLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PRILLAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'PRILLAMADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o ruta del archivo de documentación del procedimiento/patología. Tipo VARCHAR(250).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'NOMARCPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'NOMARCPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'NOMARCPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del área clínica que procesa/ejecuta el otro procedimiento. Tipo INT. FK: HCAREASC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IDAREAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID area de otro procedimiento que procesa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IDAREAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IDAREAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas y observaciones adicionales sobre la orden de procedimiento. Tipo VARCHAR(4000).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas sobre la correlación o relación entre procedimientos. Tipo VARCHAR(4000).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación correlación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de correlación: 1=Sí, 2=No, 3=Sin especificar. Tipo TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correlación  Determina si se maneja correlación :   1-Si   2-No   3-Sin especificar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CORRELACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se realiza/ejecuta el procedimiento. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECHAREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha realiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECHAREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECHAREALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad del profesional que ejecuta el procedimiento. Tipo CHAR(3). FK: INESPECIA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODESPREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo especialidad realiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODESPREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODESPREALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realiza/ejecuta el procedimiento. Tipo CHAR(20). FK: INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'MEDICOREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo medico realiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'MEDICOREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'MEDICOREALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio donde se registra la interpretación del procedimiento. Tipo CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIOINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio en el que se interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIOINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIOINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que interpreta el procedimiento/examen. Tipo CHAR(20). FK: INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODPROSALINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Profesional interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODPROSALINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODPROSALINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis, conclusiones e interpretación clínica del procedimiento realizado. Tipo VARCHAR(4000).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'INTERPRETACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'INTERPRETACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'INTERPRETACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se procesa/documenta el procedimiento en el sistema. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECHAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se procesa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECHAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECHAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que procesa el procedimiento. Tipo CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODUSUPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que procesa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODUSUPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODUSUPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Unidad Funcional desde la que se procesa el procedimiento (generado desde dashboard). Tipo CHAR(10). FK: INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'UFUCODIGOPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unidad Funcional desde el que se procesa. Este dato se genera desde el dashboard otros procedimientos cuando se procese', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'UFUCODIGOPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'UFUCODIGOPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención desde el que se procesa el procedimiento (generado desde dashboard). Tipo CHAR(10). FK: ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODCENATEPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion desde el que se procesa. Este dato se genera desde el dashboard otros procedimientos cuando se procese', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODCENATEPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODCENATEPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad del profesional que genera la orden de procedimiento. Tipo CHAR(3). FK: INESPECIA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Especialidad del profesional que genera la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que genera la orden de servicio (PII ofuscado). Tipo CHAR(20). FK: INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo Profesional con el que se genera la orden de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la orden de servicio generada en Indigo Vie tras procesar el procedimiento. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Imagenologia, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID interno de la factura en Indigo Vie con la que se facturó el servicio. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura en Indigo Vie con la que se facturó el servicio. Tipo VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENINVOICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Numero de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENINVOICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENINVOICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la Entidad Administradora de Salud (EPS) del contrato en VIE (se llena si grupo EAPB Sin Contrato). Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la Entidad Administradora de salud del contrato, este campo solo se llena si el Grupo de atencion que seleccione es de EAPB Sin Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del grupo de atención en base de datos VIE ERP. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del grupo de atencion de la base de datos de VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de descripción de relación con VIE ERP (contract.CUPSEntityContractDescriptions). Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de unidades del servicio/procedimiento solicitado. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (Catálogo de Procedimientos y Servicios en Salud) del procedimiento. Tipo CHAR(20). FK: INCUPSIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro de la orden de procedimiento en el sistema. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECHAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECHAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'FECHAREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Unidad Funcional donde se solicita el procedimiento. Tipo CHAR(10). FK: INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad funcional de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención donde se solicita el procedimiento. Tipo CHAR(10). FK: ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de atencion de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del paciente (cédula, documento, pasaporte). Tipo VARCHAR(25) PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención del paciente al que pertenece el procedimiento. Tipo CHAR(10). FK: ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del procedimiento: 1=Ordenado, 2=Realizado/Completado, 3=Interpretado, 4=Sin Interfaz, 5=Anulado/Ausente. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:   
1: Ordenado  
2: Realizado (Completado)  
3: Interpretado  
4: Sin Interfaz  
5: Anulado ó Ausente  
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la cita que genera/origina el registro del procedimiento. Tipo INT. FK: AGASICITA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cita que genera el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) del registro de procedimiento. Tipo INT, clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes ambulatorias de otros procedimientos (exámenes, imágenes, procedimientos diagnósticos o terapéuticos) solicitados durante una atención ambulatoria. Registra la solicitud, el profesional responsable, el estado de ejecución, la interpretación del resultado y el control de llamados al paciente cuando no asiste a su cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO';
