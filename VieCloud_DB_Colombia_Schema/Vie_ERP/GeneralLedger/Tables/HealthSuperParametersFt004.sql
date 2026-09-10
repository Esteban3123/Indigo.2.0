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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de medición posterior o revaluación de activos financieros: 1=Precio transacción/valor nominal/costo, 2=Costo amortizado, 3=Valor razonable, 4=Valor razonable cambios ORI, 5=Valor presente pagos futuros, 6=No aplica. Tipo: TINYINT. Contabilidad NIIF.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'SubsequentMeasurement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicion Posterior:                   1:= Precio de la Transacción / Valor Nominal / Costo                    2:= Costo Amortizado                    3:= Valor Razonable                   4:= Valor Razonable con cambios en el ORI                    5:= Valor Presente Pagos Futuros                   6:= No aplica  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'SubsequentMeasurement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'SubsequentMeasurement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de acreencia o tipo de pasivo a facturar: 1=Prestación servicios salud, 2=Insumos medicamentos, 3=Dispositivo médico/equipo biomédico, 4=Administrativo (servicios públicos, aportes, anticipos, papelería), 5=Restitución recursos, 6=Otro. Tipo: TINYINT. RIPS/facturación.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'CreditConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Acreencia:                  1:= Prestación de servicios de salud                   2:= Insumos y medicamentos                   3:= Dispositivo médico o equipo biomédico                   4:= Administrativo (servicios públicos, aportes parafiscales, avances y anticipos, papelería, etc.)                   5:= Restitución de recursos                   6:= Otro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'CreditConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'CreditConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de identificación del acreedor/tercero: 1=Usar NIT tercero, 2=Usar código cuenta contable. Tipo: TINYINT. Define si se referencia por NIT o código contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'creditorIdBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica Como se llena el formato con Nit tercero o codigo de la cuenta  1 - Nit Tercero  2 - Código de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'creditorIdBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'creditorIdBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable parametrizada. Tipo: INT. Referencia FK a [MainAccounts]. Código PUC/plan contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de cabecera de parámetros de supervigencia de salud. Tipo: INT. Referencia FK a [HealthSuperParameters]. Agrupa detalles.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle del formato de supervigencia FT004. Tipo: INT IDENTITY. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de la Superintendencia de Salud para el formato FT004, que define la configuración contable de cada parámetro: cuenta principal, criterio del acreedor, concepto de crédito y medición posterior. Se utiliza para la generación y conciliación de reportes contables regulatorios exigidos por la Supersalud.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt004';
