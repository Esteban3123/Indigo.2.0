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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGENSALAC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_AGENSALAC]
    ON [dbo].[AGENSALAC]([CODIGSALA] ASC, [CODCENATE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de modalidad DICOM (FK a RISGRIMAGE). Obligatorio si está marcada ''''Sala gestión intrahospitalaria''''; NULL en caso contrario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'IDRISGRIMAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la modalidad.
- Si se chequea la opción "Sala de gestión intrahospitalaria" en el formulario "Salas", el campo "Modalidad" pasa a ser obligatorio.
- Si no se chequea la opción mencionada anteriormente, se bloquea el campo "Modalidad", por lo tanto, se guarda NULL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'IDRISGRIMAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'IDRISGRIMAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección IP y puerto DICOM de la modalidad en red. Formato: {ip}:{puerto} para acceso a equipos de diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'IPPORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ip y puerto donde son visibles las modalidades en la red, el formato a emplear es {ip}:{port}', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'IPPORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'IPPORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'AE Title o identificador DICOM de la modalidad en red. Nombre único de la sala en comunicación de imagenes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'AETITLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de una modalidad en la red (nombre unico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'AETITLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'AETITLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visibilidad en portal web: 1=Mostrar citas en web, 0=Ocultar. Controla disponibilidad en agendamiento online.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración estándar en minutos para procedimientos radiológicos. [OBSOLETO desde 04-12-2019 por cambios de procesos internos]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DURACIONRADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración de la Radio En (Minutos)    04-12-2019 -> Campo queda obsoleto desde la fecha Indicada, esta decision es por temas de procesos que se estan cambiando en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DURACIONRADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DURACIONRADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Especialidad médica (ej: 001=Cirugía, 002=Radiología). Vincula la sala a profesionales del área.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Especialidad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Profesional de la Salud (PII ofuscado). Médico/especialista responsable o asignado a la sala. [Identification_Ofuscado]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Unidad Funcional (FK a INUNIFUNC). Agrupa salas por departamento/centro de costo administrativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio para ayudas diagnósticas: 1=Laboratorio, 2=Imagenología/Diagnóstico. NULL para quirófanos/urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOSERVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'determina Tipo de Servicio para salas de tipo de ayudas Diagnosticas : 1-> Laboratorio 2-> Imagenes Diagnosticas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOSERVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOSERVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad en días festivos/feriados: 1=Abierta, 0=Cerrada. Atiende puentes y días no laborales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'FESTIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dias festivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'FESTIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'FESTIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad el sábado: 1=Abierta, 0=Cerrada. Bit de operación semanal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'SAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Sabado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'SAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'SAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad el domingo: 1=Abierta, 0=Cerrada. Bit de operación semanal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Domingo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad el viernes: 1=Abierta, 0=Cerrada. Bit de operación semanal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'VIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Viernes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'VIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'VIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad el jueves: 1=Abierta, 0=Cerrada. Bit de operación semanal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'JUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Jueves', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'JUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'JUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad el miércoles: 1=Abierta, 0=Cerrada. Bit de operación semanal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Miercoles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad el martes: 1=Abierta, 0=Cerrada. Bit de operación semanal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Lunes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'MAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad el lunes: 1=Abierta, 0=Cerrada. Bit de operación semanal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'LUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Lunes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'LUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'LUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tratamiento para salas especiales: 1=Quimioterapia, 2=Radioterapia, 3=Diálisis. NULL si no aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'determina Tipo de Tratamiento para salas de tipo Servicios Especiales :  1 -> Quimio 2-> Radio 3-> Dialisis ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de procedimientos: 1=Habilitado, 0=Deshabilitado. Indica si la sala registra y audita procedimientos realizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CONTPROCED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control de procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CONTPROCED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CONTPROCED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora final de disponibilidad de la sala (formato DATETIME). Cierre de jornada o disponibilidad máxima.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DISPHORFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad hora final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DISPHORFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DISPHORFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora inicial de disponibilidad de la sala (formato DATETIME). Rango de apertura diaria para atenciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DISPHORINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dospinibilidad hora inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DISPHORINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DISPHORINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo: 1=Activa/Disponible, 0=Inactiva/No disponible. Indica si la sala está habilitada para atenciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la sala 1-Activo 0-Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre de la sala. Texto libre que identifica la unidad (ej: Quirófano 1, Sala de Resonancia Magnética).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DESCRIPSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Sala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DESCRIPSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'DESCRIPSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único de la sala de cirugía, ayuda diagnóstica o urgencia. Referencia para citas y procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODIGSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de salas de cirugia , apoyo diagnostico y urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODIGSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODIGSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de sala: Q=Quirúrgica, D=Apoyo Diagnóstico (laboratorio/imagenes), U=Urgencias, E=Tratamientos Especiales. Clasificación funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de la Sala   Q-Quirurgica   D-Apoyo Diagnostico   U-Urgencias    E - Tratamientos Especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'TIPOSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (FK a ADCENATEN). Identifica la sede, clínica u hospital donde funciona la sala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo de la sala (PK). Generado automáticamente, clave primaria para auditoria y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
CREATE NONCLUSTERED INDEX [IX_AGENSALAC_CODCONCEC_CODCENATE]
    ON [dbo].[AGENSALAC]([CODCONCEC] ASC, [CODCENATE] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AGENSALAC_Sala]
    ON [dbo].[AGENSALAC]([CODCONCEC] ASC, [CODCENATE] ASC)
    INCLUDE([CODIGSALA], [DESCRIPSAL], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salas de atención y recursos disponibles para agendamiento de citas. Registra las salas o consultorios de cada centro de atención, su horario de disponibilidad, los días hábiles en que operan, el tipo de servicio que prestan y el profesional o especialidad asignada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAC';
