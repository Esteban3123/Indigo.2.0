CREATE TABLE [Portfolio].[PortfolioProvisionDetail] (
    [Id]                                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioProvisionId]                   INT             NOT NULL,
    [AccountReceivableId]                    INT             NOT NULL,
    [InvoiceNumber]                          VARCHAR (50)    NOT NULL,
    [Percentage]                             NUMERIC (7, 4)  NOT NULL,
    [Value]                                  NUMERIC (18, 2) NOT NULL,
    [BalanceAccountReceivable]               NUMERIC (18, 2) NOT NULL,
    [ConfirmDateAccountReceivable]           DATETIME        NOT NULL,
    [AgesId]                                 INT             NULL,
    [NetPresentValue]                        DECIMAL (18, 2) CONSTRAINT [DF__Portfolio__NetPr__48E37C33] DEFAULT ((0)) NULL,
    [Expectative]                            INT             CONSTRAINT [DF__Portfolio__Expec__47EF57FA] DEFAULT ((0)) NULL,
    [ValueGlosado]                           DECIMAL (18, 2) CONSTRAINT [DF__Portfolio__Value__46FB33C1] DEFAULT ((0)) NOT NULL,
    [FacturerValue]                          DECIMAL (18, 2) CONSTRAINT [DF__Portfolio__Factu__46070F88] DEFAULT ((0)) NOT NULL,
    [AccumulatedDeterioration]               DECIMAL (18, 2) CONSTRAINT [DF_PortfolioProvisionDetail_AccumulatedDeterioration] DEFAULT ((0)) NOT NULL,
    [DeteriorationBalanceCurrentYear]        DECIMAL (18, 2) CONSTRAINT [DF_PortfolioProvisionDetail_DeteriorationBalanceCurrentYear] DEFAULT ((0)) NOT NULL,
    [DeteriorationBalancePreviousYear]       DECIMAL (18, 2) CONSTRAINT [DF_PortfolioProvisionDetail_DeteriorationBalancePreviousYear] DEFAULT ((0)) NOT NULL,
    [CurrentDeteriorationYear]               INT             NULL,
    [Days]                                   INT             CONSTRAINT [DF_PortfolioProvisionDetail_Days] DEFAULT ((0)) NOT NULL,
    [PortfolioDeteriorationClassificationId] INT             NULL,
    [LegalBookId]                            INT             NULL,
    CONSTRAINT [PK_PortfolioProvisionDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioProvisionDetail_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioProvisionDetail_AgesPortfolio] FOREIGN KEY ([AgesId]) REFERENCES [Portfolio].[AgesPortfolio] ([Id]),
    CONSTRAINT [FK_PortfolioProvisionDetail_DeteriorationClassification] FOREIGN KEY ([PortfolioDeteriorationClassificationId]) REFERENCES [Portfolio].[PortfolioDeteriorationClassification] ([Id]),
    CONSTRAINT [FK_PortfolioProvisionDetail_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id]),
    CONSTRAINT [FK_PortfolioProvisionDetail_PortfolioProvision] FOREIGN KEY ([PortfolioProvisionId]) REFERENCES [Portfolio].[PortfolioProvision] ([Id])
);




GO



GO





GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX UQ_PortfolioProvisionDetail_NULL_LegalBook
	ON Portfolio.PortfolioProvisionDetail (PortfolioProvisionId, AccountReceivableId)
	WHERE LegalBookId IS NULL;
GO
CREATE UNIQUE NONCLUSTERED INDEX UQ_PortfolioProvisionDetail_NotNull_LegalBook
	ON Portfolio.PortfolioProvisionDetail (PortfolioProvisionId, AccountReceivableId, LegalBookId)
	WHERE LegalBookId IS NOT NULL;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de vencimiento o diferencia entre fecha actual y fecha de radicación/facturación (INT, default 0). Antigüedad de la deuda; usado para clasificar cartera en edades.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias de diferencia con respecto a la fecha de Facturación (Radicación si aplica)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año fiscal en el que se registró el deterioro actual (INT, nullable). Período contable del reconocimiento más reciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'CurrentDeteriorationYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año actual deteriorado.  Corresponde al valor registrado en el campo CurrentDeteriorationYear de la cuenta por cobrar al momento de realizar el deterioro.  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'CurrentDeteriorationYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'CurrentDeteriorationYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deterioro reconocido en el año anterior sobre esta cuenta (DECIMAL 18,2). Provisión acumulada del ejercicio fiscal pasado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'DeteriorationBalancePreviousYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo deteriorado en el año anterior.  Corresponde al valor registrado en el campo DeteriorationBalancePreviousYear de la cuenta por cobrar al momento de realizar el deterioro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'DeteriorationBalancePreviousYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'DeteriorationBalancePreviousYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deterioro registrado en el año actual sobre la cuenta por cobrar (DECIMAL 18,2). Provisión reconocida contablemente en el período fiscal corriente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'DeteriorationBalanceCurrentYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor deteriorado para la factura en lo transcurrido del año.  Corresponde al valor registrado en el campo DeteriorationBalanceCurrentYear de la cuenta por cobrar al momento de realizar el deterioro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'DeteriorationBalanceCurrentYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'DeteriorationBalanceCurrentYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deterioro acumulado de períodos anteriores al actual (DECIMAL 18,2). Pérdida esperada reconocida en años previos; cero si es primera vez.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'AccumulatedDeterioration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'El valor del Deterioro anterior. Si es primera vez no hay Deterioro acumulado.   Corresponde al valor registrado en el campo DeteriorationBalance de la cuenta por cobrar al momento de realizar el deterioro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'AccumulatedDeterioration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'AccumulatedDeterioration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor inicial facturado o facturado totalmente de la cuenta por cobrar (DECIMAL 18,2). Monto original sin ajustes.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'FacturerValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor facturado inicialmente de la Cuenta por Cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'FacturerValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'FacturerValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total glosado, rechazado o no reconocido por pagador en la factura (DECIMAL 18,2). Monto disputado o en controversia.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Glosado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo esperado de cobro en meses (INT, default 0). Horizonte de tiempo estimado para recuperación de la deuda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Expectative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Expectativa en meses', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Expectative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Expectative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor presente neto (VPN) descontado de la cuenta por cobrar (DECIMAL 18,2). Equivalente actualizado del flujo futuro esperado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'NetPresentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Presente Neto', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'NetPresentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'NetPresentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del rango de antigüedad o vencimiento de cartera (FK a AgesPortfolio). Clasifica la factura por días vencidos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'AgesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del la edad de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'AgesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'AgesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de confirmación, radicación o recepción del comprobante de facturación (DATETIME). Usado para calcular vencimiento y antigüedad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'ConfirmDateAccountReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de confirmación de la factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'ConfirmDateAccountReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'ConfirmDateAccountReceivable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente de la cuenta por cobrar al momento de registrar el deterioro (NUMERIC 18,2). Deuda vigente sin descontar pagos posteriores.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'BalanceAccountReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo de la cuenta por cobrar al momento de realizar el documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'BalanceAccountReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'BalanceAccountReceivable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos del deterioro o provisión calculado para la cuenta (NUMERIC 18,2). Importe monetario de la afectación contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del deterioro o provision', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de deterioro o provisión aplicado a la cuenta (NUMERIC 7,4). Expresado en decimales (ej: 10.5000 = 10.5%).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de Deterioro o Provision', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura, comprobante o radicación (VARCHAR 50). Identificador único del documento de cobro con el paciente/usuario.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el numero de factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar, factura o documento de ingreso/atención (FK a AccountReceivable). Referencia la deuda original.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la provisión de cartera padre (FK a PortfolioProvision). Agrupa detalles de deterioro/provisión.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'PortfolioProvisionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la provision de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'PortfolioProvisionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'PortfolioProvisionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de cada detalle de provisión de cartera en el portafolio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las provisiones de cartera: registra línea a línea cada cuenta por cobrar (factura) incluida en un cálculo de provisión, con su porcentaje aplicado, valor provisionado, saldo pendiente, deterioro acumulado y clasificación de riesgo, para el cierre contable y seguimiento del deterioro de cartera según NIIF.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de deterioro asignada a esta factura (ej: normal, en riesgo, irrecuperable), usada para agrupar la cartera por nivel de riesgo crediticio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'PortfolioDeteriorationClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'PortfolioDeteriorationClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al libro contable o libro legal en el que se registra el deterioro de esta cuenta por cobrar, para trazabilidad contable y cumplimiento normativo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';
