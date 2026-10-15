CREATE TABLE [dbo].[AGESPACTI] (
    [CODESPECI] CHAR (3) NOT NULL,
    [CODACTMED] CHAR (3) NOT NULL,
    [VARCAC]    INT      NULL,
    CONSTRAINT [PK_AGESPACTI] PRIMARY KEY CLUSTERED ([CODESPECI] ASC, [CODACTMED] ASC),
    CONSTRAINT [FK_AGESPACTI_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGESPACTI_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ALTER TABLE [dbo].[AGESPACTI] NOCHECK CONSTRAINT [FK_AGESPACTI_AGACTIMED];


GO
ALTER TABLE [dbo].[AGESPACTI] NOCHECK CONSTRAINT [FK_AGESPACTI_INESPECIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si la relación especialidad actividad responderá alguna variable de la CAC, esto cuando la cita del paciente se pasa a cumplida    1: Responde variable 114.1  2: Responde variable 114.2  3: Responde variable 114.3  4: Responde variable 114.4  5: Responde variable 114.5  6: Responde variable 114.6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI', @level2type = N'COLUMN', @level2name = N'VARCAC';

