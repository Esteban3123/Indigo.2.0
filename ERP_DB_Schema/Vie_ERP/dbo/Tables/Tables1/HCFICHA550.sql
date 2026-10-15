CREATE TABLE [dbo].[HCFICHA550] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [SITIODEFUN]          INT           NULL,
    [CONVIVENCIA]         INT           NULL,
    [MARCOTROCUAL]        VARCHAR (50)  NULL,
    [ESCOLARIDAD]         INT           NULL,
    [REGULAFECUNDI]       INT           NULL,
    [GESTACIONES]         VARCHAR (50)  NULL,
    [PARTVAGINA]          VARCHAR (50)  NULL,
    [CESAREAS]            VARCHAR (50)  NULL,
    [MUERTOS]             VARCHAR (50)  NULL,
    [VIVOS]               VARCHAR (50)  NULL,
    [ABORTOS]             VARCHAR (50)  NULL,
    [NINGUNO]             BIT           NULL,
    [HIPERTCRON]          BIT           NULL,
    [CARDIOPATIAS]        BIT           NULL,
    [DIABETES]            BIT           NULL,
    [MOLAHIDATI]          BIT           NULL,
    [RNPRETERMINO]        BIT           NULL,
    [RNBAJOPESO]          BIT           NULL,
    [RNMASOCROMI]         BIT           NULL,
    [TRASTORMENT]         BIT           NULL,
    [OBESIDAD]            BIT           NULL,
    [DESNUTRICRON]        BIT           NULL,
    [INTERGEMENOR]        BIT           NULL,
    [ITS]                 BIT           NULL,
    [VIHSIDA]             BIT           NULL,
    [OTRASINFECC]         BIT           NULL,
    [RHNEGATIVO]          BIT           NULL,
    [TABAQUISMO]          BIT           NULL,
    [ALCOHOLIS]           BIT           NULL,
    [SUSTPSICO]           BIT           NULL,
    [DEFICISOCIO]         BIT           NULL,
    [SIFILIS]             BIT           NULL,
    [HEPATITISB]          BIT           NULL,
    [OTROFACTRIES]        BIT           NULL,
    [GINGIVITIS]          BIT           NULL,
    [PREECLAMPSIA]        BIT           NULL,
    [ECLAMPSIA]           BIT           NULL,
    [SINDROHELLP]         BIT           NULL,
    [DIABETESGEST]        BIT           NULL,
    [SEPSIS]              BIT           NULL,
    [HEMO1ERSEME]         BIT           NULL,
    [HEMO2DOSEME]         BIT           NULL,
    [HEMO3ERTRIME]        BIT           NULL,
    [DESPROPCEFALO]       BIT           NULL,
    [RETARCRECINTR]       BIT           NULL,
    [ENFEAUTOIN]          BIT           NULL,
    [MALARIA]             BIT           NULL,
    [EMBANODESEA]         BIT           NULL,
    [VIOLECONTRA]         BIT           NULL,
    [OTRACOMPLICA]        BIT           NULL,
    [GESTPRODVIOL]        BIT           NULL,
    [FETOINCOMPA]         BIT           NULL,
    [SINTODEPRES]         BIT           NULL,
    [MARCOTROFAC]         VARCHAR (50)  NULL,
    [MARCOTRACOMP]        VARCHAR (50)  NULL,
    [NOCPN]               VARCHAR (50)  NULL,
    [SEMAINICCPN]         VARCHAR (50)  NULL,
    [CONTROREALIZA]       INT           NULL,
    [NIVELATENC1]         INT           NULL,
    [NIVELATENC2]         INT           NULL,
    [REMISIOPORTU]        INT           NULL,
    [COMPLIFETORNCIE]     VARCHAR (50)  NULL,
    [MOMENOCURMUE]        INT           NULL,
    [SEMAGESTMORT]        VARCHAR (50)  NULL,
    [FECHAHORA]           DATETIME      NULL,
    [HORA]                DATETIME      NULL,
    [TIPOPARTO]           INT           NULL,
    [PARTOATENDI]         INT           NULL,
    [OTROQUIEN]           VARCHAR (50)  NULL,
    [CAUSADEFUN]          VARCHAR (50)  NULL,
    [CAUSAMUERTE]         INT           NULL,
    [DEMORA1]             VARCHAR (50)  NULL,
    [DEMORA2]             VARCHAR (50)  NULL,
    [DEMORA3]             VARCHAR (50)  NULL,
    [DEMORA4]             VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA550] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA550_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA550] NOCHECK CONSTRAINT [FK_HCFICHA550_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guardan otros campos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CAUSAMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CAUSADEFUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Otro quien (datos relacionados con la muerte materna 7.4.1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTROQUIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parto atendido por (datos relacionados con la muerte materna)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PARTOATENDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del parto (datos relacionados con la muerte materna)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Fecha y hora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'FECHAHORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEMAGESTMORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento en que ocurrio la muerte (5.Durante la gestación, 6.En los últimos 42 días posterior al parto, 7. Entre 43 días y un año antes del parto) datos relacionados con la muerte materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MOMENOCURMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'COMPLIFETORNCIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'REMISIOPORTU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NIVELATENC2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NIVELATENC1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CONTROREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semana gestacional al inicio de CPN (antecedentes prenatales del embarazo actual)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEMAINICCPN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de controles prenatales (antecedentes prenatales del embarazo actual)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NOCPN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTRACOMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTROFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SINTODEPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'FETOINCOMPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GESTPRODVIOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTRACOMPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIOLECONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'EMBANODESEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MALARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ENFEAUTOIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RETARCRECINTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DESPROPCEFALO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO3ERTRIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO2DOSEME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO1ERSEME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEPSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DIABETESGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SINDROHELLP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ECLAMPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PREECLAMPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GINGIVITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTROFACTRIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEPATITISB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SIFILIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEFICISOCIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SUSTPSICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ALCOHOLIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TABAQUISMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RHNEGATIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTRASINFECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIHSIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ITS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'INTERGEMENOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DESNUTRICRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TRASTORMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNMASOCROMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNBAJOPESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNPRETERMINO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MOLAHIDATI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CARDIOPATIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HIPERTCRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NINGUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el número de abortos (Antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ABORTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el número de vivos (antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de muertos (antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MUERTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el numero de cesareas (Antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CESAREAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de partos vaginales (antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PARTVAGINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el número de gestaciones (antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GESTACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Regulación de la fecundidad (antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'REGULAFECUNDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ESCOLARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTROCUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CONVIVENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio donde ocurrio la muerte (datos relacionados con la muerte materna)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SITIODEFUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ID';

