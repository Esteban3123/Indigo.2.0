CREATE TABLE [GeneralLedger].[HealthSuperParametersFt007] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HealthSuperParametersId]     INT             NOT NULL,
    [MainAccountId]               INT             NOT NULL,
    [Instrument]                  TINYINT         NOT NULL,
    [InvestmentType]              TINYINT         NOT NULL,
    [Mnemonic]                    VARCHAR (20)    NOT NULL,
    [CodeTitle]                   VARCHAR (7)     NOT NULL,
    [ThirdPartyId]                INT             NOT NULL,
    [SIMEVCode]                   VARCHAR (7)     NOT NULL,
    [RatingEntity]                TINYINT         NOT NULL,
    [AnotherQualifier]            VARCHAR (50)    NOT NULL,
    [RiskRating]                  VARCHAR (7)     NOT NULL,
    [BroadcastDate]               DATE            NULL,
    [ExpirationDate]              DATE            NULL,
    [PurchaseDate]                DATE            NULL,
    [PurchaseValue]               DECIMAL (18, 2) NOT NULL,
    [PurchaseRate]                DECIMAL (5, 2)  NOT NULL,
    [NominalValue]                DECIMAL (18, 2) NOT NULL,
    [CurrencyType]                CHAR (3)        NOT NULL,
    [FacialRate]                  VARCHAR (15)    NOT NULL,
    [Modality]                    TINYINT         NOT NULL,
    [Periodicity]                 CHAR (1)        NOT NULL,
    [MarketRate]                  VARCHAR (5)     NOT NULL,
    [MarketValue]                 DECIMAL (18, 2) NOT NULL,
    [Duration]                    DECIMAL (5, 2)  NOT NULL,
    [Assessment]                  BIT             NOT NULL,
    [Status]                      TINYINT         NOT NULL,
    [MeasureDate]                 DATE            NULL,
    [MeasuredValue]               DECIMAL (18, 2) NOT NULL,
    [Linked]                      BIT             NOT NULL,
    [Dematerialized]              TINYINT         NOT NULL,
    [InvestmentTechnicalReserves] BIT             NOT NULL,
    CONSTRAINT [PK_HealthSuperParametersFt007__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthSuperParametersFt007_HealthSuperParameters] FOREIGN KEY ([HealthSuperParametersId]) REFERENCES [GeneralLedger].[HealthSuperParameters] ([Id]),
    CONSTRAINT [FK_HealthSuperParametersFt007_MainAccount] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_HealthSuperParametersFt007_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inversión de Reservas Técnicas (BIT). Indicador si la inversión corresponde a reservas técnicas de seguros o pensiones.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'InvestmentTechnicalReserves';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inversión de las Reservas Técnicas', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'InvestmentTechnicalReserves';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'InvestmentTechnicalReserves';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desmaterializado (TINYINT). Estado de si el título está registrado en forma física o electrónica en depósito centralizado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Dematerialized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Desmaterializado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Dematerialized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Dematerialized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inversión en Vinculado (BIT). Indicador si la inversión es en entidad vinculada o relacionada.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Linked';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Inversión en vinculado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Linked';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Linked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Medida (DECIMAL 18,2). Valor de la inversión en la fecha de medición o evaluación contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MeasuredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MeasuredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MeasuredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Medida (DATE). Fecha en que se registró o midió el valor de la inversión para propósitos de reporte.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MeasureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MeasureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MeasureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado Jurídico (TINYINT). Clasificación: 0=No Aplica, 1=Libre de Afectación, 2=Embargos, 3=Medida Preventiva.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' 0:= No Aplica                    1:= Libre de afectación                   2:= Embargos                   3:= Medida Preventiva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gravamen (BIT). Indicador si existe gravamen, hipoteca o afectación legal sobre la inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Assessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gravamen', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Assessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Assessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración (DECIMAL 5,2). Plazo residual en años del instrumento de inversión hasta vencimiento.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Duration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Duration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Duration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de Mercado (DECIMAL 18,2). Precio actual de la inversión en mercado secundario o bolsa.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MarketValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Mercado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MarketValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MarketValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de Mercado (VARCHAR 5). Tasa de interés o rendimiento actual vigente en el mercado para instrumento similar.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MarketRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa de Mercado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MarketRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MarketRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Periodicidad (CHAR 1). Frecuencia de pago de cupones/dividendos: mensual, trimestral, semestral, anual.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Periodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodicidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Periodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Periodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad (TINYINT). Tipo de estructura del instrumento: tasa fija, variable, indexada, flotante.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Modality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modalidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Modality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Modality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa Facial (VARCHAR 15). Tasa de interés nominal pactada al momento de emisión del título.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'FacialRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa Facial', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'FacialRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'FacialRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo Moneda (CHAR 3). Divisa: COP=Peso Colombiano, USD=Dólar USA, EUR=Euro, GBP=Libra, CAD=Dólar Canadiense, CHF=Franco Suizo, JPY=Yen, OTR=Otra.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'CurrencyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Moneda:                  COP:= Pesos Colombianos                   USD:= Dólar de los Estados Unidos de América                   GBP:= Libra Británica                   EUR:= Euro                   CAD:= Dólar Canadiense                   CHF:= Franco Suizo                  JPY:= Yen Japonés                   OTR:= Otra  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'CurrencyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'CurrencyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Nominal (DECIMAL 18,2). Valor facial o principal del título al momento de emisión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'NominalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Nominal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'NominalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'NominalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa Compra (DECIMAL 5,2). Tasa de rendimiento o descuento aplicado en la adquisición de la inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Compra (DECIMAL 18,2). Precio pagado en la adquisición de la inversión (costo histórico).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Compra (DATE). Fecha de adquisición o compra del instrumento de inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Vencimiento (DATE). Fecha de redención, vencimiento o madurez del título.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Vencimiento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Emisión (DATE). Fecha en que el emisor colocó o emitió el instrumento en el mercado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'BroadcastDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Emision', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'BroadcastDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'BroadcastDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación de Riesgo (VARCHAR 7). Clasificación de riesgo crediticio del emisor (AAA, AA, A, BBB, etc.).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'RiskRating';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'calificacion de Riesgo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'RiskRating';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'RiskRating';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otra Entidad Calificadora (VARCHAR 50). Nombre o identificación de calificadora de riesgo adicional a la principal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'AnotherQualifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otra Entidad Calificadora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'AnotherQualifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'AnotherQualifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entidad Calificadora (TINYINT). Clasificador: 1=BRC Investor Services S.A., 2=Fitch Ratings Colombia, 3=Value And Risk Rating, 4=Otra Sociedad Calificadora.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'RatingEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0:= No Aplica                    1:= BRC Investor Services S.A. (Stand & Poors)                    2:= Fitch Ratings Colombia S.A. (Antes Duff & Phelps De Colombia S.A.)                   3:= Value And Risk Rating S.A.                    4:=Otra Sociedad Calificadora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'RatingEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'RatingEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código SIMEV (VARCHAR 7). Código identificador único en el Sistema Integrado de Mercados de Valores colombiano.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'SIMEVCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'SIMEV Codigo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'SIMEVCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'SIMEVCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT Entidad Emisora (INT, FK). Identificación tributaria (NIT) del tercero emisor de la inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NIT de la Entidad Emisora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Título ISIN/CFI (VARCHAR 7). Identificador internacional ISIN o código CFI del instrumento financiero.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'CodeTitle';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Título ISIN o CFI', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'CodeTitle';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'CodeTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nemotécnico DCV (VARCHAR 20). Código nemotécnico o identificador DCV del título en bolsa o depósito.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Mnemonic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registre el nemotécnico o código DCV del título', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Mnemonic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Mnemonic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo Inversión (TINYINT). Clasificación del instrumento: renta fija, variable, fondos, derivados, otros.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'InvestmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tipo de inversión donde la Entidad Vigilada tiene la inversión', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'InvestmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'InvestmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrumento Financiero (TINYINT). Categoría de activo: bonos, acciones, TES, papeles comerciales, CDT, etc.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Instrument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' instrumentos donde la entidad reportante tiene las inversiones', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Instrument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Instrument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Cuenta Contable (INT, FK). Identificador de la cuenta mayor parametrizada para registrar la inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Cabecera Parámetros (INT, FK). Identificador del registro maestro de parámetros supervisión superintendencia.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Detalle Formato (INT). Identificador único del registro detalle de inversión en formato supervisorio.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Portafolio de inversiones financieras registradas para el reporte FT007 ante la Superintendencia de Salud. Guarda los títulos valores, instrumentos de inversión y sus características de valoración, vencimiento y calificación de riesgo que las entidades de salud deben reportar como parte de sus estados financieros y reservas técnicas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007';
