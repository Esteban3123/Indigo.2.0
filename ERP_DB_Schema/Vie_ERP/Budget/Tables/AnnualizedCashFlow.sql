CREATE TABLE [Budget].[AnnualizedCashFlow] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CategoryId]                 INT             NOT NULL,
    [DocumentSource]             TINYINT         NOT NULL,
    [Month]                      TINYINT         NOT NULL,
    [InitialValue]               NUMERIC (18, 2) NOT NULL,
    [DebitModificationValue]     NUMERIC (18, 2) NOT NULL,
    [CreditModificationValue]    NUMERIC (18, 2) NOT NULL,
    [DebitTransferValue]         NUMERIC (18, 2) NOT NULL,
    [CreditTransferValue]        NUMERIC (18, 2) NOT NULL,
    [TotalScheduled]             NUMERIC (18, 2) NOT NULL,
    [ExecutedValue]              NUMERIC (18, 2) NOT NULL,
    [Balance]                    NUMERIC (18, 2) NOT NULL,
    [ReserveValue]               NUMERIC (18, 2) NOT NULL,
    [DebitReserveModValue]       NUMERIC (18, 2) NOT NULL,
    [CreditReserveModValie]      NUMERIC (18, 2) NOT NULL,
    [DebitReserveTransValue]     NUMERIC (18, 2) NOT NULL,
    [CreditReserveTransValue]    NUMERIC (18, 2) NOT NULL,
    [ExecutedReserveValue]       NUMERIC (18, 2) NOT NULL,
    [CxPValue]                   NUMERIC (18, 2) NOT NULL,
    [DebitCxPModificationValue]  NUMERIC (18, 2) NOT NULL,
    [CreditCxPModificationValue] NUMERIC (18, 2) NOT NULL,
    [DebitCxPTransferValue]      NUMERIC (18, 2) NOT NULL,
    [CreditCxPTransferValue]     NUMERIC (18, 2) NOT NULL,
    [ExecutedCxPValue]           NUMERIC (18, 2) NOT NULL,
    [Status]                     TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    CONSTRAINT [DF_AnnualizedCashFlow_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]               DATETIME        CONSTRAINT [DF_AnnualizedCashFlow_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    CONSTRAINT [PK_PAC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PAC_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([Id])
);


GO
ALTER TABLE [Budget].[AnnualizedCashFlow] NOCHECK CONSTRAINT [FK_PAC_Category];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AnnualizedCashFlow__CategoryId__Month]
    ON [Budget].[AnnualizedCashFlow]([CategoryId] ASC, [Month] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnualizedCashFlow__CategoryId]
    ON [Budget].[AnnualizedCashFlow]([CategoryId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento (Registrado = 1, Confirmado = 2, Anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor ejecutado del rubro CxP', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedCxPValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor traslado credito a la CxP del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditCxPTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor traslado debito a la CxP del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitCxPTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor modificacion credito a la CxP del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditCxPModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor modificacion debito a la CxP del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitCxPModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la cuenta por pagar CxP', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CxPValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor ejecutado del rubro de la reserva', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedReserveValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de traslado credito a la reserva del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditReserveTransValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de traslado debito a la reserva del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitReserveTransValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de modificacion credito a la reserva del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditReserveModValie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de modificacion debito a la reserva del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitReserveModValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la reserva', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ReserveValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el saldo del PAC, este se calcula de la siguiente manera  (TotalScheduled - ExecutedValue )', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Ejecutado del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Total Programado, este se calcula de la siguiente manera  (IniitalValue - DebitModificationValue + CreditModificationValue - DebitTransferValue + CreditTransferValue)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'TotalScheduled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor traslado credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor traslado debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de modificacion credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de modificacion debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes del PAC (ENERO = 1,FEBRERO = 2,MARZO =  3,ABRIL = 4,MAYO = 5,JUNIO = 6,JULIO = 7,AGOSTO =  8,SEPTIEMBRE = 9,OCTUBRE = 10,NOVIEMBRE =  11,DICIEMBRE = 12)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Presupuesto (INGRESO = 1,GASTO = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DocumentSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del pac', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Id';

