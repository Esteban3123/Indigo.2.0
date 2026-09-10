CREATE TABLE [Treasury].[TreasuryBalance] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentNumber]      VARCHAR (20)    NOT NULL,
    [DocumentDate]        DATETIME        NOT NULL,
    [DocumentType]        TINYINT         NOT NULL,
    [Nature]              TINYINT         NOT NULL,
    [CashRegisterId]      INT             NULL,
    [EntityBankAccountId] INT             NULL,
    [PreviousBalance]     DECIMAL (18, 2) NOT NULL,
    [ValueMovement]       DECIMAL (18, 2) NOT NULL,
    [CreationDate]        DATETIME        CONSTRAINT [DF_TreasuryBalance_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_TreasuryBalance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TreasuryBalance_CashRegisters] FOREIGN KEY ([CashRegisterId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_TreasuryBalance_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TreasuryBalance]
    ON [Treasury].[TreasuryBalance]([DocumentNumber] ASC, [DocumentType] ASC, [Nature] ASC, [CashRegisterId] ASC, [EntityBankAccountId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro en el sistema. Marcatimestamp automático por defecto [Common].[getdate()]. Auditoría de cuándo se registró.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de la creacion del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importe (DECIMAL 18,2) del movimiento de tesorería en caja o cuenta bancaria. Débito resta, crédito suma al saldo anterior.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'ValueMovement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'ValueMovement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'ValueMovement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo anterior (DECIMAL 18,2) de caja o cuenta bancaria antes del movimiento. Saldo actual = PreviousBalance + ValueMovement (ajustado por naturaleza).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'PreviousBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo anterior de la caja o cuenta bancaria, el saldo actual se calcularia sumando el saldo anterior y el valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'PreviousBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'PreviousBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cuenta bancaria donde se ejecutó el movimiento. Referencia a [Treasury].[EntityBankAccounts]. Puede ser NULL si es movimiento de caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta bancaria donde se realizo el movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la caja registradora donde se realizó el movimiento. Referencia a [Treasury].[CashRegisters]. Puede ser NULL si es cuenta bancaria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'CashRegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la caja donde se realizo el movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'CashRegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'CashRegisterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza del movimiento (TINYINT): 1=Débito (salida de dinero), 2=Crédito (entrada de dinero). Determina si resta o suma al saldo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza del documento que genera movimiento, 1 - Debito, 2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento (TINYINT): 1=Recibo de Caja, 2=Comprobante de Egreso, 3=Consignaciones, 4=Notas, 5=Fondo de Caja Menor. Genera el movimiento contable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del Documento que genera el movimiento:  1 - Recibo de Caja  2 - Comrpobante de Egreso  3 - Consignaciones  4 - Notas  5 - Fondo de Caja Menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del documento contable que origina el movimiento de caja o cuenta bancaria. Diferente a fecha de creación del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento que genera el movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador único (VARCHAR 20) del documento que genera el movimiento. Búsquedas: recibo, comprobante, consignación, nota, fondo caja menor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del documento que genera el movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de saldo y movimiento en tesorería. Clave primaria clustered de la tabla TreasuryBalance.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro de saldos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de movimientos y saldos de tesorería. Cada fila representa un movimiento contable (ingreso o egreso) asociado a un documento de caja o banco, reflejando el saldo anterior y el valor del movimiento para mantener el balance de caja y cuentas bancarias de la institución.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance';
