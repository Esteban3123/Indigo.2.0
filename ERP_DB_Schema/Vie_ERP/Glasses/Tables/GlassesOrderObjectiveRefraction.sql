CREATE TABLE [Glasses].[GlassesOrderObjectiveRefraction] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [IdGlassesOrderC] INT          NOT NULL,
    [TypeRefraction]  INT          NOT NULL,
    [Eye]             INT          NOT NULL,
    [Sphere]          VARCHAR (10) NOT NULL,
    [Cylinder]        VARCHAR (10) NOT NULL,
    [Axis]            VARCHAR (10) NOT NULL,
    [CODPRODUC]       CHAR (20)    NOT NULL,
    CONSTRAINT [PK_GlassesOrderObjectiveRefraction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlassesOrderObjectiveRefraction_GlassesOrderC] FOREIGN KEY ([IdGlassesOrderC]) REFERENCES [Glasses].[GlassesOrderC] ([Id])
);


GO
ALTER TABLE [Glasses].[GlassesOrderObjectiveRefraction] NOCHECK CONSTRAINT [FK_GlassesOrderObjectiveRefraction_GlassesOrderC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto asociado a la refracción (en este caso será el codigo de un dispositivo)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo eje', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Axis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo cilindro', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Cylinder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo esfera', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Sphere';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo ojo: 1: Derecho, 2:Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo tipo de refraccion: 1: con cicloplejia, 2: sin cicloplejia (Aplica solo para esta tabla)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'TypeRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla GlassesOrderC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';

