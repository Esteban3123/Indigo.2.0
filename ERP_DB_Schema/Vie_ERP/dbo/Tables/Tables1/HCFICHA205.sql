CREATE TABLE [dbo].[HCFICHA205] (
    [ID]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION]  INT           NOT NULL,
    [SEMANASEMBARAZO]      INT           NULL,
    [CODDIAGNO]            CHAR (4)      NULL,
    [CLASIFCASO]           BIT           NULL,
    [REACTIVACION]         BIT           NULL,
    [FIEBRE]               BIT           NULL,
    [DISNEA]               BIT           NULL,
    [EDEMAFACIAL]          BIT           NULL,
    [EDEMAMIEMINF]         BIT           NULL,
    [DERRAPERICAR]         BIT           NULL,
    [HEPATOESPLENOMEGALIA] BIT           NULL,
    [ADENOPATIAS]          BIT           NULL,
    [ROMANA]               BIT           NULL,
    [CHAGOMA]              BIT           NULL,
    [FALLACARDIA]          BIT           NULL,
    [DISFAGIA]             BIT           NULL,
    [DOLORTORAX]           BIT           NULL,
    [BRADICARDIA]          BIT           NULL,
    [ARRITMIACARDIA]       BIT           NULL,
    [MICROMETODO]          INT           NULL,
    [GOTAGRUESA]           INT           NULL,
    [MICROHEMATO]          INT           NULL,
    [STROUT]               INT           NULL,
    [ELISA]                INT           NULL,
    [ELISACLIA]            INT           NULL,
    [IFI]                  INT           NULL,
    [INMUNOBIOT]           INT           NULL,
    [VIATRANSMI]           INT           NULL,
    [VERSION]              VARCHAR (20)  NULL,
    [JSON]                 VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA205] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA205_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA205_HCFICHANOTIFICACION1] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA205] NOCHECK CONSTRAINT [CK_HCFICHA205_JSON];


GO
ALTER TABLE [dbo].[HCFICHA205] NOCHECK CONSTRAINT [FK_HCFICHA205_HCFICHANOTIFICACION1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Posible vía de transmisión  1=Vectorial  2=Transfuncional  3=Congénita  4=Vía oral  5=Transplante  6=Accidente de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'VIATRANSMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Inmunobiot  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'INMUNOBIOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IFI IgG T.cruzi  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'IFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ELISA/CLIA Ag recombinantes/péptidos sintéticos  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ELISACLIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ELISA Ag totales (convencional)  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ELISA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Strout  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'STROUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Microhematocrito / examen  1 = Positivo2= Negativo3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'MICROHEMATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gota gruesa / frotis de sangre  1 = Positivo2= Negativo  3= No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'GOTAGRUESA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Micrométodo  1=Positive  2=Negativo  3=No se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'MICROMETODO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones Clínicas que presenta el paciente crónico  Si selecciona   ARRITMIACARDIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ARRITMIACARDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones Clínicas que presenta el paciente crónico  Si selecciona   BRADICARDIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones Clínicas que presenta el paciente crónico  Si selecciona   DISFAGIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DOLORTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones Clínicas que presenta el paciente crónico  Si selecciona   DISFAGIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DISFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones Clínicas que presenta el paciente crónico  Si selecciona   FALLACARDIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'FALLACARDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     CHAGOMA= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CHAGOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     ROMANA= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ROMANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     ADENOPATIAS= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ADENOPATIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     HEPATOESPLENOMEGALIA= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'HEPATOESPLENOMEGALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     DERRAPERICAR= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DERRAPERICAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     EDEMAMIEMINF= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'EDEMAMIEMINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     EDEMAFACIAL= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'EDEMAFACIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona     DISNEA= True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'DISNEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Manifestaciones clínicas que presenta el paciente agudo  Si selecciona   Fiebre = True   si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reactivación  True = Si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'REACTIVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación del caso  True = Agudo   False = Crónico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Semana de embarazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'SEMANASEMBARAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA205', @level2type = N'COLUMN', @level2name = N'ID';

