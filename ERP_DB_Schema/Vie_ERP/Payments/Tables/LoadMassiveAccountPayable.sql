CREATE TABLE [Payments].[LoadMassiveAccountPayable] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoadMassiveId]      INT             NOT NULL,
    [SupplierId]         INT             NOT NULL,
    [DistributionLineId] INT             NOT NULL,
    [CostCenterId]       INT             NULL,
    [BillNumber]         VARCHAR (100)   NOT NULL,
    [DocumentDate]       DATETIME        NOT NULL,
    [BillDate]           DATETIME        NOT NULL,
    [FilingUnitId]       INT             NOT NULL,
    [SupplierTypeId]     INT             NOT NULL,
    [Term]               INT             NOT NULL,
    [InvoiceValue]       DECIMAL (18, 2) NOT NULL,
    [Coments]            VARCHAR (MAX)   NULL,
    [AccountPayableId]   INT             NULL,
    CONSTRAINT [PK_Payments_LoadMassiveAccountPayable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoadMassiveAccountPayable_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_DistributionLine] FOREIGN KEY ([DistributionLineId]) REFERENCES [Common].[DistributionLines] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_LoadMassive] FOREIGN KEY ([LoadMassiveId]) REFERENCES [Payments].[LoadMassive] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id])
);


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_AccountPayable];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_CostCenter];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_DistributionLine];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_FilingUnit];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_LoadMassive];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_Supplier];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_SupplierType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar generada', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Coments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de la factura de la cuenta por pagar, Este campo por lo general se llena en la radicacion de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plazo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de proveedor, este se obtiene de la tabla SupplierDetailType pero en esta tabla solo estan los padres y en este campos solo se pueden seleccionar los hijos de ultimo nivel de esos registros, es decir que el tipo de proveedor que seleccionen debe estar en el ultimo nivel en la jerarquia', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de radicacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'FilingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'BillDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'BillNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion de línea de distribución', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'DistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cargue masivo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'LoadMassiveId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Id';

