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
    CONSTRAINT [UQ_GlosaObjectionsReceptionC_RadicatedConsecutive] UNIQUE NONCLUSTERED ([RadicatedConsecutive] ASC),
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la respuesta de radicación vinculada; referencia a RadicateResponse.Id para trazar la resolución del oficio de objeciones/glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'IdRadicateResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la respuesta de radicación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'IdRadicateResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'IdRadicateResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) de auditoría; registra el instante exacto de creación, modificación o cambio de estado del registro de recepción de objeciones.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro; permite auditar cambios posteriores a la creación inicial.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación; identifica quién actualizó el registro de objeciones/glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro; generada automáticamente por defecto con Common.getdate().', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro; por defecto 999 si no se especifica; auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que confirmó la recepción de objeciones; vinculado al cambio de estado a ''''Confirmado''''.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de confirmacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que radicó el oficio de objeciones; responsable del registro inicial de entrada.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se confirmó la recepción de objeciones; marca el paso de ''''Sin Confirmar'''' a ''''Confirmado''''.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario largo (VARCHAR 500) registrado en el oficio radicado; documentación adicional o notas del proceso de recepción.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentCommentRadicated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario de radicado de oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentCommentRadicated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentCommentRadicated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificación (VARCHAR 200) de la persona física que recibió/receptó el oficio de objeciones/glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesTheSettled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona que recepciona el radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesTheSettled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesTheSettled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación del oficio de respuesta a las objeciones; marca cuándo se formalizó la respuesta en el sistema.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DateRadicatedDocumentReply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de radicado oficio respuesta.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DateRadicatedDocumentReply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DateRadicatedDocumentReply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de envío del oficio de respuesta a objeciones; indica cuándo se remitió la resolución al tercero/cliente.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DateResponsePostDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envio de respuesta de oficio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DateResponsePostDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DateResponsePostDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (CHAR 1) del registro: 1=Sin Confirmar, 2=Confirmado Radicado, 3=Oficio Con Respuesta Enviada, 4=Anulada; controla flujo de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la cabecera   1: Sin Confirmar   2: Confirmado Radicado   3: Oficio Con respuesta Enviada   4: Anulada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario (VARCHAR 250) general sobre la recepción de objeciones; observaciones o notas breves del proceso.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del oficio de objeciones; fecha en que se originó el documento formal de recurso/glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación en el sistema; cuándo ingresó formalmente el oficio de objeciones al ERP.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK, INT) del tercero/cliente al que se notifica la recepción de objeciones; referencia a Common.Customer.Id.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero al cual dse le hace la recepcion de objeciones.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del oficio (VARCHAR 30); identificador único del documento de objeciones junto con RadicatedConsecutive (UNIQUE).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de radicación (INT); correlativo único junto con DocumentNumber para identificar unívocamente el oficio radicado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del Radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario (INT IDENTITY); clave única y autonumérica de la cabecera de recepción de objeciones/glosa en el sistema.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico Cabecera recepcion de objeciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de recepciones de objeciones a glosas: guarda cada documento de objeción radicado por una entidad pagadora (aseguradora, EPS, cliente), con sus fechas de radicación, respuesta y confirmación, el estado de la objeción y los comentarios asociados al proceso de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC';
