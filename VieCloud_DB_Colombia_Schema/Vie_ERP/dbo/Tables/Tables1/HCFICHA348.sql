CREATE TABLE [dbo].[HCFICHA348] (
    [ID]                  INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT            NOT NULL,
    [TRABAJADORSALUD]     BIT            NULL,
    [DETERIOROCLI]        BIT            NULL,
    [CASOBROTE]           BIT            NULL,
    [VIAJO14DIASPRE]      BIT            NULL,
    [VIAJETERNAL]         BIT            NULL,
    [LUGARVIAJENAL]       VARCHAR (50)   NULL,
    [VIAJETERINTER]       BIT            NULL,
    [LUGARVIAJEINTER]     VARCHAR (50)   NULL,
    [VACUINFLUENZA]       INT            NULL,
    [DOSIS]               NUMERIC (18)   NULL,
    [SEMGESTACION]        NUMERIC (18)   NULL,
    [ANTEASMA]            BIT            NULL,
    [ANTEEPOC]            BIT            NULL,
    [ANTEDIABETES]        BIT            NULL,
    [ANTEVIH]             BIT            NULL,
    [ANTECARDIACA]        BIT            NULL,
    [ANTECANCER]          BIT            NULL,
    [ANTEMALNUTRI]        BIT            NULL,
    [ANTEOBESIDAD]        BIT            NULL,
    [ANTERENAL]           BIT            NULL,
    [ANTEMEDICA]          BIT            NULL,
    [ANTEFUMADOR]         BIT            NULL,
    [ANTEOTRO]            BIT            NULL,
    [ANTETOS]             BIT            NULL,
    [ANTEFIEBRE]          BIT            NULL,
    [ANTEGARGANTA]        BIT            NULL,
    [ANTERINORREA]        BIT            NULL,
    [ANTECONJUN]          BIT            NULL,
    [ANTECEFALEA]         BIT            NULL,
    [ANTERESPIRA]         BIT            NULL,
    [ANTEDIARREA]         BIT            NULL,
    [OTROSANTE]           VARCHAR (1000) NULL,
    [RADIOTORAX]          INT            NULL,
    [ANTIBIOTICOS]        BIT            NULL,
    [ANTIVIRALES]         BIT            NULL,
    [FECHANTIVIR]         DATE           NULL,
    [SERVHOSPITA]         INT            NULL,
    [FECHAINGREUCI]       DATE           NULL,
    [COMDERRAMEPLE]       BIT            NULL,
    [COMDERRAMEPER]       BIT            NULL,
    [COMMIOCARD]          BIT            NULL,
    [COMSEPTICE]          BIT            NULL,
    [COMRESPIRA]          BIT            NULL,
    [COMPOTRA]            BIT            NULL,
    [LABFECHATOMA1]       DATE           NULL,
    [LABFECHARECEP1]      DATE           NULL,
    [LABMUESTRA1]         INT            NULL,
    [LABPRUEBA1]          VARCHAR (3)    NULL,
    [LABAGENTE1]          VARCHAR (3)    NULL,
    [LABRESULTADO1]       INT            NULL,
    [LABFECHARECEP11]     DATETIME       NULL,
    [LABVALOREGIS1]       VARCHAR (200)  NULL,
    [LABFECHATOMA2]       DATE           NULL,
    [LABFECHARECEP2]      DATE           NULL,
    [LABMUESTRA2]         INT            NULL,
    [LABPRUEBA2]          VARCHAR (3)    NULL,
    [LABAGENTE2]          VARCHAR (3)    NULL,
    [LABRESULTADO2]       INT            NULL,
    [LABFECHARECEP22]     DATE           NULL,
    [LABVALOREGIS2]       VARCHAR (200)  NULL,
    [CODDIAGNO]           CHAR (4)       NULL,
    [NEUMOCOCO]           INT            NULL,
    [DOSISNEUMO]          NUMERIC (18)   NULL,
    [VERSION]             VARCHAR (20)   NULL,
    [JSON]                VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_HCFICHA348] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA348_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA348_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA348] NOCHECK CONSTRAINT [CK_HCFICHA348_JSON];




GO
ALTER TABLE [dbo].[HCFICHA348] NOCHECK CONSTRAINT [CK_HCFICHA348_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde V01_2020-03-06 ) =    CONTACTO_ANIMAL = booleano (Contacto estrecho ave o cerdo ultimos 14 días)  CONTACTO_PERSONA = booleano (Contacto estrecho persona ultimos 14 días)   TIENE_TOS = booleano (Paciente Tiene Tos)   TIENE_FIEBRE = booleano (Paciente Tiene Fiebre)    PAIS_TEXTO = texto (Nombre Pais internacional viaje)    PAIS_CODIGO = texto (Codigo Pais internacional viaje, 3 digitos)    ANTE_TROMBOCITOPENIA = booleano (antecedente paciente trombocitopenia)    CONTEO_PLAQUETAS = número (conteo de plaquetas)    RADIOGRAFIA_HALLAZGO = número (Radiografia hallazgo encontrado: 1=infiltrado alveolar o nuemonia, 2=infiltrados intersticiales , 3= ninguno)  COVID19 = booleano (Caso probable o confirmado de IRAG por virus nuevo (COVID-19))  HTA = booleano (HTA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'DOSISNEUMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Streptococcus pneumoniae (neumococo):   1=Si   2=No   3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'NEUMOCOCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABVALOREGIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha recepción:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   6= Valor registrado   12= Contaminado con hongos   13= Muestra escasa de celulas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABRESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente:   8= 8-Otro   16= 16-Adenovirus   18=18-Virus sincitial respiratorio   22= 22-Haemophilus influenzae   24= 24-Streptococcus pneumoniae   40= 40-Influenza A   41= 41-Influenza   42= 42-Parainfluenza   43= 43-Parainfluenza 2   44= 44-Parainfluenza 3   56= 56-Enterovirus   59= 59-Influenza A(H1N1) pdm09   64= 64-Influenza A no subtipificable   76= 76-Bocavirus   77= 77-Coronavirus   78= 78-Metaneumovirus   79= 79-Rinovirus   84= 84-virus respiratorios   1Q= 1Q-Coronavirus causante del síndrome respiratorio de oriente medio (MERS-CoV)   1R= 1R-Coronavirus subtipo 229e   1S= 1S-Coronavirus subtipo HKU1   1T= 1T-Coronavirus subtipo NL63   1U= 1U-Coronavirus subtipo OC43   1V= 1V-Influenza A(H3N2)   1W= 1W-Parainfluenza tipo 4   2H = 2H-Coronavirus subtipo COVID19 --> item nuevo desde version ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABAGENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:   4=4-PCR   E1=E1-Aislamiento viral   6=6-Otra   30=30-Patología  31=31-Inmunohistoquímica   46=46-Inhibición hemaglutinación   55=55-Cultivo   58=58-Antigenemia  76=76-IFI   92=92-Hemocultivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABPRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra:   1= 1-Sangre total   3= 3-Hisopado nasofaríngeo   4= 4-Tejido   9 = 9-Lavado Nasal --> item nuevo desde version ''V01_2020-03-06''  11= 11-Otros líquidos esteriles (antes) / 11-Lavado broncoalveolar (ahora) --> item modificado desde version ''V01_2020-03-06''  22= 22-Lavado bronquial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de toma:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABFECHATOMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABVALOREGIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha recepción:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   6= Valor registrado   12= Contaminado con hongos   13= Muestra escasa de celulas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABRESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente:   8= 8-Otro   16= 16-Adenovirus   18=18-Virus sincitial respiratorio   22= 22-Haemophilus influenzae   24= 24-Streptococcus pneumoniae   40= 40-Influenza A   41= 41-Influenza   42= 42-Parainfluenza   43= 43-Parainfluenza 2   44= 44-Parainfluenza 3   56= 56-Enterovirus   59= 59-Influenza A(H1N1) pdm09   64= 64-Influenza A no subtipificable   76= 76-Bocavirus   77= 77-Coronavirus   78= 78-Metaneumovirus   79= 79-Rinovirus   84= 84-virus respiratorios   1Q= 1Q-Coronavirus causante del síndrome respiratorio de oriente medio (MERS-CoV)   1R= 1R-Coronavirus subtipo 229e   1S= 1S-Coronavirus subtipo HKU1   1T= 1T-Coronavirus subtipo NL63   1U= 1U-Coronavirus subtipo OC43   1V= 1V-Influenza A(H3N2)   1W= 1W-Parainfluenza tipo 4   2H = 2H-Coronavirus subtipo COVID19 --> item nuevo desde version ''V01_2020-03-06''   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABAGENTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:   4=4-PCR   E1=E1-Aislamiento viral   6=6-Otra   30=30-Patología  31=31-Inmunohistoquímica   46=46-Inhibición hemaglutinación   55=55-Cultivo   58=58-Antigenemia  76=76-IFI   92=92-Hemocultivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABPRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra:   1= 1-Sangre total   3= 3-Hisopado nasofaríngeo   4= 4-Tejido   9 = 9-Lavado Nasal --> item nuevo desde version ''V01_2020-03-06''  11= 11-Otros líquidos esteriles (antes) / 11-Lavado broncoalveolar (ahora) --> item modificado desde version ''V01_2020-03-06''  22= 22-Lavado bronquial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABFECHARECEP1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LABFECHATOMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Compotra:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'COMPOTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comrespira:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'COMRESPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comseptice:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'COMSEPTICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Commiocard:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'COMMIOCARD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comderrameper:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'COMDERRAMEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comderrameple:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'COMDERRAMEPLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha ingreso UCI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'FECHAINGREUCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio en que se hospitalizó:   1=Hospitalización general   2=Unidad de cuidados intermedios  3=unidad de cuidados intensivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'SERVHOSPITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicio antiviral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'FECHANTIVIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usó antivirales en la última semana:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTIVIRALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usó antibiótico en la última semana:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTIBIOTICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se tomó radiografía de tórax:   1=Si   2=No   3=Desconocido    --> campo obsoleto desde version ''V01_2020-03-06''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'RADIOTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuáles otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'OTROSANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antediarrea:   True=Si   False=No    --> campo obsoleto desde version ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEDIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anterespira:   True=Si   False=No    --> campo obsoleto desde version ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTERESPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecefalea:   True=Si   False=No    --> campo obsoleto desde version ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTECEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anteconjun:   True=Si   False=No    --> campo obsoleto desde version ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTECONJUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anterinorrea:   True=Si   False=No    --> campo obsoleto desde version ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTERINORREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antegarganta:   True=Si   False=No    --> campo obsoleto desde version ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEGARGANTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antefiebre:   True=Si   False=No    --> campo obsoleto desde version ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEFIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antetos:   True=Si   False=No     --> campo obsoleto desde version ''V01_2020-03-06''   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTETOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anteotro:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'AnteFumador:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEFUMADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antemedica:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anterenal:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTERENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anteobesidad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEOBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antemalnutri:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEMALNUTRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecancer:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTECANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecardiaca:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTECARDIACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'antevih:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antediabetes:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEDIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anteepoc:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEEPOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anteasma:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ANTEASMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Semanas gestacion    --> campo obsoleto desde version ''V01_2020-03-06''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'SEMGESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'DOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Influenza estacional:   1=Si   2=No   3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'VACUINFLUENZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Donde: (lugar internacional)    --> campo obsoleto desde version ''V01_2020-03-06'' , ahora se utiliza columnas nuevas PAIS_TEXTO y PAIS_CODIGO en campo JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LUGARVIAJEINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El viaje fue Internacional:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'VIAJETERINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Donde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'LUGARVIAJENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El viaje fue en el territorio Nacional:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'VIAJETERNAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Viajo 14 dias pre:  True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'VIAJO14DIASPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Casabrote:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'CASOBROTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deteriorocli:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'DETERIOROCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es trabajador de la salud u otro persona:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'TRABAJADORSALUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha epidemiológica 348 para notificación de casos de influenza y otras infecciones respiratorias agudas graves (IRAG). Registra antecedentes clínicos, síntomas, factores de riesgo, vacunación, complicaciones, resultados de laboratorio y datos de viaje del paciente notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA348';
