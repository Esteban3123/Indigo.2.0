CREATE TABLE [dbo].[HCORDPARAMPAT] (
    [ID]          INT                                                                                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]   CHAR (9)                                                                           NOT NULL,
    [NUMEFOLIO]   NCHAR (10)                                                                         NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                          NOT NULL,
    [CODCENATE]   CHAR (10)                                                                          NOT NULL,
    [UFUCODIGO]   CHAR (10)                                                                          NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [FECORDMED]   DATETIME                                                                           NOT NULL,
    [CODSERIPS]   CHAR (20)                                                                          NOT NULL,
    [CANSERIPS]   INT                                                                                NOT NULL,
    [HCPARAMBID]  INT                                                                                NOT NULL,
    [INTERPRET]   VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [RESULTADO]   VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [RESULTADOID] INT                                                                                NULL,
    CONSTRAINT [PK_HCORDPARAMPAT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDPARAMPAT_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDPARAMPAT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDPARAMPAT_HCPARAMB] FOREIGN KEY ([HCPARAMBID]) REFERENCES [dbo].[HCPARAMB] ([ID]),
    CONSTRAINT [FK_HCORDPARAMPAT_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDPARAMPAT_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDPARAMPAT_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDPARAMPAT_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPARAMPAT].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPARAMPAT].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPARAMPAT].[INTERPRET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPARAMPAT].[RESULTADO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la relación con valores predefinidos del resultado paraclínico (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'RESULTADOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almecenar el ID de la Relacion. Aplica para Valores Predefinidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'RESULTADOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'RESULTADOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del examen paraclínico, laboratorio o imagen (VARCHAR 4000, ofuscado, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado del Paraclínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretación clínica del resultado paraclínico por profesional de salud (VARCHAR 4000, ofuscado, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretacion de Resultados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con configuración de paráclínicos ambulatorios (INT, FK HCPARAMB)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'HCPARAMBID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacion con la tabla de configuracion de Paraclinicos Ambulatorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'HCPARAMBID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'HCPARAMBID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de servicios RIPS facturados en la orden (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único CUPS-IPS del procedimiento, servicio o examen ordenado (CHAR 20, FK INCUPSIPS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de generación de la orden médica paraclínica (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del profesional de salud solicitante (CHAR 20, PII ofuscado, FK INPROFSAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional o área donde se solicita el paraclínico (CHAR 10, FK INUNIFUNC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o IPS proveedora (CHAR 10, FK ADCENATEN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso hospitalario o atención asociado (CHAR 10, FK ADINGRESO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, identificación PII ofuscada, cédula/documento (VARCHAR 25, FK INPACIENT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de la orden paraclínica en historia clínica (NCHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno del tipo de historia clínica (CHAR 9)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la orden paraclínica con parámetros en historia (INT, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultados e interpretaciones de órdenes médicas parametrizadas por paciente en historia clínica. Registra, para cada orden médica emitida, el servicio solicitado, la cantidad, los parámetros evaluados y los resultados o interpretaciones obtenidos (por ejemplo, resultados de laboratorio, imágenes u otros procedimientos clínicos).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMPAT';
