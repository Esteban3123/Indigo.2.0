CREATE TABLE [Billing].[QuotationServiceOrderDetailDistribution] (
    [Id]                            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [QuotationServiceOrderDetailId] INT            NOT NULL,
    [Quantity]                      INT            NOT NULL,
    [GrandTotalSalesPrice]          NUMERIC (18)   NOT NULL,
    [GrandTotalDiscount]            NUMERIC (18)   CONSTRAINT [DF_QuotationServiceOrderDetailDistribution_GrandTotalDiscount] DEFAULT ((0)) NOT NULL,
    [DistributionType]              TINYINT        NOT NULL,
    [ThirdPartySalesPrice]          NUMERIC (18)   NOT NULL,
    [ThirdPartyPercentage]          NUMERIC (5, 2) NOT NULL,
    [ApplyRecoveryFee]              TINYINT        CONSTRAINT [DF_QuotationServiceOrderDetailDistribution_ApplyRecoveryFee] DEFAULT ((1)) NOT NULL,
    [RecoveryFeeType]               TINYINT        CONSTRAINT [DF_QuotationServiceOrderDetailDistribution_RecoveryFeeType] DEFAULT ((1)) NOT NULL,
    [SubTotalPatientSalesPrice]     NUMERIC (18)   NOT NULL,
    [PatientPercentage]             NUMERIC (5, 2) NOT NULL,
    [LastCaregroupId]               INT            CONSTRAINT [DF_QuotationServiceOrderDetailDistribution_LastCaregroupId] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QuotationServiceOrderDetailDistribution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuotationServiceOrderDetailDistribution_CareGroup] FOREIGN KEY ([LastCaregroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetailDistribution_QuotationServiceOrderDetail] FOREIGN KEY ([QuotationServiceOrderDetailId]) REFERENCES [Billing].[QuotationServiceOrderDetail] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo de atención (FK a CareGroup) usado como bandera/indicador de retarificación en proceso de distribución; no representa relación directa con grupo de atención, sino marcador de cambio tarifario aplicado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'LastCaregroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atención de la última retarificación. Éste campo es usado como una bandera para las retarificaciones realizadas en el proceso de distribución. Por lo tanto no tiene relación con la tabla de grupos de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'LastCaregroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'LastCaregroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje del valor total cobrado al paciente por concepto de cuota de recuperación; complementa SubTotalPatientSalesPrice; representa participación financiera del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'PatientPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al porcentaje del valor total cobrado por el Producto / Servicio a cargo del paciente por concepto de cuota de recuperacion (RecoveryFeeType)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'PatientPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'PatientPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total calculado al paciente por cuotas de recuperación (copago, moderadora, bono); conforme a ley pero puede reducirse si se concede descuento sobre recuperación; monto a cobrar directamente al paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total calculado por el Producto / Servicio al paciente por concepto de cuotas de recuperacion. (RecoveryFeeType). Este valor es el calculado segun se establece en la ley, pero puede variar si al paciente se le concede un descuento sobre el valor de recuperacion.  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cuota de recuperación (TINYINT): 1=Ninguna, 2=Cuota Moderadora, 3=Copago, 4=Bono, 5=Cuota de Recuperación; especifica mecanismo de participación financiera del paciente según normativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Cuota de Recuperacion  1. Ninguna  2. Cuota Moderadora  3. Copago  4. Bono  5. Cuota de Recuperación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de aplicación de cuota de recuperación (TINYINT): 0=No aplica, 1=Disponible para cálculo, 2=Aplicada sobre el item; controla si el paciente contribuye con copago/cuota moderadora/bono.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica 3 estados para el calculo de la cuota de recuperacion  0 - No Aplica Cuota de Recuperacion  1 - Disponible para hacer el calculo de la cuota de recuperacion  2 - Se esta aplicando cuota de recuperacion sobre el item', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje cobrado al tercero/asegurador sobre el producto/servicio; inferior a 100% cuando se aplica cuota de recuperación; complementa ThirdPartySalesPrice.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al porcentaje cobrado por el Producto / Servicio al tercero de la factura y/o folio, este valor es diferente del 100% cuando se aplica cuota de recuperacion.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total cobrado al tercero (asegurador/contratante/folio); diferente de GrandTotalSalesPrice cuando se aplica cuota de recuperación; parte del monto facturado a terceros.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total cobrado por el Producto / Servicio al tercero de la factura y/o folio, este valor es diferente del GrandTotalSalesPrice cuando se aplica cuota de recuperacion.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de distribución (TINYINT): 1=Ninguno, 2=Distribución Normal, 3=Distribución por Corte de Cuentas, 4=Distribución por Unidad, 5=Distribución NoPOS; determina criterios de asignación a tercero/paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Distribucion  1 - Ninguno  2 - Distribucion Normal  3 - Distribucion por Corte de Cuentas  4 - Distribucion por Unidad  5 - Distribucion NoPOS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del descuento aplicado al item; este descuento se distribuye proporcionalmente cuando el item se distribuye entre tercero y paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total del descuento que se le realizo al item, Este descuento tambien se distribuye cuando el item es distribuido', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total cobrado por el producto/servicio establecido en GrandTotalSalesPrice del detalle de orden; monto base antes de aplicar descuentos o cuotas de recuperación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total cobrado por el Producto / Servicio establecido en GrandTotalSalesPrice de ServiceOrderDetail  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto o servicio a distribuir en este registro de distribución.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de la cotización de orden de servicio; referencia a QuotationServiceOrderDetail, vincula producto/servicio cotizado con su distribución.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'QuotationServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la cotización', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'QuotationServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'QuotationServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de distribución de detalle de cotización de orden de servicio (INT IDENTITY, clave primaria).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución financiera de cada ítem (servicio o producto) dentro de una orden de cotización, indicando cómo se reparte el valor total entre el tercero pagador (aseguradora, EPS, empresa) y el paciente, incluyendo descuentos, cuotas moderadoras y porcentajes de cobertura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution';
