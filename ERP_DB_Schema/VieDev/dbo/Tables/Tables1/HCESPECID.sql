CREATE TABLE [dbo].[HCESPECID] (
    [CODCONCEC]   INT                                                                                    NOT NULL,
    [CODCAMPO]    CHAR (3)                                                                               NOT NULL,
    [DESEXAME]    VARCHAR (250) MASKED WITH (FUNCTION = 'partial(0, "ExaminationPhysical_Ofuscado", 0)') NOT NULL,
    [OBLIGATORIO] BIT                                                                                    NOT NULL,
    CONSTRAINT [PK_HCESPECID] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODCAMPO] ASC),
    CONSTRAINT [FK_HCESPECID_HCESPECIC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCESPECIC] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[HCESPECID] NOCHECK CONSTRAINT [FK_HCESPECID_HCESPECIC];

