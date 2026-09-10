CREATE TABLE [Portfolio].[RadicateInvoiceC] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RadicatedConsecutive] INT           NOT NULL,
    [CustomerId]           INT           NOT NULL,
    [RadicatedDate]        DATETIME      NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [State]                CHAR (1)      NOT NULL,
    [RadicatedUser]        INT           NULL,
    [Comment]              VARCHAR (MAX) NULL,
    [CustomerRadicateConsecutive] VARCHAR (100) NULL,
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



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal ROWVERSION (timestamp binario) que registra automáticamente el instante de creación, modificación o cambio de estado del radicado de factura. Usado para control de concurrencia y auditoría de eventos en la tabla.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se realizó la última modificación del registro de radicado. NULL si no ha sido modificado desde su creación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que realizó la última modificación del radicado. NULL si el registro no ha sido editado tras su creación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de radicado en el sistema. Por defecto obtiene la fecha actual del servidor.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que creó el registro de radicado. Por defecto valor 999 si no se especifica otro usuario.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo (VARCHAR MAX) con observaciones, notas o justificación ingresada por el usuario al confirmar el radicado de factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmComment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario de confirmacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmComment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmComment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del usuario que confirmó oficialmente el radicado. NULL mientras esté en estado pendiente de confirmación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de confirmacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de confirmación del radicado ingresada manualmente por el usuario. NULL si aún no ha sido confirmado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Confirmacion ingresada por el usuario', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de confirmación registrada automáticamente por el sistema. NULL si el radicado no ha sido confirmado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmDateSystem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Confirmacion ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmDateSystem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'ConfirmDateSystem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a Budget.Recognition) del reconocimiento presupuestario que generó este radicado. Solo se completa si al menos una factura en el detalle tiene integración con presupuesto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del reconocimiento que genero el radicado, este campo solo se llena si una de las facturas del detalle del radicado tiene integracion con presupuesto', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RecognitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo (VARCHAR MAX) con observaciones, notas o contexto general del radicado de factura ante el cliente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario de Radicacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del usuario que radicó, registró o ingresó oficialmente el radicado en el sistema. NULL si aún no ha sido radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Radicacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Carácter (CHAR 1) que indica el estado del radicado: ''''1''''=Sin Confirmar, ''''2''''=Confirmado, ''''4''''=Anulado. Controla el flujo de aprobación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. Sin Confirmar  2. Confirmado  4. Anulado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de emisión del oficio o documento de cobro radicado. Diferente de la fecha de radicación en el sistema.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del oficio', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que el sistema registró el radicado de la factura. Marca el inicio del trámite administrativo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de radicado del sistema', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a Common.Customer) del cliente, tercero o empresa a quien se radica la factura o documento de cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Relacion del cliente del radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial único (INT) del radicado. Usado como identificador lógico o número de trámite ante el cliente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del numero de radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico autoincrementable (INT IDENTITY) de la cabecera del radicado de factura o cuenta de cobro. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonomunerico de cabecera de cuenta de cobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de radicación asignado por la entidad pagadora (cliente) al recibir el paquete de facturas. Alfanumérico de hasta 100 caracteres. Se registra una vez el cliente confirma la recepción del radicado. No editable cuando el radicado está en estado Confirmado (State=''2'').', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CustomerRadicateConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Radicado Cliente - Consecutivo de radicación de la entidad pagadora', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC', @level2type = N'COLUMN', @level2name = N'CustomerRadicateConsecutive';


GO
CREATE NONCLUSTERED INDEX [IX_RadicateInvoiceC_RadicatedConsecutive]
    ON [Portfolio].[RadicateInvoiceC]([RadicatedConsecutive] ASC)
    INCLUDE([DocumentDate]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Radicación de facturas de cartera por cliente (aseguradora, pagador o empresa). Registra cada radicado de cobro con su consecutivo, fechas, estado de trámite, confirmación y observaciones del proceso de facturación y glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RadicateInvoiceC';
