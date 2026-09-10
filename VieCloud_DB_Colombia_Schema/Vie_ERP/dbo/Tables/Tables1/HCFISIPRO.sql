CREATE TABLE [dbo].[HCFISIPRO] (
    [IPCODPACI] VARCHAR (25)    NOT NULL,
    [NUMINGRES] CHAR (10)       NOT NULL,
    [CODCENATE] CHAR (10)       NOT NULL,
    [UFUCODIGO] CHAR (10)       NOT NULL,
    [CODPRODUC] CHAR (20)       NOT NULL,
    [TIPPRODUC] CHAR (1)        NOT NULL,
    [CANACTPRO] INT             NOT NULL,
    [CANPEDPRO] INT             NOT NULL,
    [CANPENPRO] INT             NOT NULL,
    [TOTPROUNI] NUMERIC (18, 2) NULL,
    [DOSPROACU] NUMERIC (18, 2) NULL,
    [TOTHORACU] INT             NULL,
    [CODUNIMED] CHAR (3)        NULL,
    [INDAUDFOR] NUMERIC (18)    NOT NULL,
    [ID]        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TIPREGIST] TINYINT         CONSTRAINT [DF_HCFISIPRO_TIPREGIST] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_HCFISIPRO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFISIPRO_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCFISIPRO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCFISIPRO_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCFISIPRO_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCFISIPRO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCFISIPRO].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_HCFISIPRO_CODCENATE_UFUCODIGO_CODPRODUC_IPCODPACI_CANACTPRO_NUMINGRES]
    ON [dbo].[HCFISIPRO]([CODCENATE] ASC, [UFUCODIGO] ASC, [CODPRODUC] ASC, [IPCODPACI] ASC, [CANACTPRO] ASC)
    INCLUDE([NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCFISIPRO_CODPRODUC]
    ON [dbo].[HCFISIPRO]([CODPRODUC] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCFISIPRO_NUMINGRES_CANACTPRO_CODPRODUC_IPCODPACI]
    ON [dbo].[HCFISIPRO]([NUMINGRES] ASC)
    INCLUDE([CANACTPRO], [CODPRODUC], [IPCODPACI]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de registro: 0=Farmacia, 1=Central de Mezclas. Clasificación del origen del movimiento de productos en la historia clínica física. TINYINT, default 0.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TIPREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de registro  0: Farmacia  1: Central de Mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TIPREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TIPREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK). Consecutivo de identidad autoincrementable de la tabla HCFISIPRO. INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría. Identificador numérico del registro de auditoría asociado a la transacción de producto. NUMERIC(18), trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad de medida. Referencia a tabla de unidades (mg, ml, unidad, dosis, etc.). CHAR(3), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de horas acumuladas. Sumatoria de horas de uso, aplicación o administración acumuladas del producto en la atención. INT, nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TOTHORACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total de Horas Acumuladas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TOTHORACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TOTHORACU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del producto acumulada. Sumatoria de dosis aplicadas/administradas del medicamento o insumo durante la atención. NUMERIC(18,2), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'DOSPROACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis del producto acumulado en la aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'DOSPROACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'DOSPROACU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del producto por unidad. Costo, cantidad o valor total del producto asignado a la unidad funcional. NUMERIC(18,2), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del producto por unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente por entregar. Productos solicitados sin existencia actual en kardex, en espera de ingreso. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CANPENPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pendiente por Entregar Productos sin Existencia en el Kardex', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CANPENPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CANPENPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad solicitada sin existencia. Medicamentos o insumos pedidos que no tienen stock disponible actualmente en kardex. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Solicitada sin Existencia Actual en el Kardex', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad actual del producto. Stock presente en inventario/kardex del producto en la unidad funcional. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CANACTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Actual Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CANACTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CANACTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de producto: 1=Medicamentos, 2=Materiales e Insumos. Clasificación de la naturaleza del producto dispensado. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Producto  1: Medicamentos  2: Materiales e Insumos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto. Identificador único del medicamento, material o insumo (FK→IHLISTPRO). CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional. Identificador del departamento/servicio/área donde se entrega el producto (FK→INUNIFUNC). CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención. Identificador de la institución/sede/hospital de origen (FK→ADCENATEN). CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso. Identificador del episodio de atención/admisión del paciente (FK→ADINGRESO). CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente. Cédula/documento de identificación/identificador único del paciente (FK→INPACIENT, PII masked). VARCHAR(25), Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de productos o medicamentos asociados a la fisioterapia o historia clínica de un paciente durante un ingreso hospitalario. Guarda las cantidades pedidas, activas y pendientes de cada producto o insumo, junto con dosis acumuladas y horas de tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO';
