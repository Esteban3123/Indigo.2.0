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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAFINALBUSQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial Busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAINICIOBUSQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Entidad consultada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'IDENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha fin del proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicio del proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 -  Iniciado  2 - Terminado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la maquina que ejecuta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'NOMBREPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IP de la maquina que ejecuta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'IP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de atencion concatenados de donde se toma la informacion para generar los soportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'CENTROATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ruta de generacion de los PDF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'RUTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año de corte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ANIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario ejecuta proceso de generacion CAC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ID';

