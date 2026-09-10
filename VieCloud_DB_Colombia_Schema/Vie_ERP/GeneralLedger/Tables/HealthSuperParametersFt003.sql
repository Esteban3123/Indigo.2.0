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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Deuda (TINYINT): clasificación contable de obligación financiera. 1=Activo no financiero/Anticipo (adelanto de pago); 2=Instrumento financiero (obligación financiera). Usado en contabilidad de salud para distinguir naturaleza de la deuda en reportes de cartera y RIPS.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'Typedebt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Deuda :1:= Activo no financiero – Anticipo; 2:= Instrumento financiero.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'Typedebt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'Typedebt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medición Posterior (TINYINT): método de valoración contable aplicado post-reconocimiento inicial. 1=Precio de Transacción/Valor Nominal/Costo; 2=Costo Amortizado; 3=Valor Razonable; 4=Valor Razonable con cambios en ORI; 5=Valor Presente Pagos Futuros; 6=No aplica (ej: Anticipos). Determina cómo se registra el débito en estados financieros de la IPS.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'SubsequentMeasurement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicion Posterior:                   1:= Precio de la Transacción / Valor Nominal / Costo                   2:= Costo Amortizado                   3:= Valor Razonable                   4:= Valor Razonable con cambios en el ORI                   5:= Valor Presente Pagos Futuros                   6:= No aplica (Ejemplo: Anticipos)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'SubsequentMeasurement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'SubsequentMeasurement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de Deudores (TINYINT): clasificación de origen de la cartera por fuente de financiamiento. 1=POS (Plan Obligatorio Salud); 2=Planes adicionales/complementarios; 3=Recobros No POS; 4=Reembolsos incapacidades (fuera enfermedad general); 5=SOAT/ARL (seguros complementarios); 6=Reclamaciones ECAT; 7=Otros. Esencial para análisis de glosas, facturación y reportes de cartera.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'DebtorsConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de deudores:                  1:= Plan obligatorio de Salud                  2:= Planes adicionales de Salud                  3:= Recobros No POS                  4:= Reembolsos por incapacidades diferentes a enfermedad general                  5:= SOAT y ARL                  6:= Reclamaciones (ECAT)                  7:= Otros ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'DebtorsConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'DebtorsConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo Deudor (TINYINT): indica qué identificador diligencia el formato de reporte. 1=NIT del tercero (acreedor/proveedor); 2=Código de la cuenta contable parametrizada. Define mapeo entre deudor operacional y deudor contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'DebtorFieldId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica Como se llena el formato con Nit tercero o codigo de la cuenta  1 - Nit Tercero  2 - Código de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'DebtorFieldId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'DebtorFieldId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Cuenta Contable Principal (INT, FK): referencia a [GeneralLedger].[MainAccounts]. Identificador de la cuenta del mayor donde se registra contablemente la deuda (activo, pasivo o gasto según tipo de operación en la IPS).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Parámetros Salud (INT, FK): referencia a cabecera [GeneralLedger].[HealthSuperParameters]. Agrupa este detalle con configuración superior de parámetros contables y financieros del ente de salud.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'HealthSuperParametersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Detalle Formato (INT, PK): identificador único auto-incremental del registro de detalle. Representa cada línea de configuración de deuda dentro del formato de reporte contable-financiero de la institución de salud.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de cuentas contables para el reporte FT003 de la Superintendencia de Salud. Define la configuración de cuentas principales, campos de deudores, conceptos de cartera y tipos de medición subsecuente requeridos para la generación del formato de información financiera ante el ente de control.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'HealthSuperParametersFt003';
