CREATE TABLE [dbo].[HCACIBASD] (
    [CODCONCEC]  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDACIDBASE] INT           NOT NULL,
    [COBALACIBA] CHAR (3)      NULL,
    [VALOR]      VARCHAR (100) NULL,
    CONSTRAINT [PK_HCACIBASD] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [IDACIDBASE] ASC),
    CONSTRAINT [FK_HCACIBASD_HCACIBASD] FOREIGN KEY ([IDACIDBASE]) REFERENCES [dbo].[HCACIBASE] ([IDACIDBASE])
);


GO
ALTER TABLE [dbo].[HCACIBASD] NOCHECK CONSTRAINT [FK_HCACIBASD_HCACIBASD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modo de Ventilacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modo de Ventilacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'COBALACIBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacionado con IDACIDBASE de la tabla HCACIBASE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'IDACIDBASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

