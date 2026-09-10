CREATE TABLE [Glosas].[GlosaObjectionsReceptionD] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GlosaObjectionsReceptionCId] INT           NOT NULL,
    [PortfolioGlosaId]            INT           NOT NULL,
    [GlosasParametersInterfaceId] INT           NULL,
    [ObservationInvoiceCode]      VARCHAR (5)   NULL,
    [DocumentType]                CHAR (1)      NOT NULL,
    [InvoiceNumber]               VARCHAR (50)  NOT NULL,
    [Comment]                     VARCHAR (MAX) NULL,
    [State]                       CHAR (1)      NOT NULL,
    [TimeStamp]                   ROWVERSION    NOT NULL,
    [RadicateResponsibleId]       INT           NULL,
    [RadicatedDate]               DATETIME      NULL,
    [RadicatedReceiver]           VARCHAR (100) NULL,
    [RadicatedObservation]        VARCHAR (200) NULL,
    [ADRESCode]                   VARCHAR(50)   NULL, 
    CONSTRAINT [PK_GlosaObjectionsReceptionD__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosaObjectionsReceptionC] FOREIGN KEY ([GlosaObjectionsReceptionCId]) REFERENCES [Glosas].[GlosaObjectionsReceptionC] ([Id]),
    CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosaPortfolioGlosada] FOREIGN KEY ([PortfolioGlosaId]) REFERENCES [Glosas].[GlosaPortfolioGlosada] ([Id]),
    CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosasParametersInterface] FOREIGN KEY ([GlosasParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id]),
    CONSTRAINT [FK_GlosaObjectionsReceptionD_Responsible] FOREIGN KEY ([RadicateResponsibleId]) REFERENCES [Glosas].[Responsible] ([Id]),
    CONSTRAINT [UQ_GlosaObjectionsReceptionD__GlosaObjectionsReceptionCId__InvoiceNumber] UNIQUE NONCLUSTERED ([GlosaObjectionsReceptionCId] ASC, [InvoiceNumber] ASC)
);


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosaObjectionsReceptionC];


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosaPortfolioGlosada];


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaObjectionsReceptionD_GlosasParametersInterface];


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaObjectionsReceptionD_Responsible];


GO
CREATE NONCLUSTERED INDEX [IX_GORD_ByReceptionC]
    ON [Glosas].[GlosaObjectionsReceptionD]([GlosaObjectionsReceptionCId] ASC)
    INCLUDE([PortfolioGlosaId], [Id]);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaObjectionsReceptionD_InvoiceNumber]
    ON [Glosas].[GlosaObjectionsReceptionD]([InvoiceNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [iPortfolioGlosaId_Glosas_GlosaObjectionsReceptionD_9E9EBF3B]
    ON [Glosas].[GlosaObjectionsReceptionD]([PortfolioGlosaId] ASC);


GO
CREATE NONCLUSTERED INDEX [GlosaObjectionsReceptionD_InvoiceNumer_IND]
    ON [Glosas].[GlosaObjectionsReceptionD]([Id] ASC, [InvoiceNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GlosaObjectionsReceptionD__GlosaObjectionsReceptionCId]
    ON [Glosas].[GlosaObjectionsReceptionD]([GlosaObjectionsReceptionCId] ASC);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2020-06-19
-- Description:	Se valida el valor glosado de las facturas coincidan con sus detalles
-- =============================================
CREATE TRIGGER [Glosas].[tgg_GlosaObjectionsReceptionDetail_ValidateValueGlosado]
   ON  [Glosas].[GlosaObjectionsReceptionD]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	IF EXISTS
	(
		SELECT 1
		FROM INSERTED gord
		LEFT JOIN Glosas.GlosaPortfolioGlosada gpg ON gord.InvoiceNumber = gpg.InvoiceNumber
		LEFT JOIN
		(
			SELECT InvoiceNumber, SUM(ValueGlosado) ValueGlosado
			FROM Glosas.GlosaMovementGlosa
			WHERE MainGlosa = 1
				AND IsNormative = 1
			GROUP BY InvoiceNumber
		) gmg ON gord.InvoiceNumber = gmg.InvoiceNumber
		WHERE ROUND(ISNULL(gpg.ValueGlosado, 0), 2) <> ROUND(ISNULL(gmg.ValueGlosado, 0), 2)
	)
	BEGIN
		THROW 51000, 'GlosaObjectionsReceptionD: Error generado por control desde trigger. El valor glosado de la factura es diferente al de sus detalles', 1
	END
END
GO
-- =============================================
-- Author:		
-- Create date: 05/01/2015
-- Description:	
-- =============================================
CREATE TRIGGER [Glosas].[UpdateState]
ON [Glosas].[GlosaObjectionsReceptionD]
AFTER  UPDATE 
AS

DECLARE @id AS VARCHAR(50)  = (SELECT Id FROM INSERTED) 
Declare @StateGenesis as varchar(2) = (SELECT state FROM DELETED )
Declare @StateGenesis2 as varchar(2) = (SELECT state FROM INSERTED)


if update(state) begin



  INSERT INTO [Glosas].[Log_Audit]
           ([IdRegistro]
		   ,StateOld
		   ,StateNew
		   ,PCName
           ,[CreationUser]
           ,[CreationDate])
     VALUES
           (@id
		   ,@StateGenesis
		   ,@StateGenesis2
		   ,HOST_NAME()
           ,SUSER_NAME()
		    ,GETDATE())

end
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código ADRES' , @level0type=N'SCHEMA',@level0name=N'Glosas', @level1type=N'TABLE',@level1name=N'GlosaObjectionsReceptionD', @level2type=N'COLUMN',@level2name=N'ADRESCode'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas o anotaciones de la radicación (entrega formal) de la respuesta ante la EAPB (Entidad Administradora de Planes de Beneficios); varchar(200)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de la radicación de la respuesta ante la EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre, identificación o información del profesional/funcionario que recibe físicamente la radicación de la respuesta ante la EAPB; varchar(100)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedReceiver';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información de quien recibe la radicación de la respuesta ante la EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedReceiver';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedReceiver';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registra formalmente la radicación (entrega/presentación) de la respuesta ante la EAPB; datetime', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la radicación de la respuesta ante la EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del profesional o funcionario responsable de radicar (entregar formalmente) la respuesta ante la EAPB; FK a Responsible', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicateResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable de radicación de la respuesta ante la EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicateResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicateResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática que registra el instante exacto de creación, modificación o evento en el sistema; timestamp (auditoria)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de confirmación de la recepción: 1=Sin Confirmar, 2=Confirmada; char(1)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- Sin Confirmar   2- Confirmada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario general, observación adicional o nota sobre la factura, glosa o reiteración asociada; varchar(max)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario general de la factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de identificación de la factura, recibo o documento de cobro; varchar(50); parte de unique constraint', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Factura.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de glosa: 1=Glosa (reclamo), 2=Reiteración (insistencia de glosa); char(1)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Documento   1 - glosa      2 - reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de estado u observación de la factura según configuración del ERP Indigo Vie; varchar(5)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'ObservationInvoiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo estado de ERP', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'ObservationInvoiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'ObservationInvoiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la configuración o parámetros de interfaz de glosas; FK a GlosasParametersInterface', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id configuracion de interfaces', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la relación de cartera glosada; vincula la glosa al portafolio de deuda; FK a GlosaPortfolioGlosada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion de cartera', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera (encabezado) del proceso de recepción de objeciones/respuestas; FK a GlosaObjectionsReceptionC', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosaObjectionsReceptionCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la recepcion de objeciones.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosaObjectionsReceptionCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosaObjectionsReceptionCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable del detalle (línea) de factura en recepción de objeciones; int identity(1,1)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de detalle de facturas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las glosas recibidas en el proceso de objeciones: registra cada ítem glosado asociado a una recepción de objeción, incluyendo el número de factura, tipo de documento, estado, observaciones y datos del radicado de respuesta ante el pagador.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD';
