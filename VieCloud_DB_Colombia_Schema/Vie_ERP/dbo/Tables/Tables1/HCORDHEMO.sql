CREATE TABLE [dbo].[HCORDHEMO] (
    [IDETIPHIS] CHAR (9)                                                                           NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [NUMINGRES] CHAR (10)                                                                          NOT NULL,
    [CODCENATE] CHAR (10)                                                                          NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                          NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [FECORDMED] DATETIME                                                                           NOT NULL,
    [CODPRODUC] TINYINT                                                                            NOT NULL,
    [CANSERIPS] INT                                                                                NOT NULL,
    [OBSSERIPS] CHAR (250)                                                                         NULL,
    [PRISERIPS] CHAR (1)                                                                           NOT NULL,
    [ESTSERIPS] CHAR (1)                                                                           NULL,
    [MANEXTPRO] BIT                                                                                NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')       NOT NULL,
    [INTERPRET] VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT] CHAR (20)                                                                          NULL,
    [NUMFOLINT] NCHAR (10)                                                                         NULL,
    [INDAUDFOR] NUMERIC (18)                                                                       NOT NULL,
    [ESTALEHEM] BIT                                                                                CONSTRAINT [DF_HCORDHEMO_ESTALELAB] DEFAULT ((1)) NOT NULL,
    [FECRECHEM] DATETIME                                                                           NULL,
    [NOMARCHEM] CHAR (250)                                                                         NULL,
    [CONCURRE]  ROWVERSION                                                                         NULL,
    [USURECEXA] CHAR (20)                                                                          NULL,
    [INTERFAZ]  BIT                                                                                NULL,
    CONSTRAINT [PK_HCORDHEMO] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODPRODUC] ASC, [MANEXTPRO] ASC),
    CONSTRAINT [FK_HCORDHEMO_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDHEMO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDHEMO_HCHEMPROD] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[HCHEMPROD] ([CODPRODUC]),
    CONSTRAINT [FK_HCORDHEMO_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDHEMO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDHEMO_INPROSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDHEMO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDHEMO].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDHEMO].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDHEMO].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDHEMO].[INTERPRET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de envío a interfaz externa (1=enviado, 0=no enviado). Controla si la orden de hemoderivados fue transmitida al sistema de interfaces de laboratorio/banco de sangre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'INTERFAZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si la orden se envio o no a la interfaz.  1 - Si envio interfaz  0 - No envio interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'INTERFAZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'INTERFAZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que creó/recibió el registro. Identificador del profesional de salud responsable de la recepción del examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'USURECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario Que Crea El  Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'USURECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'USURECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de concurrencia. Control de versión optimista para auditoría de cambios en la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concurrecia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo generado. Identificador del archivo asociado a la orden de hemoderivados (transfusión, componentes sanguíneos).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NOMARCHEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NOMARCHEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NOMARCHEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/recepción del registro. Marca temporal del ingreso de la orden al sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'FECRECHEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'FECRECHEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'FECRECHEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de alerta de hemoderivados (1=activo, 0=inactivo). Controla si la orden genera notificaciones en el banco de sangre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'ESTALEHEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'ESTALEHEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'ESTALEHEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría. Índice numérico para trazabilidad y seguimiento de cambios en registros de hemoderivados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de interpretación. Referencia al folio donde se registró la interpretación clínica del resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio donde se Interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que interpreta. Identifica al médico responsable de la interpretación del resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Medico que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretación clínica de resultados paraclínicos. Análisis y conclusiones del profesional sobre el resultado de hemoderivados (transfusión, componentes).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretacion de Resultados (paraclinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico principal relacionado. Clasificación ICD de la condición clínica que justifica la orden de hemoderivados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico Principal Relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la orden corresponde a plan de manejo externo (BIT: 1=sí, 0=no). Control para órdenes derivadas a centros externos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Estudio Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del servicio en IPS (1=ordenado, 2=completado, 3=interpretado, 4=sin interfaz). Etapa actual del procesamiento de la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Servicio IPS  1: Ordenado  2: Completado  3: Interpretado  4: Sin Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad de la orden de servicio (1=urgente, 2=rutina). Clasificación que determina tiempo de procesamiento en banco de sangre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del servicio IPS. Notas clínicas adicionales relacionadas con la orden de hemoderivados o transfusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicios solicitados. Número de unidades, componentes o procedimientos de hemoderivados requeridos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de producto hemoderivado (concentrado eritrocitario, plasma, plaquetas, crioprecipitado, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Producto - Hemoderivados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de solicitud de la orden. Momento en que el médico requiere los hemoderivados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que solicita. Identifica al médico ordenante (FK a INPROFSAL). PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional solicitante. Identifica el departamento, servicio o área clínica que ordena los hemoderivados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención. Identifica la institución/sucursal donde se ordena el producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente. Referencia a ADINGRESO, vincula la orden con el episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente. Identificación única (cédula, documento, identificación) del paciente receptor. PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de la orden. Identificador único de la orden de hemoderivados en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del tipo de historia clínica. Clasificador del tipo de registro (hospitalización, urgencia, consulta).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes médicas de hemoderivados (sangre, plasma, plaquetas u otros productos hemoterapéuticos) solicitadas dentro de la historia clínica. Registra qué producto fue ordenado, quién lo ordenó, para qué paciente e ingreso, y el estado de la orden y su recepción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO';
