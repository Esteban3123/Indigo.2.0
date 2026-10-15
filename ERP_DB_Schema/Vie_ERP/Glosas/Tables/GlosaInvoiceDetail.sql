CREATE TABLE [Glosas].[GlosaInvoiceDetail] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ObjectionsReceptionDId]  INT           NOT NULL,
    [InvoiceNumber]           VARCHAR (50)  NOT NULL,
    [InvoiceDetailNativeId]   INT           NULL,
    [ServiceOrderDetailId]    INT           NULL,
    [ServiceDate]             DATETIME      NULL,
    [ServiceCode]             VARCHAR (20)  NOT NULL,
    [ServiceName]             VARCHAR (250) NOT NULL,
    [ServiceAreaCode]         VARCHAR (10)  NULL,
    [DescriptionServiceArea]  VARCHAR (300) NULL,
    [MedicalCode]             VARCHAR (20)  NULL,
    [MedicalName]             VARCHAR (200) NULL,
    [BillerCode]              VARCHAR (20)  NULL,
    [BillerName]              VARCHAR (200) NULL,
    [BillingGroupCode]        VARCHAR (50)  NULL,
    [BillingGroup]            VARCHAR (120) NULL,
    [ValueServiceManual]      MONEY         NULL,
    [UnitValue]               MONEY         NOT NULL,
    [InvoicedValue]           MONEY         NULL,
    [Ammount]                 INT           NOT NULL,
    [CostCenterCode]          VARCHAR (14)  NOT NULL,
    [CostCenterName]          VARCHAR (500) NOT NULL,
    [TypeServiceProduct]      CHAR (1)      NULL,
    [TypeProcedure]           CHAR (1)      NULL,
    [AccountantAccountIncome] VARCHAR (30)  NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    CONSTRAINT [PK_GlosaInvoiceDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaInvoiceDetail_GlosaObjectionsReceptionD] FOREIGN KEY ([ObjectionsReceptionDId]) REFERENCES [Glosas].[GlosaObjectionsReceptionD] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaInvoiceDetail] NOCHECK CONSTRAINT [FK_GlosaInvoiceDetail_GlosaObjectionsReceptionD];


GO
CREATE NONCLUSTERED INDEX [IX_GID_ByObjectionD]
    ON [Glosas].[GlosaInvoiceDetail]([ObjectionsReceptionDId] ASC)
    INCLUDE([InvoiceNumber], [ServiceCode], [ServiceName], [CostCenterCode], [CostCenterName]);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaInvoiceDetail_InvoiceNumber_AccountantAccountIncome_Ammount_BillerCode_BillerName_BillingGroup_BillingGroupCode]
    ON [Glosas].[GlosaInvoiceDetail]([InvoiceNumber] ASC)
    INCLUDE([AccountantAccountIncome], [Ammount], [BillerCode], [BillerName], [BillingGroup], [BillingGroupCode], [CostCenterCode], [CostCenterName], [DescriptionServiceArea], [InvoiceDetailNativeId], [InvoicedValue], [MedicalCode], [MedicalName], [ObjectionsReceptionDId], [ServiceAreaCode], [ServiceCode], [ServiceDate], [ServiceName], [ServiceOrderDetailId], [TimeStamp], [TypeProcedure], [TypeServiceProduct], [UnitValue], [ValueServiceManual]);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaInvoiceDetail_ValueServiceManual_UnitValue_Ammount]
    ON [Glosas].[GlosaInvoiceDetail]([ValueServiceManual] ASC, [UnitValue] ASC, [Ammount] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaInvoiceDetail_ServiceCode_ServiceName]
    ON [Glosas].[GlosaInvoiceDetail]([ServiceCode] ASC, [ServiceName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaInvoiceDetail_InvoiceValue_InvoiceNumber]
    ON [Glosas].[GlosaInvoiceDetail]([InvoicedValue] ASC, [InvoiceNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaInvoiceDetail__ObjectionsReceptionDId__INC__AccountantAccountIncome__Ammount__BillerCode__BillerName__BillingGroup__Bill]
    ON [Glosas].[GlosaInvoiceDetail]([ObjectionsReceptionDId] ASC)
    INCLUDE([AccountantAccountIncome], [Ammount], [BillerCode], [BillerName], [BillingGroup], [BillingGroupCode], [CostCenterCode], [CostCenterName], [DescriptionServiceArea], [Id], [InvoicedValue], [InvoiceNumber], [MedicalCode], [MedicalName], [ServiceAreaCode], [ServiceCode], [ServiceDate], [ServiceName], [TimeStamp], [TypeProcedure], [TypeServiceProduct], [UnitValue], [ValueServiceManual]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuenta contable de area de servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'AccountantAccountIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: No quirurgico 2: Quirurgioc 3: Paqute 4: No Aplica', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TypeProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: ServiciosIPS 2:Inventario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TypeServiceProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del centro de costo.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'CostCenterName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico del centro de costo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'CostCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Ammount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoicedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Unitario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor servicio manual', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ValueServiceManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre grupo de facturacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillingGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo grupo de facturacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillingGroupCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre del facturardor', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo del facturador', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillerCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre del medico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de medico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre area de servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'DescriptionServiceArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Area de servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del servicio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha del servicio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del detalle de la orden de servicio - Se registra cuando la integracion es Nativa de lo contrario quedara en null', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de factura de la tabla  [Billing].[InvoiceDetail] que se ingresa para integracion de modo nativo de lo contrario este quedara en NULL', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceDetailNativeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de Detalle de objecion de recepciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ObjectionsReceptionDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de detalle de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';

