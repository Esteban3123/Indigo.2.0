CREATE TABLE [dbo].[IndicatorValue] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IndicatorId]   INT             NOT NULL,
    [IndicatorDate] DATE            NOT NULL,
    [Numerator]     NUMERIC (18, 2) NOT NULL,
    [Denominator]   NUMERIC (18, 2) NOT NULL,
    [Result]        NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_IndicatorValue__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IndicatorValue_Indicator] FOREIGN KEY ([IndicatorId]) REFERENCES [dbo].[Indicator] ([Id])
);


GO
ALTER TABLE [dbo].[IndicatorValue] NOCHECK CONSTRAINT [FK_IndicatorValue_Indicator];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_IndicatorValue__IndicatorId__IndicatorDate]
    ON [dbo].[IndicatorValue]([IndicatorId] ASC, [IndicatorDate] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Denominador ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Denominator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numerador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Numerator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'IndicatorDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'IndicatorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del valor del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Id';

