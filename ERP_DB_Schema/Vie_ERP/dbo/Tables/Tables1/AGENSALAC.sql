CREATE TABLE [dbo].[AGENSALAC] (
    [CODCONCEC]     INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]     CHAR (10)                                                                     NOT NULL,
    [TIPOSALA]      CHAR (1)                                                                      NOT NULL,
    [CODIGSALA]     CHAR (6)                                                                      NOT NULL,
    [DESCRIPSAL]    VARCHAR (100)                                                                 NOT NULL,
    [ESTADO]        BIT                                                                           NULL,
    [DISPHORINI]    DATETIME                                                                      NULL,
    [DISPHORFIN]    DATETIME                                                                      NULL,
    [CONTPROCED]    BIT                                                                           NOT NULL,
    [TIPOTRATA]     TINYINT                                                                       NULL,
    [LUN]           BIT                                                                           NOT NULL,
    [MAR]           BIT                                                                           NOT NULL,
    [MIE]           BIT                                                                           NOT NULL,
    [JUE]           BIT                                                                           NOT NULL,
    [VIE]           BIT                                                                           NOT NULL,
    [DOM]           BIT                                                                           NOT NULL,
    [SAB]           BIT                                                                           NOT NULL,
    [FESTIVOS]      BIT                                                                           NOT NULL,
    [TIPOSERVIC]    TINYINT                                                                       NULL,
    [UFUCODIGO]     CHAR (10)                                                                     NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [CODESPECI]     CHAR (3)                                                                      NULL,
    [DURACIONRADIO] INT                                                                           NULL,
    [MOSTRARWEB]    BIT                                                                           NULL,
    [AETITLE]       VARCHAR (20)                                                                  NULL,
    [IPPORT]        VARCHAR (30)                                                                  NULL,
    [IDRISGRIMAGE]  INT                                                                           NULL,
    CONSTRAINT [PK_AGENSALAS_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_AGENSALAC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGENSALAC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_AGENSALAC_RISGRIMAGE] FOREIGN KEY ([IDRISGRIMAGE]) REFERENCES [dbo].[RISGRIMAGE] ([ID])
);


GO
ALTER TABLE [dbo].[AGENSALAC] NOCHECK CONSTRAINT [FK_AGENSALAC_ADCENATEN];


GO
ALTER TABLE [dbo].[AGENSALAC] NOCHECK CONSTRAINT [FK_AGENSALAC_INUNIFUNC];


GO
ALTER TABLE [dbo].[AGENSALAC] NOCHECK CONSTRAINT [FK_AGENSALAC_RISGRIMAGE];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_AGENSALAC]
    ON [dbo].[AGENSALAC]([CODIGSALA] ASC, [CODCENATE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la modalidad.
- Si se chequea la opción "Sala de gestión intrahospitalaria" en el formulario "Salas", el campo "Modalidad" pasa a ser obligatorio.
- Si no se chequea la opción mencionada anteriormente, se bloquea el campo "Modalidad", por lo tanto, se guarda NULL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'IDRISGRIMAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ip y puerto donde son visibles las modalidades en la red, el formato a emplear es {ip}:{port}', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'IPPORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de una modalidad en la red (nombre unico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'AETITLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración de la Radio En (Minutos)    04-12-2019 -> Campo queda obsoleto desde la fecha Indicada, esta decision es por temas de procesos que se estan cambiando en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DURACIONRADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Especialidad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'determina Tipo de Servicio para salas de tipo de ayudas Diagnosticas : 1-> Laboratorio 2-> Imagenes Diagnosticas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOSERVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dias festivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'FESTIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Sabado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'SAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Domingo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Viernes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'VIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Jueves', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'JUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Miercoles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Lunes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad para dia Lunes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'LUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'determina Tipo de Tratamiento para salas de tipo Servicios Especiales :  1 -> Quimio 2-> Radio 3-> Dialisis ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control de procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CONTPROCED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad hora final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DISPHORFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dospinibilidad hora inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DISPHORINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la sala 1-Activo 0-Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Sala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DESCRIPSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de salas de cirugia , apoyo diagnostico y urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODIGSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de la Sala   Q-Quirurgica   D-Apoyo Diagnostico   U-Urgencias    E - Tratamientos Especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

