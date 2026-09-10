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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento: fecha y hora exacta de creación, registro o modificación del detalle de glosa quirúrgica.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (VARCHAR 30) del área de atención para registro contable de ingresos de la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'AccountantAccountIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cuenta contable area de atencion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'AccountantAccountIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'AccountantAccountIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre del área de atención (unidad funcional) que realizó el servicio glozado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'DescriptionServiceArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre area de atencion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'DescriptionServiceArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'DescriptionServiceArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (VARCHAR 10) del área de atención, unidad funcional o centro de atención que brindó el servicio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo area atencion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del centro de costo (NCHAR 500) asociado al servicio glozado para control presupuestal.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'CostCenterName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre centro de costo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'CostCenterName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'CostCenterName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (VARCHAR 100) del centro de costo donde se originó el servicio o procedimiento glozado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'CostCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo centro de costo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'CostCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'CostCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de unidades (INT) del servicio prestado en el detalle de glosa quirúrgica.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'Ammount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'Ammount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'Ammount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total facturado (MONEY) del servicio en el detalle de glosa: cantidad × valor unitario.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'InvoicedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'InvoicedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'InvoicedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario (MONEY) del servicio o procedimiento por cada unidad prestada en la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor unitario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del servicio ajustado manualmente (MONEY) ingresado por usuario en el detalle de glosa quirúrgica.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ValueServiceManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor servicio manual', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ValueServiceManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ValueServiceManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del médico, cirujano o profesional de la salud (NCHAR 200) que ejecutó el servicio glozado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'MedicalName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre de medico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'MedicalName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'MedicalName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (VARCHAR 20) del médico, cirujano o profesional de la salud responsable del procedimiento.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'MedicalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de medico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'MedicalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'MedicalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción (VARCHAR 300) del servicio, procedimiento o prestación glozada.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre de servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (VARCHAR 50) del servicio, procedimiento, acto médico o prestación incluida en la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, nullable) de relación con tabla [ServiceOrderDetailSurgical] para integración nativa de órdenes quirúrgicas; NULL si no existe.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio quirurgico de la tabla [ServiceOrderDetailSurgical] que se ingresa para integracion de modo nativo de lo contrario este quedara en NULL', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del detalle de factura en tabla [GlosaInvoiceDetail] que origina el registro de glosa quirúrgica.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion detalle de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) del detalle de glosa quirúrgica en la tabla.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico detalle de qx', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de servicios quirúrgicos incluidos en las facturas sujetas a glosa. Registra cada procedimiento o servicio de sala de cirugía facturado, con sus valores, cantidades, centro de costo y área de servicio, para el proceso de auditoría y conciliación de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetailQX';
