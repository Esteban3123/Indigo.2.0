CREATE TABLE [dbo].[HCORHEMPRUEBAC] (
    [ID]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORHEMCOID]        INT           NOT NULL,
    [CODIGOBACTERIOLOGO] CHAR (20)     NOT NULL,
    [FECHAPRUEBA]        DATETIME      NOT NULL,
    [ESTADO]             INT           NOT NULL,
    [FECHAREGISTRO]      DATETIME      NOT NULL,
    [CODIGOCUPS]         VARCHAR (20)  NULL,
    [NOMBREBACTERIOLOGO] VARCHAR (150) NULL,
    [NUMEROBOLSA]        VARCHAR (20)  NULL,
    [SELLOCALIDAD]       VARCHAR (20)  NULL,
    CONSTRAINT [PK_HCORHEMPRUEBAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORHEMPRUEBAC_HCORHEMCO] FOREIGN KEY ([HCORHEMCOID]) REFERENCES [dbo].[HCORHEMCO] ([ID]),
    CONSTRAINT [FK_HCORHEMPRUEBAC_INPROFSAL] FOREIGN KEY ([CODIGOBACTERIOLOGO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCORHEMPRUEBAC] NOCHECK CONSTRAINT [FK_HCORHEMPRUEBAC_HCORHEMCO];


GO
ALTER TABLE [dbo].[HCORHEMPRUEBAC] NOCHECK CONSTRAINT [FK_HCORHEMPRUEBAC_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'SELLOCALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'NUMEROBOLSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del bacteriologo que realiza prueba cruzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'NOMBREBACTERIOLOGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'CODIGOCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado: 1 - Compatible     2- Incompatible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del bacteriologo que realiza prueba cruzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'CODIGOBACTERIOLOGO';

