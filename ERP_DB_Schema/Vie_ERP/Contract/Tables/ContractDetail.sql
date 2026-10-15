CREATE TABLE [Contract].[ContractDetail] (
    [Id]                               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractId]                       INT            NOT NULL,
    [ContractName]                     VARCHAR (100)  NOT NULL,
    [ContractNumber]                   VARCHAR (100)   NOT NULL,
    [Type]                             TINYINT        NOT NULL,
    [InitialDate]                      DATETIME       NOT NULL,
    [EndDate]                          DATETIME       NOT NULL,
    [BillingInitialDate]               DATETIME       NOT NULL,
    [BillingEndDate]                   DATETIME       NOT NULL,
    [Legalized]                        BIT            NOT NULL,
    [DateLegalization]                 DATETIME       NULL,
    [RadicatedBillingDate]             DATETIME       NOT NULL,
    [Observations]                     VARCHAR (MAX)  NULL,
    [PrintingMode]                     TINYINT        NOT NULL,
    [TerminationControl]               TINYINT        NOT NULL,
    [NotificationValueType]            TINYINT        NULL,
    [PercentageNotification]           NUMERIC (5, 2) NULL,
    [NotificationValue]                NUMERIC (18)   NULL,
    [NotificationTimeType]             TINYINT        NULL,
    [NotificationDays]                 INT            NULL,
    [PercentageApplyPaymentSoon]       NUMERIC (5, 2) CONSTRAINT [DF_ContractDetail_PercentageApplyPaymentSoon] DEFAULT ((0)) NOT NULL,
    [AgesPortfolioId]                  INT            NULL,
    [ValidRecord]                      BIT            NOT NULL,
    [PermanentObservationOfTheInvoice] VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_ContractDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractDetail_AgesPortfolio] FOREIGN KEY ([AgesPortfolioId]) REFERENCES [Portfolio].[AgesPortfolio] ([Id]),
    CONSTRAINT [FK_ContractDetail_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Contract].[Contract] ([Id])
);


GO
ALTER TABLE [Contract].[ContractDetail] NOCHECK CONSTRAINT [FK_ContractDetail_Contract];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación permanente a la factura, observación que se relaciona en el reporte de la factura', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PermanentObservationOfTheInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro válido', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ValidRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la edad de cartera, se habilita una vez se asigne un valor al campo código', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'AgesPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje aplicar pronto pago', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PercentageApplyPaymentSoon';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NotificationDays', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de notificacion para cuando es control por fecha  1 - Ninguno  2 - Dias de anterioridad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationTimeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de notificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notificación de porcentaje', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PercentageNotification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de notificacion para cuando el control de terminacion es de valor  1 - Ninguna  2 - % del valor del contrato  3 - Valor fijo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationValueType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control de terminacion del contrato  1- Ninguno  2 - Fecha Terminacion  3 - Valor contrato  4 - Fecha Terminacion o Valor Contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'TerminationControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modo de impresion de los reportes  1 - Manual Tarifario  2 - CUPS  3 - Codigo RIPS  4 - Descripción Relacionada  5 - CUPS Descripción Relacionada  6- Descripción relacionada ó CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PrintingMode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de radicación de facturas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'RadicatedBillingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de legalizacion del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'DateLegalization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contrato esta legalizado ?', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Legalized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final de facturación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'BillingEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicia de facturación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'BillingInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de contrato:    1. Nuevo  2. Inclusión  3. Otro Si  4. Adición  5. Prorroga  6. Otros', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Id';

