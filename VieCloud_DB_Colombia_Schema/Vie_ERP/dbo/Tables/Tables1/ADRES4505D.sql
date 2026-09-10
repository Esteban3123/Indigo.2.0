CREATE TABLE [dbo].[ADRES4505D] (
    [ID]           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDADRES4505C] INT             NOT NULL,
    [0]            INT             NULL,
    [1]            INT             NULL,
    [2]            VARCHAR (30)    NULL,
    [3]            VARCHAR (2)     NULL,
    [4]            VARCHAR (18)    NULL,
    [5]            VARCHAR (30)    NULL,
    [6]            VARCHAR (25)    NULL,
    [7]            VARCHAR (30)    NULL,
    [8]            VARCHAR (30)    NULL,
    [9]            DATE            NULL,
    [10]           VARCHAR (1)     NULL,
    [11]           INT             NULL,
    [12]           CHAR (4)        NULL,
    [13]           INT             NULL,
    [14]           INT             NULL,
    [15]           INT             NULL,
    [16]           INT             NULL,
    [17]           INT             NULL,
    [18]           INT             NULL,
    [19]           INT             NULL,
    [20]           INT             NULL,
    [21]           INT             NULL,
    [22]           INT             NULL,
    [23]           INT             NULL,
    [24]           INT             NULL,
    [25]           INT             NULL,
    [26]           INT             NULL,
    [27]           INT             NULL,
    [28]           INT             NULL,
    [29]           DATE            NULL,
    [30]           NUMERIC (18, 2) NULL,
    [31]           DATE            NULL,
    [32]           INT             NULL,
    [33]           DATE            NULL,
    [34]           INT             NULL,
    [35]           INT             NULL,
    [36]           INT             NULL,
    [37]           INT             NULL,
    [38]           INT             NULL,
    [39]           INT             NULL,
    [40]           INT             NULL,
    [41]           INT             NULL,
    [42]           INT             NULL,
    [43]           INT             NULL,
    [44]           INT             NULL,
    [45]           INT             NULL,
    [46]           INT             NULL,
    [47]           INT             NULL,
    [48]           INT             NULL,
    [49]           DATE            NULL,
    [50]           DATE            NULL,
    [51]           DATE            NULL,
    [52]           DATE            NULL,
    [53]           DATE            NULL,
    [54]           INT             NULL,
    [55]           DATE            NULL,
    [56]           DATE            NULL,
    [57]           INT             NULL,
    [58]           DATE            NULL,
    [59]           INT             NULL,
    [60]           INT             NULL,
    [61]           INT             NULL,
    [62]           DATE            NULL,
    [63]           DATE            NULL,
    [64]           DATE            NULL,
    [65]           DATE            NULL,
    [66]           DATE            NULL,
    [67]           DATE            NULL,
    [68]           DATE            NULL,
    [69]           DATE            NULL,
    [70]           INT             NULL,
    [71]           INT             NULL,
    [72]           DATE            NULL,
    [73]           DATE            NULL,
    [74]           INT             NULL,
    [75]           DATE            NULL,
    [76]           DATE            NULL,
    [77]           INT             NULL,
    [78]           DATE            NULL,
    [79]           VARCHAR (MAX)   NULL,
    [80]           DATE            NULL,
    [81]           VARCHAR (MAX)   NULL,
    [82]           DATE            NULL,
    [83]           VARCHAR (MAX)   NULL,
    [84]           DATE            NULL,
    [85]           VARCHAR (MAX)   NULL,
    [86]           INT             NULL,
    [87]           DATE            NULL,
    [88]           INT             NULL,
    [89]           INT             NULL,
    [90]           INT             NULL,
    [91]           DATE            NULL,
    [92]           VARCHAR (12)    NULL,
    [93]           DATE            NULL,
    [94]           INT             NULL,
    [95]           VARCHAR (12)    NULL,
    [96]           DATE            NULL,
    [97]           INT             NULL,
    [98]           VARCHAR (12)    NULL,
    [99]           DATE            NULL,
    [100]          DATE            NULL,
    [101]          INT             NULL,
    [102]          VARCHAR (12)    NULL,
    [103]          DATE            NULL,
    [104]          VARCHAR (MAX)   NULL,
    [105]          DATE            NULL,
    [106]          DATE            NULL,
    [107]          VARCHAR (MAX)   NULL,
    [108]          DATE            NULL,
    [109]          VARCHAR (MAX)   NULL,
    [110]          DATE            NULL,
    [111]          DATE            NULL,
    [112]          DATE            NULL,
    [113]          VARCHAR (MAX)   NULL,
    [114]          INT             NULL,
    [115]          INT             NULL,
    [116]          INT             NULL,
    [117]          INT             NULL,
    [118]          DATE            NULL,
    [REGINCONVE]   CHAR (2)        NULL,
    CONSTRAINT [PK_ADRES4505D] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro con Inconvenientes 1-Si , 2-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'REGINCONVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Terminación Tratamiento para Leishmaniasis - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'118';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tratamiento para  Lepra - 0- No aplica  1- Si recibe tratamiento pero aún no ha terminado  2- Si recibió tratamiento y ya lo terminó  16- No recibió tratamiento por tener una tradición que se lo impide  17- No recibió tratamiento por una condición de salud que se lo impide  18-  No recibió tratamiento por negación del usuario  19- No recibió tratamiento por que los datos de contacto del usuario no se encuentran actualizados  20- No recibió tratamiento por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'117';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tratamiento para  Sífilis Congénita - 0- No aplica  1- Si recibe tratamiento pero aún no ha terminado  2- Si recibió tratamiento y ya lo terminó  16- No recibió tratamiento por tener una tradición que se lo impide  17- No recibió tratamiento por una condición de salud que se lo impide  18-  No recibió tratamiento por negación del usuario  19- No recibió tratamiento por que los datos de contacto del usuario no se encuentran actualizados  20- No recibió tratamiento por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'116';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tratamiento para  Sífilis gestacional - 0- No aplica  1- Si recibe tratamiento pero aún no ha terminado  2- Si recibió tratamiento y ya lo terminó  16- No recibió tratamiento por tener una tradición que se lo impide  17- No recibió tratamiento por una condición de salud que se lo impide  18-  No recibió tratamiento por negación del usuario  19- No recibió tratamiento por que los datos de contacto del usuario no se encuentran actualizados  20- No recibió tratamiento por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'115';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tratamiento para Hipotiroidismo Congénito - 0- No aplica  1- Si recibe tratamiento pero aún no ha terminado  2- Si recibió tratamiento y ya lo terminó  16- No recibió tratamiento por tener una tradición que se lo impide  17- No recibió tratamiento por una condición de salud que se lo impide  18-  No recibió tratamiento por negación del usuario  19- No recibió tratamiento por que los datos de contacto del usuario no se encuentran actualizados  20- No recibió tratamiento por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'114';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Baciloscopia de  Diagnóstico - 1- Negativa  2- Positiva  3- En proceso  4- No  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'113';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Toma de Baciloscopia de Diagnóstico - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'112';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Toma de HDL - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'111';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Toma de  Microalbuminuria - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'110';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemoglobina  Glicosilada - Registre el dato reportado por el laboratorio  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'109';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Hemoglobina  Glicosilada - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'108';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Creatinina - Registre el dato reportado por el laboratorio.  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'107';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creatinina - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'106';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Toma de  Glicemia Basal - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no  actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'105';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemoglobina - Registre el dato reportado por el laboratorio.  Si no aplica registre 0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'104';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Toma de  Hemoglobina - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01- 01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'103';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de habilitación IPS donde se toma Biopsia Seno - Tabla REPS (Registro Especial de Prestadores de Servicios de Salud).  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'102';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado Biopsia  Seno - Registre:  1- Benigna  2- Atípica (Indeterminada)  3- Malignidad Sospechosa/Probable  4- Maligna  5- No Satisfactoria  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'101';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Resultado  Biopsia Seno - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'100';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Toma Biopsia  Seno por BACAF - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'99';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de habilitación IPS donde se toma Mamografía - Tabla REPS (Registro Especial de Prestadores de Servicios de Salud).  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'98';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado  Mamografía - Clasificación BIRADS Registre:  1- BIRADS 0: Necesidad de Nuevo Estudio Imagenológico o Mamograma previo para evaluación  2- BIRADS 1: Negativo  3- BIRADS 2: Hallazgos Benignos  4- BIRADS 3: Probablemente Benigno  5- BIRADS 4: Anormalidad Sospechosa  6- BIRADS 5: Altamente Sospechoso de Malignidad  7- BIRADS 6: Malignidad por Biopsia conocida  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'97';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Mamografía - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01- 01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'96';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de habilitación IPS donde se toma Biopsia Cervical - Tabla REPS (Registro Especial de Prestadores de Servicios de Salud).  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'95';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de Biopsia  Cervical - 1- Negativo para Neoplasia  2- Infección por VPH  3- NIC de Bajo Grado - NIC I  4- NIC de Alto Grado: NIC II - NIC III  5- Neoplasia Micro infiltrante: Escamocelular o  Adenocarcinoma  6- Neoplasia Infiltrante: Escamocelular o Adenocarcinoma.  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'94';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Biopsia  Cervical - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'93';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de habilitación IPS donde se toma Colposcopia - Tabla REPS (Registro Especial de Prestadores de Servicios de Salud).  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'92';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Colposcopia - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'91';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de habilitación IPS donde se toma Citología Cervicouterina - Tabla REPS (Registro Especial de Prestadores de Servicios de Salud).  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'90';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Calidad en la Muestra de Citología Cervicouterina - 1- Satisfactoria Zona de Transformación Presente.  2- Satisfactoria Zona de Transformación Ausente  3- Insatisfactoria  4- Rechazada  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'89';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Citología Cervico uterina Resultados según Bethesda - Escamosas:  1- ASC-US (células escamosas atípicas de significado indeterminado)  2- ASC-H (células escamosas atípicas, de significado indeterminado sugestivo de LEI de alto grado)  3- Lesión intraepitelial escamosa (LEI) de bajo grado- HPV (NIC I) (LEI BG)  4- Lesión intraepitelial escamosa (LEI) de alto grado (NIC II-III CA INSITU)  (LEI AG)  5- Lesión intraepitelial escamosa de alto grado sospechosa de infiltración.  6- Carcinoma de células escamosas (escamocelular) Glandulares:  7- Células endocervicales atípicas sin ningún otro significado.  8- Células endometriales atípicas sin ningún otro significado.  9- Células glandulares atípicas sin ningún otro significado.  10- Células endocervicales atípicas sospechosas de neoplasia.  11- Células endometriales atípicas sospechosas de neoplasia.  12- Células glandulares atípicas sospechosas de neoplasia.  13- Adenocarcinoma endocervical in situ.  14- Adenocarcinoma endocervical.  15- Adenocarcinoma endometrial.  16- Otras neoplasias  17- Negativa para lesión intraepitelial o neoplasia.  18- Inadecuada para lectura.  Si no tiene el dato registrar 999  Si no aplica registrar  0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'88';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Citología Cervico uterina - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'87';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tamizaje Cáncer de  Cuello Uterino - 0- No aplica  1- Citología cervico uterina  2- ADN – VPH  3- Técnica de inspección Visual  16- No se realiza por una Tradición  17- No se realiza por una Condición de Salud  18- No se realiza por Negación de la usuaria  19- No se realiza por tener datos de contacto de la usuaria no actualizados  20- No se realiza por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'86';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de TSH Neonatal - 0- No aplica  1- Normal  2- Anormal  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'85';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha TSH Neonatal - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'84';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado Elisa para  VIH - 0- No aplica  1- Negativo  2- Positivo  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'83';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Toma de  Elisa para VIH - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'82';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado Serología para Sífilis - 0- No aplica  1- No Reactiva  2- Reactiva  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'81';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Serología para  Sífilis - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'80';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado Antígeno de Superficie Hepatitis B en Gestantes - 0- No aplica  1- Negativo  2- Positivo  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'79';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Antígeno de Superficie Hepatitis B en Gestantes - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'78';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paciente con Diagnóstico de: Ansiedad, Depresión, Esquizofrenia, déficit de atención, consumo SPA y Bipolaridad recibió Atención en  los últimos 6 meses por Equipo Interdisciplinario Completo - 0- No aplica  1- En proceso de atención.  2- Si recibió atención por equipo interdisciplinario completo.  16- No recibió atención por tener una tradición que se lo impide  17-  No recibió atención por una condición de salud  18- No recibió atención por negación del usuario  19- No recibió atención porque los datos de contacto del usuario no se encuentran actualizados  20- No recibió atención por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'77';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Asesoría Pos test  Elisa para VIH - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no  actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'76';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Asesoría Pre test  Elisa para VIH - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'75';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Preservativos entregados a pacientes con ITS - Registre el número de Preservativos entregados durante el período de reporte. Si no se entrega por otras razones registrar 993  Si no se entrega por tener datos de contacto del usuario no actualizados registrar 994  Si no se entrega por Negación del usuario registrar 995  Si no se entrega por una Condición de Salud registrar 996  Si no se entrega por una Tradición registrar 997  Si no aplica registrar  0  Si no tiene el dato registrar 999', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'74';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta de Adulto  Primera vez - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'73';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta de Joven  Primera vez - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'72';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Suministro de Vitamina A en la Última Consulta del Menor de 10 años - 0- No aplica  1- Si se suministra  16- No se suministra por una Tradición  17- No se suministra por una Condición de Salud  18- No se suministra por Negación de la usuario  20- No se suministra por otras razones  21- Registro no Evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'71';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Suministro de Sulfato Ferroso en la Última Consulta del Menor de 10 años - 0- No aplica  1- Si se suministra  16- No se suministra por una Tradición  17- No se suministra por una Condición de Salud  18- No se suministra por Negación de la usuario  20- No se suministra por otras razones  21- Registro no Evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'70';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta de Crecimiento y Desarrollo Primera vez - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'69';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta de Psicología - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'68';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta Nutrición - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01 Si no se realiza por una  Tradición registrar 1805-01-01 Si no se realiza por una  Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'67';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta Víctimas de  Violencia Sexual - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'66';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta Mujer o Menor Víctima del Maltrato - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'65';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Diagnóstico Desnutrición Proteico Calórica - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'64';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta por  Oftalmología - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'63';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valoración de la  Agudeza Visual - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'62';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Suministro de Carbonato de Calcio en el Último Control Prenatal - 0- No aplica  1- Si se suministra  16- No se suministra por una Tradición  17- No se suministra por una Condición de Salud  18- No se suministra por Negación de la usuaria  20- No se suministra por otras razones  21- Registro no Evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'61';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Suministro de Sulfato Ferroso en el Último Control Prenatal - 0- No aplica  1- Si se suministra  16- No se suministra por una Tradición  17- No se suministra por una Condición de Salud  18- No se suministra porNegación de la usuaria  20- No se suministra por otras razones  21- Registro no Evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'60';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Suministro de Ácido Fólico en el Último Control Prenatal - 0- No aplica  1- Si se suministra  16- No se suministra por una Tradición  17- No se suministra por una Condición de Salud  18- No se suministra por Negación de la usuaria  20- No se suministra por otras razones  21- Registro no Evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'59';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Último Control  Prenatal - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'58';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Prenatal - Registre el número de controles que ha tenido en el último período de reporte durante la gestación actual.  Si no tiene el dato registrar 999  Si no aplica registrar 0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'57';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Prenatal de  Primera vez - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'56';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Suministro de Método Anticonceptivo - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'55';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Suministro de Método  Anticonceptivo - 0- No aplica  1- Dispositivo Intrauterino  2- Dispositivo Intrauterino y Barrera  3- Implante Subdérmico  4- Implante Subdérmico y Barrera  5- Oral  6- Oral y Barrera  7- Inyectable Mensual  8- Inyectable Mensual y Barrera  9- Inyectable  Trimestral  10- Inyectable Trimestral y Barrera  11- Emergencia  12- Emergencia y Barrera  13- Esterilización  14- Esterilización y Barrera  15- Barrera  16- No se suministra por una Tradición  17- No se suministra por una Condición de Salud  18- No se suministra por Negación de la usuaria  20- No se suministra por otras razones  21- Registro no Evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'54';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Planificación Familiar  Primera vez - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'53';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Recién  Nacido - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'52';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de consejería en Lactancia Materna - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no se realiza por una Tradición registrar 1805-01-01  Si no se realiza por una Condición de Salud registrar 1810-01-01  Si no se realiza por Negación del usuario registrar 1825-01-01  Si no se realiza por tener datos de contacto del usuario no actualizados registrar 1830-01-01  Si no se realiza por otras razones registrar 1835-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'51';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha salida de la atención del parto o cesárea - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'50';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha atención parto o cesárea - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'49';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control de Placa  Bacteriana - 0- No aplica  1- Si – 1ra vez en el año  2- Si – 2da vez en el año  16- No se realiza por una Tradición  17- No se realiza por una Condición de Salud  18- No se realiza por Negación del usuario  19- No se realiza por tener datos de contacto del usuario no actualizados  20- No se realiza por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'48';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TD o TT Mujeres en Edad Fértil 15 a 49 años - Registre el dato del último número de dosis aplicada así:  0- No aplica  1- Una Dosis  2- Dos Dosis  3- Tres Dosis  4- Cuatro Dosis  5- Cinco Dosis  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra por Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'47';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Virus del Papiloma  Humano (VPH) - Registre el dato del último número de dosis aplicada así:  0- No aplica  1- Una Dosis  2- Dos Dosis  3- Tres Dosis  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra porNegación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'46';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Triple Viral Niños - Registre el dato del último  número de dosis aplicada así:  0- No aplica  1- Una Dosis  2- Dos Dosis  16- No se administra por una  Tradición  17- No se administra por una  Condición de Salud  18- No se administra por  Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras  razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'45';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hepatitis A - Registre el dato del último número de dosis aplicada así:  0- No aplica  1- Una Dosis  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra por Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'44';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fiebre Amarilla niños de 1 año - Registre el dato del último  número de dosis aplicada así:  0- No aplica  1- Una Dosis  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra por Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'43';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Influenza Niños - Registre el dato del último  número de dosis aplicada así:  0- No aplica  1- Una Dosis  2- Dos Dosis  3- Tres Dosis Anual  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra por Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'42';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Neumococo - Registre el dato del último número de dosis aplicada así:  0- No aplica  1- Una Dosis  2- Dos Dosis  3- Tres Dosis  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra por Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'41';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rotavirus - Registre el dato del último número de dosis aplicada así:  0- No aplica  1- Una Dosis  2- Dos Dosis  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra por Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'40';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DPT menores de 5 años - Registre el dato del último  número de dosis aplicada así:  0- No aplica  1- Primera Dosis  2- Segunda Dosis  3- Tercera Dosis  4- Cuatro Dosis  5- Cinco Dosis  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra por Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'39';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Polio - Registre el dato del último  número de dosis aplicada así:  0- No aplica  1- Una Dosis  2- Dos Dosis  3- Tres Dosis  4- Cuatro Dosis  5- Cinco Dosis  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra por Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'38';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pentavalente - Registre el dato del último número de dosis aplicada así:  0- No aplica  1- Una Dosis  2- Dos Dosis  3- Tres Dosis  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra por Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'37';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hepatitis B menores de 1 año - Registre el dato del último número de dosis aplicada así:  0- No aplica  1- Una Dosis  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra por Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'36';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'BCG - Registre el dato del último número de dosis aplicada así:  0- No aplica  1- Una Dosis  16- No se administra por una Tradición  17- No se administra por una Condición de Salud  18- No se administra por Negación del usuario  19- No se administra por tener datos de contacto del usuario no actualizados  20- No se administra por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'35';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Gestacional al Nacer - Se registra el dato de la edad gestacional en semanas.  Si no tiene el dato registrar 999  Si no aplica registrar 0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'34';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Probable de Parto - AAAA-MM-DD  Si no se tiene el dato registrar 1800-01-01  Si no aplica registrar 1845-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'33';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla en Centímetros  (población general) - Se registra el dato obtenido de la medición.  Si no se toma registrar 999', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'32';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Talla  (población general) - AAAA-MM-DD  Si no se toma registrar 1800-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso en Kilogramos  (población general) - Se registra el dato obtenido de la medición.  Si no se toma registrar 999', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'30';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Peso  (población general) - AAAA-MM-DD  Si no se toma registrar 1800-01-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'29';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fluorosis Dental - 1- Si  2- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cáncer de Seno  (población general) - 1- Si  2- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cáncer de Cérvix - 0- No aplica  1- Si  2- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'26';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Enfermedad Mental - 1- Si el diagnóstico es Ansiedad  2- Si el diagnóstico es Depresión  3- Si el diagnóstico es esquizofrenia  4- Si el diagnóstico es Déficit de atención por Hiperactividad  5- Si el diagnóstico es consumo Sustancias Psicoactivas  6- Si el diagnóstico es Trastorno del Ánimo Bipolar  7- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'25';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Infecciones de  Trasmisión Sexual - 1- Si  2- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'24';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Víctima de Violencia  Sexual - 1- Si  2- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Víctima de Maltrato - 0- No aplica  1- Si es Mujer víctima del maltrato  2- Si es Menor víctima del maltrato  3- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obesidad o Desnutrición Proteico Calórica - 1- Si es Obesidad  2- Si es Desnutrición Proteico Calórica  3- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lepra - 1- Paucibacilar  2- Multibacilar  3- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tuberculosis  Multidrogoresistente - 0- No aplica  1- Si  2- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sintomático Respiratorio (población general) - 1- Si  2- No  21-  Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hipotiroidismo  Congénito - 0- No aplica  1- Si  2- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hipertensión Inducida por la Gestación - 0- No aplica  1- Si  2- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sífilis Gestacional o congénita - 0- No aplica  1- Si es mujer con sífilis gestacional  2- Si es recién nacido con sífilis congénita  3- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'15';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gestación - 0- No aplica  1- Si  2- No  21- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'14';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de nivel educativo (población general) - Registre según lo reporte el usuario:  1- Preescolar  2- Básica Primaria  3- Básica Secundaria  4- Media Académica o Clásica  5- Media Técnica (Bachillerato Técnico)  6- Normalista  7- Técnica Profesional  8- Tecnológica  9- Profesional  10- Especialización  11- Maestría  12- Doctorado  13-  Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'13';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de ocupación  (población general) - Código de acuerdo a la Clasificación Internacional Uniforme de Ocupaciones (CIUO).  En los casos en que no se tiene esta información registrar (9999).  En el caso que no aplique registrar (9998).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código pertenencia étnica - Registre según lo reporte el usuario:  1 - Indígena  2 - ROM (gitano)  3 - Raizal (archipiélago de San Andrés y Providencia)  4 - Palanquero de San Basilio  5 - Negro(a), Mulato(a), Afrocolombiano(a) o Afro descendiente  6 - Ninguno de los anteriores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo - M - Masculino  F - Femenino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Nacimiento - AAAA-MM-DD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo    nombre    del  usuario - Tenga en cuenta el numeral 1.  En caso que el usuario no tenga segundo apellido o no se tenga este dato Registre "NONE", en mayúscula sostenida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer nombre del  usuario - Tenga en cuenta el numeral 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo apellido del usuario - Tenga en cuenta el numeral 1.  En caso que el usuario no tenga segundo apellido o no se tenga este dato Registre "NONE", en mayúscula sostenida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer apellido del usuario - Tenga en cuenta el numeral 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de identificación del usuario - Número del documento de identificación, de acuerdo con el tipo de identificación del campo anterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de identificación del usuario - RC- Registro Civil  TI- Tarjeta De Identidad  CE- Cedula De Extranjería  CC- Cedula De Ciudadanía  PA- Pasaporte  MS- Menor Sin Identificación   AS- Adulto Sin Identificación   CD-Carnet Diplomático  NV- Certificado nacido vivo, solo  para menores con 2 meses o menos de nacidos calculando entre la fecha de nacimiento y la fecha de corte del reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de habilitación IPS primaria - Tabla REPS (Registro Especial de Prestadores de Servicios de Salud)  Si es desconocido registrar 999.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de registro - Número consecutivo de registros  de  detalle  dentro del archivo. Inicia en 1 para el primer registro de detalle y va incrementando de 1 en 1, hasta el final del archivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de registro - 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'0';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion tabla ADRES4505C cabecera de resolucion 4505', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'IDADRES4505C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de las tablas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de registros del reporte ADRES 4505 (anteriormente Resolución 4505), que almacena la información desagregada por fila de las actividades, intervenciones y procedimientos de protección específica y detección temprana (PyDT) reportados a la ADRES/Supersalud. Cada registro corresponde a una línea del archivo plano de reporte obligatorio en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505D';
