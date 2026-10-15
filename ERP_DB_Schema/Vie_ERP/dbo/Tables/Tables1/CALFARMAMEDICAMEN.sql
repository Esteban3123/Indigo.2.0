CREATE TABLE [dbo].[CALFARMAMEDICAMEN] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFARMACOVIGILANCIA] INT           NOT NULL,
    [SCI]                 INT           NULL,
    [CODPRODUC]           CHAR (20)     NULL,
    [INDICACIONES]        VARCHAR (MAX) NULL,
    [DOSIS]               VARCHAR (MAX) NULL,
    [CODUNIMED]           CHAR (3)      NULL,
    [CODVIAADM]           CHAR (3)      NULL,
    [FRECUENCIA]          VARCHAR (MAX) NULL,
    [FECHAINICIO]         DATETIME      NULL,
    [FECHAFIN]            DATETIME      NULL,
    CONSTRAINT [PK_CALFARMAMEDICAMEN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALFARMAMEDICAMEN_CALFARMACOVIGILANCIA] FOREIGN KEY ([IDFARMACOVIGILANCIA]) REFERENCES [dbo].[CALFARMACOVIGILANCIA] ([ID]),
    CONSTRAINT [FK_CALFARMAMEDICAMEN_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[CALFARMAMEDICAMEN] NOCHECK CONSTRAINT [FK_CALFARMAMEDICAMEN_CALFARMACOVIGILANCIA];


GO
ALTER TABLE [dbo].[CALFARMAMEDICAMEN] NOCHECK CONSTRAINT [FK_CALFARMAMEDICAMEN_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FECHAFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicio ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FECHAINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Via Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Unidad Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis del medicamentos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'DOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'INDICACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto Relacionado con CODPRODUC de la tabla IHLISTPRO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Accion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'SCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID que coresponde con ID de la tabla CALFARMACOVIGILANCIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'IDFARMACOVIGILANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'ID';

