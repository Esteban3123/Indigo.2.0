CREATE TABLE [dbo].[ODONTOPLANTRATAMIENTOPAC] (
    [ID]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                    CHAR (10)                                                                        NOT NULL,
    [CODUSUARICREACION]            CHAR (20)                                                                        NOT NULL,
    [FECHACREACION]                DATETIME                                                                         NOT NULL,
    [ESTADO]                       INT                                                                              NOT NULL,
    [IDODONTOMOTIVOSNOCUMPLITRATA] INT                                                                              NULL,
    [JUSTIFICACION]                VARCHAR (MAX)                                                                    NULL,
    CONSTRAINT [PK_ODONTOPLANTRATAMIENTOPAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_INPROFSAL] FOREIGN KEY ([CODUSUARICREACION]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPAC_ODONTOMOTIVOSNOCUMPLITRATA] FOREIGN KEY ([IDODONTOMOTIVOSNOCUMPLITRATA]) REFERENCES [dbo].[ODONTOMOTIVOSNOCUMPLITRATA] ([ID])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ODONTOPLANTRATAMIENTOPAC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo (VARCHAR MAX) que explica y justifica el estado del plan, especialmente para no cumplimiento o cambios en tratamiento odontológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'JUSTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'JUSTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'JUSTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del motivo de incumplimiento del tratamiento (INT, NULL); referencia FK a ODONTOMOTIVOSNOCUMPLITRATA; solo se completa si ESTADO=3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'IDODONTOMOTIVOSNOCUMPLITRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ODONTOMOTIVOSNOCUMPLITRATA ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'IDODONTOMOTIVOSNOCUMPLITRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'IDODONTOMOTIVOSNOCUMPLITRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del cumplimiento del plan: 1=En ejecución, 2=Cumplido, 3=No Cumplido; refleja progreso de tratamiento odontológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - En ejecución  2 - Cumplido  3 - No Cumplido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del plan de tratamiento odontológico (DATETIME); marca temporal del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del plan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (odontólogo) que creó el plan de tratamiento (CHAR 20); referencia FK a INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'CODUSUARICREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que creó el plan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'CODUSUARICREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'CODUSUARICREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención del paciente (CHAR 10); referencia FK a ADINGRESO para vincular el plan al episodio de urgencia o consulta odontológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII ofuscado), equivalente a cédula/documento/identificación del paciente; referencia FK a INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY), consecutivo secuencial de la tabla de planes de tratamiento odontológico por paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Planes de tratamiento odontológico asignados a cada paciente por ingreso. Registra el estado de cumplimiento del plan, el motivo si no se completó y la justificación correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPAC';
