CREATE TABLE [Common].[NormativeParametersByDistributionLines] (
    [Id]                      INT          IDENTITY (1, 1) NOT NULL,
    [Code]                    VARCHAR (50) NOT NULL,
    [DistributionLinesId]     INT          NOT NULL,
    [CreditConcept]           TINYINT      NOT NULL,
    [PostMeasurement]         TINYINT      NOT NULL,
    [ResourcesAccountPayable] VARCHAR (50) NOT NULL,
    [PaymentMethod]           VARCHAR (50) NOT NULL,
    [CreationUser]            VARCHAR (20) NOT NULL,
    [CreationDate]            DATETIME     NOT NULL,
    [ModificationUser]        VARCHAR (20) NULL,
    [ModificationDate]        DATETIME     NULL,
    [TimeStamp]               ROWVERSION   NOT NULL,
    CONSTRAINT [PK_NormativeParametersByDistributionLines__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NormativeParametersByDistributionLines_DistributionLinesId] FOREIGN KEY ([DistributionLinesId]) REFERENCES [Common].[DistributionLines] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de parámetros normativos; DATETIME NULL para auditoría', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación; VARCHAR(20) identificador de auditoría', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de parámetros normativos; DATETIME NOT NULL para trazabilidad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro; VARCHAR(20) identificador de auditoría obligatorio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de pago, forma de pago o método de cancelación del crédito; VARCHAR(50) de referencia contable', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'PaymentMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modalidad de Pago', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'PaymentMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'PaymentMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta por pagar (CxP) financiada con recursos; código de cuenta contable VARCHAR(50) para control presupuestario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ResourcesAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CxP Financiados con Recursos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ResourcesAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ResourcesAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición posterior o evaluación posterior del instrumento financiero; TINYINT indicador de estado post-desembolso', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'PostMeasurement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medición Posterior del Instrumento Financiero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'PostMeasurement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'PostMeasurement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de acreencia, tipo o clasificación del crédito adeudado; TINYINT enumerador de conceptos de deuda', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreditConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Acreencia', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreditConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreditConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de líneas de distribución; INT FK referencia a [Common].[DistributionLines] para trazabilidad presupuestaria', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'DistributionLinesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Líneas de Distribución', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'DistributionLinesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'DistributionLinesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de parámetros normativos; VARCHAR(50) identificador alfanumérico para búsqueda y auditoría', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de parametros normativos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario de parámetros normativos; INT IDENTITY clave única del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de parametros normativos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros normativos asociados a líneas de distribución contable: define los conceptos de crédito, métodos de pago, cuentas por pagar y configuraciones de poscalcificación aplicables a cada línea de distribución.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo interna del sistema para control de concurrencia y versionado del registro.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'TimeStamp';
