CREATE TABLE [Portfolio].[PortfolioNoteDistribution] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioNoteId]    INT             NOT NULL,
    [CustomerId]         INT             NOT NULL,
    [MainAccountId]      INT             NOT NULL,
    [CostCenterId]       INT             NULL,
    [Value]              NUMERIC (18, 2) NOT NULL,
    [PortfolioAdvanceId] INT             NULL,
    CONSTRAINT [PK_PortfolioNoteDistribution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioNoteDistribution_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDistribution_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDistribution_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDistribution_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDistribution_PortfolioNote] FOREIGN KEY ([PortfolioNoteId]) REFERENCES [Portfolio].[PortfolioNote] ([Id])
);




GO



GO



GO



GO



GO



GO
-- =============================================
-- Author:		Giovanny Plazas Lozano
-- Create date: 06/09/2023
-- Description:	Trigger para relacionar el recibo de caja con el anticipo cuando viene de una distribucion de anticipos para obtener el metodo de pago
-- =============================================
CREATE TRIGGER [Portfolio].[TriggerNoteDistribution]
   ON [Portfolio].[PortfolioNoteDistribution]
   AFTER INSERT,UPDATE
AS 
BEGIN
	


			update pa set CashReceiptId = pas.CashReceiptId
			from INSERTED d
			inner join Portfolio.PortfolioNote pn WITH(NOLOCK) on pn.Id = d.PortfolioNoteId
			inner join Portfolio.PortfolioAdvance pas WITH(NOLOCK) on pas.Id = pn.PortfolioAdvanceId
			inner join Portfolio.PortfolioAdvance pa WITH(NOLOCK) on pa.Id = d.PortfolioAdvanceId

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK, NULLABLE) del anticipo generado y confirmado al procesar la nota de cartera; registra qué anticipo específico se creó para esta distribución, permitiendo trazabilidad completa desde nota hasta movimiento contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del anticipo que se creo cuando se confirmo el documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto numérico (NUMERIC 18,2) que especifica el valor del anticipo distribuido a esta línea; suma de todas las líneas debe conciliar con el total de la nota de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor del nuevo anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK, NULLABLE) del centro de costo (Payroll.CostCenter) requerido solo si la cuenta contable maneja distribución por centro; usado en empresas con múltiples sedes, departamentos o unidades funcionales.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el centro de costo con el que se va a crear el nuevo anticipo, este solo se solicita si la cuenta contable maneja centro de costo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cuenta contable principal (GeneralLedger.MainAccounts) del anticipo; sugiere automáticamente desde el encabezado pero permite cambio manual para flexibilidad contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta contable del nuevo anticipo, la cual se sugiere del anticipo de la cabecera pero el usuario la puede cambiar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del cliente o entidad facturada (Common.Customer) asociado al nuevo anticipo; permite rastrear a qué tercero se asigna el valor distribuido.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cliente del nuevo anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del encabezado o nota de cartera (Portfolio.PortfolioNote) que origina esta distribución; vincula cada línea a su documento madre de factura, glosa o ajuste contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'PortfolioNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la nota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'PortfolioNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'PortfolioNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de cada línea de distribución en la tabla PortfolioNoteDistribution; clave primaria que rastrea cada asignación de valor a cuenta contable y cliente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de distribucion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución contable de notas de cartera entre clientes, cuentas principales y centros de costo. Registra cómo se reparte el valor de cada nota de cartera (nota débito, nota crédito) entre las diferentes unidades financieras.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution';
