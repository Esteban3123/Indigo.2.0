CREATE TABLE [Payments].[DeferredCausation] (
    [Id]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayable]  INT             NOT NULL,
    [BillNumber]        VARCHAR (20)    NULL,
    [IdMainAccount]     INT             NOT NULL,
    [PeriodsNumber]     INT             NOT NULL,
    [TypeDistribution]  TINYINT         NOT NULL,
    [InitialDate]       DATETIME        NOT NULL,
    [EndDate]           DATETIME        CONSTRAINT [DF_DeferredCausation_EndDate] DEFAULT (((8)/(1))/(2015)) NOT NULL,
    [IdThirdParty]      INT             NOT NULL,
    [IdCostCenter]      INT             NULL,
    [ValueCreditPeriod] DECIMAL (20, 2) NOT NULL,
    [Status]            TINYINT         NOT NULL,
    [CreationUser]      VARCHAR (20)    NOT NULL,
    [CreationDate]      DATETIME        NOT NULL,
    [ModificationUser]  VARCHAR (20)    NULL,
    [ModificationDate]  DATETIME        NULL,
    [ConfirmationUser]  VARCHAR (20)    NULL,
    [ConfirmationDate]  DATETIME        NULL,
    [AnnulmentUser]     VARCHAR (20)    NULL,
    [AnnulmentDate]     DATETIME        NULL,
    [TimeStamp]         ROWVERSION      NOT NULL,
    CONSTRAINT [PK_DeferredCausation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeferredCausation_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_DeferredCausation_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_DeferredCausation_MainAccounts] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DeferredCausation_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[DeferredCausation] NOCHECK CONSTRAINT [FK_DeferredCausation_AccountPayable];


GO
ALTER TABLE [Payments].[DeferredCausation] NOCHECK CONSTRAINT [FK_DeferredCausation_CostCenter];


GO
ALTER TABLE [Payments].[DeferredCausation] NOCHECK CONSTRAINT [FK_DeferredCausation_MainAccounts];


GO
ALTER TABLE [Payments].[DeferredCausation] NOCHECK CONSTRAINT [FK_DeferredCausation_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ValueCreditPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercero beneficiario', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la causacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de distribucion:(1=Proporcional, 2=Valor)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'TypeDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo diferido', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'PeriodsNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta activo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factura correspondiente ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'BillNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la causación diferida', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'Id';

