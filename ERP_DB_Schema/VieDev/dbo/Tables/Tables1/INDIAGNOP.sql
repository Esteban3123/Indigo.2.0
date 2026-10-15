CREATE TABLE [dbo].[INDIAGNOP] (
    [IDETIPHIS]          CHAR (9)                                                                         NULL,
    [NUMEFOLIO]          NCHAR (10)                                                                       NULL,
    [CODCENATE]          CHAR (10)                                                                        NULL,
    [UFUCODIGO]          CHAR (10)                                                                        NULL,
    [NUMINGRES]          CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODDIAGNO]          CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [CODPROSAL]          CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODDIAPRI]          BIT                                                                              NOT NULL,
    [DIAINGEGR]          CHAR (1)                                                                         NOT NULL,
    [TIPDIAGNO]          CHAR (1)                                                                         NOT NULL,
    [CLADIAGNO]          CHAR (2)                                                                         NOT NULL,
    [OBSDIAGNO]          CHAR (250)                                                                       NOT NULL,
    [FECDIAGNO]          DATETIME                                                                         NOT NULL,
    [FOLDIAGNO]          INT                                                                              NULL,
    [DIAESTADO]          INT                                                                              NULL,
    [INDAUDFOR]          NUMERIC (18)                                                                     NOT NULL,
    [PLANTDIAG]          TEXT                                                                             NULL,
    [TRATA4505]          INT                                                                              NULL,
    [FECHLEISH]          DATETIME                                                                         NULL,
    [T1]                 CHAR (2)                                                                         NULL,
    [T2]                 CHAR (2)                                                                         NULL,
    [N1]                 CHAR (2)                                                                         NULL,
    [N2]                 CHAR (2)                                                                         NULL,
    [M1]                 CHAR (2)                                                                         NULL,
    [M2]                 CHAR (2)                                                                         NULL,
    [ESTADIO]            INT                                                                              NULL,
    [IDADENFHUERFANAS]   INT                                                                              NULL,
    [TIPOCANCER]         INT                                                                              NULL,
    [ESTADIO2]           CHAR (10)                                                                        NULL,
    [CONFIRMATNMESTADIO] BIT                                                                              NULL,
    CONSTRAINT [PK_INDIAGNOP] PRIMARY KEY CLUSTERED ([NUMINGRES] ASC, [IPCODPACI] ASC, [CODDIAGNO] ASC, [CODDIAPRI] ASC),
    CONSTRAINT [FK_INDIAGNOP_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_INDIAGNOP_ADENFHUERFANAS] FOREIGN KEY ([IDADENFHUERFANAS]) REFERENCES [dbo].[ADENFHUERFANAS] ([ID]),
    CONSTRAINT [FK_INDIAGNOP_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INDIAGNOP_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_INDIAGNOP_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_INDIAGNOP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_INDIAGNOP_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[INDIAGNOP] NOCHECK CONSTRAINT [FK_INDIAGNOP_ADcenaten];


GO
ALTER TABLE [dbo].[INDIAGNOP] NOCHECK CONSTRAINT [FK_INDIAGNOP_ADENFHUERFANAS];


GO
ALTER TABLE [dbo].[INDIAGNOP] NOCHECK CONSTRAINT [FK_INDIAGNOP_ADINGRESO];


GO
ALTER TABLE [dbo].[INDIAGNOP] NOCHECK CONSTRAINT [FK_INDIAGNOP_INDIAGNOS];


GO
ALTER TABLE [dbo].[INDIAGNOP] NOCHECK CONSTRAINT [FK_INDIAGNOP_INPacient];


GO
ALTER TABLE [dbo].[INDIAGNOP] NOCHECK CONSTRAINT [FK_INDIAGNOP_INPROFSAL];


GO
ALTER TABLE [dbo].[INDIAGNOP] NOCHECK CONSTRAINT [FK_INDIAGNOP_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo para almancear el Estadio 2 solo cuando el diagnostico sea de tipo Cáncer, valores 0-A-B-C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'ESTADIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Cáncer.  1 - Primario   2 - Otro Primario   3 - Primario desconocido   4 - Metástasis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0=estadio clínico (ec) 0 (tumor in situ)  1=ec I o 1  2=ec IA o 1A  3=ec IA1  4=ec IA2  5=ec IB o 1b  6=ec IB1  7=ec IB2  8=ec IC o 1c  9=ec IS o 1s  10=ec II o 2  11=ec IIA o 2a  12=ec IIA1  13=ec IIA2  14=ec IIB o 2b  15=ec IIC o 2c  16=ec III o 3  17=ec IIIA o 3a  18= ec IIIB o 3b  19= ec IIIC o 3c  20=ec IV o 4  21= ec IVA o 4a  22=ec IVB o 4b  23=ec IVC o 4c  24= ec 4S (para neuroblastoma)  25= ec V o 5  26=Estadio IAB  55=Persona con aseguramiento (régimen subsidiado o contributivo y que no son PPNA) que recibió servicios de salud por parte del ente territorial durante el periodo de reporte  93= Sin información de estadificación en historia clínica  98=No Aplica (Es cáncer de piel basocelular, es cáncer hematológico o es cáncer en SNC, excepto neuroblastoma)  99=Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos"  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'ESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''M'' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'M2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''M'' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'M1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''N'' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'N2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''N'' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'N1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''T'' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'T2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''T'' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'T1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si el paciente con diagnostico de: Ansiedad, Depresión, Esquizofrenia, Deficit de atención, consumo SPA y Bipolaridad  1- El paciente está en procesio de atención por equipo interdisciplinario en el hospital ..  2- El paciente recibió atención por equipo interdisciplinario completo en el hospital...  16- El paciente no recibió atención por tener una tradición que se lo impide  17- No recibió atención por una condición de salud  18- No recibió atención por negación del usuario  20- No recibió atención por otras razones  22- Sin dato    Si el diagnostico Hipotiroidismo congenito, sifilis gestacional, sifilis congenita, lepra  1- El paciente recibe tratamiento en hospital...pero aún no ha terminado  2- El paciente recibió tratamiento en el hospital... y ya lo terminó  16- No recibió tratamiento por tener una tradición que se lo impide  17- No recibió tratamiento por una condición de salud que se lo impide  18- No recibió tratamiento por negación del usuario  20- No recibió tratamiento por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TRATA4505';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado   1: Activo  2: Descartado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'DIAESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Diagnostico  PR: Pre-operatorio  PO: Pos-operatorio  PP: Pre y Pos-Operatorio  HI: Hispatologico  NA: No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Diagnostico  I: Impresion Diagnostica  C: Confirmado Nuevo  R: Confirmado Repetido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el Diagnostico es de Ingreso o Egreso  I: Ingreso  E: Egreso  A: Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';

