CREATE TABLE [dbo].[CHINTERFA] (
    [CONSECUTI] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [FECINIEST] DATETIME                                                                         NOT NULL,
    [FECFINEST] DATETIME                                                                         NOT NULL,
    [REGDIAEST] TINYINT                                                                          NOT NULL,
    [NUMORDSER] CHAR (10)                                                                        NOT NULL,
    [INTFECHAD] DATETIME                                                                         NOT NULL,
    [INTUSUARI] CHAR (3)                                                                         NOT NULL,
    [INTUSUIND] CHAR (3)                                                                         NOT NULL,
    [INTUSUWIN] CHAR (60)                                                                        NOT NULL,
    [INTESTRED] CHAR (60)                                                                        NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_CHINTERFA] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_CHINTERFA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHINTERFA_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[CHINTERFA] NOCHECK CONSTRAINT [FK_CHINTERFA_ADINGRESO];


GO
ALTER TABLE [dbo].[CHINTERFA] NOCHECK CONSTRAINT [FK_CHINTERFA_INPacient];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estacion de Trabajo desde donde se creo el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTESTRED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Windows que creo el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUWIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Indigo que creo el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUIND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que creo el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creacion del Documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTFECHAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Orden de Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'NUMORDSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Dias de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'REGDIAEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'FECFINEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'FECINIEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de registro  Identificador: 00000010', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

