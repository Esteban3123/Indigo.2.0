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
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_AccountPayable];


GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por pagar (CxP) generada tras radicación, INT, FK a Payments.AccountPayable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar generada', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas o comentarios adicionales sobre la cuenta por pagar, VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Coments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Coments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Coments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de la factura de la cuenta por pagar (DECIMAL 18,2), se completa típicamente en radicación de CxP', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de la factura de la cuenta por pagar, Este campo por lo general se llena en la radicacion de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo o días de pago acordados con el proveedor, INT, define vencimiento de la obligación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plazo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de proveedor (padre-hijo jerárquico), solo nivel más bajo, INT, FK a Common.SupplierType', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de proveedor, este se obtiene de la tabla SupplierDetailType pero en esta tabla solo estan los padres y en este campos solo se pueden seleccionar los hijos de ultimo nivel de esos registros, es decir que el tipo de proveedor que seleccionen debe estar en el ultimo nivel en la jerarquia', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de radicación donde se registra la CxP, INT, FK a Payments.FilingUnit', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'FilingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de radicacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'FilingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'FilingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión de la factura del proveedor, DATETIME', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'BillDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'BillDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'BillDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de elaboración o validación del documento de soporte, DATETIME', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o referencia de la factura del proveedor, VARCHAR(100), clave para auditoría RIPS', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'BillNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'BillNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'BillNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo asignado a la CxP, INT, FK a Payroll.CostCenter, nullable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la línea de distribución contable, INT, FK a Common.DistributionLines', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'DistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de línea de distribución', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'DistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'DistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del proveedor (empresa, profesional, fabricante), INT, FK a Common.Supplier', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cargue masivo o lote de cuentas por pagar importadas, INT, FK a Payments.LoadMassive', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'LoadMassiveId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cargue masivo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'LoadMassiveId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'LoadMassiveId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de carga masiva de CxP, INT IDENTITY', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el detalle de las cuentas por pagar cargadas de forma masiva a proveedores, incluyendo los valores de factura, fechas, centro de costos y la cuenta por pagar resultante de cada línea procesada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayable';
