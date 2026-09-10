CREATE TABLE [dbo].[HCRENAL2463D] (
    [Id]             INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRENAL2463C] INT          NOT NULL,
    [1]              VARCHAR (20) NULL,
    [2]              VARCHAR (30) NULL,
    [3]              VARCHAR (20) NULL,
    [4]              VARCHAR (30) NULL,
    [5]              VARCHAR (2)  NULL,
    [6]              VARCHAR (25) NULL,
    [7]              DATE         NULL,
    [8]              VARCHAR (1)  NULL,
    [9]              VARCHAR (1)  NULL,
    [10]             VARCHAR (6)  NULL,
    [11]             INT          NULL,
    [12]             INT          NULL,
    [13]             VARCHAR (5)  NULL,
    [14]             VARCHAR (30) NULL,
    [15]             DATE         NULL,
    [16]             VARCHAR (12) NULL,
    [17]             DATE         NULL,
    [18]             INT          NULL,
    [19]             DATE         NULL,
    [19.1]           INT          NULL,
    [20]             INT          NULL,
    [21]             DATE         NULL,
    [21.1]           INT          NULL,
    [22]             INT          NULL,
    [23]             VARCHAR (5)  NULL,
    [24]             INT          NULL,
    [25]             INT          NULL,
    [26]             INT          NULL,
    [27]             VARCHAR (5)  NULL,
    [27.1]           DATE         NULL,
    [28]             VARCHAR (5)  NULL,
    [28.1]           DATE         NULL,
    [29]             VARCHAR (5)  NULL,
    [29.1]           DATE         NULL,
    [30]             VARCHAR (5)  NULL,
    [30.1]           DATE         NULL,
    [31]             VARCHAR (5)  NULL,
    [31.1]           DATE         NULL,
    [32]             VARCHAR (5)  NULL,
    [32.1]           DATE         NULL,
    [33]             VARCHAR (5)  NULL,
    [33.1]           DATE         NULL,
    [34]             VARCHAR (5)  NULL,
    [34.1]           DATE         NULL,
    [35]             VARCHAR (5)  NULL,
    [36]             INT          NULL,
    [37]             INT          NULL,
    [38]             INT          NULL,
    [39]             INT          NULL,
    [40]             DATE         NULL,
    [41]             INT          NULL,
    [42]             INT          NULL,
    [43]             INT          NULL,
    [44]             DATE         NULL,
    [45]             DATE         NULL,
    [46]             INT          NULL,
    [47]             VARCHAR (5)  NULL,
    [48]             INT          NULL,
    [49]             INT          NULL,
    [50]             VARCHAR (5)  NULL,
    [51]             VARCHAR (5)  NULL,
    [52]             INT          NULL,
    [53]             INT          NULL,
    [54]             INT          NULL,
    [55]             DATE         NULL,
    [56]             DATE         NULL,
    [57]             INT          NULL,
    [58]             INT          NULL,
    [59]             VARCHAR (5)  NULL,
    [60]             VARCHAR (5)  NULL,
    [61]             VARCHAR (5)  NULL,
    [62]             INT          NULL,
    [62.1]           INT          NULL,
    [62.2]           INT          NULL,
    [62.3]           INT          NULL,
    [62.4]           INT          NULL,
    [62.5]           INT          NULL,
    [62.6]           INT          NULL,
    [62.7]           INT          NULL,
    [62.8]           INT          NULL,
    [62.9]           INT          NULL,
    [62.10]          INT          NULL,
    [62.11]          INT          NULL,
    [63]             DATE         NULL,
    [63.1]           VARCHAR (12) NULL,
    [64]             INT          NULL,
    [65]             VARCHAR (6)  NULL,
    [66]             VARCHAR (12) NULL,
    [67]             INT          NULL,
    [68]             INT          NULL,
    [69]             INT          NULL,
    [69.1]           DATE         NULL,
    [69.2]           DATE         NULL,
    [69.3]           DATE         NULL,
    [69.4]           DATE         NULL,
    [69.5]           DATE         NULL,
    [69.6]           DATE         NULL,
    [69.7]           DATE         NULL,
    [70]             INT          NULL,
    [70.1]           INT          NULL,
    [70.2]           INT          NULL,
    [70.3]           INT          NULL,
    [70.4]           INT          NULL,
    [70.5]           INT          NULL,
    [70.6]           INT          NULL,
    [70.7]           VARCHAR (20) NULL,
    [70.8]           VARCHAR (20) NULL,
    [70.9]           VARCHAR (20) NULL,
    [71]             INT          NULL,
    [72]             DATE         NULL,
    [73]             DATE         NULL,
    [74]             INT          NULL,
    [75]             INT          NULL,
    [76]             INT          NULL,
    [77]             INT          NULL,
    [78]             VARCHAR (6)  NULL,
    [79]             INT          NULL,
    [80]             INT          NULL,
    [80.1]           DATE         NULL,
    [REGINCONVE]     CHAR (2)     NULL,
    CONSTRAINT [PK_HCRENAL2463D] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HCRENAL2463D_HCRENAL2463C] FOREIGN KEY ([IDHCRENAL2463C]) REFERENCES [dbo].[HCRENAL2463C] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'columna que me identifica si tiene errores si = 1  no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'REGINCONVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de muerte -  Registre la fecha en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como día. Registre 1800-01-01= desconocida. Registre 1845-01-01=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'80.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Causa de Muerte -  1=Enfermedad renal crónica  2= Enfermedad Cardiovascular  3= Cáncer  4= Infección  5= Por causa diferente a las descritas en 1, 2, 3 y 4  6= Causa Externa  98= No aplica, el usuario no ha fallecido  99= Sin dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'80';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Novedad con respecto al reporte anterior -  Las novedades en este campo corresponderán a eventos ocurridos respecto al reporte anterior.   1= Persona que falleció  2= Persona que ingresó a la EPS y traía el diagnóstico de ERC  3= Persona antigua en la EPS y se le realizó nuevo diagnóstico de ERC  4= Pe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'79';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la EPS de origen -  Registre el código de la (EPS/EOC/ESS/CCF) donde estaba afiliado el usuario antes de trasladarse a la EPS que reporta.   98= No aplica.   99=Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'78';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo Total -  Costo total acumulado de la atención del usuario a la fecha de reporte (de 1 de julio a 30 de junio). En este campo se deben agregar todos los gastos en el usuario, relacionados con los diagnósticos registrados, incluyendo, entre otros, los costos de cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'77';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de prestación de servicios -  Número de meses en los que el usuario efectivamente recibió servicios a cargo de la EPS que reporta (del periodo comprendido entre 1 de julio del año anterior y 30 de junio de año de reporte).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'76';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo de la terapia postrasplante -  Registre Costo total de la terapia postrasplante del usuario durante el período de reporte (1 de julio del año anterior hasta el 30 de junio). Para los pacientes que iniciaron terapia postrasplante después del 1 de julio cuente el costo incurrido desde el', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'75';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de trasplantes renales que ha recibido -  Registre el número de trasplantes recibidos.   98= No aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'74';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de retorno a diálisis -  Registre la fecha en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 01. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'73';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del primer rechazo del injerto -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'72';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Cuantos episodios de rechazo agudo en los últimos 12 meses al trasplante, confirmado por biopsia, ha presentado el usuario? -  Registre el número de episodios.   98= No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'71';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En tratamiento inmunosupresor ha recibido Medicamento NO POS (medicamento 3) -  Registre el código CUM del medicamento NO POS usado en este caso (códigos CUM disponibles en la página Web de la CAC)  97=No Aplica (no recibió medicamento no POS)  98= No Aplica (El usuario no ha tenido tratamiento inmunosupresor).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'70.9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En tratamiento inmunosupresor ha recibido Medicamento NO POS (medicamento 2) -  Registre el código CUM del medicamento NO POS usado en este caso (códigos CUM disponibles en la página Web de la CAC)  97=No Aplica (no recibió medicamento no POS)  98= No Aplica (El usuario no ha tenido tratamiento inmunosupresor).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'70.8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En tratamiento inmunosupresor ha recibido Medicamento NO POS (medicamento 1) -  Registre el código CUM del medicamento NO POS usado en este caso (códigos CUM disponibles en la página Web de la CAC)  97=No Aplica (no recibió medicamento no POS)  98= No Aplica (El usuario no ha tenido tratamiento inmunosupresor).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'70.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En algún momento, desde el último reporte hasta el reporte actual ha recibido prednisona -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'70.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En algún momento, desde el último reporte hasta el reporte actual ha recibido tacrolimus -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'70.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En algún momento, desde el último reporte hasta el reporte actual ha recibido micofenolato -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'70.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En algún momento, desde el último reporte hasta el reporte actual ha recibido ciclosporina -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'70.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En algún momento, desde el último reporte hasta el reporte actual ha recibido azatioprina -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'70.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En algún momento, desde el último reporte hasta el reporte actual ha recibido metilprednisolona -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'70.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantos medicamentos inmunosupresores se formularon para el manejo en este último corte -  número de medicamentos inmunosupresores formulados en el periodo de reporte actual  98= No Aplica (Paciente no está en proceso de trasplante, ni tiene trasplante).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'70';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del primer diagnóstico de cáncer -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'69.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnóstico si ha presentado alguna complicación herida quirúrgica -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'69.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnóstico si ha presentado alguna complicación urológica -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'69.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnóstico si ha presentado alguna complicación vascular -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'69.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnóstico si ha presentado infección por tuberculosis -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'69.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnóstico si ha presentado infección por hongos -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'69.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnóstico si ha presentado infección por Citomegalovirus -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'69.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿El usuario ha presentado alguna complicación relacionada con el trasplante renal? -  1= Si  2=No  98= No aplica  99=Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'69';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo del trasplante -  Costo total del trasplante incluyendo todos los gastos por servicios POS asociados al procedimiento de trasplante que fueron cubiertos por la EPS o EOC, tales como la obtención o rescate del componente anatómico, su preservación y almacenamiento, así como', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'68';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de donante -  1= Fallecido  2=Vivo  98=No aplica  99=Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'67';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS o Grupo de trasplante, que realizó el trasplante -  código de habilitación de la IPS que realizó el trasplante (grupo de trasplante).   98= No aplica.   99= Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'66';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la EPS que realizó el trasplante -  código de la (EPS/EOC/ESS/CCF) que realizó o pagó el trasplante renal (sea este funcional o no al momento del corte)  98= No aplica  99=Sin dato (Solamente cuando el trasplante renal no haya sido efectuado por la EPS o EOC que reporta el usuario y no se dis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'65';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿El usuario ha recibido trasplante renal? -  1=SI, el usuario ha recibido trasplante renal en la EPS o EOC que reporta (y está funcional)  2= SI, el usuario ha recibido trasplante renal, pero no en la EPS o EOC que reporta (y está funcional)  3= SI, el usuario ha recibido trasplante renal en la EPS o ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'64';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registre el código de la IPS donde está en lista de espera -  código válido de habilitación (códigos de habilitación disponibles en la página Web de la CAC – IPS trasplantadoras -)  98= No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'63.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Ingreso a lista de espera para la realización del trasplante -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'63';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología, que el paciente presenta otras enfermedades crónicas? -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62.11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología, que el paciente presenta enfermedad pulmonar crónica? -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62.10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología, que el paciente presenta enfermedad inmunológica activa los últimos tres meses antes de la fecha de corte? -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62.9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se reportó infección por el VHC, como contraindicación para el trasplante renal, en la valoración de nefrología? -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62.8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se reportó infección por el VIH, como contraindicación para el trasplante renal, en la valoración de nefrología? -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se reportó enfermedad cardiaca, cerebrovascular o vascular periférica, como contraindicación para el trasplante renal, en la valoración de nefrología? -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente presenta potenciales limitaciones al autocuidado y adherencia al tratamiento post trasplante? -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente presenta esperanza de vida menor o igual a 6 meses? -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente NO ha manifestado su deseo de trasplantarse? -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se reportó infección crónica o activa no tratada o no controlada hasta en los últimos tres meses antes de la fecha de corte, como contraindicación para el trasplante renal, en la valoración de nefrología? -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se reportó cáncer activo en los últimos 12 meses, como contraindicación para el trasplante renal, en la valoración de nefrología? -  1= Si  2=No  98=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valoración Clínica inicial por nefrología a personas ERC5 en diálisis o tratamiento médico, en relación con la posibilidad de trasplante renal -  Registre el concepto de la valoración clínica realizada por el nefrólogo:   1= No contraindicado  2= Contraindicado  97= No aplica porque es una persona que no está en Estadio 5 o ya tiene trasplante funcional  98= No aplica, no tiene enfermedad renal  99= sin', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'62';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fósforo (P) (aplica solo cuando el usuario está en diálisis o en Terapia no Dialítica para ERC estadio 5). Los niveles séricos de fósforo deben ser del último trimestre contado a partir de la fecha de corte y sus tomas debieron ser pre-diálisis en persona -  Valor promedio de mediciones de fósforo sérico del último trimestre contado a partir de la fecha de corte, en (mg/dl). Este valor debe estar entre 0.1 y 12 (mg/dl).  98= No aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'61';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Albúmina Sérica (aplica solo cuando el usuario está en diálisis). La albúmina debe tener máximo tres meses de antigüedad a partir de la fecha de corte y su toma debió ser pre-diálisis en personas en hemodiálisis -  Resultado (valor) del último examen de albúmina Sérica en (g/dl). Este valor debe estar entre 0.5 a 10 (g/dl). Registre   98= No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'60';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemoglobina (aplica solo cuando el usuario está en diálisis o en Terapia no Dialítica para ERC estadio 5). Las hemoglobinas deben ser del último trimestre contado a partir de la fecha de corte y sus tomas debieron ser pre-diálisis en personas en hemodiáli -  Valor promedio de hemoglobinas del último trimestre contado a partir de la fecha de corte, en (g/dl). Este valor debe estar entre 3 y 23 (g/dl).  98= No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'59';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo de la terapia ERC estadio 5 con tratamiento médico -  costo de la terapia exclusivamente con tratamiento médico para ERC5 (lo que la EPS o EOC considere contenido en dicha terapia), excluyendo otros gastos en esta persona que no están relacionados con la TRR.  98= No aplica, cuando el usuario no recibió dicha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'58';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Terapia no Dialítica para ERC estadio 5 (también llamada tratamiento médico de nefroproteción) -  1= el usuario con ERC estadio 5 recibe solamente tratamiento médico especial y multidisciplinario sin diálisis en el momento de la fecha de corte  2= el usuario con ERC estadio 5 no recibe esta alternativa terapéutica en el momento de la fecha de corte  98=', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'57';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registre la fecha de diagnóstico de la infección por Hepatitis C, si el usuario la ha presentado -  Registre la fecha de diagnóstico de la infección por Hepatitis C, si el usuario la ha presentado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'56';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registre la fecha de diagnóstico de la infección por Hepatitis B, si el usuario la ha presentado -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'55';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vacuna Hepatitis B -  1= Tiene esquema completo  2= Tiene esquema incompleto  3= No tiene vacunación de hepatitis B  98= No aplica (sin TRR)  99= Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'54';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo DP durante el período de reporte -  costo de la DP (lo que la EPS o EOC considere contenido en la DP), excluyendo otros gastos en esta persona como las citas de control o los medicamentos que no están relacionados con la TRR. Registre  98= No aplica, cuando el usuario no recibió terapia de D', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'53';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peritonitis -  Número de episodios de peritonitis Infecciosa que sufrió el usuario en los últimos 12 meses a la fecha de reporte  98= No Aplica, el usuario no ha estado en diálisis peritoneal en ningún momento en los últimos 12 meses.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'52';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de horas de hemodiálisis -  número de horas promedio de las sesiones de hemodiálisis realizadas en los últimos 3 meses al paciente que recibe hemodiálisis a la fecha de corte  98= No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'51';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis de diálisis (Kt/V) KTV/dpd de máximo cuatro meses de antigüedad contados a partir de la fecha de corte -  resultado (valor) de la dosis de diálisis en las sesiones realizadas al paciente, de los últimos 4 meses, expresado como el volumen de fluido filtrado de urea sobre el volumen de agua en el cuerpo del usuario (Kt/V). Este valor debe estar entre 0.5 y 4.5 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'50';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diálisis peritoneal (DP) -  1= el usuario recibe diálisis peritoneal manual al momento de la fecha de corte  2= el usuario recibe diálisis peritoneal automatizada al momento de la fecha de corte  98= No aplica, el usuario no recibe terapia de diálisis peritoneal al momento de la fecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'49';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo total de la hemodiálisis (HD) durante el período de reporte -  Registre exclusivamente el costo de la HD (lo que la EPS o EOC considere contenido en la HD), excluyendo otros gastos en esta persona como las citas de control o los medicamentos que no están relacionados con la TRR.  98= No aplica, cuando el usuario no re', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'48';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis de diálisis (Kt/V) single pool -  resultado (valor) del promedio de las últimas mediciones del trimestre de la dosis de diálisis (se mide de forma mensual) expresado como el volumen de fluido filtrado de urea sobre el volumen de agua en el cuerpo del usuario (Kt/V). Este valor debe estar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'47';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemodiálisis (HD) -  1= el usuario recibe terapia de hemodiálisis a través de fístula al momento de la Fecha de corte  2= el usuario recibe terapia de hemodiálisis a través de catéter al momento de la fecha de corte  98= No aplica, el usuario no recibe terapia de hemodiálisis e', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'46';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Ingreso a la Unidad Renal Actual que le presta el servicio al momento de la fecha de corte en cualquier modalidad de terapia dialítica -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'45';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se inició la terapia de reemplazo renal que recibe el usuario en el momento de la fecha de corte (si el Trasplante es la terapia reportada, esta Fecha se refiere a la Fecha de trasplante) -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'44';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modo de Inicio de la Terapia de Reemplazo Renal (primera TRR) -  1= el usuario con ERC estadio 5 inició por primera vez la terapia dialítica ingresando por URGENCIAS  2= el usuario con ERC estadio 5 inició por primera vez la terapia dialítica de forma PROGRAMADA  3= el usuario inició la TRR en otra EPS diferente a la que', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'43';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TFG medida en la fecha en que el usuario inició la primera terapia de reemplazo renal –TRR- -  valor medido de TFG (es diferente del estimado por las fórmulas de C-G u otras)  98= No aplica, usuario no ha tenido TRR  99= Sin dato, no se conoce porque el usuario tuvo su primera TRR en una EPS diferente a la que reporta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'42';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La persona se encuentra en un programa de atención de ERC (renoprotección, nefroprotección, protección renal, prediálisis) -  1= Si  2= No  98=No aplica  99=Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'41';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnóstico de ERC estadio 5 (Solo aplica si han presentado ERC5 –TRR tipo diálisis u otras-) -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'40';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estadio de ERC (Enfermedad Renal Crónica) -  1= el usuario tiene TFG igual o mayor a 90 ml/min y pruebas clínicas que soportan daño renal  2= el usuario tiene TFG entre 60 y menor de 90 ml/min y pruebas clínicas que soportan daño renal  3= el usuario tiene TFG entre 30 y menor de 60 ml/min  4= el usuar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'39';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario tiene diagnóstico de enfermedad renal crónica en cualquiera de sus estadios -  0= No presenta Enfermedad Renal Crónica  1= presenta ERC  2= Indeterminado entre estadios 1, 2 ó sin ERC  3= el usuario no ha sido estudiado para ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'38';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibe Antagonista de los Receptores de Angiotensina II (ARA II) -  1= sí recibe  2=No recibió, no fue formulado dentro del plan terapéutico  3= No recibió, Aunque fue formulado dentro del plan terapéutico  98= No Aplica  99=Sin dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'37';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario recibe Inhibidor de la Enzima Convertidora de Angiotensina (IECA) -  1= sí recibe  2=No recibió, no fue formulado dentro del plan terapéutico  3= No recibió, Aunque fue formulado dentro del plan terapéutico  98= No Aplica  99=Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'36';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tasa de filtración glomerular estimada (TFGE) según Cockcroft-Gault o Swhartz -  valor calculado según fórmula de Cockcroft-Gault (en adultos) o Swhartz (de 0 a 17 años de edad) según última Creatinina válida (fórmulas y calculadora, si se requiere, se encuentran disponibles en los archivos operativos de la página web de la CAC)  988= ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'35';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la última PTH -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Registre 1800-01-01= desconocida. Registre 1845-01-01= No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'34.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PTH -  valor de la última PTH tomada en personas con ERC estadios 4 ó 5  988= No aplica por estadio renal (estadio de ERC 1, 2, 3, o no tiene ERC)  999=No existen datos en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'34';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del último LDL -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Registre 1800-01-01= desconocida. Registre 1845-01-01= No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'33.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'LDL -  Registre el valor del ultimo LDL tomado  999=No existen datos en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'33';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la último HDL -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Registre 1800-01-01= desconocida. Registre 1845-01-01= No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'32.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HDL -  Registre el valor del ultimo HDL tomado  999=No existen datos en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'32';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la último Colesterol total -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Registre 1800-01-01= desconocida. Registre 1845-01-01= No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'31.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Colesterol total -  Registre el valor del último colesterol total tomado; 999=No existen datos en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la última Creatinuria -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Registre 1800-01-01= desconocida. Registre 1845-01-01= No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'30.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Creatinuria -  valor de la última Creatinuria realizada  98= No Aplica, usuario con ERC 5  99=No existen datos en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'30';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la última Albuminuria -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Registre 1800-01-01= desconocida. Registre 1845-01-01= No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'29.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Albuminuria (microalbuminuria) -  Registre el valor de la última albuminuria tomada; 999=No existen datos en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'29';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de última Hemoglobina Glicosilada -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Registre 1800-01-01= desconocida. Registre 1845-01-01=No Aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'28.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemoglobina Glicosilada -  Valor de la última Hemoglobina Glicosilada en personas con diagnóstico de Diabetes Mellitus  98= No Aplica, usuario no tiene Diabetes Mellitus  99=No existen datos en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de última Creatinina -  Registre la fecha en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce sólo el año y el mes, registre el día 01. Registre 1800-01-01= desconocida. Registre 1845-01-01= No Aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'27.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Creatinina en sangre (mg/dl) -  Registre el valor de la última Creatinina tomada  98= No Aplica, usuario con ERC5 a quien ya no le miden creatinina  99=No existen datos en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tensión arterial diastólica (mm de Hg) -  Registre el último valor absoluto reportado en la historia clínica relacionada con su patología de base (del último mes en personas con ERC5 y de los últimos 6 meses en las demás personas reportadas). Registre 999 si no tiene valor de tensión arterial dia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'26';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tensión arterial sistólica (mm de Hg) -  Registre el último valor absoluto reportado en la historia clínica relacionada con su patología de base (del último mes en personas con ERC5 y de los últimos 6 meses en las demás personas reportadas). Registre 999 si no tiene valor de tensión arterial sis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'25';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla (cm) -  Registre el último valor absoluto reportado en la historia clínica (sin decimales)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'24';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso (kg) -  Registre el último valor absoluto reportado en la historia clínica (con máximo un decimal) dentro del periodo de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Etiología de la ERC -  1= HTA o DM  2= Autoinmune  3= Nefropatía Obstructiva  4= Enfermedad Poliquística  5= otras  98= Si no tiene ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo DM durante el período de reporte -  Registre exclusivamente el costo de la DM, excluyendo otros gastos en esta persona como las citas de control o los medicamentos que no están relacionados con la DM. Registre 98= No aplica, cuando el usuario no tiene este diagnóstico durante el período de ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'21.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnóstico de la Diabetes Mellitus -  Registre la fecha en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como día. Registre 1800-01-01= desconocida. Registre 1845-01-01=No Aplica. Verifique que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario tiene diagnóstico confirmado de Diabetes Mellitus- DM (CIE-10 con códigos entre E10-E149; O24-O249; P702) -  1= Si   2=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo HTA durante el período de reporte -  Registre exclusivamente el costo de la HTA, excluyendo otros gastos en esta persona como las citas de control o los medicamentos que no están relacionados con la HTA. Registre 98= No aplica, cuando el usuario no tiene este diagnóstico durante el período d', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'19.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnóstico de la Hipertensión Arterial -  Registre la fecha en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 01. Si conoce solamente el año registre el año e incluya 01 como mes y 01 como día. Registre 1800-01-01= desconocida. Registre 1845-01-01=No Aplica. Verifique que ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El usuario tiene diagnóstico confirmado de Hipertensión Arterial -HTA (CIE-10 con códigos entre I10-I159) -  1= Si   2=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de ingreso al programa de nefroprotección dentro de la EPS que reporta -  Registre la fecha en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 01. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). Si conoce solamente el año registre el año e incluya 01 como mes y 01 como ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS donde se hace seguimiento al usuario -  Registre el código válido de habilitación según el estado clínico de seguimiento del usuario: *Para las personas en diálisis corresponde al código de la unidad renal (los códigos de habilitación de IPS dializadoras están disponibles en los archivos operat', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de afiliación a la EPS que registra -  Fecha en la que el usuario se afilió a la EPS en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). La fecha de afiliación no puede ser inferior a la fecha de nacimiento y debe ser superior a 1995-01', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'15';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número telefónico del paciente (incluyendo a familiares y cuidadores) -  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'14';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Municipio de residencia -  Registre el Código del municipio en donde reside el afiliado según la división político administrativa DIVIPOLA – DANE. Este código debe ser reportado en 5 dígitos, donde los dos primeros dígitos corresponden al departamento donde se localiza el municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'13';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo poblacional -  1=Indigentes; 2=Población infantil a cargo del ICBF; 3=Madres comunitarias; 4=Artistas, autores, compositores; 5=Otro grupo poblacional; 6=Recién nacidos; 7=Discapacitados; 8=Desmovilizados; 9=Desplazados; 10=Población ROM; 11=Población raizal; 12=Poblaci', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código pertenencia étnica -  1=Indígena  2=ROM (gitano)  3=Raizal del archipiélago de San Andrés y Providencia  4=Palenquero de san Basilio  5=Negro(a), mulato(a), afro colombiano(a) o afro descendiente  6=Ninguna de las anteriores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la EPS o de la entidad territorial -  Cuando el usuario tenga EPS u otra EOC escriba el código de la empresa aseguradora que registra al usuario (EPS/EOC/EPSI/ESS/CCF/EAS). Los códigos de todas las EPS/EOC, autorizadas por la Superintendencia Nacional de Salud, están disponibles en los archiv', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Régimen de afiliación AL SGSS -  C=Régimen Contributivo  S=Régimen Subsidiado  P=Regímenes de excepción  E=Régimen especial  N=No asegurado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo -  F si es femenino  M si es masculino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de nacimiento -  Fecha de nacimiento del usuario en el formato AAAA-MM-DD. Verifique que el orden de los números sea AÑO-MES-DÍA y el separador sea guión (-). La fecha de nacimiento no puede ser superior a la fecha de afiliación ni a la fecha de corte del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Identificación del usuario -  Longitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Identificación del usuario -  RC=Registro Civil  TI=Tarjeta Identidad  CC=Cédula de Ciudadanía  CE=Cédula Extranjería  PA=Pasaporte  MS=Menor sin Identificación  AS=Adulto sin Identificación  CD=Carnet Diplomático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo apellido del usuario -  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer apellido del usuario -  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo nombre del usuario -  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer nombre del usuario -  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla  HCRENAL2463C para relacionar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'IDHCRENAL2463C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de secciones del formulario de historia clínica renal (nefrología), correspondiente al bloque D del formulario 2463. Contiene las respuestas estructuradas de campos clínicos relacionados con el seguimiento renal del paciente, vinculada al encabezado de la historia clínica renal mediante IDHCRENAL2463C.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463D';
