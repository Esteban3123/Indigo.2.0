CREATE TABLE [Treasury].[BankReconciliationDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankReconciliationId] INT             NOT NULL,
    [DocumentType]         TINYINT         NOT NULL,
    [Nature]               TINYINT         NOT NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    [EntityId]             INT             NOT NULL,
    [EntityCode]           VARCHAR (20)    NOT NULL,
    [EntityName]           VARCHAR (250)   NOT NULL,
    [Reconciled]           BIT             NOT NULL,
    CONSTRAINT [PK_BankReconciliationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankReconciliationDetail_BankReconciliation] FOREIGN KEY ([BankReconciliationId]) REFERENCES [Treasury].[BankReconciliation] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que señala si el registro de movimiento está conciliado y presente en el extracto bancario. Valores: 1=Conciliado/Verificado en banco, 0=Pendiente de conciliación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el registro se encuentra o no en el extracto bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del documento de origen (recibo, egreso, nota, consignación o fondo). VARCHAR(250), identifica el comprobante que generó el movimiento bancario.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del documento de origen (ej: número de recibo, comprobante de egreso, referencia de consignación). VARCHAR(20), clave para búsqueda de transacciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del documento de origen en su tabla respectiva. Clave foránea implícita que vincula a recibos, egresos, notas o consignaciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto decimal del movimiento bancario (DECIMAL 18,2). Representa debito o crédito según la columna Nature. Dinero movido en la transacción.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza del movimiento bancario (TINYINT): 1=Débito (salida de dinero), 2=Crédito (entrada de dinero). Clasifica si es ingreso o gasto.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza del movimiento  1 - Debito  2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante que originó el movimiento (TINYINT): 1=Recibo de caja, 2=Comprobante de egreso, 3=Notas, 4=Consignaciones, 5=Fondo de caja menor. Categoriza el documento origen.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del documento   1 - Recibo de caja  2 - Comprobante de egreso  3 - Notas  4 - Consignaciones  5 - Fondo de Caja Menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la conciliación bancaria padre. Clave foránea (FK) que vincula al registro [Treasury].[BankReconciliation]. Agrupa detalles de una misma reconciliación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la conciliación bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de detalle de conciliación. PRIMARY KEY CLUSTERED. IDENTITY(1,1). Clave técnica del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada movimiento incluido en una conciliación bancaria: registra si el ítem (cheque, depósito, transferencia u otro documento) ya fue conciliado, su valor, naturaleza contable y la entidad relacionada (proveedor, cliente, banco, etc.).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail';
