CREATE TABLE [GeneralLedger].[HealthSuperParametersFt004] (
    [Id]                      INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HealthSuperParametersId] INT     NOT NULL,
    [MainAccountId]           INT     NOT NULL,
    [creditorIdBy]            TINYINT NOT NULL,
    [CreditConcept]           TINYINT NOT NULL,
    [SubsequentMeasurement]   TINYINT NOT NULL,
    CONSTRAINT [PK_HealthSuperParametersFt004__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthSuperParametersFt004_HealthSuperParameters] FOREIGN KEY ([HealthSuperParametersId]) REFERENCES [GeneralLedger].[HealthSuperParameters] ([Id]),
    CONSTRAINT [FK_HealthSuperParametersFt004_MainAccount] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt004] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt004_HealthSuperParameters];


GO
ALTER TABLE [GeneralLedger].[HealthSuperParametersFt004] NOCHECK CONSTRAINT [FK_HealthSuperParametersFt004_MainAccount];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicion Posterior:                   1:= Precio de la Transacción / Valor Nominal / Costo                    2:= Costo Amortizado                    3:= Valor Razonable                   4:= Valor Razonable con cambios en el ORI                    5:= Valor Presente Pagos Futuros                   6:= No aplica  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'SubsequentMeasurement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de Acreencia:                  1:= Prestación de servicios de salud                   2:= Insumos y medicamentos                   3:= Dispositivo médico o equipo biomédico                   4:= Administrativo (servicios públicos, aportes parafiscales, avances y anticipos, papelería, etc.)                   5:= Restitución de recursos                   6:= Otro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'CreditConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica Como se llena el formato con Nit tercero o codigo de la cuenta  1 - Nit Tercero  2 - Código de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'creditorIdBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'Id';

