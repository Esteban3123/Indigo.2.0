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
ALTER TABLE [dbo].[SOLTELEFO] NOCHECK CONSTRAINT [FK_TELEFONOS_PROVEEDORES];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el si el telefono es principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'TelefPrincipal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo del telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el tipo de telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'TipoTelefono';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Telefono';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTELEFO', @level2type = N'COLUMN', @level2name = N'Autonumerico';

