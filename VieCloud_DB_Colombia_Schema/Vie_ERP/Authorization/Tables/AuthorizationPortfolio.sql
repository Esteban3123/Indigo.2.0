CREATE TABLE [Authorization].[AuthorizationPortfolio] (
	[Id]               INT           IDENTITY (1,1) NOT NULL,
	[OperatingUnitId]  INT           NOT NULL,
	[Code]             VARCHAR (20)  NOT NULL,
	[Name]             VARCHAR (100) NOT NULL,
	[Status]           BIT           NOT NULL,
	[CreationUser]     VARCHAR (20)  NOT NULL,
	[CreationDate]     DATETIME      NOT NULL,
	[ModificationUser] VARCHAR (20)  NULL,
	[ModificationDate] DATETIME      NULL,
	[TimeStamp]        TIMESTAMP     NOT NULL,
	[TypePortfolio]    [TINYINT]       NULL,
 CONSTRAINT [PK_AuthorizationPortfolio] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Authorization].[AuthorizationPortfolio] ADD  DEFAULT ((1)) FOR [TypePortfolio]
GO

ALTER TABLE [Authorization].[AuthorizationPortfolio]  WITH CHECK ADD  CONSTRAINT [FK_AuthorizationPortfolio_OperatingUnit] FOREIGN KEY([OperatingUnitId])
REFERENCES [Common].[OperatingUnit] ([Id])
GO

ALTER TABLE [Authorization].[AuthorizationPortfolio] CHECK CONSTRAINT [FK_AuthorizationPortfolio_OperatingUnit]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del Registro' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationPortfolio', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la Unidad Operativa' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationPortfolio', @level2type=N'COLUMN',@level2name=N'OperatingUnitId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del Portafolio' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationPortfolio', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del Portafolio' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationPortfolio', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del Registro (Falso = Inactivo, True = Activo)' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationPortfolio', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario Creación' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationPortfolio', @level2type=N'COLUMN',@level2name=N'CreationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de Creación' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationPortfolio', @level2type=N'COLUMN',@level2name=N'CreationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario Modificación' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationPortfolio', @level2type=N'COLUMN',@level2name=N'ModificationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha Modificación' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationPortfolio', @level2type=N'COLUMN',@level2name=N'ModificationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationPortfolio', @level2type=N'COLUMN',@level2name=N'TimeStamp'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de Portafolio 1.Ambulatorio 2.Hospitalario' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationPortfolio', @level2type=N'COLUMN',@level2name=N'TypePortfolio'
