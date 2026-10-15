CREATE TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioNoteId]               INT             NOT NULL,
    [AccountReceivableId]           INT             NULL,
    [AccountReceivableShareId]      INT             NULL,
    [MainAccountId]                 INT             NULL,
    [AccountReceivableAccountingId] INT             NULL,
    [PortfolioAdvanceId]            INT             NULL,
    [AdjusmentValue]                NUMERIC (20, 2) NOT NULL,
    [PercentageValue]               NUMERIC (5, 2)  NOT NULL,
    [PreviousBalance]               NUMERIC (20, 2) NOT NULL,
    [Balance]                       NUMERIC (20, 2) NOT NULL,
    [ConceptId]                     INT             NULL,
    CONSTRAINT [PK_PortfolioNoteAccountReceivableAdvance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivableAccounting] FOREIGN KEY ([AccountReceivableAccountingId]) REFERENCES [Portfolio].[AccountReceivableAccounting] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivableShare] FOREIGN KEY ([AccountReceivableShareId]) REFERENCES [Portfolio].[AccountReceivableShare] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_PortfolioNote] FOREIGN KEY ([PortfolioNoteId]) REFERENCES [Portfolio].[PortfolioNote] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivable];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivableAccounting];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivableShare];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_PortfolioAdvance];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_PortfolioNote];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PortfolioNoteAccountReceivableAdvance]
    ON [Portfolio].[PortfolioNoteAccountReceivableAdvance]([PortfolioNoteId] ASC, [AccountReceivableId] ASC, [AccountReceivableShareId] ASC, [MainAccountId] ASC, [PortfolioAdvanceId] ASC);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-06
-- Description:	Trigger para que genere error cuando se inserta la misma factura o un anticipo en un rango de 10 minutos
-- =============================================
CREATE TRIGGER [Portfolio].[tggValidatePortfolioNoteAccountReceivableAdvanceDuplicate]
   ON [Portfolio].[PortfolioNoteAccountReceivableAdvance]
   AFTER INSERT
AS 
BEGIN		
	IF EXISTS 
	(
		SELECT 1
		FROM Portfolio.PortfolioNote pni WITH (NOLOCK)
		JOIN INSERTED pnarai WITH (NOLOCK) ON pni.Id = pnarai.PortfolioNoteId
		JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK) 
			ON ISNULL(pnarai.AccountReceivableId, 0) = ISNULL(pnara.AccountReceivableId, 0)
				AND ISNULL(pnarai.PortfolioAdvanceId, 0) = ISNULL(pnara.PortfolioAdvanceId, 0)
		JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pnara.PortfolioNoteId = pn.Id
				AND pni.NoteType = pn.NoteType AND pn.Status = 2
				AND pni.Id <> pn.Id 
		WHERE ABS(DATEDIFF(MINUTE, pn.ConfirmationDate, pni.ConfirmationDate)) < 10
	)
	BEGIN
		THROW 51000, 'Validacion tggValidatePortfolioNoteAccountReceivableAdvanceDuplicate: No se puede agregar el registro porque hace menos de 10 minutos ya se registro un detalle similar', 1
	END
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [Portfolio].[TriggerValidateData]
   ON  [Portfolio].[PortfolioNoteAccountReceivableAdvance]
   AFTER  INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if (select count(*) from inserted i inner join Portfolio.PortfolioNote pn on i.PortfolioNoteId = pn.Id where pn.NoteType in(1,2) and i.AccountReceivableAccountingId is null and i.AccountReceivableShareId is null ) > 0 begin
		declare @xx as varchar(200) = 'Datos Nulos';
		THROW 51000, @xx, 1
	end

    -- Insert statements for trigger here

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de la Nota relacionado con la factura electrónica. Dependiendo de la naturaleza, puede ser:  
    - Débito:  
      1. Intereses  
      2. Gastos por cobrar  
      3. Cambio de valor  
    - Crédito:  
      1. Devolución o no aceptación de partes del servicio  
      2. Anulación de factura electrónica  
      3. Rebaja total aplicada  
      4. Descuento total aplicado  
      5. Rescisión: nulidad por falta de requisitos  
      6. Otros  
	  7. Sin referencia a una factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Saldo de la CXC o del anticipo, Es decir que es el nuevo saldo de CXC o del anticipo despues de que se ejecuto la nota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el saldo anterior de la factura o del anticipo, es decir que es el saldo que tenia antes de ser afectada por la nota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'PreviousBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentage que afecta el valor ajustado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'PercentageValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor ajustado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'AdjusmentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'AccountReceivableAccountingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuota de la cuenta por pagar que se va afectar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'AccountReceivableShareId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la nota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'PortfolioNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'Id';

