CREATE TABLE [dbo].[HCENTREGATURNOPROFSAL] (
    [ID]                INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCENTREGATURNOC] INT                                                                           NOT NULL,
    [CODPROSAL]         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI]         CHAR (3)                                                                      NOT NULL,
    [ROLPROFESIONAL]    INT                                                                           NOT NULL,
    CONSTRAINT [PK_HCENTREGATURNOPROSAL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCENTREGATURNOPROFSAL_HCENTREGATURNOC] FOREIGN KEY ([IDHCENTREGATURNOC]) REFERENCES [dbo].[HCENTREGATURNOC] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOPROFSAL_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCENTREGATURNOPROFSAL_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCENTREGATURNOPROFSAL] NOCHECK CONSTRAINT [FK_HCENTREGATURNOPROFSAL_INESPECIA];


GO
ALTER TABLE [dbo].[HCENTREGATURNOPROFSAL] NOCHECK CONSTRAINT [FK_HCENTREGATURNOPROFSAL_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Roles   0 :  Entrega Turno  1 :  Recibe Turno  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'ROLPROFESIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo IdAutonumerico de la Entrega Turno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPROFSAL', @level2type = N'COLUMN', @level2name = N'ID';

