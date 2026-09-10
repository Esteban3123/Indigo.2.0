CREATE TABLE [Billing].[BillingOdontologyProcedureServiceOrder] (
    [Id]              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumber] VARCHAR (20) NOT NULL,
    [Folio]           VARCHAR (10) NOT NULL,
    [ProcedureCode]   INT          NOT NULL,
    [ServiceOrderId]  INT          NOT NULL,
    CONSTRAINT [PK_BillingOdontologyProcedureServiceOrder] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la orden de servicio que originó el procedimiento odontológico; referencia a la orden que generó el cargo en facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de servicio que generó', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del procedimiento odontológico (extraído de INDIGO008..ODOPARTRA.CONSECTRA); identifica el tipo de servicio dental facturado (limpieza, endodoncia, extracción, etc.).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'ProcedureCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del procedimiento, este valor se saca de la tabla INDIGO008..ODOPARTRA del campo CONSECTRA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'ProcedureCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'ProcedureCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del folio de facturación; referencia del comprobante fiscal o documento de cobro para el servicio odontológico prestado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'Folio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'Folio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'Folio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso u atención del paciente; vincula el procedimiento a la admisión clínica donde se realizó el servicio odontológico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del ingreso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del registro de relación entre procedimiento odontológico, orden de servicio y folio de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de procedimientos odontológicos asociados a órdenes de servicio dentro del proceso de facturación. Vincula cada procedimiento dental con su número de ingreso, folio de factura y orden de servicio correspondiente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder';
