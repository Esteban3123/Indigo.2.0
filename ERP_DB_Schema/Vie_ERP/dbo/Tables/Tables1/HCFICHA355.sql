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
ALTER TABLE [dbo].[HCFICHA355] NOCHECK CONSTRAINT [FK_HCFICHA355_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otra:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'OTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sangre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'SANGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vomito:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VOMITO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Heces:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HECES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuál otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CUALOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente identificado:   1=Coliformes fecales   2=Coliformes totales   3=Bacilus cereus   4=Bacilus anthracis   5=Staphylococcus aureus   6=Streptococcus sp   7=Clostridium perfringens   8=Aeromonas hydrophila   9=Campylobacter jejuni   10=Escherichia coli   11=Shigella sp   12=Salmonella spp   13=Salmonella Typhi   14=Salmonella Paratyphi   15=Clostridiumbotulinum   16=Vibrio sp   17=Vibrio parahaemolyticus   18=Brucella abortus   19=Mycobacterium bovis   20=Listeria monocytogenes   21=Proteus sp   22=Norovirus   23=Rotavirus   24=Parvovirus   25=Astrovirus   26=Adenovirus   27=Hepatitis E   28=Ascaris lumbricoides   29=Complejo Entamoeba histolytica/dispar   30=Fasciola hepática   31=Taenia saginata  32=Cyclospora   33=Giardia duodenalis   34=Taenia solium   35=Trichinella spiralis   36=Balantidium coli   37=Cryptosporidium   38=Isospora belli   39=Trichuris trichiura   40=Uncinarias   41=Enterobius vermicularis   42=Strongyloides stercolaris   43=Hymenolepis nana   44=Hymenolepis diminuta   45=Dipylidium caninum   46=Entamoeba hartamanni   47=Entamoeba coli   48=Endolimax nana   49=Lodamoeba butschlii   50=Chilomastix mesnili   51=Trichomonas hominis   52=Antimonio   53=Cadmio   54=Cobre   55=Fluoruro   56=Plomo   57=Estaño   58=Zinc   59=Nitritos o Nitratos   60=Cloruros   61=Hidroxido de sodio   62=Organofosforados   63=Carbamatos   64=Acido okadaico   65=Saxitoxina   66=Alcaloides   67=Hidrocarburo clorado   68=Mercurio   69=Fosfato de triortocresilo   70=Glutamatomonosodico   71=Micotinato sódico   72=Vibrio cholerae O1 no toxigénico  ---> item eliminado desde version ''V01_2020-03-06''   73=Vibrio cholerae O1 toxigénico ---> item eliminado desde version ''V01_2020-03-06''   74=Vibrio cholerae O139 ---> item eliminado desde version ''V01_2020-03-06''   75=Vibrio cholerae no O1, noO139 no toxigénico ---> item eliminado desde version ''V01_2020-03-06''   76=Vibrio cholerae no O1, no O139 toxigénico ---> item eliminado desde version ''V01_2020-03-06''   77=T-cruzi ---> item eliminado desde version ''V01_2020-03-06''    78=Otro   79=Pendiente   80=No detectado   81 = Hepatitis A ---> item nuevo desde version ''V01_2020-03-06''    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente identificado:   1=Coliformes fecales   2=Coliformes totales   3=Bacilus cereus   4=Bacilus anthracis   5=Staphylococcus aureus   6=Streptococcus sp   7=Clostridium perfringens   8=Aeromonas hydrophila   9=Campylobacter jejuni   10=Escherichia coli   11=Shigella sp   12=Salmonella spp   13=Salmonella Typhi   14=Salmonella Paratyphi   15=Clostridiumbotulinum   16=Vibrio sp   17=Vibrio parahaemolyticus   18=Brucella abortus   19=Mycobacterium bovis   20=Listeria monocytogenes   21=Proteus sp   22=Norovirus   23=Rotavirus   24=Parvovirus   25=Astrovirus   26=Adenovirus   27=Hepatitis E   28=Ascaris lumbricoides   29=Complejo Entamoeba histolytica/dispar   30=Fasciola hepática   31=Taenia saginata  32=Cyclospora   33=Giardia duodenalis   34=Taenia solium   35=Trichinella spiralis   36=Balantidium coli   37=Cryptosporidium   38=Isospora belli   39=Trichuris trichiura   40=Uncinarias   41=Enterobius vermicularis   42=Strongyloides stercolaris   43=Hymenolepis nana   44=Hymenolepis diminuta   45=Dipylidium caninum   46=Entamoeba hartamanni   47=Entamoeba coli   48=Endolimax nana   49=Lodamoeba butschlii   50=Chilomastix mesnili   51=Trichomonas hominis   52=Antimonio   53=Cadmio   54=Cobre   55=Fluoruro   56=Plomo   57=Estaño   58=Zinc   59=Nitritos o Nitratos   60=Cloruros   61=Hidroxido de sodio   62=Organofosforados   63=Carbamatos   64=Acido okadaico   65=Saxitoxina   66=Alcaloides   67=Hidrocarburo clorado   68=Mercurio   69=Fosfato de triortocresilo   70=Glutamatomonosodico   71=Micotinato sódico   72=Vibrio cholerae O1 no toxigénico  ---> item eliminado desde version ''V01_2020-03-06''   73=Vibrio cholerae O1 toxigénico ---> item eliminado desde version ''V01_2020-03-06''   74=Vibrio cholerae O139 ---> item eliminado desde version ''V01_2020-03-06''   75=Vibrio cholerae no O1, noO139 no toxigénico ---> item eliminado desde version ''V01_2020-03-06''   76=Vibrio cholerae no O1, no O139 toxigénico ---> item eliminado desde version ''V01_2020-03-06''   77=T-cruzi ---> item eliminado desde version ''V01_2020-03-06''    78=Otro   79=Pendiente   80=No detectado   81 = Hepatitis A ---> item nuevo desde version ''V01_2020-03-06''    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente identificado:   1=Coliformes fecales   2=Coliformes totales   3=Bacilus cereus   4=Bacilus anthracis   5=Staphylococcus aureus   6=Streptococcus sp   7=Clostridium perfringens   8=Aeromonas hydrophila   9=Campylobacter jejuni   10=Escherichia coli   11=Shigella sp   12=Salmonella spp   13=Salmonella Typhi   14=Salmonella Paratyphi   15=Clostridiumbotulinum   16=Vibrio sp   17=Vibrio parahaemolyticus   18=Brucella abortus   19=Mycobacterium bovis   20=Listeria monocytogenes   21=Proteus sp   22=Norovirus   23=Rotavirus   24=Parvovirus   25=Astrovirus   26=Adenovirus   27=Hepatitis E   28=Ascaris lumbricoides   29=Complejo Entamoeba histolytica/dispar   30=Fasciola hepática   31=Taenia saginata  32=Cyclospora   33=Giardia duodenalis   34=Taenia solium   35=Trichinella spiralis   36=Balantidium coli   37=Cryptosporidium   38=Isospora belli   39=Trichuris trichiura   40=Uncinarias   41=Enterobius vermicularis   42=Strongyloides stercolaris   43=Hymenolepis nana   44=Hymenolepis diminuta   45=Dipylidium caninum   46=Entamoeba hartamanni   47=Entamoeba coli   48=Endolimax nana   49=Lodamoeba butschlii   50=Chilomastix mesnili   51=Trichomonas hominis   52=Antimonio   53=Cadmio   54=Cobre   55=Fluoruro   56=Plomo   57=Estaño   58=Zinc   59=Nitritos o Nitratos   60=Cloruros   61=Hidroxido de sodio   62=Organofosforados   63=Carbamatos   64=Acido okadaico   65=Saxitoxina   66=Alcaloides   67=Hidrocarburo clorado   68=Mercurio   69=Fosfato de triortocresilo   70=Glutamatomonosodico   71=Micotinato sódico   72=Vibrio cholerae O1 no toxigénico  ---> item eliminado desde version ''V01_2020-03-06''   73=Vibrio cholerae O1 toxigénico ---> item eliminado desde version ''V01_2020-03-06''   74=Vibrio cholerae O139 ---> item eliminado desde version ''V01_2020-03-06''   75=Vibrio cholerae no O1, noO139 no toxigénico ---> item eliminado desde version ''V01_2020-03-06''   76=Vibrio cholerae no O1, no O139 toxigénico ---> item eliminado desde version ''V01_2020-03-06''   77=T-cruzi ---> item eliminado desde version ''V01_2020-03-06''    78=Otro   79=Pendiente   80=No detectado   81 = Hepatitis A ---> item nuevo desde version ''V01_2020-03-06''    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente identificado:   1=Coliformes fecales   2=Coliformes totales   3=Bacilus cereus   4=Bacilus anthracis   5=Staphylococcus aureus   6=Streptococcus sp   7=Clostridium perfringens   8=Aeromonas hydrophila   9=Campylobacter jejuni   10=Escherichia coli   11=Shigella sp   12=Salmonella spp   13=Salmonella Typhi   14=Salmonella Paratyphi   15=Clostridiumbotulinum   16=Vibrio sp   17=Vibrio parahaemolyticus   18=Brucella abortus   19=Mycobacterium bovis   20=Listeria monocytogenes   21=Proteus sp   22=Norovirus   23=Rotavirus   24=Parvovirus   25=Astrovirus   26=Adenovirus   27=Hepatitis E   28=Ascaris lumbricoides   29=Complejo Entamoeba histolytica/dispar   30=Fasciola hepática   31=Taenia saginata  32=Cyclospora   33=Giardia duodenalis   34=Taenia solium   35=Trichinella spiralis   36=Balantidium coli   37=Cryptosporidium   38=Isospora belli   39=Trichuris trichiura   40=Uncinarias   41=Enterobius vermicularis   42=Strongyloides stercolaris   43=Hymenolepis nana   44=Hymenolepis diminuta   45=Dipylidium caninum   46=Entamoeba hartamanni   47=Entamoeba coli   48=Endolimax nana   49=Lodamoeba butschlii   50=Chilomastix mesnili   51=Trichomonas hominis   52=Antimonio   53=Cadmio   54=Cobre   55=Fluoruro   56=Plomo   57=Estaño   58=Zinc   59=Nitritos o Nitratos   60=Cloruros   61=Hidroxido de sodio   62=Organofosforados   63=Carbamatos   64=Acido okadaico   65=Saxitoxina   66=Alcaloides   67=Hidrocarburo clorado   68=Mercurio   69=Fosfato de triortocresilo   70=Glutamatomonosodico   71=Micotinato sódico   72=Vibrio cholerae O1 no toxigénico  ---> item eliminado desde version ''V01_2020-03-06''   73=Vibrio cholerae O1 toxigénico ---> item eliminado desde version ''V01_2020-03-06''   74=Vibrio cholerae O139 ---> item eliminado desde version ''V01_2020-03-06''   75=Vibrio cholerae no O1, noO139 no toxigénico ---> item eliminado desde version ''V01_2020-03-06''   76=Vibrio cholerae no O1, no O139 toxigénico ---> item eliminado desde version ''V01_2020-03-06''   77=T-cruzi ---> item eliminado desde version ''V01_2020-03-06''    78=Otro   79=Pendiente   80=No detectado   81 = Hepatitis A ---> item nuevo desde version ''V01_2020-03-06''    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'AGENIDENT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se recolectó muestra biológica=   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'RECOMUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relación con la exposición:   True=Comensal   False=Manipulador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'RELAEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Caso captado por:   True=UPGD   False=Búsqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CASOCAPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Caso asociado a un brote:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CASOASOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DIRECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del lugar de consumo implicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMLUG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORACON1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lugar del consumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LUGCONSU1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del alimento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NOMALIME1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de inicio de los síntomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'HORAINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marcó otros, registre cual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MARCOTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'OTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Espasmos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ESPASMOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sialorrea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'SIALORREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parestesias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Escalofrio:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ESCALOFRIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lesiones:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'LESIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mareo:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MAREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Artralgias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mialgias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cianosis:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CIANOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deshidrata:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DESHIDRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cefalea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Calambres:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CALAMBRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fiebre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diarrea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vomito:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nauseas:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'NAUSEAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA355', @level2type = N'COLUMN', @level2name = N'ID';

