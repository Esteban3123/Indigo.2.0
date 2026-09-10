CREATE TABLE [Portfolio].[ReportPortfolioByAgeDetail] (
    [IdIdentity]                   INT             IDENTITY (1, 1) NOT NULL,
    [Id]                           INT             NOT NULL,
    [IdReportPortfolioByAge]       INT             NOT NULL,
    [OrderBy]                      VARCHAR (MAX)   NULL,
    [OrderByAdvanceAtEnd]          INT             NOT NULL,
    [DocumentCode]                 VARCHAR (20)    NOT NULL,
    [AccountReceivableDate]        DATETIME        NOT NULL,
    [AccountReceivableType]        INT             NOT NULL,
    [PortfolioStatus]              INT             NOT NULL,
    [PortfolioStatusName]          VARCHAR (25)    NULL,
    [NumberShares]                 INT             NOT NULL,
    [Term]                         INT             NOT NULL,
    [OpeningBalance]               INT             NOT NULL,
    [ThirdPartyNit]                VARCHAR (25)    NOT NULL,
    [ThirdPartyName]               VARCHAR (300)   NOT NULL,
    [IdentificationType]           INT             NOT NULL,
    [Category]                     VARCHAR (123)   NULL,
    [Regimen]                      VARCHAR (50)    NULL,
    [CareGroupCode]                VARCHAR (20)    NULL,
    [CareGroupName]                VARCHAR (100)   NULL,
    [ContractCode]                 VARCHAR (20)    NULL,
    [ContractName]                 VARCHAR (100)   NULL,
    [AccountWithoutRadicateNumber] VARCHAR (50)    NULL,
    [RadicatedConsecutive]         INT             NULL,
    [RadicatedUser]                VARCHAR (20)    NULL,
    [RadicatedDate]                DATETIME        NULL,
    [DocumentDate]                 DATETIME        NULL,
    [RadicatedState]               CHAR (1)        NULL,
    [MainAccountNumber]            VARCHAR (50)    NULL,
    [MainAccountName]              VARCHAR (100)   NULL,
    [DocumentValue]                NUMERIC (18, 2) NOT NULL,
    [RetentionValue]               NUMERIC (38, 2) NULL,
    [InitialValue]                 NUMERIC (38, 2) NULL,
    [DebitValue]                   NUMERIC (38, 2) NULL,
    [CreditValue]                  NUMERIC (38, 2) NULL,
    [TransferValue]                DECIMAL (38, 2) NOT NULL,
    [CashReceiptValue]             DECIMAL (38, 2) NOT NULL,
    [CrossingValue]                DECIMAL (38, 2) NOT NULL,
    [Balance]                      NUMERIC (38, 2) NULL,
    [CurrentBalance]               NUMERIC (18, 2) NOT NULL,
    [CenterAttention]              CHAR (100)      NULL,
    [ExpiredDate]                  DATETIME        NULL,
    [Age]                          INT             NULL,
    [RegimenCalculated]            VARCHAR (MAX)   NULL,
    [ValueGlosado]                 MONEY           NOT NULL,
    [ValueAcceptedFirstInstance]   MONEY           NOT NULL,
    [ValueAcceptedSecondInstance]  MONEY           NOT NULL,
    [GlosaState]                   TINYINT         NULL,
    [GlosaStateName]               VARCHAR (35)    NULL,
    CONSTRAINT [PK_ReportPortfolioByAgeDetail] PRIMARY KEY CLUSTERED ([IdIdentity] ASC),
    CONSTRAINT [FK_ReportPortfolioByAgeDetail_ReportPortfolioByAge] FOREIGN KEY ([IdReportPortfolioByAge]) REFERENCES [Portfolio].[ReportPortfolioByAge] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del estado de la glosa (aceptada, rechazada, pendiente). Texto VARCHAR(35) para búsqueda en procesos de revisión y aceptación de glosas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'GlosaStateName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el nombre del estado de la glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'GlosaStateName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'GlosaStateName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la glosa (TINYINT). Código numérico que indica el resultado: aceptada en primera instancia, aceptada en segunda instancia, rechazada o pendiente de revisión.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'GlosaState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el estado de la glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'GlosaState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'GlosaState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario aceptado en segunda instancia de revisión de glosa (MONEY). Monto aprobado tras apelación o recurso en proceso de glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor aceptado en segunda instancia.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario aceptado en primera instancia de revisión de glosa (MONEY). Monto inicial aprobado en el análisis de la glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor aceptado en primera instancia.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total glosado o cuestionado en el documento (MONEY). Monto reclamado, objetado o bajo revisión en el proceso de auditoría de cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el valor glosado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Régimen de afiliación calculado automáticamente (VARCHAR MAX). Contributivo, subsidiado, especial u otro régimen determinado por el sistema para el tercero.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RegimenCalculated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el régimen calculado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RegimenCalculated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RegimenCalculated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período o rango de antigüedad de la cuenta por cobrar en días (INT). Indica si está 0-30, 31-60, 61-90 días o superior desde el vencimiento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Age';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el periodo del detalle de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Age';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Age';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de caducidad, vencimiento o prescripción del documento (DATETIME). Límite legal para cobro, glosa o acción coactiva de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de caducidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de atención, unidad funcional o sede donde se prestó el servicio (CHAR 100). Ubicación física de la IPS, clínica, consultorio o hospital.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el centro de atención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo actual pendiente de cobro en la cuenta (NUMERIC 18,2). Deuda vigente sin cancelar tras abonos, retenciones y cruces.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CurrentBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el saldo actual.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CurrentBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CurrentBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo del detalle de la cuenta por cobrar (NUMERIC 38,2). Monto pendiente después de aplicar débitos, créditos y transferencias.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el saldo del detalle de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor aplicado por cruce de cuentas entre deudor y acreedor (DECIMAL 38,2). Compensación entre saldos a favor y en contra.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CrossingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor del cruce.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CrossingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CrossingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor recaudado en efectivo del recibo o comprobante de pago (DECIMAL 38,2). Dinero cobrado al tercero documentado en recaudos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CashReceiptValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el valor de efectivo del recibo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CashReceiptValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CashReceiptValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del cruce o anticipo transferido a otras cuentas (DECIMAL 38,2). Monto movido entre conceptos o anticipos aplicados al saldo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'TransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del cruce de anticipo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'TransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'TransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de crédito u abono aplicado a la cuenta (NUMERIC 38,2). Notas crédito, devoluciones, ajustes positivos o rebajas otorgadas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor del crédito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de débito o cargo aplicado a la cuenta (NUMERIC 38,2). Factura, cobro adicional, intereses o ajustes negativos causados.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor del débito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor inicial de la factura o documento antes de ajustes (NUMERIC 38,2). Monto original sin retenciones, abonos ni modificaciones.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor inicial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor retenido, descuentado o asegurado del documento (NUMERIC 38,2). Retención en la fuente, garantía, depósito o descuento aplicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RetentionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el valor de retención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RetentionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RetentionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del documento factura o comprobante (NUMERIC 18,2). Monto bruto cobrable incluyendo impuestos antes de ajustes.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del documento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la cuenta contable principal o de destino (VARCHAR 100). Denominación del rubro donde se impacta contablemente el movimiento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'MainAccountName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la cuenta principal establecida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'MainAccountName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'MainAccountName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código de la cuenta contable principal (VARCHAR 50). Referencia PUC u otro plan de cuentas para contabilización.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'MainAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de la cuenta principal establecida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'MainAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'MainAccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del radicado: A (Activo), I (Inactivo) u otro (CHAR 1). Indica si el documento fue formalmente presentado, tramitado o cancelado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el estado del radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de expedición, emisión o generación del documento (DATETIME). Cuándo se creó la factura, recibo o comprobante.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la fecha del documento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se formalizó o presentó oficialmente el radicado (DATETIME). Cuándo ingresó formalmente el documento al sistema administrativo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha del radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario que realizó la radicación (VARCHAR 20). Persona responsable de formalizar el documento en el sistema.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el codigo de usuario que realizó la radicación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del radicado asignado por el sistema (INT). Correlativo único para trazabilidad del documento radiado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el número del consecutivo del radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia de la cuenta sin asignar número de radicado formal (VARCHAR 50). Documento en trámite o pendiente de formalización.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la cuenta sin número de radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del contrato vigente (VARCHAR 100). Identificación de la póliza, acuerdo o convenio suscrito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ContractName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el nombre del contrato.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ContractName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ContractName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del contrato en el sistema (VARCHAR 20). Referencia para búsqueda y trazabilidad de términos contractuales.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del contrato.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del grupo o red de atención asociada (VARCHAR 100). Red de prestadores, grupo empresarial o alianza asistencial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CareGroupName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CareGroupName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CareGroupName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo de atención en el catálogo (VARCHAR 20). Identificador de la agrupación de centros de servicio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CareGroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CareGroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CareGroupCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Régimen de afiliación del tercero: contributivo, subsidiado, especial (VARCHAR 50). Modalidad de aseguramiento en salud del paciente o cliente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Regimen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el régimen.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Regimen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Regimen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría de clasificación del detalle: cotizante, beneficiario, etc. (VARCHAR 123). Tipo de afiliado, paciente o tercero en el proceso.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Category';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece la categoria del detalle.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Category';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Category';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identificación: CC, CE, PA, NIT, etc. (INT). Código que especifica la clase de cédula o documento válido.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el tipo de identificación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del tercero, paciente o cliente asociado (VARCHAR 300). Razón social o nombre civil del deudor de la cuenta.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del tercero asociado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación tributaria o cédula del tercero (VARCHAR 25). NIT, cédula, pasaporte u otro identificador único del tercero.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyNit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit del tercero asociado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyNit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo inicial de la cuenta al período reportado (INT). Deuda inicial antes de movimientos de débito y crédito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OpeningBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el saldo inicial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OpeningBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OpeningBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Término, plazo o período del detalle en el reporte (INT). Rango de días de antigüedad o vencimiento de la obligación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el término del detalle en el reporte.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de recursos, cuotas o partidas compartidas (INT). Cantidad de registros o fracciones que integran el movimiento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'NumberShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece número de recursos compartidos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'NumberShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'NumberShares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del estado del portafolio: Activo, Inactivo, etc. (VARCHAR 25). Descripción si la cartera está vigente o cancelada.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatusName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el nombre del estado del portfolio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatusName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatusName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del portafolio: 1=Activo, 0=Inactivo (INT). Código que indica si la cartera está en cobro o archivada.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el estado del portfolio 1 - Activo, 0 - Inactivo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cuenta por cobrar: factura, nota débito, etc. (INT). Clasificación del documento según su naturaleza contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de generación o corte de la cuenta por cobrar (DATETIME). Cuándo se originó la obligación de pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del documento o factura (VARCHAR 20). Número de referencia para búsqueda rápida del comprobante.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del documento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de anticipos al final para agrupación de reporte (INT). Orden o secuencia de anticipos en la presentación final.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OrderByAdvanceAtEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el número del anticipo al final por el que se desea agrupar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OrderByAdvanceAtEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OrderByAdvanceAtEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de agrupación seleccionado para el reporte (VARCHAR MAX). Criterio de ordenamiento: por contrato, centro, régimen, etc.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OrderBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el campo por el que se desea agrupar los datos del reporte.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OrderBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OrderBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del informe de cartera por rangos de edad (INT). FK a ReportPortfolioByAge para trazabilidad del reporte padre.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdReportPortfolioByAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del informe de cartera por edades.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdReportPortfolioByAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdReportPortfolioByAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro en el detalle (INT). Clave primaria del detalle, sin repeticiones.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidad autoincrementada del registro (INT). Identificador técnico secuencial para integridad referencial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdIdentity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la identificación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdIdentity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdIdentity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del informe de cartera por antigüedad de saldos. Registra cada documento de cuentas por cobrar con su estado de cartera, valores de movimiento (débitos, créditos, recaudos, cruces), saldos vigentes, información del tercero pagador (aseguradora, EPS, entidad), contrato, radicación de facturas y estado de glosas; permite analizar la antigüedad de la deuda por rango de días vencidos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail';
