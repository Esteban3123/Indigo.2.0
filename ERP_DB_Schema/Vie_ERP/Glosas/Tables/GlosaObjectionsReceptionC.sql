CREATE TABLE [Glosas].[GlosaObjectionsReceptionC] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RadicatedConsecutive]       INT           NOT NULL,
    [DocumentNumber]             VARCHAR (30)  NOT NULL,
    [CustomerId]                 INT           NOT NULL,
    [RadicatedDate]              DATETIME      NOT NULL,
    [DocumentDate]               DATETIME      NOT NULL,
    [Comment]                    VARCHAR (250) NOT NULL,
    [State]                      CHAR (1)      NOT NULL,
    [DateResponsePostDocument]   DATETIME      NULL,
    [DateRadicatedDocumentReply] DATETIME      NULL,
    [ReceivesTheSettled]         VARCHAR (200) NULL,
    [DocumentCommentRadicated]   VARCHAR (500) NULL,
    [ConfirmDate]                DATETIME      NULL,
    [RadicatedUser]              INT           NULL,
    [ConfirmUser]                INT           NULL,
    [CreationUser]               VARCHAR (20)  CONSTRAINT [DF_GlosaObjectionsReceptionC_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]               DATETIME      CONSTRAINT [DF_GlosaObjectionsReceptionC_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    [TimeStamp]                  ROWVERSION    NOT NULL,
    [IdRadicateResponse]         INT           NULL,
    CONSTRAINT [PK_GlosaObjectionsReceptionC__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaObjectionsReceptionC_RadicateResponse] FOREIGN KEY ([IdRadicateResponse]) REFERENCES [Glosas].[RadicateResponse] ([Id]),
    CONSTRAINT [FK_ObjectionsReceptionC_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [UQ_GlosaObjectionsReceptionC__DocumentNumber__RadicatedConsecutive] UNIQUE NONCLUSTERED ([DocumentNumber] ASC, [RadicatedConsecutive] ASC)
);


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionC] NOCHECK CONSTRAINT [FK_GlosaObjectionsReceptionC_RadicateResponse];


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionC] NOCHECK CONSTRAINT [FK_ObjectionsReceptionC_Customer];


GO
CREATE NONCLUSTERED INDEX [IX_GlosaObjectionsReceptionC_RadicateDate_ConfirmDate_DocumentDate]
    ON [Glosas].[GlosaObjectionsReceptionC]([RadicatedDate] ASC, [ConfirmDate] ASC, [DocumentDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaObjectionsReceptionC__CustomerId]
    ON [Glosas].[GlosaObjectionsReceptionC]([CustomerId] ASC);


GO

-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2020-06-19
-- Description:	Se valida el valor glosado de las facturas coincidan con sus detalles
-- =============================================
CREATE TRIGGER [Glosas].[tgg_GlosaObjectionsReception_ValidateValueGlosado]
   ON  [Glosas].[GlosaObjectionsReceptionC]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	IF EXISTS
	(
		SELECT 1
		FROM INSERTED gor
		JOIN Glosas.GlosaObjectionsReceptionD gord ON gor.Id = gord.GlosaObjectionsReceptionCId
		LEFT JOIN Glosas.GlosaPortfolioGlosada gpg ON gord.InvoiceNumber = gpg.InvoiceNumber
		LEFT JOIN
		(
			SELECT InvoiceNumber, SUM(ValueGlosado) ValueGlosado
			FROM Glosas.GlosaMovementGlosa
			WHERE MainGlosa = 1 And IsNormative = 1
			GROUP BY InvoiceNumber
		) gmg ON gord.InvoiceNumber = gmg.InvoiceNumber
		WHERE gor.State IN ('1', '2', '3')
			AND ROUND(ISNULL(gpg.ValueGlosado, 0), 2) <> ROUND(ISNULL(gmg.ValueGlosado, 0), 2)
	)
	BEGIN
		THROW 51000, 'GlosaObjectionsReceptionC: Error generado por control desde trigger. Existen facturas con valor glosado diferente al de sus detalles', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la respuesta de radicación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'IdRadicateResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de confirmacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario de radicado de oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentCommentRadicated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona que recepciona el radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesTheSettled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de radicado oficio respuesta.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DateRadicatedDocumentReply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de envio de respuesta de oficio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DateResponsePostDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la cabecera   1: Sin Confirmar   2: Confirmado Radicado   3: Oficio Con respuesta Enviada   4: Anulada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero al cual dse le hace la recepcion de objeciones.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del Radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico Cabecera recepcion de objeciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'Id';

