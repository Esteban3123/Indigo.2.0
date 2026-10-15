CREATE TABLE [dbo].[HCCTRNEUR] (
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST]  DATETIME                                                                         NOT NULL,
    [APEOJOESP]  CHAR (10)                                                                        NULL,
    [APEOJOVOZ]  CHAR (1)                                                                         NULL,
    [APEOJODOL]  CHAR (1)                                                                         NULL,
    [APEOJONIN]  CHAR (1)                                                                         NULL,
    [RESVERORI]  CHAR (1)                                                                         NULL,
    [RESVERCON]  CHAR (1)                                                                         NULL,
    [RESVERINA]  CHAR (1)                                                                         NULL,
    [RESVERINC]  CHAR (1)                                                                         NULL,
    [RESVERNIN]  CHAR (1)                                                                         NULL,
    [RESMOTOBE]  CHAR (1)                                                                         NULL,
    [RESMOTLOC]  CHAR (1)                                                                         NULL,
    [RESMOTRET]  CHAR (1)                                                                         NULL,
    [RESMOTFLE]  CHAR (1)                                                                         NULL,
    [RESMOTEXT]  CHAR (1)                                                                         NULL,
    [RESMOTFLA]  CHAR (1)                                                                         NULL,
    [VALGLASGO]  CHAR (2)                                                                         NULL,
    [PUPDERTAM]  CHAR (1)                                                                         NULL,
    [PUPDERREAC] CHAR (2)                                                                         NULL,
    [PUPIZQTAM]  CHAR (1)                                                                         NULL,
    [PUPIZQREA]  CHAR (2)                                                                         NULL,
    [FMMMSSNOR]  CHAR (1)                                                                         NULL,
    [FMMMSSDEB]  CHAR (1)                                                                         NULL,
    [FMMMSSAUS]  CHAR (1)                                                                         NULL,
    [FMMMIINOR]  CHAR (1)                                                                         NULL,
    [FMMMIIDEB]  CHAR (1)                                                                         NULL,
    [FMMMIIAUS]  CHAR (1)                                                                         NULL,
    [ESTMENALE]  CHAR (1)                                                                         NULL,
    [ESTMENSOM]  CHAR (1)                                                                         NULL,
    [ESTMENEST]  CHAR (1)                                                                         NULL,
    [ESTMENCOM]  CHAR (1)                                                                         NULL,
    [OBSGENREG]  VARCHAR (500)                                                                    NULL,
    [REFNORMAL]  CHAR (1)                                                                         NULL,
    [REFAUMENT]  CHAR (1)                                                                         NULL,
    [REFDISMIN]  CHAR (1)                                                                         NULL,
    [REFBABYNS]  CHAR (1)                                                                         NULL,
    [RESNORMAL]  CHAR (1)                                                                         NULL,
    [RESCHEYNE]  CHAR (1)                                                                         NULL,
    [RESKUSSMA]  CHAR (1)                                                                         NULL,
    [RESPIBIOT]  CHAR (1)                                                                         NULL,
    [RESAPNEUS]  CHAR (1)                                                                         NULL,
    [RESPARRES]  CHAR (1)                                                                         NULL,
    [RESASIRES]  CHAR (1)                                                                         NULL,
    [POSCABPAC]  CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCCTRNEUR] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [FECREGIST] ASC),
    CONSTRAINT [FK_HCCTRNEUR_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRNEUR_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRNEUR_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRNEUR_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRNEUR_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCTRNEUR] NOCHECK CONSTRAINT [FK_HCCTRNEUR_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCTRNEUR] NOCHECK CONSTRAINT [FK_HCCTRNEUR_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCTRNEUR] NOCHECK CONSTRAINT [FK_HCCTRNEUR_INPACIENT];


GO
ALTER TABLE [dbo].[HCCTRNEUR] NOCHECK CONSTRAINT [FK_HCCTRNEUR_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCTRNEUR] NOCHECK CONSTRAINT [FK_HCCTRNEUR_INUNIFUNC];

