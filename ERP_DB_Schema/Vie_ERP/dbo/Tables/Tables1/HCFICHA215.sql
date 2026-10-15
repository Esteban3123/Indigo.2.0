CREATE TABLE [dbo].[HCFICHA215] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NOMMAD]              VARCHAR (50)  NULL,
    [TIPID]               VARCHAR (50)  NULL,
    [NUMIDENT]            VARCHAR (50)  NULL,
    [EDAD]                VARCHAR (50)  NULL,
    [NUMEMB]              VARCHAR (50)  NULL,
    [NACVIVOS]            VARCHAR (50)  NULL,
    [ABORTOS]             VARCHAR (50)  NULL,
    [MORTINAT]            VARCHAR (50)  NULL,
    [DIAGNOS]             BIT           NULL,
    [EDADGESTDIA]         VARCHAR (50)  NULL,
    [PATCRON]             BIT           NULL,
    [CUALES]              VARCHAR (MAX) NULL,
    [EMBMULT]             BIT           NULL,
    [NATIVIVO]            INT           NULL,
    [EDADGESTNAC]         VARCHAR (50)  NULL,
    [PESO]                VARCHAR (50)  NULL,
    [PERIMCEF]            VARCHAR (50)  NULL,
    [DESC1]               VARCHAR (MAX) NULL,
    [DESC2]               VARCHAR (MAX) NULL,
    [DESC3]               VARCHAR (MAX) NULL,
    [DESC4]               VARCHAR (MAX) NULL,
    [DESC5]               VARCHAR (MAX) NULL,
    [DESC6]               VARCHAR (MAX) NULL,
    [DESC7]               VARCHAR (MAX) NULL,
    [DESC8]               VARCHAR (MAX) NULL,
    [STORCH]              BIT           NULL,
    [TSH1]                BIT           NULL,
    [TOTSUE1]             BIT           NULL,
    [LIBSUE1]             BIT           NULL,
    [TSH2]                BIT           NULL,
    [TOTSUE2]             BIT           NULL,
    [LIBSUE2]             BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA215] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA215_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA215_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA215] NOCHECK CONSTRAINT [CK_HCFICHA215_JSON];


GO
ALTER TABLE [dbo].[HCFICHA215] NOCHECK CONSTRAINT [FK_HCFICHA215_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'T4 Libre Suero 2  True = si false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'LIBSUE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'T4 Total suero 2  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TOTSUE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TSH 2  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TSH2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'T4 Libre Suero  True = si false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'LIBSUE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'T4 Total suero  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TOTSUE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TSH  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TSH1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'STORCH en Recién Nacido  true = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'STORCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perímetro Cefálico (Cms)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PERIMCEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso (Gramos) al Nacer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Gestacional al Momento del Nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDADGESTNAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nativivo  1=si  2=no   3=No ha nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NATIVIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Embarazo Múltiple   true = si   false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EMBMULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuales  otros ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'CUALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Patología Crónica Adicional o Complicaciones en Embarazo  true = si   false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PATCRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Gestional al Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDADGESTDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DIAGNOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mortinatos(>=22)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'MORTINAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Abortos (<22 Sem)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'ABORTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nacidos Vivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NACVIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Embarazos Totales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NUMEMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo ID (Documento Identificación) de la Madre --> desde versión ''V01_2020-03-06'' es con una enumeración:  1 = RC    2 = TI   3 = CC    4 = CE   5 = PA   6 = MS   7 = AS   8 = PE   9 = CN   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TIPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombres y Apellidos de la Madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NOMMAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'ID';

