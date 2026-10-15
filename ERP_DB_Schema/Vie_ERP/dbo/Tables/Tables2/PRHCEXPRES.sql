CREATE TABLE [dbo].[PRHCEXPRES] (
    [ID]         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC] INT           NOT NULL,
    [EXPRESION]  VARCHAR (MAX) NOT NULL,
    [NOMRIESGO]  VARCHAR (500) NOT NULL,
    [DESRIESGO]  VARCHAR (MAX) NOT NULL,
    [PLANRIESGO] VARCHAR (MAX) NOT NULL,
    [FECHREGIS]  DATETIME      NOT NULL,
    [AGRPAQUETE] BIT           NULL,
    CONSTRAINT [PK_PRHCEXPRES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCEXPRES_MODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCEXPRES] NOCHECK CONSTRAINT [FK_PRHCEXPRES_MODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'True = SI  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'AGRPAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda le fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el plan de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'PLANRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la descripción del riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'DESRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el nombre del riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'NOMRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Expresion a validar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id modelo de historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'ID';

