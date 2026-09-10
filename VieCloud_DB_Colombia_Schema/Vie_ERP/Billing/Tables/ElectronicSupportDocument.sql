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



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [ix_EntityId_EntityName]
    ON [Billing].[ElectronicSupportDocument]([EntityId] ASC, [EntityName] ASC) WHERE ([EntityId] IS NOT NULL AND [EntityName] IS NOT NULL);




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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código QR en formato VARCHAR(500), enlace o representación visual del documento electrónico para consulta y validación ante terceros.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'QR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el link del código QR.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'QR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'QR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta de almacenamiento del archivo XML/PDF del documento soporte en el sistema de archivos, VARCHAR(250).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'FilePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la ruta del archivo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'FilePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'FilePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo secuencial del documento soporte electrónico dentro del año fiscal, INT DEFAULT 0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del documento de soporte electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año de emisión del documento soporte, utilizado para organización fiscal y cumplimiento DIAN, INT DEFAULT 2022.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el año del documento realizado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave Zip generada por la DIAN durante fase de habilitación/pruebas, VARCHAR(500), permite validación en ambiente de prueba.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ZipKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Zip generado por la DIAN cuando estan en proceso de habilitación (pruebas).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ZipKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ZipKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de envío del documento electrónico a DIAN para validación y registro, DATETIME DEFAULT ''''2022-08-01''''.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envio a la DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de validación ante DIAN (0=Inválida, 1=Registrada, 2=Enviada, 3=Validada, 4=Validación Fallida, 66=Reenvío Obligatorio, 77/88/99=Pendiente), TINYINT.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'StatusElectronic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el estado actual del registro ante la DIAN      0. Invalida (Los detalles de la factura no concuerdan)      1. Registrada (Registro Creado)      2. Enviada (Factura enviada y recibida)      3. Validada (Factura valida)      4. Validacion Fallida (Factura invalida)      ----------------------------------------------      66. Reenvío Obligatorio (Volver a generar el XML y enviar)      77. Pendiente (Documento de otra versión)      88. Pendiente (En proceso de envío)      99. Pendiente (En proceso de validación)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'StatusElectronic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'StatusElectronic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador de intentos de reenvío del documento electrónico a DIAN, INT DEFAULT 0, para rastrear fallos de transmisión.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el numero de intentos de envio del documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Retry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad operativa, centro de atención o sede que genera el documento, INT FK OperativeUnit, DEFAULT 1.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del documento soporte asignado desde la resolución de BillingAuthorization, VARCHAR(50), para trazabilidad fiscal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del documento soporte, se genera de la resolución en BillingAuthorization', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo (timestamp) que registra el instante exacto de creación, modificación o cambio de estado del documento, TIMESTAMP.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro del documento soporte, DATETIME NULL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de sesión que realizó la última modificación del documento, VARCHAR(20) NULL, para auditoría.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del documento soporte electrónico, DATETIME NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de sesión que creó el registro del documento soporte, VARCHAR(20) NOT NULL, para auditoría.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del módulo, proceso o entidad origen que genera la interfaz con el documento soporte (factura, reclamación, etc.), INT NULL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla del proceso o modulo desde donde se esta haciendo interfaz', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del proceso o módulo origen que realiza la interfaz (ej: Facturación, Glosas, RIPS), VARCHAR(250) NULL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del proceso o modulo desde donde se esta haciendo interfaz', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del proceso o módulo que realiza interfaz para generar el documento soporte, VARCHAR(20) NULL, identificación funcional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del proceso que esta haciendo interfaz para realizar el documento Soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico CUDS (Código Único de Documento Soporte) asignado por DIAN, VARCHAR(500) NULL, para identificación tributaria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CUDS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo alfanumerico CUDS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CUDS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CUDS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado administrativo del documento (0=Registrado, 1=Confirmado), BIT NOT NULL, controla ciclo de vida interno.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 0-Registrado, 1-Confirmado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del documento incluyendo retenciones y descuentos, DECIMAL(18,2), en moneda local (COP).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'VALOR TOTAL INLCUYENDO RETENCIONES', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de retenciones, impuestos y descuentos aplicados (IVA, ICA, retención fuente, etc.), DECIMAL(18,2).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valore de retenciones (IVA...)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'TaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal del documento antes de aplicar retenciones, impuestos o descuentos, DECIMAL(18,2).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Subtotal (antes de retenciones)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o concepto del documento soporte electrónico (factura, nota crédito, reclamación), VARCHAR(500).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'descripcion del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de autorización de facturación (FK), vincula con rango de numeración, vigencia y resolución DIAN autorizada, INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de Autorizacion de facturacion, para:                    Llevar un numero que corresponda a un sistema de numeración consecutiva de documento soporte                   incluyendo el número, rango, y vigencia autorizada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero adquiriente/comprador (FK ThirdParty), quien recibe o es facturado por el servicio, INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CustomerThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero, Adquiriente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CustomerThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'CustomerThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero proveedor/vendedor (FK ThirdParty), quien emite y factura el documento de soporte, INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'SupplierThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero; Vendedor', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'SupplierThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'SupplierThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento o expiración del documento soporte, plazo de pago establecido, DATETIME NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DueDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA de expiracion del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DueDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DueDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación o presentación oficial del documento soporte, DATETIME NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'RadicationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA de Radicacion del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'RadicationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'RadicationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de generación o emisión del documento soporte electrónico, DATETIME NOT NULL, origen de la transacción.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA DE GENERACION DEL DOCUMENTO', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno único del documento soporte dentro del sistema, VARCHAR(20) NOT NULL, identificador legible.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno Documento soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (primary key) del registro de documento soporte electrónico en tabla, INT IDENTITY(1,1) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla documento Soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documentos soporte electrónicos de facturación generados entre un proveedor y un cliente, incluyendo valores, impuestos, fechas de radicación y vencimiento, estado de transmisión electrónica y datos del archivo digital (CUDS, QR, ruta del archivo ZIP). Equivale al documento soporte de pago a no obligados a facturar o documentos equivalentes en facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocument';
