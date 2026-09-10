
CREATE PROCEDURE [dbo].[SPINV_CrearTablaAuditoriaSuministro]
(
@TablaCabecera Char(15),
@TablaDetalle Char(15)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sql AS NVARCHAR(MAX)
	SET @sql = 'CREATE TABLE [dbo].[' + @TablaCabecera + '] (
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CODCONCEC] [int] NOT NULL,
	[FECGENLOG] [datetime] NOT NULL,
	[USUARIO] [varchar](20) NOT NULL,
	[MENSAJE] [varchar](2000) NULL,
	[PROESTADO] [bit] NULL,
	[CODCENATE] [char](10) NOT NULL,
	[UFUCODIGO] [char](10) NOT NULL,
	[TIPORDEN] [tinyint] NOT NULL,
 CONSTRAINT [PK_' + @TablaCabecera + '] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [dbo].[' + @TablaCabecera + ']  WITH CHECK ADD  CONSTRAINT [FK_' + @TablaCabecera + '_ADCENATEN] FOREIGN KEY([CODCENATE])
REFERENCES [dbo].[ADCENATEN] ([CODCENATE])

ALTER TABLE [dbo].[' + @TablaCabecera + '] CHECK CONSTRAINT [FK_' + @TablaCabecera + '_ADCENATEN]

ALTER TABLE [dbo].[' + @TablaCabecera + ']  WITH CHECK ADD  CONSTRAINT [FK_' + @TablaCabecera + '_INUNIFUNC] FOREIGN KEY([UFUCODIGO])
REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])

ALTER TABLE [dbo].[' + @TablaCabecera + '] CHECK CONSTRAINT [FK_' + @TablaCabecera + '_INUNIFUNC]

CREATE TABLE [dbo].[' + @TablaDetalle + '](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LOGSUMID] [int] NOT NULL,
	[PROCODCUM] [varchar](50) NULL,
	[PROCODIGO] [char](20)  NULL,
	[PRONOMBRE] [varchar](500)  NULL,
	[CODLOTE] [varchar](50) NULL,
	[INNLOTSER] [varchar](100) NULL,
	[CANTIDAD] [int] NOT NULL,
	[INNMSUMPA] [varchar](100) NULL,
	[NUMSUMINIS] [varchar](50) NULL,
	[INNDOCUME] [varchar](100) NULL,
 CONSTRAINT [PK_' + @TablaDetalle + '] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[' + @TablaDetalle + ']  WITH CHECK ADD  CONSTRAINT [FK_' + @TablaDetalle + '_' + @TablaCabecera + '] FOREIGN KEY([LOGSUMID])
REFERENCES [dbo].[' + @TablaCabecera + '] ([ID])

ALTER TABLE [dbo].[' + @TablaDetalle + '] CHECK CONSTRAINT [FK_' + @TablaDetalle + '_' + @TablaCabecera + ']

'
EXEC sp_executesql @sql
	
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Crea dinámicamente un par de tablas de auditoría para registrar el log de suministros de medicamentos e insumos. La tabla de cabecera (nombre recibido por parámetro) almacena el encabezado del log: concepto de suministro, fecha y hora del registro, usuario responsable, mensaje de resultado, estado del proceso, centro de atención y unidad funcional. La tabla de detalle (también por parámetro) guarda el desglose por ítem suministrado: código CUM del producto, código interno, nombre del suministro, lote, número de suministro, cantidad y documento de referencia. Ambas tablas quedan vinculadas entre sí y con las tablas maestras de centros de atención (ADCENATEN) y unidades funcionales (INUNIFUNC), permitiendo trazabilidad completa del proceso de dispensación o entrega de suministros por período o corte de auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPINV_CrearTablaAuditoriaSuministro';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPINV_CrearTablaAuditoriaSuministro';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Crea dinámicamente un par de tablas (cabecera y detalle) para registrar logs/auditoría de movimientos de suministro, con sus llaves primarias y relaciones a maestros de centro de atención y unidad funcional."', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPINV_CrearTablaAuditoriaSuministro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los nombres recibidos para cabecera y detalle no deben corresponder a tablas ya existentes en dbo.; Deben existir las tablas referenciadas dbo.ADCENATEN y dbo.INUNIFUNC para que las FKs sean válidas.; El ejecutor debe contar con permisos DDL (CREATE TABLE, ALTER TABLE) en el esquema dbo.; Los nombres provistos deben ser identificadores válidos de SQL Server (riesgo de inyección al concatenarse en SQL dinámico).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPINV_CrearTablaAuditoriaSuministro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla de cabecera siempre se crea con PK clustered sobre ID IDENTITY y FKs hacia ADCENATEN (CODCENATE) e INUNIFUNC (UFUCODIGO).; La tabla de detalle siempre se crea con PK clustered sobre ID IDENTITY y FK LOGSUMID referenciando el ID de la cabecera, garantizando integridad cabecera-detalle.; Las FK se crean con WITH CHECK y se habilitan explícitamente (CHECK CONSTRAINT).; Cabecera incluye campos obligatorios de trazabilidad: CODCONCEC, FECGENLOG, USUARIO, CODCENATE, UFUCODIGO y TIPORDEN NOT NULL.; Detalle exige CANTIDAD y LOGSUMID NOT NULL; el resto de atributos del producto/lote/documento son opcionales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPINV_CrearTablaAuditoriaSuministro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Auditoría de suministros; Centro de atención; Unidad funcional; Lote; Producto/CUM; Orden de suministro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPINV_CrearTablaAuditoriaSuministro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.<TablaCabecera dinámica>: Ejecuta SQL dinámico vía sp_executesql que crea la tabla de cabecera con PK sobre ID y FKs a ADCENATEN y INUNIFUNC.; [RETURN_RESULT] dbo.<TablaDetalle dinámica>: Ejecuta SQL dinámico que crea la tabla de detalle con PK sobre ID y FK LOGSUMID hacia la tabla de cabecera recién creada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPINV_CrearTablaAuditoriaSuministro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPINV_CrearTablaAuditoriaSuministro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPINV_CrearTablaAuditoriaSuministro';
-- GO
