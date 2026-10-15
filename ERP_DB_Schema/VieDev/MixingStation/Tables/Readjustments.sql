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
ALTER TABLE [MixingStation].[Readjustments] NOCHECK CONSTRAINT [FK_Readjustments_RequestPackageDetailStatus];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro es una re-adecuación, debe estar en verdadero cuando se guardar por concepto técnico de readecuaciones - Dashboard control calidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'IsReadjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que almacena lo digitado en concepto técnico en el popUp concepto técnico de readecuaciones - Dashboard control calidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para almacenar la temperatura digitada en popUp concepto ténico de readecuación - Dashboard control calidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Temperature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimiento - guarda la fecha digitada en el popUp concepto técnico de readecuaciones - Dashboard control calidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ExpiratedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de concepto técnico, se almacena en el pop concepto técnico de readecuaciones - dashboard contro calidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConceptDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  0-Pendiente   1 - Readecuada (Cuando se da en clic en guardar en concept técnico de readecuación - Dashboard calidad)  2 - Eliminado (Cuando se da clic en Eliminar en dashboard calidad o concepto técnico de readeacuciones  3-Enlazada a una Solicitud Pre existente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificacion del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificacion del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0- Readecuacion  1- Control de Calidad  2- Farmacia', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'SendTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de registros de readecuaciones que usan el mismo Id del estado del detalle del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote Inicial del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'BatchCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto de la solicitud', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la tabla desde donde viene la readecuacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla desde donde viene la readecuacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Readjustments', @level2type = N'COLUMN', @level2name = N'Id';

