CREATE TABLE [dbo].[INDIAGNOH] (
    [IDETIPHIS]          CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]          NCHAR (10)                                                                       NOT NULL,
    [CODCENATE]          CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]          CHAR (10)                                                                        NOT NULL,
    [NUMINGRES]          CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODDIAGNO]          CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [CODPROSAL]          CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODDIAPRI]          BIT MASKED WITH (FUNCTION = 'default()')                                         NOT NULL,
    [DIAINGEGR]          CHAR (1)                                                                         NOT NULL,
    [TIPDIAGNO]          CHAR (1)                                                                         NOT NULL,
    [CLADIAGNO]          CHAR (2)                                                                         NOT NULL,
    [OBSDIAGNO]          CHAR (250)                                                                       NOT NULL,
    [FECDIAGNO]          DATETIME                                                                         NOT NULL,
    [FOLDIAGNO]          INT                                                                              NULL,
    [DIAESTADO]          INT                                                                              NULL,
    [INDAUDFOR]          NUMERIC (18)                                                                     NOT NULL,
    [PLANTDIAG]          VARCHAR (MAX)                                                                    NULL,
    [TRATA4505]          INT                                                                              NULL,
    [FECHLEISH]          DATETIME                                                                         NULL,
    [ESTADIO]            INT                                                                              NULL,
    [T1]                 CHAR (2)                                                                         NULL,
    [T2]                 CHAR (2)                                                                         NULL,
    [N1]                 CHAR (2)                                                                         NULL,
    [N2]                 CHAR (2)                                                                         NULL,
    [M1]                 CHAR (2)                                                                         NULL,
    [M2]                 CHAR (2)                                                                         NULL,
    [IDADENFHUERFANAS]   INT                                                                              NULL,
    [TIPOCANCER]         INT                                                                              NULL,
    [CONFIRMATNMESTADIO] BIT                                                                              NULL,
    CONSTRAINT [PK_INDIAGNOH] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [NUMINGRES] ASC, [IPCODPACI] ASC, [CODDIAGNO] ASC),
    CONSTRAINT [FK_INDIAGNOH_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_INDIAGNOH_ADENFHUERFANAS] FOREIGN KEY ([IDADENFHUERFANAS]) REFERENCES [dbo].[ADENFHUERFANAS] ([ID]),
    CONSTRAINT [FK_INDIAGNOH_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INDIAGNOH_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_INDIAGNOH_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_INDIAGNOH_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_INDIAGNOH_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[INDIAGNOH] NOCHECK CONSTRAINT [FK_INDIAGNOH_ADcenaten];


GO
ALTER TABLE [dbo].[INDIAGNOH] NOCHECK CONSTRAINT [FK_INDIAGNOH_ADENFHUERFANAS];


GO
ALTER TABLE [dbo].[INDIAGNOH] NOCHECK CONSTRAINT [FK_INDIAGNOH_ADINGRESO];


GO
ALTER TABLE [dbo].[INDIAGNOH] NOCHECK CONSTRAINT [FK_INDIAGNOH_INDIAGNOS];


GO
ALTER TABLE [dbo].[INDIAGNOH] NOCHECK CONSTRAINT [FK_INDIAGNOH_INPacient];


GO
ALTER TABLE [dbo].[INDIAGNOH] NOCHECK CONSTRAINT [FK_INDIAGNOH_INPROFSAL];


GO
ALTER TABLE [dbo].[INDIAGNOH] NOCHECK CONSTRAINT [FK_INDIAGNOH_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [PK_INDIAGNOH1]
    ON [dbo].[INDIAGNOH]([NUMINGRES] ASC, [IPCODPACI] ASC)
    INCLUDE([CODDIAGNO], [CODDIAPRI], [NUMEFOLIO], [OBSDIAGNO]);


GO
CREATE NONCLUSTERED INDEX [IX_INDIAGNOH_IPCODPACI_FECDIAGNO]
    ON [dbo].[INDIAGNOH]([IPCODPACI] ASC, [FECDIAGNO] DESC)
    INCLUDE([NUMINGRES], [CODDIAGNO], [CODDIAPRI], [NUMEFOLIO]);


GO
CREATE NONCLUSTERED INDEX [IX_INDIAGNOH__IPCODPACI__CODDIAGNO__INC__ALL2]
    ON [dbo].[INDIAGNOH]([IPCODPACI] ASC, [CODDIAGNO] ASC)
    INCLUDE([IDETIPHIS], [NUMEFOLIO], [CODCENATE], [UFUCODIGO], [NUMINGRES], [CODPROSAL], [CODDIAPRI], [DIAINGEGR], [TIPDIAGNO], [CLADIAGNO], [OBSDIAGNO], [FECDIAGNO], [FOLDIAGNO], [DIAESTADO], [INDAUDFOR], [PLANTDIAG], [TRATA4505], [FECHLEISH], [T2], [N1], [N2], [T1], [IDADENFHUERFANAS], [TIPOCANCER], [M1], [M2], [ESTADIO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Confirmación de campos T, N, M y Estadio --> 0=No, 1=Si ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CONFIRMATNMESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Cáncer.  1 - Primario   2 - Otro Primario   3 - Primario desconocido   4 - Metástasis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion de la enfermedad huerfana asociada al Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''M'' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'M1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Para Almacenar el Datos ''N'' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'N2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Para Almacenar el Datos ''N'' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'N1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Para Almacenar el Datos ''T'' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'T2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Para Almacenar el Datos ''T'' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'T1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'ESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Terminación Tratamiento para Leishmaniasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FECHLEISH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si el paciente con diagnostico de: Ansiedad, Depresión, Esquizofrenia, Deficit de atención, consumo SPA y Bipolaridad  1- El paciente está en procesio de atención por equipo interdisciplinario en el hospital ..  2- El paciente recibió atención por equipo interdisciplinario completo en el hospital...  16- El paciente no recibió atención por tener una tradición que se lo impide  17- No recibió atención por una condición de salud  18- No recibió atención por negación del usuario  20- No recibió atención por otras razones  22- Sin dato    Si el diagnostico Hipotiroidismo congenito, sifilis gestacional, sifilis congenita, lepra  1- El paciente recibe tratamiento en hospital...pero aún no ha terminado  2- El paciente recibió tratamiento en el hospital... y ya lo terminó  16- No recibió tratamiento por tener una tradición que se lo impide  17- No recibió tratamiento por una condición de salud que se lo impide  18- No recibió tratamiento por negación del usuario  20- No recibió tratamiento por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TRATA4505';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantillas de diagnosticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado   1: Activo  2: Descartado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'DIAESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del folio de la historia clinica en donde se especifico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Asignacion del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion general del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Diagnostico  PR: Pre-operatorio  PO: Pos-operatorio  PP: Pre y Pos-Operatorio  HI: Hispatologico  NA: No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Diagnostico  I: Impresion Diagnostica  C: Confirmado Nuevo  R: Confirmado Repetido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el Diagnostico es de Ingreso o Egreso  I: Ingreso  E: Egreso  A: Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diagnostico Principal - Solo Aplica uno por Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';

