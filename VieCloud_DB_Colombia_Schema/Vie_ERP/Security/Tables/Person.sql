CREATE TABLE [Security].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Identification] [varchar](15) NOT NULL,
	[IdentificationType] [smallint] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[SecondName] [varchar](50) NULL,
	[FirstLastName] [varchar](50) NOT NULL,
	[SecondLastName] [varchar](50) NULL,
	[Fullname] [varchar](250) NOT NULL,
	[BirthDay] [datetime] NULL,
	[Fingerprint] [varbinary](max) NULL,
	[Gender] [smallint] NOT NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_Person_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Person] UNIQUE NONCLUSTERED 
(
	[Identification] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de personas del sistema de seguridad. Guarda los datos de identidad, nombre completo, fecha de nacimiento, huella dactilar y género de cada usuario o persona registrada en el sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único de la persona en el sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de documento o cédula de la persona (identificación, documento de identidad).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identidad (cédula de ciudadanía, pasaporte, tarjeta de identidad, etc.).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre de la persona.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre de la persona (puede estar vacío).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido de la persona.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido de la persona (puede estar vacío).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la persona (nombres y apellidos concatenados).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Fullname';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Fullname';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento de la persona.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BirthDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BirthDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Huella dactilar de la persona almacenada en formato binario, usada para biometría o autenticación.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Fingerprint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Fingerprint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género o sexo de la persona (masculino, femenino u otro, codificado numéricamente).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo de la persona en el sistema (activo = 1, inactivo = 0).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'State';
