CREATE TABLE [Common].[ThirdPartyAccumulatedExemptIncome] (
    [Id]                                    INT             IDENTITY (1, 1) NOT NULL,
    [ThirdPartyId]                          INT             NOT NULL,
    [Year]                                  INT             NOT NULL,
    [AccumulatedValue]                      NUMERIC (18, 2) NOT NULL,
    [AccumulatedMaxDeductionsAndRentExents] NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_ThirdPartyAccumulatedExemptIncome] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ThirdPartyAccumulatedExemptIncome_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Common].[ThirdPartyAccumulatedExemptIncome] NOCHECK CONSTRAINT [FK_ThirdPartyAccumulatedExemptIncome_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Acumulado Rentas Exentas y Deducciones', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyAccumulatedExemptIncome', @level2type = N'COLUMN', @level2name = N'AccumulatedMaxDeductionsAndRentExents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Acumulado Renta Excenta 25%', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyAccumulatedExemptIncome', @level2type = N'COLUMN', @level2name = N'AccumulatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año Renta Excenta Acumulada de Terceros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyAccumulatedExemptIncome', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id tercero Renta Excenta Acumulada de Terceros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyAccumulatedExemptIncome', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id tabla Renta Excenta Acumulada de Terceros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyAccumulatedExemptIncome', @level2type = N'COLUMN', @level2name = N'Id';

