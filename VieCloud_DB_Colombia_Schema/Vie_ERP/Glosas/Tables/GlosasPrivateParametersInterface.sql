CREATE TABLE [Glosas].[GlosasPrivateParametersInterface] (
    [Id]                    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId] INT         NULL,
    [YearAccount]           INT         NULL,
    [ConceptPortfolio]      VARCHAR (3) NULL,
    [ConceptDebitNote]      VARCHAR (3) NULL,
    [ConceptCashReceipts]   VARCHAR (3) NULL,
    [ConceptCreditNotes]    VARCHAR (3) NULL,
    [TimeStamp]             ROWVERSION  NOT NULL,
    CONSTRAINT [PK_GlosasPrivateParametersInterface] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosasPrivateParametersInterface_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosasPrivateParametersInterface] NOCHECK CONSTRAINT [FK_GlosasPrivateParametersInterface_GlosasParametersInterface];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) que registra el instante exacto de creación, modificación o auditoría del registro de parámetros de glosa. Utilizado para trazabilidad y control de versiones.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código contable VARCHAR(3) que identifica el concepto o cuenta a utilizar para el registro de notas de crédito (devoluciones, ajustes favorables) en la interfaz de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptCreditNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de las notas credito', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptCreditNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptCreditNotes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código contable VARCHAR(3) que especifica el concepto o cuenta destino para recibos de caja (ingresos en efectivo, pagos recaudados) dentro del proceso de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptCashReceipts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de los recibos de caja', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptCashReceipts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptCashReceipts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código contable VARCHAR(3) que define el concepto o cuenta para notas de débito (cargos adicionales, ajustes desfavorables) en la gestión de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptDebitNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de la nota debito', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptDebitNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptDebitNote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código contable VARCHAR(3) que especifica la cuenta contable principal donde se registran todas las glosas recepcionadas, ajustes, recuperos y movimientos asociados.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptPortfolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable en donde se va a registrar todas las glosas recepcionadas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptPortfolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptPortfolio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año fiscal o período contable (INT) asociado a los parámetros de interfaz de glosas, para segregar registros por ejercicio contable.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'YearAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año de las cuentas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'YearAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'YearAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) que vincula con la tabla GlosasParametersInterface [Id], referenciando la configuración matriz de parámetros de la interfaz de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de los parametros de interfaz', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autoincrementable (INT IDENTITY) de la tabla. Clave primaria que identifica cada conjunto de parámetros privados de interfaz de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración de la interfaz de glosas para entidades privadas, asociados a un año contable específico. Define los conceptos contables utilizados en la conciliación de cartera, notas débito, recaudos y notas crédito dentro del módulo de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface';
