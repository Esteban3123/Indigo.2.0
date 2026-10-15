CREATE TABLE [dbo].[HCSOPORTECAC] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODUSUARIO]      CHAR (20)     NOT NULL,
    [ANIO]            INT           NOT NULL,
    [RUTA]            VARCHAR (MAX) NOT NULL,
    [CENTROATENCION]  VARCHAR (MAX) NOT NULL,
    [IP]              VARCHAR (50)  NOT NULL,
    [NOMBREPC]        VARCHAR (250) NOT NULL,
    [ESTADO]          INT           NOT NULL,
    [FECHACREACION]   DATETIME      NOT NULL,
    [FECHAFIN]        DATETIME      NULL,
    [IDENTIDAD]       INT           NULL,
    [FECHAINICIOBUSQ] DATETIME      NULL,
    [FECHAFINALBUSQ]  DATETIME      NULL,
    [TIPOBUSQUEDA]    INT           NULL,
    CONSTRAINT [PK_HCSOPORTECAC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - consulta general (pestaña 1 soporte cac)  2 - consulta x documento (pestaña 2 soporte cac)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'TIPOBUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 -  Iniciado  2 - Terminado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ESTADO';

