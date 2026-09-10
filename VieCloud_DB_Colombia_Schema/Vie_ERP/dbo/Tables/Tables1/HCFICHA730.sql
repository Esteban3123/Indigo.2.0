CREATE TABLE [dbo].[HCFICHA730] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NOMPADMAD]           VARCHAR (50)  NULL,
    [OCUPAPADMAD]         VARCHAR (50)  NULL,
    [DIRETRABA]           VARCHAR (50)  NULL,
    [CASODETECT]          INT           NULL,
    [VACUCONTSARA]        INT           NULL,
    [DOSIS1]              VARCHAR (50)  NULL,
    [DOSIS2]              VARCHAR (50)  NULL,
    [ULTIDOSIS1]          DATE          NULL,
    [ULTIDOSIS2]          DATE          NULL,
    [FUENTE1]             INT           NULL,
    [FUENTE2]             INT           NULL,
    [TIPOVACU1]           INT           NULL,
    [TIPOVACU2]           INT           NULL,
    [VISIDOMI]            DATE          NULL,
    [DIAGNOINIC]          VARCHAR (50)  NULL,
    [INICIFIEBRE]         DATE          NULL,
    [TIPOERUP]            INT           NULL,
    [INICIERUP]           DATE          NULL,
    [DURAERUP]            VARCHAR (50)  NULL,
    [TOS]                 INT           NULL,
    [CORIZA]              INT           NULL,
    [CONJUNTIVI]          INT           NULL,
    [ADENOPATIA]          INT           NULL,
    [ARTRALGIA]           INT           NULL,
    [EMBARAZADA]          INT           NULL,
    [NUMSEMANAS]          VARCHAR (50)  NULL,
    [LUGAPROBA]           VARCHAR (50)  NULL,
    [HUBOCONTAC]          INT           NULL,
    [HUBOCASOCON]         INT           NULL,
    [VIAJODURANT]         INT           NULL,
    [ADONDE]              VARCHAR (50)  NULL,
    [TUVOCONTAC]          INT           NULL,
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
    [VALORREGIS1]         VARCHAR (50)  NULL,
    [VALORREGIS2]         VARCHAR (50)  NULL,
    [VALORREGIS3]         VARCHAR (50)  NULL,
    [VALORREGIS4]         VARCHAR (50)  NULL,
    [HUBOVACU]            INT           NULL,
    [HUBOMONITOR]         INT           NULL,
    [SEHIZOSEGUI]         INT           NULL,
    [CASOCONFIR]          INT           NULL,
    [CASOIMPOR]           VARCHAR (50)  NULL,
    [CASODESCART]         INT           NULL,
    [VACURUBEOLA]         INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA730] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA730_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA730_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA730] NOCHECK CONSTRAINT [CK_HCFICHA730_JSON];




GO
ALTER TABLE [dbo].[HCFICHA730] NOCHECK CONSTRAINT [CK_HCFICHA730_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campos JSON: columnas nuevas.   Desde versión "V02_2021-01-21" -->  - SOSPECHAMISC   - FUENTECONTAGIO   - FECHASEGUI   - RTPCR   - IGMIGG   - NEXOCOVID   - SINTOMASINI   - FIBRINOGESI   - PROTEINAC   - FERRITINA   - DIMEROD   - LINFOPENIA   - VALOR1   - VALOR2   - VALOR3   - VALOR4   - VALOR5   - CODPAIS_IMPORTADO: 3 caracteres alfanumericos    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la Ficha de Notificación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Vacuna contra la rubeola  1 = Si   2 = No   3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'VACURUBEOLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Si el caso es descartado, señale el criterio para descartar  1 = Laboratorio Negativo  2= Reacción vacunal 3 =Dengue   4 =Parvovirus B19      5 =Herpes 6.6    6 =Reacción Alérgica    7 = Otro Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'CASODESCART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda   Si el caso fue importado ¿De qué país?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'CASOIMPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Si el caso fue confirmado, señale fuente de infección   1 =Importado   2 = Relacionado con importación  3 =Fuente desconocida   4 =Autóctono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'CASOCONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda ¿Se hizo seguimiento a contactos?   1 = Si  2 = No  3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'SEHIZOSEGUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ¿Hubo monitoreo rápido de cobertura?  1 = Si  2 = No  3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'HUBOMONITOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ¿Hubo vacunación de bloqueo?   1 = Si  2 = No  3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'HUBOVACU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor registrado4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'VALORREGIS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor registrado3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'VALORREGIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor registrado2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'VALORREGIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor registrado1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'VALORREGIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del resultado4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHARESUL4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del resultado3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHARESUL3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del resultado2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHARESUL2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del resultado1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHARESUL1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado4     1 =Positivo   2 =  Negativo 3 = No Procesado   4 =Inadecuado   5 =Dudoso   6 = Valor Registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'RESULTADO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado3     1 =Positivo   2 =  Negativo 3 = No Procesado   4 =Inadecuado   5 =Dudoso   6 = Valor Registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'RESULTADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado2    1 =Positivo   2 =  Negativo 3 = No Procesado   4 =Inadecuado   5 =Dudoso   6 = Valor Registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado1     1 =Positivo   2 =  Negativo 3 = No Procesado   4 =Inadecuado   5 =Dudoso   6 = Valor Registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'RESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda agente4       1 =Sarampión    2 = Rubéola   3 =Dengue    4 =Citomegalovirus   5 =Herpes Virus   6= Parvovirus  7 =Chikungunya', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'AGENTE4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda agente3       1 =Sarampión    2 = Rubéola   3 =Dengue    4 =Citomegalovirus   5 =Herpes Virus   6= Parvovirus  7 =Chikungunya', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'AGENTE3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda agente2       1 =Sarampión    2 = Rubéola   3 =Dengue    4 =Citomegalovirus   5 =Herpes Virus   6= Parvovirus  7 =Chikungunya', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'AGENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda agente1       1 =Sarampión    2 = Rubéola   3 =Dengue    4 =Citomegalovirus   5 =Herpes Virus   6= Parvovirus  7 =Chikungunya', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'AGENTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la prueba4     1 =IgM   2 = IgG  3 = PCR   4 = Elisa  5 =Aislamiento Viral  6 = Pruebas genotípicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'PRUEBA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la prueba3     1 =IgM   2 = IgG  3 = PCR   4 = Elisa  5 =Aislamiento Viral  6 = Pruebas genotípicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'PRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la prueba2     1 =IgM   2 = IgG  3 = PCR   4 = Elisa  5 =Aislamiento Viral  6 = Pruebas genotípicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'PRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la prueba1     1 =IgM   2 = IgG  3 = PCR   4 = Elisa  5 =Aislamiento Viral  6 = Pruebas genotípicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'PRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Muestra4        1 =  Orina    2 = Hisopado Nasofaringeo    3 =Aspirado Nasofaringeo    4 = Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'MUESTRA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Muestra3        1 =  Orina    2 = Hisopado Nasofaringeo    3 =Aspirado Nasofaringeo    4 = Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'MUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Muestra2      1 =  Orina    2 = Hisopado Nasofaringeo    3 =Aspirado Nasofaringeo    4 = Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'MUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Muestra1        1 =  Orina    2 = Hisopado Nasofaringeo    3 =Aspirado Nasofaringeo    4 = Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'MUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de recepción4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHARECE4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de recepción3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHARECE3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de recepción2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHARECE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de recepción1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHARECE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de la toma4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHATOMA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de la toma3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de la toma2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de la toma1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda 7.5 ¿Tuvo contacto con una mujer embarazada entre 5 días antes del inicio y 7 días después del inicio de los síntomas?   1 =Si  2 =No  3 =Desconocido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'TUVOCONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda ¿A dónde?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'ADONDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  ¿Viajó durante los (7-23) días previos al inicio de la erupción?   1 = si    2 =  No  3 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'VIAJODURANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ¿Hubo algún caso confirmado de sarampión/rubéola en el área antes de este caso?   1 = Sarampión  2 =Rubéola   3 =Ambos  4 =Ninguno  5 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'HUBOCASOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda ¿Hubo contacto con otro caso de sarampión/rubéola (7-23) días antes de inicio de la erupción?   1 = Sarampión  2 =Rubéola   3 =Ambos  4 =Ninguno  5 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'HUBOCONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Lugar probable de parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'LUGAPROBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero de semanas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'NUMSEMANAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el embarazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'EMBARAZADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Artralgia 1 = Si   2 =No  3 =Desconocido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'ARTRALGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Adenopatia 1 = Si   2 =No  3 =Desconocido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'ADENOPATIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  ¿Conjuntivitis? 1 = Si  2= No 3= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'CONJUNTIVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la coriza  1 =Si 2 =No 3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'CORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la tos   1 = Si  2 = No  3 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'TOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Duración de la erupción (días)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'DURAERUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de  Inicio de erupción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'INICIERUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el tipo de erupción  1 = Máculo papular  2 =Vesicular  3 = Otro   4 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'TIPOERUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de inicio de fiebre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'INICIFIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Diagnóstico inicial CIE 10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'DIAGNOINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha  Visita domiciliaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'VISIDOMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Tipo de vacuna2:   1 =Sarampión S  2 =Sarampión rubéola SR  3 =Triple viral SRP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'TIPOVACU2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Tipo de vacuna1:   1 =Sarampión S  2 =Sarampión rubéola SR  3 =Triple viral SRP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'TIPOVACU1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fuente2:  1 = Carné  2 =Verbal  3 =Registro de salud o RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FUENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fuente1:  1 = Carné  2 =Verbal  3 =Registro de salud o RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'FUENTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de Última dosis 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'ULTIDOSIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de Última dosis 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'ULTIDOSIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la dosis2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'DOSIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la dosis 1 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'DOSIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda vacuna contra sarampión  1 =Si   2=No   3 = Desconocido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'VACUCONTSARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el caso detectado por :  1 =Consulta  2 =Laboratorio  3 =Búsqueda activa institucional  4 =Búsqueda activa comunitaria  5 = Investigación de contactos  6 =Comunidad 7=otros   8 = desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'CASODETECT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la dierección del trabajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'DIRETRABA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Ocupación del padre o de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'OCUPAPADMAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el nombre del padre o de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'NOMPADMAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica 730 para casos de sarampión y rubéola. Registra los datos clínicos, antecedentes de vacunación, síntomas, muestras de laboratorio y clasificación final del caso notificado, en cumplimiento de la vigilancia epidemiológica obligatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA730';
