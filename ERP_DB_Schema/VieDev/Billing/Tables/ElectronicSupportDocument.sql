CREATE TABLE [Billing].[ElectronicSupportDocument] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)    NOT NULL,
    [DocumentDate]           DATETIME        NOT NULL,
    [RadicationDate]         DATETIME        NOT NULL,
    [DueDate]                DATETIME        NOT NULL,
    [SupplierThirdPartyId]   INT             NOT NULL,
    [CustomerThirdPartyId]   INT             NOT NULL,
    [BillingAuthorizationId] INT             NOT NULL,
    [Description]            VARCHAR (500)   NOT NULL,
    [SubTotalValue]          DECIMAL (18, 2) NOT NULL,
    [TaxValue]               DECIMAL (18, 2) NOT NULL,
    [TotalValue]             DECIMAL (18, 2) NOT NULL,
    [Status]                 BIT             NOT NULL,
    [CUDS]                   VARCHAR (500)   NULL,
    [EntityCode]             VARCHAR (20)    NULL,
    [EntityName]             VARCHAR (250)   NULL,
    [EntityId]               INT             NULL,
    [CreationUser]           VARCHAR (20)    NOT NULL,
    [CreationDate]           DATETIME        NOT NULL,
    [ModificationUser]       VARCHAR (20)    NULL,
    [ModificationDate]       DATETIME        NULL,
    [TimeStamp]              ROWVERSION      NOT NULL,
    [DocumentNumber]         VARCHAR (50)    NULL,
    [OperativeUnitId]        INT             CONSTRAINT [DF__Electroni__Opera__61F6E0BB] DEFAULT ((1)) NOT NULL,
    [Retry]                  INT             CONSTRAINT [DF__Electroni__Retry__0FBDAB6B] DEFAULT ((0)) NOT NULL,
    [StatusElectronic]       TINYINT         CONSTRAINT [DF__Electroni__Statu__10B1CFA4] DEFAULT ((0)) NOT NULL,
    [ShippingDate]           DATETIME        CONSTRAINT [DF__Electroni__Shipp__302A7AFD] DEFAULT ('2022-08-01') NULL,
    [ZipKey]                 VARCHAR (500)   NULL,
    [Year]                   INT             CONSTRAINT [DF_ElectronicSupportDocument_Year] DEFAULT ((2022)) NOT NULL,
    [Consecutive]            INT             CONSTRAINT [DF_ElectronicSupportDocument_Consecutive] DEFAULT ((0)) NOT NULL,
    [FilePath]               VARCHAR (250)   NULL,
    [QR]                     VARCHAR (500)   NULL,
    CONSTRAINT [PK_ElectronicSupportDocument__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicSupportDocument_BillingAuthorization] FOREIGN KEY ([BillingAuthorizationId]) REFERENCES [Billing].[BillingAuthorization] ([Id]),
    CONSTRAINT [FK_ElectronicSupportDocument_ThirdParty] FOREIGN KEY ([SupplierThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_ElectronicSupportDocument_ThirdParty1] FOREIGN KEY ([CustomerThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Billing].[ElectronicSupportDocument] NOCHECK CONSTRAINT [FK_ElectronicSupportDocument_BillingAuthorization];


GO
ALTER TABLE [Billing].[ElectronicSupportDocument] NOCHECK CONSTRAINT [FK_ElectronicSupportDocument_ThirdParty];


GO
ALTER TABLE [Billing].[ElectronicSupportDocument] NOCHECK CONSTRAINT [FK_ElectronicSupportDocument_ThirdParty1];


GO
CREATE UNIQUE NONCLUSTERED INDEX [ix_EntityId_EntityName]
    ON [Billing].[ElectronicSupportDocument]([EntityId] ASC, [EntityName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ElectronicSupportDocument_Status_StatusElectronic]
    ON [Billing].[ElectronicSupportDocument]([Status] ASC, [StatusElectronic] ASC);


GO
-- =============================================
-- Author:		Juan David Capera Núñez
-- Create date: 2022-08-01
-- Description:	Trigger para que genere automáticamente el consecutivo del documento soporte electrónico
-- =============================================
CREATE TRIGGER [Billing].[tggGenerateConsecutiveDocumentElectronicSupport]
ON [Billing].[ElectronicSupportDocument]
 AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON

	DECLARE @Consecutive INT = 0
	
	--Se obtiene el último documento por año
	SELECT @Consecutive = MAX(esd.Consecutive)
	FROM INSERTED i
	JOIN Billing.ElectronicSupportDocument esd ON esd.Year = i.Year

	/* Una vez obtenido, procedo a actualizarlo en el registro nuevo, en caso de que no se encuentre el registro, 
	   significa que el año ha cambiado, por consiguiente el consecutivo vuelva a empezar en 1 */
	UPDATE esd
		SET esd.Consecutive = ISNULL(@Consecutive, 0) + 1
	FROM INSERTED i
	JOIN Billing.ElectronicSupportDocument esd ON esd.Id = i.Id
END
GO
-- ==============================================
-- Descriptio: Se crea trigger para validar que todas las autorizaciones se han de tipo DS 
-- Author: Juan David Capera
-- ==============================================
CREATE TRIGGER [Billing].[tgg_ValidateAuthorization] 
   ON [Billing].[ElectronicSupportDocument] 
   AFTER INSERT, UPDATE
AS 
BEGIN
	IF EXISTS
	(
		SELECT 1
		FROM INSERTED i 
		JOIN Billing.ElectronicSupportDocument esd ON i.Id = esd.Id
		JOIN Billing.BillingAuthorization ba ON ba.Id = i.BillingAuthorizationId
		WHERE ba.InvoiceType <> 4
	)
	BEGIN 
		THROW 51000, 'Error generado por control desde trigger. La autorización seleccionada no es de tipo documento soporte', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el link del código QR.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'QR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la ruta del archivo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'FilePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo del documento de soporte electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina el año del documento realizado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Zip generado por la DIAN cuando estan en proceso de habilitación (pruebas).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ZipKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de envio a la DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el estado actual del registro ante la DIAN      0. Invalida (Los detalles de la factura no concuerdan)      1. Registrada (Registro Creado)      2. Enviada (Factura enviada y recibida)      3. Validada (Factura valida)      4. Validacion Fallida (Factura invalida)      ----------------------------------------------      66. Reenvío Obligatorio (Volver a generar el XML y enviar)      77. Pendiente (Documento de otra versión)      88. Pendiente (En proceso de envío)      99. Pendiente (En proceso de validación)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'StatusElectronic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el numero de intentos de envio del documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Retry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del documento soporte, se genera de la resolución en BillingAuthorization', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla del proceso o modulo desde donde se esta haciendo interfaz', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del proceso o modulo desde donde se esta haciendo interfaz', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del proceso que esta haciendo interfaz para realizar el documento Soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo alfanumerico CUDS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CUDS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 0-Registrado, 1-Confirmado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'VALOR TOTAL INLCUYENDO RETENCIONES', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valore de retenciones (IVA...)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Subtotal (antes de retenciones)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'descripcion del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de Autorizacion de facturacion, para:                    Llevar un numero que corresponda a un sistema de numeración consecutiva de documento soporte                   incluyendo el número, rango, y vigencia autorizada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero, Adquiriente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CustomerThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero; Vendedor', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'SupplierThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA de expiracion del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DueDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA de Radicacion del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'RadicationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE GENERACION DEL DOCUMENTO', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno Documento soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla documento Soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Id';

