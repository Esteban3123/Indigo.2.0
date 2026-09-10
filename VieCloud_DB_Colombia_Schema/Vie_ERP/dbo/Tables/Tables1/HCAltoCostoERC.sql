CREATE TABLE [dbo].[HCAltoCostoERC] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [CODDIAGNO] VARCHAR (4)   NULL,
    [1]         VARCHAR (20)  NULL,
    [2]         VARCHAR (30)  NULL,
    [3]         VARCHAR (20)  NULL,
    [4]         VARCHAR (30)  NULL,
    [5]         VARCHAR (2)   NULL,
    [6]         VARCHAR (25)  NULL,
    [7]         DATE          NULL,
    [8]         VARCHAR (1)   NULL,
    [9]         VARCHAR (1)   NULL,
    [10]        VARCHAR (9)   NULL,
    [11]        INT           NULL,
    [12]        INT           NULL,
    [13]        VARCHAR (5)   NULL,
    [14]        VARCHAR (MAX) NULL,
    [15]        DATE          NULL,
    [16]        VARCHAR (12)  NULL,
    [17]        DATE          NULL,
    [18]        INT           NULL,
    [19]        DATE          NULL,
    [19.1]      INT           NULL,
    [20]        INT           NULL,
    [21]        DATE          NULL,
    [21.1]      INT           NULL,
    [22]        INT           NULL,
    [23]        VARCHAR (6)   NULL,
    [24]        VARCHAR (6)   NULL,
    [25]        VARCHAR (6)   NULL,
    [26]        VARCHAR (6)   NULL,
    [36]        INT           NULL,
    [37]        INT           NULL,
    [38]        INT           NULL,
    [39]        INT           NULL,
    [40]        DATE          NULL,
    [41]        INT           NULL,
    [42]        VARCHAR (6)   NULL,
    [43]        INT           NULL,
    [44]        DATE          NULL,
    [45]        DATE          NULL,
    [46]        INT           NULL,
    [47]        VARCHAR (6)   NULL,
    [48]        INT           NULL,
    [49]        INT           NULL,
    [50]        VARCHAR (6)   NULL,
    [51]        VARCHAR (6)   NULL,
    [52]        INT           NULL,
    [53]        INT           NULL,
    [54]        INT           NULL,
    [55]        DATE          NULL,
    [56]        DATE          NULL,
    [57]        INT           NULL,
    [58]        INT           NULL,
    [59]        VARCHAR (6)   NULL,
    [60]        VARCHAR (6)   NULL,
    [61]        VARCHAR (6)   NULL,
    [62]        INT           NULL,
    [62.1]      INT           NULL,
    [62.2]      INT           NULL,
    [62.3]      INT           NULL,
    [62.4]      INT           NULL,
    [62.5]      INT           NULL,
    [62.6]      INT           NULL,
    [62.7]      INT           NULL,
    [62.8]      INT           NULL,
    [62.9]      INT           NULL,
    [62.10]     INT           NULL,
    [62.11]     INT           NULL,
    [63]        DATE          NULL,
    [63.1]      NUMERIC (12)  NULL,
    [64]        INT           NULL,
    [65]        NUMERIC (12)  NULL,
    [66]        NUMERIC (12)  NULL,
    [67]        INT           NULL,
    [68]        INT           NULL,
    [69]        INT           NULL,
    [69.1]      DATE          NULL,
    [69.2]      DATE          NULL,
    [69.3]      DATE          NULL,
    [69.4]      DATE          NULL,
    [69.5]      DATE          NULL,
    [69.6]      DATE          NULL,
    [69.7]      DATE          NULL,
    [70]        INT           NULL,
    [70.1]      INT           NULL,
    [70.2]      INT           NULL,
    [70.3]      INT           NULL,
    [70.4]      INT           NULL,
    [70.5]      INT           NULL,
    [70.6]      INT           NULL,
    [70.7]      INT           NULL,
    [70.8]      NUMERIC (12)  NULL,
    [70.9]      INT           NULL,
    [71]        INT           NULL,
    [72]        DATE          NULL,
    [73]        DATE          NULL,
    [74]        INT           NULL,
    [75]        INT           NULL,
    [76]        INT           NULL,
    [77]        INT           NULL,
    [78]        INT           NULL,
    [79]        INT           NULL,
    [80]        INT           NULL,
    [80.1]      DATE          NULL,
    [81]        NUMERIC (20)  NULL,
    [82]        DATE          NULL,
    [27]        VARCHAR (6)   NULL,
    [27.1]      DATE          NULL,
    [28]        VARCHAR (6)   NULL,
    [28.1]      DATE          NULL,
    [29]        VARCHAR (6)   NULL,
    [29.1]      DATE          NULL,
    [30]        VARCHAR (6)   NULL,
    [30.1]      DATE          NULL,
    [31]        VARCHAR (6)   NULL,
    [31.1]      DATE          NULL,
    [32]        VARCHAR (6)   NULL,
    [32.1]      DATE          NULL,
    [33]        VARCHAR (6)   NULL,
    [33.1]      DATE          NULL,
    [34]        VARCHAR (6)   NULL,
    [34.1]      DATE          NULL,
    [35]        VARCHAR (6)   NULL,
    CONSTRAINT [PK_HCAltoCostoERC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de filtración glomerular (TFG)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'35';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de la última parathormona PTH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'34.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Parathormona PTH (pg/mL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'34';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha del último colesterol LDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'33.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Colesterol LDL (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'33';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha del último colesterol HDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'32.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Colesterol HDL (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'32';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha del último colesterol total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'31.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Colesterol total (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de la última albuminuria/creatinuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'30.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de la última albuminuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'29.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Albuminuria (mg/24h)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'29';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de última hemoglobina glicosilada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'28.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Hemoglobina glicosilada (%)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha de última creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'27.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la creatinina en sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de corte del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'82';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'80.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Causa de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'80';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Novedad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'79';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Código de la EPS de origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'78';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'77';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de prestación de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'76';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo de la terapia postrasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'75';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Número de trasplantes renales que ha recibido el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'74';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de retorno a diálisis por pérdida definitiva del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'73';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha del primer rechazo agudo del injerto (confirmado por biopsia en los primeros 12 meses posteriores al trasplante)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'72';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'episodios de rechazo agudo confirmados por biopsia en los primeros 12 meses posteriores al trasplante, ha presentado el paciente con trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'71';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'medicamentos inmunosupresores no incluidos en las variables 70.1 a 70.6 o no incluidos en el plan de beneficios (medicamento 3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'70.9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'recibe para el manejo del trasplante renal medicamentos inmunosupresores no incluidos en las variables 70.1 a 70.6 o no incluidos en el plan de beneficios  (medicamento 2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'70.8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'medicamentos inmunosupresores no incluidos en las variables 70.1 a 70.6 o no incluidos en el plan de beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'70.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Prednisona para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'70.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Tacrolimus para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'70.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Micofenolato para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'70.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Ciclosporina para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'70.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Azatioprina para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'70.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Metilprednisolona para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'70.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Cuántos medicamentos inmunosupresores se formularon para el manejo en este último corte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'70';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha del primer diagnóstico de cáncer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'69.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de diagnóstico si ha presentado alguna complicación herida quirúrgica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'69.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico si ha presentado alguna complicación urológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'69.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico si ha presentado alguna complicación vascular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'69.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico si ha presentado infección por tuberculosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'69.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico si ha presentado infección por hongos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'69.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico si ha presentado infección por citomegalovirus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'69.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'El usuario ha presentado alguna complicación relacionada con el trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'69';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo del trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'68';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Tipo de donante de último transplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'67';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Código de la IPS o Grupo de trasplante, que realizó el trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'66';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Código de la EPS que realizó el trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'65';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda El usuario ha recibido trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'64';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Código de la IPS donde está en lista de espera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'63.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de ingreso a lista de espera para la realización del trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'63';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62.11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' reportó como contraindicación para el trasplante renal, en la valoración de nefrología, que el paciente presenta enfermedad pulmonar crónica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62.10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'reportó como contraindicación para el trasplante renal, en la valoración de nefrología, que el paciente presenta enfermedad inmunológica activa los últimos tres meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62.9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó infección por el VHC, como contraindicación para el trasplante renal, en la valoración de nefrología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62.8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó infección por el VIH, como contraindicación para el trasplante renal, en la valoración de nefrología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'enfermedad cardiaca, cerebrovascular o vascular periférica, como contraindicación para el trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente presenta esperanza de vida menor o igual a 6 meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente NO ha manifestado su deseo de trasplantarse', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó infección crónica o activa no tratada o no controlada hasta en los últimos tres meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'e reportó cáncer activo en los últimos 12 meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última Valoración Clínica inicial por nefrología a personas ERC5 en diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'62';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Fórforo sérico (P) (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'61';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Albúmina sérica (g/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'60';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemoglobina (g/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'59';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Costo total del TMND', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'58';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Tratamiento no Dialítico para ERC estadio 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'57';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha de diagnóstico de la infección por Hepatitis C, si el usuario la ha presentado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'56';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Fecha de diagnóstico de la infección por Hepatitis B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'55';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Vacuna Hepatitis B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'54';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo DP durante el período de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'53';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Peritonitis Infecciosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'52';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Número de horas de diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'51';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Dosis de diálisis (Kt/V) dpd. KTV/dpd', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'50';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Diálisis peritoneal (DP)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'49';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo total de la hemodiálisis HD durante el período de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'48';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Dosis de diálisis (Kt/V) single pool', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'47';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda   Hemodiálisis (HD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'46';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha de ingreso a la unidad renal actual que le presta el servicio de terapia dialítica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'45';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se inició la terapia de reemplazo renal que recibe el usuario en el momento de la fecha de corte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'44';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Modo de inicio de la primera Terapia de Reemplazo Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'43';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el usuario inició la primera terapia de reemplazo renal -TRR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'42';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'persona se encuentra en un programa de atención de ERC (renoprotección, nefroprotección, protección renal, prediálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'41';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha de diagnóstico de ERC estadio 5 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'40';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Estadio de ERC (Enfermedad Renal Crónica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'39';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'usuario tiene diagnóstico de ERC en cualquier de sus estadios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'38';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'recibe Antagonista de los receptores de angiotensina 11', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'37';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  El usuario recibe Inhibidor de la Enzima convertidora de angiotensina (lECA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'36';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda lo selecionado Etiología de la ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Costo DM durante el período de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'21.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico de la Diabetes Mellitus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  El usuario tiene diagnostico confirmado de Diabetes Mellitus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Costo HTA durante el período de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'19.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico de Hipertensión Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  El usuario tiene diagnóstico confirmado 1 = Si 2= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'17. Fecha de ingreso al programa de atención renal (nefroprotección, protección renal) dentro de la EAPB que reporta:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Código de la IPS donde se hace seguimiento al usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la  Fecha de afiliación a la EAPB que registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'15';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Número telefónico del paciente (Incluyendo familiares y cuidadores)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'14';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Guarda el Municipio de residencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'13';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Guarda el Grupo poblacional   1 =Indigentes     2 = Población infantil a cargo del ICBF   3 =Madres comunitarias    4 =Artistas, autores, compositores   5 =Otro grupo poblacional   6 =Recién nacidos   8 =Desmovilizados  9 =Desplazados    10 =Población ROM   11 = Población raizal  12 =Población en centros psiquiátricos   13 =Migratorio   14 =Población en centros carcelarios  15 =Población rural no migratoria    16 =Afrocolombiano   31 = Adulto mayor   32 =Cabeza de familia  33 =Mujer embarazada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Código pertenencia étnica    1 = lndígena   2 = ROM (gitano)   3 =Raizal del archipiélago de San Andrés y Providencia    4 =Palenquero de San Basilio    5 =Negro(a) mulato(a), afrocolombiano(a) o afrodescendiente    6 =Ninguna de las anteriores  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Código de la EAPB o de la entidad Territorial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Régimen de afiliación al SSGSS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Sexo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Guarda la Fecha de Nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Número de Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Guara el Tipo de Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Segundo Apellido del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Primer Apellido del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gurada el Segundo Nombre del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' guarda Primer Nombre del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el código diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de pacientes con Enfermedad Renal Crónica (ERC) en el programa de Alto Costo. Almacena los datos clínicos, laboratorios, tratamientos y seguimiento periódico requeridos para el reporte a la Cuenta de Alto Costo (CAC) según normativa colombiana.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento o terapia inmunosupresora número 1 prescrita al paciente con ERC (campo 23 del formulario CAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento o terapia inmunosupresora número 2 prescrita al paciente con ERC (campo 24 del formulario CAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'24';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'24';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento o terapia inmunosupresora número 3 prescrita al paciente con ERC (campo 25 del formulario CAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'25';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'25';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento o terapia inmunosupresora número 4 prescrita al paciente con ERC (campo 26 del formulario CAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'26';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'26';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación o documento del paciente con ERC (cédula, identificación, documento del paciente en el reporte de alto costo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'81';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'81';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento o tratamiento adicional número 4 asociado al manejo de la ERC (campo 30 del formulario CAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'30';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoERC', @level2type = N'COLUMN', @level2name = N'30';
