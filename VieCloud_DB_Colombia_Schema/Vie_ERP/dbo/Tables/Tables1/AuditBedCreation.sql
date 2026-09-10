CREATE TABLE [dbo].[AuditBedCreation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OriginId] [int] NOT NULL,
	[Origin] [varchar](50) NOT NULL,
	[UserCreate] [char](20) NOT NULL,
	[RegisteredAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AuditBedCreation]  WITH CHECK ADD  CONSTRAINT [FK_AuditBedCreation_UserCreate] FOREIGN KEY([UserCreate])
REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
GO

ALTER TABLE [dbo].[AuditBedCreation] CHECK CONSTRAINT [FK_AuditBedCreation_UserCreate]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador único autoincremental de la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuditBedCreation', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID del registro  del activo creado (cama, consultorio, sala)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuditBedCreation', @level2type=N'COLUMN',@level2name=N'OriginId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la tabla de origen del activo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuditBedCreation', @level2type=N'COLUMN',@level2name=N'Origin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del profesional que creó el activo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuditBedCreation', @level2type=N'COLUMN',@level2name=N'UserCreate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora en que se realizó la creación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuditBedCreation', @level2type=N'COLUMN',@level2name=N'RegisteredAt'
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de auditoría que registra la creación de activos hospitalarios como camas, consultorios y salas. Almacena el identificador y nombre de la tabla de origen del activo creado, el usuario responsable de la creación (referenciado en la tabla de seguridad `SEGusuaru`) y la fecha y hora exacta del evento, permitiendo trazabilidad de altas de recursos físicos en el sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuditBedCreation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuditBedCreation';
GO
