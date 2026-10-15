CREATE TABLE [dbo].[ADNIVELES] (
    [NIVCODIGO] CHAR (2)       NOT NULL,
    [NIVDESCRI] CHAR (100)     NOT NULL,
    [NIVPORCMO] NUMERIC (6, 2) NOT NULL,
    [NIVPORCOP] NUMERIC (6, 2) NOT NULL,
    [NIVPORSUB] NUMERIC (6, 2) NOT NULL,
    [NIVPORVIN] NUMERIC (6, 2) NOT NULL,
    [NIVSISBEN] INT            NOT NULL,
    [TOPEVECMO] MONEY          NOT NULL,
    [TOPEVECOP] MONEY          NOT NULL,
    [TOPEVESUB] MONEY          NOT NULL,
    [TOPEVEVIN] MONEY          NOT NULL,
    [TOPANUCMO] MONEY          NOT NULL,
    [TOPANUCOP] MONEY          NOT NULL,
    [TOPANUSUB] MONEY          NOT NULL,
    [TOPANUVIN] MONEY          NOT NULL,
    [INDAUDFOR] NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_ADNiveles] PRIMARY KEY CLUSTERED ([NIVCODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tope anual vinculados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUVIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tope anual regimen subsidiado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tope anual regimen contributivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tope anual cuota moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUCMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tope evento vinculados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVEVIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tope vento regimen subsidiado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVESUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tope evento regimen contributivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVECOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tope  evento cuota moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVECMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel del Sisben', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVSISBEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del copago para pacientes vinculados del nivel o estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORVIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del copago para el regimen subsidiado para el nivel o estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del copago para el regimen contributivo del nivel o estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de la cuota moderadora para el nivel o estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORCMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Nivel o Estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Nivel o Estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVCODIGO';

