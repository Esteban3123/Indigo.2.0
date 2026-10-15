CREATE TABLE [GeneralLedger].[HealthSuperParametersFt006] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HealthSuperParametersId] INT             NOT NULL,
    [MainAccountId]           INT             NOT NULL,
    [Storehouse]              VARCHAR (16)    NOT NULL,
    [ThirdPartyId]            INT             NOT NULL,
    [SIMEVCode]               VARCHAR (16)    NOT NULL,
    [RiskRating]              VARCHAR (7)     NOT NULL,
    [RatingEntity]            TINYINT         NOT NULL,
    [AnotherQualifier]        VARCHAR (50)    NOT NULL,
    [ClassAccount]            TINYINT         NOT NULL,
    [CurrencyType]            CHAR (3)        NOT NULL,
    [Assessment]              BIT             NOT NULL,
    [Status]                  TINYINT         NOT NULL,
    [MeasureDate]             DATE            NOT NULL,
    [MeasuredValue]           DECIMAL (18, 2) NOT NULL,
    [Yields]                  DECIMAL (18, 2) NOT NULL,
    [InvestmentReserves]      DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_HealthSuperParametersFt006__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthSuperParametersFt006_HealthSuperParameters] FOREIGN KEY ([HealthSuperParametersId]) REFERENCES [GeneralLedger].[HealthSuperParameters] ([Id]),
    CONSTRAINT [FK_HealthSuperParametersFt006_MainAccount] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_HealthSuperParametersFt006_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt006] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt006_HealthSuperParameters];


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt006] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt006_MainAccount];


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt006] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt006_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Inversiones de reserva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'InvestmentReserves';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rendimientos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Yields';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MeasuredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MeasureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' 0:= No Aplica                    1:= Libre de afectación                   2:= Embargos                   3:= Medida Preventiva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gravamen', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Assessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Moneda:                  COP:= Pesos Colombianos                   USD:= Dólar de los Estados Unidos de América                   GBP:= Libra Británica                   EUR:= Euro                   CAD:= Dólar Canadiense                   CHF:= Franco Suizo                  JPY:= Yen Japonés                   OTR:= Otra  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'CurrencyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase Cuenta:  1:= Cuenta Corriente                        2:= Cuenta de Ahorros                        3:= Cuenta Maestra de Recaudo                        4:= Cartera Colectiva Abierta o Fondos de Inversión en Mercado Monetario                        5:= Cartera Colectiva Cerrada                        6:= Otro tipo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'ClassAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otra Calificadora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'AnotherQualifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' 0:= No Aplica                    1:= BRC Investor Services S.A. (Stand & Poors)                    2:= Fitch Ratings Colombia S.A. (Antes Duff & Phelps De Colombia S.A.)                   3:= Value And Risk Rating S.A.                    4:=Otra Sociedad Calificadora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'RatingEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'calificacion de Riesgo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'RiskRating';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo SIMEV', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'SIMEVCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el Id de del tercero para asociar el NIT', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Establecimiento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Storehouse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Id';

