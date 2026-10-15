CREATE TABLE [Portfolio].[RadicateInvoiceC] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RadicatedConsecutive] INT           NOT NULL,
    [CustomerId]           INT           NOT NULL,
    [RadicatedDate]        DATETIME      NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [State]                CHAR (1)      NOT NULL,
    [RadicatedUser]        INT           NULL,
    [Comment]              VARCHAR (MAX) NULL,
    [RecognitionId]        INT           NULL,
    [ConfirmDateSystem]    DATETIME      NULL,
    [ConfirmDate]          DATETIME      NULL,
    [ConfirmUser]          INT           NULL,
    [ConfirmComment]       VARCHAR (MAX) NULL,
    [CreationUser]         VARCHAR (20)  CONSTRAINT [DF_RadicateInvoiceC_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]         DATETIME      CONSTRAINT [DF_RadicateInvoiceC_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    CONSTRAINT [PK_RadicateInvoiceC__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RadicateInvoiceC_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_RadicateInvoiceC_Recognition] FOREIGN KEY ([RecognitionId]) REFERENCES [Budget].[Recognition] ([Id])
);


GO
ALTER TABLE [Portfolio].[RadicateInvoiceC] NOCHECK CONSTRAINT [FK_RadicateInvoiceC_Customer];


GO
ALTER TABLE [Portfolio].[RadicateInvoiceC] NOCHECK CONSTRAINT [FK_RadicateInvoiceC_Recognition];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_RadicateInvoiceC__RadicatedConsecutive]
    ON [Portfolio].[RadicateInvoiceC]([RadicatedConsecutive] ASC);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2018-06-01
-- Description:	Se valida que el consecutivo siga la una secuencia
--				(Exista un valor anterior)
-- =============================================
CREATE TRIGGER [Portfolio].[tgg_ValidateRadicatedConsecutive]
   ON  [Portfolio].[RadicateInvoiceC]
   AFTER INSERT, UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Validamos que exista un registro con el consecutivo anterior al actual
    IF NOT EXISTS 
	(
		SELECT i.RadicatedConsecutive
		FROM INSERTED i
		WHERE EXISTS 
		(
			SELECT ric.RadicatedConsecutive
			FROM Portfolio.RadicateInvoiceC ric
			WHERE ric.RadicatedConsecutive  = (i.RadicatedConsecutive - 1) OR (i.RadicatedConsecutive - 1) =0
		)
	)
	BEGIN 
		--Si no existe validamos que no sea el primer registro que se esta insertando en la tabla
		IF EXISTS
		(
			SELECT ric.Id
			FROM Portfolio.RadicateInvoiceC ric
			LEFT JOIN inserted i ON ric.Id = i.Id
			WHERE i.Id IS NULL
		)
		BEGIN
			THROW 51000, 'Error generado por control desde trigger. El consecutivo no sigue el orden preestablecido', 1
		END		
	END
END
GO
DISABLE TRIGGER [Portfolio].[tgg_ValidateRadicatedConsecutive]
    ON [Portfolio].[RadicateInvoiceC];


GO
CREATE INDEX IX_RadicateInvoiceC_RadicatedConsecutive
    ON Portfolio.RadicateInvoiceC (RadicatedConsecutive)
    INCLUDE (DocumentDate);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario de confirmacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmComment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de confirmacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Confirmacion ingresada por el usuario', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Confirmacion ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmDateSystem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del reconocimiento que genero el radicado, este campo solo se llena si una de las facturas del detalle del radicado tiene integracion con presupuesto', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RecognitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario de Radicacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Radicacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. Sin Confirmar  2. Confirmado  4. Anulado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del oficio', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de radicado del sistema', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Relacion del cliente del radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del numero de radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonomunerico de cabecera de cuenta de cobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'Id';

