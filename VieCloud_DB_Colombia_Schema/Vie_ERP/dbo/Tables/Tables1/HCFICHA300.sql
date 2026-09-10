CREATE TABLE [dbo].[HCFICHA300] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [TIPOAGRE]            INT           NULL,
    [AREAMORD]            INT           NULL,
    [AGREPROV]            BIT           NULL,
    [TIPOLESI]            BIT           NULL,
    [PROFUNDI]            BIT           NULL,
    [CABEZA]              BIT           NULL,
    [MANOS]               BIT           NULL,
    [TRONCO]              BIT           NULL,
    [MIEMSUP]             BIT           NULL,
    [MIEMINF]             BIT           NULL,
    [PIES]                BIT           NULL,
    [GENITA]              BIT           NULL,
    [FECHAAGRE]           DATE          NULL,
    [ESPAGRE]             INT           NULL,
    [ANIVAC]              INT           NULL,
    [PRESECARNE]          BIT           NULL,
    [FECHAVACU]           DATE          NULL,
    [NOMPROP]             VARCHAR (50)  NULL,
    [DIREPROP]            VARCHAR (50)  NULL,
    [ESTAANAGRE]          INT           NULL,
    [ESTAANCON]           INT           NULL,
    [UBIANIM]             INT           NULL,
    [TIPOEXPO]            INT           NULL,
    [SUEROANTI]           INT           NULL,
    [FECHAAPLI]           DATE          NULL,
    [VACUANTI]            INT           NULL,
    [NUMDOSIS]            VARCHAR (50)  NULL,
    [FECHAULT]            DATE          NULL,
    [LAVHERI]             BIT           NULL,
    [SUTUHER]             BIT           NULL,
    [ORDENSUE]            BIT           NULL,
    [ORDENAPLI]           BIT           NULL,
    [TELEFONO]            VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA300] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA300_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA300_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA300] NOCHECK CONSTRAINT [CK_HCFICHA300_JSON];




GO
ALTER TABLE [dbo].[HCFICHA300] NOCHECK CONSTRAINT [CK_HCFICHA300_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON; varchar(max); almacena extensiones de la ficha de notificación de rabia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación de rabia; varchar(20); NULL = primera versión; controla cambios históricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto del propietario o responsable del animal agresor; varchar(50); datos de comunicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de orden de aplicación de vacuna antirrábica; bit; 1=sí, 0=no; decisión clínica post-exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ORDENAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ordenó Aplicación Vacuna  True = si false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ORDENAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ORDENAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de orden de suero antirrábico; bit; 1=sí, 0=no; decisión clínica post-exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ORDENSUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ordenó Suero Antirrábico  True = si false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ORDENSUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ORDENSUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sutura de la herida por agresión; bit; 1=sí, 0=no; procedimiento de cierre de lesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'SUTUHER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sutura de la Herida  True = si false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'SUTUHER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'SUTUHER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de lavado de herida con agua y jabón; bit; 1=sí, 0=no; primera medida de descontaminación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'LAVHERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lavado de Herida con Agua y Jabón  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'LAVHERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'LAVHERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última dosis de vacuna antirrábica aplicada; date (dd/mm/aaaa); seguimiento de esquema vacunal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Última Dosis (dd/mm/aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis de vacuna antirrábica aplicadas; varchar(50); contador de dosis en esquema post-exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'NUMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'NUMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'NUMDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de aplicación de vacuna antirrábica al paciente; 1=sí, 2=no, 3=no sabe; estado de inmunización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'VACUANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacuna Antirrábica  1=si   2=no  3=no sabe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'VACUANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'VACUANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de aplicación de vacuna o suero antirrábico; date (dd/mm/aaaa); registro del procedimiento preventivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Aplicación (dd/mm/aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de aplicación de suero antirrábico; 1=sí, 2=no, 3=no sabe; inmunoprofilaxis post-exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'SUEROANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Suero Antirrábico  1=si  2=no  3=No sabe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'SUEROANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'SUEROANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de tipo de exposición al virus rábico; 1=no exposición, 2=exposición leve, 3=exposición grave; determina necesidad de profilaxis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Exposición  1= No exposicion  2=Exposicion leve  3=Exposicion Grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación del animal agresor después del incidente; 1=observable, 2=perdido; afecta decisión de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'UBIANIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicación del Animal Agreso  1=Observable  2=Perdido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'UBIANIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'UBIANIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado clínico del animal al momento de consulta; 1=vivo, 2=muerto, 3=desconocido; información para observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESTAANCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Animal al Momento de la Consulta  1=vivo  2=Muerto  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESTAANCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESTAANCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado clínico del animal al momento de agresión; 1=signos de rabia, 2=sin signos, 3=desconocido; riesgo de transmisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESTAANAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Animal al Momento de la Agresión o Contacto  1=Con Signos de Rabia  2=Sin Signos de Rabia  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESTAANAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESTAANAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección del propietario o responsable del animal agresor; varchar(50); datos para seguimiento y trazabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'DIREPROP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección del Propietario o Responsable del Agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'DIREPROP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'DIREPROP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del propietario o responsable del animal agresor; varchar(50); identificación de contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'NOMPROP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Propietario o Responsable del Agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'NOMPROP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'NOMPROP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última vacunación antirrábica del animal agresor; date (dd/mm/aaaa); indica protección del animal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAVACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Vacunación (dd/mm/aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAVACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAVACU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de presentación de carné de vacunación antirrábica del animal; bit; 1=sí, 0=no; evidencia de inmunización animal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PRESECARNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentó Carné de Vacunación Antirrábica  True = si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PRESECARNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PRESECARNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de vacunación previa del animal agresor; 1=sí, 2=no, 3=desconocido; información sobre protección animal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ANIVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Animal Vacunado  1= si   2= no  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ANIVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ANIVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especie del animal agresor; 1=perro, 2=gato, 3=bovino-bufalino, 4=equidos, 5=porcino, 6=murciélago, 7=zorro, 8=primate, 9=humano, 10=silvestres, 11=ovino-caprino, 12=roedores; epidemiología de rabia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESPAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especie Agresora  1=perro  2=gato  3=bovino -bufalino  4=equidos  5=porcino(Cerdo)  6=murcielago  7=zorro  8=mico  9=humano  10=otros silvestres  11=ovino - caprino  12= grandes roedores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESPAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESPAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del evento de agresión o contacto con animal; date (dd/mm/aaaa); marca inicio de exposición rábica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Agresión o Contacto (dd/mm/aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'FECHAAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag de lesión en genitales externos; bit; 1=genitales externos; localización anatómica de herida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'GENITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización Anatómica de la Lesión  7=Genitales Externos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'GENITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'GENITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag de lesión en pies o dedos; bit; 1=pies y dedos; localización anatómica de herida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización Anatómica de la Lesión  6=Pies, Dedos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag de lesión en miembros inferiores; bit; 1=miembros inferiores; localización anatómica de herida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MIEMINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización Anatómica de la Lesión  5=Miembros Inferiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MIEMINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MIEMINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag de lesión en miembros superiores; bit; 1=miembros superiores; localización anatómica de herida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MIEMSUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización Anatómica de la Lesión  4=Miembros Superiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MIEMSUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MIEMSUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag de lesión en tronco; bit; 1=tronco; localización anatómica de herida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TRONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización Anatómica de la Lesión  3=Tronco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TRONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TRONCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag de lesión en manos o dedos; bit; 1=manos y dedos; localización anatómica de herida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MANOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización Anatómica de la Lesión  2=Manos, Dedos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MANOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MANOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag de lesión en cabeza, cara o cuello; bit; 1=cabeza/cara/cuello; localización anatómica de herida (alto riesgo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'CABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización Anatómica de la Lesión  1=Cabeza, Cara, Cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'CABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'CABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de profundidad de lesión; bit; 1=superficial, 0=profunda; afecta evaluación de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PROFUNDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profundidad  True = SuperficialFalse = Profunda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PROFUNDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PROFUNDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de multiplicidad de lesiones; bit; 1=única, 0=múltiple; caracteriza patrón de agresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOLESI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Lesión  True = Unica  False = Multiple', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOLESI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOLESI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de agresión provocada por la víctima; bit; 1=sí, 0=no; contexto del incidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'AGREPROV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agresión Provocada  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'AGREPROV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'AGREPROV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área corporal de mordedura si aplica; 1=área cubierta del cuerpo, 2=área descubierta; afecta riesgo de transmisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'AREAMORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si marcó mordedura, Seleccione Área  1=En Área Cubierta del Cuerpo  2=En Área Descubierta del Cuerpo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'AREAMORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'AREAMORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de contacto o agresión; 1=mordedura, 2=arañazo, 3=contacto de mucosa con saliva, 4=contacto con tejido nervioso, 5=inhalación de aerosoles, 6=trasplante de órganos; define mecanismo de exposición rábica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Agresión o Contacto  1=Mordedura  2=Arañazo o Rasguño  3=Contacto de Mucosa o Piel Lesionada con Saliva o Baba Infectada con Virus Rábico  4=Contacto de Mucosa o Piel Lesionada con Tejido Nervioso, Material Biológico o Secreciones Infectadas con Virus Rábico  5=Inhalación en Ambientes Cargados o Virus Rábico (Aerosoles)  6=Transplante de Órganos o Tejidos Infectados con Virus Rábico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 de la exposición o infección rábica; char(4); clasificación de morbilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a ficha de notificación padre (FK); int; relación con HCFICHANOTIFICACION.ID; identifica caso de vigilancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado; int; primary key; clave única de registro de exposición rábica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación de accidente ofídico o agresión por animal (mordedura/ataque), incluyendo datos del tipo de agresión, área corporal afectada, fechas, estado del animal agresor, vacunación antitetánica y antirrábica, tratamiento aplicado (suero antiofídico, vacunas) y datos del propietario del animal. Corresponde al formulario 300 de vigilancia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300';
