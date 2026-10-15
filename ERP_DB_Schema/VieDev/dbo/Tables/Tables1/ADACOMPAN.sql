CREATE TABLE [dbo].[ADACOMPAN] (
    [CONSEACOM] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDACOMPAN] VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [TIPIDACOM] CHAR (2)                                                                         NULL,
    [PRINOMBRE] VARCHAR (60) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')      NULL,
    [SEGNOMBRE] VARCHAR (60) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')     NULL,
    [PRIAPELLI] VARCHAR (60) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')   NULL,
    [SEGAPELLI] VARCHAR (60) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')  NULL,
    [DIRACOMPA] VARCHAR (100) MASKED WITH (FUNCTION = 'default()')                               NULL,
    [TELACOMPA] VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')          NULL,
    [PARACOMPA] CHAR (2)                                                                         NULL,
    [FECHAREGI] DATETIME                                                                         NOT NULL,
    [CODUSUREG] CHAR (20)                                                                        NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NULL,
    [RESPONSAB] CHAR (1)                                                                         NULL,
    CONSTRAINT [PK_ADACOMPAN] PRIMARY KEY CLUSTERED ([CONSEACOM] ASC),
    CONSTRAINT [FK_ADACOMPAN_ADACOMPAN] FOREIGN KEY ([CONSEACOM]) REFERENCES [dbo].[ADACOMPAN] ([CONSEACOM]),
    CONSTRAINT [FK_ADACOMPAN_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADACOMPAN_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADACOMPAN] NOCHECK CONSTRAINT [FK_ADACOMPAN_ADACOMPAN];


GO
ALTER TABLE [dbo].[ADACOMPAN] NOCHECK CONSTRAINT [FK_ADACOMPAN_ADINGRESO];


GO
ALTER TABLE [dbo].[ADACOMPAN] NOCHECK CONSTRAINT [FK_ADACOMPAN_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_ADACOMPAN_IPCODPACI]
    ON [dbo].[ADACOMPAN]([IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADACOMPAN_IPCODPACI_PARACOMPA_PRIAPELLI_PRINOMBRE_SEGAPELLI_SEGNOMBRE]
    ON [dbo].[ADACOMPAN]([IPCODPACI] ASC)
    INCLUDE([PARACOMPA], [PRIAPELLI], [PRINOMBRE], [SEGAPELLI], [SEGNOMBRE]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parentesco del Acompañante    1: Padre  2: Madre  3: Esposo  4: Esposa  5: Hijo(a)  6: Hermano(a)  7: Abuelo(a)  8: Tío(a)  9: Primo(a)  10: Sobrino(a)  11: Amigo(a)  12: Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'PARACOMPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo identificacion acompañante  1-Cédula de Ciudadanía  2-Cédula de Extranjería  3-Tarjeta de Identidad  4-Registro Civil  5-Pasporte  6-Adulto Sin Identificación  7-Menor Sin Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'TIPIDACOM';

