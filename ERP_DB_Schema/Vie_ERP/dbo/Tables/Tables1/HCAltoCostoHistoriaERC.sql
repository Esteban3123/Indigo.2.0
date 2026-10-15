CREATE TABLE [dbo].[HCAltoCostoHistoriaERC] (
    [ID]            INT          IDENTITY (1, 1) NOT NULL,
    [ESTADO]        INT          NOT NULL,
    [CODDIAGNO]     CHAR (4)     NOT NULL,
    [IPCODPACI]     VARCHAR (25) NOT NULL,
    [NUMINGRES]     CHAR (10)    NOT NULL,
    [NUMEFOLIO]     CHAR (10)    NOT NULL,
    [CODPROSAL]     CHAR (20)    NOT NULL,
    [FECHAREGISTRO] DATETIME     NOT NULL,
    [CODCENATE]     CHAR (10)    NOT NULL,
    [UFUCODIGO]     CHAR (10)    NOT NULL,
    [16]            VARCHAR (12) NULL,
    [17]            DATE         NULL,
    [18]            INT          NULL,
    [19]            DATE         NULL,
    [19.1]          INT          NULL,
    [20]            INT          NULL,
    [21]            DATE         NULL,
    [21.1]          INT          NULL,
    [22]            INT          NULL,
    [36]            INT          NULL,
    [37]            INT          NULL,
    [38]            INT          NULL,
    [39]            INT          NULL,
    [40]            DATE         NULL,
    [41]            INT          NULL,
    [42]            VARCHAR (6)  NULL,
    [43]            INT          NULL,
    [44]            DATE         NULL,
    [45]            DATE         NULL,
    [46]            INT          NULL,
    [47]            VARCHAR (6)  NULL,
    [49]            INT          NULL,
    [50]            VARCHAR (6)  NULL,
    [51]            VARCHAR (6)  NULL,
    [52]            INT          NULL,
    [54]            INT          NULL,
    [55]            DATE         NULL,
    [56]            DATE         NULL,
    [57]            INT          NULL,
    [59]            VARCHAR (6)  NULL,
    [60]            VARCHAR (6)  NULL,
    [61]            VARCHAR (6)  NULL,
    [62]            INT          NULL,
    [62.1]          INT          NULL,
    [62.2]          INT          NULL,
    [62.3]          INT          NULL,
    [62.4]          INT          NULL,
    [62.5]          INT          NULL,
    [62.6]          INT          NULL,
    [62.7]          INT          NULL,
    [62.8]          INT          NULL,
    [62.9]          INT          NULL,
    [62.10]         INT          NULL,
    [62.11]         INT          NULL,
    [63]            DATE         NULL,
    [63.1]          NUMERIC (12) NULL,
    [64]            INT          NULL,
    [65]            NUMERIC (12) NULL,
    [66]            NUMERIC (12) NULL,
    [67]            INT          NULL,
    [69]            INT          NULL,
    [69.1]          DATE         NULL,
    [69.2]          DATE         NULL,
    [69.3]          DATE         NULL,
    [69.4]          DATE         NULL,
    [69.5]          DATE         NULL,
    [69.6]          DATE         NULL,
    [69.7]          DATE         NULL,
    [70]            INT          NULL,
    [70.1]          INT          NULL,
    [70.2]          INT          NULL,
    [70.3]          INT          NULL,
    [70.4]          INT          NULL,
    [70.5]          INT          NULL,
    [70.6]          INT          NULL,
    [70.7]          INT          NULL,
    [70.8]          NUMERIC (12) NULL,
    [70.9]          INT          NULL,
    [71]            INT          NULL,
    [72]            DATE         NULL,
    [73]            DATE         NULL,
    [74]            INT          NULL,
    [79]            INT          NULL,
    [80]            INT          NULL,
    [80.1]          DATE         NULL,
    [27]            VARCHAR (6)  NULL,
    [27.1]          DATE         NULL,
    [28]            VARCHAR (6)  NULL,
    [28.1]          DATE         NULL,
    [29]            VARCHAR (6)  NULL,
    [29.1]          DATE         NULL,
    [30]            VARCHAR (6)  NULL,
    [30.1]          DATE         NULL,
    [31]            VARCHAR (6)  NULL,
    [31.1]          DATE         NULL,
    [32]            INT          NULL,
    [32.1]          DATE         NULL,
    [33]            VARCHAR (6)  NULL,
    [33.1]          DATE         NULL,
    [34]            VARCHAR (6)  NULL,
    [34.1]          DATE         NULL,
    [23]            VARCHAR (6)  NULL,
    [24]            VARCHAR (6)  NULL,
    [25]            VARCHAR (6)  NULL,
    [26]            VARCHAR (6)  NULL,
    [35]            VARCHAR (6)  NULL,
    [48]            INT          NULL,
    [53]            INT          NULL,
    [58]            INT          NULL,
    [68]            INT          NULL,
    [75]            INT          NULL,
    [76]            INT          NULL,
    [77]            INT          NULL,
    [78]            INT          NULL,
    [81]            NUMERIC (20) NULL,
    [82]            DATE         NULL,
    CONSTRAINT [PK_HCAltoCostoHistoriaERC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de la última parathormona PTH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'34.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Parathormona PTH (pg/mL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'34';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha del último colesterol LDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'33.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Colesterol LDL (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'33';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha del último colesterol HDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'32.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Colesterol HDL (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'32';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha del último colesterol total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'31.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Colesterol total (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de la última albuminuria/creatinuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'30.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Relación albuminuria/creatinuria ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'30';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de la última albuminuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'29.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Albuminuria (mg/24h)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'29';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de última hemoglobina glicosilada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'28.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Hemoglobina glicosilada (%)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha de última creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'27.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la creatinina en sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'80.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Causa de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'80';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Novedad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'79';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Número de trasplantes renales que ha recibido el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'74';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de retorno a diálisis por pérdida definitiva del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'73';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha del primer rechazo agudo del injerto (confirmado por biopsia en los primeros 12 meses posteriores al trasplante)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'72';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'episodios de rechazo agudo confirmados por biopsia en los primeros 12 meses posteriores al trasplante, ha presentado el paciente con trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'71';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'medicamentos inmunosupresores no incluidos en las variables 70.1 a 70.6 o no incluidos en el plan de beneficios (medicamento 3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'recibe para el manejo del trasplante renal medicamentos inmunosupresores no incluidos en las variables 70.1 a 70.6 o no incluidos en el plan de beneficios  (medicamento 2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'medicamentos inmunosupresores no incluidos en las variables 70.1 a 70.6 o no incluidos en el plan de beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Prednisona para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Tacrolimus para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Micofenolato para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Ciclosporina para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Azatioprina para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualmente (última prescripción) el paciente recibe Metilprednisolona para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Cuántos medicamentos inmunosupresores se formularon para el manejo en este último corte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha del primer diagnóstico de cáncer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de diagnóstico si ha presentado alguna complicación herida quirúrgica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico si ha presentado alguna complicación urológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico si ha presentado alguna complicación vascular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico si ha presentado infección por tuberculosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico si ha presentado infección por hongos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico si ha presentado infección por citomegalovirus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'El usuario ha presentado alguna complicación relacionada con el trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Tipo de donante de último transplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'67';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Código de la IPS o Grupo de trasplante, que realizó el trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'66';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Código de la EPS que realizó el trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'65';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda El usuario ha recibido trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'64';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Código de la IPS donde está en lista de espera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'63.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de ingreso a lista de espera para la realización del trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'63';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' reportó como contraindicación para el trasplante renal, en la valoración de nefrología, que el paciente presenta enfermedad pulmonar crónica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'reportó como contraindicación para el trasplante renal, en la valoración de nefrología, que el paciente presenta enfermedad inmunológica activa los últimos tres meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó infección por el VHC, como contraindicación para el trasplante renal, en la valoración de nefrología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó infección por el VIH, como contraindicación para el trasplante renal, en la valoración de nefrología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'enfermedad cardiaca, cerebrovascular o vascular periférica, como contraindicación para el trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente presenta esperanza de vida menor o igual a 6 meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente NO ha manifestado su deseo de trasplantarse', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se reportó infección crónica o activa no tratada o no controlada hasta en los últimos tres meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'e reportó cáncer activo en los últimos 12 meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última Valoración Clínica inicial por nefrología a personas ERC5 en diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórforo sérico (P) (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'61';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Albúmina sérica (g/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'60';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'59';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Tratamiento no Dialítico para ERC estadio 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'57';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha de diagnóstico de la infección por Hepatitis C, si el usuario la ha presentado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'56';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Fecha de diagnóstico de la infección por Hepatitis B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'55';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Vacuna Hepatitis B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'54';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Peritonitis Infecciosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'52';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Número de horas de diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'51';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Dosis de diálisis (Kt/V) dpd. KTV/dpd', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'50';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Diálisis peritoneal (DP)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'49';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Dosis de diálisis (Kt/V) single pool', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'47';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda   Hemodiálisis (HD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'46';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha de ingreso a la unidad renal actual que le presta el servicio de terapia dialítica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'45';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se inició la terapia de reemplazo renal que recibe el usuario en el momento de la fecha de corte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'44';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Modo de inicio de la primera Terapia de Reemplazo Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'43';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el usuario inició la primera terapia de reemplazo renal -TRR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'42';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'persona se encuentra en un programa de atención de ERC (renoprotección, nefroprotección, protección renal, prediálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'41';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha de diagnóstico de ERC estadio 5 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'40';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Estadio de ERC (Enfermedad Renal Crónica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'39';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'usuario tiene diagnóstico de ERC en cualquier de sus estadios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'38';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'recibe Antagonista de los receptores de angiotensina 11', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'37';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  El usuario recibe Inhibidor de la Enzima convertidora de angiotensina (lECA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'36';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda lo selecionado Etiología de la ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Costo DM durante el período de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'21.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico de la Diabetes Mellitus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  El usuario tiene diagnostico confirmado de Diabetes Mellitus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Costo HTA durante el período de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'19.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de diagnóstico de Hipertensión Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  El usuario tiene diagnóstico confirmado de hipertensión  - HPTA        1 = Si    2= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda fecha de ingreso programa de atencion renal ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Código de la IPS donde se hace seguimiento al usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'codigo centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fecha en el q se guarda el dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'codigoprofesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda codigo del paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gurada  el estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'ID';

