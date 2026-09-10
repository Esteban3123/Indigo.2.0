CREATE TABLE [dbo].[AGAGEMECE] (
    [CODAUTONU] INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODAUTOAG] INT                                                                           NULL,
    [FECHORAIN] DATETIME                                                                      NOT NULL,
    [FECHORAFI] DATETIME                                                                      NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE] CHAR (10)                                                                     NOT NULL,
    [TIPAGEMED] CHAR (1)                                                                      NOT NULL,
    [CODIGOCON] CHAR (6)                                                                      NULL,
    [CODESPECI] CHAR (3)                                                                      NULL,
    [CODUSUASI] CHAR (20)                                                                     NOT NULL,
    [FECREGSIS] DATETIME                                                                      NOT NULL,
    [CODUSUARI] CHAR (20)                                                                     NOT NULL,
    [FECHELIMI] DATETIME                                                                      NOT NULL,
    [CODCAUCAN] CHAR (3)                                                                      NOT NULL,
    [OBSCAUCAN] NCHAR (100)                                                                   NULL,
    CONSTRAINT [PK_AGAGEMECE] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGAGEMECE_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGAGEMECE_AGCAUCANA] FOREIGN KEY ([CODCAUCAN]) REFERENCES [dbo].[AGCAUCANA] ([CODCAUCAN]),
    CONSTRAINT [FK_AGAGEMECE_AGCONSULT] FOREIGN KEY ([CODIGOCON], [CODCENATE]) REFERENCES [dbo].[AGCONSULT] ([CODIGOCON], [CODCENATE]),
    CONSTRAINT [FK_AGAGEMECE_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AGAGEMECE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AGAGEMECE] NOCHECK CONSTRAINT [FK_AGAGEMECE_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGAGEMECE].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO



GO



GO



GO



GO
ALTER TABLE [dbo].[AGAGEMECE] NOCHECK CONSTRAINT [FK_AGAGEMECE_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o descripción textual de la causa de cancelación de la agenda médica, cita o campaña de salud (NCHAR 100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la observacion de la causa de cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la causa de cancelación de agenda médica, referencia a tabla AGCAUCANA (CHAR 3, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Causa de Cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de eliminación o cancelación del registro de agendamiento en el sistema (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECHELIMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Fecha y hora a la que se elimino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECHELIMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECHELIMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que ejecutó la eliminación o cancelación de la agenda médica en el sistema (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Codigo del Usuario que elimino la Agenda Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro o creación inicial de la agenda médica, cita o campaña en el sistema (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Fecha y hora en que se registro la Agenda Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que asignó u originó el agendamiento médico, cita o campaña de salud (CHAR 20, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Asigno la Agenda Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODUSUASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica asociada al agendamiento, referencia a tabla INESPECIA (CHAR 3, FK nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consultorio o sala médica donde se agenda la cita, referencia a tabla AGCONSULT (CHAR 6, FK nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODIGOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Consultorio Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODIGOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODIGOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de agendamiento: 0=Cita médica, 1=Campaña de salud (CHAR 1, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'TIPAGEMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Agendamiento   0:Cita Medica   1:Campaña de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'TIPAGEMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'TIPAGEMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, institución o sede donde se realiza el agendamiento, referencia a tabla ADCENATEN (CHAR 10, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, especialista) con identificación PII ofuscada, referencia a tabla INPROFSAL (CHAR 20, FK masked)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final o de término del intervalo de tiempo asignado para la agenda médica, cita o campaña (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora final de la Agenda Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial o de inicio del intervalo de tiempo asignado para la agenda médica, cita o campaña (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora inicial de la Agenda Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico de la agenda médica relacionada, referencia cruzada (INT nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODAUTOAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Agenda Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODAUTOAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODAUTOAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único y principal de la tabla AGAGEMECE, clave primaria (INT identity, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de mecanismos de excepción o cancelación en la agenda médica. Guarda los bloques de tiempo excluidos, cancelados o exceptuados en la programación de agendas de profesionales de salud por centro de atención, incluyendo la causa y el usuario responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE';
