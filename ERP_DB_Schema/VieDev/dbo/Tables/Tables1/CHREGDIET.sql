CREATE TABLE [dbo].[CHREGDIET] (
    [CONSECUTI] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODTIPDIE] CHAR (3)                                                                         NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [OBSERVACI] VARCHAR (500)                                                                    NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_CHREGDIET] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_CHREGDIET_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHREGDIET_CHTIPDIET] FOREIGN KEY ([CODTIPDIE]) REFERENCES [dbo].[CHTIPDIET] ([CODTIPDIE]),
    CONSTRAINT [FK_CHREGDIET_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CHREGDIET_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[CHREGDIET] NOCHECK CONSTRAINT [FK_CHREGDIET_ADINGRESO];


GO
ALTER TABLE [dbo].[CHREGDIET] NOCHECK CONSTRAINT [FK_CHREGDIET_CHTIPDIET];


GO
ALTER TABLE [dbo].[CHREGDIET] NOCHECK CONSTRAINT [FK_CHREGDIET_INPacient];


GO
ALTER TABLE [dbo].[CHREGDIET] NOCHECK CONSTRAINT [FK_CHREGDIET_INPROFSAL];


GO
CREATE NONCLUSTERED INDEX [IX_CHREGDIET_[NUMINGRES]
    ON [dbo].[CHREGDIET]([NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGDIET__NUMINGRES__IPCODPACI]
    ON [dbo].[CHREGDIET]([NUMINGRES] ASC, [IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso    Nota: Este campo quedara NULL si la solicitud es para un Medico, Personal de Apoyo o Reserva. Solo se podrán realizar solicitudes de Dietas con reservas que cumplan con el criterio de hora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGDIET', @level2type = N'COLUMN', @level2name = N'NUMINGRES';

