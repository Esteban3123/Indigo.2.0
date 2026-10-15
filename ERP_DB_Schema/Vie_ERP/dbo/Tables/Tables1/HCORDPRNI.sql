CREATE TABLE [dbo].[HCORDPRNI] (
    [IDETIPHIS]   CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]   NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NOT NULL,
    [CODCENATE]   CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]   CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]   DATETIME                                                                         NOT NULL,
    [CODSERIPS]   CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]   INT                                                                              NOT NULL,
    [OBSSERIPS]   CHAR (250)                                                                       NULL,
    [PRISERIPS]   CHAR (1)                                                                         NOT NULL,
    [ESTSERIPS]   CHAR (1)                                                                         NULL,
    [MANEXTPRO]   BIT                                                                              NOT NULL,
    [CODDIAGNO]   CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [EXREASITI]   BIT                                                                              NOT NULL,
    [INDAUDFOR]   NUMERIC (18)                                                                     NOT NULL,
    [AUTO]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TIPENFERME]  TINYINT                                                                          NULL,
    [NUMDIALSEM]  INT                                                                              NULL,
    [TIEMPSESION] INT                                                                              NULL,
    [UF]          INT                                                                              NULL,
    CONSTRAINT [PK_HCORDPRNI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDPRNI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDPRNI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDPRNI_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]) ON UPDATE CASCADE,
    CONSTRAINT [FK_HCORDPRNI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDPRNI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDPRNI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCORDPRNI] NOCHECK CONSTRAINT [FK_HCORDPRNI_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDPRNI] NOCHECK CONSTRAINT [FK_HCORDPRNI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDPRNI] NOCHECK CONSTRAINT [FK_HCORDPRNI_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDPRNI] NOCHECK CONSTRAINT [FK_HCORDPRNI_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDPRNI] NOCHECK CONSTRAINT [FK_HCORDPRNI_INPACIENT];


GO
ALTER TABLE [dbo].[HCORDPRNI] NOCHECK CONSTRAINT [FK_HCORDPRNI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso de Dialisis:  UF campo de 4 digitos, no permite decimales  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'UF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso de Dialisis:  Tiempo de la sesion en horas, campo de 2 digitos, no permite decimales  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'TIEMPSESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso de Dialisis:  Numero de dialisis por semana  campo de 2 digitos, no permite decimales  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'NUMDIALSEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso de Dialisis:  Tipo de enfermedad: 1->Crónico, 2-> Agudo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'TIPENFERME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el examen es realizado en sitio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'EXREASITI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Diagnostico Principal Relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Ordenado  2: Completado  3: Interpretado  4: Sin Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRNI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';

