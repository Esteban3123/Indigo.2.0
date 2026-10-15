CREATE TABLE [Billing].[BillingOdontologyProcedureServiceOrder] (
    [Id]              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumber] VARCHAR (20) NOT NULL,
    [Folio]           VARCHAR (10) NOT NULL,
    [ProcedureCode]   INT          NOT NULL,
    [ServiceOrderId]  INT          NOT NULL,
    CONSTRAINT [PK_BillingOdontologyProcedureServiceOrder] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la orden de servicio que generó', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del procedimiento, este valor se saca de la tabla INDIGO008..ODOPARTRA del campo CONSECTRA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'ProcedureCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'Folio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del ingreso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingOdontologyProcedureServiceOrder', @level2type = N'COLUMN', @level2name = N'Id';

