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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de Pago', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'PaymentMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CxP Financiados con Recursos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'ResourcesAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición Posterior del Instrumento Financiero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'PostMeasurement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de Acreencia', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'CreditConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Líneas de Distribución', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'DistributionLinesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de parametros normativos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de parametros normativos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'NormativeParametersByDistributionLines', @level2type = N'COLUMN', @level2name = N'Id';

