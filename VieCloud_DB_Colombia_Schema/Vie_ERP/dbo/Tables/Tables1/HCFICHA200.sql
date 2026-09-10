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
ALTER TABLE [dbo].[HCFICHA200] NOCHECK CONSTRAINT [CK_HCFICHA200_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON (VARCHAR MAX), validado con ISJSON; contiene campos de notificación epidemiológica expandidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación (VARCHAR 20); NULL indica primera versión, incrementa en cambios posteriores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente identificado en cultivo/PCR (INT): 1=Vibrio Cholerae, 2=Vibrio Spp, 3=Otro, 4=Pendiente, 5=No detectado, 6=V.C. O1 toxigénico, 7=V.C. O1 no toxigénico, 8=V.C. no O1/O139 toxigénico, 9=V.C. no O1/O139 no toxigénico, 10=V.C. O139', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'AGENTIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente Identificado  1=Vibrio Cholerae  2=Vibrio Spp  3=Otro  4=Pendiente  5=No detectado  6=Vibrio Cholerae O1 toxigénico  7=Vibrio Cholerae O1 no toxigénico  8=Vibrio Cholerae no O1, no O139 toxigénico  9=Vibrio Cholerae no O1, no O139 no toxigénico  10=Vibrio Cholerae O139', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'AGENTIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'AGENTIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de captura del caso (INT): 1=UPGD (Unidad Primaria Generadora Datos), 2=Búsqueda activa institucional, 3=Vigilancia intensificada, 4=Búsqueda activa comunitaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CASOCAPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caso Captado por   1=UPGD  2=Busqueda activa institucional  3=Vig. Intensificada  4=Busquedad activa comunitaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CASOCAPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CASOCAPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caso asociado a brote epidemiológico (BIT): 1=Sí, 0=No; vinculación a eventos de salud pública', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CASOASOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caso Asociado a un Brote  True = si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CASOASOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CASOASOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de contactos/respuesta epidemiológica (VARCHAR 50); registro narrativo ingresado por usuario responsable de investigación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CONQUIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Con quien respuesta que escribio el Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CONQUIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CONQUIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contacto confirmado con personas con síntomas similares (BIT): 1=Sí, 0=No; indica transmisibilidad interpersonal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'HECHOCONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ha hecho contacto con personas con los mismos sintomas  True = Si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'HECHOCONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'HECHOCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de llegada a 4º destino/localidad en desplazamiento (DATE); última ubicación en cadena de movilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Llegada 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de llegada a 3º destino/localidad en desplazamiento (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Llegada 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de llegada a 2º destino/localidad en desplazamiento (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Llegada 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de llegada a 1º destino/localidad en desplazamiento (DATE); primera parada en ruta de viaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Llegada 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHLLEG1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de salida/desplazamiento desde 4º punto (DATE); último origen en cadena de viajes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Llegada 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de salida/desplazamiento desde 3º punto (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Llegada 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de salida/desplazamiento desde 2º punto (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Llegada 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de salida/desplazamiento desde 1º punto (DATE); inicio de ruta de movilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Llegada 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'FECHDESP1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de 4º destino en desplazamiento (VARCHAR 50); última nación en itinerario internacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de 3º destino en desplazamiento (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de 2º destino en desplazamiento (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de 1º destino en desplazamiento (VARCHAR 50); primer país destino en viaje internacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'PAIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad/municipio de 4º destino en desplazamiento (VARCHAR 50); última ciudad en ruta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad/municipio de 3º destino en desplazamiento (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad/municipio de 2º destino en desplazamiento (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad/municipio de 1º destino en desplazamiento (VARCHAR 50); primera ciudad destino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CIUDAD1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento/estado de 4º localidad registrado por usuario (VARCHAR 50); última región administrativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o Estado 4 que escribio el usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento/estado de 3º localidad registrado por usuario (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o Estado 3 que escribio el usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento/estado de 2º localidad registrado por usuario (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o Estado 2 que escribio el usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento/estado de 1º localidad registrado por usuario (VARCHAR 50); primer departamento en ruta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento o Estado 1 que escribio el usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DEPART1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desplazamiento en últimos cinco días previos a síntomas (BIT): 1=Sí, 0=No; factor de exposición epidemiológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desplazamiento Últimos Cinco Días  True = Si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CHAR 4); clasificación CIE-10 simplificada del evento notificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de ficha de notificación (INT); FK a HCFICHANOTIFICACION; vincula con evento de vigilancia epidemiológica principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID único autoincremental (INT IDENTITY); clave primaria de detalle de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle epidemiológico de desplazamientos y contactos asociado a una ficha de notificación de evento de salud pública (ej. enfermedad de notificación obligatoria). Registra hasta cuatro trayectos de viaje del paciente (departamento, ciudad, país, fechas de salida y llegada), si hubo contacto con otras personas, con quién, y si existe un caso asociado o captado previamente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA200';
