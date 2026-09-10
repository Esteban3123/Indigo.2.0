CREATE TABLE [GeneralLedger].[HealthSuperParametersFt008] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HealthSuperParametersId] INT             NOT NULL,
    [MainAccountId]           INT             NOT NULL,
    [Country]                 VARCHAR (20)    NOT NULL,
    [ThirdPartyId]            INT             NOT NULL,
    [InvestmentType]          TINYINT         NOT NULL,
    [AnotherInvestmentType]   VARCHAR (100)   NOT NULL,
    [BroadcastDate]           DATE            NULL,
    [ExpirationDate]          DATE            NULL,
    [PurchaseDate]            DATE            NULL,
    [PurchaseValue]           DECIMAL (18, 2) NOT NULL,
    [PurchaseRate]            DECIMAL (5, 2)  NOT NULL,
    [NominalValue]            DECIMAL (18, 2) NOT NULL,
    [CurrencyType]            CHAR (3)        NOT NULL,
    [FacialRate]              VARCHAR (15)    NOT NULL,
    [Modality]                TINYINT         NOT NULL,
    [Periodicity]             CHAR (1)        NOT NULL,
    [MarketRate]              VARCHAR (5)     NOT NULL,
    [MarketValue]             DECIMAL (18, 2) NOT NULL,
    [Duration]                DECIMAL (5, 2)  NOT NULL,
    [Participation]           DECIMAL (5, 2)  NOT NULL,
    [Yields]                  DECIMAL (18, 2) NOT NULL,
    [Assessment]              BIT             NOT NULL,
    [Status]                  TINYINT         NOT NULL,
    [MeasureDate]             DATE            NULL,
    [MeasuredValue]           DECIMAL (18, 2) NOT NULL,
    [Linked]                  BIT             NOT NULL,
    CONSTRAINT [PK_HealthSuperParametersFt008__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthSuperParametersFt008_HealthSuperParameters] FOREIGN KEY ([HealthSuperParametersId]) REFERENCES [GeneralLedger].[HealthSuperParameters] ([Id]),
    CONSTRAINT [FK_HealthSuperParametersFt008_MainAccount] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_HealthSuperParametersFt008_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de inversión en vinculado (BIT: 0=No aplica, 1=Sí es inversión vinculada). Identifica si la inversión involucra terceros relacionados o afiliados a la entidad vigilada.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Linked';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Inversión en vinculado : 0:= No Aplica; 1:= Inversión en vinculado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Linked';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Linked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor medido en pesos o moneda extranjera (DECIMAL 18,2). Resultado de la medición o valoración de la inversión en fecha específica para propósitos de reconocimiento contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MeasuredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MeasuredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MeasuredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de medida o valoración (DATE). Día en que se realizó la última medición, evaluación o marcación a mercado del instrumento de inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MeasureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MeasureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MeasureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado legal o de afectación de la inversión (TINYINT: 0=No aplica, 1=Libre de afectación, 2=Embargos, 3=Medida preventiva). Indica restricciones o gravámenes sobre la titularidad.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' 0:= No Aplica                    1:= Libre de afectación                   2:= Embargos                   3:= Medida Preventiva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de gravamen o hipoteca (BIT: 0=No tiene gravamen, 1=Sí tiene gravamen). Refleja si la inversión está afecta a deuda u obligación.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Assessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gravamen :0:= No tiene gravamen ;1:= Si tiene gravamen', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Assessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Assessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rendimientos o intereses generados (DECIMAL 18,2). Ingresos por capital ganado, cupones, dividendos o ganancias de la inversión durante período.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Yields';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rendimientos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Yields';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Yields';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de participación accionaria (DECIMAL 5,2). Porcentaje de capital que representa la inversión en el emisor o fondo.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Participation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Participacion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Participation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Participation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración o plazo remanente (DECIMAL 5,2). Período en años o meses desde fecha de medida hasta vencimiento del instrumento de inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Duration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Duration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Duration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de mercado o precio vigente (DECIMAL 18,2). Valor actual de cotización en bolsa o valoración de mercado del instrumento de inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MarketValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Mercado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MarketValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MarketValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de mercado o rendimiento de mercado (VARCHAR 5). Porcentaje vigente de rendimiento o descuento en el mercado para instrumento similar.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MarketRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa de Mercado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MarketRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MarketRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Periodicidad de pago de rendimientos (CHAR 1: M=Mensual, B=Bimestral, T=Trimestral, S=Semestral, A=Anual, O=Otro/No aplica). Frecuencia de distribución de cupones o dividendos.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Periodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodicidad: M:= Mensual                      B:= Bimestral                      T:= Trimestral                      S:= Semestral                      A:= Anual                      O:= Otro o No Aplica  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Periodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Periodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de pago del cupón (TINYINT: 0=No aplica, 1=Vencida, 2=Anticipada). Indica si los intereses se pagan al vencimiento o de forma anticipada.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Modality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modalidad: 0:= No Aplica. 1:= Vencida.2:= Anticipada.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Modality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Modality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa facial o de emisión (VARCHAR 15). Tasa de interés nominal fija establecida al momento de emisión del instrumento de inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'FacialRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa Facial', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'FacialRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'FacialRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de moneda (CHAR 3: COP, USD, GBP, EUR, CAD, CHF, JPY, OTR). Divisa en que está denominada la inversión; identifica moneda de transacción.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'CurrencyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Moneda:                  COP:= Pesos Colombianos                   USD:= Dólar de los Estados Unidos de América                   GBP:= Libra Británica                   EUR:= Euro                   CAD:= Dólar Canadiense                   CHF:= Franco Suizo                  JPY:= Yen Japonés                   OTR:= Otra  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'CurrencyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'CurrencyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor nominal o principal (DECIMAL 18,2). Valor facial de la inversión declarado por el emisor, base para cálculo de rendimientos.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'NominalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Nominal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'NominalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'NominalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de compra o adquisición (DECIMAL 5,2). Rendimiento o descuento aplicado al momento de compra o inversión inicial.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de compra o inversión inicial (DECIMAL 18,2). Monto en pesos o moneda desembolsado para adquirir el instrumento de inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de compra o desembolso (DATE). Día en que se realizó la adquisición o inversión del instrumento financiero.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento o redención (DATE). Día de maduración en que se espera recuperar el principal de la inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Vencimiento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión del instrumento (DATE). Día en que el emisor colocó públicamente el instrumento de inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'BroadcastDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Emision', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'BroadcastDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'BroadcastDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación alternativa o descripción de tipo de inversión (VARCHAR 100). Denominación adicional cuando el tipo no corresponde a categorías estándar.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'AnotherInvestmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro Tipo de Inversion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'AnotherInvestmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'AnotherInvestmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de instrumento de inversión (TINYINT: 1=TES, 2=CDT, 3=Bonos ordinarios, 4=Bonos subordinados, 5=Bonos convertibles en acciones, 6=Bonos obligatoriamente convertibles, 7=Bonos capitalización, 8=Acciones ordinarias, 9=Acciones preferenciales, 10=Otro). Clasifica el instrumento financiero adquirido.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'InvestmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tipo de inversión donde la Entidad Vigilada tiene la inversión :                   1:= Títulos de Deuda Pública (TES)                   2:= Certificados de Depósito a Término (CDT)                   3:= Bonos Ordinarios                   4:= Bonos Subordinados                   5:= Bonos Opcionalmente Convertibles en acciones                   6:= Bonos Obligatoriamente Convertibles en acciones                   7:= Bonos de Capitalización                   8:= Acciones Ordinarias                   9:= Acciones preferenciales                   10:= Otro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'InvestmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'InvestmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación (NIT, RUC, CÉDULA) de la entidad emisora (FK a ThirdParty). Clave única del tercero que emite el instrumento de inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NIT de la Entidad Emisora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de la entidad emisora (VARCHAR 20). Nación donde está domiciliada legalmente la entidad que emite la inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Country';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pais', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Country';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Country';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable parametrizada (INT, FK a MainAccounts). Referencia al plan de cuentas donde se registra la inversión en el mayor general.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera de parámetros de superintendencia (INT, FK a HealthSuperParameters). Vincula a la política o normativa supervisora madre.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de inversión (INT, PK). Clave primaria que identifica cada registro de inversión en el formato supervisado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de inversiones financieras del formato FT-008 de la Superintendencia Nacional de Salud. Almacena el detalle de cada título o instrumento de inversión: tipo, fechas clave, valores de compra y mercado, tasas, moneda, vencimiento y estado de valoración, para el reporte de portafolio de inversiones ante el ente regulador.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008';
