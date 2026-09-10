CREATE TABLE [Glasses].[GlassesOrderC] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA]          INT           NOT NULL,
    [NUMEFOLIO]            CHAR (10)     NOT NULL,
    [IPCODPACI]            VARCHAR (25)  NOT NULL,
    [NUMINGRES]            CHAR (10)     NOT NULL,
    [CODCENATE]            CHAR (10)     NOT NULL,
    [UFUCODIGO]            CHAR (10)     NOT NULL,
    [CODPROSAL]            CHAR (20)     NOT NULL,
    [CODPRODUC]            CHAR (20)     NOT NULL,
    [CODDIAGNO]            CHAR (4)      NOT NULL,
    [Status]               INT           NOT NULL,
    [OrderDate]            DATETIME      NOT NULL,
    [WayOfUse]             INT           NULL,
    [PupillaryDistance]    VARCHAR (100) NULL,
    [Filters]              VARCHAR (100) NULL,
    [Color]                VARCHAR (100) NULL,
    [Photosensitive]       BIT           NOT NULL,
    [Antiglare]            BIT           NOT NULL,
    [HighIndex]            BIT           NOT NULL,
    [PeriodDuration]       VARCHAR (100) NULL,
    [RelevantClinicalData] VARCHAR (200) NULL,
    [Quantity]             INT           NOT NULL,
    [Extramural]           BIT           NOT NULL,
    [Material]             VARCHAR (100) NULL,
    [GlassesType]          INT           NULL,
    [PrintType]            INT           NULL,
    CONSTRAINT [PK_GlassesOrderC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ADCENATEN_NCODCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADINGRESO_NUMINGRES] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHISPACA_ID] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_IHLISTPRO_CODPRODUC] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_INDIAGNOS_CODDIAGNO] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_INPACIENT_IPCODPACI] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_INPROFSAL_CODPROSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_INUNIFUNC_UFUCODIGO] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de impresión de la orden de lentes: 0=Sin envío (defecto), 1=Refracción objetiva con cicloplejía, 2=Refracción objetiva sin cicloplejía, 3=Refracción subjetiva con cicloplejía, 4=Refracción subjetiva sin cicloplejía. INT, determina método de prescripción.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PrintType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de impresión:   0. No envio nada el medico (valor defecto)  1. Refracción objetiva con ciclopejia,   2. Refracción objetiva sin ciclopejia,   3. Refracción subjetiva con ciclopejia,   4. Refracción subjetiva sin ciclopejia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PrintType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PrintType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de lentes oftálmicos: 1=Monofocal, 2=Bifocal, 3=Progresivo/multifocal. INT, define estructura óptica de la prescripción.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'GlassesType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de lentes: 1. Monofocal, 2. Bifocal, 3. Progresivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'GlassesType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'GlassesType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Material del lente: VARCHAR(100), especifica composición física (plástico, cristal, policarbonato, etc.) del formulario de orden.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Material';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Material del formulario', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Material';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Material';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de manejo extramural: 1=Tratamiento fuera de centro de atención, 0=Atención intramural. Relevant para pacientes en domicilio.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Extramural';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde a manejo extramural', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Extramural';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Extramural';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de lentes ordenados: INT, número de unidades de gafas/espejuelos a dispensar al paciente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos clínicos relevantes para la orden: VARCHAR(200), información médica adicional, contraindicaciones, alergias u observaciones del profesional.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'RelevantClinicalData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Datos clínicos relevantes', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'RelevantClinicalData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'RelevantClinicalData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período de duración/vigencia de la orden: VARCHAR(100), tiempo estimado de uso o validez de la prescripción oftálmica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PeriodDuration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodo de duración', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PeriodDuration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PeriodDuration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de lente de alto índice refractivo: 1=Sí, 0=No. Mejora estética en altas graduaciones.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'HighIndex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Alto índice', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'HighIndex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'HighIndex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de recubrimiento antirreflejo/antireflectante: 1=Sí, 0=No. Reduce reflejos y mejora transmisión de luz.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Antiglare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antirreflejo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Antiglare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Antiglare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de lente fotosensible/fotocromático: 1=Sí, 0=No. Oscurece con luz UV, protege en exteriores.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Photosensitive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fotosensible', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Photosensitive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Photosensitive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color del lente: VARCHAR(100), tonalidad visual (transparente, gris, marrón, ámbar, etc.) del producto oftálmico.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Filtros ópticos especiales: VARCHAR(100), protecciones adicionales (azul, UV, polarizados, etc.) incorporadas en la lente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Filters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Filtros', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Filters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Filters';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distancia interpupilar (DP): VARCHAR(100), medida en mm entre centros de pupilas, crítica para centrado óptico correcto.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Distancia pupilar', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de uso/régimen de uso: INT, 1=Permanente/continuo, 2=Ocupacional/laboral, 3=Ocasional. Define frecuencia de prescripción.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'WayOfUse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma de uso: Lista desplegable de selección única con las siguientes opciones:     1 - Permanente    2 - Ocupacional   3 - Ocasional ', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'WayOfUse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'WayOfUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la orden médica: DATETIME, timestamp de creación/solicitud de lentes por profesional.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'OrderDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del registro de la orden medica', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'OrderDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: INT, 1=Solicitado/Pendiente. Indica etapa procesamiento de la orden de lentes.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro  1 - Solicitado  ', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico oftálmico: CHAR(4), FK→INDIAGNOS.CODDIAGNO. Justificación médica de la prescripción (miopía, hipermetropía, astigmatismo, etc.).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico con el que se justifico la orden medica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto de lente: CHAR(20), FK→IHLISTPRO.CODPRODUC. Identificador único del artículo/marco oftálmico en inventario.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional de la salud ordenante: CHAR(20), FK→INPROFSAL.CODPROSAL. Oftalmólogo/optómetra que emite la prescripción.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional de la salud de la orden medica', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad funcional: CHAR(10), FK→INUNIFUNC.UFUCODIGO. Departamento/servicio oftalmológico donde se generó la orden.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional donde se hizo la orden', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código centro de atención: CHAR(10), FK→ADCENATEN.CODCENATE. Sede/institución donde se registró la orden de lentes.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro atencion donde se hizo la orden medica', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente: CHAR(10), FK→ADINGRESO.NUMINGRES. Identificador de la admisión/atención relacionada.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificación del paciente: VARCHAR(25), FK→INPACIENT.IPCODPACI. Cédula/documento/identificación única del paciente [PII_Ofuscado].', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id paciente', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador relación folio/historia: INT, FK→HCHISPACA.ID. Vínculo con registro maestro de folios y tabla histórica de pacientes.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con el tablero de folios HCHISPACA', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes médicas de gafas (lentes ópticos) prescritas a pacientes. Registra la solicitud de gafas formulada en una historia clínica, incluyendo especificaciones técnicas del lente, diagnóstico asociado y datos del profesional que prescribe.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único de la orden de gafas.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o consecutivo de la orden de gafas, usado para referencia e impresión de la fórmula óptica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
