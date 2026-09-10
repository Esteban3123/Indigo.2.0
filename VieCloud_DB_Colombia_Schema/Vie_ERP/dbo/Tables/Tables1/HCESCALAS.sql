CREATE TABLE [dbo].[HCESCALAS] (
    [ID]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO]        NCHAR (10)                                                                       NULL,
    [NUMINGRES]        CHAR (10)                                                                        NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHAREGISTRO]    DATETIME                                                                         NOT NULL,
    [TIPOESCALA]       INT                                                                              NOT NULL,
    [RESULTADO]        INT                                                                              NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODCENATE]        CHAR (10)                                                                        NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NULL,
    [HCCTRNOTEID]      INT                                                                              NULL,
    [RESULTADODECIMAL] DECIMAL (5, 2)                                                                   NULL,
    [CODESPECI]        CHAR (3)                                                                         NULL,
    [CONSECTRIAGEU]    CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCESCALAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALAS_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCESCALAS_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCESCALAS_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCESCALAS].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCESCALAS].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_HCESCALAS_NUMEFOLIO_NUMINGRES_TIPOESCALA]
    ON [dbo].[HCESCALAS]([NUMEFOLIO] ASC, [NUMINGRES] ASC, [TIPOESCALA] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCESCALAS_NUMINGRES_IPCODPACI_TIPOESCALA]
    ON [dbo].[HCESCALAS]([NUMINGRES] ASC, [IPCODPACI] ASC, [TIPOESCALA] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCESCALAS_IPCODPACI_TIPOESCALA]
    ON [dbo].[HCESCALAS]([IPCODPACI] ASC, [TIPOESCALA] ASC)
    INCLUDE([FECHAREGISTRO], [NUMEFOLIO], [NUMINGRES], [RESULTADO], [RESULTADODECIMAL]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo de triage (CHAR 20), identificador de triaje urgencia/prioridad clínica asociado al registro de escala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CONSECTRIAGEU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Consecutivo de Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CONSECTRIAGEU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CONSECTRIAGEU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica (CHAR 3, FK→INESPECIA) del profesional que diligenciá la escala (medicina general, urgencia, pediatría, psiquiatría, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad del medico o enfermera de diligencio la escala. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado decimal (DECIMAL 5,2) de la escala cuando el valor no es entero; complementa RESULTADO para escalas con puntajes fraccionarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'RESULTADODECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda resultado de escala en dato decimal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'RESULTADODECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'RESULTADODECIMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de nota de enfermería (INT, referencia a HCCTRNOTE.ID) asociada al registro de escala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'HCCTRNOTEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de nota de enfermeria HCCTRNOTE:ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'HCCTRNOTEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'HCCTRNOTEID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (CHAR 10, FK→INUNIFUNC), servicio/departamento/área clínica donde se aplica la escala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, FK→ADCENATEN), institución/clínica/hospital donde se registra la escala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (CHAR 20, PII Ofuscado) que diligenciá/registró la escala (médico, enfermero, especialista).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Profesional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado numérico entero de la escala (INT NULL); NULL cuando escala tiene tabla detalle con múltiples resultados o resultado es decimal (ver RESULTADODECIMAL).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que permita NULL, cuando este campo este NULL significa que la escala que se ha diligenciado tiene una tabla detalle y por ende la Escala tiene varios resultados ó porque el resultado es un decimal y por ende va en otra columna que se llama RESULTADODECIMAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de escala clínica (INT enumeración 0-94): CAGE, APGAR, EDPS, Biopsicosocial, Tanner, Wagner, Glasgow, SOFA, Barthel, Norton, MNA, NEWS, NIHSS, FLACC, entre otras 80+ escalas de valoración clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Escala (Enumeración Escala) :   - NoAplica = 0   - EscalaCAGE = 1   - EscalaAPGAR_Familiar = 2   - EscalaEDPS = 3   - EscalaBiopsicosocial = 4   - EscalaTamizajeViolenciaDomestica = 5   - EscalaRiesgoFramingham = 6   - EscalaMorisky = 7   - TestFINDRISC = 8   - TestMiniMental = 9   - TestDependenciaNicotina = 10   - EscalaTannerDesarrolloMamarioMujer = 11   - EscalaTannerDesarrolloVelloPubianoMujer = 12   - EscalaTannerDesarrolloGenitalHombre = 13   - EscalaTannerDesarrolloVelloPubianoHombre = 14   - EscalaWagner = 15   - EscalaModificadaDisnea = 16   - EscalaCAT_COPD_AssessmentTest = 17   - Exacerbaciones = 18   - ClasificacionEPOC = 19 ''''''''Guarda Detalle   - TestGoodenough = 20   - GOLD_EPOC = 21   - EscalaAbreviadaDesarrollo = 22 ''''''''Guarda Detalle   - EscalaTISS_28 = 23   - Escala_Branden = 24   - Escala_ApacheII = 25   - Escala_Karnosfky = 26   - Escala_Ecog = 27   - Escala_Nems = 28   - Escala_Glasgow_Mayor5Anos = 29 ''''''''Se guarda en otra tabla   - Escala_Glasgow_de1a5Anos = 30 ''''''''Se guarda en otra tabla   - Escala_Glasgow_Menor1Ano = 31 ''''''''Se guarda en otra tabla   - Escala_SOFA = 32   - Escala_Charlson = 33   - Escala_SAPS3 = 34   - Escala_Barthel = 35   - Escala_Morse = 36   - Escala_Macdems = 37   - Escala_NSRAS = 38   - Escala_MSTS = 39   - Escala_Person = 40   - Escala_beck = 41   - Escala_Zarit = 42   - Escala_RQC = 43   - Escala_BacterianaSilness = 44   - Escala_VALE = 45 ''''''''Se guarda en otra tabla   - EscalaRASS = 46   - EscalaDown = 47   - EscalaNorton = 48   - EscalaVass = 49   - EscalaNutricion = 50   - EscalaSQR = 51 ''''''''Guarda detalle   - EscalaM_CHAT = 52   - EscalaWHOOLEY = 53   - EscalaAUDIT = 54   - EscalaLindaFried = 55   - EscalaLawton_Brody = 56   - EscalaGAD = 57   - EscalaMNA = 58   - EscalaMNASimplificada = 59   - EscalaAssist = 60   - EscalaNews = 61   - EscalaCHA2DS2_VASc = 62   - EscalaCRUSADE = 63   - EscalaHAS-BLED = 64   - EscalaHEMORR2HAGES = 65   - EscalaEUROSCOREII = 66   - EscalaNYHA  = 67   - EscalaKILLIP = 68   - EscalaPADUA = 69   - EscalaCAPRINI = 70   - EscalaMUST = 71   - Escala_STRONG_KIDS = 72   - Escala_VGSDEN (Valoracion Global Subjetiva del Estado Nutricional) =73   - Escala_TIMI_CEST = 74   - Escala_WELLS_TVP = 75   - Escala_WELLS_TEP = 76   - Escala_NPC = 77   - Escala_GRACE = 78   - Escala_TIMI_SEST = 79   - Escala_ANTHONISEN = 80   - Escala_DAS28 = 81   - EscalaMRS = 82   - EscalaHAQ = 83   - EscalaASPECT = 84   - EscalaAbreviadaDesarrolloV3 = 85   - EscalaIndiceOLeary = 86   - EscalaNIHSS = 87   - EscalaHumptyDumpty = 88   - EscalaRiesgoEnfermedadesPotencialTransmisibles = 89   - Escala_PIPP_R = 90    - Escala_FLACC = 91  - Escala_FPSR = 93  - Escala_NRS = 94    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de registro/diligenciamiento de la escala clínica en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII Ofuscado), cédula/identificación/documento de identidad del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso (CHAR 10) del paciente, referencia al episodio de atención/hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (NCHAR 10) asociado al registro de escala, identificador de documento de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del folio ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de registro de escala clínica en historia clínica electrónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de escalas clínicas y de valoración aplicadas a pacientes durante su atención. Almacena el resultado de herramientas de evaluación médica (escalas de dolor, riesgo, triaje, funcionalidad, entre otras) asociadas a un ingreso o folio de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAS';
