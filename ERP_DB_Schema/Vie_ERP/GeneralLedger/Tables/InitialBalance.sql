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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'BaseValue', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor facturado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'BillingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo concepto de retención', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'RetentionConceptCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza (Débito = 1, Crédito = 2)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo centro de cosyo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'CostCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'ThirdPartyNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de cuenta principal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'MainAccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código libro legal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'LegalBookCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'InitialBalance', @level2type = N'COLUMN', @level2name = N'Id';

