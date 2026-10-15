CREATE TABLE [dbo].[HCPRESCDI] (
    [CODCONCEC]        NUMERIC (18)                                                                     NOT NULL,
    [IDETIPHIS]        CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]        NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [CODVIAADM]        CHAR (3)                                                                         NOT NULL,
    [CODFORMED]        VARCHAR (20)                                                                     NOT NULL,
    [DOSISPROD]        NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]        CHAR (3)                                                                         NULL,
    [FRECUENCI]        INT                                                                              NULL,
    [UNIFRECUE]        CHAR (1)                                                                         NULL,
    [FECINIDOS]        DATETIME                                                                         NOT NULL,
    [TIPFORMED]        CHAR (1)                                                                         NOT NULL,
    [DURACIDOS]        CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ]        INT                                                                              NULL,
    [UNIDURFIJ]        CHAR (1)                                                                         NULL,
    [FECFINDOS]        DATETIME                                                                         NULL,
    [CANPEDPRO]        INT                                                                              NOT NULL,
    [PREFECANT]        BIT                                                                              NOT NULL,
    [MOTPREANT]        CHAR (250)                                                                       NULL,
    [PREESTADO]        INT                                                                              NOT NULL,
    [INDAPLMED]        VARCHAR (MAX)                                                                    NULL,
    [MANEXTPRO]        BIT                                                                              NOT NULL,
    [FOLIOINIC]        NCHAR (10)                                                                       NOT NULL,
    [TRATMODIF]        BIT                                                                              NOT NULL,
    [FORMUMANU]        BIT                                                                              NULL,
    [DESADMINI]        VARCHAR (MAX)                                                                    NULL,
    [INDAUDFOR]        NUMERIC (18)                                                                     NOT NULL,
    [DOSISPRFN]        NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMFN]        CHAR (3)                                                                         NULL,
    [OBSJUSMEE]        VARCHAR (MAX)                                                                    NULL,
    [CODJUMEES]        CHAR (3)                                                                         NULL,
    [FORMAPRESCRIBE]   TINYINT                                                                          NULL,
    [NUMERODOSIS]      TINYINT                                                                          NULL,
    [DOSISPROD1]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD2]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD3]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD4]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD5]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD6]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD7]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD8]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD9]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD10]      NUMERIC (18, 2)                                                                  NULL,
    [HORA1]            DATETIME                                                                         NULL,
    [HORA2]            DATETIME                                                                         NULL,
    [HORA3]            DATETIME                                                                         NULL,
    [HORA4]            DATETIME                                                                         NULL,
    [HORA5]            DATETIME                                                                         NULL,
    [HORA6]            DATETIME                                                                         NULL,
    [HORA7]            DATETIME                                                                         NULL,
    [HORA8]            DATETIME                                                                         NULL,
    [HORA9]            DATETIME                                                                         NULL,
    [HORA10]           DATETIME                                                                         NULL,
    [JUSTIFICARB]      VARCHAR (500)                                                                    NULL,
    [JUSTIFICACIONPBS] VARCHAR (2000)                                                                   NULL,
    CONSTRAINT [PK_HCPRESCDI] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRODUC] ASC, [MANEXTPRO] ASC),
    CONSTRAINT [FK_HCPRESCDI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCPRESCDI] NOCHECK CONSTRAINT [FK_HCPRESCDI_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Medicamento  1: Realiza Interfase con Inventarios  2: No Realiza Interfase con Inventarios x existencia del producto en el Kardex del Paciente correspondiente a la Centro de Atencion y Unidad Funcional Actual  3: No Realiza Interfase con Inventarios x no existencia del producto en el Kardez del modulo de Inventario.  4: Manejo Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';

