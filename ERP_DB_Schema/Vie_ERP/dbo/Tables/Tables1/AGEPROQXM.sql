CREATE TABLE [dbo].[AGEPROQXM] (
    [AUTONUMER] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [FECHORAIN] DATETIME                                                                         NOT NULL,
    [FECHORAFI] DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_AGEPROQXM] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGEPROQXM_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGEPROQXM_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[AGEPROQXM] NOCHECK CONSTRAINT [FK_AGEPROQXM_ADCENATEN];


GO
ALTER TABLE [dbo].[AGEPROQXM] NOCHECK CONSTRAINT [FK_AGEPROQXM_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA Y HORA FINAL DE LA CIRUGIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA Y HORA INICIAL DE LA CIRUGIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'autonumerico CIRUGIA PROGRAMADA MULTIPLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'AUTONUMER';

