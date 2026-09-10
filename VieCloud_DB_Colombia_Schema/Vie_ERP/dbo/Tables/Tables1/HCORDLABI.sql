CREATE TABLE [dbo].[HCORDLABI] (
    [AUTO]      INT                                                                                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                           NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [NUMINGRES] CHAR (10)                                                                          NOT NULL,
    [CODCENATE] CHAR (10)                                                                          NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                          NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [FECORDMED] DATETIME                                                                           NOT NULL,
    [CODSERIPS] CHAR (20)                                                                          NOT NULL,
    [CANSERIPS] INT                                                                                NOT NULL,
    [OBSSERIPS] CHAR (250)                                                                         NULL,
    [PRISERIPS] CHAR (1)                                                                           NOT NULL,
    [ESTSERIPS] CHAR (1)                                                                           NULL,
    [MANEXTPRO] BIT                                                                                NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')       NULL,
    [INTERPRET] VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT] CHAR (20)                                                                          NULL,
    [NUMFOLINT] NCHAR (10)                                                                         NULL,
    [SERREAINT] BIT                                                                                NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                       NOT NULL,
    [ESTALELAB] BIT                                                                                NOT NULL,
    [FECRECMUE] DATETIME                                                                           NULL,
    [NOMARCLAB] CHAR (250)                                                                         NULL,
    [CONCURRE]  ROWVERSION                                                                         NULL,
    [USURECMUE] CHAR (20)                                                                          NULL,
    CONSTRAINT [PK_HCORDLABI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDLABI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDLABI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDLABI_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDLABI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDLABI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDLABI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [IX_HCORDLABI_1] UNIQUE NONCLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC, [MANEXTPRO] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABI].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABI].[INTERPRET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABI__IPCODPACI__NUMINGRES__NUMEFOLIO__AUTO]
    ON [dbo].[HCORDLABI]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC, [AUTO] ASC)
    INCLUDE([INTERPRET]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que recolecta la muestra de laboratorio; personal técnico, flebólogo o auxiliar de enfermería responsable de la toma (VARCHAR 20, auditoría PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario Que recolecta la muestra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'USURECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de control de concurrencia; marca de versión para sincronización y bloqueos optimistas en cambios simultáneos de registros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo adjunto, documentación o reporte anexo al resultado de laboratorio; ruta o identificador del archivo digital generado (VARCHAR 250).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de recolección de la muestra de laboratorio; momento en que se tomó el especimen (sangre, orina, tisular, etc.) para análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Recoleccion de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'FECRECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de alerta de laboratorio; indica si existe condición crítica, resultado fuera de rango o hallazgo relevante que requiera acción inmediata (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'ESTALELAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, contador o índice de auditoría forense; trazabilidad de cambios y accesos al registro de orden de laboratorio (NUMERIC 18).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si el servicio (procedimiento IPS) realiza interfaz; integración con sistemas externos, RIPS, laboratorio extramural o interconsulta (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Servicio Realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'SERREAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio donde se registra la interpretación del resultado; referencia a historia clínica donde el profesional documenta el análisis (NCHAR 10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio donde se Interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, patólogo, laboratorista especialista) que interpreta y valida el resultado del examen (CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Medico que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretación clínica de resultados paraclínicos; análisis narrativo, diagnóstico presuntivo, recomendaciones y hallazgos relevantes del laboratorio (VARCHAR 4000, PII enmascarado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretacion de Resultados (paraclinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico principal relacionado; clasificación ICD-10 o códigos diagnósticos que justifican o se asocian con la orden de laboratorio (CHAR 4, PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico Principal Relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el producto/servicio corresponde a plan de manejo externo; servicios derivados a laboratorio externo, interconsulta o proveedor tercerista (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del servicio IPS; ciclo de vida de la orden: 1-Solicitado, 2-Muestra Recolectada, 3-Resultado Entregado, 4-Examen Interpretado, 5-Remitido, 6-Anulado, 7-Extramural, 8-Muestra Parcial (CHAR 1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural  8:Muestra Recolectada Parcialmente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad de la orden de servicio; 1-Urgente (resultado inmediato, patología aguda, emergencia), 2-Rutina (procesamiento estándar) (CHAR 1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del servicio IPS; notas adicionales, restricciones preanalíticas, condiciones especiales de recolección o procesamiento de la muestra (CHAR 250).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicios IPS solicitados; número de veces que se solicita el mismo procedimiento/código en una orden o ingreso (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del procedimiento o servicio (CUPS, SOAT, interno); identificador internacional o local del examen de laboratorio solicitado; FK a INCUPSIPS (CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de solicitud de la orden médica; momento en que el profesional prescribe el examen de laboratorio o procedimiento diagnóstico (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que solicita la orden (médico, especialista, enfermero); ordenante del laboratorio o servicio paraclínico (VARCHAR 25, PII enmascarado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se solicita; servicio, departamento o área clínica (urgencia, hospitalización, consulta externa) que requiere el examen; FK a INUNIFUNC (CHAR 10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o institución prestadora de servicios; identificador de la clínica, hospital o laboratorio donde se realiza la atención; FK a ADCENATEN (CHAR 10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso o admisión del paciente; identificador único del episodio de atención/internación; enlace a historia clínica; FK a ADINGRESO (CHAR 10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente; identificación única del paciente (cédula, pasaporte, documento de identidad) en el sistema de información; FK a INPACIENT; PII enmascarado (VARCHAR 25).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de la historia clínica; secuencial que identifica el documento o evento clínico dentro del expediente del paciente (NCHAR 10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno del tipo de historia clínica; clasificación interna que categoriza el tipo de registro (consulta, ingreso, urgencia, ambulatorio) en el sistema (CHAR 9).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico de la tabla; clave primaria generada automáticamente que asegura unicidad del registro de orden de laboratorio (INT IDENTITY).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes médicas de laboratorio clínico asociadas a un ingreso o atención del paciente. Registra cada servicio de laboratorio solicitado, su estado, prioridad, interpretación de resultados, diagnóstico relacionado y datos de recepción de muestra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI';
