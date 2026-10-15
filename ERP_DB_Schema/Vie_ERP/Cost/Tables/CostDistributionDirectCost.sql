CREATE TABLE [Cost].[CostDistributionDirectCost] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)    NOT NULL,
    [GeneralExpenseId]             INT             NOT NULL,
    [Description]                  VARCHAR (300)   NOT NULL,
    [Year]                         INT             NOT NULL,
    [Month]                        INT             NOT NULL,
    [Value]                        NUMERIC (20, 2) NOT NULL,
    [Status]                       TINYINT         NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [TimeStamp]                    ROWVERSION      NOT NULL,
    [ThirdPartyId]                 INT             NOT NULL,
    [ConfirmUser]                  VARCHAR (20)    NULL,
    [ConfirmDate]                  DATETIME        NULL,
    [AnnulmentUser]                VARCHAR (20)    NULL,
    [AnnulmentDate]                DATETIME        NULL,
    [Observation]                  VARCHAR (500)   CONSTRAINT [DF_CostDistributionDirectCost_Observation] DEFAULT ('') NOT NULL,
    [BillNumber]                   VARCHAR (100)   CONSTRAINT [DF__CostDistr__BillN__11093764] DEFAULT ('') NOT NULL,
    [AccountPayableId]             INT             NULL,
    [OperatingUnitId]              INT             NULL,
    [SupplierId]                   INT             NULL,
    [SuppliersDistributionLinesId] INT             NULL,
    [Term]                         INT             NULL,
    [CostCenterId]                 INT             NULL,
    [FilingUnitId]                 INT             NULL,
    [SupplierTypeId]               INT             NULL,
    [BillDate]                     DATE            NULL,
    [MainAccountId]                INT             NULL,
    [ServicePeriodDate]            DATE            NULL,
    [PositionId]                   INT             NULL,
    [Hours]                        INT             NULL,
    [DeductibleIva]                BIT             NULL,
    [CurrencyId]                   INT             CONSTRAINT [DF__CostDistr__Curre__0ECA5289] DEFAULT ((1)) NOT NULL,
    [ProvisionDocument]            BIT             CONSTRAINT [DF__CostDistr__Provi__09517055] DEFAULT ((0)) NOT NULL,
    [AccountPayableSameSupplier]   BIT             CONSTRAINT [DF__CostDistr__Accou__50F80899] DEFAULT ((0)) NOT NULL,
    [TaxRegistration]              TINYINT         NULL,
    [DocumentDate]                 DATE            NULL,
    CONSTRAINT [PK_CostDistributionDirectCost__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCost_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_CostGeneralExpense] FOREIGN KEY ([GeneralExpenseId]) REFERENCES [Cost].[CostGeneralExpense] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_SuppliersDistributionLines] FOREIGN KEY ([SuppliersDistributionLinesId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_CostDistributionDirectCost__Id__GeneralExpenseId__Year__Month] UNIQUE NONCLUSTERED ([Id] ASC, [GeneralExpenseId] ASC, [Year] ASC, [Month] ASC)
);


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_AccountPayable];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_CostCenter];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_CostGeneralExpense];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_Currency];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_FilingUnit];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_MainAccounts];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_OperatingUnit];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_Position];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_Supplier];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_SuppliersDistributionLines];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_SupplierType];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_ThirdParty];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CostDistributionDirectCost]
    ON [Cost].[CostDistributionDirectCost]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento cuando No se genera CxP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Iva parametrizado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si/No para cuando se crea la cxp si los detalles van al mismo proveedor o al tercero puesto en ellos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'AccountPayableSameSupplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si es un documento de provision', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'ProvisionDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Moneda del documento', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica el parametro de IVA descontable', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'DeductibleIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas laboradas para la creación de la cuenta por pagar si se maneja un cargo (empleado independiente)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'Hours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cargo asociado al proveedor para la linea de distribucion seleccionada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha radicación para la creación de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'ServicePeriodDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar asociada al proveedor con su respectiva linea de distribucion para la creación de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Factura para la creación de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'BillDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de proveedor para la creación de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Unidad de radicación para la creación de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'FilingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro del costo, si la cuenta contable lo requiere para la creación de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plazo (Dias) para la creación de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la linea de distribución asociada al proveedor', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'SuppliersDistributionLinesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Proveedor', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factura correspondiente a la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'BillNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones que se le haga a la distribución por parte del usuario', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'ConfirmDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'ConfirmUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad   1 - Registrado   2 - Confirmado   3 - Anulado   4 - Reversado   5 - Confirmado Sin Legalizar   6 - Confirmado Legalizado ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Total que se va a distribuir', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes de la distribucion del gasto directo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año de la distribucion del gasto directo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Elemento del costo seleccionado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del elemento del costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'GeneralExpenseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la distribución', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la distribucion de elemento del costo (Gastos Directos)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'Id';

