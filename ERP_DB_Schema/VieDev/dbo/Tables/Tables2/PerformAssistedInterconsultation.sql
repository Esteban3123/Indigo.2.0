CREATE TABLE [dbo].[PerformAssistedInterconsultation] (
    [Id]        INT                                                                           IDENTITY (1, 1) NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI] CHAR (3)                                                                      NOT NULL,
    CONSTRAINT [PK_PerformAssistedInterconsultation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_INESPECIA_PerformAssistedInterconsultation] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_INPROFSAL_PerformAssistedInterconsultation] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[PerformAssistedInterconsultation] NOCHECK CONSTRAINT [FK_INESPECIA_PerformAssistedInterconsultation];


GO
ALTER TABLE [dbo].[PerformAssistedInterconsultation] NOCHECK CONSTRAINT [FK_INPROFSAL_PerformAssistedInterconsultation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la especialidad en las que realiza interconsulta asistida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PerformAssistedInterconsultation', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del profesional que realiza interconsulta asistidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PerformAssistedInterconsultation', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PerformAssistedInterconsultation', @level2type = N'COLUMN', @level2name = N'Id';

