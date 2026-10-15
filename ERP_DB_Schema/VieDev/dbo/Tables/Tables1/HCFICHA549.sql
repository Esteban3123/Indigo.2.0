CREATE TABLE [dbo].[HCFICHA549] (
    [ID]                              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION]             INT           NOT NULL,
    [INGRESAREMI]                     BIT           NULL,
    [TIEMTRAMITE]                     VARCHAR (6)   NULL,
    [INSTITUCIONREF1]                 VARCHAR (500) NULL,
    [INSTITUCIONREF2]                 VARCHAR (500) NULL,
    [NUMGESTA]                        INT           NULL,
    [PARVAGINALES]                    INT           NULL,
    [CESAREAS]                        INT           NULL,
    [ABORTOS]                         INT           NULL,
    [MOLAS]                           INT           NULL,
    [ECTOPICOS]                       INT           NULL,
    [MUERTOS]                         INT           NULL,
    [VIVOS]                           INT           NULL,
    [FECHULTMENS]                     DATE          NULL,
    [CONTPRENATALES]                  INT           NULL,
    [SEMAINICIOCPN]                   INT           NULL,
    [TERMGESTACION]                   INT           NULL,
    [MOMOCUGESTACION]                 INT           NULL,
    [ECLAMPSIA]                       BIT           NULL,
    [SEPSIS]                          BIT           NULL,
    [HEMORRAGIA]                      BIT           NULL,
    [PREECLAMPSIA]                    BIT           NULL,
    [RUPTUTERINA]                     BIT           NULL,
    [ABOSEPTICO]                      BIT           NULL,
    [EMBAECTOPICO]                    BIT           NULL,
    [AUTOINMUNE]                      BIT           NULL,
    [HEMATOLOGICA]                    BIT           NULL,
    [ONCOLOGIA]                       BIT           NULL,
    [ENDOCRINO]                       BIT           NULL,
    [RENALES]                         BIT           NULL,
    [GASTROINTESTINALES]              BIT           NULL,
    [EVETROMBOEMBOLI]                 BIT           NULL,
    [CARDIOCEREBROVASC]               BIT           NULL,
    [OTRAS]                           BIT           NULL,
    [CARDIACA]                        BIT           NULL,
    [VASCULAR]                        BIT           NULL,
    [RENAL]                           BIT           NULL,
    [HEPATICA]                        BIT           NULL,
    [METABOLICA]                      BIT           NULL,
    [CEREBRAL]                        BIT           NULL,
    [RESPIRATORIA]                    BIT           NULL,
    [COAGULACION]                     BIT           NULL,
    [TOTALCRITERIOS]                  INT           NULL,
    [INGRESOUCI]                      BIT           NULL,
    [CIRUADICIONAL]                   BIT           NULL,
    [TRANSFUSION]                     BIT           NULL,
    [ACCIDENTE]                       BIT           NULL,
    [INTOXACCIDENTAL]                 BIT           NULL,
    [INTESUICIDA]                     BIT           NULL,
    [VICTVIOLENCIA]                   BIT           NULL,
    [OTROEVENSALUDPUB]                BIT           NULL,
    [CUALOTROEVENSALUDPUB]            VARCHAR (500) NULL,
    [DIASESTAHOSPITA]                 INT           NULL,
    [DIASESTANCIAUCI]                 INT           NULL,
    [CIRUADICIONAL1]                  INT           NULL,
    [CODDIAGNO_CAUSAPRINCI]           CHAR (4)      NULL,
    [CAUPRINTRASTORNOS]               BIT           NULL,
    [CAUPRINCOMPLIHEMORRA]            BIT           NULL,
    [CAUPRINCOMPLIABORTO]             BIT           NULL,
    [CAUPRINSEPSISOBSTETRICO]         BIT           NULL,
    [CAUPRINSEPSISNOOBSTETRICO]       BIT           NULL,
    [CAUPRINSEPSISPULMONAR]           BIT           NULL,
    [CAUPRINPREEXISTENTE]             BIT           NULL,
    [CAUPRINOTRACAUSA]                BIT           NULL,
    [CODDIAGNO_CAUSAASOCIADA1]        CHAR (4)      NULL,
    [CODDIAGNO_CAUSAASOCIADA2]        CHAR (4)      NULL,
    [CODDIAGNO_CAUSAASOCIADA3]        CHAR (4)      NULL,
    [FECHAEGRESO]                     DATE          NULL,
    [CODDIAGNO]                       CHAR (4)      NULL,
    [EGRESO]                          INT           NULL,
    [ABORTOSEPTICO]                   BIT           NULL,
    [ENFERMEDADMOLAR]                 BIT           NULL,
    [EMBARAZOECTOPICO]                BIT           NULL,
    [ENFAUTOINMUNE]                   BIT           NULL,
    [ENFGASTROINTESTINAL]             BIT           NULL,
    [EVENTOS]                         BIT           NULL,
    [ENFCARDIOVASCULAR]               BIT           NULL,
    [OTRASENFERESPECIFICARELACIONADA] BIT           NULL,
    [VERSION]                         VARCHAR (20)  NULL,
    [JSON]                            VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA549] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA549_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA549_INDIAGNOS] FOREIGN KEY ([CODDIAGNO_CAUSAPRINCI]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCFICHA549_INDIAGNOS1] FOREIGN KEY ([CODDIAGNO_CAUSAASOCIADA1]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCFICHA549_INDIAGNOS2] FOREIGN KEY ([CODDIAGNO_CAUSAASOCIADA2]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCFICHA549_INDIAGNOS3] FOREIGN KEY ([CODDIAGNO_CAUSAASOCIADA3]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCFICHA549] NOCHECK CONSTRAINT [CK_HCFICHA549_JSON];


GO
ALTER TABLE [dbo].[HCFICHA549] NOCHECK CONSTRAINT [FK_HCFICHA549_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCFICHA549] NOCHECK CONSTRAINT [FK_HCFICHA549_INDIAGNOS1];


GO
ALTER TABLE [dbo].[HCFICHA549] NOCHECK CONSTRAINT [FK_HCFICHA549_INDIAGNOS2];


GO
ALTER TABLE [dbo].[HCFICHA549] NOCHECK CONSTRAINT [FK_HCFICHA549_INDIAGNOS3];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde V01_2020-03-06 ) =    CIRUADICIONAL1_CUAL : texto (otra cirugia adicional #1),    CIRUADICIONAL2_TIPO: número (mismas opciones de CIRUADICIONAL1 para tipo cirugia adicional #2),    CIRUADICIONAL2_CUAL: texto (otra cirugia adicional #2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda otras enfermedades especifica relacionada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'OTRASENFERESPECIFICARELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la enfcardiovascular true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ENFCARDIOVASCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda los eventos true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'EVENTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la enfermedad gastroinstestianal true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ENFGASTROINTESTINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la enfautoinmune true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ENFAUTOINMUNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el embarazoectopico true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'EMBARAZOECTOPICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda molar true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ENFERMEDADMOLAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el ABOSEPTICO (terminación de la gestiación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ABORTOSEPTICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'FECHAEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico causa asociada 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CODDIAGNO_CAUSAASOCIADA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico causa asociada 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CODDIAGNO_CAUSAASOCIADA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico causa asociada 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CODDIAGNO_CAUSAASOCIADA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda true or false de otra causa (Causa principal agrupada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CAUPRINOTRACAUSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda true or false de enf. preexistente que se complica (Causa principal agrupada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CAUPRINPREEXISTENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda true or false de sepsis de origen pulmonar (Causa principal agrupada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CAUPRINSEPSISPULMONAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda true or false de sepsis de origen no obstétrico (Causa principal agrupada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CAUPRINSEPSISNOOBSTETRICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda true or false de sepsis de origen obstétrico (Causa principal agrupada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CAUPRINSEPSISOBSTETRICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda true or false de complicaciones de aborto (Causa principal agrupada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CAUPRINCOMPLIABORTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda true or false de complicaciones hemorragicas  (Causa principal agrupada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CAUPRINCOMPLIHEMORRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda true or false de transtornos (Causa principal agrupada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CAUPRINTRASTORNOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico causa principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CODDIAGNO_CAUSAPRINCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Cirugia Adicional #1 -->  1 = Histerectomía   2 = Laparotomía   3 = Legrado  4 = Otra   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CIRUADICIONAL1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el dia estancia UCI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'DIASESTANCIAUCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el día estancia hospital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'DIASESTAHOSPITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda cual otro evento salud publica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CUALOTROEVENSALUDPUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda otroeventosaludpublica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'OTROEVENSALUDPUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda victimas de violencia armada (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'VICTVIOLENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda el intento suicida (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'INTESUICIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la intoxicación accidental (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'INTOXACCIDENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el accidente (terminación de la gestiación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ACCIDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'TRANSFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la cirugía adicional true or false (criterios de inclusión relacionados con el manejo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CIRUADICIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ingreso en la UCI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'INGRESOUCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el total de criterios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'TOTALCRITERIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda respiratoria (1.si 2. no)(relacionado con disfunción de organo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'RESPIRATORIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda true or false de cerebral (Relacionada con difunción de órgano)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CEREBRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda metabolica (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'METABOLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la hepatica true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'HEPATICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el renales (1.si 2. no)(relacionado con disfunción de organo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'RENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el cardiovascular (1. si ó 2. no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'VASCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda otras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'OTRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el cardio cerebro vascular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CARDIOCEREBROVASC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el evento tromboembolia true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'EVETROMBOEMBOLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda los gastroinstestinales true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'GASTROINTESTINALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda renales (1.si 2. no)(relacionado con disfunción de organo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'RENALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el endocrino true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ENDOCRINO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda oncologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ONCOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la hematologia true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'HEMATOLOGICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el AUTOINMUNE (terminación de la gestiación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'AUTOINMUNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el embaectopico true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'EMBAECTOPICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el abortoseptico (terminación de la gestación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ABOSEPTICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la ruptura uterina (1.si 2. no)(relacionado con enfermedad especifica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'RUPTUTERINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la hemorragia true or false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'HEMORRAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la sepsis (1.si 2. no)(relacionado con enfermedad especifica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'SEPSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la eclampsia true or false (relacionadas con enfermedad especifica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ECLAMPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el momento de ocurrencia con relación a terminación de gestación (antes, durante, despues)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'MOMOCUGESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el termino de gestación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'TERMGESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda las semanas al inicio CPN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'SEMAINICIOCPN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de controles prenatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CONTPRENATALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha ultima menstruación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'FECHULTMENS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el numero de vivos (caracteristicas maternas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'VIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guardan los Muertos (caracteristicas maternas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'MUERTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ectopicos (caracteristicas maternas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ECTOPICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda las molas (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'MOLAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Aborto (terminación de la gestación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ABORTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la cesarea (terminación de la gestación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'CESAREAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda en numero de partos vaginales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'PARVAGINALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el número de gestaciones (caracteristicas maternas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'NUMGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la institución de referencia 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'INSTITUCIONREF2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la institución de referencia 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'INSTITUCIONREF1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tiempo del tramite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'TIEMTRAMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ingreso de la remisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'INGRESAREMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el id de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA549', @level2type = N'COLUMN', @level2name = N'ID';

