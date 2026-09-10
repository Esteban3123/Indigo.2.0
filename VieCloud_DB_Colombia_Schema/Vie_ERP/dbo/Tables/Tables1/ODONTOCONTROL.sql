CREATE TABLE [dbo].[ODONTOCONTROL] (
    [ID]                    INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]             CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]             CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]             VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]             CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]             NCHAR (10)                                                                       NOT NULL,
    [CODPROSAL]             CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHAREG]              DATETIME                                                                         NOT NULL,
    [ESURGENCIA]            BIT                                                                              NOT NULL,
    [OBSERVACIONESGENERA]   VARCHAR (MAX)                                                                    NULL,
    [IDRIASCUPS]            INT                                                                              NULL,
    [screenShotValoracion]  VARBINARY (MAX)                                                                  NULL,
    [screenShotTratamiento] VARBINARY (MAX)                                                                  NULL,
    CONSTRAINT [PK_ODONTOCONTROL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOCONTROL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ODONTOCONTROL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ODONTOCONTROL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ODONTOCONTROL_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOCONTROL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_ODONTOCONTROL_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ODONTOCONTROL].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ODONTOCONTROL].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_ODONTOCONTROL_1]
    ON [dbo].[ODONTOCONTROL]([IPCODPACI] ASC);


GO
ALTER INDEX [IX_ODONTOCONTROL_1]
    ON [dbo].[ODONTOCONTROL] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Captura de pantalla (VARBINARY) del plan de tratamiento odontológico registrado en la consulta; evidencia visual del tratamiento propuesto o ejecutado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'screenShotTratamiento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la captura de pantalla tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'screenShotTratamiento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'screenShotTratamiento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Captura de pantalla (VARBINARY) de la valoración odontológica inicial; evidencia visual del diagnóstico y evaluación clínica dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'screenShotValoracion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la captura de pantalla valoración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'screenShotValoracion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'screenShotValoracion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK → RIASCUPS.ID) que relaciona el control con la agenda médica RIAS/CUPS; NULL si la actividad no aplica o no tiene programación RIAS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo en la cual me relaciona el ID de la RIASCUPS que tiene la agenda medica, si la actividad de agendamiento No tiene ó no aplica a RIAS pues guarda Dbnull.Value.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto (VARCHAR MAX) con anotaciones clínicas del odontólogo sobre el odontograma completo, hallazgos y recomendaciones del control dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'OBSERVACIONESGENERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la observaciones que hizo el medico por todo el odontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'OBSERVACIONESGENERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'OBSERVACIONESGENERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 1=urgencia odontológica, 0=consulta programada; marca prioridad de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'ESURGENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿es urgencia? (0. no, 1. si)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'ESURGENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'ESURGENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de control odontológico en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'FECHAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'FECHAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'FECHAREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 20, PII ofuscado, FK → INPROFSAL) del profesional de la salud (odontólogo) que registra el control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo profesional que registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (NCHAR 10) de la historia clínica (HC) asociada al control odontológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de folio HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso (CHAR 10, FK → ADINGRESO) que identifica la atención o consulta en la que se realiza el control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación (VARCHAR 25, PII ofuscado, FK → INPACIENT) del paciente; equivalente a cédula, documento o número de identificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 10, FK → INUNIFUNC) de la unidad funcional (servicio, departamento odontológico) donde se realiza el control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'relacion con unidades funcionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 10, FK → ADCENATEN) del centro de atención, clínica o sede donde se registra el control odontológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK IDENTITY) del registro de control odontológico en la tabla ODONTOCONTROL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de  la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de controles y consultas odontológicas: guarda cada atención odontológica realizada a un paciente durante un ingreso, incluyendo el profesional tratante, la fecha, si fue urgencia, observaciones clínicas generales y capturas de pantalla del tratamiento y la valoración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL';
