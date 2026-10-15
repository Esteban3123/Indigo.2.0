CREATE TABLE [dbo].[INESCALADT] (
    [NUMINGRES]         CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [EDADPUNTAJE]       TINYINT                                                                          NULL,
    [EDADSELECCION]     TINYINT                                                                          NULL,
    [FECHAREGIS]        DATETIME                                                                         NOT NULL,
    [CAIDPREVIA]        TINYINT                                                                          NOT NULL,
    [TRANQUILIZANTES]   TINYINT                                                                          NOT NULL,
    [DIURETICOS]        TINYINT                                                                          NOT NULL,
    [HIPOTENSORES]      TINYINT                                                                          NOT NULL,
    [ANTIPARKINSON]     TINYINT                                                                          NOT NULL,
    [ANTIDESPRESIVO]    TINYINT                                                                          NOT NULL,
    [OTROMEDICAMEN]     TINYINT                                                                          NOT NULL,
    [MEDICAMENTOS]      VARCHAR (MAX)                                                                    NULL,
    [ALTERACIONVISUAL]  TINYINT                                                                          NOT NULL,
    [ALTERACIONAUDIT]   TINYINT                                                                          NOT NULL,
    [ICTUSEXTREMIDAD]   TINYINT                                                                          NOT NULL,
    [ESTADOMENTAL]      TINYINT                                                                          NOT NULL,
    [SEGURAAYUDA]       TINYINT                                                                          NOT NULL,
    [INSEGURAAYUDA]     TINYINT                                                                          NOT NULL,
    [IMPOSIBLE]         TINYINT                                                                          NOT NULL,
    [PATOLOGIA]         TINYINT                                                                          NOT NULL,
    [ESTADONUTRICIONAL] TINYINT                                                                          NOT NULL,
    CONSTRAINT [PK_INESCALADT_1] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC),
    CONSTRAINT [FK_INESCALADT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INESCALADT_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[INESCALADT] NOCHECK CONSTRAINT [FK_INESCALADT_ADINGRESO];


GO
ALTER TABLE [dbo].[INESCALADT] NOCHECK CONSTRAINT [FK_INESCALADT_INPACIENT];

