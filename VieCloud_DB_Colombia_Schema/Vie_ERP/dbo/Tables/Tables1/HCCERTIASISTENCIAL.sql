CREATE TABLE [dbo].[HCCERTIASISTENCIAL] (
    [ID]              INT                                                                            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODEPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identication_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                      NOT NULL,
    [TEXTOCERTI]      VARCHAR (MAX)                                                                  NOT NULL,
    [FECHAREGISTRO]   DATETIME                                                                       NOT NULL,
    [USUARIOREGISTO]  CHAR (20)                                                                      NOT NULL,
    [IDCERTIFICADO]   VARCHAR (6)                                                                    NULL,
    [CODESPECIALIDAD] CHAR (3)                                                                       NOT NULL,
    [CODCENATE]       CHAR (10)                                                                      NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                      NOT NULL,
    CONSTRAINT [PK_HCCERTIASISTENCIAL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_HCPLANDOC] FOREIGN KEY ([IDCERTIFICADO]) REFERENCES [dbo].[HCPLANDOC] ([CODCONSEC]),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_INESPECIA] FOREIGN KEY ([CODESPECIALIDAD]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_INPACIENT] FOREIGN KEY ([IPCODEPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCERTIASISTENCIAL] NOCHECK CONSTRAINT [FK_HCCERTIASISTENCIAL_HCPLANDOC];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCERTIASISTENCIAL].[IPCODEPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional (UF) donde se genera el certificado asistencial, referencia a centro organizacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (hospital, clínica, consultorio) donde se emite el certificado asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica del profesional de salud que certifica (ej: medicina general, cirugía, pediatría).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'CODESPECIALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la especialidad del Profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'CODESPECIALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'CODESPECIALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del tipo/plantilla de certificado asistencial asociado al registro (FK HCPLANDOC).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'IDCERTIFICADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del certificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'IDCERTIFICADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'IDCERTIFICADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario/profesional que registra y firma el certificado asistencial en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que hizo el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el usuario registró el certificado asistencial en la base de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha en la que se  registro el certificado el usuario ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto completo del certificado asistencial emitido (contenido médico-legal del documento).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'TEXTOCERTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Texto del Certificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'TEXTOCERTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'TEXTOCERTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención del paciente vinculado al certificado asistencial (FK ADINGRESO).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Ingreso del Paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente (cédula, documento, identificación única), enmascarado PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'IPCODEPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'IPCODEPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'IPCODEPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico del registro de certificado asistencial en la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Certificados de asistencia asistencial emitidos a pacientes durante sus ingresos. Registra el texto del certificado, la especialidad, el centro de atención y la unidad funcional donde fue generado, junto con el usuario y la fecha de emisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL';
