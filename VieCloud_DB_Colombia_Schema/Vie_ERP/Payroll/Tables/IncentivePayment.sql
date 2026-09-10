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



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_IncentivePayment__ContractId]
    ON [Payroll].[IncentivePayment]([ContractId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmó el pago del incentivo, VARCHAR(20), auditoría de aprobación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación del pago de incentivo, DATETIME, timestamp de auditoría', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de cotización IBC para cesantías/desempleo, NUMERIC(18), cálculo de aporte fondo desempleo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'IBCUnemployment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IBC de Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'IBCUnemployment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'IBCUnemployment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje o descripción en formato RTF para retención en la fuente, VARCHAR(MAX), detalle de procedimiento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'MessageRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'MessageRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'MessageRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de artículo tributario para retefuente: 0=Ninguno, 1=Artículo 383, 2=Artículo 384, TINYINT, clasificación fiscal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TypeArticleRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Artículo Usado en Retefuente: 0 - Ninguno, 1 - Artículo 383, 2 - Artículo 384', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TypeArticleRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TypeArticleRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de retención en la fuente aplicado: 1=Procedimiento 1, 2=Procedimiento 2, TINYINT, método cálculo retención', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ProcedureTypeRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Procedimiento para Retención en la Fuente: 1 - Procedimiento 1; 2 - Procedimiento 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ProcedureTypeRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ProcedureTypeRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base imponible para cálculo de retención en la fuente, NUMERIC(18), monto sobre el cual se aplica porcentaje retefuente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RetentionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la base para la retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RetentionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RetentionBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del fondo de cesantías/desempleo, INT, FK a Payroll.Fund, válido sector público', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'UnemployementFundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del fondo de Cesantías (Válido para el Sector Público)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'UnemployementFundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'UnemployementFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de retención en la fuente calculado, NUMERIC(18), solo para pago período independiente según tipo procedimiento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RetentionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de Retención en la Fuente, ÚNICAMENTE se calcula si el tipo de Pago de la Prima es por Periodo Independiente.  Toma en cuenta el Tipo de Procedimiento, por cada uno de los empleados, de acuerdo al que tenga parametrizado en Talento Humano', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RetentionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RetentionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de prima pagada al profesional, NUMERIC(18), monto neto desembolsado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaidValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Prima Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaidValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaidValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de deducciones aplicadas (retención, embargo, cuotas), NUMERIC(18), suma de descuentos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TotalDeducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TotalDeducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TotalDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total devengado o causado de la prima de incentivo, NUMERIC(18), acumulado antes deducciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TotalAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días pagados en el período de incentivo, SMALLINT, días efectivos de pago', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaidDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Días Pagados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaidDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaidDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha programada del siguiente ciclo de nómina si prima se paga con nómina, DATE, referencia próximo proceso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PayrollNextDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Próxima Nómina (En caso de que la Prima se pague con nómina)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PayrollNextDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PayrollNextDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Embargo por alimentos decretado en juicio, NUMERIC(18), descuento (-) obligatorio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingEmbargo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Embargo de Alimetnos (-)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingEmbargo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingEmbargo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aporte a Fonsalud descontado de la prima, NUMERIC(18), descuento (-) fondo salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Fonsalud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fonsalud (-)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Fonsalud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Fonsalud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuota por asuntos de alimentación descontada, NUMERIC(18), descuento (-) obligación familiar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingQuote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuota por Alimentos (-)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingQuote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingQuote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de bonificación adicional otorgada, NUMERIC(18), incremento (+) al incentivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'BonificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor bonificacion (+)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'BonificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'BonificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Auxilio de alimentos adicionado, NUMERIC(18), incremento (+) complementario profesional salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingHelpValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auxilio de Alimentos (+)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingHelpValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'FeedingHelpValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gastos de representación reembolsados, NUMERIC(18), incremento (+) asignación profesional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RepresentationCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gastos de Representación (+)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RepresentationCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RepresentationCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de meses del período de pago de incentivo, INT, cantidad meses liquidados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Meses', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días efectivamente trabajados en el período, SMALLINT, base cálculo diario prima', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'WorkingDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Días Trabajados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'WorkingDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'WorkingDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días descontados por sanciones disciplinarias, SMALLINT, reducción días pagados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'SanctionDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Días de Sanciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'SanctionDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'SanctionDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario promedio usado para cálculo de prima, NUMERIC(18), base remuneración para incentivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'AverageSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario Promedio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'AverageSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'AverageSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del auxilio de transporte si empleado tiene derecho, NUMERIC(18), incremento (+) asociado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TransportHelpValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Auxilio del Transporte (Si el empleado tiene derecho)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TransportHelpValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'TransportHelpValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario básico vigente del profesional de salud, NUMERIC(18), monto base contratado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario Básico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio del contrato vinculado, DATE, período válido del acuerdo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ContractInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicio del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ContractInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ContractInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato, INT, FK a Payroll.Contract, referencia acuerdo laboral', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del período de liquidación del incentivo, DATE, límite temporal pago', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PeriodEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Fin del Periodo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PeriodEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PeriodEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del período de liquidación de incentivo, DATE, inicio temporal pago', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PeriodInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial del Periodo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PeriodInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PeriodInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período de pago del incentivo, CHAR(1), parámetro temporal período', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Period';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Period';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Period';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de pago: N=Nómina, P=Período Independiente, CHAR(1), método desembolso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaymentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Pago: 1. Nómina (N), 2. Periodo Independiente (P) ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaymentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'PaymentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de empleados, INT, FK a Payroll.Group, agrupación profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Sin Confirmar, 2=Confirmado, TINYINT, validación aprobación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RegisterStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Sin Confirmar, 2 - Confirmado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RegisterStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'RegisterStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro de incentivo, INT IDENTITY, clave primaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de pagos de incentivos y bonificaciones del personal en nómina. Contiene los valores devengados, deducciones, días trabajados, salario base y demás conceptos liquidados por período para cada contrato de empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePayment';
