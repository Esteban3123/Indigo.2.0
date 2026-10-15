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
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt007] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt007_HealthSuperParameters];


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt007] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt007_MainAccount];


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt007] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt007_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Inversión de las Reservas Técnicas', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'InvestmentTechnicalReserves';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Desmaterializado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Dematerialized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Inversión en vinculado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Linked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MeasuredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MeasureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' 0:= No Aplica                    1:= Libre de afectación                   2:= Embargos                   3:= Medida Preventiva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gravamen', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Assessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Duration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Mercado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MarketValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tasa de Mercado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MarketRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodicidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Periodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modalidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Modality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tasa Facial', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'FacialRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Moneda:                  COP:= Pesos Colombianos                   USD:= Dólar de los Estados Unidos de América                   GBP:= Libra Británica                   EUR:= Euro                   CAD:= Dólar Canadiense                   CHF:= Franco Suizo                  JPY:= Yen Japonés                   OTR:= Otra  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'CurrencyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Nominal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'NominalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tasa Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'PurchaseDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Vencimiento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Emision', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'BroadcastDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'calificacion de Riesgo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'RiskRating';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otra Entidad Calificadora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'AnotherQualifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0:= No Aplica                    1:= BRC Investor Services S.A. (Stand & Poors)                    2:= Fitch Ratings Colombia S.A. (Antes Duff & Phelps De Colombia S.A.)                   3:= Value And Risk Rating S.A.                    4:=Otra Sociedad Calificadora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'RatingEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SIMEV Codigo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'SIMEVCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NIT de la Entidad Emisora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Título ISIN o CFI', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'CodeTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registre el nemotécnico o código DCV del título', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Mnemonic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tipo de inversión donde la Entidad Vigilada tiene la inversión', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'InvestmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' instrumentos donde la entidad reportante tiene las inversiones', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Instrument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt007', @level2type = N'COLUMN', @level2name = N'Id';

