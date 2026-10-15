CREATE TABLE [dbo].[SOLCATEGO] (
    [CATEAUTON] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CATCODIGO] VARCHAR (20)  NOT NULL,
    [CATDESCRI] VARCHAR (100) NOT NULL,
    [CATESTADO] BIT           NOT NULL,
    CONSTRAINT [PK_CATEGORIAS] PRIMARY KEY CLUSTERED ([CATEAUTON] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la categoria 1= Activo;2=Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la categoria ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la categoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATEAUTON';

