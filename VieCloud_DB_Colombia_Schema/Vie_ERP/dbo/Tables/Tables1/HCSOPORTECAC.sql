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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de búsqueda en generación de soportes CAC: 1=consulta general (pestaña 1), 2=consulta por documento/identificación (pestaña 2). INT, dominio cerrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'TIPOBUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - consulta general (pestaña 1 soporte cac)  2 - consulta x documento (pestaña 2 soporte cac)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'TIPOBUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'TIPOBUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final del rango de búsqueda de registros para generar soporte CAC. DATETIME, nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAFINALBUSQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAFINALBUSQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAFINALBUSQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial del rango de búsqueda de registros para generar soporte CAC. DATETIME, nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAINICIOBUSQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial Busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAINICIOBUSQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAINICIOBUSQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de la entidad/paciente/usuario consultado durante la búsqueda CAC. INT, nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'IDENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Entidad consultada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'IDENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'IDENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización real del proceso de generación de soporte CAC. DATETIME, nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha fin del proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHAFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio del proceso de generación de soporte CAC. DATETIME, timestamp de creación del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicio del proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del proceso: 1=Iniciado, 2=Terminado/Completado. INT, dominio cerrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 -  Iniciado  2 - Terminado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o hostname de la máquina/equipo que ejecutó la generación del soporte CAC. VARCHAR(250).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'NOMBREPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la maquina que ejecuta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'NOMBREPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'NOMBREPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección IP de la máquina que ejecutó el proceso de generación de soporte CAC. VARCHAR(50), PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'IP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IP de la maquina que ejecuta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'IP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'IP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro(s) de atención concatenados de donde se extrajeron datos para generar soportes CAC. VARCHAR(MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'CENTROATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de atencion concatenados de donde se toma la informacion para generar los soportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'CENTROATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'CENTROATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta completa del servidor de archivos donde se almacenó el PDF del soporte CAC generado. VARCHAR(MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'RUTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ruta de generacion de los PDF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'RUTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'RUTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año de corte/ejercicio fiscal del período consultado en la generación del soporte CAC. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ANIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año de corte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ANIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ANIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que ejecutó el proceso de generación de soporte CAC. CHAR(20), PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario ejecuta proceso de generacion CAC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) del registro de ejecución de soporte CAC. INT IDENTITY, PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de soportes o archivos adjuntos del CAC (Centro de Atención al Cliente / Historia Clínica), donde se almacenan las rutas de archivos cargados por usuario y año, junto con el estado del proceso, la máquina desde la que se realizó la carga y los rangos de fecha utilizados en búsquedas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECAC';
