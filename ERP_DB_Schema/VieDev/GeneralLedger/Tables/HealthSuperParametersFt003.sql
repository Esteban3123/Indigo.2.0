CREATE TABLE [GeneralLedger].[HealthSuperParametersFt003] (
    [Id]                      INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HealthSuperParametersId] INT     NOT NULL,
    [MainAccountId]           INT     NOT NULL,
    [DebtorFieldId]           TINYINT NOT NULL,
    [DebtorsConcept]          TINYINT NOT NULL,
    [SubsequentMeasurement]   TINYINT NOT NULL,
    [Typedebt]                TINYINT NOT NULL,
    CONSTRAINT [PK_HealthSuperParametersFt003__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthSuperParametersFt003_HealthSuperParameters] FOREIGN KEY ([HealthSuperParametersId]) REFERENCES [GeneralLedger].[HealthSuperParameters] ([Id]),
    CONSTRAINT [FK_HealthSuperParametersFt003_MainAccount] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt003] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt003_HealthSuperParameters];


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt003] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt003_MainAccount];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Deuda :1:= Activo no financiero – Anticipo; 2:= Instrumento financiero.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'Typedebt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicion Posterior:                   1:= Precio de la Transacción / Valor Nominal / Costo                   2:= Costo Amortizado                   3:= Valor Razonable                   4:= Valor Razonable con cambios en el ORI                   5:= Valor Presente Pagos Futuros                   6:= No aplica (Ejemplo: Anticipos)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'SubsequentMeasurement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de deudores:                  1:= Plan obligatorio de Salud                  2:= Planes adicionales de Salud                  3:= Recobros No POS                  4:= Reembolsos por incapacidades diferentes a enfermedad general                  5:= SOAT y ARL                  6:= Reclamaciones (ECAT)                  7:= Otros ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'DebtorsConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica Como se llena el formato con Nit tercero o codigo de la cuenta  1 - Nit Tercero  2 - Código de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'DebtorFieldId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'Id';

