CREATE TABLE [Payroll].[AgreementsC] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Consecutive]                   INT             NOT NULL,
    [GroupId]                       INT             NULL,
    [EmployeeId]                    INT             NOT NULL,
    [CompanyId]                     INT             NOT NULL,
    [ConceptId]                     INT             NOT NULL,
    [KindsAgreementsId]             INT             NOT NULL,
    [Comments]                      VARCHAR (250)   NULL,
    [LiquidationType]               TINYINT         NOT NULL,
    [TermType]                      TINYINT         NOT NULL,
    [AgreementValue]                DECIMAL (18, 2) NOT NULL,
    [NumberShares]                  INT             NOT NULL,
    [State]                         VARCHAR (1)     NOT NULL,
    [StartingDate]                  DATETIME        NOT NULL,
    [CurrentBalance]                DECIMAL (18, 2) NULL,
    [EndDateSuspend]                DATETIME        NULL,
    [CommentChangeState]            VARCHAR (250)   NULL,
    [TimeStamp]                     ROWVERSION      NOT NULL,
    [PaidVacation]                  BIT             CONSTRAINT [DF_AgreementsC_PaidVacation] DEFAULT ((0)) NULL,
    [Transfertype]                  TINYINT         NULL,
    [AccountReceivableAccountingId] INT             NULL,
    CONSTRAINT [PK_AgreementsC__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Agreements_AccountReceivableAccounting] FOREIGN KEY ([AccountReceivableAccountingId]) REFERENCES [Portfolio].[AccountReceivableAccounting] ([Id]),
    CONSTRAINT [FK_Agreements_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Payroll].[Company] ([Id]),
    CONSTRAINT [FK_Agreements_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_Agreements_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Agreements_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_AgreementsC_KindsAgreements] FOREIGN KEY ([KindsAgreementsId]) REFERENCES [Payroll].[KindsAgreements] ([Id]),
    CONSTRAINT [UQ_AgreementsC_Consecutive] UNIQUE NONCLUSTERED ([Consecutive] ASC)
);


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_Agreements_AccountReceivableAccounting];


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_Agreements_Company];


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_Agreements_Concept];


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_Agreements_Employee];


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_Agreements_Group];


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_AgreementsC_KindsAgreements];


GO
CREATE NONCLUSTERED INDEX [IX_AgreementsC__EmployeeId]
    ON [Payroll].[AgreementsC]([EmployeeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AgreementsC__CompanyId]
    ON [Payroll].[AgreementsC]([CompanyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Movimiento de la Factura relacionada si la clase de convenio afecta cuentas por cobrar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'AccountReceivableAccountingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando la clase de convenio es afecta cuentas por cobrar se solicita el tipo de traslado:  1 - Mismo Tercero  2 - Diferente Tercero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Transfertype';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se utiliza para que el valor del Convenio se descuente por Vacaciones con Pago Inmediato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'PaidVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de control de concurrencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario General del cambio de Estado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CommentChangeState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Suspensión del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'EndDateSuspend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo Actual del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CurrentBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'StartingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Convenio: "1 " Sin confirmar - "2" Confirmado - "3" Suspendido - "4" Terminado - "5" Anulado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de cuotas del convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'NumberShares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Total del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'AgreementValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Plazo: 1 - Con N° Meses 2 - Sin N° Meses', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'TermType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Liquidacion: 1 - Con Monto 2 - Sin Monto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obervacion general del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Clase del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'KindsAgreementsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto con el que se va a realizar la liquidacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la empresa con la que se suscribe el convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Del Grupo En Nomina ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo  para los convenios', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Convenio Cabecera', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Id';

