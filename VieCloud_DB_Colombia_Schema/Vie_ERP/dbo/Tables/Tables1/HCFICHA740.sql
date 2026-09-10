CREATE TABLE [dbo].[HCFICHA740] (
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
    [NOMAPEMAD]           VARCHAR (50)  NULL,
    [TIPOID]              VARCHAR (50)  NULL,
    [NUMIDENT]            VARCHAR (50)  NULL,
    [RESGEST]             BIT           NULL,
    [NUMPROD]             VARCHAR (50)  NULL,
    [EDADGEST]            VARCHAR (50)  NULL,
    [RESSEREOMAD]         INT           NULL,
    [RESSEREORECNA]       INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA740] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA740_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA740_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA740] NOCHECK CONSTRAINT [CK_HCFICHA740_JSON];




GO
ALTER TABLE [dbo].[HCFICHA740] NOCHECK CONSTRAINT [CK_HCFICHA740_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento JSON de columnas adicionales; desde V01_2020-03-06 incluye TIEMPO_RESIDENCIA (booleano: true=>6 meses en país, false=<6 meses); nulo en versiones anteriores. Tipo: VARCHAR(MAX) con validación ISJSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde V01_2020-03-06 ) =    TIEMPO_RESIDENCIA = booleano ( Tiempo de Residencia en el País:  true = mayor a 6 / false = menor a 6)   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de ficha de notificación de sífilis gestacional; nulo indica versión inicial; desde V01_2020-03-06 introduce nuevas enumeraciones y campos. Tipo: VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de serología (DILS) del recién nacido: 1=≤2, 2=4, 3=8, 4=16, 5=32, 6=64, 7=128, 8=256, 9=512, 10=512, 11=2048, 12=No Reactiva, 13=2048, 14=No Reactiva. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESSEREORECNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Resultado serología del recién nacido   1 = (3) <= 2 DILS   2 = (4) 4 DILS    3 =(5) 8 DILS     4 = (6) 16 DILS      5 =(7) 32 DILS       6 =(8) 64 DILS     7 =(9) 128 DILS      8 =(10) 256 DILS      9 =  (11) 512 DILS   10 =(11) 512 DILS   11 = (13) 2048 DILS      12 =(14) No Reactiva   13 =  (13) 2048 DILS     14 = No Reactiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESSEREORECNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESSEREORECNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de serología (DILS) de la madre en el parto: 1=≤2, 2=4, 3=8, 4=16, 5=32, 6=64, 7=128, 8=256, 9=512, 10=1024, 11=2048. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESSEREOMAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Resultado de la serología de la madre en el parto  1 =(3) <= 2 DILS     2 =(4) 4 DILS     3 =(5) 8 DILS      4 =(6) 16 DILS     5 =(7) 32 DILS    6 = (8) 64 DILS    7 =(9) 128 DILS     8 =(10) 256 DILS     9 =(11) 512 DILS    10 =(12) 1024 DILS  11 =(13) 2048 DILS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESSEREOMAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESSEREOMAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional al nacimiento en semanas. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDADGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Edad gestacional al nacimiento (Semanas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDADGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDADGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de productos (hijos/embarazos múltiples) al nacimiento. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'NUMPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Número de productos al nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'NUMPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'NUMPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado gestacional: 1=Recién Nacido Vivo, 0=Mortinato. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda   Resultado de la gestación  1 =Recién Nacido Vivo     0 =Mortinato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación (cédula, documento) de la madre. PII - Identification_Ofuscado. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Número de identificación de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de la madre (desde V01_2020-03-06): 1=RC, 2=TI, 3=CC, 4=CE, 5=PA, 6=MS, 7=AS, 8=PE, 9=PT. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo ID (Documento Identificación) de la Madre --> desde versión ''''V01_2020-03-06'''' es con una enumeración:   1 = RC    2 = TI   3 = CC    4 = CE   5 = PA   6 = MS   7 = AS   8 = PE   9 = PT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'TIPOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre y apellido del padre o madre. PII. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'NOMAPEMAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda nombre del padre o la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'NOMAPEMAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'NOMAPEMAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tratamiento de contactos realizado: 1=Sí, 0=No. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'TRATCONTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Tratamiento de contactos   1 =Si    0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'TRATCONTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'TRATCONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de aplicación de primera dosis de penicilina benzatínica en gestante. Tipo: DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha aplicación primera dosis en la gestante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Penicilina benzatínica - cantidad de dosis: 1=0 dosis, 2=1 dosis, 3=2 dosis, 4=3 dosis. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'PENICILINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Penicilina benzatínica - número de dosis  1  =0 Dosis     2 =1 Dosis    3 =2 Dosis    4 =3 Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'PENICILINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'PENICILINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de segunda prueba serológica (DILS): 1=≤2, 2=4, 3=8, 4=16, 5=32, 6=64, 7=128, 8=256, 9=512, 10=1024, 11=2048. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda resultado2    1 =(3) <= 2 DILS       2 =(4) 4 DILS      3 =(5) 8 DILS      4 =(6) 16 DILS    5 =(7) 32 DILS      6=(8) 64 DILS      7=(9) 128 DILS     8 =(10) 256 DILS    9 =(11) 512 DILS    10  =(12) 1024 DILS     11 =(13) 2048 DILS  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional a realización de segunda prueba en semanas. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Edad gestacional a la realización de la prueba (Semanas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba no treponémica (VDRL o RPR) realizada: 1=Sí, 0=No. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'PRUEBANOTREPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Prueba no treponémica (VDRL ó RPR)   1 =  Si    0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'PRUEBANOTREPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'PRUEBANOTREPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de prueba treponémica: 1=Positivo/Sí, 0=Negativo/No. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Resultado  1 = Si    0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo prueba treponémica: 1=TPPA (eliminado V01_2020-03-06), 2=TPHA (eliminado V01_2020-03-06), 3=Prueba Rápida, 4=Otra (nuevo V01_2020-03-06). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual? (prueba treponémica):    1 = TPPA--> item eliminado desde versión ''''V01_2020-03-06''''    2 = TPHA --> item eliminado desde versión ''''V01_2020-03-06''''    3 = Prueba Rapida    4 = Otra  --> item nuevo desde versión ''''V01_2020-03-06''''    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional a realización de prueba treponémica en semanas. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Edad gestacional a la realización de la prueba (Semanas) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba treponémica realizada: 1=Sí, 0=No. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'PRUEBATREPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Prueba treponémica   1 =Si    0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'PRUEBATREPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'PRUEBATREPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico en embarazo actual: 1=Primera vez, 0=Reinfección. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'DIAGEMBACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Diagnóstico embarazo actual   1  = Primera Vez   0 =Reinfección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'DIAGEMBACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'DIAGEMBACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional al primer control prenatal en semanas. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDADGESTPRIMCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Edad gestacional al primer control prenatal (Semanas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDADGESTPRIMCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'EDADGESTPRIMCTRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control prenatal realizado en embarazo actual: 1=Sí, 0=No. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CTRLPRENEMBACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda ¿Control prenatal en embarazo actual?   1 =Si     0  =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CTRLPRENEMBACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CTRLPRENEMBACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición de la madre al momento diagnóstico: 1=Embarazo, 2=Parto, 3=Puerperio, 4=Post Aborto (eliminado V01_2020-03-06). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CONDMOMEDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Condición al Momento del Diagnóstico:    1 = Embarazo    2 = Parto    3 = Puerperio    4 = Post Aborto  --> item eliminado desde versión ''''V01_2020-03-06''''    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CONDMOMEDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CONDMOMEDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 de sífilis gestacional. Tipo: CHAR(4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ficha de notificación de sífilis (FK → HCFICHANOTIFICACION). Tipo: INT, NOT NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (consecutivo) del registro en tabla. Tipo: INT IDENTITY(1,1), PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación 740 para vigilancia epidemiológica de sífilis gestacional y congénita. Registra los datos clínicos y de laboratorio asociados al diagnóstico, tratamiento y resultado serológico de la madre y el recién nacido, incluyendo pruebas treponémicas y no treponémicas, manejo con penicilina y seguimiento del embarazo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA740';
