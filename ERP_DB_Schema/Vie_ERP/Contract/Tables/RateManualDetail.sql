CREATE TABLE [Contract].[RateManualDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RateManualId]            INT             NOT NULL,
    [IPSServiceId]            INT             NOT NULL,
    [SalesValueWithSurcharge] NUMERIC (18, 2) CONSTRAINT [DF_RateManualDetail_SalesValueWithSurcharge] DEFAULT ((0)) NOT NULL,
    [SalesValue]              NUMERIC (18, 2) CONSTRAINT [DF_RateManualDetail_SalesValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RateManualDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RateManualDetail_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_RateManualDetail_RateManual] FOREIGN KEY ([RateManualId]) REFERENCES [Contract].[RateManual] ([Id])
);


GO
ALTER TABLE [Contract].[RateManualDetail] NOCHECK CONSTRAINT [FK_RateManualDetail_IPSService];


GO
ALTER TABLE [Contract].[RateManualDetail] NOCHECK CONSTRAINT [FK_RateManualDetail_RateManual];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_RateManualDetail__RateManualId__IPSServiceId]
    ON [Contract].[RateManualDetail]([RateManualId] ASC, [IPSServiceId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del servicio que se va cobrar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor con recargo que se va cobrar en el rango de fechas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetail', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio IPS que va tener precio dentro del manual, estos servicios IPS solo van a ser de Clase "No Quirurgico" de tipo ninguno y los de clase "Paquetes"', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del manual tarifario al que pertenece', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del manual tarifario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetail', @level2type = N'COLUMN', @level2name = N'Id';

