CREATE TABLE [dbo].[HCFICHA355] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NAUSEAS]             BIT           NULL,
    [VOMITO]              BIT           NULL,
    [DIARREA]             BIT           NULL,
    [FIEBRE]              BIT           NULL,
    [CALAMBRES]           BIT           NULL,
    [CEFALEA]             BIT           NULL,
    [DESHIDRATA]          BIT           NULL,
    [CIANOSIS]            BIT           NULL,
    [MIALGIAS]            BIT           NULL,
    [ARTRALGIAS]          BIT           NULL,
    [MAREO]               BIT           NULL,
    [LESIONES]            BIT           NULL,
    [ESCALOFRIO]          BIT           NULL,
    [PARESTESIAS]         BIT           NULL,
    [SIALORREA]           BIT           NULL,
    [ESPASMOS]            BIT           NULL,
    [OTROS]               BIT           NULL,
    [MARCOTROS]           VARCHAR (MAX) NULL,
    [HORAINICIO]          DATETIME      NULL,
    [NOMALIME1]           VARCHAR (50)  NULL,
    [NOMALIME2]           VARCHAR (50)  NULL,
    [NOMALIME3]           VARCHAR (50)  NULL,
    [NOMALIME4]           VARCHAR (50)  NULL,
    [NOMALIME5]           VARCHAR (50)  NULL,
    [NOMALIME6]           VARCHAR (50)  NULL,
    [NOMALIME7]           VARCHAR (50)  NULL,
    [NOMALIME8]           VARCHAR (50)  NULL,
    [NOMALIME9]           VARCHAR (50)  NULL,
    [LUGCONSU1]           VARCHAR (MAX) NULL,
    [LUGCONSU2]           VARCHAR (MAX) NULL,
    [LUGCONSU3]           VARCHAR (MAX) NULL,
    [LUGCONSU4]           VARCHAR (MAX) NULL,
    [LUGCONSU5]           VARCHAR (MAX) NULL,
    [LUGCONSU6]           VARCHAR (MAX) NULL,
    [LUGCONSU7]           VARCHAR (MAX) NULL,
    [LUGCONSU8]           VARCHAR (MAX) NULL,
    [LUGCONSU9]           VARCHAR (MAX) NULL,
    [HORACON1]            DATETIME      NULL,
    [HORACON2]            DATETIME      NULL,
    [HORACON3]            DATETIME      NULL,
    [HORACON4]            DATETIME      NULL,
    [HORACON5]            DATETIME      NULL,
    [HORACON6]            DATETIME      NULL,
    [HORACON7]            DATETIME      NULL,
    [HORACON8]            DATETIME      NULL,
    [HORACON9]            DATETIME      NULL,
    [NOMLUG]              VARCHAR (MAX) NULL,
    [DIRECCION]           VARCHAR (MAX) NULL,
    [CASOASOC]            BIT           NULL,
    [CASOCAPT]            BIT           NULL,
    [RELAEXPO]            BIT           NULL,
    [RECOMUES]            BIT           NULL,
    [CUAL]                VARCHAR (MAX) NULL,
    [AGENIDENT1]          INT           NULL,
    [AGENIDENT2]          INT           NULL,
    [AGENIDENT3]          INT           NULL,
    [AGENIDENT4]          INT           NULL,
    [CUALOTRO]            VARCHAR (MAX) NULL,
    [HECES]               BIT           NULL,
    [VOMITO2]             BIT           NULL,
    [SANGRE]              BIT           NULL,
    [OTRA]                BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA355] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA355_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA355_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA355] NOCHECK CONSTRAINT [CK_HCFICHA355_JSON];




GO
ALTER TABLE [dbo].[HCFICHA355] NOCHECK CONSTRAINT [CK_HCFICHA355_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido adicional en formato JSON; VARCHAR(MAX), validado con CHECK ISJSON; almacena nuevas columnas y extensiones de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación; VARCHAR(20); nulo indica primera versión; control de cambios en formulario (ej: V01_2020-03-06)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otra sustancia o agente presente; BIT (1=Sí, 0=No); indicador binario de síntomas o exposiciones adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'OTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otra:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'OTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'OTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de sangre en deposiciones o vómito; BIT (1=Sí, 0=No); hallazgo clínico en intoxicación o ETA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'SANGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sangre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'SANGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'SANGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vómito presente en el paciente; BIT (1=Sí, 0=No); síntoma gastrointestinal secundario en vigilancia ETA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VOMITO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vomito:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VOMITO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VOMITO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alteración en heces (sangre, moco, aspecto); BIT (1=Sí, 0=No); manifestación clínica de brote por alimentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HECES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Heces:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HECES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HECES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otro agente, sustancia o síntoma; VARCHAR(MAX); texto libre cuando se marca opción ''''Otro''''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CUALOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuál otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CUALOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CUALOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarto agente etiológico identificado (laboratorio); INT; catálogo 1-81 (bacterias, virus, parásitos, químicos, Hepatitis A desde V01_2020-03-06); permite hasta 4 agentes por caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente identificado:   1=Coliformes fecales   2=Coliformes totales   3=Bacilus cereus   4=Bacilus anthracis   5=Staphylococcus aureus   6=Streptococcus sp   7=Clostridium perfringens   8=Aeromonas hydrophila   9=Campylobacter jejuni   10=Escherichia coli   11=Shigella sp   12=Salmonella spp   13=Salmonella Typhi   14=Salmonella Paratyphi   15=Clostridiumbotulinum   16=Vibrio sp   17=Vibrio parahaemolyticus   18=Brucella abortus   19=Mycobacterium bovis   20=Listeria monocytogenes   21=Proteus sp   22=Norovirus   23=Rotavirus   24=Parvovirus   25=Astrovirus   26=Adenovirus   27=Hepatitis E   28=Ascaris lumbricoides   29=Complejo Entamoeba histolytica/dispar   30=Fasciola hepática   31=Taenia saginata  32=Cyclospora   33=Giardia duodenalis   34=Taenia solium   35=Trichinella spiralis   36=Balantidium coli   37=Cryptosporidium   38=Isospora belli   39=Trichuris trichiura   40=Uncinarias   41=Enterobius vermicularis   42=Strongyloides stercolaris   43=Hymenolepis nana   44=Hymenolepis diminuta   45=Dipylidium caninum   46=Entamoeba hartamanni   47=Entamoeba coli   48=Endolimax nana   49=Lodamoeba butschlii   50=Chilomastix mesnili   51=Trichomonas hominis   52=Antimonio   53=Cadmio   54=Cobre   55=Fluoruro   56=Plomo   57=Estaño   58=Zinc   59=Nitritos o Nitratos   60=Cloruros   61=Hidroxido de sodio   62=Organofosforados   63=Carbamatos   64=Acido okadaico   65=Saxitoxina   66=Alcaloides   67=Hidrocarburo clorado   68=Mercurio   69=Fosfato de triortocresilo   70=Glutamatomonosodico   71=Micotinato sódico   72=Vibrio cholerae O1 no toxigénico  ---> item eliminado desde version ''''V01_2020-03-06''''   73=Vibrio cholerae O1 toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   74=Vibrio cholerae O139 ---> item eliminado desde version ''''V01_2020-03-06''''   75=Vibrio cholerae no O1, noO139 no toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   76=Vibrio cholerae no O1, no O139 toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   77=T-cruzi ---> item eliminado desde version ''''V01_2020-03-06''''    78=Otro   79=Pendiente   80=No detectado   81 = Hepatitis A ---> item nuevo desde version ''''V01_2020-03-06''''    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercer agente etiológico identificado (laboratorio); INT; catálogo 1-81 (Coliformes, Salmonella, Vibrio, Norovirus, Rotavirus, Hepatitis A, pesticidas, metales); multiple identification support', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente identificado:   1=Coliformes fecales   2=Coliformes totales   3=Bacilus cereus   4=Bacilus anthracis   5=Staphylococcus aureus   6=Streptococcus sp   7=Clostridium perfringens   8=Aeromonas hydrophila   9=Campylobacter jejuni   10=Escherichia coli   11=Shigella sp   12=Salmonella spp   13=Salmonella Typhi   14=Salmonella Paratyphi   15=Clostridiumbotulinum   16=Vibrio sp   17=Vibrio parahaemolyticus   18=Brucella abortus   19=Mycobacterium bovis   20=Listeria monocytogenes   21=Proteus sp   22=Norovirus   23=Rotavirus   24=Parvovirus   25=Astrovirus   26=Adenovirus   27=Hepatitis E   28=Ascaris lumbricoides   29=Complejo Entamoeba histolytica/dispar   30=Fasciola hepática   31=Taenia saginata  32=Cyclospora   33=Giardia duodenalis   34=Taenia solium   35=Trichinella spiralis   36=Balantidium coli   37=Cryptosporidium   38=Isospora belli   39=Trichuris trichiura   40=Uncinarias   41=Enterobius vermicularis   42=Strongyloides stercolaris   43=Hymenolepis nana   44=Hymenolepis diminuta   45=Dipylidium caninum   46=Entamoeba hartamanni   47=Entamoeba coli   48=Endolimax nana   49=Lodamoeba butschlii   50=Chilomastix mesnili   51=Trichomonas hominis   52=Antimonio   53=Cadmio   54=Cobre   55=Fluoruro   56=Plomo   57=Estaño   58=Zinc   59=Nitritos o Nitratos   60=Cloruros   61=Hidroxido de sodio   62=Organofosforados   63=Carbamatos   64=Acido okadaico   65=Saxitoxina   66=Alcaloides   67=Hidrocarburo clorado   68=Mercurio   69=Fosfato de triortocresilo   70=Glutamatomonosodico   71=Micotinato sódico   72=Vibrio cholerae O1 no toxigénico  ---> item eliminado desde version ''''V01_2020-03-06''''   73=Vibrio cholerae O1 toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   74=Vibrio cholerae O139 ---> item eliminado desde version ''''V01_2020-03-06''''   75=Vibrio cholerae no O1, noO139 no toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   76=Vibrio cholerae no O1, no O139 toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   77=T-cruzi ---> item eliminado desde version ''''V01_2020-03-06''''    78=Otro   79=Pendiente   80=No detectado   81 = Hepatitis A ---> item nuevo desde version ''''V01_2020-03-06''''    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo agente etiológico identificado (laboratorio); INT; catálogo 1-81; incluye microorganismos y tóxicos químicos en intoxicación alimentaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente identificado:   1=Coliformes fecales   2=Coliformes totales   3=Bacilus cereus   4=Bacilus anthracis   5=Staphylococcus aureus   6=Streptococcus sp   7=Clostridium perfringens   8=Aeromonas hydrophila   9=Campylobacter jejuni   10=Escherichia coli   11=Shigella sp   12=Salmonella spp   13=Salmonella Typhi   14=Salmonella Paratyphi   15=Clostridiumbotulinum   16=Vibrio sp   17=Vibrio parahaemolyticus   18=Brucella abortus   19=Mycobacterium bovis   20=Listeria monocytogenes   21=Proteus sp   22=Norovirus   23=Rotavirus   24=Parvovirus   25=Astrovirus   26=Adenovirus   27=Hepatitis E   28=Ascaris lumbricoides   29=Complejo Entamoeba histolytica/dispar   30=Fasciola hepática   31=Taenia saginata  32=Cyclospora   33=Giardia duodenalis   34=Taenia solium   35=Trichinella spiralis   36=Balantidium coli   37=Cryptosporidium   38=Isospora belli   39=Trichuris trichiura   40=Uncinarias   41=Enterobius vermicularis   42=Strongyloides stercolaris   43=Hymenolepis nana   44=Hymenolepis diminuta   45=Dipylidium caninum   46=Entamoeba hartamanni   47=Entamoeba coli   48=Endolimax nana   49=Lodamoeba butschlii   50=Chilomastix mesnili   51=Trichomonas hominis   52=Antimonio   53=Cadmio   54=Cobre   55=Fluoruro   56=Plomo   57=Estaño   58=Zinc   59=Nitritos o Nitratos   60=Cloruros   61=Hidroxido de sodio   62=Organofosforados   63=Carbamatos   64=Acido okadaico   65=Saxitoxina   66=Alcaloides   67=Hidrocarburo clorado   68=Mercurio   69=Fosfato de triortocresilo   70=Glutamatomonosodico   71=Micotinato sódico   72=Vibrio cholerae O1 no toxigénico  ---> item eliminado desde version ''''V01_2020-03-06''''   73=Vibrio cholerae O1 toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   74=Vibrio cholerae O139 ---> item eliminado desde version ''''V01_2020-03-06''''   75=Vibrio cholerae no O1, noO139 no toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   76=Vibrio cholerae no O1, no O139 toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   77=T-cruzi ---> item eliminado desde version ''''V01_2020-03-06''''    78=Otro   79=Pendiente   80=No detectado   81 = Hepatitis A ---> item nuevo desde version ''''V01_2020-03-06''''    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer agente etiológico identificado (laboratorio); INT; catálogo 1-81 (bacterias entéricas, virus GI, parásitos, xenobióticos); búsqueda: Escherichia coli, Salmonella, Vibrio, mercurio, plomo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente identificado:   1=Coliformes fecales   2=Coliformes totales   3=Bacilus cereus   4=Bacilus anthracis   5=Staphylococcus aureus   6=Streptococcus sp   7=Clostridium perfringens   8=Aeromonas hydrophila   9=Campylobacter jejuni   10=Escherichia coli   11=Shigella sp   12=Salmonella spp   13=Salmonella Typhi   14=Salmonella Paratyphi   15=Clostridiumbotulinum   16=Vibrio sp   17=Vibrio parahaemolyticus   18=Brucella abortus   19=Mycobacterium bovis   20=Listeria monocytogenes   21=Proteus sp   22=Norovirus   23=Rotavirus   24=Parvovirus   25=Astrovirus   26=Adenovirus   27=Hepatitis E   28=Ascaris lumbricoides   29=Complejo Entamoeba histolytica/dispar   30=Fasciola hepática   31=Taenia saginata  32=Cyclospora   33=Giardia duodenalis   34=Taenia solium   35=Trichinella spiralis   36=Balantidium coli   37=Cryptosporidium   38=Isospora belli   39=Trichuris trichiura   40=Uncinarias   41=Enterobius vermicularis   42=Strongyloides stercolaris   43=Hymenolepis nana   44=Hymenolepis diminuta   45=Dipylidium caninum   46=Entamoeba hartamanni   47=Entamoeba coli   48=Endolimax nana   49=Lodamoeba butschlii   50=Chilomastix mesnili   51=Trichomonas hominis   52=Antimonio   53=Cadmio   54=Cobre   55=Fluoruro   56=Plomo   57=Estaño   58=Zinc   59=Nitritos o Nitratos   60=Cloruros   61=Hidroxido de sodio   62=Organofosforados   63=Carbamatos   64=Acido okadaico   65=Saxitoxina   66=Alcaloides   67=Hidrocarburo clorado   68=Mercurio   69=Fosfato de triortocresilo   70=Glutamatomonosodico   71=Micotinato sódico   72=Vibrio cholerae O1 no toxigénico  ---> item eliminado desde version ''''V01_2020-03-06''''   73=Vibrio cholerae O1 toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   74=Vibrio cholerae O139 ---> item eliminado desde version ''''V01_2020-03-06''''   75=Vibrio cholerae no O1, noO139 no toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   76=Vibrio cholerae no O1, no O139 toxigénico ---> item eliminado desde version ''''V01_2020-03-06''''   77=T-cruzi ---> item eliminado desde version ''''V01_2020-03-06''''    78=Otro   79=Pendiente   80=No detectado   81 = Hepatitis A ---> item nuevo desde version ''''V01_2020-03-06''''    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle o especificación de la respuesta anterior; VARCHAR(MAX); texto libre complementario a opciones cerradas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se recolectó muestra biológica para análisis; BIT (1=Sí, 0=No); indicador de muestras de heces, vómito o alimento en vigilancia epidemiológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'RECOMUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se recolectó muestra biológica=   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'RECOMUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'RECOMUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación del caso con exposición alimentaria; BIT (1=Comensal, 0=Manipulador); diferencia consumidor de manejador de alimentos en brote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'RELAEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la exposición:   True=Comensal   False=Manipulador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'RELAEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'RELAEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de captación del caso; BIT (1=UPGD, 0=Búsqueda); indica si notificación proviene de unidad generadora de datos o búsqueda activa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CASOCAPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caso captado por:   True=UPGD   False=Búsqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CASOCAPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CASOCAPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caso asociado a brote confirmado; BIT (1=Sí, 0=No); flag de vinculación a brote epidemiológico de intoxicación o ETA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CASOASOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caso asociado a un brote:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CASOASOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CASOASOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección domiciliaria o del lugar de exposición; VARCHAR(MAX); ubicación geográfica para trazabilidad y búsqueda de contactos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DIRECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del establecimiento o lugar de consumo implicado; VARCHAR(MAX); restaurante, comedor, mercado, hogar; búsqueda: sitio de exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMLUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del lugar de consumo implicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMLUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMLUG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del noveno consumo de alimento implicado; DATETIME; timestamp para reconstrucción de periodo de incubación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del octavo consumo de alimento implicado; DATETIME; permite rastrear múltiples exposiciones en brote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del séptimo consumo de alimento implicado; DATETIME; cronología de ingesta en intoxicación alimentaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del sexto consumo de alimento implicado; DATETIME; útil para análisis de dosis-respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del quinto consumo de alimento implicado; DATETIME; permite mapeo de exposiciones múltiples', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del cuarto consumo de alimento implicado; DATETIME; apoyo a epidemiología descriptiva de brote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del tercer consumo de alimento implicado; DATETIME; reconstrucción de línea de tiempo de exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del segundo consumo de alimento implicado; DATETIME; vigilancia de exposición repetida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del primer consumo de alimento implicado; DATETIME; inicio de periodo de exposición en ETA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar de consumo del noveno alimento; VARCHAR(MAX); ubicación de exposición para seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar de consumo del octavo alimento; VARCHAR(MAX); sitio de ingesta en caso de múltiples locales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar de consumo del séptimo alimento; VARCHAR(MAX); localización de comida consumida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar de consumo del sexto alimento; VARCHAR(MAX); establecimiento de consumo de alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar de consumo del quinto alimento; VARCHAR(MAX); punto de ingesta registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar de consumo del cuarto alimento; VARCHAR(MAX); ubicación en caso de exposición múltiple', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar de consumo del tercer alimento; VARCHAR(MAX); sitio de comida relacionado con brote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar de consumo del segundo alimento; VARCHAR(MAX); localización de second exposure', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar de consumo del primer alimento; VARCHAR(MAX); sitio principal de exposición alimentaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del noveno alimento consumido; VARCHAR(50); identificación de producto en intoxicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del octavo alimento consumido; VARCHAR(50); alimento consumido en brote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del séptimo alimento consumido; VARCHAR(50); producto alimenticio relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del sexto alimento consumido; VARCHAR(50); comida ingerida en exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del quinto alimento consumido; VARCHAR(50); alimento potencialmente contaminado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del cuarto alimento consumido; VARCHAR(50); producto consumido antes de síntomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del tercer alimento consumido; VARCHAR(50); comida ingerida en evento de exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del segundo alimento consumido; VARCHAR(50); producto alimenticio en historial de ingesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del primer alimento consumido; VARCHAR(50); alimento principal en exposición epidemiológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de síntomas; DATETIME; onset para cálculo de período de incubación en ETA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORAINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de inicio de los síntomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORAINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORAINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción cuando marca síntomas ''''Otros''''; VARCHAR(MAX); síntomas adicionales no incluidos en lista estándar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MARCOTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marcó otros, registre cual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MARCOTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MARCOTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de otros síntomas no listados; BIT (1=Sí, 0=No); síntomas adicionales en cuadro clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'OTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'OTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'OTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Espasmos musculares presentes; BIT (1=Sí, 0=No); síntoma de toxina botulínica o envenenamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ESPASMOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Espasmos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ESPASMOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ESPASMOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salivación excesiva presente; BIT (1=Sí, 0=No); manifestación de intoxicación o infección parasitaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'SIALORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sialorrea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'SIALORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'SIALORREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensación de hormigueo o entumecimiento; BIT (1=Sí, 0=No); síntoma neurológico en toxinas marinas o químicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parestesias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escalofríos presentes; BIT (1=Sí, 0=No); síntoma sistémico en infecciones por ETA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ESCALOFRIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Escalofrio:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ESCALOFRIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ESCALOFRIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lesiones de piel o mucosas presentes; BIT (1=Sí, 0=No); manifestación dermatológica en brote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LESIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lesiones:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LESIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LESIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mareo o vértigo presente; BIT (1=Sí, 0=No); síntoma neurológico en intoxicación alimentaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MAREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mareo:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MAREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MAREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dolor articular presente; BIT (1=Sí, 0=No); síntoma en Hepatitis A o infecciones virales por alimentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Artralgias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dolor muscular presente; BIT (1=Sí, 0=No); manifestación sistémica en Trichinella o virus entéricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mialgias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coloración azulada de piel y mucosas; BIT (1=Sí, 0=No); signo de hipoxia en envenenamiento químico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CIANOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cianosis:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CIANOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CIANOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signos clínicos de deshidratación; BIT (1=Sí, 0=No); complicación frecuente en gastroenteritis por ETA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DESHIDRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deshidrata:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DESHIDRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DESHIDRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dolor de cabeza presente; BIT (1=Sí, 0=No); síntoma común en brotes de intoxicación alimentaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cefalea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calambres abdominales presentes; BIT (1=Sí, 0=No); dolor cólico en gastroenteritis infecciosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CALAMBRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Calambres:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CALAMBRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CALAMBRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura elevada presente; BIT (1=Sí, 0=No); síntoma sistémico en infecciones bacterianas por alimentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fiebre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evacuaciones líquidas presentes; BIT (1=Sí, 0=No); síntoma principal en ETA, indicador de gravedad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diarrea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vómito presente en el paciente; BIT (1=Sí, 0=No); síntoma gastrointestinal superior en intoxicación alimentaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vomito:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Náuseas presentes; BIT (1=Sí, 0=No); síntoma prodrómal en ETA y envenenamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NAUSEAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nauseas:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NAUSEAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NAUSEAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10; CHAR(4); clasificación nosológica de la enfermedad transmitida por alimentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de ficha de notificación RIPS; INT; FK a HCFICHANOTIFICACION; referencia de caso notificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental; INT PRIMARY KEY IDENTITY; clave primaria de detalle de síntomas y exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación 355 para eventos de Enfermedad Transmitida por Alimentos (ETA). Registra los síntomas presentados, los alimentos consumidos sospechosos, los lugares y horarios de consumo, y los agentes identificados como posibles causantes del brote, en el contexto de vigilancia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355';
