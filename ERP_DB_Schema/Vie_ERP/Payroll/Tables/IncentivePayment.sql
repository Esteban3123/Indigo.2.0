CREATE TABLE [Payroll].[IncentivePayment] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RegisterStatus]      TINYINT       NULL,
    [GroupId]             INT           NOT NULL,
    [PaymentType]         CHAR (1)      NOT NULL,
    [Period]              CHAR (1)      NOT NULL,
    [PeriodInitialDate]   DATE          NOT NULL,
    [PeriodEndDate]       DATE          NOT NULL,
    [ContractId]          INT           NOT NULL,
    [ContractInitialDate] DATE          NOT NULL,
    [BasicSalary]         NUMERIC (18)  NOT NULL,
    [TransportHelpValue]  NUMERIC (18)  NULL,
    [AverageSalary]       NUMERIC (18)  NOT NULL,
    [SanctionDays]        SMALLINT      NULL,
    [WorkingDays]         SMALLINT      NOT NULL,
    [Month]               INT           NULL,
    [RepresentationCost]  NUMERIC (18)  NULL,
    [FeedingHelpValue]    NUMERIC (18)  NULL,
    [BonificationValue]   NUMERIC (18)  NULL,
    [FeedingQuote]        NUMERIC (18)  NULL,
    [Fonsalud]            NUMERIC (18)  NULL,
    [FeedingEmbargo]      NUMERIC (18)  NULL,
    [PayrollNextDate]     DATE          NULL,
    [PaidDays]            SMALLINT      NOT NULL,
    [TotalAccrued]        NUMERIC (18)  CONSTRAINT [DF_IncentivePayment_TotalAccrued] DEFAULT ((0)) NULL,
    [TotalDeducted]       NUMERIC (18)  CONSTRAINT [DF_IncentivePayment_TodalDeducted] DEFAULT ((0)) NULL,
    [PaidValue]           NUMERIC (18)  NOT NULL,
    [RetentionValue]      NUMERIC (18)  CONSTRAINT [DF_IncentivePayment_RetentionValue] DEFAULT ((0)) NULL,
    [UnemployementFundId] INT           NULL,
    [RetentionBase]       NUMERIC (18)  CONSTRAINT [DF_IncentivePayment_RetentionBase] DEFAULT ((0)) NOT NULL,
    [ProcedureTypeRTF]    TINYINT       CONSTRAINT [DF_IncentivePayment_ProcedureTypeRTF] DEFAULT ((0)) NOT NULL,
    [TypeArticleRTF]      TINYINT       CONSTRAINT [DF_IncentivePayment_TypeArticleRTF] DEFAULT ((0)) NOT NULL,
    [MessageRTF]          VARCHAR (MAX) NULL,
    [IBCUnemployment]     NUMERIC (18)  CONSTRAINT [DF_IncentivePayment_IBCUnemployment] DEFAULT ((0)) NULL,
    [ConfirmationDate]    DATETIME      NULL,
    [ConfirmationUser]    VARCHAR (20)  NULL,
    CONSTRAINT [PK_IncentivePayment__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IncentivePayment_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_IncentivePayment_Fund] FOREIGN KEY ([UnemployementFundId]) REFERENCES [Payroll].[Fund] ([Id]),
    CONSTRAINT [FK_IncentivePayment_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[IncentivePayment] NOCHECK CONSTRAINT [FK_IncentivePayment_Contract];


GO
ALTER TABLE [Payroll].[IncentivePayment] NOCHECK CONSTRAINT [FK_IncentivePayment_Fund];


GO
ALTER TABLE [Payroll].[IncentivePayment] NOCHECK CONSTRAINT [FK_IncentivePayment_Group];


GO
CREATE NONCLUSTERED INDEX [IX_IncentivePayment__ContractId]
    ON [Payroll].[IncentivePayment]([ContractId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC de Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'IBCUnemployment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'MessageRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Artículo Usado en Retefuente: 0 - Ninguno, 1 - Artículo 383, 2 - Artículo 384', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TypeArticleRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Procedimiento para Retención en la Fuente: 1 - Procedimiento 1; 2 - Procedimiento 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ProcedureTypeRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la base para la retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RetentionBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del fondo de Cesantías (Válido para el Sector Público)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'UnemployementFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de Retención en la Fuente, ÚNICAMENTE se calcula si el tipo de Pago de la Prima es por Periodo Independiente.  Toma en cuenta el Tipo de Procedimiento, por cada uno de los empleados, de acuerdo al que tenga parametrizado en Talento Humano', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RetentionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Prima Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaidValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TotalDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TotalAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días Pagados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaidDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Próxima Nómina (En caso de que la Prima se pague con nómina)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PayrollNextDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Embargo de Alimetnos (-)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingEmbargo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fonsalud (-)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Fonsalud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuota por Alimentos (-)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingQuote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor bonificacion (+)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'BonificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auxilio de Alimentos (+)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingHelpValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gastos de Representación (+)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RepresentationCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Meses', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días Trabajados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'WorkingDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Sanciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'SanctionDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario Promedio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'AverageSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Auxilio del Transporte (Si el empleado tiene derecho)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TransportHelpValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario Básico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicio del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ContractInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Fin del Periodo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PeriodEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial del Periodo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PeriodInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Period';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Pago: 1. Nómina (N), 2. Periodo Independiente (P) ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaymentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Sin Confirmar, 2 - Confirmado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RegisterStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Id';

