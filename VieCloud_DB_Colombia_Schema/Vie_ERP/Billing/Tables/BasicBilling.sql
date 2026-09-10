CREATE TABLE [Billing].[BasicBilling] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)    NOT NULL,
    [DocumentDate]                 DATETIME        NOT NULL,
    [Description]                  VARCHAR (MAX)   NOT NULL,
    [SaleModality]                 TINYINT         NOT NULL,
    [Status]                       TINYINT         NOT NULL,
    [OperatingUnitId]              INT             NOT NULL,
    [BillingAuthorizationId]       INT             NOT NULL,
    [FunctionalUnitId]             INT             NULL,
    [CustomerId]                   INT             NOT NULL,
    [AddressId]                    INT             NOT NULL,
    [WarehouseId]                  INT             NULL,
    [InvoiceId]                    INT             NULL,
    [Value]                        DECIMAL (18, 2) NOT NULL,
    [ValueDiscount]                DECIMAL (18, 2) NOT NULL,
    [ValueIVA]                     DECIMAL (18, 2) NOT NULL,
    [WithholdingTax]               DECIMAL (18, 2) NOT NULL,
    [RetentionIdIVA]               INT             NULL,
    [RetentionPercentageIVA]       DECIMAL (6, 3)  NOT NULL,
    [WithholdingIVA]               DECIMAL (18, 2) NOT NULL,
    [WithholdingICA]               DECIMAL (18, 2) NOT NULL,
    [TotalValue]                   DECIMAL (18, 2) NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [ConfirmationUser]             VARCHAR (20)    NULL,
    [ConfirmationDate]             DATETIME        NULL,
    [AnnulmentUser]                VARCHAR (20)    NULL,
    [AnnulmentDate]                DATETIME        NULL,
    [TimeStamp]                    ROWVERSION      NOT NULL,
    [RoundLevel]                   INT             NOT NULL,
    [BudgetId]                     INT             NULL,
    [ReversalReasonId]             INT             NULL,
    [ReversalReasonDescription]    VARCHAR (200)   NULL,
    [CurrencyId]                   INT             CONSTRAINT [DF__BasicBill__Curre__11A6BF34] DEFAULT ((2)) NOT NULL,
    [ThirdPartyEntityCopayId]      INT             NULL,
    [CommercialDiscountAccounting] BIT             NULL,
    [ConditionSalesId]             INT             NULL,
    [IsImported]                   BIT             CONSTRAINT [DF__BasicBill__IsImp__1266A49A] DEFAULT ((0)) NOT NULL,
    [EconomicActivityId]           INT             NULL,
    [IsMandateBilling]             BIT             CONSTRAINT [DF_BasicBilling_IsMandateBilling] DEFAULT ((0)) NOT NULL,
    [MandatorThirdPartyId]         INT             NULL,
    CONSTRAINT [PK_BasicBilling] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasicBilling_Address] FOREIGN KEY ([AddressId]) REFERENCES [Common].[Address] ([Id]),
    CONSTRAINT [FK_BasicBilling_BillingAuthorization] FOREIGN KEY ([BillingAuthorizationId]) REFERENCES [Billing].[BillingAuthorization] ([Id]),
    CONSTRAINT [FK_BasicBilling_BillingReversalReason] FOREIGN KEY ([ReversalReasonId]) REFERENCES [Billing].[BillingReversalReason] ([Id]),
    CONSTRAINT [FK_BasicBilling_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_BasicBilling_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_BasicBilling_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_BasicBilling_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_BasicBilling_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_BasicBilling_RetentionConcepts] FOREIGN KEY ([RetentionIdIVA]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_BasicBilling_ThirdPartyEntityCopay] FOREIGN KEY ([ThirdPartyEntityCopayId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_BasicBilling_MandatorThirdParty] FOREIGN KEY ([MandatorThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_BasicBilling_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_BillingReversalReason];




GO
ALTER TABLE [Billing].[BasicBilling] CHECK CONSTRAINT [FK_BasicBilling_Address];


GO



GO
ALTER TABLE [Billing].[BasicBilling] NOCHECK CONSTRAINT [FK_BasicBilling_BillingReversalReason];


GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_BasicBilling__Code]
    ON [Billing].[BasicBilling]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que indica si el registro de facturación básica fue creado mediante proceso de importación de datos externos. Valor por defecto: 0 (no importado).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'IsImported';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo booleano que determina verdadero si el registro se creo a partir de importación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'IsImported';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'IsImported';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la condición de venta aplicada a la factura: modalidad de pago, plazos y términos comerciales. Vinculado a tabla de condiciones de venta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ConditionSalesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la condicion de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ConditionSalesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ConditionSalesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que especifica si el descuento comercial está parametrizado en configuración de facturación para ser considerado en reversión/glosa de la factura básica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CommercialDiscountAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo booleano que especifica verdadero si la factura basica tiene parametrizado el descuento comercial en parametros de facturación para tenerlo en cuenta al momento de reversarla.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CommercialDiscountAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CommercialDiscountAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del tercero (asegurador, administrador, entidad) que genera el copago o participación financiera del afiliado. Referencia a tercero/entidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ThirdPartyEntityCopayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercero de la entidad administradora que genera el copago', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ThirdPartyEntityCopayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ThirdPartyEntityCopayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la moneda del documento de facturación (COP, USD, etc.). Valor por defecto: 2 (moneda local). Referencia a tabla [Common].[Currency].', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 200) del motivo por el cual se revierte o anula la factura básica (glosa, error, cambio de cobertura, etc.).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ReversalReasonDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ReversalReasonDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ReversalReasonDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del concepto/código de razón de reversión o anulación de la factura. Referencia a [Billing].[BillingReversalReason].', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ReversalReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de Razon de Reversión', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ReversalReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ReversalReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del presupuesto de ingresos asignado para reconocimiento contable y analítico de la factura básica. Referencia a [Budget].[Budget].', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del presupuesto de ingresos a usar en el reconocimiento de facturas básicas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel o cantidad de decimales (INT) para redondeo de cálculos monetarios en la factura. Define precisión: 0=enteros, 2=centavos, 3=milésimos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'RoundLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de redondeo usado, el numero indica la cantidad de decimales que se manejaran', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'RoundLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'RoundLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo (TIMESTAMP) generado automáticamente por SQL Server para auditoría y control de concurrencia. Última modificación del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de tiempo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que fue anulada/cancelada la factura básica. Nula si factura está vigente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Login/código (VARCHAR 20) del usuario que ejecutó la anulación de la factura. Nulo si factura no está anulada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que fue confirmada/validada la factura básica para contabilización. Auditoría de aprobación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Login/código (VARCHAR 20) del usuario autorizado que confirmó la factura básica. Responsable de validación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último cambio realizado al registro de facturación básica. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Login/código (VARCHAR 20) del usuario que realizó la última modificación de la factura. Trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de facturación básica en el sistema. Marca de generación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Login/código (VARCHAR 20) del usuario que creó el registro de facturación. Responsable de origen del documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total DECIMAL(18,2) de la factura: Value - ValueDiscount + ValueIVA - WithholdingTax - WithholdingIVA - WithholdingICA. Neto a pagar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Total del Documento el cual se obtiene (Value - ValueDiscount + ValueIVA -WithholdingTax - WithholdingIVA - WithholdingICA)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total DECIMAL(18,2) retenido por Impuesto de Comercio (ICA) municipal. Suma de retenciones ICA de todos los detalles de factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la Retencion de ICA, es la suma de todo el WithholdingICA de los detalles', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WithholdingICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total DECIMAL(18,2) retenido del IVA (Impuesto al Valor Agregado). Suma de retenciones IVA de detalles según concepto de retención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WithholdingIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la Retencion de IVA, es la suma de todo el WithholdingIVA de los detalles', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WithholdingIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WithholdingIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje DECIMAL(6,3) de retención aplicado al IVA: 0.000 a 100.000%. Define magnitud de retención en la fuente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'RetentionPercentageIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de Retención de IVA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'RetentionPercentageIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'RetentionPercentageIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del concepto/código de retención en IVA configurado. Referencia a [GeneralLedger].[RetentionConcepts].', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'RetentionIdIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Concepto Retencion de IVA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'RetentionIdIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'RetentionIdIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total DECIMAL(18,2) retenido en la fuente (gravamen nacional sobre ingresos). Suma de retenciones en fuente de detalles.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WithholdingTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la Retencion en la Fuente, es la suma de todo el WithholdingTax de los detalles', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WithholdingTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WithholdingTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto DECIMAL(18,2) del Impuesto al Valor Agregado (IVA) a cobrar. Suma de IVA de todos los detalles de la factura básica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ValueIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del IVA, es la suma de todo el ValueIVA de los detalles', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ValueIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ValueIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto DECIMAL(18,2) de descuento comercial u operativo aplicado. Suma de descuentos de todos los detalles de factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, es la suma del ValueDiscount de todos los detalles', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'ValueDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto subtotal DECIMAL(18,2) de la factura antes de impuestos y retenciones. Suma de valores de todos los detalles de línea.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la factura, es decir que es la suma del Value de todos los detalles', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la factura electrónica/documento fiscal generado desde esta facturación básica. Referencia a [Billing].[Invoice].', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Factura Generada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del almacén de origen/destino para control y movimiento de inventario asociado a la factura. Referencia a [Inventory].[Warehouse].', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Almacen para el control del inventario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de dirección del cliente/tercero usada para cálculo de retenciones ICA por ciudad de venta. Referencia a [Common].[Address].', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'AddressId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la dirección del tercero asociada a la factura, esto con la finalidad del calculo de las retenciones de ICA correspondientes a la ciudad de la venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'AddressId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'AddressId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del cliente/paciente/tercero facturado. Referencia a [Common].[Customer] - entidad principal facturada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Cliente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de unidad funcional (urgencias, hospitalización, consulta, laboratorio, etc.) que genera la atención facturada. Opcional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la autorización previa de facturación emitida. Referencia a [Billing].[BillingAuthorization] - permiso para facturar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Autorización de Facturación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de unidad operativa (sucursal, sede, centro de atención) responsable de la facturación. Referencia a [Common].[OperatingUnit].', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código TINYINT del estado actual de factura básica: 1=Generada, 2=Confirmada, 3=Anulada, 4=Reversada, etc. Estado del ciclo de vida.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Factura Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código TINYINT de modalidad de pago: 1=Contado (pago inmediato), 2=Crédito (plazo). Define condición de pago de la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'SaleModality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modalidad de Venta:       1. Contado      2. Crédito', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'SaleModality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'SaleModality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR MAX) del contenido, concepto o resumen de la facturación básica. Texto libre para documentación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Factura Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del documento de facturación. Fecha contable y fiscal de emisión de la factura básica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) de la factura básica. Identificador legible para búsqueda de comprobante de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Factura Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario (INT IDENTITY) único e incremental de cada registro en tabla [Billing].[BasicBilling]. Clave técnica del sistema.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro maestro de documentos de facturación básica (cotizaciones, prefacturas o facturas en borrador) que consolida los valores de venta, descuentos, impuestos (IVA, retenciones, ICA) y retenciones aplicadas a un cliente, asociando el documento a una unidad operativa, unidad funcional, bodega y presupuesto antes de su confirmación o anulación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad económica del cliente o del documento de facturación, usada para determinar la tarifa de retención de ICA u otros tributos según el giro del negocio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBilling', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';

GO
CREATE NONCLUSTERED INDEX [IX_BasicBilling_InvoiceId]
    ON [Billing].[BasicBilling]([InvoiceId] ASC)
    INCLUDE([Status], [ThirdPartyEntityCopayId]);
