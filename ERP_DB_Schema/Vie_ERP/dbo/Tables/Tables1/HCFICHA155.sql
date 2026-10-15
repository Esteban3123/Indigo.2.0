CREATE TABLE [dbo].[HCFICHA155] (
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [TIPOCANCER]          INT           NULL,
    [FECHAPROCEDI]        DATETIME      NULL,
    [FECHARESULT1]        DATETIME      NULL,
    [RESULBIOPSIA]        INT           NULL,
    [GRADOHISPA1]         INT           NULL,
    [FECHATOMAMUES]       DATE          NULL,
    [FECHARESULT2]        DATE          NULL,
    [BIOEXOTRANSF]        BIT           NULL,
    [RESULBIOPSIAEXO]     INT           NULL,
    [GRADOHISPA2]         INT           NULL,
    [BIOPSIAENDOCER]      BIT           NULL,
    [ADENOCARCINOMA]      INT           NULL,
    [GRADOHISPA3]         INT           NULL,
    [TRATAINITUMOR]       BIT           NULL,
    [TRADIOTERAPIA]       BIT           NULL,
    [TQUIRURGICO]         BIT           NULL,
    [TQUIMIOTERAPIA]      BIT           NULL,
    [THORMONTERAPIA]      BIT           NULL,
    [TCUIDADOSPALI]       BIT           NULL,
    [TINMUNOTERAPIA]      BIT           NULL,
    [FECHAINITRATA]       DATE          NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_HCFICHA115] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA155_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA115_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA155] NOCHECK CONSTRAINT [CK_HCFICHA155_JSON];


GO
ALTER TABLE [dbo].[HCFICHA155] NOCHECK CONSTRAINT [FK_HCFICHA115_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicio tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHAINITRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Tipos de tratamiento    si selecionan   Inmunoterapia= True   si no NULL      campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TINMUNOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Tipos de tratamiento    si selecionan   Cuidados paliativos= True   si no NULL      campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TCUIDADOSPALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Tipos de tratamiento    si selecionan   Hormonterapia= True   si no NULL      campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'THORMONTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Tipos de tratamiento    si selecionan   Quimioterapia= True   si no NULL      campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TQUIMIOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Tipos de tratamiento    si selecionan   Quirúrgico= True   si no NULL    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TQUIRURGICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos de tratamiento  si selecionan   Radioterapia = True   si no NULL    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TRADIOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tratamiento inicial del tumor  True = SiFalse = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TRATAINITUMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grado histopatológico  1=In-situ  2=Infiltrante  3=No indicado    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023   cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado biopsia endocervix:  Adenocarcinoma:  1= Positivo  2=Negativo  campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023   cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'ADENOCARCINOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Biopsia endocervix  True = Si  False = NO  campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023   cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'BIOPSIAENDOCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grado histopatológico  1=In-situ  2=Infiltrante  3=No indicado  4=Invasor / Infiltrante (Figo IA o IB2)  5=Invasor / Infiltrante (Figo >= IB3) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado biopsia exocervix  1= LEI AG NCIII / In situ  2= Carcinoma escamocelular  3=Adenocarcinoma o mixtos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'RESULBIOPSIAEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Biopsia exocervix y zona de transformación  True = Si   Fasle = No     campo queda obsoleto con versionamiento V:03 2023-04-01 desde 26/10/2023 cambios pedidos en PBI 12155, en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'BIOEXOTRANSF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHARESULT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha toma de muestra ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grado histopatológico   1=In-situ  2=Infiltrante  3=No indicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'GRADOHISPA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado biopsia  1=Carcioma ductal    2=Carcioma lobulillar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'RESULBIOPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHARESULT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'FECHAPROCEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de cancer  1: CA Mama  2: CA Cuello uterino  3: Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA155', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';

