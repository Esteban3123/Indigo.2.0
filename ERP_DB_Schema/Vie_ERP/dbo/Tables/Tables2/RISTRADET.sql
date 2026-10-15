CREATE TABLE [dbo].[RISTRADET] (
    [CODCONSEC] VARCHAR (10)                                                                  NOT NULL,
    [CODCENATE] CHAR (10)                                                                     NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                     NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                      NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                    NOT NULL,
    [NUMINGRES] CHAR (10)                                                                     NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHORORD] DATETIME                                                                      NOT NULL,
    [CODPROTEC] CHAR (20)                                                                     NULL,
    [FECHORINI] DATETIME                                                                      NULL,
    [FECHORFIN] DATETIME                                                                      NULL,
    [CODPRORAD] CHAR (20)                                                                     NULL,
    [FECHORLEC] NCHAR (10)                                                                    NULL,
    [CODUSUARI] CHAR (20)                                                                     NULL,
    [FECHORTRA] DATETIME                                                                      NULL,
    CONSTRAINT [PK_ITRADTRAD] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_RISTRADET_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_RISTRADET_RISTRACAB] FOREIGN KEY ([CODCONSEC]) REFERENCES [dbo].[RISTRACAB] ([CODCONSEC])
);


GO
ALTER TABLE [dbo].[RISTRADET] NOCHECK CONSTRAINT [FK_RISTRADET_ADINGRESO];


GO
ALTER TABLE [dbo].[RISTRADET] NOCHECK CONSTRAINT [FK_RISTRADET_RISTRACAB];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Hora de Transcripcion del Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'FECHORTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario de Transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Hora de la Lectura del Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'FECHORLEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud de Radiologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'CODPRORAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Hora Finalizacion del Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'FECHORFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Hora Inicio del Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'FECHORINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional Tecnologo de Radiologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'CODPROTEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora de la Orden del Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'FECHORORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud que Ordeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRADET', @level2type = N'COLUMN', @level2name = N'CODCONSEC';

