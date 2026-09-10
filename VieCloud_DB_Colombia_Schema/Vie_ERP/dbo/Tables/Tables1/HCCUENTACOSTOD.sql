CREATE TABLE [dbo].[HCCUENTACOSTOD] (
    [ID]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCCUENTACOSTOC] INT           NOT NULL,
    [CODDIAGNO]        CHAR (4)      NOT NULL,
    [IDEntidadVIE]     INT           NULL,
    [1]                VARCHAR (20)  NULL,
    [2]                VARCHAR (30)  NULL,
    [3]                VARCHAR (20)  NULL,
    [4]                VARCHAR (30)  NULL,
    [5]                VARCHAR (2)   NULL,
    [6]                VARCHAR (25)  NULL,
    [7]                DATE          NULL,
    [8]                VARCHAR (1)   NULL,
    [9]                VARCHAR (4)   NULL,
    [10]               VARCHAR (1)   NULL,
    [11]               VARCHAR (9)   NULL,
    [12]               INT           NULL,
    [13]               INT           NULL,
    [14]               VARCHAR (5)   NULL,
    [15]               VARCHAR (MAX) NULL,
    [16]               DATE          NULL,
    [17]               VARCHAR (4)   NULL,
    [18]               DATE          NULL,
    [19]               DATE          NULL,
    [20]               DATE          NULL,
    [21]               INT           NULL,
    [22]               INT           NULL,
    [23]               DATE          NULL,
    [24]               DATE          NULL,
    [25]               VARCHAR (12)  NULL,
    [26]               DATE          NULL,
    [27]               INT           NULL,
    [28]               INT           NULL,
    [29]               INT           NULL,
    [30]               DATE          NULL,
    [31]               INT           NULL,
    [32]               DATE          NULL,
    [33]               INT           NULL,
    [34]               INT           NULL,
    [35]               DATE          NULL,
    [36]               INT           NULL,
    [37]               INT           NULL,
    [38]               INT           NULL,
    [39]               DATE          NULL,
    [40]               INT           NULL,
    [41]               INT           NULL,
    [42]               INT           NULL,
    [43]               DATE          NULL,
    [44]               VARCHAR (4)   NULL,
    [45]               INT           NULL,
    [46]               INT           NULL,
    [46.1]             VARCHAR (2)   NULL,
    [46.2]             VARCHAR (2)   NULL,
    [46.3]             VARCHAR (2)   NULL,
    [46.4]             VARCHAR (2)   NULL,
    [46.5]             VARCHAR (2)   NULL,
    [46.6]             VARCHAR (2)   NULL,
    [46.7]             VARCHAR (2)   NULL,
    [46.8]             VARCHAR (2)   NULL,
    [47]               INT           NULL,
    [48]               INT           NULL,
    [49]               DATE          NULL,
    [50]               INT           NULL,
    [51]               VARCHAR (12)  NULL,
    [52]               VARCHAR (12)  NULL,
    [53]               INT           NULL,
    [53.1]             VARCHAR (20)  NULL,
    [53.2]             VARCHAR (20)  NULL,
    [53.3]             VARCHAR (20)  NULL,
    [53.4]             VARCHAR (20)  NULL,
    [53.5]             VARCHAR (20)  NULL,
    [53.6]             VARCHAR (20)  NULL,
    [53.7]             VARCHAR (20)  NULL,
    [53.8]             VARCHAR (20)  NULL,
    [53.9]             VARCHAR (20)  NULL,
    [AdicionalCAC10]   INT           NULL,
    [AdicionalODO1]    INT           NULL,
    [AdicionalODO2]    INT           NULL,
    [AdicionalCAC13]   INT           NULL,
    [AdicionalODO4]    INT           NULL,
    [AdicionalODO5]    INT           NULL,
    [AdicionalODO6]    INT           NULL,
    [AdicionalODO8]    INT           NULL,
    [AdicionalCAC20]   INT           NULL,
    [AdicionalCAC7]    INT           NULL,
    [AdicionalCAC8]    INT           NULL,
    [AdicionalCAC34]   INT           NULL,
    [AdicionalCAC1]    INT           NULL,
    [54]               VARCHAR (20)  NULL,
    [55]               VARCHAR (20)  NULL,
    [56]               VARCHAR (20)  NULL,
    [57]               INT           NULL,
    [58]               DATE          NULL,
    [59]               INT           NULL,
    [60]               INT           NULL,
    [61]               INT           NULL,
    [62]               DATE          NULL,
    [63]               INT           NULL,
    [64]               VARCHAR (12)  NULL,
    [65]               VARCHAR (12)  NULL,
    [66]               INT           NULL,
    [66.1]             VARCHAR (20)  NULL,
    [66.2]             VARCHAR (20)  NULL,
    [66.3]             VARCHAR (20)  NULL,
    [66.4]             VARCHAR (20)  NULL,
    [66.5]             VARCHAR (20)  NULL,
    [66.6]             VARCHAR (20)  NULL,
    [66.7]             VARCHAR (20)  NULL,
    [66.8]             VARCHAR (20)  NULL,
    [66.9]             VARCHAR (20)  NULL,
    [AdicionalODO10]   INT           NULL,
    [AdicionalODO11]   INT           NULL,
    [AdicionalCAC12]   INT           NULL,
    [AdicionalCAC14]   INT           NULL,
    [AdicionalCAC17]   INT           NULL,
    [AdicionalCAC18]   INT           NULL,
    [AdicionalCAC3]    INT           NULL,
    [AdicionalCAC4]    INT           NULL,
    [AdicionalCAC26]   INT           NULL,
    [AdicionalCAC27]   INT           NULL,
    [AdicionalCAC28]   INT           NULL,
    [AdicionalCAC29]   INT           NULL,
    [AdicionalCAC30]   INT           NULL,
    [AdicionalCAC31]   INT           NULL,
    [AdicionalCAC32]   INT           NULL,
    [67]               VARCHAR (20)  NULL,
    [68]               VARCHAR (20)  NULL,
    [69]               VARCHAR (20)  NULL,
    [70]               INT           NULL,
    [71]               DATE          NULL,
    [72]               INT           NULL,
    [73]               INT           NULL,
    [74]               INT           NULL,
    [75]               INT           NULL,
    [76]               DATE          NULL,
    [77]               VARCHAR (100) NULL,
    [78]               VARCHAR (20)  NULL,
    [79]               INT           NULL,
    [80]               DATE          NULL,
    [81]               INT           NULL,
    [82]               VARCHAR (100) NULL,
    [83]               VARCHAR (20)  NULL,
    [84]               INT           NULL,
    [85]               INT           NULL,
    [86]               INT           NULL,
    [87]               INT           NULL,
    [88]               DATE          NULL,
    [89]               INT           NULL,
    [90]               VARCHAR (20)  NULL,
    [91]               INT           NULL,
    [92]               VARCHAR (12)  NULL,
    [93]               VARCHAR (12)  NULL,
    [94]               DATE          NULL,
    [95]               INT           NULL,
    [96]               INT           NULL,
    [97]               DATE          NULL,
    [98]               INT           NULL,
    [99]               VARCHAR (20)  NULL,
    [100]              INT           NULL,
    [101]              VARCHAR (12)  NULL,
    [102]              VARCHAR (12)  NULL,
    [103]              DATE          NULL,
    [104]              INT           NULL,
    [105]              INT           NULL,
    [106]              INT           NULL,
    [107]              INT           NULL,
    [108]              INT           NULL,
    [109]              DATE          NULL,
    [110]              VARCHAR (12)  NULL,
    [111]              INT           NULL,
    [112]              DATE          NULL,
    [113]              VARCHAR (12)  NULL,
    [114]              INT           NULL,
    [114.1]            INT           NULL,
    [114.2]            INT           NULL,
    [114.3]            INT           NULL,
    [114.4]            INT           NULL,
    [114.5]            INT           NULL,
    [114.6]            INT           NULL,
    [115]              DATE          NULL,
    [116]              VARCHAR (12)  NULL,
    [117]              INT           NULL,
    [118]              DATE          NULL,
    [119]              VARCHAR (12)  NULL,
    [120]              INT           NULL,
    [121]              DATE          NULL,
    [122]              VARCHAR (12)  NULL,
    [123]              INT           NULL,
    [124]              INT           NULL,
    [125]              INT           NULL,
    [126]              INT           NULL,
    [127]              INT           NULL,
    [128]              INT           NULL,
    [129]              INT           NULL,
    [130]              DATE          NULL,
    [131]              DATE          NULL,
    [132]              INT           NULL,
    [133]              INT           NULL,
    [134]              DATE          NULL,
    [AdicionalCAC33]   INT           NULL,
    [AdicionalCAC35]   INT           NULL,
    [AdicionalODO9]    INT           NULL,
    [AdicionalCAC6]    VARCHAR (5)   NULL,
    [AdicionalCAC5]    VARCHAR (5)   NULL,
    [AdicionalCAC9]    VARCHAR (5)   NULL,
    [AdicionalCAC19]   DATE          NULL,
    [AdicionalCAC21]   DATE          NULL,
    [AdicionalCAC11]   VARCHAR (5)   NULL,
    [AdicionalCAC16]   VARCHAR (5)   NULL,
    [AdicionalCAC2]    INT           NULL,
    [AdicionalCAC25]   CHAR (10)     NULL,
    [AdicionalODO3]    VARCHAR (5)   NULL,
    [AdicionalODO7]    VARCHAR (5)   NULL,
    CONSTRAINT [PK_HCCUENTACOSTOD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCUENTACOSTOD_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Ki67 (Factor de proliferación celular) ()', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalODO7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TPS (Antígeno polipeptídico tisular específico - %)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalODO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de primera cirugía reconstructiva en este periodo de reporte (98 - No Aplica, 100 - Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC25';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de la prueba hormonal (estrógeno y progesterona) (0 - No es cáncer de mama, 1 - (Positivo+++, >1%, 1 razón, para progesterona y estrógeno), 2 - (Positivo+++, >1%, 1 razón progesterona pero (-) para estrógeno), 3 - sin dato o no se realizó, 4 - (Positivo+++, >1%, 1 razón estrógeno pero (-) para progesterona), 5 - Negativo para progesterona y estrógeno)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Para casos glosados con causal "Por tener otro tipo de cáncer" diferente al reportado (0 - No procede glosa por diagnóstico, 100 - Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cual fue el valor de la PSA posterior a los 3 a 12 meses de tratamiento con intensión curativa (98 - No es cáncer de próstata, 99 -No lo realizaron en paciente con cáncer de próstata, 3 - No hay evidencia en historia clínica, 100 - Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Remisión para el trasplante de células progenitoras hematopoyética recibido en este periodo de reporte ((01/01/1845) - No aplica no es Hematolinfático o lo es, pero no tiene indicado trasplante, (01/01/1800) - Recibió trasplante, pero no está soportada la fecha de la remisión, (01/01/1500) - Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del primer abordaje de Nutrición y Psicología para definición de rehabilitación funcional (Post Tratamiento quirúrgico en cáncer Colorrectal) ((01/01/1845) - No aplica no es colorrectal o es colorrectal sin tratamiento quirúrgico, (01/01/1800) - No soportado el abordaje, (01/01/1500) - Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cual fue el valor de la PSA al momento del diagnóstico (98 - No es cáncer de próstata, 99 - No lo realizaron en paciente con cáncer de próstata, 100 - Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de ganglios extraídos reportados en el informe histopatológicos (cáncer gástrico y colon - rectal) (98 - No tiene informe histopatológico, 99 - No es cáncer gástrico y colon - rectal, 100 - Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de ganglios positivos (cáncer gástrico y colon - rectal) (98 - No tiene informe histopatológico, 99 - No es cáncer gástrico y colon - rectal, 100 - Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gen BRAF (0 - No aplica, 1 - Mutado, 2 - No mutado, 3 - Sin dato o No se realizó)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalODO9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado CD-20 (0 - No es linfoma no Hodking, 1 - Positivo, 2 - Negativo, 3 - No cuenta con inmunohistoquímica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC35';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' ¿Cuál fue la técnica utilizada para el diagnóstico de linfoma? (0 - No es linfoma Hodgkin ni no Hodgkin, 1 - Biopsia escisional, 2 - Punción por aguja gruesa guiada por imagen, 3 - El soporte de historia clínica no menciona la técnica utilizada para tomar la biopsia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC33';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de corte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'134';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Código único de identificación (BDUA-BDEX-PVS).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'133';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Causa de muerte -  Registre:  1=muerte asociada al cáncer  2=muerte por patología clínica no relacionada al cáncer  3=muerte por causa externa  4=muerte por causa no conocida  98=No Aplica, no ha fallecido o se desconoce su estado vital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'132';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de muerte -  Registre la Fecha en la que el usuario falleció en el formato AAAA-MM-DD  1845-01-01=No Aplica, el usuario no falleció o su estado vital no se conoce', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'131';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de desafiliación de la EPS -  Registre la Fecha en la que el usuario se desafilió de la EPS en el formato AAAA-MM-DD  1845-01-01= No Aplica, el usuario no se desafilió', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'130';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Novedad clínica  del usuario a la fecha de corte -  Registre:  1= usuario que está en manejo inicial curativo  2= usuario que está en manejo inicial paliativo  3= usuario que finalizó tratamiento inicial y está en seguimiento luego de remisión  4= usuario con recaída que está en manejo médico con propósito cur', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'129';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Novedad ADMINISTRATIVA del usuario respecto al reporte anterior -  Registre:  0= no presenta novedad  1= usuario ingresó a la EPS con diagnóstico de cáncer  2= usuario antiguo en la EPS, se le realizó nuevo diagnóstico de cáncer  3= usuario antiguo en la EPS y antiguo dx de cáncer que no había sido incluido en reporte  4= usu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'128';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado vital al finalizar este corte -  Registre:  1= vivo  2= muerte relacionada al cáncer  3= muerte por otras causas  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'127';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado final del manejo oncológico en este corte -  Luego de ser tratado en este periodo el usuario está en:  1=curación  2= progresión  3= remisión (respuesta) parcial  4= remisión (respuesta) completa  5= sin cambios (cáncer estable o sin respuesta al tratamiento)  6= abandono del tratamiento  97= No Aplicable ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'126';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de tratamiento que está recibiendo el usuario a la fecha de corte -  Registre:  1= radioterapia  2= terapia sistémica (incluye quimioterapia, anticuerpos monoclonales, terapia biológica, terapia hormonal)  3= cirugía  4= 1 y 2  5= 1 y 3  6= 2 y 3  7= manejo expectante pretratamiento  8= en seguimiento luego de tratamiento  9=alta d', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'125';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿El usuario ha recibido terapias complementarias para su rehabilitación? -  1=Si, Terapia física  2=Si, terapia de lenguaje  3=Si, Terapia ocupacional  4=No  5=1 y 2  6=1 y3  7= 2 y 3  8=1,2 y 3  98=No aplica, no se han ordenado terapias  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'124';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿El usuario recibió soporte nutricional? -  Registre:  1=Si, enteral  2=Si, parenteral  3=Si, 1y2  4=No  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'123';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS donde recibió la valoración por nutrición, en este corte -  Registre el código de Habilitación de IPS  98=No Aplica  99= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'122';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de consulta inicial con nutrición en este corte -  Registre la Fecha en que se inició esta atención en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al apli', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'121';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Fue valorado el usuario por profesional en nutrición durante este corte? -  Registre:  1=sí  2=no  98=No aplica, no se ha ordenado valoración por nutrición  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'120';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS  donde recibió la primera valoración de psiquiatría en este corte -  Registre el código de Habilitación de IPS  98=No Aplica  99= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'119';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de primera consulta con el servicio de psiquiatría (para todos los usuarios) en este corte -  Fecha de primera interconsulta en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al aplicar esta regla, pa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'118';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Ha sido valorado el usuario por el servicio de psiquiatría durante este corte? -  Registre:  1=sí  2=no  98=No aplica, no se ha ordenado valoración por psiquiatría  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'117';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS donde recibió la primera valoración de cuidado Paliativo -  Registre el código de Habilitación de IPS  98=No Aplica  99= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'116';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de primera consulta o procedimiento de cuidado paliativo en este corte -  Registre la Fecha de primera interconsulta en el formato AAAA-MM-DD.  Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al aplicar e', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'115';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió consulta o procedimiento de cuidado paliativo en este corte, por otro profesional de salud (no médico, incluye psicólogo) no especializado -  Registre:  1=sí recibió  2=no recibió  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'114.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió consulta o procedimiento de cuidado paliativo en este corte, por trabajo social -  Registre:  1=sí recibió  2=no recibió  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'114.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió consulta o procedimiento de cuidado paliativo en este corte, por médico general -  Registre:  1=sí recibió  2=no recibió  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'114.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió consulta o procedimiento de cuidado paliativo en este corte, por médico especialista, otra especialidad -  Registre:  1=sí recibió  2=no recibió  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'114.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió consulta o procedimiento de cuidado paliativo en este corte, por profesional de la salud (no médico, incluye psicólogo) especialista en cuidado paliativo -  Registre:  1=sí recibió  2=no recibió  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'114.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió consulta o procedimiento de cuidado paliativo en este corte, por médico especialista en cuidado paliativo -  Registre:  1=sí recibió  2=no recibió  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'114.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿El usuario fue valorado en consulta o procedimiento de cuidado paliativo durante este corte? (pueden haber sido múltiples) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'114';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS que realizó cirugía reconstructiva -  Registre código de Habilitación de IPS  98=No Aplica  99= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'113';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la cirugía -  Fecha en que se realizó la cirugía  en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al aplicar esta regl', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'112';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario, ¿recibió cirugía reconstructiva? -  Registre:  1=sí recibió cirugía  2=no recibió cirugía  98=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'111';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS que realizó este trasplante -  Registre código de Habilitación de IPS  98=No Aplica  99= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'110';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de trasplante -  Fecha de realización del trasplante en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al aplicar esta regl', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'109';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación temporal de este trasplante en relación al manejo oncológico -  Este trasplante de células progenitoras hematopoyéticas fue:  1=parte del manejo inicial curativo   2=parte del manejo de primera recaída  3=parte del manejo de segunda recaída o posterior  98= No Aplica  99= desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'108';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de trasplante recibido -  Registre:  1=autólogo  2=alogénico de donante idéntico relacionado  3=alogénico de donante no idéntico relacionado  4=alogénico de donante idéntico no relacionado  5=alogénico de donante no idéntico no relacionado  6=alogénico de cordón umbilical idéntico famil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'107';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Recibió el usuario trasplante de células progenitoras hematopoyética dentro del periodo de corte actual? -  Registre:  1=sí  2=no  98=No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'106';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la finalización de este último esquema de radioterapia suministrado en el corte actual (Aplica si registró la opción 2 de la pregunta anterior) Selecciona un sólo número (lo que primero ocurrió). -  Registre:  1= toxicidad  2= otros motivos médicos  3= muerte  4= cambio de EPS  5= decisión del usuario  6= otros motivos administrativos  7= otras causas no contempladas  98= No Aplica  99=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'105';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Características actuales de este último esquema de radioterapia suministrado en el corte actual -  Registre:  1=finalizado, dosis completa de radioterapia administrada  2= finalizado, dosis incompleta pero finalizada por algún motivo  3=no finalizado, esquema incompleto pero aún bajo tratamiento  98= No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'104';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de finalización del último esquema de radioterapia suministrado en el corte actual -  Registre la Fecha de finalización de la radioterapia en el formato AAAA-MM-DD.  Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'103';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS2 que suministra la radioterapia -  Registre el código de Habilitación de IPS  98= No Aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pero se tiene el dato solicitado entonces se debe repo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'102';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS1 que suministra la radioterapia -  Registre el código de Habilitación de IPS.  98= No Aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pero se tiene el dato solicitado entonces se debe rep', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'101';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de IPS que suministran este  último esquema de radioterapia suministrado en el corte actual -  Registre el número de IPS que intervinieron en la administración de la dosis de radioterapia  98=No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'100';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de radioterapia aplicada en el último esquema de radioterapia suministrado en el corte actual -  Registre:  1=externa para tratar  2=interna para tratar (incluye, braquiterapia)  3= profiláctica (por ejemplo a sistema nervioso central)  4=1 y 2 exclusivamente  5=1 y 3 exclusivamente  6=2 y 3 exclusivamente  7=1,2 y 3  98= No Aplica  99= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'99';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación temporal del último esquema de radioterapia suministrado en el corte actual -  Registre:  1=neoadyuvancia (manejo  inicial prequirúrgico)  2= tratamiento inicial curativo  sin cirugía sugerida (por ejemplo, sería una opción frecuente en caso de leucemias o linfomas, u otros cánceres a quienes no se les hizo cirugía)  3=adyuvancia (mane', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'98';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicio del último esquema de radioterapia suministrado en el corte actual -  Registre la fecha en que se inició este esquema en el formato AAAA-MM-DD.  Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al apli', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'97';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la finalización de este primer o único esquema de radioterapia (Aplica si registró la opción 2 de la pregunta anterior) Selecciona un sólo número (lo que primero ocurrió). -  Registre:  1= toxicidad  2= otros motivos médicos  3= muerte  4= cambio de EPS  5= decisión del usuario  6= otros motivos administrativos  7= otras causas no contempladas  98= No Aplica  99=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'96';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Características actuales de este primer o único esquema de radioterapia -  Registre:  1=finalizado, dosis completa de radioterapia administrada  2= finalizado, dosis incompleta pero finalizada por algún motivo  3=no finalizado, esquema incompleto pero aún bajo tratamiento  98= No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'95';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de finalización de primer o único esquema de radioterapia -  Registre la Fecha de finalización de la radioterapia externa en el formato AAAA-MM-DD.  Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'94';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS2 que suministra la radioterapia -  Registre el código de Habilitación de IPS  98= No Aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pero se tiene el dato solicitado entonces se debe repo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'93';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS1 que suministra la radioterapia -  Registre el código de Habilitación de IPS  98= No Aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pero se tiene el dato solicitado entonces se debe repo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'92';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de IPS que suministran este primer o único esquema de radioterapia -  Registre el número de IPS que intervinieron en la administración de la dosis de radioterapia.  98= No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'91';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de radioterapia aplicada en este primer o único esquema -  Registre:  1=externa para tratar  2=interna para tratar (incluye, braquiterapia)  3= profiláctica (por ejemplo a sistema nervioso central)  4=1 y 2  5=1 y 3  6=2 y 3  7=1,2 y 3  98= No Aplica  99= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'90';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación temporal del primer o único esquema de radioterapia en este corte -  Registre para todos los usuarios:  1=neoadyuvancia (manejo  inicial prequirúrgico)  2= tratamiento inicial curativo sin cirugía sugerida (por ejemplo, sería una opción frecuente en caso de leucemias o linfomas, u otros cánceres a quienes no se les hizo ciru', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'89';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicio de primer o único esquema de radioterapia suministrado en el corte actual -  Registre la fecha en que se inició la radioterapia en el formato AAAA-MM-DD.  Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al a', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'88';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de esquemas de radioterapia suministrados en el corte actual -  Registre el número de esquemas de radioterapia suministrados durante el periodo de reporte actual  98=No Aplica (si respondió 2 en la pregunta anterior)  99= desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'87';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Recibió el usuario algún tipo de radioterapia en el corte actual? -  Registre:  1= Si  2= No  98= No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'86';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado vital al finalizar la única o última cirugía de este corte -  Registre:  1=vivo  2=fallece  98= No aplica (No cirugías en este corte)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'85';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación temporal de esta última cirugía en relación al manejo oncológico, en este corte -  Esta cirugía es:  1=parte del manejo inicial para el cáncer  2=parte del manejo de primera recaída  3=parte del manejo de segunda recaída  4=parte del manejo de tercera recaída o adicional  98=No Aplica (sólo hubo una intervención en este corte o no hubo cirug', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'84';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de última cirugía -  Registre el código de procedimiento CUPS  98=No Aplica (sólo hubo una intervención en este corte o no hubo cirugías en este corte)  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'83';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS  que realiza el último de los procedimientos quirúrgicos en este corte -  Registre el código de Habilitación de IPS  98=No Aplica (sólo hubo una intervención en este corte o no hubo cirugías en este corte)  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'82';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de haber realizado la última intervención quirúrgica de este corte -  Registre:  1= complementar tratamiento quirúrgico del cáncer no asociado a complicaciones de la primera cirugía  2=complicaciones debida a la primera cirugía o siguientes  3= complicaciones por otras condiciones médicas no relacionadas a la cirugía (por ejem', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'81';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de realización del último procedimiento quirúrgico o de reintervención en este corte. -  Registre la Fecha de realización de la última cirugía en el formato AAAA-MM-DD.  Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, a', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'80';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación temporal de esta primera cirugía en relación al manejo oncológico -  Esta cirugía es:  1=parte del manejo inicial para el cáncer  2=parte del manejo de primera recaída  3=parte del manejo de segunda recaída  4=parte del manejo de tercera recaída o adicional  98= No Aplica  99= desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'79';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de primera cirugía -  Registre el código de procedimiento CUPS  98=No Aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pero se tiene el dato solicitado entonces se debe report', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'78';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS  que realizó la primera cirugía de este corte -  Registre el código de habilitación de la IPS  98=No Aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pero se tiene el dato solicitado entonces se debe re', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'77';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de realización de la primera cirugía en este reporte -  Registre la fecha en el formato AAAA-MM-DD.  Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al aplicar esta regla, para que sean ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'76';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de cirugías a las que fue sometido el usuario durante el periodo de reporte actual -  Registre el número de cirugías a las que el usuario fue sometido durante el periodo de reporte actual, incluya aquellas por complicaciones relacionadas a la cirugía inicial.  98=No Aplica (si respondió 2 en la pregunta anterior)  99= desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'75';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Fue sometido el usuario a una o más cirugías curativas o paliativas como parte del manejo del cáncer durante este reporte? -  Registre:  1= si fue sometido a al menos una cirugía durante este corte  2= no recibió cirugía durante este corte  3= no recibió cirugía durante este corte, pero está programada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'74';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la finalización (prematura) de este último ciclo (Aplica si registró la opción 2 de la pregunta anterior) Selecciona un sólo número (lo que primero ocurrió). -  Registre:  1=toxicidad de uno o más medicamentos  2=otros motivos médicos  3=muerte  4= cambio de EPS  5=decisión del usuario  6=no hay disponibilidad de medicamentos  7=otros motivos administrativos  8=otras causas no contempladas  98= No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'73';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Características actuales del último  ciclo de este corte -  Registre:  1=finalizado, ciclo completo según medicamentos programados  2= finalizado, ciclo incompleto pero finalizado por algún motivo  3=no finalizado, ciclo incompleto pero aún bajo tratamiento  98=No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'72';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de finalización del último ciclo de este corte -  Fecha en que terminó la administración último ciclo de quimioterapia en el formato AAAA-MM-DD.  Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'71';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Recibió quimioterapia intratecal en el último ciclo de este corte? -  Registre:  1= si recibió  2= no recibió  98: No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'70';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento no POS 3 administrado al usuario- último ciclo -  Registre el código CUM del medicamento NO POS usado en este caso  97=No Aplica (no recibió medicamento no POS)  98= No Aplica (no tuvo este ciclo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'69';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento no POS 2 administrado al usuario- último ciclo -  Registre el código CUM del medicamento NO POS usado en este caso  97=No Aplica (no recibió medicamento no POS)  98= No Aplica (no tuvo este ciclo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'68';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento no POS 1 administrado al usuario- último ciclo -  Registre el código CUM del medicamento NO POS usado en este caso  97=No Aplica (no recibió medicamento no POS)  98= No Aplica (no tuvo este ciclo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'67';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'La patología cumple con los criterios de calidad (Leucemia aguda) (0 - No es Leucemia aguda, 1 - Si, 2 - No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC32';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Fueron realizadas pruebas moleculares (FISH o PCR) en paciente con Leucemia Aguda para la clasificación de riesgo? (0 - No es Leucemia aguda, 1 - Si, 2 - No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Fue realizada la citogenética convencional en paciente con Leucemia Aguda? (0 - No es Leucemia aguda, 1 - Si, 2 - No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC30';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Fue realizada citometría de flujo en paciente con Leucemia Aguda? (0 - No es Leucemia aguda, 1 - Si, 2 - No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC29';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pacientes con cáncer de pulmón en estadio tempranos (estadios I y II (IA, IB, IIA y IIB)) llevados a cirugía con intención curativa en quienes se realizó el estudio de riesgo cardiovascular medido por índice de Framignham o ecocardiograma (0 - No es cáncer de pulmón, 1 - Si se realizó, 2 - No está descrito en la historia clínica, 3 - Es cáncer de pulmón en estadio avanzado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pacientes con cáncer de pulmón en estadio tempranos (estadios I y II (IA, IB, IIA y IIB)) llevados a cirugía con intención curativa en quienes se realizó el estudio de función pulmonar (espirometría) (0 - No es cáncer de pulmón, 1 - Si se realizó, 2 - No está descrito en la historia clínica, 3 - Es cáncer de pulmón en estadio avanzado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'EGFR mutado (cáncer de pulmón de tipo célula no pequeña que incluye adenocarcinoma, escamocelular y otros tipos no mencionados) (0 - No es cáncer de pulmón, 1 - Sí, 2 - No, 3 - Es cáncer de pulmón de célula pequeña)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC26';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado prueba CISH (0 - No es cáncer de mama, 1 - Positivo, 2 - Negativo, 3 - Sin dato o No se realizó)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se realizó prueba FISH ó CISH (0 - No es cáncer de mama, 1 - Sí, 2 - No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicar los órganos en los que se presenta la metástasis (0 - No registra metástasis, 1 - Ganglios linfáticos regionales, 2 - Hígado, 3 - Pulmón, 4 - Peritoneo, 5 - Ganglios linfáticos distantes, 6 - Hueso, 7 - Suprarrenales, 8 - Pleura, 9 - Cerebro, 10 - Otro, 11 -  2 o más órganos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'El paciente presenta en el periodo actual metástasis del cáncer primario reportado en este registro (1 - Sí, 2 -No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de radiación externa suministrada en los esquemas iniciados en este periodo (0 - No recibió radioterapia externa, 100 - Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC14';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica utilizada en radioterapia (0 - No recibió radioterapia, 1 - Convencional 2D, 2 - IMRT (radioterapia con intensidad moderada), 3 - VMAT (Volumétrica modulada), 4 - IGRT (radioterapia guiada por imagen), 5 - Conformacional 3D, 6 - Radioterapia estereotaxica, 7 - Radiocirugia, 8 - Otra radioterapia interna o radiofármaco (ejemplo braquiterapia, yodoterapia), 9 - radioterapia externa sin mención de la técnica en los soportes)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación anatómica del cáncer colorrectal (0 - No aplica, 1 - Derecho (trayecto desde el ciego hasta el ángulo esplénico), 2 - Izquierdo (trayecto desde el ángulo esplénico al recto))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalODO11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estudio de Microsatélites (0 - No aplica, 1 - Estable, 2 - Inestable, 3 - Sin dato o No se realizó)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalODO10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este último ciclo el usuario recibió Clorambucilo (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'66.9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este último ciclo el usuario recibió Citarabina (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'66.8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este último ciclo el usuario recibió Cisplatino (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'66.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este último ciclo el usuario recibió Ciclosporina (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'66.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este último ciclo el usuario recibió Ciclofosfamida (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'66.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este último ciclo el usuario recibió Carboplatino (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'66.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este último ciclo el usuario recibió Capecitabina (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'66.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este último ciclo el usuario recibió Busulfano (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'66.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este último ciclo el usuario recibió Bleomicina (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'66.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantos medicamentos antineoplásicos, el (los) especialista(s) tratante(s) del cáncer propusieron como manejo en este último ciclo de este corte -  Escriba el número de medicamentos antineoplásicos propuestos en este último ciclo del periodo de reporte actual  98= No Aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la f', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'66';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS2 que suministra el último ciclo en este reporte -  Código de Habilitación de IPS  98=No Aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pero se tiene el dato solicitado entonces se debe reportar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'65';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS1 que suministra el último ciclo en este reporte -  Código de Habilitación de IPS  98=No Aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pero se tiene el dato solicitado entonces se debe reportar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'64';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de IPS que suministran el último ciclo de este corte -  Registre el número de Instituciones Prestadoras de Servicios de Salud que suministran el último ciclo de quimioterapia  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2014-01-01, se aclara que si el diagnóstico fue previo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'63';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicio del último ciclo de  quimioterapia de este corte -  Fecha en que se inició este ciclo de quimioterapia en el formato AAAA-MM-DD.  Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al a', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'62';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación temporal del último ciclo de este corte en relación al manejo oncológico -  Registre:  1=neoadyuvancia (manejo  inicial prequirúrgico)  2= tratamiento inicial curativo  sin cirugía sugerida (por ejemplo, sería un opción frecuente en caso de leucemias o linfomas, u otros cánceres a quienes no se les hizo cirugía)  3=adyuvancia (manej', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'61';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la finalización (prematura) de este primer ciclo (Aplica si registró la opción 2 de la pregunta anterior)  Selecciona un sólo número (lo que primero ocurrió). -  Registre:  1=toxicidad de uno o más medicamentos  2=otros motivos médicos  3=muerte  4= cambio de EPS  5=decisión del usuario  6=no hay disponibilidad de medicamentos  7=otros motivos administrativos  8=otras causas no contempladas  98= No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'60';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Características actuales del primer ciclo de este corte -  1=finalizado, ciclo completo según medicamentos programados  2= finalizado, ciclo incompleto pero finalizado por algún motivo  3=no finalizado, ciclo incompleto pero aún bajo tratamiento  98=No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'59';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de finalización del primer ciclo de este corte -  Fecha en que terminó la administración primer ciclo de quimioterapia en el formato AAAA-MM-DD.  Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'58';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Recibió quimioterapia intratecal en el primer ciclo de este corte? -  Registre:  1= si recibió  2=no recibió  98= No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'57';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento no POS 3 administrado al usuario- primer ciclo -  Registre el código CUM del medicamento NO POS usado en este caso.  97=No Aplica (no recibió medicamento no POS)  98= No Aplica (no tuvo este ciclo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'56';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento no POS 2 administrado al usuario- primer ciclo -  Registre el código CUM del medicamento NO POS usado en este caso.  97=No Aplica (no recibió medicamento no POS)  98= No Aplica (no tuvo este ciclo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'55';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento no POS 1 administrado al usuario- primer ciclo -  Registre el código CUM del medicamento NO POS usado en este caso.  97=No Aplica (no recibió medicamento no POS)  98= No Aplica (no tuvo este ciclo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'54';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se realizó prueba de receptores de progesterona y/o estrógeno (cáncer de mama) (0 - No es cáncer de mama, 1 - Si progesterona y estrógeno, 2 - Si progesterona, pero no para estrógeno, 3 - Si estrógeno, pero no para progesterona, 4 - No se realizó progesterona ni estrógeno)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'El reporte de biopsia describe las coloraciones básicas* y se cuenta con el reporte de inmunohistoquímica (0 - No es linfoma Hodgkin ni no Hodgkin, 1 - Si, 2 - No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC34';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se realizó PSA en el momento del diagnóstico (0 - No es cáncer de próstata, 1 - Sí, 2 - No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'La patología cumple con los criterios de calidad (solo para cáncer colon – recto) (0 - No es cáncer de colon – recto, 1 - Sí, 2 - No, 3 - Sin evidencia de patología - No tiene informe histopatológico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se realizó abordaje por grupo multidisciplinario para definición del tratamiento inicial (0 - No aplica, 1 - Sí, todo el grupo de profesionales, 2 - No soportado el abordaje multidisciplinario)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gen RAS (0 - No aplica, 1 - Mutado, 2 - No mutado, 3 - Sin dato o No se realizó)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalODO8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ki67 (Factor de proliferación celular) (0 - No aplica, 1 - Si, 2 - No, 3 - Sin dato o No se realizó)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalODO6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Terapia Dirigida Recibida (0 - No aplica, 1 - Pembrolizumab, 2 - Nivolumab, 3 - Ipilimumab, 4 - Atezolizumab, 5 - Gefitinib, 6 - Erlotinib, 7 - Bevacizumab, 8 - Cetuximab, 9 - Panitumumab, 10 - Otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalODO5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ROS-1 (Proto-oncogen tirosina-protein quinasa) (0 - No aplica, 1 - Positivo, 2 - Negativo, 3 - Sin dato o No se realizó)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalODO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis total de radiación externa indicada en los esquemas iniciados en este periodo (0 - No recibió radioterapia externa, 100 - Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC13';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmunorreactividad PD-L1 (Programmed Death-ligand 1) (0 - (<1%,), 1 - (1-49%), 2 - (>= 50%), 3 - Sin dato o No se realizó)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalODO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmunorreactividad para ALK (Cinasa linfoma anaplásico) (0 - No aplica, 1 - Positivo, 2 - Negativo, 3 - Sin dato o No se realizó)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalODO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se realizó PSA 3 a 12 meses posterior al tratamiento con intensión curativa (0 - No es cáncer de próstata, 1 - Sí, 2 - No, 3 - No hay evidencia en historia clínica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'AdicionalCAC10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este primer ciclo el usuario recibió Clorambucilo (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'53.9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este primer ciclo el usuario recibió Citarabina (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'53.8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este primer ciclo el usuario recibió Cisplatino (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'53.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este primer ciclo el usuario recibió Ciclosporina (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'53.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este primer ciclo el usuario recibió Ciclofosfamida (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'53.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este primer ciclo el usuario recibió Carboplatino (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'53.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este primer ciclo el usuario recibió Capecitabina (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'53.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este primer ciclo el usuario recibió Busulfano (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'53.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En este primer ciclo el usuario recibió Bleomicina (puede haber recibido más de un medicamento) -  Registre:  1= sí recibió  2=No recibió, no fue propuesto dentro del plan terapéutico  3= No recibió, Aunque fue propuesto dentro del plan terapéutico  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'53.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantos medicamentos antineoplásicos, el (los) especialista(s) tratante(s) del cáncer propusieron como manejo en el primer ciclo de este corte -  Escriba el número de medicamentos antineoplásicos propuestos en el primer ciclo de este corte  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'53';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS2 que suministra el primer ciclo de este corte -  Código de Habilitación de IPS  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pero se tiene el dato solicitado entonces se debe reportar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'52';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS1 que suministra el primer ciclo de este corte -  Código de Habilitación de IPS  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pero se tiene el dato solicitado entonces se debe reportar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'51';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de IPS que suministran el primer ciclo de este corte -  Registre el número de Instituciones Prestadoras de Servicios de Salud que suministran el primer ciclo de quimioterapia  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'50';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicio del primer ciclo de  quimioterapia  de este corte -  Fecha en que se inició este ciclo de quimioterapia en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al ap', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'49';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación temporal del primer ciclo en el periodo en relación al manejo oncológico -  Registre:  1=neoadyuvancia (manejo  inicial prequirúrgico)  2= tratamiento inicial curativo  sin cirugía sugerida (por ejemplo, sería un opción frecuente en caso de leucemias o linfomas, u otros cánceres a quienes no se les hizo cirugía)  3=adyuvancia(manejo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'48';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de ciclos iniciados y administrados en el periodo de reporte, incluyendo  el que aún recibe en la fecha de corte. -  Escriba el número de ciclos iniciados en el periodo de reporte actual  98= No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'47';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió en este corte  Otra fase de quimioterapia denominada diferente a las anteriores  (aplica para hematolinfáticos, puede haber recibido más de una fase) -  Registre:  1= sí recibió  2=no recibió  97= No Aplica (no es hematolinfático)  99= desconocido (es cáncer hematolinfático, recibió quimioterapia, pero no hay información del nombre de la fase)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'46.8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió en este corte  la fase de quimioterapia denominada  Mantenimiento largo o final (aplica para hematolinfáticos, puede haber recibido más de una fase) -  Registre:  1= sí recibió  2=no recibió  97= No Aplica (no es hematolinfático)  99= desconocido (es cáncer hematolinfático, recibió quimioterapia, pero no hay información del nombre de la fase)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'46.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió en este corte  la fase de quimioterapia denominada  Mantenimiento (aplica para hematolinfáticos, puede haber recibido más de una fase) -  Registre:  1= sí recibió  2=no recibió  97= No Aplica (no es hematolinfático)  99= desconocido (es cáncer hematolinfático, recibió quimioterapia, pero no hay información del nombre de la fase)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'46.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió en este corte  la fase de quimioterapia denominada Reinducción (aplica para hematolinfáticos, puede haber recibido más de una fase) -  Registre:  1= sí recibió  2=no recibió  97= No Aplica (no es hematolinfático)  99= desconocido (es cáncer hematolinfático, recibió quimioterapia, pero no hay información del nombre de la fase)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'46.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió en este corte  la fase de quimioterapia denominada Consolidación  (aplica para hematolinfáticos, puede haber recibido más de una fase) -  Registre:  1= sí recibió  2=no recibió  97= No Aplica (no es hematolinfático)  99= desconocido (es cáncer hematolinfático, recibió quimioterapia, pero no hay información del nombre de la fase)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'46.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió en este corte  la fase de quimioterapia denominada Intensificación  (aplica para hematolinfáticos, puede haber recibido más de una fase) -  Registre:  1= sí recibió  2=no recibió  97= No Aplica (no es hematolinfático)  99= desconocido (es cáncer hematolinfático, recibió quimioterapia, pero no hay información del nombre de la fase)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'46.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió en este corte  la fase de quimioterapia denominada Inducción (aplica para hematolinfáticos, puede haber recibido más de una fase) -  Registre:  1= sí recibió  2=no recibió  97= No Aplica (no es hematolinfático)  99= desconocido (es cáncer hematolinfático, recibió quimioterapia, pero no hay información del nombre de la fase)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'46.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibió en este corte  la fase de quimioterapia denominada Prefase o Citorreducción inicial  (aplica para hematolinfáticos, puede haber recibido más de una fase) -  Registre:  1= sí recibió  2=no recibió  97= No Aplica (no es hematolinfático)  99= desconocido (es cáncer hematolinfático, recibió quimioterapia, pero no hay información del nombre de la fase)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'46.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Cuántas fases de quimioterapia recibió el usuario en este corte? (aplica para hematolinfáticos) -  Escriba el número de fases de quimioterapia propuestas para este corte  98= No Aplica  99= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'46';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Recibió el usuario quimioterapia u otra terapia sistémica (por ejemplo, hormonoterapia) dentro del periodo de corte actual? (Aclaración: en esta sección solo incluya ciclos iniciados en este corte. Por favor, no incluya el último ciclo informado como "ac -  Registre:  1= sí recibió  2=no recibió (en casos donde se indica quimioterapia pero no recibió)  98= No Aplica  99=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'45';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo (nombre) de ese cáncer antecedente -  Registre la enfermedad maligna antecedente o concurrente. Tenga en cuenta que antes de definir, por ejemplo, un  linfoma, el usuario pudo tener un diagnóstico de cáncer a estudio o tumor de células pequeñas, redondas y azules, debe notificarse el linfoma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'44';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnóstico del otro cáncer primario -  Fecha en que se diagnosticó el otro cáncer primario que afecta al usuario  en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'43';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene antecedente de otro cáncer primario (es decir, tiene o tuvo otro tumor maligno diferente al que está notificando) -  Registre:  1=Sí  2=No  99=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'42';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Objetivo de la intervención médica durante el periodo de reporte. -  Registre:  1=observación previa a tratamiento únicamente (manejo expectante todo el periodo)  2=ofrecer tratamiento curativo o paliativo dirigido al cáncer (quimioterapia, hormonoterapia, radioterapia, cirugía, terapia biológica) inicial o por recaída única', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'41';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Objetivo (o intención) del tratamiento médico inicial (al diagnóstico) -  Registre:  1=curación  2=paliación (intención paliativa) exclusivamente  99=no hay información disponible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'40';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de clasificación de riesgo -  Registre la fecha en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al aplicar esta regla, para que sean f', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'39';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación de riesgo leucemias o linfomas, y sólidos pediátricos -  Registre:  1=estándar, bajo, o favorable  2=bajo intermedio  3=intermedio  4=alto intermedio  5=alto o desfavorable  6=favorable temprano  7=desfavorable temprano  8=favorable avanzado  9= desfavorable avanzado  10=R1  11=R2  12=R3  13= R4  97=otro no definido  98=No Ap', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'38';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para cáncer de próstata, valor de clasificación de la escala Gleason en el momento del diagnóstico -  Registre número de la clasificación histológica de Gleason entre 2 y 10  98= no es cáncer de próstata  99=es cáncer de próstata pero no hay información en la historia clínica acerca de esta estadificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'37';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estadificación clínica  en linfoma no Hodgkin (Murphy) y linfoma Hodgkin (Ann Arbor), Mieloma Múltiple u otros cánceres hematológicos -  Registre:  1= estado (etapa) I  2=estado (etapa) II  3=estado (etapa) III  4=estado (etapa)  IV  98=No Aplica (no son linfomas ni Mieloma ni otros cánceres hematológicos)  99=No hay información de estadificación para linfomas Hodgkin o no Hodgkin o Mieloma u ot', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'36';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se realizó la estadificación de Dukes -  Registre la fecha en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al aplicar esta regla, para que sean f', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'35';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para cáncer colorrectal, estadificación de Dukes -  Registre:  1=A  2=B  3=C  4=D  98=No Aplica (no es cáncer colorrectal)  99=es cáncer colorrectal pero no hay información en la historia clínica acerca de esta estadificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'34';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para cáncer de mama, resultado de la primera o única prueba HER2 -  Registre:  1=positiva (o 3+ según la técnica)  2= negativa (o 0, 1+, 2+, según la técnica)  3=no hay información en la historia clínica acerca del HER2 previo al inicio del tratamiento  98=No Aplica (no es cáncer de mama)  99=se solicitó pero no hay informació', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'33';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para cáncer de mama, fecha de realización de la primera o única prueba HER2 -  Registre la fecha en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día.  Registre 1800-01-01= desconocida.  Registre 1845-01-01=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'32';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para cáncer de mama, ¿se le realizó a este usuario la prueba HER2 (llamado también receptor 2 del factor de crecimiento epidérmico humano, también llamado erb-B2) antes del inicio del tratamiento? -  Registre  1=sí se le realizó  2= no se le realizó (explícito en la historia que se ordena)  98=No Aplica (no es cáncer de mama)  99=no hay información en la historia clínica acerca del HER2 previo al inicio del tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se realizó esta estadificación -  Registre la fecha en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día.  Registre 1800-01-01= desconocida.  Registre 1845-01-01=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'30';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si es tumor sólido, cuál fue la primera estadificación basada en TNM, FIGO, u otras compatibles con esta numeración según tumor. -  En caso de más de una estadificación (clínica, patológica, etc.), apunte aquella que fue usada para iniciar el tratamiento. Escoja el número que representa la estadificación clínica:  0=estadio clínico (ec) 0 (tumor in situ)  1=ec I o 1  2=ec IA o 1A  3=ec IA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'29';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grado de diferenciación del tumor sólido maligno según la biopsia o informe de primera cirugía -  Apunte el número que corresponde al grado de diferenciación de la biopsia diagnóstica del cáncer (como primera opción), si no hay información en ese informe use la quirúrgica.  1=bien diferenciado  2= moderadamente diferenciado  3=mal diferenciado  4=anaplási', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Histología del tumor en muestra de biopsia o quirúrgica -  Registre el número que corresponde al subtipo histológico de la biopsia diagnóstica del cáncer (como primera opción), si no hay información en ese reporte use la quirúrgica:  1=adenocarcinoma, con o sin otra especificación  2=carcinoma escamocelular (epider', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de primera consulta con médico tratante de la enfermedad maligna -  Registre la fecha en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al aplicar esta regla, para que sean f', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'26';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código válido de habilitación de la IPS donde se realiza la confirmación diagnóstica -  Registre el código de habilitación de IPS  96= No aplica por diagnóstico fuera del país  98= No aplica  99= Desconocido (opción válida solo en caso de diagnóstico antes de 2009-01-01, se aclara que si el diagnóstico fue previo a la fecha mencionada pero se t', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'25';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de informe histopatológico válido -  Registre la fecha en el formato AAAA-MM-DD. (Registre la fecha de la primera prueba que confirmó diagnóstico de cáncer y dio inicio a un manejo, así haya requerido pruebas adicionales posteriormente para el diagnóstico definitivo).  Si conoce sólo el año ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'24';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de recolección de muestra para estudio histopatológico -  Registre la fecha en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al aplicar esta regla, para que sean f', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo por el cual el usuario no tuvo diagnóstico por histopatología a la fecha de corte (aplica para registros con respuesta igual a 7 en la variable anterior) -  1=clínica, usuario con coagulopatía  2=clínica, debido a localización del tumor  3=clínica, debido al estado funcional del usuario (deterioro)  4=negativa del usuario o su acudiente para realizar el estudio histopatológico, con documentación de soporte  5=adm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de estudio con el que se realizó el diagnóstico de cáncer -  Tipo de prueba: Registre  1= mielograma o aspirado de médula ósea  2=biopsia de médula ósea  3= biopsia de ganglios  4= biopsia de masa  5= inmunohistoquímica  6= citometría de flujo  7=clínica exclusivamente (incluye estudios imagenológicos y bioquímicos en aqu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de ingreso a la institución que realizó el diagnóstico luego de la remisión -  Registre la fecha en formato AAAA-MM-DD (si no hay remisión registre la fecha de primera consulta relacionada al cáncer en la institución que realizó el diagnóstico). Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la nota de remisión del médico o institución general hacia la institución que hizo el diagnóstico -  Revisar en la historia clínica la remisión más antigua disponible previa al diagnóstico y que pudiese estar asociada a la enfermedad maligna actual (que contenga impresión diagnóstica de cáncer realizada por un médico que remitió a la institución que diag', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad de la persona al momento del diagnóstico del cáncer informado actualmente -  Edad  del usuario en años cumplidos cuando fue realizado el diagnóstico del cáncer informado en el periodo actual. (de 0 a 120 años)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la neoplasia (cáncer o tumor) maligna reportada -  Registre el código de la enfermedad maligna diagnosticada del usuario según código CIE -10.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de afiliación a la EAPB que reporta -  Fecha en la que el usuario se afilió a la EPS  en el formato AAAA-MM-DD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número telefónico del paciente (incluyendo a familiares y cuidadores) -  Registre sólo dos números de teléfono(s) fijos y/o móviles completos para contactar al paciente y separe por comas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'15';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Municipio  de residencia -  Registre el Código del municipio en donde reside el afiliado según la división político administrativa DIVIPOLA  – DANE. Este código debe ser reportado en 5 dígitos, donde los dos primeros dígitos corresponden al departamento donde se localiza el municipi', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'14';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo poblacional -  1=Indigentes  2=Población infantil a cargo del ICBF  3=Madres comunitarias  4=Artistas, autores, compositores  5=Otro grupo poblacional  6=Recién Nacidos  7=Discapacitados  8=Desmovilizados  9=Desplazados  10=Población ROM  11=Población raizal  12=Población en centr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'13';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código pertenencia étnica -  Registre el grupo étnico del usuario:  1=Indígena  2=ROM (gitano)  3=Raizal del archipiélago de San Andrés y Providencia  4=Palenquero de San Basilio  5=Negro(a), mulato(a), afro colombiano(a) o afro descendiente  6=Ninguna de las anteriores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la EPS o de la entidad territorial -  Cuando el usuario tenga EPS u otra EOC escriba el código de la empresa aseguradora que registra al usuario (EPS/EOC/EPSI/ESS/CCF/EAS). Cuando el usuario sea notificado por entidad territorial escriba el código de departamento y municipio según DANE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Régimen de afiliación AL SGSSS -  C=Régimen Contributivo  S=Régimen Subsidiado   P=Regímenes de excepción  E=Régimen especial  N=No asegurado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ocupación -  Código según la Clasificación Internacional Uniforme de Ocupaciones.  9999= No existe información  9998= No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo -  M= masculino  F= femenino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de nacimiento -  Fecha de nacimiento del usuario en el formato AAAA-MM-DD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Identificación del usuario -  Número de identificación del afiliado según el tipo de identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Identificación del usuario -  RC=Registro Civil  TI=Tarjeta Identidad  CC=Cédula de Ciudadanía  CE=Cédula Extranjería  PA=Pasaporte  MS=Menor sin Identificación (solo para el Régimen Subsidiado)  AS=Adulto sin Identificación (solo para el Régimen Subsidiado)  CD=Carnet Diplomático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo apellido del usuario -  Escriba el segundo apellido del usuario.  Registre "NOAP", en mayúscula sostenida, cuando el usuario no tiene segundo apellido (NOAP=Ningún Otro Apellido")', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer apellido del usuario -  Escriba el primer apellido del usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo nombre del usuario -  Escriba el segundo nombre del usuario. En caso de un tercer nombre, escríbalo separado por un espacio.  Registre  "NONE", en mayúscula sostenida, cuando el usuario no tiene segundo nombre (NONE="Ningún Otro Nombre Escrito").', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer nombre del usuario -  Escriba el primer nombre del usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id EntidadVIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'IDEntidadVIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID HCCUENTACOSTOC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'IDHCCUENTACOSTOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de diagnósticos asociados a cuentas de costo (HC) para la generación de RIPS y reportes de facturación. Cada registro vincula un diagnóstico CIE-10 a una cuenta de costo de historia clínica, almacenando los campos posicionales requeridos por los diferentes formatos de reporte (RIPS, cuentas de cobro, odontología, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOD';
