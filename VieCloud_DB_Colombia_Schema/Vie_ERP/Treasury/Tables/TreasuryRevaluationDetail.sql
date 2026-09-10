CREATE TABLE [Treasury].[TreasuryRevaluationDetail] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [TreasuryRevaluationId]        INT             NOT NULL,
    [DocumentType]                 TINYINT         NOT NULL,
    [Nature]                       TINYINT         NOT NULL,
    [DocumentNumber]               VARCHAR (20)    NOT NULL,
    [DocumentDate]                 DATETIME        NOT NULL,
    [ValueMovement]                NUMERIC (18, 2) NOT NULL,
    [CurrencyId]                   INT             NOT NULL,
    [CurrencyConvertedId]          INT             NOT NULL,
    [ValueCurrency]                NUMERIC (20, 5) NOT NULL,
    [ValueCurrencyReverse]         NUMERIC (20, 5) NOT NULL,
    [ActualValueCurrency]          NUMERIC (20, 5) NOT NULL,
    [ActualValueCurrencyReverse]   NUMERIC (20, 5) NOT NULL,
    [ValueMovementConverted]       NUMERIC (18, 2) NOT NULL,
    [ActualValueMovementConverted] NUMERIC (18, 2) NOT NULL,
    [ProfitLostValue]              NUMERIC (18, 2) NOT NULL,
    [TreasuryBalanceId]            INT             NULL,
    CONSTRAINT [PK_TreasuryRevaluationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TreasuryRevaluationDetail_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_TreasuryRevaluationDetail_CurrencyConverted] FOREIGN KEY ([CurrencyConvertedId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_TreasuryRevaluationDetail_TreasuryBalance] FOREIGN KEY ([TreasuryBalanceId]) REFERENCES [Treasury].[TreasuryBalance] ([Id]),
    CONSTRAINT [FK_TreasuryRevaluationDetail_TreasuryRevaluation] FOREIGN KEY ([TreasuryRevaluationId]) REFERENCES [Treasury].[TreasuryRevaluation] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK a TreasuryBalance; identifica el movimiento de tesorería (ingreso/egreso) que afectó la caja o banco revalorizándose.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'TreasuryBalanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del movimiento de tesoreria que afecto la caja o el banco', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'TreasuryBalanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'TreasuryBalanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(18,2): ganancia (positivo) o pérdida (negativo) por diferencia de tasa: ActualValueMovementConverted - ValueMovementConverted.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ProfitLostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo especifica el valor de ganancia o perdida, siendo esta positiva o negativa respectivamente   La formula aplicada es ActualMovementConverted - ValueMovementConverted', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ProfitLostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ProfitLostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(18,2): ValueMovement convertido a CurrencyConvertedId usando tasas ActualValueCurrency/ActualValueCurrencyReverse del cierre mensual.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueMovementConverted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del movimiento convertido a la moneda a revalorizar (CurrencyConvertedId), La conversion se debe hacer usando los campos de ActualValueCurrency o ActualValueCurrencyReverse pues el valor debe ser convertido con la tasa del dia del cierre (Ultimo dia del mes)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueMovementConverted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueMovementConverted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(18,2): ValueMovement convertido a CurrencyConvertedId usando tasas ValueCurrency/ValueCurrencyReverse del día del movimiento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueMovementConverted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del movimiento convertido a la moneda a revalorizar (CurrencyConvertedId), La conversion se debe hacer usando los campos de ValueCurrency o ValueCurrencyReverse pues el valor debe ser convertido con la tasa del dia del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueMovementConverted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueMovementConverted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,5): tasa inversa (TRM) de CurrencyConvertedId a CurrencyId en fecha de cierre (último día del mes).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valores de TRM de la fecha de cierre (Ultimo dia del mes):  Valor de la tasa de cambio de la moneda de esta tabla (CurrencyConvertedId) contra la moneda de la Caja o Banco   Ejemplo Caja en dolares, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo Caja en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrencyReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,5): tasa de cambio (TRM) de CurrencyId a CurrencyConvertedId en fecha de cierre (último día del mes).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valores de TRM de la fecha del cierre (Ultimo dia del mes):  Valor de la tasa de cambio de la moneda de la Caja contra la moneda de esta tabla (CurrencyConvertedId)  Ejemplo Caja en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo Caja en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,5): tasa inversa (TRM) de CurrencyConvertedId a CurrencyId en fecha del movimiento; ej: 1 COP=0,00153 USD.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valores de TRM de la fecha del movimiento:  Valor de la tasa de cambio de la moneda de esta tabla (CurrencyConvertedId) contra la moneda de la Caja o Banco   Ejemplo Caja en dolares, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo Caja en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrencyReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,5): tasa de cambio (TRM) de CurrencyId a CurrencyConvertedId en fecha del movimiento; ej: 1 USD=650 COP.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valores de TRM de la fecha del movimiento:  Valor de la tasa de cambio de la moneda de la Caja contra la moneda de esta tabla (CurrencyConvertedId)  Ejemplo Caja en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo Caja en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK a Common.Currency; moneda destino de revaluación, siempre diferente a la de la caja/banco.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyConvertedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda destino de revalorizacion, es decir que este currency siempre va a ser diferente a la moenda de la caja o banco', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyConvertedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyConvertedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK a Common.Currency; moneda origen del movimiento (caja o cuenta bancaria), referencia para tasas de cambio.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda origen del documento, en este caso la moneda de la caja o banco', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(18,2): valor del movimiento en la moneda original de caja/banco (CurrencyId), base para cálculos de conversión.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueMovement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del movimiento en la moneda original de la cja o banco', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueMovement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueMovement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME: fecha del movimiento, igual a DocumentDate de TreasuryBalance; para saldo inicial es el primer día del mes revalorizándose.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha en la que se realizo el movimiento, este campo es igual al documentdate de Treasury.TreasuryBalance,  Cuando sea el movimiento de saldo de la caja esta fecha debe ser el primer dia del mes que estoy revalorizando', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20): número o código del documento; para saldos iniciales coincide con el código de caja/cuenta bancaria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del documento del movimiento, para el caso del saldo inicial el numero del documento va a ser igual al codigo de la caja o cuenta bancaria ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: naturaleza contable del movimiento (1=Débito, 2=Crédito), determina el flujo del efectivo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza del documento que genera movimiento,   1 - Debito   2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: tipo de documento generador del movimiento (0=Saldo inicial, 1=Recibo de Caja, 2=Comprobante de Egreso, 3=Consignación, 4=Nota, 5=Fondo de Caja Menor).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de documento del movimiento  0 - Saldo de la caja o banco  1 - Recibo de Caja  2 - Comrpobante de Egreso  3 - Consignaciones  4 - Notas  5 - Fondo de Caja Menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a TreasuryRevaluation; identifica la cabecera de revaluación y la caja/cuenta bancaria cuyos movimientos se revalorizan.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'TreasuryRevaluationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que se relaciona con la cabecera de la revvalorizacion, esto nos ayuda a identificar de que caja o cuenta bancaria son los movimientos que estoy revalorizando', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'TreasuryRevaluationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'TreasuryRevaluationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle de revaluación, clave primaria del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de revaluación de tesorería: registra línea por línea los documentos financieros (facturas, pagos, notas) afectados por un proceso de revaluación de moneda extranjera, mostrando los valores originales, convertidos y la ganancia o pérdida cambiaria generada.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluationDetail';
