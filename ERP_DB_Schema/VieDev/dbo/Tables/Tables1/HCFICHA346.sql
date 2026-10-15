CREATE TABLE [dbo].[HCFICHA346] (
    [ID]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION]  INT             NOT NULL,
    [TRABADESALUD]         INT             NULL,
    [VIAJOAREASVIRUS]      INT             NULL,
    [VIAJETERNAL]          BIT             NULL,
    [LUGARVIAJENAL]        VARCHAR (50)    NULL,
    [VIAJETERINTER]        BIT             NULL,
    [LUGARVIAJEINTER]      VARCHAR (50)    NULL,
    [CONTACTO14DIAS]       INT             NULL,
    [SIN_TOS]              BIT             NULL,
    [SIN_FIEBRE]           BIT             NULL,
    [SIN_ODINOFAGIA]       BIT             NULL,
    [SIN_DIFICULTARESPIRA] BIT             NULL,
    [SIN_FATIGA]           BIT             NULL,
    [VACUINFLUENZA]        INT             NULL,
    [DOSIS]                NUMERIC (18, 1) NULL,
    [ANTEASMA]             BIT             NULL,
    [ANTEEPOC]             BIT             NULL,
    [ANTEDIABETES]         BIT             NULL,
    [ANTEVIH]              BIT             NULL,
    [ANTECARDIACA]         BIT             NULL,
    [ANTECANCER]           BIT             NULL,
    [ANTEMALNUTRI]         BIT             NULL,
    [ANTEOBESIDAD]         BIT             NULL,
    [ANTERENAL]            BIT             NULL,
    [ANTEMEDICA]           BIT             NULL,
    [ANTEFUMADOR]          BIT             NULL,
    [ANTTUBERCULOS]        BIT             NULL,
    [ANTEOTRO]             BIT             NULL,
    [OTROSANTE]            VARCHAR (1000)  NULL,
    [RADIOTORAX]           INT             NULL,
    [ANTIBIOTICOS]         BIT             NULL,
    [LABFECHATOMA1]        DATE            NULL,
    [LABFECHARECEP1]       DATE            NULL,
    [LABMUESTRA1]          INT             NULL,
    [LABPRUEBA1]           VARCHAR (3)     NULL,
    [LABAGENTE1]           VARCHAR (3)     NULL,
    [LABRESULTADO1]        INT             NULL,
    [LABFECHARECEP11]      DATETIME        NULL,
    [LABVALOREGIS1]        VARCHAR (200)   NULL,
    [LABFECHATOMA2]        DATE            NULL,
    [LABFECHARECEP2]       DATE            NULL,
    [LABMUESTRA2]          INT             NULL,
    [LABPRUEBA2]           VARCHAR (3)     NULL,
    [LABAGENTE2]           VARCHAR (3)     NULL,
    [LABRESULTADO2]        INT             NULL,
    [LABFECHARECEP22]      DATE            NULL,
    [LABVALOREGIS2]        VARCHAR (200)   NULL,
    [CODDIAGNO]            CHAR (4)        NULL,
    [CODDEPTOMUN]          VARCHAR (50)    NULL,
    [CODPAISINT]           INT             NULL,
    [SIN_RINORREA]         BIT             NULL,
    [SIN_CONJUNTIVITIS]    BIT             NULL,
    [SIN_CEFALEA]          BIT             NULL,
    [SIN_DIARREA]          BIT             NULL,
    [SIN_OLFATO]           BIT             NULL,
    [OTROSINT]             BIT             NULL,
    [OTROSINTO]            VARCHAR (50)    NULL,
    [ANTEHIPER]            BIT             NULL,
    [SERVHOS]              INT             NULL,
    [FECHINGUCI]           DATE            NULL,
    [COMPDEPLEURAL]        BIT             NULL,
    [COMPDEPERI]           BIT             NULL,
    [COMPMIOCARDITIS]      BIT             NULL,
    [COMPSEPTICEMIA]       BIT             NULL,
    [COMPRESPI]            BIT             NULL,
    [COMPOTRO]             BIT             NULL,
    [OTROCOMPLI]           VARCHAR (50)    NULL,
    [VERSION]              VARCHAR (20)    NULL,
    [JSON]                 VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_HCFICHA346] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA346_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA346_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA346] NOCHECK CONSTRAINT [CK_HCFICHA346_JSON];


GO
ALTER TABLE [dbo].[HCFICHA346] NOCHECK CONSTRAINT [FK_HCFICHA346_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde V01_2020-05-08 ) =    CODIGOPAIS : texto (Codigo Internacional del País en alfanumérico )', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo pais internacional    --> campo obsoleto desde versión ''V01_2020-05-08'' , lo reemplaza la columna CODIGOPAIS en campo JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CODPAISINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   6= Valor registrado   12= Contaminado con hongos   13= Muestra escasa de celulas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABRESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente:   8= 8-Otro   16= 16-Adenovirus   18=18-Virus sincitial respiratorio   22= 22-Haemophilus influenzae   24= 24-Streptococcus pneumoniae   40= 40-Influenza A   41= 41-Influenza   42= 42-Parainfluenza 1   43= 43-Parainfluenza 2   44= 44-Parainfluenza 3   56= 56-Enterovirus   59= 59-Influenza A(H1N1) pdm09   64= 64-Influenza A no subtipificable   76= 76-Bocavirus   77= 77-Coronavirus   78= 78-Metaneumovirus   79= 79-Rinovirus   84= 84-virus respiratorios   1Q= 1Q-Coronavirus causante del síndrome respiratorio de oriente medio (MERS-CoV)   1R= 1R-Coronavirus subtipo 229e   1S= 1S-Coronavirus subtipo HKU1   1T= 1T-Coronavirus subtipo NL63   1U= 1U-Coronavirus subtipo OC43   1V= 1V-Influenza A(H3N2)   1W= 1W-Parainfluenza tipo 4   2H= 2H. Coronavirus subtipo 2019-nCov', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABAGENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:  4= 4-PCR   E1= E1-Aislamiento viral   6= 6-Otra --> item eliminado desde version V01_2020-05-08   30= 30-Patología   31= 31-Inmunohistoquímica   46= 46-Inhibición hemaglutinación   55= 55-Cultivo   58= 58-Antigenemia --> item eliminado desde version V01_2020-05-08   76=76-IFI   92= 92-Hemocultivo     Los siguientes son items nuevos desde version V01_2020-05-08 :   2 = 2 - IgM   3 = 3 - IgG   F3 = F3 - Determinación de antígenos   H9 = H9 - IgG , IgM   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABPRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra:   1= 1-Sangre total   3= 3-Hisopado nasofaríngeo   4= 4-Tejido   8= 8-Aspirado nasofaríngeo   9= 9- Lavado nasal  --> item eliminado desde version V01_2020-05-08   11=  11-Lavado broncoalveolar (antes)  / 11 - Otros líquidos estériles (ahora) -->  item modificado desde version V01_2020-05-08   22= 22-Lavado bronquial ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   6= Valor registrado   12= Contaminado con hongos   13= Muestra escasa de celulas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABRESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente:   8= 8-Otro   16= 16-Adenovirus   18=18-Virus sincitial respiratorio   22= 22-Haemophilus influenzae   24= 24-Streptococcus pneumoniae   40= 40-Influenza A   41= 41-Influenza   42= 42-Parainfluenza 1   43= 43-Parainfluenza 2   44= 44-Parainfluenza 3   56= 56-Enterovirus   59= 59-Influenza A(H1N1) pdm09   64= 64-Influenza A no subtipificable   76= 76-Bocavirus   77= 77-Coronavirus   78= 78-Metaneumovirus   79= 79-Rinovirus   84= 84-virus respiratorios   1Q= 1Q-Coronavirus causante del síndrome respiratorio de oriente medio (MERS-CoV)   1R= 1R-Coronavirus subtipo 229e   1S= 1S-Coronavirus subtipo HKU1   1T= 1T-Coronavirus subtipo NL63   1U= 1U-Coronavirus subtipo OC43   1V= 1V-Influenza A(H3N2)   1W= 1W-Parainfluenza tipo 4   2H= 2H. Coronavirus subtipo 2019-nCov', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABAGENTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:  4= 4-PCR   E1= E1-Aislamiento viral   6= 6-Otra --> item eliminado desde version V01_2020-05-08   30= 30-Patología   31= 31-Inmunohistoquímica   46= 46-Inhibición hemaglutinación   55= 55-Cultivo   58= 58-Antigenemia --> item eliminado desde version V01_2020-05-08   76=76-IFI   92= 92-Hemocultivo     Los siguientes son items nuevos desde version V01_2020-05-08 :   2 = 2 - IgM   3 = 3 - IgG   F3 = F3 - Determinación de antígenos   H9 = H9 - IgG , IgM   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABPRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra:   1= 1-Sangre total   3= 3-Hisopado nasofaríngeo   4= 4-Tejido   8= 8-Aspirado nasofaríngeo   9= 9- Lavado nasal --> item eliminado desde version V01_2020-05-08   11=  11-Lavado broncoalveolar (antes)  / 11 - Otros líquidos estériles (ahora) -->  item modificado desde version V01_2020-05-08   22= 22-Lavado bronquial ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'LABMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usó antibiótico en la última semana:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTIBIOTICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se tomó radiografía de tórax:   1= 1. Infiltrado alveolar o neumonía   2= 2. Infiltrados intersticiales   3= 3. Infiltrados basales en vidrio esmerilado 4= 4. Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'RADIOTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anteotro:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antetuberculos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTTUBERCULOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antefumador:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEFUMADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antemedica:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anterenal:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTERENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anteobesidad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEOBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antemalnutri:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEMALNUTRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecancer:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTECANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecardiaca:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTECARDIACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'AnteVIH:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antediabetes:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEDIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anteepoc:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEEPOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anteasma:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'ANTEASMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Influenza estacional:   1=Si   2=No   3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VACUINFLUENZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sin_fatiga:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_FATIGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sin_Dificultarespira:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_DIFICULTARESPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sin_Odinofagia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_ODINOFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sin_fiebre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sin_tos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'SIN_TOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tuvo contacto estrcho en los últimos 14 días con caso probable o confirmado con infección respiratoria por virus nuevo:   1=Si   2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'CONTACTO14DIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El viaje fue Internacional:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJETERINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El viaje fue en el territorio Nacional:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJETERNAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Viajó a áreas de circulación del virus nuevo:   1=Si   2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'VIAJOAREASVIRUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es trabajador de la salud u otro persona:   1=Si   2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA346', @level2type = N'COLUMN', @level2name = N'TRABADESALUD';

