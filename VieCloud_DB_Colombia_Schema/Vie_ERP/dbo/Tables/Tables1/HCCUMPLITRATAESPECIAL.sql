CREATE TABLE [dbo].[HCCUMPLITRATAESPECIAL] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDHCORDPRON]   INT                                                                              NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODESPECI]     CHAR (3)                                                                         NOT NULL,
    [FECHAREGISTRO] DATETIME                                                                         NOT NULL,
    [IDCITA]        INT                                                                              NULL,
    [TIPOTRATA]     INT                                                                              CONSTRAINT [DF_HCCUMPLITRATAESPECIAL_TIPOTRATA] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCCUMPLIMIENTOQUIMIO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_HCCUMPLIMIENTOQUIMIO] FOREIGN KEY ([IDHCORDPRON]) REFERENCES [dbo].[HCORDPRON] ([AUTO]),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCCUMPLITRATAESPECIAL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCCUMPLITRATAESPECIAL] NOCHECK CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_HCCUMPLIMIENTOQUIMIO];


GO
ALTER TABLE [dbo].[HCCUMPLITRATAESPECIAL] NOCHECK CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCUMPLITRATAESPECIAL].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCUMPLITRATAESPECIAL].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tratamiento oncológico especial: 1=Quimioterapia, 2=Radioterapia, 3=Braquiterapia. Clasificación del procedimiento terapéutico aplicado al paciente (INT, default=1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Tratamiento Especial.   1 - Quimioterapia   2 - Radioterapia   3 - Braquiterapia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo de la cita médica en la cual se realizó la aplicación del tratamiento especial (INT, FK a IDCITA).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la cita con el que se realizo la aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del cumplimiento del tratamiento especial en el sistema (DATETIME, auditora).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica relacionada con el tratamiento especial (ej: Oncología). FK a INESPECIA (CHAR 3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico oncólogo, radiólogo) que aplicó o supervisó el tratamiento. PII ofuscado. FK a INPROFSAL (CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se aplicó el tratamiento especial (consulta, radioterapia, quimioterapia). FK a INUNIFUNC (CHAR 10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se registra el cumplimiento del tratamiento especial. FK a ADCENATEN (CHAR 10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención del paciente en el cual se ejecutó el tratamiento. FK a ADINGRESO (CHAR 10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con la tabla HCORDPRON (órdenes/procedimientos no quirúrgicos, ej: prescripción de quimio/radio). FK a HCORDPRON.AUTO (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de los procedimientos NoQx HCORDPRON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (cédula, identificación, documento de identidad). PII ofuscado. FK a INPACIENT (VARCHAR 25).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (consecutivo/secuencia) del registro de cumplimiento de tratamiento especial (INT IDENTITY, PK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del cumplimiento de tratamientos especiales ordenados en la historia clínica. Guarda cada vez que un profesional de la salud ejecuta o completa un tratamiento especial indicado en una orden pronóstico, asociado a un ingreso y una cita del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL';
