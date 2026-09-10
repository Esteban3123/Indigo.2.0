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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inversiones de reserva (DECIMAL 18,2) - fondos reservados, provisiones o fondos de inversión constituidos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'InvestmentReserves';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inversiones de reserva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'InvestmentReserves';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'InvestmentReserves';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rendimientos (DECIMAL 18,2) - ingresos, intereses o ganancias generadas por la inversión o cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Yields';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rendimientos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Yields';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Yields';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor medida (DECIMAL 18,2) - monto en la moneda especificada, valor evaluado o medido en la fecha', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MeasuredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MeasuredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MeasuredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de medida (DATE) - fecha en que se realizó la medición o evaluación del valor', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MeasureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha Medida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MeasureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MeasureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (TINYINT): 0=No aplica, 1=Libre afectación, 2=Embargos, 3=Medida preventiva - estado jurídico de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' 0:= No Aplica                    1:= Libre de afectación                   2:= Embargos                   3:= Medida Preventiva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gravamen (BIT) - indicador booleano de si existe gravamen, embargo o limitación sobre la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Assessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gravamen', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Assessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Assessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de moneda (CHAR 3): COP=Pesos colombianos, USD=Dólar USA, GBP=Libra británica, EUR=Euro, CAD=Dólar canadiense, CHF=Franco suizo, JPY=Yen, OTR=Otra', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'CurrencyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Moneda:                  COP:= Pesos Colombianos                   USD:= Dólar de los Estados Unidos de América                   GBP:= Libra Británica                   EUR:= Euro                   CAD:= Dólar Canadiense                   CHF:= Franco Suizo                  JPY:= Yen Japonés                   OTR:= Otra  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'CurrencyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'CurrencyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de cuenta (TINYINT): 1=Corriente, 2=Ahorros, 3=Maestra recaudo, 4=Cartera colectiva abierta/Fondos monetarios, 5=Cartera colectiva cerrada, 6=Otro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'ClassAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase Cuenta:  1:= Cuenta Corriente                        2:= Cuenta de Ahorros                        3:= Cuenta Maestra de Recaudo                        4:= Cartera Colectiva Abierta o Fondos de Inversión en Mercado Monetario                        5:= Cartera Colectiva Cerrada                        6:= Otro tipo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'ClassAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'ClassAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otra calificadora (VARCHAR 50) - nombre alternativo de entidad calificadora de riesgo si aplica', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'AnotherQualifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otra Calificadora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'AnotherQualifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'AnotherQualifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entidad calificadora (TINYINT): 0=No aplica, 1=BRC/Stand&Poors, 2=Fitch, 3=Value&Risk, 4=Otra sociedad calificadora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'RatingEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' 0:= No Aplica                    1:= BRC Investor Services S.A. (Stand & Poors)                    2:= Fitch Ratings Colombia S.A. (Antes Duff & Phelps De Colombia S.A.)                   3:= Value And Risk Rating S.A.                    4:=Otra Sociedad Calificadora', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'RatingEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'RatingEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación de riesgo (VARCHAR 7) - clasificación de riesgo crediticio o de inversión asignada a la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'RiskRating';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'calificacion de Riesgo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'RiskRating';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'RiskRating';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código SIMEV (VARCHAR 16) - clasificación de inversiones y valores según normativa superintendencia', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'SIMEVCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo SIMEV', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'SIMEVCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'SIMEVCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del tercero, NIT o razón social asociada - referencia FK a entidad, IPS, proveedor o asegurador', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el Id de del tercero para asociar el NIT', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del establecimiento, sede, centro de atención o almacén asociado (VARCHAR 16) - ubicación física del recurso', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Storehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Establecimiento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Storehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Storehouse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cuenta contable parametrizada en el plan de cuentas - referencia FK a MainAccounts', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cabecera o registro maestro de parámetros súper de salud - referencia FK a tabla padre', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del detalle del registro de parámetros súper de salud - clave primaria de la fila', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros del reporte FT-006 exigido por la Superintendencia Nacional de Salud: registra las inversiones financieras de la entidad, incluyendo cuenta contable, tercero, calificación de riesgo, entidad calificadora, moneda, fecha de valoración, valor medido, rendimientos y reservas de inversión.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt006';
