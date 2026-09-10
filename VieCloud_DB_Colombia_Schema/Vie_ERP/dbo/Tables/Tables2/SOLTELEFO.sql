CREATE TABLE [dbo].[SOLTELEFO] (
    [Autonumerico]   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Telefono]       VARCHAR (30) NOT NULL,
    [TipoTelefono]   VARCHAR (20) NOT NULL,
    [Codigo]         NUMERIC (18) NOT NULL,
    [TelefPrincipal] BIT          NOT NULL,
    CONSTRAINT [PK_SOLTELEF] PRIMARY KEY CLUSTERED ([Autonumerico] ASC),
    CONSTRAINT [FK_TELEFONOS_PROVEEDORES] FOREIGN KEY ([Codigo]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que determina si el teléfono es el número principal o de contacto preferente del proveedor. Valores: 1=Sí es principal, 0=No es principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'TelefPrincipal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el si el telefono es principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'TelefPrincipal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'TelefPrincipal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de referencia del proveedor (NUMERIC 18). Clave foránea que vincula con SOLPROVEE.PROVAUTO para identificar a qué proveedor pertenece el teléfono.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de teléfono (VARCHAR 20): celular, fijo, corporativo, etc. Define la categoría o modalidad de contacto del proveedor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'TipoTelefono';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el tipo de telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'TipoTelefono';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'TipoTelefono';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico del proveedor (VARCHAR 30). Campo requerido que almacena el dígito de contacto en formato variable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Telefono';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Telefono';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Telefono';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de cada registro de teléfono en la tabla SOLTELEFO. Clave primaria que no se replica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Autonumerico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Autonumerico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Autonumerico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Números de teléfono de contacto asociados a solicitudes o registros del sistema, indicando el tipo de teléfono y si es el principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO';
