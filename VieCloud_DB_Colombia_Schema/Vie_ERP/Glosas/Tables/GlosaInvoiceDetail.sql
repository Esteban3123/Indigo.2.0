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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP SQL Server) del evento de creación, registro o modificación del detalle de glosa. Registra el instante exacto de cambio en el sistema.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de ingresos (PUC) asignada al área de servicio para registro contable de facturación y glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'AccountantAccountIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cuenta contable de area de servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'AccountantAccountIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'AccountantAccountIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de procedimiento: 1=No quirúrgico, 2=Quirúrgico, 3=Paquete, 4=No Aplica. Clasificación para facturación y glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TypeProcedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: No quirurgico 2: Quirurgioc 3: Paqute 4: No Aplica', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TypeProcedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TypeProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio o producto: 1=Servicios IPS, 2=Inventario. Diferencia entre prestación de salud e insumo/medicamento.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TypeServiceProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: ServiciosIPS 2:Inventario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TypeServiceProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TypeServiceProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del centro de costo (unidad funcional, área, departamento) que prestó el servicio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'CostCenterName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del centro de costo.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'CostCenterName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'CostCenterName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único autonumérico identificador del centro de costo para trazabilidad en glosa y facturación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'CostCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico del centro de costo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'CostCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'CostCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad, número de unidades o actos del servicio prestado facturable y objetable en glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Ammount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Ammount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Ammount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total facturado del renglón (cantidad × valor unitario). Monto sujeto a glosa en controversia.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoicedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoicedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoicedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario o tarifa individual del servicio aplicada en facturación y revisión de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Unitario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del servicio registrado manualmente, diferente a tarifa automática. Usado en facturación no estándar.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ValueServiceManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor servicio manual', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ValueServiceManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ValueServiceManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del grupo de facturación que agrupa servicios afines para gestión de cobro y glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillingGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre grupo de facturacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillingGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillingGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del grupo de facturación para clasificación de rubros facturables.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillingGroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo grupo de facturacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillingGroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillingGroupCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del profesional, entidad o departamento responsable de la facturación del servicio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre del facturardor', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del facturador (puede ser profesional, unidad funcional o tercero) para auditoría de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillerCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del facturador', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillerCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BillerCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del médico, profesional de la salud o especialista que ejecutó el servicio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre del medico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del profesional de la salud que prestó el servicio (RP, cédula, matrícula).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de medico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre del área de servicio (urgencia, cirugía, laboratorio, imagenología, etc.).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'DescriptionServiceArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre area de servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'DescriptionServiceArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'DescriptionServiceArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del área de servicio para clasificación clínica y operativa en glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Area de servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del servicio, procedimiento, examen o prestación facturable y sujeta a glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del servicio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código estándar del servicio (SOAT, RIPS, interno) para identificación en facturación y glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del servicio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se ejecutó/prestó el servicio objeto de la factura y eventual glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha del servicio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de orden de servicio. Solo se registra en integración nativa, NULL si viene de fuente externa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del detalle de la orden de servicio - Se registra cuando la integracion es Nativa de lo contrario quedara en null', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del detalle de factura original desde tabla [Billing].[InvoiceDetail] en integración nativa. NULL si no aplica.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceDetailNativeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de factura de la tabla  [Billing].[InvoiceDetail] que se ingresa para integracion de modo nativo de lo contrario este quedara en NULL', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceDetailNativeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceDetailNativeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura que contiene este detalle de servicio glosado o en controversia.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico de la recepción de objeción/glosa asociada (FK a [Glosas].[GlosaObjectionsReceptionD]).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ObjectionsReceptionDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de Detalle de objecion de recepciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ObjectionsReceptionDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ObjectionsReceptionDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único del registro detalle de factura en glosa para auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de detalle de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los ítems de factura asociados a una recepción de glosas. Registra cada servicio, procedimiento o medicamento facturado que puede ser objeto de glosa o devolución por parte del pagador (aseguradora, EPS, etc.).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaInvoiceDetail';
