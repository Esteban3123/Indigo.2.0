CREATE TABLE [dbo].[INTERCTRL] (
    [AUTO]          INT                                                                               IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO]  VARCHAR (20)                                                                      NOT NULL,
    [NUMUESTRA]     TINYINT                                                                           NOT NULL,
    [ESTADOINT]     BIT                                                                               NOT NULL,
    [INTERPRET]     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [AUTOLABOR]     INT                                                                               NOT NULL,
    [FECGENERA]     VARCHAR (20)                                                                      NULL,
    [FECREGIST]     DATETIME                                                                          NULL,
    [FECSERIPS]     VARCHAR (20)                                                                      NULL,
    [CODPROSAL]     CHAR (70) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NULL,
    [MICROBIOLOGIA] BIT                                                                               NULL,
    [TIPORESULTADO] BIT                                                                               NULL,
    CONSTRAINT [PK_INTERCTRL] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INTERCTRL].[INTERPRET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INTERCTRL].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_PARACLINICOS]
    ON [dbo].[INTERCTRL]([AUTOLABOR] ASC)
    INCLUDE([INTERPRET]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERCTRL_7_825822054__K6_K1_K2]
    ON [dbo].[INTERCTRL]([AUTOLABOR] ASC, [AUTO] ASC, [ORDEN_INDIGO] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERCTRL_6_825822054__K4_K6_K8_3_10]
    ON [dbo].[INTERCTRL]([ESTADOINT] ASC, [AUTOLABOR] ASC, [FECREGIST] ASC)
    INCLUDE([CODPROSAL], [NUMUESTRA]);


GO
CREATE NONCLUSTERED INDEX [IX_INTERCTRL_ORDEN_INDIGO_AUTOLABOR_FECREGIST]
    ON [dbo].[INTERCTRL]([ORDEN_INDIGO] ASC, [AUTOLABOR] ASC)
    INCLUDE([FECREGIST]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERCTRL_7_825822054__K2_6]
    ON [dbo].[INTERCTRL]([ORDEN_INDIGO] ASC)
    INCLUDE([AUTOLABOR]);


GO
ALTER INDEX [_dta_index_INTERCTRL_7_825822054__K2_6]
    ON [dbo].[INTERCTRL] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de resultado del examen de laboratorio: 0=Preliminar (provisional), 1=Final (definitivo). Indica el estado de validación del resultado enviado en XML a RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor  "0": Indica que el resultado enviado en el XML es preliminar  Valor "1": Indica que el resultado enviado en el XML es final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de estudio microbiológico: 0=No es microbiología (sin resultados preliminares), 1=Es microbiología (maneja resultados preliminares y progresivos).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'''''Valor "0": Indica que el estudio no es de microbiología, es decir que no maneja resultados preliminares     ''''Valor "1": Indica que el estudio es de microbiología, es decir que maneja resultados preliminares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o nombre del profesional de la salud (laboratorista/médico) responsable de la interpretación del resultado. Contiene datos de identificación del profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Profesional del Resultado de Laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se ejecutó/realizó el servicio de laboratorio en la IPS (centro de atención). Formato texto para compatibilidad RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del resultado de laboratorio en el sistema Indigo Vie Cloud. Marca cuándo la interfaz ingresó el dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro del Resultado de la interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de generación del resultado de la interfaz en el laboratorio. Indica cuándo el analizador o profesional generó el resultado antes de transmitir.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECGENERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Generacion del Resultado de la Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECGENERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'FECGENERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autónumerico) del laboratorio origen o entidad de laboratorio vinculada. Referencia a tabla de laboratorios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de laboratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto de interpretación clínica del resultado de laboratorio (examen, análisis, prueba diagnóstica). Contiene observaciones y conclusiones del laboratorio. Dato sensible ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: 0=No interpretado, 1=Sí está interpretado. Refleja si el resultado tiene análisis o interpretación clínica completada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'ESTADOINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta Interpretado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'ESTADOINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'ESTADOINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de la muestra dentro del orden de laboratorio. Permite rastrear múltiples muestras del mismo paciente en una solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la orden de laboratorio en Indigo Vie Cloud. Concatenación de código de paciente (cédula/documento) con número de folio/secuencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden Indigo  Paciente concatenado con el numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumerico (identificador único interno) de la fila en tabla INTERCTRL. Clave primaria de la interpretación y control de resultados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
CREATE NONCLUSTERED INDEX [IX_INTERCTRL_AUTOLABOR_AUTO_DESC]
    ON [dbo].[INTERCTRL]([AUTOLABOR] ASC, [AUTO] DESC)
    INCLUDE([NUMUESTRA]);


GO
CREATE NONCLUSTERED INDEX [IX_INTERCTRL_AUTOLABOR]
    ON [dbo].[INTERCTRL]([AUTOLABOR] ASC)
    INCLUDE([NUMUESTRA], [AUTO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control e interpretación de resultados de órdenes de laboratorio o exámenes en Indigo. Guarda el estado de procesamiento, la interpretación clínica, fechas clave y el profesional responsable de cada muestra analizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL';
