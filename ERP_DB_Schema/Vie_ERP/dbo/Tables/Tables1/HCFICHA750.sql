CREATE TABLE [dbo].[HCFICHA750] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CONDMOMEDIAG]        INT           NULL,
    [CTRLPRENEMBACT]      BIT           NULL,
    [EDADGESTPRIMCTRL]    VARCHAR (50)  NULL,
    [DIAGEMBACT]          BIT           NULL,
    [PRUEBATREPO]         BIT           NULL,
    [EDGESTREAPRUE]       VARCHAR (50)  NULL,
    [CUAL]                INT           NULL,
    [RESULTADO]           BIT           NULL,
    [PRUEBANOTREPO]       BIT           NULL,
    [EDGESTREAPRUE2]      VARCHAR (50)  NULL,
    [RESULTADO2]          INT           NULL,
    [PENICILINA]          INT           NULL,
    [FECHAAPLI]           DATE          NULL,
    [TRATCONTAC]          BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA750] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA750_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA750_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA750] NOCHECK CONSTRAINT [CK_HCFICHA750_JSON];


GO
ALTER TABLE [dbo].[HCFICHA750] NOCHECK CONSTRAINT [FK_HCFICHA750_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tratamiento de contactos (TRATAMIENTO MATERNO, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'TRATCONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha aplicación primera dosis en la  (TRATAMIENTO MATERNO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Penicilina benzatínica - número de Dosis  (TRATAMIENTO MATERNO, (1. 0 dosis, 2. 1 dosis, 3. 2 dosis, 4. 3 dosis))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PENICILINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'RESULTADO2 ( PRUEBA NO TREPONEMICA (VDRL ó RPR), (1. (3) <= 2 DILS, 2. (4) 4 DILS, 3. (5) 8 DILS, 4. (6) 16 DILS, 5. (7) 32 DILS, 6. (8) 64 DILS, 7. (9) 128 DILS, 8. (10) 256 DILS, 9. (11) 512 DILS, 10. (12) 1024 DILS), 11. (13) 2048 DILS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional a la realización de la prueba (PRUEBA TREPONÉMICA, (semanas))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba no treponémica (PRUEBA NO TREPONEMICA (VDRL ó RPR, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PRUEBANOTREPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'RESULTADO ( PRUEBA TREPONÉMICA, (1. positivo, 2. negativo))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cual (PRUEBA TREPONÉMICA, (4. Prueba rápida, 5. Otra))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional a la realización de la prueba  (PRUEBA TREPONÉMICA, (semanas))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba treponémica (PRUEBA TREPONÉMICA, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PRUEBATREPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico embarazo actual (DIAGNÓSTICO MATERNO, (1. primera vez, 2. Reinfección))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'DIAGEMBACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional al primer control prenatal (DIAGNÓSTICO MATERNO, (semanas))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDADGESTPRIMCTRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Control prenatal en embarazo actual? (DIAGNÓSTICO MATERNO, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CTRLPRENEMBACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición al momento del diagnóstico (DIAGNÓSTICO MATERNO, (1. embarazo, 2. parto, 3. puerperio))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CONDMOMEDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'ID';

