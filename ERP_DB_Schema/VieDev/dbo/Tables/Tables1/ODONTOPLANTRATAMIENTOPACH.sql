CREATE TABLE [dbo].[ODONTOPLANTRATAMIENTOPACH] (
    [ID]                         INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOCONTROL]            INT       NOT NULL,
    [IDODONTOPLANTRATAMIENTOPAC] INT       NOT NULL,
    [IDODOPARTRA]                INT       NOT NULL,
    [FECHAORDENO]                DATETIME  NOT NULL,
    [PROFESIONALORDENO]          CHAR (20) NULL,
    [ESTADO]                     INT       NULL,
    [PROFESIONALREALIZO]         CHAR (20) NULL,
    [FECHAREALIZO]               DATETIME  NULL,
    [PROFESIONALCANCELO]         CHAR (20) NULL,
    [FECHACANCELO]               DATETIME  NULL,
    CONSTRAINT [PK_ODONTOPLANTRATAMIENTOPACH] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_INPROFSAL] FOREIGN KEY ([PROFESIONALCANCELO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_INPROFSAL1] FOREIGN KEY ([PROFESIONALORDENO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_INPROFSAL2] FOREIGN KEY ([PROFESIONALREALIZO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODONTOCONTROL] FOREIGN KEY ([IDODONTOCONTROL]) REFERENCES [dbo].[ODONTOCONTROL] ([ID]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODONTOPLANTRATAMIENTOPAC] FOREIGN KEY ([IDODONTOPLANTRATAMIENTOPAC]) REFERENCES [dbo].[ODONTOPLANTRATAMIENTOPAC] ([ID]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODONTOPLANTRATAMIENTOPACH] FOREIGN KEY ([ID]) REFERENCES [dbo].[ODONTOPLANTRATAMIENTOPACH] ([ID]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODOPARTRA] FOREIGN KEY ([IDODOPARTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACH] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_INPROFSAL];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACH] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_INPROFSAL1];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACH] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_INPROFSAL2];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACH] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODONTOCONTROL];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACH] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODONTOPLANTRATAMIENTOPAC];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACH] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODONTOPLANTRATAMIENTOPACH];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACH] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODOPARTRA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de la cancelación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHACANCELO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el profesional quien cancelo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALCANCELO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha que se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHAREALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el profesional quien realizo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALREALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado  1 =  true = activo        0 = false = inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el profesional que ordeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALORDENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHAORDENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación de la tabla id odontograma tratamientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relacion con la tabla ODONTOPLANTRATAMIENTOPAC que es el id del plan de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODONTOPLANTRATAMIENTOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de relacion con la tabla de control de odontograma ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'ID';

