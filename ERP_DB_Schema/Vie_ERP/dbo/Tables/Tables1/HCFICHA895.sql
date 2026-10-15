CREATE TABLE [dbo].[HCFICHA895] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [COMPLITIPONEUR]      BIT           NULL,
    [FECHAINICIOSIND]     DATE          NULL,
    [TIPOCOMPLNEUR]       VARCHAR (50)  NULL,
    [DESPLAULTI]          BIT           NULL,
    [MUNIDEPA]            VARCHAR (50)  NULL,
    [FECHAULTMENS]        DATE          NULL,
    [REALPRIMECOGES]      BIT           NULL,
    [FECHAPRIMECOG]       DATE          NULL,
    [EDADGESTPRIMEC]      VARCHAR (50)  NULL,
    [ENCUSEGUEAPB]        INT           NULL,
    [GESTTERMEMB]         BIT           NULL,
    [FECHTERMEMB]         DATE          NULL,
    [CONDIFINAL]          INT           NULL,
    [PERIMCEFA]           VARCHAR (50)  NULL,
    [REALNECROCLI]        BIT           NULL,
    [DEFECONGE]           BIT           NULL,
    [TOMOMUESSUER]        BIT           NULL,
    [TOMOMUESCORD]        BIT           NULL,
    [EXANTEMA]            BIT           NULL,
    [FIEBRE]              BIT           NULL,
    [HIPEREMIA]           BIT           NULL,
    [ARTRALGIAS]          BIT           NULL,
    [MIALGIAS]            BIT           NULL,
    [CEFALEA]             BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA895] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA895_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA895_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA895] NOCHECK CONSTRAINT [CK_HCFICHA895_JSON];


GO
ALTER TABLE [dbo].[HCFICHA895] NOCHECK CONSTRAINT [FK_HCFICHA895_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON     Desde versión "V01_2020-03-06" -->  - CODIGOCIE10 : 4 caracteres alfanumericos   - CODPAIS_DESPLAZAMIENTO : 3 caracteres alfanumericos   - CODDEPARTAMENTO_DESPLAZAMIENTO : 2 caracteres alfanumericos   - CODMUNICIPIO_DESPLAZAMIENTO : 3 caracteres alfanumericos    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si Seleccione los signos y sintomas que haya presentado  Cefalea  1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si Seleccione los signos y sintomas que haya presentado  Mialgias   1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si Seleccione los signos y sintomas que haya presentado  Artralgias   1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si Seleccione los signos y sintomas que haya presentado  Hiperemia conjuntival (Conjuntivitis)   1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'HIPEREMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si Seleccione los signos y sintomas que haya presentado     Fiebre   1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si Seleccione los signos y sintomas que haya presentado  Exantema (Rash)   1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'EXANTEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  ¿Se tomó muestra de cordón umbilical?   1 = Si     0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TOMOMUESCORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  ¿Se tomó muestra de suero?   1 = Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TOMOMUESSUER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Defecto congénito  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'DEFECONGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ¿Se realizó necropsia clínica?   1 = Si    0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'REALNECROCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Perímetro cefálico (Cms)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'PERIMCEFA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Condición final  1 =Aborto     2 =Muerte perinatal     3 =Nacido vivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CONDIFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha de terminación del embarazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHTERMEMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ¿La gestante terminó el embarazo?   1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'GESTTERMEMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ¿Se encuentra en seguimiento por la EAPB?   1=Si     2 =No     3 =Pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ENCUSEGUEAPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Edad gestacional de la primera ecografía obstétrica ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'EDADGESTPRIMEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de la primera ecografía obstétrica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAPRIMECOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ¿Se le realizó la primera ecografía a la gestante?   1 = Si       0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'REALPRIMECOGES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de la última menstruación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAULTMENS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Municipio/Departamento/País de desplazamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'MUNIDEPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ¿Desplazamientos en los últimos 30 días?     1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'DESPLAULTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Tipo de complicación neurológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLNEUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de inicio del sindrome neurológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAINICIOSIND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ¿Tiene complicaciones de tipo neurológico?  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'COMPLITIPONEUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ID';

