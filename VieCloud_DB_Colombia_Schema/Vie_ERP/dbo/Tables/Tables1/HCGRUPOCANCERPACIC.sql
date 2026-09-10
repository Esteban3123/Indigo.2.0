CREATE TABLE [dbo].[HCGRUPOCANCERPACIC] (
    [ID]                 INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ESTADO]             INT                                                                              NOT NULL,
    [CODDIAGNO]          CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]          CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]          CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]          CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHAREGISTRO]      DATETIME                                                                         NOT NULL,
    [CODCENATE]          CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]          CHAR (10)                                                                        NOT NULL,
    [TIPO]               INT                                                                              NOT NULL,
    [18]                 DATE                                                                             NULL,
    [21]                 INT                                                                              NULL,
    [22]                 INT                                                                              NULL,
    [23]                 DATE                                                                             NULL,
    [24]                 DATE                                                                             NULL,
    [26]                 DATE                                                                             NULL,
    [27]                 INT                                                                              NULL,
    [28]                 INT                                                                              NULL,
    [29]                 INT                                                                              NULL,
    [30]                 DATE                                                                             NULL,
    [31]                 INT                                                                              NULL,
    [32]                 DATE                                                                             NULL,
    [33]                 INT                                                                              NULL,
    [34]                 INT                                                                              NULL,
    [35]                 DATE                                                                             NULL,
    [36]                 INT                                                                              NULL,
    [37]                 INT                                                                              NULL,
    [38]                 INT                                                                              NULL,
    [39]                 DATE                                                                             NULL,
    [40]                 INT                                                                              NULL,
    [41]                 INT                                                                              NULL,
    [42]                 INT                                                                              NULL,
    [43]                 DATE                                                                             NULL,
    [44]                 CHAR (4)                                                                         NULL,
    [125]                INT                                                                              NULL,
    [AdicionalCAC32]     INT                                                                              NULL,
    [AdicionalCAC33]     INT                                                                              NULL,
    [AdicionalCAC34]     INT                                                                              NULL,
    [AdicionalCAC18]     INT                                                                              NULL,
    [AdicionalODO2]      INT                                                                              NULL,
    [AdicionalCAC11]     VARCHAR (5)                                                                      NULL,
    [AdicionalODO4]      INT                                                                              NULL,
    [AdicionalODO3]      VARCHAR (5)                                                                      NULL,
    [AdicionalODO1]      INT                                                                              NULL,
    [AdicionalCAC1]      INT                                                                              NULL,
    [AdicionalCAC2]      INT                                                                              NULL,
    [AdicionalCAC3]      INT                                                                              NULL,
    [AdicionalCAC4]      INT                                                                              NULL,
    [AdicionalCAC8]      INT                                                                              NULL,
    [AdicionalCAC9]      VARCHAR (6)                                                                      NULL,
    [AdicionalCAC10]     INT                                                                              NULL,
    [AdicionalCAC35]     INT                                                                              NULL,
    [AdicionalCAC17]     INT                                                                              NULL,
    [AdicionalODO7]      VARCHAR (5)                                                                      NULL,
    [AdicionalCAC20]     INT                                                                              NULL,
    [AdicionalCAC26]     INT                                                                              NULL,
    [AdicionalCAC29]     INT                                                                              NULL,
    [AdicionalCAC30]     INT                                                                              NULL,
    [AdicionalCAC31]     INT                                                                              NULL,
    [19]                 DATE                                                                             NULL,
    [20]                 DATE                                                                             NULL,
    [126]                INT                                                                              NULL,
    [120]                INT                                                                              NULL,
    [121]                DATE                                                                             NULL,
    [122]                VARCHAR (12)                                                                     NULL,
    [123]                INT                                                                              NULL,
    [CONFIRMARESTADIO36] BIT                                                                              NULL,
    [CONFIRMARESTADIO38] BIT                                                                              NULL,
    [AdicionalODO5]      INT                                                                              NULL,
    [AdicionalODO6]      INT                                                                              NULL,
    [AdicionalODO8]      INT                                                                              NULL,
    [AdicionalODO9]      INT                                                                              NULL,
    [AdicionalODO10]     INT                                                                              NULL,
    [AdicionalODO11]     INT                                                                              NULL,
    [AdicionalCAC27]     INT                                                                              NULL,
    [AdicionalCAC28]     INT                                                                              NULL,
    CONSTRAINT [PK_HCGRUPOCANCERC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCGRUPOCANCERPACIC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCGRUPOCANCERPACIC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCGRUPOCANCERPACIC].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCGRUPOCANCERPACIC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCGRUPOCANCERPACIC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_HCGRUPOCANCERPACIC_IPCODPACI]
    ON [dbo].[HCGRUPOCANCERPACIC]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pacientes con cáncer de pulmón en estadio tempranos (estadios I y II (IA, IB, IIA y IIB)) llevados a cirugía con intención curativa en quienes se realizó el estudio de riesgo cardiovascular medido por índice de Framignham o ecocardiograma.   0 =No es cáncer de pulmón  1 =Si se realizó  2 =No está descrito en la historia clínica  3 =Es cáncer de pulmón en estadio avanzado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pacientes con cáncer de pulmón en estadio tempranos (estadios I y II (IA, IB, IIA y IIB)) llevados a cirugía con intención curativa en quienes se realizó el estudio de función pulmonar (espirometría)   0 =No es cáncer de pulmón    1 =Si se realizó    2 =No está descrito en la historia clínica   3 =Es cáncer de pulmón en estadio avanzado    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación anatómica del cáncer colorrectal.    0 =No aplica    1 =Derecho (trayecto desde el ciego hasta el ángulo esplénico)  2 =Izquierdo (trayecto desde el ángulo esplénico al recto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalODO11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estudio de Microsatélites.   0 =No aplica   1 =Estable   2 = Inestable    3 =Sin dato o No se realizó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalODO10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gen BRAF.   0 = No aplica  1 =Mutado   2 = No mutado   3 = Sin dato o No se realizó ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalODO9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gen RAS.   0 = No aplica  1 =Mutado   2 = No mutado  3 = Sin dato o No se realizó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalODO8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ki67 (Factor de proliferación celular).  0 =No aplica   1 =Si   2 =No  3 = Sin dato o No se realizó  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalODO6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Terapia Dirigida Recibida.   0= No aplica  1 = Pembrolizumab  2 =Nivolumab   3 =Ipilimumab   4 =Atezolizumab   5 =Gefitinib   6 =Erlotinib   7 =Bevacizumab   8 =Cetuximab   9=Panitumumab  10 =Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalODO5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TRUE -> Si se confirmo el estadio por parte del medico  FALSE  -> NO se confirmo el estadio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'CONFIRMARESTADIO38';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TRUE -> Si se confirmo el estadio por parte del medico  FALSE  -> NO se confirmo el estadio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'CONFIRMARESTADIO36';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿El usuario recibió soporte nutricional? -  Registre:  1=Si, enteral  2=Si, parenteral  3=Si, 1y2  4=No  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'123';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS donde recibió la valoración por nutrición, en este corte -  Registre el código de Habilitación de IPS  98=No Aplica  99= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'122';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de consulta inicial con nutrición en este corte -  Registre la Fecha en que se inició esta atención en el formato AAAA-MM-DD. Si conoce sólo el año y el mes, registre el día 15. Si conoce solamente el año registre el año e incluya 06 como mes y 30 como día (tenga en cuenta las fechas relacionadas, al apli', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'121';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Fue valorado el usuario por profesional en nutrición durante este corte? -  Registre:  1=sí  2=no  98=No aplica, no se ha ordenado valoración por nutrición  99=desconocido, no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'120';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado final del manejo oncológico en este periodo de reporte, luego de ser tratado en este periodo.   1 = Pseudoprogresión (aplica solo para inmunoterapia)   2 =Progresión o recaída   3 =Respuesta parcial   4 =Respuesta completa   5 = Enfermedad estable  6 =Abandono del tratamiento o alta voluntaria  7 =Paciente en seguimiento por antecedente de cáncer   8 =Pendiente iniciar el tratamiento luego del diagnóstico (pudo haber sido definido por especialista, o estar pendiente por valoración oncológica inicial, que defina el tratamiento)  97 =No Aplicable en este periodo, aún bajo tratamiento inicial  98 =No Aplicable en este periodo, aún bajo tratamiento de recaída  99 =No Aplica, el paciente se encuentra fallecido o se encuentra desafiliado  55 =Persona con aseguramiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'126';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso a la institución que realizó el diagnóstico luego de la remisión o interconsulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Fecha de la nota de remisión del médico o institución general hacia la institución que hizo el diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Fueron realizadas pruebas moleculares (FISH o PCR) en paciente con Leucemia Aguda para la clasificación de riesgo?  0 = No es Leucemia aguda  1 =Si  2 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Fue realizada la citogenética convencional en paciente con Leucemia Aguda?    0 =No es Leucemia aguda   1 =Si   2 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC30';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Fue realizada citometría de flujo en paciente con Leucemia Aguda?    0 = No es Leucemia aguda  1 = Si   2 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC29';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' EGFR mutado (cáncer de pulmón de tipo célula no pequeña que incluye adenocarcinoma, escamocelular y otros tipos no mencionados)  0 =No es cáncer de pulmón   1 =Si    2 =No  3 =Es cáncer de pulmón de célula pequeña', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC26';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se realizó abordaje por grupo multidisciplinario para definición del tratamiento inicial.   0 =No aplica   1 =Sí, todo el grupo de profesionales    2 =No soportado el abordaje multidisciplinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Ki67 (Factor de proliferación celular).   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalODO7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'El paciente presenta en el periodo actual metástasis del cáncer primario reportado en este registro.  1  = Si   2 = No ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado CD-20   0 =No es linfoma no Hodking   1 =Positivo   2 =  Negativo  3 =No cuenta con inmunohistoquímica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC35';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se realizó PSA 3 a 12 meses posterior al tratamiento con intensión curativa.    0 =No es cáncer de próstata    1 =Si    2 =No   3 = No hay evidencia en historia clínica ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cual fue el valor de la PSA al momento del diagnóstico.   98 =No es cáncer de próstata   99 =No lo realizaron en paciente con cáncer de próstata  100 = Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se realizó PSA en el momento del diagnóstico.  0 =No es cáncer de próstata   1 =Si    2 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado prueba FISH ó CISH.   0 =No es cáncer de mama   1 =Positivo  2 = Negativo    3 =Sin dato o No se realizó    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se realizó prueba FISH ó CISH.   0 = No es cáncer de mama   1 =Si    2 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de la prueba hormonal (estrógeno y progesterona).   0 = No es cáncer de mama  1 =Positivo+++, >1%, 1 razón, para progesterona y estrógeno   2 =Positivo+++, >1%, 1 razón progesterona pero (-) para estrógeno  3 =sin dato o no se realizó   4 = Positivo+++, >1%, 1 razón estrógeno pero (-) para progesterona   5 =Negativo para progesterona y estrógeno   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se realizó prueba de receptores de progesterona y/o estrógeno (cáncer de mama)   0 =No es cáncer de mama   1 = Si progesterona y estrógeno   2 =Si progesterona, pero no para estrógeno  3 =Si estrógeno, pero no para progesterona   4 =No se realizó progesterona ni estrógeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Inmunorreactividad para ALK (Cinasa linfoma anaplásico).   0= No aplica  1 =Positivo   2 =Negativo   3 =Sin dato o No se realizó   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalODO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TPS (Antígeno polipeptídico tisular específico - %). ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalODO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' ROS-1 (Proto-oncogen tirosina-protein quinasa).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalODO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cual fue el valor de la PSA posterior a los 3 a 12 meses de tratamiento con intensión curativa.   98 = No es cáncer de próstata   99 = No lo realizaron en paciente con cáncer de próstata  3 =No hay evidencia en historia clínica   100 =Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmunorreactividad PD-L1 (Programmed Death-ligand 1).   0 =<1%,    1 = 1-49%   2 = >= 50%    3 = Sin dato o No se realizó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalODO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Indicar los órganos en los que se presenta la metástasis.  0 = No registra metástasis   1 =Ganglios linfáticos regionales   2 =Hígado  3 =Pulmón   4 =Peritoneo  5 =Ganglios linfáticos distantes   6 =Hueso   7 =Suprarrenales   8 = Pleura   9=Cerebro   10 =Otro   11= 2 o más órganos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'El reporte de biopsia describe las coloraciones básicas* y se cuenta con el reporte de inmunohistoquímica     0 =No es linfoma Hodgkin ni no Hodgkin   1 =Si   2 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC34';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Cuál fue la técnica utilizada para el diagnóstico de linfoma?     0 =No es linfoma Hodgkin ni no Hodgkin    1 =Biopsia escisional   2 =Punción por aguja gruesa guiada por imagen  3 = El soporte de historia clínica no menciona la técnica utilizada para tomar la biopsia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC33';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'La patología cumple con los criterios de calidad (Leucemia aguda)  0 = No es Leucemia aguda  1 = Si    2 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'AdicionalCAC32';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tratamiento que está recibiendo el usuario a la fecha de corte.  1 = Radioterapia  2 = Terapia sistémica  3 =Cirugía   4 =1 Y 2   5 =1 Y 3       6 =2 Y 3    7 =Manejo expectante pretratamiento    8 =En seguimiento, luego de tratamiento durante el periodo  9 = Antecedente de cáncer  
 10 = 1, 2 y 3  11 =Manejo de cuidado paliativo o terapia complementaria   98 =No aplica    55 = Persona con aseguramiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'125';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo (CIE 10) de ese cáncer antecedente o concurrente.   55 =Persona con aseguramiento   99 =No aplica    100 =Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'44';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico del otro cáncer primario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'43';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene antecedente o padece de otro cáncer primario (es decir, tiene o tuvo otro tumor maligno diferente al que está notificando).  1 = Sí     2 =No    55 =Persona con aseguramiento   99 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'42';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intervención médica durante el periodo del reporte.  1 = Observación previa a tratamiento   2 = Ofrecer tratamiento curativo o paliativo dirigido al cáncer inicial o por recaída.  3 = Observación o seguimiento oncológico luego de tratamiento inicial  4 =1 y 2 únicamente   5 =2 y 3 únicamente     6 =1, 2 y 3    55 = Persona con aseguramiento    
99 = No hay intervención en el periodo (abandono de terapia, alta oncológica o alta voluntaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'41';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Objetivo (o intención) del tratamiento médico inicial (al diagnóstico).    1 = Curación   2 =Paliación (intención paliativa)   55 =Persona con aseguramiento   99 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'40';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de clasificación de riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'39';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de riesgo leucemias o linfomas (para toda la población).   1 = Bajo Riesgo / Riesgo estándar, bajo / Riesgo estándar, favorable  
2 = Riesgo intermedio bajo  3 =Intermedio / Riesgo intermedio   4 =Riesgo intermedio alto   5 =Riesgo alto / Riesgo alto, desfavorable 
55= Persona con aseguramiento  98= No Aplica (No es leucemia ni linfoma)  99 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'38';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Para cáncer de próstata, valor de clasificación de la escala Gleason en el momento del diagnóstico.  11 = Gleason ≤ 6:≤ 3 + 3    12 = Gleason 7: 3+4  13 =Gleason 7: 4+3  14 =Gleason 8: 4+4 o 3+5 o 5+3   15 =Gleason 9 o 10: 4+5 o 5+4 o 5+5    97 =Es cáncer de próstata, pero no hay información acerca de esta estadificación  98 =No es cáncer de próstata   99 =Es cáncer de próstata, pero no hay información en la historia clínica  55 =Persona con aseguramiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'37';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadificación clínica  en linfoma no Hodgkin pediátrico (Murphy), linfoma no Hodgkin adulto y linfoma Hodgkin adulto/pediátrico (Ann Arbor o Lugano).   1 =Estadio I   2 = Estadio II    3 =Estadio III   4 = Estadio IV   5 =Estadio IA   6=Estadio IB   7=Estadio IIA  8 =Estadio IIB   9 =Estadio IIIA   10 =Estadio IIIB  11 =Estadio IVA   12 =Estadio IVB  13 =Extranodal cualquier estadio  14 =Primario SNC   15 =Primario Mediastinal   16 =Primario de otros órganos   55 = Persona con aseguramiento  98 =No Aplica (tumor diferente a los enunciados) 
  99 = Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'36';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se realizó la estadificación de Dukes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'35';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Para cáncer colorrectal, estadificación de Dukes.   1 =A   2 =B  3 =C  4 =D   55 =Persona con aseguramiento   98 =No Aplica (no es cáncer colorrectal)   99 = Es cáncer colorrectal pero no hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'34';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Para cáncer de mama, resultado de la primera o única prueba HER2.   1 =- +++ (positiva)    2 =++ (equivoco o indeterminado)   3 =+ (negativo)  4 =Cero ó (negativo)    97 = No aplica (cáncer de mama in situ)   98 =No Aplica (no es cáncer de mama) o marcó la variable 31 con la opción 2   99 = Desconocido  55 =Persona con aseguramiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'33';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Para cáncer de mama, fecha de realización de la primera o última prueba HER2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'32';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Para cáncer de mama, ¿se le realizó a este usuario la prueba HER2 (llamado también receptor 2 del factor de crecimiento epidérmico humano, también llamado erb-B2) antes del inicio del tratamiento?
1 = Sí se le realizó  2 =No se le realizó   55= Persona con aseguramiento  97 =No aplica por que es cáncer de mama in situ  98 =No Aplica (no es cáncer de mama)   99 =Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se realizó esta estadificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'30';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'29. Si es tumor sólido, cuál fue la primera estadificación basada en TNM, FIGO, u otras compatibles con esta numeración según tumor.   0 =estadio clínico (ec) 0 (tumor in situ)   1 = ec I o 1    2 =ec IA o 1A     3 = ec IA1    4 =ec IA2   5 =ec IB o 1b   6 =ec IB1    7 = ec IB2   8 =ec IC o 1c   9 =ec IS o 1s  10 =ec II o 2   11 =ec IIA o 2a  12 =ec IIA1    13 =ec IIA2     14 = ec IIB o 2b  15 =ec IIC o 2c    16 =ec III o 3  17 =ec IIIA o 3a   18 =ec IIIB o 3b   19 =ec IIIC o 3c 20 =ec IV o 4   21 = ec IVA o 4a   22 =ec IVB o 4b   23 =ec IVC o 4c   24 =ec 4S  25 = ec V o 5   26 = Estadio IAB   55=Persona con aseguramiento  98=No Aplica  99 = Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'29';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado de diferenciación del tumor sólido maligno según la biopsia o informe de primera cirugía.   1 = Bien diferenciado (grado 1)    2 =Moderadamente diferenciado (grado 2)    3 = Mal diferenciado (grado 3)    4 =Anaplásico o indiferenciado (grado 4)    55=  Persona con aseguramiento   94 =Es un cáncer sólido cuyo reporte de patología no incluye la descripción de la diferenciación celular    95=No es sólido (cáncer hematolinfático)    98 = No se realizó estudio histopatológico (en la variable 21 registro la opción 7)    99 =No hay información en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Histología del tumor en muestra de biopsia o quirúrgica.  1 = Adenocarcinoma, con o sin otra especificación  2 = Carcinoma escamocelular (epidermoide), con o sin otra especificación  3 = Carcinoma de células basales (basocelular)  4 = Carcinoma, con o sin otra especificación diferentes a las anteriores  5 = Oligodendroglioma, con o sin otra especificación 6 = Astrocitoma, con o sin otra especificación 7 = Ependimoma, con o sin otra especificación  8 = Neuroblastoma, con o sin otra especificación  9 = Meduloblastoma, con o sin otra especificación  10 = Hepatoblastoma, con o sin otra especificación  11 =Rabdomiosarcoma, con o sin otra especificación   12 = Leiomiosarcoma, con o sin otra especificación  13 = Osteosarcoma, con o sin otra especificación  14 = Fibrosarcoma, con o sin otra especificación  15 = Angiosarcoma, con o sin otra especificación 16 =Condrosarcoma, con o sin otra especificación   17 =Otros sarcomas, con o sin otra especificación  18 =18  Pancreatoblastoma, con o sin otra especificación  19 = Blastoma pleuropulmonar, con o sin otra especificación   20 =Otros tipos histológicos no mencionados    23 =  Melanoma   24 =Carcinoma papilar de tiroides  55 =Persona con aseguramiento    99=No se realizó estudio histopatológico   99= Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de primera consulta con médico tratante de la enfermedad maligna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'26';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Fecha de primero o único informe histopatológico válido de diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'24';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Fecha de recolección de muestra para estudio histopatológico de diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Motivo por el cual el usuario no tuvo diagnóstico por histopatología.  1 = Clínica, usuario con coagulopatía  2 =Clínica, debido a localización del tumor    3 = Clínica, debido al estado funcional del usuario (deterioro)  4 = Negativa del usuario o su acudiente para realizar el estudio histopatológico, con documentación de soporte  5 =Administrativa  6 = Clínica por reporte de imágenes o laboratorios  55=  Persona con aseguramiento   98 = Tiene confirmación por histopatología  99 =  Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de estudio con el que se realizó el diagnóstico de cáncer.  5= Inmunohistoquímica   6 = Citometría de flujo   7 = Clínica exclusivamente    8 =Otro    9 =Genética   10 = Patología básica  99 =Desconocido   55 =Persona con aseguramiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Fecha de diagnóstico del cáncer reportado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Primario  2 - Otro Primario  3 - Primario Desconocido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional donde se realiza el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro atención donde se hizo el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo profesional salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'identificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Pacientes con Diagnosticos Nuevos  2 - pacientes con datos modificados  3 - Pacientes con diagnosticos visados  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro clínico del grupo de cáncer por paciente. Guarda el seguimiento oncológico de cada paciente diagnosticado con cáncer, incluyendo diagnóstico, estadificación, datos del ingreso, profesional tratante, fechas de seguimiento y variables adicionales requeridas por la ruta integral de atención en cáncer (RIAS/CAC) y odontología oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPOCANCERPACIC';
