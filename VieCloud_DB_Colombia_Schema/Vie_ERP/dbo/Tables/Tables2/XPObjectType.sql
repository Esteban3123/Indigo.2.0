CREATE TABLE [dbo].[XPObjectType] (
    [OID]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TypeName]     NVARCHAR (254) NULL,
    [AssemblyName] NVARCHAR (254) NULL,
    CONSTRAINT [PK_XPObjectType__OID] PRIMARY KEY CLUSTERED ([OID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_XPObjectType__TypeName]
    ON [dbo].[XPObjectType]([TypeName] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del ensamblado .NET, referencia al componente o módulo compilado que contiene la definición del tipo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'AssemblyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre ensamblado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'AssemblyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'AssemblyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del tipo de objeto escrito o completo (fully qualified name), identificador textual del tipo de entidad dentro del sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'TypeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre escrito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'TypeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'TypeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del objeto (OID), clave primaria numérica INT que identifica unívocamente cada tipo de objeto en el registro del sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'OID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificadores de Objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'OID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'OID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro interno del framework de persistencia que cataloga los tipos de objetos del sistema (clases .NET) junto con su ensamblado de origen. Se usa para mapear entidades del modelo de datos a sus definiciones de código en la aplicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType';
