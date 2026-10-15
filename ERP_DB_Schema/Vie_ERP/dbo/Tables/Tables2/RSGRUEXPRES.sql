CREATE TABLE [dbo].[RSGRUEXPRES] (
    [ID]         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCGRUPO]  INT           NOT NULL,
    [EXPRESION]  VARCHAR (MAX) NOT NULL,
    [NOMRIESGO]  VARCHAR (500) NOT NULL,
    [DESRIESGO]  VARCHAR (MAX) NOT NULL,
    [PLANRIESGO] VARCHAR (MAX) NOT NULL,
    [FECHREGIS]  DATETIME      NOT NULL,
    CONSTRAINT [PK_RSGRUEXPRES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RSGRUEXPRES_RSGRUPO] FOREIGN KEY ([IDHCGRUPO]) REFERENCES [dbo].[RSGRUPO] ([ID])
);


GO
ALTER TABLE [dbo].[RSGRUEXPRES] NOCHECK CONSTRAINT [FK_RSGRUEXPRES_RSGRUPO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Generación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plan a seguir de acuerdo a lo que evalua la expresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'PLANRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del riesgo que evalua la expresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'DESRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del riesgo que evalua la expresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'NOMRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Expresion a validar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del grupo de revisión por sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'IDHCGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'ID';

