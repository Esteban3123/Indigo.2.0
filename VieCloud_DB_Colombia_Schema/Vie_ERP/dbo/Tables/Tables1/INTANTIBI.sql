CREATE TABLE [dbo].[INTANTIBI] (
    [AUTO]          INT                                                                       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO]  INT                                                                       NOT NULL,
    [CODSERIPS]     CHAR (20)                                                                 NOT NULL,
    [NOMUESTRA]     VARCHAR (50)                                                              NOT NULL,
    [NOMMICROR]     VARCHAR (150)                                                             NULL,
    [NOMANTIBI]     VARCHAR (150)                                                             NOT NULL,
    [CMI]           VARCHAR (50)                                                              NOT NULL,
    [RESULTADO]     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)') NULL,
    [NUMMUESTRA]    INT                                                                       NULL,
    [MICROBIOLOGIA] BIT                                                                       NULL,
    [TIPORESULTADO] BIT                                                                       NULL,
    [IDINTERCTRL]   INT                                                                       NULL,
    CONSTRAINT [PK_INTANTIBI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_CODSERIPSs] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_ORDEN_INDIGO] FOREIGN KEY ([ORDEN_INDIGO]) REFERENCES [dbo].[INTERCABE] ([AUTO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INTANTIBI].[RESULTADO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
CREATE NONCLUSTERED INDEX [IX_INTANTIBI_ORDEN_INDIGO]
    ON [dbo].[INTANTIBI]([ORDEN_INDIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de control de interpretación en tabla INTERCTRL; referencia a resultados preliminares de laboratorio/microbiología. INT, FK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'IDINTERCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla INTERCTRL (aplica para los preliminares)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'IDINTERCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'IDINTERCTRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de resultado: 0=Preliminar (XML enviado antes de confirmación), 1=Final (resultado confirmado). BIT, dominio laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor  "0": Indica que el resultado enviado en el XML es preliminar  Valor "1": Indica que el resultado enviado en el XML es final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de estudio microbiológico: 0=No es microbiología (sin resultados preliminares), 1=Es microbiología (maneja preliminares). BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'''''Valor "0": Indica que el estudio no es de microbiología, es decir que no maneja resultados preliminares     ''''Valor "1": Indica que el estudio es de microbiología, es decir que maneja resultados preliminares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de la muestra de laboratorio/examen. INT, identificador de muestra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del antibiograma o análisis microbiológico. VARCHAR(MAX), PII enmascarado como ''''Result_Ofuscado''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración Mínima Inhibitoria; parámetro de sensibilidad antimicrobiana en antibiograma. VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'CMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CMI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'CMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'CMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del antibiótico testeado en antibiograma o cultivo microbiológico. VARCHAR(150).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NOMANTIBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Antibiotico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NOMANTIBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NOMANTIBI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del microorganismo identificado en cultivo/análisis microbiológico. VARCHAR(150), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NOMMICROR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Microorganismo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NOMMICROR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NOMMICROR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la muestra (sangre, orina, esputo, tejido, etc.). VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NOMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NOMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'NOMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Servicio IPS en nomenclatura CUPS/RIPS; referencia a tabla INCUPSIPS. CHAR(20), FK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de orden de laboratorio Indigo: paciente+número de folio; referencia INTERCABE. INT, FK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden Indigo  Paciente concatenado con el numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incremental (IDENTITY) de registro en tabla INTANTIBI. INT, PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'AutoNumerico Identity', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultados de antibiogramas y pruebas de sensibilidad microbiana asociadas a órdenes de laboratorio. Registra para cada muestra analizada el microorganismo identificado, el antibiótico evaluado y el resultado de sensibilidad/resistencia (CMI).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI';
