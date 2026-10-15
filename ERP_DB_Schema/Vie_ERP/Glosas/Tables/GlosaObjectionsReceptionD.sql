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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de la radicación de la respuesta ante la EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Información de quien recibe la radicación de la respuesta ante la EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedReceiver';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la radicación de la respuesta ante la EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable de radicación de la respuesta ante la EAPB', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicateResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Sin Confirmar   2- Confirmada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario general de la factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Factura.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Documento   1 - glosa      2 - reiteracion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo estado de ERP', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'ObservationInvoiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id configuracion de interfaces', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion de cartera', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la recepcion de objeciones.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosaObjectionsReceptionCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de detalle de facturas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionD', @level2type = N'COLUMN', @level2name = N'Id';

