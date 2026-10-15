CREATE TABLE [dbo].[JUNTAMEDICA] (
    [ID]         INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]  NCHAR (10)                                                                       NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODESPECIA] CHAR (3)                                                                         NOT NULL,
    [IDMODELOHC] INT                                                                              NULL,
    CONSTRAINT [PK_JUNTAMEDICA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_JUNTAMEDICA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_JUNTAMEDICA_INESPECIA] FOREIGN KEY ([CODESPECIA]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_JUNTAMEDICA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_JUNTAMEDICA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_JUNTAMEDICA_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[JUNTAMEDICA] NOCHECK CONSTRAINT [FK_JUNTAMEDICA_ADINGRESO];


GO
ALTER TABLE [dbo].[JUNTAMEDICA] NOCHECK CONSTRAINT [FK_JUNTAMEDICA_INESPECIA];


GO
ALTER TABLE [dbo].[JUNTAMEDICA] NOCHECK CONSTRAINT [FK_JUNTAMEDICA_INPACIENT];


GO
ALTER TABLE [dbo].[JUNTAMEDICA] NOCHECK CONSTRAINT [FK_JUNTAMEDICA_INPROFSAL];


GO
ALTER TABLE [dbo].[JUNTAMEDICA] NOCHECK CONSTRAINT [FK_JUNTAMEDICA_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID modelo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la especialidad del medico con la que se registro en la junta medica. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'CODESPECIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'JUNTAMEDICA', @level2type = N'COLUMN', @level2name = N'ID';

