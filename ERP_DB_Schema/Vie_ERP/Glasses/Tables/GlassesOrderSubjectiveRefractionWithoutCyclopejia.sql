CREATE TABLE [Glasses].[GlassesOrderSubjectiveRefractionWithoutCyclopejia] (
    [Id]                   INT          IDENTITY (1, 1) NOT NULL,
    [IdGlassesOrderC]      INT          NOT NULL,
    [Eye]                  INT          NOT NULL,
    [Sphere]               VARCHAR (10) NOT NULL,
    [Cylinder]             VARCHAR (10) NOT NULL,
    [Axis]                 VARCHAR (10) NOT NULL,
    [Adition]              VARCHAR (20) NOT NULL,
    [FarVisualAcuity]      INT          NOT NULL,
    [ProximalVisualAcuity] INT          NOT NULL,
    [CODPRODUC]            CHAR (20)    NOT NULL,
    [Pinhole]              INT          NULL,
    CONSTRAINT [PK_GlassesOrderSubjectiveRefractionWithoutCyclopejia] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlassesOrderSubjectiveRefractionWithoutCyclopejia_IdGlassesOrderC] FOREIGN KEY ([IdGlassesOrderC]) REFERENCES [Glasses].[GlassesOrderC] ([Id])
);


GO
ALTER TABLE [Glasses].[GlassesOrderSubjectiveRefractionWithoutCyclopejia] NOCHECK CONSTRAINT [FK_GlassesOrderSubjectiveRefractionWithoutCyclopejia_IdGlassesOrderC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto asociado a la refracción (en este caso será el codigo de un dispositivo)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo agudeza visual proxima', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'ProximalVisualAcuity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo agudeza visual lejana', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'FarVisualAcuity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo adición', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Adition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Eje', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Axis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo cilindro', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Cylinder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Esfera', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Sphere';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo ojo: 1:Derecho, 2 :Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla GlassesOrderC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';

