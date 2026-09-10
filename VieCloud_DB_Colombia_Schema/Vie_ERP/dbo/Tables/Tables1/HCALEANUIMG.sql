CREATE TABLE [dbo].[HCALEANUIMG] (
    [CODCONSEC] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [CODMOTANU] CHAR (4)                                                                         NOT NULL,
    [OBSERVACI] CHAR (80)                                                                        NULL,
    [FECANULE]  DATETIME                                                                         NOT NULL,
    [USUANUALE] CHAR (20)                                                                        NOT NULL,
    CONSTRAINT [PK_HCALEANUIMG] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCALEANUIMG_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCALEANUIMG_HCMOANULB] FOREIGN KEY ([CODMOTANU]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCALEANUIMG_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCALEANUIMG].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que anula la alerta de imagen; profesional de salud, identificación del operador que registra la anulación. (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'USUANUALE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que Anula la alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'USUANUALE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'USUANUALE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de anulación de la alerta de imagen; timestamp del evento de cancelación. (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'FECANULE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Anulacion de la alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'FECANULE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'FECANULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas o comentarios adicionales sobre la anulación de la alerta de imagen. (CHAR 80)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de anulación de la alerta de imagen; referencia a tabla HCMOANULB con razón de cancelación. (CHAR 4, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Motivo de Anulacion de la alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de procedimiento o servicio (CUPS/RIPS); identificador del servicio o procedimiento relacionado. (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional; área, departamento o sección del centro de atención. (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención; identificador de la clínica, hospital o sede. (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso, atención o episodio de paciente; referencia a tabla ADINGRESO. (CHAR 10, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente; cédula, identificación o documento del usuario (PII ofuscado). (VARCHAR 25, FK, Identification_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio; identificador único del registro o expediente de la alerta de imagen. (NCHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno; identificador único autoincrementado del registro en la tabla. (INT, PK IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de anulaciones de imágenes diagnósticas (o procedimientos de imagenología) en la historia clínica. Guarda qué servicio fue anulado, por quién, cuándo y el motivo de la anulación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANUIMG';
