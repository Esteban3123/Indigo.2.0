CREATE TABLE [dbo].[HCFICHA310] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CARNEVACU]           BIT           NULL,
    [VACUFIEAMA]          INT           NULL,
    [FECHAAPLI]           DATE          NULL,
    [FIEBRE]              BIT           NULL,
    [MIALGIAS]            BIT           NULL,
    [ARTRALGIAS]          BIT           NULL,
    [CEFALEA]             BIT           NULL,
    [VOMITO]              BIT           NULL,
    [ICTERICIA]           BIT           NULL,
    [SFAGET]              BIT           NULL,
    [OLIGURIA]            BIT           NULL,
    [CHOQUEXSHOCK]        BIT           NULL,
    [BRADICARDIA]         BIT           NULL,
    [FALLARENAL]          BIT           NULL,
    [FALLAHEPA]           BIT           NULL,
    [HEPATOMEGALIA]       BIT           NULL,
    [SIGNOSHEMORRA]       BIT           NULL,
    [SIGHEMORRA]          INT           NULL,
    [CASOFIEAMA]          BIT           NULL,
    [SITIOPROBA]          VARCHAR (50)  NULL,
    [FECHATOMA]           DATE          NULL,
    [FECHARECE]           DATE          NULL,
    [MUESTRA]             INT           NULL,
    [PRUEBA]              INT           NULL,
    [AGENTE]              INT           NULL,
    [RESULTADO]           INT           NULL,
    [FECHARESUL]          DATE          NULL,
    [VALOR]               VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA310] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA310_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA310_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA310] NOCHECK CONSTRAINT [CK_HCFICHA310_JSON];


GO
ALTER TABLE [dbo].[HCFICHA310] NOCHECK CONSTRAINT [FK_HCFICHA310_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde V01_2020-03-06 ) =    SH_HEMOPTISIS = booleano (S.H. - Hemoptisis)    SH_HIPEREMIA = booleano (S.H. - Hiperemia)    SH_HEMATEMESIS = booleano (S.H. - Hematemesis)    SH_PETEQUIAS = booleano (S.H. - Petequias)    SH_METRORRAGIA = booleano (S.H. - Metrorragia)    SH_MELENAS = booleano (S.H. - Melenas)    SH_EQUIMOSIS = booleano (S.H. - Equimosis)    SH_EPISTAXIS = booleano (S.H. - Epistaxis)    SH_HEMATURIA = booleano (S.H. - Hematuria)    --> en donde S. H. : Signos Hemorrágicos   CODSITIO_INFECCION  = texto ( codigo numerico sitio probable infección)    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sitio Probable de Infección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1=Positivo   2=Negativo   3=No Procesado   4=Inadecuado   5=Valor Registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente:  1=Fiebre Amarilla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:   1=PCR   2=Aislamiento   3=TGO   4=TGP   5=Bilirrubina Total   6=Bilirrubina Directa   7=Bilirrubina Indirecta   8=Creatinina   9=BUN   10=Parología   11=Estudio Directo   12=Elisa   13=Tiempo de Protombina   14=Tiempo Parcial de Tromboplastina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra:  1=Sangre Total   2=Tejido   3=Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Toma de Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sitio Probable de Infección    --> campo obsoleto desde version ''V01_2020-03-06'' , ahora se utiliza la columna CODSITIO_INFECCION en el campo JSON  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SITIOPROBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Casofieama:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CASOFIEAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Signos Hemorrágicos:  1=Hemoptisis   2=Hiperemia Conjuntival   3=Hematemesis   4=Petequias   5=Metrorragia   6=Melenas   7=Equimosis   8=Epistaxis   9=Hematuria    --> campo obsoleto desde version ''V01_2020-03-06'' , ahora es opción multiple, por lo que se utiliza cada columna en el campo JSON  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SIGHEMORRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Signoshemorra (Signos Hemorrágicos) :  True=Si   False=No    --> campo obsoleto desde version ''V01_2020-03-06''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SIGNOSHEMORRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hepatomegalia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fallahepa:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FALLAHEPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fallarenal:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FALLARENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bradicardia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Choquexshot:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CHOQUEXSHOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Oliguria:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'OLIGURIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sfaget:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SFAGET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ictericia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ICTERICIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vomito:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cefalea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Artralgias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mialgias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fiebre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vacuna fiebre amarilla:   1= Si   2= No   3= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VACUFIEAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Carnet de vacunacion:  True=Si   false=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CARNEVACU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ID';

