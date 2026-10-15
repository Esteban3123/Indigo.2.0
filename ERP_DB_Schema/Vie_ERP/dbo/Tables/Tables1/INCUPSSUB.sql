CREATE TABLE [dbo].[INCUPSSUB] (
    [CODGRUSUB]    CHAR (13)  NOT NULL,
    [CODGRUIPS]    CHAR (3)   NOT NULL,
    [CODSUBIPS]    CHAR (10)  NOT NULL,
    [DESSUBIPS]    CHAR (300) NOT NULL,
    [MDASHIMAG]    BIT        NOT NULL,
    [IDRISGRIMAGE] INT        NULL,
    CONSTRAINT [PK_INCUPSSUB] PRIMARY KEY CLUSTERED ([CODGRUSUB] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Identificador de grupo de imagenología, tabla RISGRIMAGE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'IDRISGRIMAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el subgrupo se lista en el dashboard de Imagenologia y Lecturas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'MDASHIMAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del SubGrupo del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'DESSUBIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del SubGrupo del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODSUBIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Grupo del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODGRUIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo concatenado del  grupo y subgrupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODGRUSUB';

