CREATE TABLE [GeneralLedger].[InitialBalance] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LegalBookCode]        VARCHAR (50)    NOT NULL,
    [MainAccountNumber]    VARCHAR (50)    NOT NULL,
    [ThirdPartyNit]        VARCHAR (20)    NULL,
    [CostCenterCode]       VARCHAR (20)    NULL,
    [Nature]               TINYINT         NOT NULL,
    [Detail]               VARCHAR (MAX)   NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    [RetentionConceptCode] VARCHAR (20)    NULL,
    [BillingValue]         DECIMAL (18, 2) NULL,
    [BaseValue]            DECIMAL (18, 2) NULL,
    CONSTRAINT [PK__InitialB__3214EC07DCBBFCD2] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base o valor neto (DECIMAL 18,2) usado para cálculo de retenciones y tributos en asientos contables iniciales', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'BaseValue', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor facturado o monto total facturado (DECIMAL 18,2) registrado en el asiento contable inicial del mayor general', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor facturado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'BillingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del concepto de retención (VARCHAR 20): retención en la fuente, IVA, otros impuestos aplicables al movimiento contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'RetentionConceptCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo concepto de retención', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'RetentionConceptCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'RetentionConceptCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto del asiento contable inicial (DECIMAL 18,2): débito o crédito según naturaleza en el mayor general', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle, descripción o nota explicativa (VARCHAR MAX) del asiento contable inicial, referencia a documento o justificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza contable: tipo de movimiento (TINYINT): 1=Débito, 2=Crédito en el asiento inicial del mayor', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza (Débito = 1, Crédito = 2)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de costo (VARCHAR 20): identificador de la unidad funcional o área que genera el movimiento contable inicial', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'CostCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo centro de cosyo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'CostCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'CostCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT o identificación del tercero (VARCHAR 20): código del proveedor, paciente, asegurador u otra entidad relacionada al movimiento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'ThirdPartyNit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'ThirdPartyNit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'ThirdPartyNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código de cuenta principal (VARCHAR 50): cuenta contable destino del movimiento en el mayor general', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'MainAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de cuenta principal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'MainAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'MainAccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del libro contable legal (VARCHAR 50): identificador del libro mayor, diario o registro según normativa fiscal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'LegalBookCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código libro legal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'LegalBookCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'LegalBookCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID autoincremental primario (INT IDENTITY): identificador único secuencial de cada registro de saldo inicial', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldos iniciales del libro mayor contable. Registra los valores de apertura por cuenta contable, tercero (NIT), centro de costo y libro legal, usados para inicializar el período contable en el ERP.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance';
