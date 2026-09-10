CREATE TABLE [dbo].[AGDEMANDA] (
    [CODAUTONU] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [FECHACITA] DATE                                                                             NOT NULL,
    [MOTIVODEM] CHAR (1)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI] CHAR (3)                                                                         NOT NULL,
    [FECREGSIS] DATETIME                                                                         NOT NULL,
    [ESTADODEM] BIT                                                                              NOT NULL,
    [CODUSUASI] CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_AGDEMANDA] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGEDEMINS_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AGEDEMINS_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGDEMANDA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [FECHACITA]
    ON [dbo].[AGDEMANDA]([FECHACITA] ASC);


GO
ALTER INDEX [FECHACITA]
    ON [dbo].[AGDEMANDA] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IPCODPACI]
    ON [dbo].[AGDEMANDA]([IPCODPACI] ASC);


GO
ALTER INDEX [IPCODPACI]
    ON [dbo].[AGDEMANDA] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario profesional de la salud que asignó la cita médica. VARCHAR(20), puede ser nulo. Referencia al operador del sistema que registró la agendación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Asigno la Cita Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODUSUASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado booleano de la demanda insatisfecha: True=visible en sistema, False=no visible u oculta. BIT, controla visualización del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'ESTADODEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mantiene el Estado de la Demanda   True = Visible   False = No Visible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'ESTADODEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'ESTADODEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro del sistema cuando se creó la demanda. DATETIME, timestamp automático de auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro del Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica solicitada (ej: Cardiología, Pediatría). CHAR(3), FK a INESPECIA. Búsqueda por especialidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, equivalente a identificación/cédula/documento de identidad. VARCHAR(25), PII ofuscado, FK a INPACIENT. Búsqueda por paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de la demanda insatisfecha: 0=Físico, 1=Humano, 2=Financiero, 3=Represamiento. CHAR(1), clasifica causa del incumplimiento de cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'MOTIVODEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de la Demanda Insatisfecha.    0: Fisico  1: Humano  2: Financiero  3: Represamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'MOTIVODEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'MOTIVODEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha deseada de la cita médica que no pudo agendarse. DATE, referencia temporal de la demanda insatisfecha.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'FECHACITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se Deseaba la Cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'FECHACITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'FECHACITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se solicitó la cita. CHAR(10), identifica unidad funcional o institución de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la demanda insatisfecha. INT IDENTITY, clave primaria, secuencial no replicado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de demanda de citas médicas por especialidad. Guarda cada solicitud de agendamiento que un paciente realiza en un centro de atención, incluyendo la fecha deseada, la especialidad requerida y el motivo de la demanda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA';
