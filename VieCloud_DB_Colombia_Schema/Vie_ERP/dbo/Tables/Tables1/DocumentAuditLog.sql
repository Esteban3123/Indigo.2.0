CREATE TABLE [dbo].[DocumentAuditLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Action] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[IDHCMOANULB] [char](4) NOT NULL,
	[Observation] [varchar](300) NOT NULL,
	[UserAction] [char](20) NOT NULL,
	[Json] [varchar](2000) NOT NULL,
 CONSTRAINT [PK_DocumentAuditLog] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consecutivo de la tabla ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentAuditLog', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 - Sustitución del documento
  2 - Anulación / Eliminación del documento ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentAuditLog', @level2type=N'COLUMN',@level2name=N'Action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha cuando se ejecuto la accion  de    1 - Sustitución del documento ó   2 - Anulación / Eliminación del documento ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentAuditLog', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Relacion con la tabla:  HCMOANULB  que lista los motivos y causas generales ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentAuditLog', @level2type=N'COLUMN',@level2name=N'IDHCMOANULB'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Observacion que el usuario diligencio cuando hizo la acción de  1 - Sustitución del documento ó   2 - Anulación / Eliminación del documento ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentAuditLog', @level2type=N'COLUMN',@level2name=N'Observation'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'usuario que ejecuta la accion de 1 - Sustitución del documento ó   2 - Anulación / Eliminación del documento ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentAuditLog', @level2type=N'COLUMN',@level2name=N'UserAction'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Este campo lo que va almacenar es toda la información del registro seleccionado, es decir capturamos toda la fila de la rejilla y guardamos en un solo campo toda esa información, esto para manejar autoria.
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentAuditLog', @level2type=N'COLUMN',@level2name=N'Json'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla destinada a almacenar la auditoría de la gestión de documentos, registrando las acciones de sustitución y eliminación realizadas sobre cada documento.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentAuditLog'
GO
