CREATE TABLE [Portfolio].[PortfolioAdvance] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)    NOT NULL,
    [CashReceiptId]           INT             NULL,
    [CashReceiptDetailId]     INT             NULL,
    [AdmissionNumber]         CHAR (10)       NULL,
    [ThirdPartyId]            INT             NOT NULL,
    [MainAccountId]           INT             NOT NULL,
    [CostCenterId]            INT             NULL,
    [DocumentDate]            DATETIME        NOT NULL,
    [CustomerId]              INT             NULL,
    [SellerId]                INT             NULL,
    [Value]                   NUMERIC (18, 2) NOT NULL,
    [TransferValue]           NUMERIC (18, 2) NOT NULL,
    [DebitValue]              DECIMAL (18, 2) NOT NULL,
    [CreditValue]             DECIMAL (18, 2) NOT NULL,
    [DistributionValue]       DECIMAL (18, 2) CONSTRAINT [DF_PortfolioAdvance_DistributionValue] DEFAULT ((0)) NOT NULL,
    [Balance]                 DECIMAL (18, 2) NOT NULL,
    [Observations]            VARCHAR (300)   NULL,
    [OpeningBalance]          BIT             CONSTRAINT [DF_PortfolioAdvance_OpeningBalance] DEFAULT ((0)) NOT NULL,
    [Status]                  TINYINT         NOT NULL,
    [CreationUser]            VARCHAR (20)    NOT NULL,
    [CreationDate]            DATETIME        NOT NULL,
    [ModificationUser]        VARCHAR (20)    NULL,
    [ModificationDate]        DATETIME        NULL,
    [ConfirmationUser]        VARCHAR (20)    NULL,
    [ConfirmationDate]        DATETIME        NULL,
    [AnnulmentUser]           VARCHAR (20)    NULL,
    [AnnulmentDate]           DATETIME        NULL,
    [TimeStamp]               ROWVERSION      NOT NULL,
    [CurrencyId]              INT             NULL,
    [TRMValue]                NUMERIC (20, 5) CONSTRAINT [DF__Portfolio__TRMVa__21282E8F] DEFAULT ((1)) NOT NULL,
    [ValueInCurrencyHeader]   NUMERIC (20, 5) CONSTRAINT [DF__Portfolio__Value__0362C17E] DEFAULT ((0)) NOT NULL,
    [ThirdPartyBeneficiaryId] INT             NULL,
    CONSTRAINT [PK_PortfolioAdvance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioAdvance_CashReceiptDetails] FOREIGN KEY ([CashReceiptDetailId]) REFERENCES [Treasury].[CashReceiptDetails] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_CashReceipts] FOREIGN KEY ([CashReceiptId]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_Seller] FOREIGN KEY ([SellerId]) REFERENCES [Common].[Seller] ([Id]),
    CONSTRAINT [FK_PortfolioAdvance_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioAdvance] NOCHECK CONSTRAINT [FK_PortfolioAdvance_CashReceiptDetails];




GO
ALTER TABLE [Portfolio].[PortfolioAdvance] NOCHECK CONSTRAINT [FK_PortfolioAdvance_CashReceiptDetails];


GO



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_PortfolioAdvance__CashReceiptDetailId]
    ON [Portfolio].[PortfolioAdvance]([CashReceiptDetailId] ASC);


GO
-- =============================================
-- Author:		<Giovanny Plazas>
-- Create date: <20/06/2024>
-- Description:	<Trigger para insertar la tasa del anticipo cuando se confirme>
-- =============================================

CREATE TRIGGER [Portfolio].[TG_PortfolioAdvanceRates]
ON [Portfolio].[PortfolioAdvance]
AFTER INSERT,UPDATE
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @_portfolioAdvanceId INT, @_currencyId INT , @_documentDate DATE;

	IF EXISTS(	SELECT 1 
				FROM INSERTED i
				left join Portfolio.PortfolioAdvanceExchangeRate paer with(NOLOCK) on i.Id=paer.PortfolioAdvanceId
				WHERE i.Status=2 and paer.Id is NULL)
	BEGIN
		/*-------------------INSERCION TABLA EXGANGED DE Advanced--------*/
		DECLARE @TablePortfolioAdvance TABLE (Id INT, CurrencyId INT, DocumentDate DATE);

		INSERT INTO @TablePortfolioAdvance (Id, CurrencyId,DocumentDate)
		SELECT i.Id, i.CurrencyId , i.DocumentDate
		FROM [INSERTED] i
		left join Portfolio.PortfolioAdvanceExchangeRate paer with(NOLOCK) on i.Id=paer.PortfolioAdvanceId
		WHERE i.Status=2 and paer.Id is NULL

		DECLARE @rowcount INT;
		SET @rowcount = (select count(*) from @TablePortfolioAdvance );;

		WHILE (@rowcount > 0)
		BEGIN
			SELECT TOP 1 @_portfolioAdvanceId = Id, @_currencyId = CurrencyId , @_documentDate = DocumentDate
			FROM @TablePortfolioAdvance;

			DECLARE @StateResult INT, @MessageOutput VARCHAR(100);

			EXEC [Common].[SP_InsertIntoExchangeRateWithDate] 
				'PortfolioAdvance', @_portfolioAdvanceId, @_currencyId, @_documentDate,
				@StateResult=@StateResult OUTPUT, @MessageOutput = @MessageOutput OUTPUT;

			IF ISNULL(@StateResult, 999) = 999
			BEGIN
				-- Lanzar la excepción con código de error y mensaje
				THROW 51000, 'Error al guardar el TRM del documento', 1
				BREAK; -- Exit the loop on error
			END

			DELETE FROM @TablePortfolioAdvance where Id=@_portfolioAdvanceId;
			SET @rowcount = (select count(*) from @TablePortfolioAdvance );
		END;

		/*--------------------------------------------------------------------*/
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero beneficiario resultante del recibo de caja. Depende del tipo de concepto de recibo seleccionado. Referencia a Common.ThirdParty (INT, FK).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyBeneficiaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el tercero beneficiario resultante del recibo de caja, este dato depende del tipo de concepto de recibo de caja seleccionado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyBeneficiaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyBeneficiaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor convertido a la moneda de cabecera del documento. Tipo NUMERIC(20,5), default 0. Cálculo de conversión de moneda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor convertido a la moneda de la cabecera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de Cambio Real (TRM) utilizada para calcular el valor de cuentas por cobrar. Tipo NUMERIC(20,5), default 1. Conversión de moneda extranjera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TRM con el que se calculo el valor de la cxc', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda de la nota débito/crédito. Referencia a Common.Currency (INT, FK). Sinónimos: divisa, moneda, múltiples opciones.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda de la nota deb/cred', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría que registra el instante exacto de creación, registro o modificación del documento. Tipo TIMESTAMP, no editable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se anuló el documento de anticipo. Tipo DATETIME NULL. Estado anulado = 3.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario responsable de la anulación del documento. Identificación del auditor que canceló. Tipo VARCHAR(20) NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación oficial del anticipo. Tipo DATETIME NULL. Transición a estado confirmado = 2.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmó el documento. Identificación del profesional de salud o administrador autorizante. Tipo VARCHAR(20) NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro. Tipo DATETIME NULL. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación. Identificación del editor. Tipo VARCHAR(20) NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del documento de anticipo. Tipo DATETIME NOT NULL. Auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el documento. Identificación del originador. Tipo VARCHAR(20) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del documento: 1=Registrado, 2=Confirmado, 3=Anulado. Tipo TINYINT NOT NULL. Flujo de estados del anticipo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento (Registrado = 1, Confirmado = 2, Anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que señala si el anticipo corresponde a saldo inicial. Tipo BIT, default 0. Apertura de ejercicio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'OpeningBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si es Saldo Inicial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'OpeningBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'OpeningBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de observaciones libres del anticipo. Notas, comentarios o información adicional. Tipo VARCHAR(300) NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo actual del anticipo tras débitos, créditos y distribuciones. Tipo DECIMAL(18,2) NOT NULL. Extracción de fondos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Balance del anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importe distribuido del anticipo hacia conceptos específicos. Dinero girado del total disponible. Tipo DECIMAL(18,2) NOT NULL, default 0.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DistributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los valores que se han distribuido del anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DistributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DistributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor acreditado (ingreso/abono) al anticipo. Dinero que entra. Tipo DECIMAL(18,2) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que acredita en el anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor debitado (egreso/retiro) del anticipo. Dinero que sale. Tipo DECIMAL(18,2) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se a debitado al anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor trasladado de o hacia el anticipo. Movimiento entre cuentas. Tipo NUMERIC(18,2) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Traslado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'TransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor principal del anticipo otorgado. Monto base de dinero adelantado. Tipo NUMERIC(18,2) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del vendedor/profesional de salud asociado al anticipo. Referencia a Common.Seller (INT, FK).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'SellerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del vendedor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'SellerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'SellerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cliente/paciente asociado al anticipo. Referencia a Common.Customer (INT, FK).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cliente asociado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento de anticipo. Tipo DATETIME NOT NULL. Fecha operacional del movimiento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo que genera el anticipo. Referencia a Payroll.CostCenter (INT, FK). Unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable general asociada. Referencia a GeneralLedger.MainAccounts (INT, FK). Contabilidad RIPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero otorgador del anticipo. Referencia a Common.ThirdParty (INT, FK NOT NULL). Proveedor, EPS, IPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente asociado al anticipo. Extraído de tabla Crystal ADINGRESO. Tipo CHAR(10) NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero del ingreso del paciente asociado al anticipo, esto se saca de la tabla ADINGRESO de Crystal', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del recibo de caja relacionado. Referencia a Treasury.CashReceiptDetails (INT, FK). Movimiento tesorería.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del detalle del recibo de caja', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del recibo de caja origen del anticipo. Referencia a Treasury.CashReceipts (INT, FK). Comprobante tesorería.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CashReceiptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de recibo de caja', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CashReceiptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'CashReceiptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del anticipo. Identificador alfanumérico para búsqueda rápida. Tipo VARCHAR(20) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y secuencial del registro de anticipo. Clave primaria INT IDENTITY. Índice clustered para búsqueda principal.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anticipos y pagos adelantados de cartera recibidos de terceros (clientes, aseguradoras, pagadores). Registra los valores recibidos antes de aplicar a una factura específica, incluyendo saldos disponibles, distribuciones parciales, traslados y anulaciones. Se usa en procesos de recaudo, conciliación de pagos y gestión de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvance';
