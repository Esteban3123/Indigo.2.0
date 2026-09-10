CREATE TABLE [MixingStation].[Readjustments] (
    [Id]                           INT           IDENTITY (1, 1) NOT NULL,
    [EntityId]                     INT           NOT NULL,
    [EntityName]                   VARCHAR (50)  NOT NULL,
    [RequestPackageDetailStatusId] INT           NOT NULL,
    [BatchCode]                    VARCHAR (50)  NOT NULL,
    [Number]                       INT           CONSTRAINT [DF_Readjustments_Number] DEFAULT ((0)) NOT NULL,
    [SendTo]                       TINYINT       NOT NULL,
    [CreationUser]                 VARCHAR (20)  NOT NULL,
    [CreationDate]                 DATETIME      NOT NULL,
    [ModificationUser]             VARCHAR (20)  NULL,
    [ModificationDate]             DATETIME      NULL,
    [Status]                       TINYINT       CONSTRAINT [DF__Readjustm__Statu__60639E04] DEFAULT ((0)) NOT NULL,
    [TechnicalConceptDate]         DATETIME      NULL,
    [ExpiratedDate]                DATETIME      NULL,
    [Temperature]                  VARCHAR (20)  NULL,
    [TechnicalConcept]             VARCHAR (500) NULL,
    [IsReadjustment]               BIT           CONSTRAINT [DF_Readjustments_IsReadjustment] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Readjustments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Readjustments_RequestPackageDetailStatus] FOREIGN KEY ([RequestPackageDetailStatusId]) REFERENCES [MixingStation].[RequestPackageDetailStatus] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RequestPackageDetailStatusId_BatchCode]
    ON [MixingStation].[Readjustments]([RequestPackageDetailStatusId] ASC, [BatchCode] ASC);


GO
-- =============================================
-- Author:		Juan David Capera
-- Create date: 2022-09-20
-- Description:	Trigger para que genere error cuando se inserta un mismo paquete en readecuaciones dentro en un intervalo de tiempo
-- =============================================
CREATE TRIGGER [MixingStation].[tggControlReadjustments]
	ON [MixingStation].[Readjustments]
	AFTER INSERT
AS 
BEGIN 
	IF EXISTS 
	(
		SELECT 1
		FROM INSERTED i WITH (NOLOCK)
		JOIN
		(
			SELECT MAX(Id) Id, RequestPackageDetailStatusId
			FROM [MixingStation].[Readjustments] WITH (NOLOCK)
			GROUP BY RequestPackageDetailStatusId
		) rc ON rc.RequestPackageDetailStatusId = i.RequestPackageDetailStatusId
		JOIN MixingStation.Readjustments r ON r.Id = rc.Id
		WHERE DATEADD(DAY, 2, r.CreationDate) <= i.CreationDate
	)
	BEGIN
		THROW 51000, 'Error generado por control desde trigger. No se puede agregar el registro porque hace menos de 1 un día se realizó un registro similar', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si el registro constituye una re-adecuación de producto. Verdadero cuando se guarda por concepto técnico en readecuaciones. Dashboard control de calidad. Sinónimos: reajuste, readecuación, ajuste técnico.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'IsReadjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el registro es una re-adecuación, debe estar en verdadero cuando se guardar por concepto técnico de readecuaciones - Dashboard control calidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'IsReadjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'IsReadjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto (VARCHAR 500) que almacena la opinión técnica digitada en el popup de concepto técnico de readecuaciones. Dashboard control de calidad. Almacena evaluación, dictamen, veredicto técnico del producto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que almacena lo digitado en concepto técnico en el popUp concepto técnico de readecuaciones - Dashboard control calidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto (VARCHAR 20) para registrar la temperatura digitada en popup concepto técnico de readecuación. Dashboard control de calidad. Valor crítico en cadena de frío, almacenamiento, validación de integridad del producto farmacéutico.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Temperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almacenar la temperatura digitada en popUp concepto ténico de readecuación - Dashboard control calidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Temperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Temperature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de vencimiento del producto, capturada en popup concepto técnico de readecuaciones. Dashboard control de calidad. Sinónimos: fecha de caducidad, fecha de expiración, vigencia del lote.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ExpiratedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento - guarda la fecha digitada en el popUp concepto técnico de readecuaciones - Dashboard control calidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ExpiratedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ExpiratedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) en que se registra el concepto técnico en popup readecuaciones. Dashboard control de calidad. Marca momento de evaluación técnica, dictamen, validación de producto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConceptDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de concepto técnico, se almacena en el pop concepto técnico de readecuaciones - dashboard contro calidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConceptDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConceptDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (TINYINT) del registro: 0=Pendiente, 1=Readecuada (al guardar concepto técnico), 2=Eliminado (al eliminar en dashboard), 3=Enlazada a solicitud preexistente. Control de calidad. Sinónimos: estatus, condición, clasificación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:  0-Pendiente   1 - Readecuada (Cuando se da en clic en guardar en concept técnico de readecuación - Dashboard calidad)  2 - Eliminado (Cuando se da clic en Eliminar en dashboard calidad o concepto técnico de readeacuciones  3-Enlazada a una Solicitud Pre existente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de última modificación del registro. Auditoría, trazabilidad de cambios en readecuaciones.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificacion del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación del registro. Auditoría, responsable del cambio, usuario de sistema.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificacion del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de creación inicial del registro de readecuación. Auditoría, trazabilidad, fecha de origen.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro. Auditoría, responsable de la creación, autor del registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino (TINYINT): 0=Readecuación, 1=Control de Calidad, 2=Farmacia. Enrutamiento de producto, asignación de área responsable, workflow de proceso.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'SendTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0- Readecuacion  1- Control de Calidad  2- Farmacia', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'SendTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'SendTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador (INT) de registros de readecuaciones que comparten el mismo RequestPackageDetailStatusId. Agrupa readecuaciones por producto de solicitud, cantidad de reajustes del mismo detalle.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de registros de readecuaciones que usan el mismo Id del estado del detalle del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 50) del lote inicial del producto. Identificación de lote farmacéutico, trazabilidad de producto, número de serie del lote.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'BatchCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote Inicial del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'BatchCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'BatchCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK (INT) que referencia RequestPackageDetailStatus. Identifica el producto/ítem de la solicitud. Vinculación a detalles de paquete, solicitud de producto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto de la solicitud', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre (VARCHAR 50) de la tabla origen desde donde proviene la readecuación. Trazabilidad de procedencia, entidad fuente, tabla referenciada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la tabla desde donde viene la readecuacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del registro en la tabla origen. Clave foránea lógica, vinculación a tabla origen, trazabilidad de procedencia.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla desde donde viene la readecuacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de readecuación. Clave primaria, PK, identificador secuencial del ajuste técnico.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de reajustes generados en la estación de mezclas (preparación de medicamentos). Cada registro corresponde a una solicitud de reajuste o corrección sobre un lote de preparación, incluyendo su estado, concepto técnico, temperatura y fechas de vigencia.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments';
