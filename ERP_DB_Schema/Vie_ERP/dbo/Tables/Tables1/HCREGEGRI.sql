CREATE TABLE [dbo].[HCREGEGRI] (
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [FECALTPAC] DATETIME                                                                         NOT NULL,
    [ESTPACEGR] INT                                                                              NOT NULL,
    [FECMUEPAC] DATETIME                                                                         NULL,
    [CODCAUMUE] CHAR (3)                                                                         NULL,
    [NUMCERDEF] CHAR (40) MASKED WITH (FUNCTION = 'partial(0, "DeathCertificate_Ofuscado", 0)')  NULL,
    [DEPMUNCOD] CHAR (5)                                                                         NULL,
    [AIPSREMIS] CHAR (60)                                                                        NULL,
    [REHORASOL] DATETIME                                                                         NULL,
    [REHORCON]  DATETIME                                                                         NULL,
    [REHORLLE]  DATETIME                                                                         NULL,
    [REMINIVEL] INT                                                                              NULL,
    [REESPECIA] CHAR (60)                                                                        NULL,
    [REPERCONF] CHAR (60)                                                                        NULL,
    [RESERVICI] CHAR (60)                                                                        NULL,
    [IOBSERVAC] VARCHAR (MAX)                                                                    NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CTRSALSEG] BIT                                                                              NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [NOMSOLREM] VARCHAR (50)                                                                     NULL,
    [MOTIVEMI]  CHAR (1)                                                                         NULL,
    CONSTRAINT [PK_HCREGEGRI] PRIMARY KEY CLUSTERED ([NUMINGRES] ASC),
    CONSTRAINT [FK_HCREGEGRI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCREGEGRI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREGEGRI_INCAUMUER] FOREIGN KEY ([CODCAUMUE]) REFERENCES [dbo].[INCAUMUER] ([CODCAUMUE]),
    CONSTRAINT [FK_HCREGEGRI_INMunicip] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]),
    CONSTRAINT [FK_HCREGEGRI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCREGEGRI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCREGEGRI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_ADcenaten];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_INCAUMUER];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_INMunicip];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_INPACIENT];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_INPROFSAL];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'MOTIVEMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Quien solicita la remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'NOMSOLREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el egreso ya tiene el control de Salida y Seguimiento  Null -> No tiene el control por lo tanto se muestra  True -> Ya tiene el control no se muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'CTRSALSEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que Crea el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de uso administrativo para especificar observaciones generales del Egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'IOBSERVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio Remitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'RESERVICI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona que Confirma la Remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'REPERCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especialidad Remitida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'REESPECIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de Remitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'REMINIVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de LLegada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'REHORLLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de Confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'REHORCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de Solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'REHORASOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica la IPS a donde Remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'AIPSREMIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica el codigo del Municipio a la IPS donde remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del certificado de defuncion del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'NUMCERDEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Causa de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'CODCAUMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de muerte del paciente, solo si aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'FECMUEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Paciente al Egreso   1: Mejor  2: Igual o Peor  3: Fallecido  4: Remitido  5: Hospitalizacion en Casa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'ESTPACEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Alta del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'FECALTPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';

