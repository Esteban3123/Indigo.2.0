CREATE TABLE [dbo].[HCFICHA200] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [DESPLAZAMIENTO]      BIT           NULL,
    [DEPART1]             VARCHAR (50)  NULL,
    [DEPART2]             VARCHAR (50)  NULL,
    [DEPART3]             VARCHAR (50)  NULL,
    [DEPART4]             VARCHAR (50)  NULL,
    [CIUDAD1]             VARCHAR (50)  NULL,
    [CIUDAD2]             VARCHAR (50)  NULL,
    [CIUDAD3]             VARCHAR (50)  NULL,
    [CIUDAD4]             VARCHAR (50)  NULL,
    [PAIS1]               VARCHAR (50)  NULL,
    [PAIS2]               VARCHAR (50)  NULL,
    [PAIS3]               VARCHAR (50)  NULL,
    [PAIS4]               VARCHAR (50)  NULL,
    [FECHDESP1]           DATE          NULL,
    [FECHDESP2]           DATE          NULL,
    [FECHDESP3]           DATE          NULL,
    [FECHDESP4]           DATE          NULL,
    [FECHLLEG1]           DATE          NULL,
    [FECHLLEG2]           DATE          NULL,
    [FECHLLEG3]           DATE          NULL,
    [FECHLLEG4]           DATE          NULL,
    [HECHOCONT]           BIT           NULL,
    [CONQUIEN]            VARCHAR (50)  NULL,
    [CASOASOC]            BIT           NULL,
    [CASOCAPT]            INT           NULL,
    [AGENTIDENT]          INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA200] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA200_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA200_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA200] NOCHECK CONSTRAINT [CK_HCFICHA200_JSON];


GO
ALTER TABLE [dbo].[HCFICHA200] NOCHECK CONSTRAINT [FK_HCFICHA200_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente Identificado  1=Vibrio Cholerae  2=Vibrio Spp  3=Otro  4=Pendiente  5=No detectado  6=Vibrio Cholerae O1 toxigénico  7=Vibrio Cholerae O1 no toxigénico  8=Vibrio Cholerae no O1, no O139 toxigénico  9=Vibrio Cholerae no O1, no O139 no toxigénico  10=Vibrio Cholerae O139', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'AGENTIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Caso Captado por   1=UPGD  2=Busqueda activa institucional  3=Vig. Intensificada  4=Busquedad activa comunitaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CASOCAPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Caso Asociado a un Brote  True = si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CASOASOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ha hecho contacto con personas con los mismos sintomas  True = Si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'HECHOCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desplazamiento Últimos Cinco Días  True = Si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';

