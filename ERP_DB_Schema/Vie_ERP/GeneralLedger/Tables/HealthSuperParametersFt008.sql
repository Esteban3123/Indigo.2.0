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
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt008] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt008_HealthSuperParameters];


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt008] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt008_MainAccount];


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt008] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt008_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Inversión en vinculado : 0:= No Aplica; 1:= Inversión en vinculado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Linked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MeasuredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MeasureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' 0:= No Aplica                    1:= Libre de afectación                   2:= Embargos                   3:= Medida Preventiva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gravamen :0:= No tiene gravamen ;1:= Si tiene gravamen', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Assessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rendimientos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Yields';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Participacion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Participation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Duration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Mercado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MarketValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tasa de Mercado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MarketRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodicidad: M:= Mensual                      B:= Bimestral                      T:= Trimestral                      S:= Semestral                      A:= Anual                      O:= Otro o No Aplica  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Periodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modalidad: 0:= No Aplica. 1:= Vencida.2:= Anticipada.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Modality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tasa Facial', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'FacialRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Moneda:                  COP:= Pesos Colombianos                   USD:= Dólar de los Estados Unidos de América                   GBP:= Libra Británica                   EUR:= Euro                   CAD:= Dólar Canadiense                   CHF:= Franco Suizo                  JPY:= Yen Japonés                   OTR:= Otra  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'CurrencyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Nominal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'NominalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tasa Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Compra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'PurchaseDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Vencimiento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Emision', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'BroadcastDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro Tipo de Inversion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'AnotherInvestmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tipo de inversión donde la Entidad Vigilada tiene la inversión :                   1:= Títulos de Deuda Pública (TES)                   2:= Certificados de Depósito a Término (CDT)                   3:= Bonos Ordinarios                   4:= Bonos Subordinados                   5:= Bonos Opcionalmente Convertibles en acciones                   6:= Bonos Obligatoriamente Convertibles en acciones                   7:= Bonos de Capitalización                   8:= Acciones Ordinarias                   9:= Acciones preferenciales                   10:= Otro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'InvestmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NIT de la Entidad Emisora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pais', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Country';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt008', @level2type = N'COLUMN', @level2name = N'Id';

