/****** Object:  Table [Admissions].[UsersType]    Script Date: 11/06/2026 3:49:15 p. m. ******/

CREATE TABLE [Admissions].[UsersType](
	[UserTypeCode] [varchar](2) NOT NULL,
	[Name] [nvarchar](80) NOT NULL,
	[ReportCode] [varchar](2) NOT NULL,
	[PatientTypeCode] [tinyint] NOT NULL,
	[AffiliateTypeCode] [tinyint] NULL,
	[EntityTypeId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeCode] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Admissions].[UsersType] ADD  CONSTRAINT [DF_UserType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [Admissions].[UsersType] ADD  CONSTRAINT [DF_UserType_CreatedAt]  DEFAULT ([Common].[GETDATE]()) FOR [CreatedAt]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre descriptivo del tipo de usuario. Alfanumérico, máximo 80 caracteres.' , @level0type=N'SCHEMA',@level0name=N'Admissions', @level1type=N'TABLE',@level1name=N'UsersType', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de reporte SISPRO (tabla tipo de usuario, máximo 2 dígitos). Inmutable una vez guardado.' , @level0type=N'SCHEMA',@level0name=N'Admissions', @level1type=N'TABLE',@level1name=N'UsersType', @level2type=N'COLUMN',@level2name=N'ReportCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del Tipo Paciente del sistema con el que se homologa. Valores: 1=Contributivo, 2=Subsidiado, 3=No afiliado, 4=Particular, 9=Especial/Excepción, 10=PPL FNS, 11=ARL, 12=SOAT, 13=Planes voluntarios, 14=Especial Ley 352/1997.' , @level0type=N'SCHEMA',@level0name=N'Admissions', @level1type=N'TABLE',@level1name=N'UsersType', @level2type=N'COLUMN',@level2name=N'PatientTypeCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de afiliado. Solo aplica cuando PatientTypeCode IN (1, 9). Valores: 1=Cotizante, 2=Beneficiario, 3=Adicional. NULL en los demás casos.' , @level0type=N'SCHEMA',@level0name=N'Admissions', @level1type=N'TABLE',@level1name=N'UsersType', @level2type=N'COLUMN',@level2name=N'AffiliateTypeCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FK al maestro de Tipo de Entidad (módulo Contratos EAPB). Puede ser NULL si no aplica.' , @level0type=N'SCHEMA',@level0name=N'Admissions', @level1type=N'TABLE',@level1name=N'UsersType', @level2type=N'COLUMN',@level2name=N'EntityTypeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del registro. 1 = Activo, 0 = Inactivo.' , @level0type=N'SCHEMA',@level0name=N'Admissions', @level1type=N'TABLE',@level1name=N'UsersType', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Maestro de Tipo de Usuario para homologación SISPRO / RIPS electrónico. Permite parametrizar la codificación de la tabla SISPRO y mapearla al Tipo Paciente del sistema (VIE RCM - Módulo Admisiones).' , @level0type=N'SCHEMA',@level0name=N'Admissions', @level1type=N'TABLE',@level1name=N'UsersType'
GO

