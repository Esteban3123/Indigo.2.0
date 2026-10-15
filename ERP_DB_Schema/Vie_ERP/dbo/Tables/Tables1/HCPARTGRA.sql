CREATE TABLE [dbo].[HCPARTGRA] (
    [IDPARTGRA]     INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECPARTOG]     INT                                                                              NOT NULL,
    [FECREGIST]     DATETIME                                                                         NULL,
    [TENSSARTE]     VARCHAR (10)                                                                     NULL,
    [FRECUCARD]     INT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [SMFRERESP]     INT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [TEMPERATU]     FLOAT (53) MASKED WITH (FUNCTION = 'default()')                                  NULL,
    [AUFRECUEN]     VARCHAR (10) MASKED WITH (FUNCTION = 'partial(0, "MaternalSigns_Ofuscado", 0)')  NULL,
    [AUINTESID]     INT                                                                              NULL,
    [AUDURACIO]     INT                                                                              NULL,
    [BFFETOCAR]     INT                                                                              NULL,
    [BFDESACEL]     INT                                                                              NULL,
    [BFMOVFETA]     INT                                                                              NULL,
    [TVDILATAC]     INT                                                                              NULL,
    [TVBORRAMI]     NUMERIC (18)                                                                     NULL,
    [TVESTACIO]     INT                                                                              NULL,
    [TVMEMBRAN]     INT                                                                              NULL,
    [TVLIQANMI]     INT                                                                              NULL,
    [TVVARPOSI]     INT                                                                              NULL,
    [OBSERVACI]     VARCHAR (500)                                                                    NULL,
    [FECINIREG]     DATETIME                                                                         NULL,
    [PARPELVIS]     CHAR (1)                                                                         NULL,
    [ESTREGPAR]     BIT                                                                              NULL,
    [PARIDAD]       TINYINT                                                                          NULL,
    [POSMATERNA]    TINYINT                                                                          NULL,
    [IDENTIFICADOR] BIT                                                                              NULL,
    CONSTRAINT [PK_HCPARTGRA] PRIMARY KEY CLUSTERED ([IDPARTGRA] ASC),
    CONSTRAINT [FK_HCPARTGRA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARTGRA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPARTGRA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPARTGRA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPARTGRA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPARTGRA] NOCHECK CONSTRAINT [FK_HCPARTGRA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARTGRA] NOCHECK CONSTRAINT [FK_HCPARTGRA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPARTGRA] NOCHECK CONSTRAINT [FK_HCPARTGRA_INPACIENT];


GO
ALTER TABLE [dbo].[HCPARTGRA] NOCHECK CONSTRAINT [FK_HCPARTGRA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCPARTGRA] NOCHECK CONSTRAINT [FK_HCPARTGRA_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCPARTGRA__IPCODPACI]
    ON [dbo].[HCPARTGRA]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En esta tabla se crean por paciente siempre 15 registros, este campo es para identificar que registros se diligenciaron de todos esos 10 registros.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IDENTIFICADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Posición De la Materna  1: Horizontal  2: Vertical  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'POSMATERNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paridad de la materna  1: Nulípara  2: Multípara', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'PARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro de partograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'ESTREGPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pelvis  1:Adecuada  2:Dudosa  3:No Adecuada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'PARPELVIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que se inicia el servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECINIREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones por Hora o Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tacto Vaginal/Variedad de Posicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVVARPOSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tacto Vaginal/Liquido Amniotico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVLIQANMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tacto Vaginal/Membranas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVMEMBRAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tacto Vaginal/Estacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVESTACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tacto Vaginal/Borramiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVBORRAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tacto Vaginal/Dilatacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVDILATAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bienestar Fetal/Movimiento Fetal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFMOVFETA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bienestar Fetal/Desaceleracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFDESACEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bienestar Fetal/Fetocardia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFFETOCAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actividad Uterina/Duracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUDURACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actividad Uterina/Intensidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUINTESID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actividad Uterina/Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUFRECUEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Signos Maternos/Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TEMPERATU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Signos Maternos/Frecuencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'SMFRERESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Signos Maternos/Frecuencia Cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FRECUCARD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Signos Maternos/Tesion Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TENSSARTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Hora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECPARTOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IDPARTGRA';

