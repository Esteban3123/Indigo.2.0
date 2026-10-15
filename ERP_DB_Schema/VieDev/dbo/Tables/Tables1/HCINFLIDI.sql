CREATE TABLE [dbo].[HCINFLIDI] (
    [CODCONCEC]        NUMERIC (18)                                                                     NOT NULL,
    [IDETIPHIS]        CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]        NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [TIPMEZLIQ]        CHAR (1)                                                                         NOT NULL,
    [METAPLMED]        CHAR (1)                                                                         NOT NULL,
    [ABRPROMEZ]        CHAR (200)                                                                       NULL,
    [CANPROCAL]        INT                                                                              NOT NULL,
    [ESBOLMEDL]        BIT                                                                              NULL,
    [DOSISBOLO]        NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDBOL]        CHAR (3)                                                                         NULL,
    [ESLIQINFU]        BIT                                                                              NULL,
    [DOSISINFU]        NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDINF]        CHAR (3)                                                                         NULL,
    [FRECUEINF]        INT                                                                              NULL,
    [UNIFREINF]        CHAR (1)                                                                         NULL,
    [FECINIINF]        DATETIME                                                                         NULL,
    [DURACIINF]        CHAR (20)                                                                        NULL,
    [VALDURINF]        INT                                                                              NULL,
    [UNIDURINF]        CHAR (1)                                                                         NULL,
    [FECANTINF]        BIT                                                                              NULL,
    [MOTANTINF]        CHAR (250)                                                                       NULL,
    [CODPRODIL]        CHAR (20)                                                                        NULL,
    [CANPASDIL]        NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDDIL]        CHAR (3)                                                                         NULL,
    [ESBOLOMEZ]        BIT                                                                              NULL,
    [DOBOLOMEZ]        NUMERIC (18, 2)                                                                  NULL,
    [UNMEBOMEZ]        CHAR (3)                                                                         NULL,
    [VADUBOMEZ]        INT                                                                              NULL,
    [UNDUBOMEZ]        CHAR (1)                                                                         NULL,
    [ESBOLMEDM]        BIT                                                                              NULL,
    [DOSISBOLM]        NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDBOM]        CHAR (3)                                                                         NULL,
    [CODVIABOM]        CHAR (2)                                                                         NULL,
    [ESMEZINFU]        BIT                                                                              NULL,
    [UNIMEINFM]        CHAR (10)                                                                        NULL,
    [CANMEINFM]        NUMERIC (18, 2)                                                                  NULL,
    [INFTITULA]        BIT                                                                              NULL,
    [UNIMEINFT]        CHAR (10)                                                                        NULL,
    [CANMEINFT]        NUMERIC (18, 2)                                                                  NULL,
    [CANCCHORA]        NUMERIC (18, 2)                                                                  NULL,
    [CANCCHORT]        NUMERIC (18, 2)                                                                  NULL,
    [FECINIINFM]       DATETIME                                                                         NULL,
    [DURACIINFM]       CHAR (20)                                                                        NULL,
    [VALDURINFM]       INT                                                                              NULL,
    [UNIDURINFM]       CHAR (1)                                                                         NULL,
    [FECANTINFM]       BIT                                                                              NULL,
    [MOTANTINFM]       CHAR (250)                                                                       NULL,
    [INDAPLMED]        VARCHAR (MAX)                                                                    NULL,
    [FOLIOINIC]        NCHAR (10)                                                                       NOT NULL,
    [TRATNUEVO]        BIT                                                                              NOT NULL,
    [TRATMODIF]        BIT                                                                              NOT NULL,
    [MEZLIQPAC]        CHAR (500)                                                                       NOT NULL,
    [ADMMEZLIQ]        CHAR (500)                                                                       NOT NULL,
    [JUSTIFICACIONPBS] VARCHAR (2000)                                                                   NULL,
    [VIAADMDIL]        VARCHAR (20)                                                                     NULL,
    CONSTRAINT [PK_HCINFLIDI_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCINFLIDI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFLIDI_HCINFLIDI] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCINFLICI] ([CODCONCEC]),
    CONSTRAINT [FK_HCINFLIDI_HCVIAADMI] FOREIGN KEY ([VIAADMDIL]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_HCINFLIDI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCINFLIDI] NOCHECK CONSTRAINT [FK_HCINFLIDI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINFLIDI] NOCHECK CONSTRAINT [FK_HCINFLIDI_HCINFLIDI];


GO
ALTER TABLE [dbo].[HCINFLIDI] NOCHECK CONSTRAINT [FK_HCINFLIDI_HCVIAADMI];


GO
ALTER TABLE [dbo].[HCINFLIDI] NOCHECK CONSTRAINT [FK_HCINFLIDI_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la via de administración de mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VIAADMDIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mezcla - Unidad del Valor de la duracion fija de la infusion:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIDURINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mezcla - Cantidad Calculada de CC por Hora Titulable  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANCCHORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mezcla - Unidad de Medida de la infusion de la mezcla titulable :  mcg/Kg/min  mcg/min  UI/hr  mEq/hr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEINFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mezcla - Unidad de Medida de la infusion de la mezcla:  mcg/Kg/min  mcg/min  UI/hr  mEq/hr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mezcla - Unidad del Valor de la duracion fija del bolo mezcla:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNDUBOMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquido - Unidad del Valor de la duracion fija de la infusion:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIDURINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquido - Unidad del Valor de la Frecuencia de la infusion:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIFREINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Metodo de Aplicacion de Medicamento  1: Bolo Mezcla  2: Infusion Mezcla  3: Bolo Medicamento Mezcla  4: Infusion Liquido  5: Bolo medicamento Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'METAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden Medica:  1. Mezcla  2. Liquido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto por Tipo de Registro   1: Bolo Mezcla - Especifica el Diluyente  2: Infusion - Especifica el Diluyente  3: Bolo Medicamento - Especifica el Medicamento  4: Liquido - Especifica el Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';

