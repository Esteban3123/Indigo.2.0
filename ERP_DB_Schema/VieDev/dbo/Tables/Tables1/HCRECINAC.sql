CREATE TABLE [dbo].[HCRECINAC] (
    [NUMCONSEC]                      INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]                      CHAR (9)                                                                         NOT NULL,
    [CODPROSAL]                      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [NUMEFOLIO]                      CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                      CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                      CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                      CHAR (10)                                                                        NOT NULL,
    [NUMHIJREG]                      INT                                                                              NOT NULL,
    [FECHANACIM]                     DATETIME                                                                         NOT NULL,
    [SEXRECNAC]                      CHAR (1)                                                                         NOT NULL,
    [VITANACIM]                      VARCHAR (50)                                                                     NOT NULL,
    [RECPERCEF]                      INT                                                                              NULL,
    [RECPERTOR]                      INT                                                                              NULL,
    [RECPERABD]                      INT                                                                              NULL,
    [TEMPERREC]                      CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "Temperature_Ofuscado", 0)')       NULL,
    [FRECARREC]                      CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "HeartRate_Ofuscado", 0)')         NULL,
    [FRERESREC]                      CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "HeartRespiratory_Ofuscado", 0)')  NULL,
    [APGAR1REC]                      INT                                                                              NULL,
    [APGAR5REC]                      INT                                                                              NULL,
    [APGAR10RN]                      INT                                                                              NULL,
    [ADAPRECNA]                      VARCHAR (50)                                                                     NULL,
    [TALLARECI]                      INT                                                                              NULL,
    [PESORECNA]                      NUMERIC (18, 3)                                                                  NULL,
    [ANOCABREC]                      BIT                                                                              NULL,
    [DESCABREC]                      VARCHAR (2000)                                                                   NULL,
    [ANOTORREC]                      BIT                                                                              NULL,
    [DESTORREC]                      VARCHAR (2000)                                                                   NULL,
    [ANOABDREC]                      BIT                                                                              NULL,
    [DESABDREC]                      VARCHAR (2000)                                                                   NULL,
    [ANOGEUREC]                      BIT                                                                              NULL,
    [DESGEUREC]                      VARCHAR (2000)                                                                   NULL,
    [ANOCADREC]                      BIT                                                                              NULL,
    [DESCADREC]                      VARCHAR (2000)                                                                   NULL,
    [ANOEXTREC]                      BIT                                                                              NULL,
    [DESEXTREC]                      VARCHAR (2000)                                                                   NULL,
    [ANONEUREC]                      BIT                                                                              NULL,
    [DESNEUREC]                      VARCHAR (2000)                                                                   NULL,
    [RNGRUPSAM]                      CHAR (2)                                                                         NULL,
    [RNRHSANGM]                      CHAR (1)                                                                         NULL,
    [DESRECNAC]                      VARCHAR (100)                                                                    NULL,
    [NUMSEMCAP]                      INT                                                                              NULL,
    [DESPESXAG]                      VARCHAR (100)                                                                    NULL,
    [PATOLOGIA]                      VARCHAR (1000)                                                                   NULL,
    [OBSERVACI]                      VARCHAR (2000)                                                                   NULL,
    [CAMCOOMBS]                      VARCHAR (10)                                                                     NULL,
    [SENSIBILI]                      VARCHAR (10)                                                                     NULL,
    [EDADGESNAC]                     INT                                                                              NULL,
    [FECHISPAC]                      DATETIME                                                                         NULL,
    [BALLARD]                        INT                                                                              NULL,
    [NUMINGRESHIJO]                  CHAR (10)                                                                        NULL,
    [IPCODPACIHIJO]                  VARCHAR (25)                                                                     NULL,
    [FECMUEPAC]                      DATETIME                                                                         NULL,
    [CODCAUMUE]                      CHAR (3)                                                                         NULL,
    [NUMCERDEF]                      CHAR (40) MASKED WITH (FUNCTION = 'partial(0, "DeathCertficate_Ofuscado", 0)')   NULL,
    [PreductalSaturation]            INT                                                                              NULL,
    [PostductalSaturation]           INT                                                                              NULL,
    [BloodPressure]                  NCHAR (100)                                                                      NULL,
    [ThyroidStimulatingHormone]      NUMERIC (4, 2)                                                                   NULL,
    [ExpandedScreening]              TINYINT                                                                          NULL,
    [CODESPECI]                      CHAR (3)                                                                         NULL,
    [BirthWeightClassification]      CHAR (3)                                                                         NULL,
    [CordClamping]                   CHAR (3)                                                                         NULL,
    [SkinToSkinContact]              CHAR (3)                                                                         NULL,
    [SkinToSkinContactBreastfeeding] BIT                                                                              NULL,
    [WhyNotSkinToSkinBreastfeeding]  VARCHAR (100)                                                                    NULL,
    [NewbornCareSequence]            VARCHAR (2000)                                                                   NULL,
    [NeonatalAdaptationObservations] VARCHAR (500)                                                                    NULL,
    [Meconium]                       BIT                                                                              NULL,
    [CordClampingReason]             VARCHAR (50)                                                                     NULL,
    CONSTRAINT [PK_HCRECINAC] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK_HCRECINAC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRECINAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCRECINAC_INCAUMUER] FOREIGN KEY ([CODCAUMUE]) REFERENCES [dbo].[INCAUMUER] ([CODCAUMUE]),
    CONSTRAINT [FK_HCRECINAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCRECINAC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCRECINAC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCRECINAC] NOCHECK CONSTRAINT [FK_HCRECINAC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCRECINAC] NOCHECK CONSTRAINT [FK_HCRECINAC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCRECINAC] NOCHECK CONSTRAINT [FK_HCRECINAC_INCAUMUER];


GO
ALTER TABLE [dbo].[HCRECINAC] NOCHECK CONSTRAINT [FK_HCRECINAC_INPACIENT];


GO
ALTER TABLE [dbo].[HCRECINAC] NOCHECK CONSTRAINT [FK_HCRECINAC_INPROFSAL];


GO
ALTER TABLE [dbo].[HCRECINAC] NOCHECK CONSTRAINT [FK_HCRECINAC_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCRECINAC_NUMINGRES]
    ON [dbo].[HCRECINAC]([NUMINGRES] ASC)
    INCLUDE([IPCODPACI], [IPCODPACIHIJO], [NUMINGRESHIJO], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCRECINAC_IPCODPACIHIJO]
    ON [dbo].[HCRECINAC]([IPCODPACIHIJO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena campo de texto de Por que pinzamiento precoz solo se guarda(Habilita) si el campo Pinzamiento cordon se selecciona la opcion 2 = ''Precoz''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'CordClampingReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el meconium   1 = Si    0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'Meconium';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el dato del campo "Observaciones adaptación neonatal" de la HC recien nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'NeonatalAdaptationObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el dato del campo "Descripcion de la secuencia de atencion al recien nacido en sala de partos" de la HC recien nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'NewbornCareSequence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el dato del campo "¿Por que?" cuando se selecciona "No" en el campo "lactancia al contacto piel a piel" de la HC recien nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'WhyNotSkinToSkinBreastfeeding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el dato del campo "lactancia al contacto piel a piel" de la HC recien nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'SkinToSkinContactBreastfeeding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el dato del campo "Contacto piel a piel" de la HC recien nacido   1 = ''Si''  2 = ''No''  3 = ''No aplica''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'SkinToSkinContact';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el dato del campo "Pinzamiento del cordón" de la HC recien nacido   1 = ''Habitual''  2 = ''Precoz''  3 = ''Temprano''  4 = ''Tardío''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'CordClamping';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el dato del campo "Clasificación del peso al nacer" de la HC recien nacido   1 = ''Alto peso al nacer (APN): Mayor de 4000 g''  2 = ''Peso normal al nacer (PNN): Entre 2500 g y 4000 g''  3 = ''Peso bajo al nacer (PBN): Menos de 2500 g''  4 = ''Muy bajo peso al nacer (MBPS): Menos de 1500 g''  5 = ''Extremado bajo peso al nacer (EBPN): Menos de 1000 g''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'BirthWeightClassification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el codigo de la especialidad con la que se firmo la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '"Tamizaje ampliado" con las opciones "Si" = 1 y "No" = 2 (** Campo obsoleto por el PBI 17133 -  Refactoring historia "Recién nacido" - page "Datos de los recién nacidos -- 08/05/2024 --**)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'ExpandedScreening';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '"TSH (mUI/L)"  hormona estimulante de la tiroides (** Campo obsoleto por el PBI 17133 -  Refactoring historia "Recién nacido" - page "Datos de los recién nacidos -- 08/05/2024 --**)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'ThyroidStimulatingHormone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '"Tensión arterial (mmHg)" "MSD" = Miembro superior derecho , "MSI" = Miembro superior izquierdo , "MID"= Miembro inferior derecho, "MII" = Miembro inferior izquierdo (** Campo obsoleto por el PBI 17689 - 3. Ajuste sección "Valoración objetiva" HC "Recién nacido" -- 17/05/2024 --**)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'BloodPressure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '"Saturación post-ductal" de 3 dígitos sin decimales (** Campo obsoleto por el PBI 17689 - 3. Ajuste sección "Valoración objetiva" HC "Recién nacido" -- 17/05/2024 --**)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'PostductalSaturation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '"Saturación pre-ductal" de 3 dígitos sin decimales (** Campo obsoleto por el PBI 17689 - 3. Ajuste sección "Valoración objetiva" HC "Recién nacido" -- 17/05/2024 --**)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'PreductalSaturation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo numérico  valor minimo -10  valor maximo 50', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'BALLARD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sensibilizada cuando el RH es negativo (** Campo obsoleto por el PBI 17133 -  Refactoring historia "Recién nacido" - page "Datos de los recién nacidos -- 08/05/2024 --**)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'SENSIBILI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Coombs - cuando el RH es negativo (** Campo obsoleto por el PBI 17133 -  Refactoring historia "Recién nacido" - page "Datos de los recién nacidos -- 08/05/2024 --**)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'CAMCOOMBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RH:  +  - (** Campo obsoleto por el PBI 17133 -  Refactoring historia "Recién nacido" - page "Datos de los recién nacidos -- 08/05/2024 --**)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'RNRHSANGM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo Sanguineo:   A  B  AB  O  (** Campo obsoleto por el PBI 17133 -  Refactoring historia "Recién nacido" - page "Datos de los recién nacidos -- 08/05/2024 --**)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'RNGRUPSAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo del Recien nacido:  1=Masculino  2=Femenino  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECINAC', @level2type = N'COLUMN', @level2name = N'SEXRECNAC';

