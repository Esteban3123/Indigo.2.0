CREATE TABLE [dbo].[HCCTRLIQA] (
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]          CHAR (10)                                                                        NOT NULL,
    [CODCENATE]          CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]          CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]          CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST]          DATETIME                                                                         NOT NULL,
    [NOMLIQADM]          VARCHAR (100)                                                                    NOT NULL,
    [CANLIQADM]          NUMERIC (6, 2)                                                                   NOT NULL,
    [VIAADMINI]          CHAR (40)                                                                        NOT NULL,
    [AutomaticLoadedMix] BIT                                                                              CONSTRAINT [DF_HCCTRLIQA_AutomaticLoadedMix] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_HCCTRLIQA_1] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [FECREGIST] ASC, [NOMLIQADM] ASC),
    CONSTRAINT [FK_HCCTRLIQA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRLIQA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRLIQA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRLIQA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRLIQA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRLIQA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRLIQA]
    ON [dbo].[HCCTRLIQA]([NUMINGRES] ASC, [FECREGIST] ASC, [NOMLIQADM] ASC)
    INCLUDE([CANLIQADM], [CODCENATE], [CODPROSAL], [IPCODPACI], [UFUCODIGO], [VIAADMINI]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de carga automática de mezcla (1=Sí, 0=No). Identifica si el líquido fue cargado automáticamente desde el botón ''''Cargar mezclas'''' en Balance y Líquidos; bloquea edición de registros con valor 1. Tipo: BIT, PII=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'AutomaticLoadedMix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla cargada automáticamente  ( 1:Si    -   0:No )    Identifica si el líquido es cargado desde el boton de cargar mezclas de la barra de botones identificando que la mezcla fue cargada automaticamente, este campo me va a servir para no dejar modificar los registros que aca esten en 1 que es que el liqudio se registro desde esta nueva opción.    Esta logica es para el formulario de Balance y liquidos    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'AutomaticLoadedMix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'AutomaticLoadedMix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la vía de administración del líquido (intravenosa, oral, intramuscular, etc.). Tipo: CHAR(40), referencia a catálogo de vías. Búsqueda: vía de infusión, ruta de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'VIAADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'VIAADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'VIAADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de líquido administrado al paciente, expresada en unidades de volumen (ml, cc, etc.). Tipo: NUMERIC(6,2). Búsqueda: volumen, dosis líquida, balance de fluidos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'CANLIQADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Liquido Administrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'CANLIQADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'CANLIQADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o denominación del líquido administrado (suero fisiológico, Ringer, albumina, etc.). Tipo: VARCHAR(100). Búsqueda: medicamento líquido, solución, infusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'NOMLIQADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Liquido Administrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'NOMLIQADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'NOMLIQADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro del líquido administrado en el sistema. Tipo: DATETIME. Búsqueda: timestamp, auditoría, trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que registró la administración del líquido. FK→INPROFSAL. Tipo: CHAR(20), PII=Ofuscado. Búsqueda: médico, enfermero, profesional sanitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se administró el líquido (urgencias, UCI, quirófano, etc.). FK→INUNIFUNC. Tipo: CHAR(10). Búsqueda: departamento, servicio, área clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (hospital, clínica) donde ocurrió la administración. FK→ADCENATEN. Tipo: CHAR(10). Búsqueda: institución, establecimiento sanitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso hospitalario del paciente. FK→ADINGRESO. Tipo: CHAR(10). Búsqueda: número de admisión, atención, episodio clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (cédula, identificación, documento). FK→INPACIENT. Tipo: VARCHAR(25), PII=Ofuscado. Búsqueda: cédula, número de documento, identificación paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control de liquidación de medicamentos administrados durante una atención clínica. Guarda el detalle de cada ítem liquidado (nombre, cantidad y vía de administración) por paciente, ingreso, profesional de salud y unidad funcional, permitiendo el seguimiento y auditoría del proceso de liquidación de administración de medicamentos en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA';
