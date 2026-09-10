CREATE TABLE [Billing].[ElectronicDocument] (
    [Id]              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId] INT             NOT NULL,
    [CustomerPartyId] INT             NOT NULL,
    [EntityId]        INT             NOT NULL,
    [EntityName]      VARCHAR (250)   NOT NULL,
    [DocumentDate]    DATETIME        NOT NULL,
    [DocumentType]    TINYINT         NOT NULL,
    [Status]          TINYINT         NOT NULL,
    [CreationDate]    DATETIME        NOT NULL,
    [ShippingDate]    DATETIME        NULL,
    [Container]       VARCHAR (50)    NOT NULL,
    [FilePath]        VARCHAR (250)   NOT NULL,
    [Prefix]          VARCHAR (5)     NULL,
    [DocumentNumber]  VARCHAR (20)    NOT NULL,
    [CUFE]            VARCHAR (500)   NULL,
    [DianVersion]     DECIMAL (18, 2) CONSTRAINT [DF_ElectronicDocument_DianVersion] DEFAULT ((1)) NOT NULL,
    [ValidationDate]  DATETIME        NULL,
    [Retry]           INT             CONSTRAINT [DF_ElectronicDocument_Retry] DEFAULT ((0)) NOT NULL,
    [ZipKey]          VARCHAR (500)   NULL,
    [Year]            INT             CONSTRAINT [DF_ElectronicDocument_Year] DEFAULT ((2019)) NOT NULL,
    [Consecutive]     INT             CONSTRAINT [DF_ElectronicDocument_Consecutive] DEFAULT ((0)) NOT NULL,
    [HttpContent]     VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ElectronicDocument] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicDocument_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_ElectronicDocument_ThirdParty] FOREIGN KEY ([CustomerPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_InvoiceId]
    ON [Billing].[ElectronicDocument]([EntityId] ASC, [EntityName] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_ElectronicDocument_Year]
    ON [Billing].[ElectronicDocument]([Year] ASC)
    INCLUDE([DocumentType], [Consecutive]);


GO
CREATE NONCLUSTERED INDEX [IX_ElectronicDocument_Status]
    ON [Billing].[ElectronicDocument]([Status] ASC);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-11-27
-- Description:	Trigger para que genere automáticamente el consecutivo del documento electrónico
-- =============================================
CREATE TRIGGER [Billing].[tggElectronicDocumentGenerateConsecutive]
   ON  [Billing].[ElectronicDocument]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON

    DECLARE @Consecutive INT = 0

	SELECT @Consecutive = MAX(ed.Consecutive)
	FROM INSERTED i
	JOIN Billing.ElectronicDocument ed ON i.Year = ed.Year
		AND [Billing].[GetElectronicDocumentDocumentType](i.DocumentType) = [Billing].[GetElectronicDocumentDocumentType](ed.DocumentType)

	UPDATE ed
		SET ed.Consecutive = ISNULL(@Consecutive, 0) + 1
	FROM INSERTED i
	JOIN Billing.ElectronicDocument ed ON i.Id = ed.Id
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido XML estructurado en formato UBL 2.0 o versión DIAN requerida, serializado como texto para auditoría y reenvío (VARCHAR MAX).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'HttpContent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contenido estructurado en un formato especifico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'HttpContent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'HttpContent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo secuencial del documento dentro del año de vigencia, controla numeración por resolución (INT, default 0).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del documento electrónico en la vigencia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año de vigencia contable y fiscal del documento electrónico, para segmentación anual (INT, default 2019).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año de la vigencia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/token generado tras superar validaciones iniciales, asigna documento a cola de procesamiento DIAN (VARCHAR 500).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'ZipKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al número generado una vez concluida exitosamente las validaciones iniciales y los documentos pasan a la cola de validación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'ZipKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'ZipKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de reintentos de envío a DIAN (INT, default 0), incrementa si fallan transmisiones o validaciones iniciales.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de intentos de envío', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Retry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de validación ante DIAN, transición de estado Enviada a Válida o Inválida. Nulo si pendiente (DATETIME NULL).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'ValidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de validación ante la DIAN (Fecha en que cambia del estado Enviado a Válido o Inválido)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'ValidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'ValidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del estándar DIAN de Facturación Electrónica (DECIMAL, default 1.0), determina estructura XML y validaciones.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DianVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión Dian de Facturación Electrónica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DianVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DianVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Único de Factura Electrónica (DIAN): CUFE para factura, CUDE para nota crédito/débito/contingencia. Generado por DIAN tras validación (VARCHAR 500).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'CUFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Unico del Documento Electronico, si es una factura es el CUFE, si es una factura de contingencia, nota u otro documento es el CUDE', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'CUFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'CUFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del documento sin prefijo, según resolución de facturación electrónica (VARCHAR 20).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del documento sin Prefijo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo de resolución DIAN usado en numeración de factura electrónica (ej: ''''FAC'''', ''''NC'''', máx 5 caracteres, nullable).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prefijo usado en la resolución de Facturación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta o path del archivo físico XML del documento electrónico en el contenedor de almacenamiento (VARCHAR 250).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'FilePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ruta donde se encuentra el archivo físico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'FilePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'FilePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenedor de almacenamiento (blob, archivo físico o base de datos) donde reside el documento XML de factura electrónica para envío DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Container';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contenedor de la Base de datos, el cual nos servirá para almacenar los documentos electronicos y realizar el envío de la factura electrónica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Container';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Container';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de envío a DIAN, transición del estado Registrada a Enviada. Nulo si aún no se envía (DATETIME NULL).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envío a la DIAN (Fecha en que cambia del estado Registrado a Enviado)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro electrónico en la base de datos (DATETIME).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado ante DIAN (TINYINT): 0=Inválida, 1=Registrada, 2=Enviada, 3=Validada, 4=Validación Fallida, 66=Reenvío Obligatorio, 77/88/99=Pendiente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el estado actual del registro ante la DIAN      0. Invalida (Los detalles de la factura no concuerdan)      1. Registrada (Registro Creado)      2. Enviada (Factura enviada y recibida)      3. Validada (Factura valida)      4. Validacion Fallida (Factura invalida)      ----------------------------------------------      66. Reenvío Obligatorio (Volver a generar el XML y enviar)      77. Pendiente (Documento de otra versión)      88. Pendiente (En proceso de envío)      99. Pendiente (En proceso de validación)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento RIPS/DIAN: factura (EAPB con/sin contrato, particular, capitada, básica, productos), nota crédito, nota débito, control de capitación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos de Documentos      
1. Factura EAPB con Contrato      
2. Factura EAPB Sin Contrato      
3. Factura Particular      
4. Factura Capitada      
5. Control de Capitacion      
6. Factura Basica      
7. Factura de Venta de Productos      
--------------------------------      
91. Nota Credito Validacion Previa
92. Nota Debito Validacion Previa
93. Nota Credito Validacion Previa - Facturas version anterior
94. Nota Debito Validacion Previa - Facturas version anterior
98. Nota Debito
99. Nota Credito', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha efectiva del documento: factura, nota crédito, nota débito, según resolución de facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad operativa, unidad funcional o centro de atención desde el cual se originó el documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad desde la cual se genero el registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del documento origen (atención, ingreso, urgencia, procedimiento) que genera este registro electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento que genero el registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK → Common.ThirdParty. Tercero, cliente, asegurador o entidad receptora del documento electrónico de factura, nota crédito o débito.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'CustomerPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercero del cliente de la transacción', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'CustomerPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'CustomerPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK → Common.OperatingUnit. Unidad operativa, centro de atención o sucursal desde donde se origina el documento de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Operativa en la cual se realiza el documento de origen', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del documento electrónico en el sistema de facturación Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento electronico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de documentos electrónicos de facturación emitidos ante la DIAN (facturas, notas crédito, notas débito). Guarda el estado de envío, validación, ruta del archivo XML/PDF, CUFE y datos de respuesta del servicio de facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument';

GO
CREATE NONCLUSTERED INDEX [IX_ElectronicDocument_Entity_ValidationDate]
    ON [Billing].[ElectronicDocument]([EntityName] ASC, [EntityId] ASC, [ValidationDate] DESC)
    INCLUDE([Status]);
