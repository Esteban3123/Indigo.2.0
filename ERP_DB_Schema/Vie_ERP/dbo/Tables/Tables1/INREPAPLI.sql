CREATE TABLE [dbo].[INREPAPLI] (
    [NUMCONSEC] INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMREPARC] VARCHAR (50)    NOT NULL,
    [NOMREPVIS] VARCHAR (255)   NOT NULL,
    [DESCREPOR] VARCHAR (255)   NOT NULL,
    [INDMODULO] CHAR (2)        NULL,
    [ESMODIFIC] BIT             NOT NULL,
    [BUFFER]    VARBINARY (MAX) NULL,
    CONSTRAINT [PK_INREPORCO] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK_INREPORCO_SEGmodulu] FOREIGN KEY ([INDMODULO]) REFERENCES [dbo].[SEGmodulu] ([indmodulo]),
    CONSTRAINT [FK_INREPUSUA_SEGmodulu] FOREIGN KEY ([INDMODULO]) REFERENCES [dbo].[SEGmodulu] ([indmodulo])
);


GO
ALTER TABLE [dbo].[INREPAPLI] NOCHECK CONSTRAINT [FK_INREPORCO_SEGmodulu];


GO
ALTER TABLE [dbo].[INREPAPLI] NOCHECK CONSTRAINT [FK_INREPUSUA_SEGmodulu];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_INREPAPLI]
    ON [dbo].[INREPAPLI]([NOMREPARC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Buffer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'BUFFER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite Modificar el Diseño del Reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'ESMODIFIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del modulo en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'INDMODULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'DESCREPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Logico del Reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NOMREPVIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Archivo Fisico (Sin extension)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NOMREPARC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Autonumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';

