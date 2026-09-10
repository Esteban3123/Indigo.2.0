CREATE TABLE [Billing].[ElectronicSupportDocumentAdjustmentNote] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20)    NOT NULL,
    [DocumentDate]                DATETIME        NOT NULL,
    [ElectronicSupportDocumentId] INT             NOT NULL,
    [Description]                 VARCHAR (500)   NOT NULL,
    [SubTotalValue]               DECIMAL (18, 2) NOT NULL,
    [TaxValue]                    DECIMAL (18, 2) NOT NULL,
    [TotalValue]                  DECIMAL (18, 2) NOT NULL,
    [OperativeUnitId]             INT             CONSTRAINT [DF_ElectronicSupportDocumentAdjustmentNote_OperativeUnitId] DEFAULT ((1)) NOT NULL,
    [Status]                      TINYINT         CONSTRAINT [DF_ElectronicSupportDocumentAdjustmentNote_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                VARCHAR (20)    NOT NULL,
    [CreationDate]                DATETIME        NOT NULL,
    [ModificationUser]            VARCHAR (20)    NULL,
    [ModificationDate]            DATETIME        NULL,
    [ConfirmationUser]            VARCHAR (20)    NULL,
    [ConfirmationDate]            DATETIME        NULL,
    [AnnulmentUser]               VARCHAR (20)    NULL,
    [AnnulmentDate]               DATETIME        NULL,
    [TimeStamp]                   ROWVERSION      NOT NULL,
    [NoteType]                    TINYINT         NOT NULL,
    [Nature]                      TINYINT         NOT NULL,
    [CUDS]                        VARCHAR (500)   NULL,
    [Retry]                       INT             CONSTRAINT [DF_ElectronicSupportDocumentAdjustmentNote_Retry] DEFAULT ((0)) NOT NULL,
    [ShippingDate]                DATETIME        NULL,
    [ZipKey]                      VARCHAR (500)   NULL,
    [Year]                        INT             CONSTRAINT [DF_ElectronicSupportDocumentAdjustmentNote_Year] DEFAULT ((2022)) NOT NULL,
    [Consecutive]                 INT             CONSTRAINT [DF_ElectronicSupportDocumentAdjustmentNote_Consecutive] DEFAULT ((0)) NULL,
    [FilePath]                    VARCHAR (250)   NULL,
    [QR]                          VARCHAR (500)   NULL,
    [EntityId]                    INT             NULL,
    [EntityName]                  VARCHAR (50)    NULL,
    [StatusElectronic]            TINYINT         CONSTRAINT [DF__Electroni__Statu__0BF8077F] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ElectronicSupportDocumentAdjustmentNote] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicSupportDocumentAdjustmentNote_ElectronicSupportDocument] FOREIGN KEY ([ElectronicSupportDocumentId]) REFERENCES [Billing].[ElectronicSupportDocument] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ElectronicSupportDocumentAdjustmentNote_Code]
    ON [Billing].[ElectronicSupportDocumentAdjustmentNote]([Code] ASC) WHERE ([Id]>(15));


GO
-- =============================================
-- Author:		Juan David Capera Núñez
-- Create date: 2022-08-01
-- Description:	Trigger para que genere automáticamente el consecutivo de la nota de ajuste a documento soporte electrónico
-- =============================================
CREATE TRIGGER [Billing].[tggGenerateConsecutiveElectronicAdjustmentNote]
ON [Billing].[ElectronicSupportDocumentAdjustmentNote]
 AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON

	DECLARE @Consecutive INT = 0
	
	--Se obtiene el último documento por año
	SELECT @Consecutive = MAX(esdn.Consecutive)
	FROM INSERTED i
	JOIN Billing.ElectronicSupportDocumentAdjustmentNote esdn ON esdn.Year = i.Year

	/* Una vez obtenido, procedo a actualizarlo en el registro nuevo, en caso de que no se encuentre el registro, 
	   significa que el año ha cambiado, por consiguiente el consecutivo vuelva a empezar en 1 */
	UPDATE esdn
		SET esdn.Consecutive = ISNULL(@Consecutive, 0) + 1
	FROM INSERTED i
	JOIN Billing.ElectronicSupportDocumentAdjustmentNote esdn ON esdn.Id = i.Id
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado electrónico de la nota de ajuste (TINYINT): diferencia del estado administrativo del documento. 1=Sin procesar, 3=Procesado por DIAN. Indica fase de validación electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'StatusElectronic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado electrónico para diferenciarlo del estado del documento. 0 - Documento sin procesar, 3 - Documento procesado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'StatusElectronic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'StatusElectronic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad emisora o receptora de la nota de ajuste (VARCHAR 50). Empresa, IPS, centro de atención involucrado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la entidad (INT). Referencia al tercero, proveedor o prestador que emite/recibe la nota de ajuste.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código QR/URL generado por DIAN para validación y trazabilidad electrónica del documento ajuste (VARCHAR 500). Vinculado a RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'QR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el link del código QR.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'QR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'QR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta de almacenamiento del archivo XML de la nota de ajuste en el servidor (VARCHAR 250). Trazabilidad de archivos electrónicos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'FilePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ruta donde se guarda el XML', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'FilePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'FilePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número correlativo de la nota de ajuste dentro del año y unidad funcional (INT). Control secuencial DIAN para facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del documento electrónico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año de expedición de la nota de ajuste (INT, default 2022). Necesario para control de consecutivos por período fiscal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año del documento realizado (para control de consecutivos)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código ZIP de habilitación generado por DIAN durante fase de pruebas (VARCHAR 500). Solo en ambiente de prueba/validación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ZipKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo zip generado por la DIAN cuando están en proceso de habilitación (pruebas)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ZipKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ZipKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de envío de la nota de ajuste a DIAN (DATETIME). Marca de transmisión de documento electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envio a la DIAN', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de reintentos de envío a DIAN (INT, default 0). Contador de fallos en transmisión electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el numero de intentos de envio del documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Retry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de soporte alfanumérico CUDS emitido por DIAN (VARCHAR 500). Identificación definitiva del documento electrónico en RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CUDS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo alfanumerico CUDS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CUDS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CUDS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza de la nota de ajuste (TINYINT): 1=Débito/Aumento, 2=Crédito/Disminución. Indica si aumenta o reduce el valor facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza:  1 - Débito  2 - Crédito', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de nota de ajuste (TINYINT): 1=Devolución parcial, 2=Anulación/Reversión, 3=Rebaja/Descuento, 4=Ajuste de precio, 5=Otros. Clasificación motivo ajuste.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'NoteType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo:  1 - Devolución parcial  2 - Anulación o Reversión del documento  3 - Rebaja o descuento  4 - Ajuste de precio  5 - Otros', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'NoteType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'NoteType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal del evento de creación, modificación o registro (TIMESTAMP). Instante exacto de generación de registro en BD.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de anulación de la nota de ajuste (DATETIME NULL). Cuando se revierte o cancela el documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/identificación que anuló el documento (VARCHAR 20). Trazabilidad de quién ejecutó la reversión.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación/aprobación de la nota de ajuste (DATETIME NULL). Validación por autoridad competente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/identificación que confirmó el documento (VARCHAR 20). Responsable de validación del ajuste.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro (DATETIME NULL). Control de cambios post-creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/identificación que modificó el documento (VARCHAR 20). Trazabilidad de ediciones.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación de la nota de ajuste (DATETIME). Instante de origen del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/identificación que creó la nota de ajuste (VARCHAR 20). Responsable de generación inicial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado administrativo de la nota de ajuste (TINYINT, default 1): 1=Registrado, 2=Confirmado, 3=Anulado, 4=Reversado. Ciclo de vida del documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (registrado = 1,confirmado = 2,anulado = 3, Reversado = 4)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa/funcional que genera la nota (INT, default 1). Centro de atención, sede o departamento facturador.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de la nota de ajuste incluidas retenciones y tributos (DECIMAL 18,2). Monto final facturado/ajustado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'VALOR TOTAL INLCUYENDO RETENCIONES', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de retenciones, IVA y otros tributos aplicados (DECIMAL 18,2). Componente fiscal de la nota de ajuste.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valore de retenciones (IVA...)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal antes de retenciones e impuestos (DECIMAL 18,2). Base para cálculo de tributos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Subtotal (antes de retenciones)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del motivo y alcance de la nota de ajuste (VARCHAR 500). Justificación clínica/administrativa del ajuste facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la nota de ajuste del documento de soporte electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la factura/documento de soporte original (INT, FK). Referencia a documento que se ajusta/anula.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ElectronicSupportDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento del soporte electrónico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ElectronicSupportDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ElectronicSupportDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de expedición de la nota de ajuste (DATETIME). Fecha de emisión del documento electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la nota de ajuste (VARCHAR 20). Número identificador de control interno.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial del registro (INT IDENTITY). Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas de ajuste (notas débito y crédito) asociadas a documentos soporte electrónicos de facturación. Registra los ajustes, correcciones o anulaciones sobre documentos soporte electrónicos emitidos, incluyendo valores, estado de envío a la DIAN y trazabilidad de auditoría.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote';
