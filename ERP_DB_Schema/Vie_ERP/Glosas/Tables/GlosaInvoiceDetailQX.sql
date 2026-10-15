CREATE TABLE [Glosas].[GlosaInvoiceDetailQX] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceDetailId]              INT           NOT NULL,
    [ServiceOrderDetailSurgicalId] INT           NULL,
    [ServiceCode]                  VARCHAR (50)  NOT NULL,
    [ServiceName]                  VARCHAR (300) NOT NULL,
    [MedicalCode]                  VARCHAR (20)  NULL,
    [MedicalName]                  NCHAR (200)   NULL,
    [ValueServiceManual]           MONEY         NOT NULL,
    [UnitValue]                    MONEY         NOT NULL,
    [InvoicedValue]                MONEY         NOT NULL,
    [Ammount]                      INT           NOT NULL,
    [CostCenterCode]               VARCHAR (100) NOT NULL,
    [CostCenterName]               NCHAR (500)   NOT NULL,
    [ServiceAreaCode]              VARCHAR (10)  NOT NULL,
    [DescriptionServiceArea]       NCHAR (300)   NOT NULL,
    [AccountantAccountIncome]      VARCHAR (30)  NOT NULL,
    [TimeStamp]                    ROWVERSION    NOT NULL,
    CONSTRAINT [PK_GlosaInvoiceDetailQX] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaInvoiceDetailQX_InvoiceDetail] FOREIGN KEY ([InvoiceDetailId]) REFERENCES [Glosas].[GlosaInvoiceDetail] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaInvoiceDetailQX] NOCHECK CONSTRAINT [FK_GlosaInvoiceDetailQX_InvoiceDetail];


GO
CREATE NONCLUSTERED INDEX [IX_GlosaInvoiceDetailQX_ServiceCode_ServiceName]
    ON [Glosas].[GlosaInvoiceDetailQX]([ServiceCode] ASC, [ServiceName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaInvoiceDetailQX_InvoiceValue_InvoiceNumber]
    ON [Glosas].[GlosaInvoiceDetailQX]([InvoicedValue] ASC, [InvoiceDetailId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaInvoiceDetailQX_InvoiceDetailId_[AccountantAccountIncome_Ammount_CostCenterCode_CostCenterName_[DescriptionServiceArea]
    ON [Glosas].[GlosaInvoiceDetailQX]([InvoiceDetailId] ASC)
    INCLUDE([AccountantAccountIncome], [Ammount], [CostCenterCode], [CostCenterName], [DescriptionServiceArea], [InvoicedValue], [MedicalCode], [MedicalName], [ServiceAreaCode], [ServiceCode], [ServiceName], [ServiceOrderDetailSurgicalId], [TimeStamp], [UnitValue], [ValueServiceManual]);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaInvoiceDetailQX_ValueServiceManual_UnitValue_Ammount]
    ON [Glosas].[GlosaInvoiceDetailQX]([ValueServiceManual] ASC, [UnitValue] ASC, [Ammount] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuenta contable area de atencion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'AccountantAccountIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre area de atencion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'DescriptionServiceArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo area atencion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre centro de costo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'CostCenterName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo centro de costo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'CostCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'Ammount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'InvoicedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor unitario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor servicio manual', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ValueServiceManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre de medico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'MedicalName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de medico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'MedicalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre de servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio quirurgico de la tabla [ServiceOrderDetailSurgical] que se ingresa para integracion de modo nativo de lo contrario este quedara en NULL', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion detalle de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico detalle de qx', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'Id';

