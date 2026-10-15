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
ALTER TABLE [Billing].[ElectronicSupportDocumentAdjustmentNote] NOCHECK CONSTRAINT [FK_ElectronicSupportDocumentAdjustmentNote_ElectronicSupportDocument];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el estado electrónico para diferenciarlo del estado del documento. 0 - Documento sin procesar, 3 - Documento procesado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'StatusElectronic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el link del código QR.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'QR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ruta donde se guarda el XML', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'FilePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del documento electrónico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año del documento realizado (para control de consecutivos)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo zip generado por la DIAN cuando están en proceso de habilitación (pruebas)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ZipKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de envio a la DIAN', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el numero de intentos de envio del documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Retry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo alfanumerico CUDS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CUDS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza:  1 - Débito  2 - Crédito', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo:  1 - Devolución parcial  2 - Anulación o Reversión del documento  3 - Rebaja o descuento  4 - Ajuste de precio  5 - Otros', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'NoteType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (registrado = 1,confirmado = 2,anulado = 3, Reversado = 4)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'VALOR TOTAL INLCUYENDO RETENCIONES', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valore de retenciones (IVA...)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'TaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Subtotal (antes de retenciones)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la nota de ajuste del documento de soporte electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento del soporte electrónico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'ElectronicSupportDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNote', @level2type = N'COLUMN', @level2name = N'Id';

