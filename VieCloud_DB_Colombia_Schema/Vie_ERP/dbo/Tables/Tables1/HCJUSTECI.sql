CREATE TABLE [dbo].[HCJUSTECI] (
    [CODCONCEC] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED] DATETIME                                                                         NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [DESAYUDIA] VARCHAR (MAX)                                                                    NOT NULL,
    [DESJUSTEC] VARCHAR (MAX)                                                                    NOT NULL,
    [DESINCAYU] VARCHAR (MAX)                                                                    NOT NULL,
    CONSTRAINT [PK_HCJUSTECI] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCJUSTECI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCJUSTECI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCJUSTECI_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCJUSTECI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCJUSTECI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCJUSTECI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCJUSTECI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la incidencia o hallazgo en la ayuda diagnóstica solicitada; detalla problemas, limitaciones o eventos adversos durante el examen (laboratorio, imagen, procedimiento diagnóstico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'DESINCAYU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Incidencia de ayuda diagnostica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'DESINCAYU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'DESINCAYU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación técnica y científica de la solicitud; argumentación clínica que respalda la necesidad de la ayuda diagnóstica ordenada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'DESJUSTEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'justificacion tecnica cientifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'DESJUSTEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'DESJUSTEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de las ayudas diagnósticas solicitadas; detalle de exámenes de laboratorio, imagenología, procedimientos diagnósticos o estudios complementarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'DESAYUDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descirpcion de las ayudas diagnosticas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'DESAYUDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'DESAYUDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único CUPS/RIPS del procedimiento o servicio diagnóstico solicitado; identificador estándar del servicio en facturación y reportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios solicitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de solicitud de la orden médica; timestamp de generación de la requisición de ayuda diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que solicita; identificación del médico, especialista u otro profesional que ordena el procedimiento (PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional solicitante; identificador del servicio clínico, área o departamento que requiere la ayuda diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención; identificador de la institución, clínica u hospital donde se ordena el servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente; identificador único del episodio de atención o admisión relacionada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, documento, identificación); identificador único del paciente en el sistema (PII ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio; identificador secuencial del documento o justificación dentro del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno del tipo de historia clínica; clasificación o categoría del registro clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo interno; identificador autonumérico único de la justificación o registro en la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Concecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificaciones de ayudas diagnósticas o terapéuticas ordenadas en la historia clínica. Registra, por cada orden médica, la descripción del servicio solicitado, la justificación clínica y las indicaciones de ayuda, asociando el paciente, el ingreso, el profesional y el centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECI';
