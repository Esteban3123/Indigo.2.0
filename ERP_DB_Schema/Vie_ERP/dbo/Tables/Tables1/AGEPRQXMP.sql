CREATE TABLE [dbo].[AGEPRQXMP] (
    [AUTONUMER] INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPROQXM] INT                                                                           NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    CONSTRAINT [PK_AGEPRQXMP] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGEPRQXMP_AGEPROQXM] FOREIGN KEY ([CODPROQXM]) REFERENCES [dbo].[AGEPROQXM] ([AUTONUMER]),
    CONSTRAINT [FK_AGEPRQXMP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AGEPRQXMP] NOCHECK CONSTRAINT [FK_AGEPRQXMP_AGEPROQXM];


GO
ALTER TABLE [dbo].[AGEPRQXMP] NOCHECK CONSTRAINT [FK_AGEPRQXMP_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CODIGO CIRUGIA PROGRAMADA MULTIPLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMP', @level2type = N'COLUMN', @level2name = N'CODPROQXM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'AUTONUMERICO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMP', @level2type = N'COLUMN', @level2name = N'AUTONUMER';

