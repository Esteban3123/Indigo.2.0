CREATE TABLE [dbo].[HCFICHA345] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [PRESECARNE]          BIT           NULL,
    [NEUMOCOCO]           INT           NULL,
    [INFLUENZAESTACIO]    INT           NULL,
    [DOSIS1]              VARCHAR (50)  NULL,
    [DOSIS2]              VARCHAR (50)  NULL,
    [FECHAULTDOS1]        DATE          NULL,
    [FECHAULTDOS2]        DATE          NULL,
    [ASMA]                BIT           NULL,
    [OBESIDAD]            BIT           NULL,
    [TOS]                 BIT           NULL,
    [EPOC]                BIT           NULL,
    [INSUFIRENAL]         BIT           NULL,
    [FIEBRE]              BIT           NULL,
    [DIABETES]            BIT           NULL,
    [TOMAMEDICA]          BIT           NULL,
    [DOLORGARGANTA]       BIT           NULL,
    [VIH]                 BIT           NULL,
    [FUMADOR]             BIT           NULL,
    [RINORREA]            BIT           NULL,
    [ENFECARDIACA]        BIT           NULL,
    [OTROS]               BIT           NULL,
    [CONJUNTIVITIS]       BIT           NULL,
    [CANCER]              BIT           NULL,
    [CEFALEA]             BIT           NULL,
    [MALNUTRICION]        BIT           NULL,
    [DIFIRESPIRA]         BIT           NULL,
    [DIARREA]             BIT           NULL,
    [CUALESOTROS]         VARCHAR (50)  NULL,
    [SEMAGEST]            VARCHAR (50)  NULL,
    [TOMRADIOTORAX]       INT           NULL,
    [FECHATOMA1]          DATE          NULL,
    [FECHATOMA2]          DATE          NULL,
    [FECHATOMA3]          DATE          NULL,
    [HALLAZGOS]           INT           NULL,
    [USOANTIBIO]          BIT           NULL,
    [FECHAINICIO1]        DATE          NULL,
    [FECHAINICIO2]        DATE          NULL,
    [USOANTIVIRAL]        BIT           NULL,
    [SERVIHOSPITAL]       BIT           NULL,
    [FECHAINGRESO]        DATE          NULL,
    [DERRAPLEURAL]        BIT           NULL,
    [DERRAPERICAR]        BIT           NULL,
    [MIOCARDITIS]         BIT           NULL,
    [SEPTICEMIA]          BIT           NULL,
    [FALLARESPIR]         BIT           NULL,
    [OTRO]                BIT           NULL,
    [OTROSCUALES]         VARCHAR (50)  NULL,
    [DIAGNOINICI]         VARCHAR (50)  NULL,
    [DIAGNOEGRESO]        VARCHAR (50)  NULL,
    [FECHARECE1]          DATE          NULL,
    [FECHARECE2]          DATE          NULL,
    [FECHARECE3]          DATE          NULL,
    [FECHARECE4]          DATE          NULL,
    [MUESTRA1]            INT           NULL,
    [MUESTRA2]            INT           NULL,
    [PRUEBA1]             INT           NULL,
    [PRUEBA2]             INT           NULL,
    [AGENTE1]             INT           NULL,
    [AGENTE2]             INT           NULL,
    [RESULTADO1]          INT           NULL,
    [RESULTADO2]          INT           NULL,
    [VALORREGIS1]         VARCHAR (50)  NULL,
    [VALORREGIS2]         VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA345] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA345_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA345_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA345] NOCHECK CONSTRAINT [CK_HCFICHA345_JSON];


GO
ALTER TABLE [dbo].[HCFICHA345] NOCHECK CONSTRAINT [FK_HCFICHA345_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guardan los columna-dato nuevos de FichaEnfermedadSimilarInfluenza (COVID19, DOSIS3, FECHAULTDOS3, NOMVACU, Hipertension)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado2:   1=Positivo   2=Negativo   3=No Procesado   4=Inadecuado   5=Valor Registrado   6=Contaminado con hongos   7=Muestra escasa de células', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado1:   1=Positivo   2=Negativo   3=No Procesado   4=Inadecuado   5=Valor Registrado   6=Contaminado con hongos   7=Muestra escasa de células', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente2:  1=Otro   2=Adenovirus   3=Virus sincitial respiratorio   4=Haemophilus influenzae   5=Streptococcus pneumoniae  6=Influenza A   7=Influenza B   8=Parainfluenza 1   9=Parainfluenza 2   10=Parainfluenza 3   11=Enterovirus   12=Influenza A(H1N1) pdm09   13=Influenza A no subtipificable   14=Bocavirus   15=Coronavirus   16=Metaneumovirus   17=Rinovirus   18=Virus respiratorios   19=Coronavirus causante del síndrome respiratorio de oriente medio (MERS - CoV)   20=Coronavirus subtipo 229e   21=Coronavirus subtipo HKU1   22=Coronavirus subtipo NL63   23=Coronavirus subtipo OC43   24=Influenza A(H3N2)   25=Parainfluenza tipo 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'AGENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente1:   1=Otro   2=Adenovirus   3=Virus sincitial respiratorio   4=Haemophilus influenzae   5=Streptococcus pneumoniae  6=Influenza A   7=Influenza B   8=Parainfluenza 1   9=Parainfluenza 2   10=Parainfluenza 3   11=Enterovirus   12=Influenza A(H1N1) pdm09   13=Influenza A no subtipificable   14=Bocavirus   15=Coronavirus   16=Metaneumovirus   17=Rinovirus   18=Virus respiratorios   19=Coronavirus causante del síndrome respiratorio de oriente medio (MERS - CoV)   20=Coronavirus subtipo 229e   21=Coronavirus subtipo HKU1   22=Coronavirus subtipo NL63   23=Coronavirus subtipo OC43   24=Influenza A(H3N2)   25=Parainfluenza tipo 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'AGENTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:   1=PCR   2=Aislamiento viral   3=Patología   4=Inmunohistoquímica   5=Inhibición hemaglutinación   6=Cultivo   7=IFI   8=Hemocultivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:   1=PCR   2=Aislamiento viral   3=Patología   4=Inmunohistoquímica   5=Inhibición hemaglutinación   6=Cultivo   7=IFI   8=Hemocultivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra:   1=Sangre total   2=Hisopado nasofaríngeo   3=Tejido   4=Aspirado nasofaríngeo   5=Otros líquidos esteriles   6=Lavado bronquial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra:   1=Sangre total   2=Hisopado nasofaríngeo   3=Tejido   4=Aspirado nasofaríngeo   5=Otros líquidos esteriles   6=Lavado bronquial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Falla respiratoria:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FALLARESPIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Septicemia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'SEPTICEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Miocarditis:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MIOCARDITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Derrapericar:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DERRAPERICAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DERRAPLEURAL:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DERRAPLEURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio en el que se hospitalizó:   Hospitalización general=Si   UCI=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'SERVIHOSPITAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usó antivirales:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'USOANTIVIRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usó antibióticos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'USOANTIBIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diarrea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Difirespira:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIFIRESPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Malnutricion:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'MALNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cefalea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cancer:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Conjuntivitis:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'CONJUNTIVITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Enfecardiaca:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'ENFECARDIACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rinorrea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'RINORREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fumador:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FUMADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vih:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'VIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dolor garganta:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DOLORGARGANTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tomamedica:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'TOMAMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diabetes:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fiebre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Insufirenal:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'INSUFIRENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Epoc:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'EPOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'TOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obesidad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Asma:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'ASMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Influenza estacional:  1=Si   2=No   3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'INFLUENZAESTACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Streptococcus Pneumoniae (neumococo):  1=Si   2=No   3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'NEUMOCOCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presentó Carné:  True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA345', @level2type = N'COLUMN', @level2name = N'PRESECARNE';

