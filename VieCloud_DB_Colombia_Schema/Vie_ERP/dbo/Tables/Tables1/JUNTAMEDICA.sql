CREATE TABLE [dbo].[JUNTAMEDICA] (
    [ID]         INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]  NCHAR (10)                                                                       NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODESPECIA] CHAR (3)                                                                         NOT NULL,
    [IDMODELOHC] INT                                                                              NULL,
    CONSTRAINT [PK_JUNTAMEDICA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_JUNTAMEDICA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_JUNTAMEDICA_INESPECIA] FOREIGN KEY ([CODESPECIA]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_JUNTAMEDICA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_JUNTAMEDICA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_JUNTAMEDICA_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[JUNTAMEDICA] NOCHECK CONSTRAINT [FK_JUNTAMEDICA_INESPECIA];


GO
ALTER TABLE [dbo].[JUNTAMEDICA] NOCHECK CONSTRAINT [FK_JUNTAMEDICA_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[JUNTAMEDICA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[JUNTAMEDICA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del modelo de historia clínica/expediente asociado a la junta médica. Referencia a PRMODELOHC, permite vincular el formato de documentación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID modelo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica del profesional registrado en la junta (3 caracteres). Especialidad clínica del médico. Referencia a INESPECIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'CODESPECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la especialidad del medico con la que se registro en la junta medica. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'CODESPECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'CODESPECIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (PII ofuscado), médico o especialista que participa en la junta médica. Referencia a INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio único del registro de junta médica, comprobante o referencia documental del acta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención del paciente, identificador del episodio de hospitalización o consulta. Referencia a ADINGRESO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (PII ofuscado), cédula/identificación/documento del paciente que participa en la junta médica. Referencia a INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de junta médica en la tabla JUNTAMEDICA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de juntas médicas realizadas a pacientes durante su ingreso. Vincula al paciente, el número de ingreso y el folio de la junta con el profesional de salud y la especialidad participante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA';
