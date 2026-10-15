CREATE TABLE [dbo].[HCESCBROMA] (
    [ID]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (15)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (15)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS] DATETIME                                                                         NOT NULL,
    [ACTIVIDAD] TINYINT                                                                          NOT NULL,
    CONSTRAINT [PK_HCESCBROMA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCBROMA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCBROMA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCESCBROMA] NOCHECK CONSTRAINT [FK_HCESCBROMA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCESCBROMA] NOCHECK CONSTRAINT [FK_HCESCBROMA_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0=Flexión completa de rodillas y pies.  1=Capaz de mover rodillas.  2=Solo capaz de mover pies.  3=Incapaz de movilizar pies y rodillas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCBROMA', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';

