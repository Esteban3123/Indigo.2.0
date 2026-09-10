CREATE TABLE [dbo].[HCFICHA720] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CLASIFINIC]          BIT           NULL,
    [LUGNACIPAC]          VARCHAR (50)  NULL,
    [DEPAMUNIVIAJ]        VARCHAR (50)  NULL,
    [FUENTENOTI]          INT           NULL,
    [NOMMADRE]            VARCHAR (50)  NULL,
    [EDADMADRE]           VARCHAR (50)  NULL,
    [EMBARAZOS]           VARCHAR (50)  NULL,
    [VIAJES]              INT           NULL,
    [SEMEMBA]             VARCHAR (50)  NULL,
    [PAISVIAJO]           VARCHAR (50)  NULL,
    [DEPAMUNIVIAJ2]       VARCHAR (50)  NULL,
    [APGAR]               VARCHAR (50)  NULL,
    [BAJOPESNAC]          INT           NULL,
    [PESO]                VARCHAR (50)  NULL,
    [PEQEDAGEST]          INT           NULL,
    [SEMANAS]             VARCHAR (50)  NULL,
    [CATARATAS]           INT           NULL,
    [GLAUCOMA]            INT           NULL,
    [RETINOPA]            INT           NULL,
    [OTRO1]               VARCHAR (50)  NULL,
    [PERSISTENT]          INT           NULL,
    [ESTENOSIS]           INT           NULL,
    [OTRO2]               VARCHAR (50)  NULL,
    [SORDERA]             INT           NULL,
    [OTRO3]               VARCHAR (50)  NULL,
    [MICROCEF]            INT           NULL,
    [RETRASO]             INT           NULL,
    [PURPURA]             INT           NULL,
    [HIGADO]              INT           NULL,
    [ICTERICIA]           INT           NULL,
    [BAZO]                INT           NULL,
    [OSTEOPA]             INT           NULL,
    [MENINGO]             INT           NULL,
    [OTRO4]               VARCHAR (50)  NULL,
    [FECHAINICIO]         DATE          NULL,
    [DIAGNOFIN]           INT           NULL,
    [INVESTPOR]           VARCHAR (50)  NULL,
    [TELEFONO]            VARCHAR (50)  NULL,
    [FECHATOMA1]          DATE          NULL,
    [FECHATOMA2]          DATE          NULL,
    [FECHATOMA3]          DATE          NULL,
    [FECHATOMA4]          DATE          NULL,
    [FECHARECE1]          DATE          NULL,
    [FECHARECE2]          DATE          NULL,
    [FECHARECE3]          DATE          NULL,
    [FECHARECE4]          DATE          NULL,
    [MUESTRA1]            INT           NULL,
    [MUESTRA2]            INT           NULL,
    [MUESTRA3]            INT           NULL,
    [MUESTRA4]            INT           NULL,
    [PRUEBA1]             INT           NULL,
    [PRUEBA2]             INT           NULL,
    [PRUEBA3]             INT           NULL,
    [PRUEBA4]             INT           NULL,
    [AGENTE1]             INT           NULL,
    [AGENTE2]             INT           NULL,
    [AGENTE3]             INT           NULL,
    [AGENTE4]             INT           NULL,
    [RESULTADO1]          INT           NULL,
    [RESULTADO2]          INT           NULL,
    [RESULTADO3]          INT           NULL,
    [RESULTADO4]          INT           NULL,
    [FECHARESUL1]         DATE          NULL,
    [FECHARESUL2]         DATE          NULL,
    [FECHARESUL3]         DATE          NULL,
    [FECHARESUL4]         DATE          NULL,
    [VALREGIS1]           VARCHAR (50)  NULL,
    [VALREGIS2]           VARCHAR (50)  NULL,
    [VALREGIS3]           VARCHAR (50)  NULL,
    [VALREGIS4]           VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA720] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA720_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA720_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA720] NOCHECK CONSTRAINT [CK_HCFICHA720_JSON];




GO
ALTER TABLE [dbo].[HCFICHA720] NOCHECK CONSTRAINT [CK_HCFICHA720_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor registrado1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'VALREGIS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor registrado1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'VALREGIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor registrado1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'VALREGIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor registrado1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'VALREGIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del resultado4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHARESUL4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del resultado3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHARESUL3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del resultado2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHARESUL2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del resultado1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHARESUL1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado4   1 =Positivo   2 =  Negativo 3 = No Procesado   4 =Inadecuado   5 =Dudoso   6 = Valor Registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'RESULTADO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado3   1 =Positivo   2 =  Negativo 3 = No Procesado   4 =Inadecuado   5 =Dudoso   6 = Valor Registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'RESULTADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado2    1 =Positivo   2 =  Negativo 3 = No Procesado   4 =Inadecuado   5 =Dudoso   6 = Valor Registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado1  1 =Positivo   2 =  Negativo 3 = No Procesado   4 =Inadecuado   5 =Dudoso   6 = Valor Registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'RESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda agente4  1 =Rubéola    2 = Citomegalovirus   3 = Toxoplasma    4 =Sífilis   5 =Virus Herpes  6 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'AGENTE4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda agente3   1 =Rubéola    2 = Citomegalovirus   3 = Toxoplasma    4 =Sífilis   5 =Virus Herpes  6 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'AGENTE3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda agente2  1 =Rubéola    2 = Citomegalovirus   3 = Toxoplasma    4 =Sífilis   5 =Virus Herpes  6 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'AGENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda agente1   1 =Rubéola    2 = Citomegalovirus   3 = Toxoplasma    4 =Sífilis   5 =Virus Herpes  6 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'AGENTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la prueba4   1  =PCR   2 =Patología   3 = Elisa   4 = Aislamiento Viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'PRUEBA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la prueba3   1  =PCR   2 =Patología   3 = Elisa   4 = Aislamiento Viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'PRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la prueba2   1  =PCR   2 =Patología   3 = Elisa   4 = Aislamiento Viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'PRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la prueba1  1  =PCR   2 =Patología   3 = Elisa   4 = Aislamiento Viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'PRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Muestra4    1 =Orina   2  =Hisopado  3 = Tejido   4 =Aspirado Nasofaríngeo  5 =Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'MUESTRA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Muestra3     1 =Orina   2  =Hisopado  3 = Tejido   4 =Aspirado Nasofaríngeo  5 =Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'MUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Guarda la Muestra2     1 =Orina   2  =Hisopado  3 = Tejido   4 =Aspirado Nasofaríngeo  5 =Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'MUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Muestra1    1 =Orina   2  =Hisopado  3 = Tejido   4 =Aspirado Nasofaríngeo  5 =Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'MUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de recepción4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHARECE4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de recepción3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHARECE3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de recepción2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHARECE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de recepción1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHARECE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de la toma4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHATOMA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de la toma3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de la toma2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de la toma1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Investigado por:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'INVESTPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Diagnóstico final  1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'DIAGNOFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Fecha de inicio de investigación de campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FECHAINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro de  Otros Órganos ---> (item de pregunta 7.9) = Eliminada desde version V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'OTRO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Meningoencefalitis   1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'MENINGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Osteopatía radiolúcida   1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'OSTEOPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bazo agrandado (Esplenomegalia)  1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'BAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Ictericia al nacer  1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'ICTERICIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Hígado agrandado (Hepatomegalia)  1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'HIGADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda purpura    1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'PURPURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Retraso en el desarrollo psicomotor     1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'RETRASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guara el Microcefalia       1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'MICROCEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el otro3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'OTRO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la sordera   1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'SORDERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Otro2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'OTRO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estenosis de la arteria pulmonar  1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'ESTENOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda persistente del conducto arterioso   1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'PERSISTENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Otro1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'OTRO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si tiene Retinopatia en los ojos    1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'RETINOPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si tiene Glaucoma en los ojos    1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'GLAUCOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si tiene cataratas en los ojos    1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'CATARATAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda las semanas de nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'SEMANAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  pequeño para la edad gestacional   1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'PEQEDAGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'PESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el bajo de peso al nacer   1 = Si   2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'BAJOPESNAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda APGAR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'APGAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda departamento donde viajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'DEPAMUNIVIAJ2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda pais donde viajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'PAISVIAJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda las semanas de embarazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'SEMEMBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda viajes    1 = Si   2 = No  3 =Desconocido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'VIAJES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda numero de embarazoz previos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'EMBARAZOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la edad de la madre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'EDADMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el nombre de la madre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'NOMMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fuente de notificación  1 =Pública  2 =Privada  3 =Laboratorio   4 =Comunidad   5 =Búsqueda Activa  6 =Otras   7 =Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'FUENTENOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Departamento / Municipio donde Viajó ---> (pregunta 5.3) = Eliminada desde version V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'DEPAMUNIVIAJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el lugar de nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'LUGNACIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la clasificación inicial   1 = Sospechoso por datos clínicos   0 =  Sospechoso por hijo de madre con sospecha o confirmación de rubéola', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'CLASIFINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación 720 para vigilancia epidemiológica de Síndrome de Rubéola Congénita (SRC) y eventos perinatales similares. Registra datos del recién nacido, la madre, manifestaciones clínicas congénitas, viajes, muestras de laboratorio y resultados de pruebas diagnósticas exigidos por el SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA720';
