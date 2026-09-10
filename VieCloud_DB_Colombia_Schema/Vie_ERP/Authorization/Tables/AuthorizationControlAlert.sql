
CREATE TABLE [Authorization].[AuthorizationControlAlert](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuthorizationControlId] [int] NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationUser] [varchar](20) NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_AuthorizationControlAlert] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [Authorization].[AuthorizationControlAlert]  WITH CHECK ADD  CONSTRAINT [FK_AuthorizationControlAlert_AuthorizationControl] FOREIGN KEY([AuthorizationControlId])
REFERENCES [Authorization].[AuthorizationControl] ([Id])
GO

ALTER TABLE [Authorization].[AuthorizationControlAlert] CHECK CONSTRAINT [FK_AuthorizationControlAlert_AuthorizationControl]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del registro' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id de la cabecera del trámite' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'AuthorizationControlId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentarios' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'Comments'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del registro: Activo o Suspendido' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario quien creó la alerta' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'CreationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación de la alerta' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'CreationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario quien modificó la alerta' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'ModificationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de modificación de la alerta' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'ModificationDate'
GO


