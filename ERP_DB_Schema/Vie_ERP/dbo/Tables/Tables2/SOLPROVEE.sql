CREATE TABLE [dbo].[SOLPROVEE] (
    [PROVAUTO]    NUMERIC (18)  IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PROVENIT]    CHAR (15)     NOT NULL,
    [PROVNOMBRE]  VARCHAR (100) NOT NULL,
    [PROVESTADO]  BIT           NOT NULL,
    [PROVUBICA]   CHAR (2)      NOT NULL,
    [PROVCALPRE]  TINYINT       NULL,
    [PROVCALCALI] TINYINT       NULL,
    [PROVCALTEN]  TINYINT       NULL,
    CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED ([PROVAUTO] ASC),
    CONSTRAINT [FK_SOLPROVEE_INDEPARTA] FOREIGN KEY ([PROVUBICA]) REFERENCES [dbo].[INDEPARTA] ([depcodigo])
);


GO
ALTER TABLE [dbo].[SOLPROVEE] NOCHECK CONSTRAINT [FK_SOLPROVEE_INDEPARTA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la calificacion del proveedor ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALTEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la calificacion del proveedor en calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALCALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la calificacion del proveedor en precios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la ubicacion del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVUBICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el estado del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el nombre del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el nit del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVENIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene le autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVAUTO';

