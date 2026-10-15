CREATE TABLE [Billing].[BillingDeadline] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]  INT          NOT NULL,
    [PeriodNumber] INT          NOT NULL,
    [InitialDate]  DATE         NOT NULL,
    [EndDate]      DATE         NOT NULL,
    [Value]        NUMERIC (18) NOT NULL,
    [TimeStamp]    ROWVERSION   NOT NULL,
    CONSTRAINT [PK_BillingDeadline] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingDeadline_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id])
);


GO
ALTER TABLE [Billing].[BillingDeadline] NOCHECK CONSTRAINT [FK_BillingDeadline_CareGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingDeadline', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acumula el valor facturado para controlar el tope establecido en el grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingDeadline', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final del Periodo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingDeadline', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial del Periodo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingDeadline', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Periodo de facturacion segun el Periodo de Facturacion del Grupo de Atencion ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingDeadline', @level2type = N'COLUMN', @level2name = N'PeriodNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Grupo de Atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingDeadline', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingDeadline', @level2type = N'COLUMN', @level2name = N'Id';

