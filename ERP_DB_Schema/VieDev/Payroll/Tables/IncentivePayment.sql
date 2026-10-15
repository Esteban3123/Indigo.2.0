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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'MessageRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de Retención en la Fuente, ÚNICAMENTE se calcula si el tipo de Pago de la Prima es por Periodo Independiente.  Toma en cuenta el Tipo de Procedimiento, por cada uno de los empleados, de acuerdo al que tenga parametrizado en Talento Humano', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RetentionValue';

