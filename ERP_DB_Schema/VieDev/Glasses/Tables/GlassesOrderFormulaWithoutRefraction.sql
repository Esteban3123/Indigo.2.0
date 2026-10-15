CREATE TABLE [Glasses].[GlassesOrderFormulaWithoutRefraction] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [IdGlassesOrderC] INT          NOT NULL,
    [Eye]             INT          NOT NULL,
    [Sphere]          VARCHAR (10) NOT NULL,
    [Cylinder]        VARCHAR (10) NOT NULL,
    [Axis]            VARCHAR (10) NOT NULL,
    [Adition]         VARCHAR (10) NOT NULL,
    [CODPRODUC]       CHAR (20)    NOT NULL,
    CONSTRAINT [PK_GlassesOrderFormulaWithoutRefraction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlassesOrderFormulaWithoutRefraction_GlassesOrderC] FOREIGN KEY ([IdGlassesOrderC]) REFERENCES [Glasses].[GlassesOrderC] ([Id])
);


GO
ALTER TABLE [Glasses].[GlassesOrderFormulaWithoutRefraction] NOCHECK CONSTRAINT [FK_GlassesOrderFormulaWithoutRefraction_GlassesOrderC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto (En este caso dispositivos oftalmológicos)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo adicion', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Adition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo eje', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Axis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo cilindro', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Cylinder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo esfera', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Sphere';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo ojo: 1: derecho, 2:izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla GlassesOrdrC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';

