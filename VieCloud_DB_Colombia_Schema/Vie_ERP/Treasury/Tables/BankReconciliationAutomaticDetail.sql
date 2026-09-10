CREATE TABLE [Treasury].[BankReconciliationAutomaticDetail] (
    [Id]                                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankReconciliationAutomaticId]       INT             NOT NULL,
    [DocumentType]                        TINYINT         NOT NULL,
    [Nature]                              TINYINT         NOT NULL,
    [Value]                               DECIMAL (18, 2) NOT NULL,
    [EntityId]                            INT             NOT NULL,
    [EntityCode]                          VARCHAR (20)    NOT NULL,
    [EntityName]                          VARCHAR (250)   NOT NULL,
    [Reconciled]                          BIT             NOT NULL,
    [Comments]                            VARCHAR (100)   NULL,
    [ReconciledStatus]                    TINYINT         NULL,
    [BankReconciliationAutomaticOriginId] INT             NULL,
    [DocumentDate]                        DATE            NOT NULL,
    [IsInitialBalance]                    BIT             CONSTRAINT [DF_BankReconciliationAutomaticDetail_IsInitialBalance] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_BankReconciliationDetail_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankReconciliationAutomaticDetail_BankReconciliationAutomatic] FOREIGN KEY ([BankReconciliationAutomaticId]) REFERENCES [Treasury].[BankReconciliationAutomatic] ([Id]),
    CONSTRAINT [FK_BankReconciliationAutomaticDetail_BankReconciliationAutomaticOrigin] FOREIGN KEY ([BankReconciliationAutomaticOriginId]) REFERENCES [Treasury].[BankReconciliationAutomatic] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha calendario (DATE) del documento de origen, criterio de ordenamiento y validación temporal', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK nullable: Id de conciliación anterior donde quedó pendiente/sin conciliar, vincula regitros en transición', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticOriginId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la conciliación bancaria original donde quedó pendiente por Conciliar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticOriginId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticOriginId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del detalle: 1=Pendiente por conciliar, 2=Descartado (TINYINT, nullable)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'ReconciledStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del detalle en el proceso de Conciliación: 1. Pendiente por Conciliar.  2. Descartado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'ReconciledStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'ReconciledStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo texto (VARCHAR 100) para anotaciones y observaciones que facilitan el cierre de conciliación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información actualizada para posteriormente realiza el cierre de la conciliación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el registro coincide/existe en el extracto bancario actual (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el registro se encuentra o no en el extracto bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del documento de origen (VARCHAR 250), información visual del comprobante', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (VARCHAR 20) del documento de origen, referencia para auditoría y trazabilidad', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK: Identificador del documento de origen (recibo, egreso, nota o consignación) que genera el movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto o valor del movimiento en pesos (DECIMAL 18,2), susceptible a validación contra extracto bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza contable del movimiento: 1=Débito (salida), 2=Crédito (entrada) (TINYINT enumerado)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza del movimiento  1 - Debito  2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del documento: 1=Recibo de caja, 2=Comprobante de egreso, 3=Notas, 4=Consignaciones (TINYINT enumerado)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del documento   1 - Recibo de caja  2 - Comprobante de egreso  3 - Notas  4 - Consignaciones', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK: Id de la conciliación bancaria automática padre a la que pertenece este detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la conciliación bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro detalle en la conciliación bancaria automática', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT, default 0) que indica si el detalle corresponde a una partida de saldo inicial importada desde Excel (documento confirmado anterior a la existencia de la conciliación automática), en lugar de un documento hallado por el proceso mensual normal', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'IsInitialBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la partida es un saldo inicial importado manualmente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'IsInitialBalance';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la conciliación bancaria automática: registra cada movimiento o documento (débito/crédito) identificado en el proceso automático de conciliación, indicando si ya fue conciliado, su valor, la entidad relacionada y el estado del cruce con el extracto bancario.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail';
